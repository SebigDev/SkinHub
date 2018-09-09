using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkinHubApp.DTOs;
using SkinHubApp.Models;
using SkinHubApp.Services;

namespace SkinHubApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly IMemberServices _authServices;

      //Constructor for Dependency Injection
      public MemberController(IMemberServices authServices)
      {
          _authServices = authServices;
      }

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(MemberDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
         public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
      {
          registerDto.Username = registerDto.Username.ToLower();
          try
          {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
 
                if(await _authServices.MemberExists(registerDto.Username, registerDto.EmailAddress))
                {
                    return BadRequest("Membername or Email Address exists, Choose anothe name");
                }
                var memberToCreate = new Member
                {
                    Username = registerDto.Username,
                    EmailAddress = registerDto.EmailAddress,
                    Color = registerDto.Color,
                    Gender = registerDto.Gender,
                    Firstname = registerDto.Firstname,
                    Middlename = registerDto.Middlename,
                    Lastname = registerDto.Lastname,
                    DateOfBirth = registerDto.DateOfBirth,


                };
                var createMember = await _authServices.Register(memberToCreate, registerDto.Password);
                return StatusCode(201, $" Hello {registerDto.Username}, Your Registration was Successful.");

          }
          catch (Exception)
          {
             return BadRequest("Error!- Member cannot be created, Contact Administrator");
          }
      }

    [HttpPost]
    [Route("[action]")]
    [Produces(typeof(MemberDto))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var logMember = await _authServices.Login(loginDto.Username.ToLower(),loginDto.EmailAddress, loginDto.Password);
            if(logMember == null)
            {
                return Unauthorized();
            }
            //Generating Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Super Secret Key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
               Subject =  new System.Security.Claims.ClaimsIdentity(new Claim []
               {
                   new Claim(ClaimTypes.NameIdentifier, logMember.ID.ToString()),
                   new Claim(ClaimTypes.Name, logMember.Username),
                   new Claim(ClaimTypes.Email, loginDto.EmailAddress),
               }),
               Expires = DateTime.Now.AddDays(2),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok( new {tokenString});
        }
        catch (Exception)
        {
          return StatusCode(400, "Error Processing this request, please contact the Administration");
        }
    }
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(MemberDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMemberByID(long ID)
    {
       try
       {
            var memberByID = await _authServices.GetMemberByID(ID);
            if(memberByID != null)
            {
                return Ok(memberByID);
            }
            return NotFound($"Member with ID: {ID} not found");
       }
       catch (Exception)
       {
          return BadRequest("Error!  Request cannot be completed, Please contact administrator");
       }
    }

    [HttpGet]
    [Route("[action]")]
    [Produces(typeof(MemberDto))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetMemberByMembername(string username)
    {
       try
       {
            var MemberByMembername = await _authServices.GetMemberByUsername(username);
            if(MemberByMembername != null)
            {
                return Ok(MemberByMembername);
            }
            return NotFound($"Member with Membername: {username} not found");
       }
       catch (Exception)
       {
          return BadRequest("Error!  Request cannot be completed, Please contact administrator");
       }
    }
    }
    
}