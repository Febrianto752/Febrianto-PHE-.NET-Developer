using DataAccess.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
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

        [HttpGet, Authorize]
        public IActionResult Index()
        {
            var managerLogistics = _accountRepository.GetAll().Where(a => a.Role == nameof(RoleEnum.Manager));

            if (managerLogistics == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
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

            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Data found",
                Data = managerLogisticsVM
            });
        }

        [HttpPost]
        public IActionResult Create(CreateAccountVM createAccountVM)
        {
            var emailIsExist = _accountRepository.GetByEmail(createAccountVM.Email);

            if (emailIsExist is not null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Email already exist"
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
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Check your field"
                });
            }

            return Created("", new ResponseVM
            {
                IsSuccess = true,
                Message = "Data created",
                Data = newAccount
            });
        }

        [HttpPut("{guid}")]
        public IActionResult Edit(Guid guid, [FromBody] EditAccountVM editAccountVM)
        {
            var account = _accountRepository.GetByGuid(guid);

            if (account == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            try
            {
                account.Name = editAccountVM.Name;
                account.NoTelp = editAccountVM.NoTelp;
                account.ModifiedDate = DateTime.Now;

                _accountRepository.SaveChanges();

                return Ok(new ResponseVM
                {
                    IsSuccess = true,
                    Message = "updated successfully"
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
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }
            try
            {
                _accountRepository.Delete(account);

                return Ok(new ResponseVM
                {
                    IsSuccess = true,
                    Message = "Successfully deleted data"
                });
            }
            catch
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                });
            }

        }

    }
}
