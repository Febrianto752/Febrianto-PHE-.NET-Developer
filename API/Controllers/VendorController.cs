using DataAccess.Data;
using DataAccess.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Models.ViewModels.Vendor;
using Utilities.Enums;
using Utilities.Handlers;

namespace API.Controllers
{
    [Route("api/vendors")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ApplicationDbContext _db;
        public VendorController(IAccountRepository accountRepository, IVendorRepository vendorRepository, ApplicationDbContext db)
        {
            _accountRepository = accountRepository;
            _vendorRepository = vendorRepository;
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Vendor> vendors = _vendorRepository.GetAll(includeProperties: "Account").ToList();


            if (vendors == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }


            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Data found",
                Data = vendors
            });
        }

        [HttpGet("{guid}")]
        public IActionResult Details(Guid guid)
        {
            var vendor = _vendorRepository.GetByGuid(v => v.Guid == guid, includeProperties: "Account");


            if (vendor == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }


            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "Data found",
                Data = vendor
            });
        }

        [HttpPost]
        public IActionResult Create([FromForm] CreateVendorVM createVendorVM)
        {
            var emailIsExist = _accountRepository.GetByEmail(createVendorVM.CreateAccountVM.Email);

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
                Name = createVendorVM.CreateAccountVM.Name,
                Email = createVendorVM.CreateAccountVM.Email,
                Password = HashingHandler.HashPassword(createVendorVM.CreateAccountVM.Password),
                NoTelp = createVendorVM.CreateAccountVM.NoTelp,
                Role = nameof(RoleEnum.Vendor),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };

            var transaction = _db.Database.BeginTransaction();

            var account = _accountRepository.Create(newAccount);

            if (account == null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Check your field"
                });
            }

            var newVendor = new Vendor()
            {
                Guid = Guid.NewGuid(),
                ProfileImage = "",
                AccountGuid = account.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = VendorStatusEnum.Pending,
            };
            var fileName = "";
            var filePath = "";
            var directoryLocation = "";
            try
            {
                if (createVendorVM.ProfileImage != null)
                {

                    fileName = newVendor.Guid + Path.GetExtension(createVendorVM.ProfileImage.FileName);
                    filePath = @"wwwroot\images\profiles\" + fileName;

                    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                    directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    FileInfo file = new FileInfo(directoryLocation);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        createVendorVM.ProfileImage.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    newVendor.ProfileImage = baseUrl + "/images/profiles/" + fileName;

                }
                else
                {
                    newVendor.ProfileImage = "/images/profiles/default_image.jpg";
                }

            }
            catch
            {

                if (System.IO.File.Exists(directoryLocation))
                {
                    System.IO.File.Delete(directoryLocation);
                }

                transaction.Rollback();
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "file failed uplaoded"
                });
            }



            var vendor = _vendorRepository.Create(newVendor);

            if (vendor == null)
            {
                transaction.Rollback();
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "data failed created"
                });
            }

            transaction.Commit();

            return Created("", new ResponseVM
            {
                IsSuccess = true,
                Message = "Data created",
                Data = newAccount
            });
        }

        [HttpPut("{guid}/approved-by-admin")]
        public IActionResult ApprovedByAdmin(Guid guid)
        {
            var vendor = _vendorRepository.GetByGuid(v => v.Guid == guid);

            if (vendor == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            vendor.Status = VendorStatusEnum.ApproveByAdmin;
            _vendorRepository.SaveChanges();

            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "vendor approved by admin"
            });
        }

        [HttpPut("{guid}/approved-by-manager")]
        public IActionResult ApprovedByManager(Guid guid)
        {
            var vendor = _vendorRepository.GetByGuid(v => v.Guid == guid);

            if (vendor == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            vendor.Status = VendorStatusEnum.Accepted;
            _vendorRepository.SaveChanges();

            return Ok(new ResponseVM
            {
                IsSuccess = true,
                Message = "vendor approved by manager, account is activated"
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var vendor = _vendorRepository.GetByGuid(v => v.Guid == guid);
            if (vendor == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            var accountVendor = _accountRepository.GetByGuid(a => a.Guid == vendor.Guid);

            if (accountVendor == null)
            {
                return NotFound(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Data not found"
                });
            }

            try
            {
                _vendorRepository.Delete(vendor);
                _accountRepository.Delete(accountVendor);

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