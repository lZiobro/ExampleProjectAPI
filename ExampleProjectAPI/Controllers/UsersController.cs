using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model.Entities;
using Service.Abstract;
using Service.Concrete;
using Service.DtoModel.DtoIn;
using Service.DtoModel.DtoOut;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ExampleProjectAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthenticationSettings _authenticationSettings;
        //as we are not using a service, which would handle dtos, we need to actually map it here
        private readonly IMapper _mapper;
        public UsersController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment, SignInManager<ApplicationUser> signInManager, AuthenticationSettings authenticationSettings, IMapper mapper)
        {
            this._userManager = userManager;
            this._webHostEnvironment = webHostEnvironment;
            this._signInManager = signInManager;
            this._authenticationSettings = authenticationSettings;
            this._mapper = mapper;
        }
        #region helperMethods
        private async Task<FileContentResult?> getProfilePictureAsync(ApplicationUser user)
        {
            var path = _webHostEnvironment.ContentRootPath + "uploads\\images\\users\\" + user.UserName + '\\';

            if (!Directory.Exists(path)) return null;

            var directory = new DirectoryInfo(path);

            var myFile = directory.GetFiles()
                .Where(f => f.Name.StartsWith("pfp"))
                .OrderByDescending(f => f.LastWriteTime)
                .First();

            Byte[] b = await System.IO.File.ReadAllBytesAsync(myFile.FullName);
            return File(b, "image/jpeg");
        }
        
        private async Task<ApplicationUser?> findUserByNameWithDetailsAsync(string userName)
        {
            //we wont get userDetails this way
            //var user = await _userManager.FindByNameAsync(userName);

            //https://github.com/dotnet/aspnetcore/blob/main/src/Identity/EntityFrameworkCore/src/UserStore.cs
            var user = await _userManager.Users.Include(u => u.Details).FirstOrDefaultAsync(u => u.NormalizedUserName == userName);

            return user;
        }
        #endregion
        [EnableCors("CorsPolicy")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewApplicationUserAsync(ApplicationUserInDto applicationUserInDto)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(applicationUserInDto);

            applicationUser.UserName = applicationUserInDto.UserName;
            applicationUser.Email = applicationUser.UserName;
            applicationUser.Details.UserId = applicationUser.Id;

            var result = await _userManager.CreateAsync(applicationUser, applicationUserInDto.Password);

            return result.Succeeded ? Ok() : BadRequest();
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(ApplicationUserInDto applicationUserInDto)
        {
            var applicationUser = await _userManager.FindByNameAsync(applicationUserInDto.UserName);

            if (applicationUser == null) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(applicationUser, applicationUserInDto.Password, false, false);

            if (!result.Succeeded) return BadRequest();

            var userRoles = await _userManager.GetRolesAsync(applicationUser);
            var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, applicationUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var token = new JwtSecurityToken(
                    issuer: _authenticationSettings.JwtIssuer,
                    audience: _authenticationSettings.JwtAudience,
                    expires: DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        private async Task<IActionResult> DeleteUserAsync(string userName)
        {
            var user = await _userManager.Users.Include(u => u.Details).Include(u => u.Messages).Include(u => u.SendMessages).FirstOrDefaultAsync(u => u.NormalizedUserName == userName);
            if (user == null) return NotFound();
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Ok() : BadRequest();
        }


        [EnableCors("CorsPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetUserWithDetailsAsync(string userName)
        {
            var user = await findUserByNameWithDetailsAsync(userName);

            if (user == null) return BadRequest("user not found");
            if (!user.isListed && userName != User?.Identity?.Name) return Unauthorized();

            var pfpFile = await getProfilePictureAsync(user);

            var applicationUser = _mapper.Map<ApplicationUserOutDto>(user);
            return Ok(new { user = applicationUser, pfp = pfpFile });
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("getListedUsers")]
        /* Pretty bad implementation of pagination, but works */
        public async Task<IActionResult> GetListedUsersAsync(int? page, string? race, string? occupation, string? experience)
        {
            var pagesize = 10;
            if (page == null) page = 0;
            if (page > 0) page -= 1;

            var result = _userManager.Users.Where(x => x.isListed == true);

            if (result == null) return NotFound();
            if (race != null && race != "any") result = result.Where(x => x.Details.Race.Contains(race));
            if (occupation != null && occupation != "any") result = result.Where(x => x.Details.Race.Contains(occupation));
            if (experience != null && experience != "any") result = result.Where(x => x.Details.Race.Contains(experience));

            int userCount = result.Count();

            result = result.OrderByDescending(x => x.Id);

            result = result.Skip((int)page * pagesize);

            result = result.Take(pagesize);

            var userFinal = await result.Include(x => x.Details).ToListAsync();

            var profilePictures = new List<FileContentResult>();

            foreach(var user in userFinal)
            {
                profilePictures.Add(await getProfilePictureAsync(user));
            }

            var applicationUsers = _mapper.Map<List<ApplicationUserOutDto>>(userFinal);

            var final = applicationUsers.Zip(profilePictures, (first, second) => new {user=first, pfp=second});
            return Ok(new
            {
                usersWithPictures = final,
                userCount = userCount
            });
        }







        [EnableCors("CorsPolicy")]
        [HttpPost("upload/profilePicture")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePictureAsync(IFormFile profilePicture)
        {
            if (profilePicture?.Length == 0) return BadRequest("Empty file");

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return Unauthorized();
            var path = _webHostEnvironment.ContentRootPath + "uploads\\images\\users\\" + User.Identity.Name + '\\';

            var imageName = "pfp" + DateTime.Now.ToString("yyyyMMddhhmmfff") + ".jpg";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var filestream = new FileStream(path + imageName, FileMode.Create))
            {
                await profilePicture.CopyToAsync(filestream);
                await filestream.FlushAsync();
            }
            return Ok();
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("profilePicture")]
        public async Task<IActionResult> GetProfilePictureAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return BadRequest("user not found");

            var pfp = await getProfilePictureAsync(user);
            return Ok(pfp);
        }

        [EnableCors("CorsPolicy")]
        [HttpPost("editDetails")]
        [Authorize]
        public async Task<IActionResult> EditUserDetailsAsync(ApplicationUserInDto? applicationUserInDto)
        {
            var user = await findUserByNameWithDetailsAsync(User.Identity.Name);

            //configure automapper with ignoring nulls?

            if (applicationUserInDto.Race != null) user.Details.Race = applicationUserInDto.Race;
            if (applicationUserInDto.Occupation != null) user.Details.Occupation = applicationUserInDto.Occupation;
            if (applicationUserInDto.Experience != null) user.Details.Experience = applicationUserInDto.Experience;
            if (applicationUserInDto.Home != null) user.Details.Home = applicationUserInDto.Home;
            if (applicationUserInDto.hasEquipment != null) user.Details.HasEquipment = (bool)applicationUserInDto.hasEquipment;
            if (applicationUserInDto.Likes != null) user.Details.Likes = applicationUserInDto.Likes;
            if (applicationUserInDto.Dislikes != null) user.Details.Dislikes = applicationUserInDto.Dislikes;
            if (applicationUserInDto.Specialty != null) user.Details.Specialty = applicationUserInDto.Specialty;
            if (applicationUserInDto.AboutMe != null) user.Details.AboutMe = applicationUserInDto.AboutMe;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? Ok() : BadRequest();
        }


        [EnableCors("CorsPolicy")]
        [HttpPost("setListing")]
        [Authorize]
        public async Task<IActionResult> SetListingAsync([FromBody]bool value)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null) return BadRequest("user not found");

            Console.WriteLine("Value: " + value);

            user.isListed = value;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? Ok() : BadRequest();
        }
    }
}
