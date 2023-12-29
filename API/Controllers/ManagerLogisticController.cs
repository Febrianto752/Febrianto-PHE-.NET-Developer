using DataAccess.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels.Account;
using System.Net;
using Utilities.Enums;
using Utilities.Handlers;

namespace API.Controllers
{
    [Route("api/manager-logistics")]
    [ApiController]
    public class ManagerLogisticController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public ManagerLogisticController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var managerLogistics = _accountRepository.GetAll().Where(a => a.Role == nameof(RoleEnum.Manager));

            if (managerLogistics == null)
            {
                return NotFound(new ResponseHandler<string>()
                {
                    Code = 404,
                    Status = "Not Found",
                    Message = "Manager Logistics Not Found",
                    Data = null
                });
            }

            var managerLogisticsVM = managerLogistics.Select(ml => new GetAccountVM
            {
                Guid = ml.Guid,
                Name = ml.Name,
                Email = ml.Email,
                NoTelp = ml.NoTelp,
                Role = ml.Role,
            });

            return Ok(new ResponseHandler<IEnumerable<GetAccountVM>>()
            {
                Code = 200,
                Status = "Data Found",
                Message = "Manager Logistics Found",
                Data = managerLogisticsVM
            });
        }

        [HttpPost]
        public IActionResult Create(CreateAccountVM createAccountVM)
        {
            var emailIsExist = _accountRepository.GetByEmail(createAccountVM.Email);

            if (emailIsExist is not null)
            {
                return BadRequest(new ResponseHandler<string>()
                {
                    Code = 400,
                    Status = "Bad Request",
                    Message = "Email Already Exist",
                    Data = null
                });
            }

            var newAccount = new Account()
            {
                Guid = Guid.NewGuid(),
                Name = createAccountVM.Name,
                Email = createAccountVM.Email,
                Password = HashingHandler.HashPassword(createAccountVM.Password),
                NoTelp = createAccountVM.NoTelp,
                Role = nameof(RoleEnum.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var account = _accountRepository.Create(newAccount);

            if (account == null)
            {
                return BadRequest(new ResponseHandler<string>()
                {
                    Code = 400,
                    Status = "Bad Request",
                    Message = "Manager Logistics Not Found",
                    Data = null
                });
            }

            return Created("", new ResponseHandler<Account>()
            {
                Code = 201,
                Status = "Data Created",
                Message = "Manager Logistics has been created",
                Data = newAccount
            });
        }

        [HttpPut("{guid}")]
        public IActionResult Edit(Guid guid, [FromBody] EditAccountVM editAccountVM)
        {
            var account = _accountRepository.GetByGuid(guid);

            if (account == null)
            {
                return NotFound(new ResponseHandler<string>()
                {
                    Code = 404,
                    Status = "Not Found",
                    Message = "Account Not Found",
                    Data = null
                });
            }

            try
            {
                account.Name = editAccountVM.Name;
                account.NoTelp = editAccountVM.NoTelp;
                account.ModifiedDate = DateTime.Now;

                _accountRepository.SaveChanges();

                return Ok(new ResponseHandler<string>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Successfully updated",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseHandler<string>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check your data"
                });
            }

        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);

            if (account == null)
            {
                return NotFound(new ResponseHandler<string>()
                {
                    Code = 404,
                    Status = "Not Found",
                    Message = "Account Not Found",
                    Data = null
                });
            }
            try
            {
                _accountRepository.Delete(account);

                return Ok(new ResponseHandler<string>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Successfully deleted"
                });
            }
            catch
            {
                return BadRequest(new ResponseHandler<string>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Something went wrong"
                });
            }

        }

    }
}
