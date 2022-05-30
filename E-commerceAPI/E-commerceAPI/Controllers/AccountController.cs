using E_commerceAPI.DTO;
using E_commerceAPI.Model;
using E_commerceAPI.Repository.AddressRepository;
using E_commerceAPI.Repository.AdminRepo;
using E_commerceAPI.Repository.PhoneRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_commerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<AppUser> UserManager;
        public IConfiguration Configuration;
        public IAddressRepository addressRepository;
        public IPhoneRepository PhoneRepository;
        private IAdminRepository adminRepository;

        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration,
            IAddressRepository _addressRepository,
            IPhoneRepository _PhoneRepository,
            IAdminRepository AdminRepository)
        {
            UserManager = userManager;
            Configuration = configuration;
            addressRepository = _addressRepository;
            PhoneRepository = _PhoneRepository;
            adminRepository = AdminRepository;
        }

        [HttpGet("{UserID}")]
        //[Route("GetUser")]
        public async Task<IActionResult> GetUser(string UserID)
        {
            try
            {
                if(UserID != null)
                {
                    var appUser = await UserManager.FindByIdAsync(UserID);
                    UserDTO userDTO = new UserDTO()
                    {
                        Address = addressRepository.GetById(appUser.AddressId),
                        Email = appUser.Email,
                        Full_Name = appUser.UserName,
                        id = appUser.Id,
                        password = appUser.UserName
                    };
                    foreach (var item in PhoneRepository.GetPhones(appUser.Id))
                    {
                        userDTO.Mobile_number.Add(item.PhoneNumber);
                    }
                    return Ok(userDTO);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("{UserEditID}")]
        //[Route("GetUser")]
        public async Task<IActionResult> EditUser(string UserEditID, UserDTO user)
        {
            try
            {
                if (UserEditID != null)
                {
                    var appUser = await UserManager.FindByIdAsync(UserEditID);
                    List<Phone> phones = new List<Phone>();
                    UserDTO userDTO = new UserDTO()
                    {
                        Address = addressRepository.GetById(appUser.AddressId),
                        Email = appUser.Email,
                        Full_Name = appUser.UserName,
                        id = appUser.Id,
                        password = appUser.PasswordHash
                    };
                    //foreach (var item in userDTO.Mobile_number)
                    //{
                    //    phones.Add(new Phone() { AppUserId = appUser.Id, PhoneNumber = item });
                    //}

                    Address add = addressRepository.GetById(appUser.AddressId);
                    add.street = user.Address.street;
                    add.postalCode = user.Address.postalCode;
                    add.City = user.Address.City;
                    appUser.Full_Name = user.Full_Name;
                    addressRepository.Update(appUser.AddressId, add);
                    appUser.Email = user.Email;
                    appUser.PasswordHash = user.password;
                    //appUser.Mobile_number = user.N;
                    IdentityResult result = await UserManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        //foreach (var item in phones)
                        //{
                        //    PhoneRepository.GetPhones(appUser.Id);
                        //    PhoneRepository.Update()
                        //}
                        return Ok(userDTO);
                    }
                    else
                    {
                        return BadRequest();
                    }
                    
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet("GetUsers")]
        //public async Task<IActionResult> GetUsers()
        //{
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationDTO registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser appUser = new AppUser()
                    {
                        UserName = registration.Full_Name,
                        Email = registration.Email,
                        PasswordHash = registration.password,
                        Id = Guid.NewGuid().ToString(),
                    };
                    Address address = new Address()
                    {
                        City = registration.Address.City,
                        postalCode = registration.Address.postalCode,
                        street = registration.Address.street
                    };

                    if (addressRepository.Insert(address) > 0)
                    {
                        appUser.AddressId = address.id;
                        IdentityResult result = await UserManager.CreateAsync(appUser, registration.password);
                        
                        if (result.Succeeded)
                        {
                            foreach (var item in registration.Mobile_number)
                            {
                                Phone phone = new Phone()
                                {
                                    PhoneNumber = item,
                                    AppUserId = appUser.Id
                                };
                                PhoneRepository.Insert(phone);
                            }
                            return Ok(appUser);
                        }
                        else
                        {
                            return BadRequest(result.Errors);
                        }
                    }
                    else
                    {
                        return BadRequest("Address Error");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("logIn")]
        public async Task<IActionResult> logIn(LoginDTO logIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                AppUser user = await UserManager.FindByNameAsync(logIn.UserName);
                if (user != null && await UserManager.CheckPasswordAsync(user, logIn.Password))
                {
                    var roles = await UserManager.GetRolesAsync(user);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, logIn.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]));
                    var token = new JwtSecurityToken(

                       issuer: Configuration["JWT:ValidationISS"],
                       audience: Configuration["JWT:ValidationAud"],
                       claims: claims,
                       expires: DateTime.Now.AddHours(1),
                       signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        UserId = user.Id,
                        UserName = user.Full_Name
                    });
                }
                return Unauthorized();
            }
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(UserDTO newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AppUser UserModel = new AppUser();
                    UserModel.UserName = newUser.Full_Name;
                    UserModel.PasswordHash = newUser.password;
                    //UserModel.Address = newUser.Address;
                    //UserModel.PhoneNumber = newUser.PhoneNumber;
     
                    Address address = new Address()
                    {
                        City = newUser.Address.City,
                        postalCode = newUser.Address.postalCode,
                        street = newUser.Address.street
                    };

                    if (addressRepository.Insert(address) > 0)
                    {
                        UserModel.AddressId = address.id;
                        IdentityResult result =
                            await UserManager.CreateAsync(UserModel, UserModel.PasswordHash);
                        if (result.Succeeded)
                        {
                            IdentityResult RoleResult = await UserManager.AddToRoleAsync(UserModel, "Admin");
                            if (RoleResult.Succeeded)
                            {
                                //Admin admin = new Admin()
                                //{
                                //    Name = newUser.Full_Name,
                                //    Password = newUser.password
                                //};
                                //adminRepository.Insert(admin);
                                return Ok(newUser);
                                //return RedirectToAction("Index", "Home", categoryRepository.GetAll());
                            }
                            else
                            {
                                return BadRequest();

                            }
                        }

                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }

                }
                return BadRequest();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPost("logInAdmin")]
        public async Task<IActionResult> logInAdmin(LoginDTO logInAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                AppUser user = await UserManager.FindByNameAsync(logInAdmin.UserName);
                if (user != null && await UserManager.CheckPasswordAsync(user, logInAdmin.Password))
                {
                    var roles = await UserManager.GetRolesAsync(user);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, logInAdmin.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]));
                    var token = new JwtSecurityToken(

                       issuer: Configuration["JWT:ValidationISS"],
                       audience: Configuration["JWT:ValidationAud"],
                       claims: claims,
                       expires: DateTime.Now.AddHours(1),
                       signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        UserId = user.Id,
                        UserName = user.Full_Name
                    });
                }
                return Unauthorized();
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAdmin()
        {
            try
            {
                var admins = adminRepository.GetAll();
                if(admins != null)
                {
                    return Ok(admins);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
