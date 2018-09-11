using System;
using System.Collections.Generic;
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

/// <summary>
/// Creates a New Member
/// </summary>
/// <param name="registerDto"></param>
/// <returns>Returns the member created </returns>
        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(MemberDto))]
        [ProducesResponseType(201)]
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
                    return BadRequest("Username or Email Address exists, Choose anothe name");
                }
                var memberToCreate = new Member
                {
                    Username = registerDto.Username,
                    EmailAddress = registerDto.EmailAddress,
                    ColorTypeID = registerDto.ColorTypeID,
                    Gender = registerDto.Gender,
                    Firstname = registerDto.Firstname,
                    Middlename = registerDto.Middlename,
                    Lastname = registerDto.Lastname,
                    DateOfBirth = registerDto.DateOfBirth,

                };
                var createMember = await _authServices.Register(memberToCreate, registerDto.Password);
                return StatusCode(201, $" Hello {registerDto.Username}, Your Registration was Successful.");

          }
          catch (Exception ex)
          {
             return BadRequest($"{ex.Message}, Error!- Member cannot be created, Contact Administrator");
          }
      }


/// <summary>
/// Logs in a Member
/// </summary>
/// <param name="loginDto"></param>
/// <returns>returns a token  for the member</returns>
    [HttpPost]
    [Route("[action]")]
    [Produces(typeof(MemberDto))]
    [ProducesResponseType(200)]
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

    /// <summary>
    /// Retrieves Member by Member Identity
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>returns a member by Id</returns>
    [HttpGet]
    [Route("[action]")]
    [Produces(typeof(MemberDto))]
    [ProducesResponseType(200)]
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


/// <summary>
/// Retrieves Member by Member's Username
/// </summary>
/// <param name="username"></param>
/// <returns>returns a member by username</returns>
    [HttpGet]
    [Route("[action]")]
    [Produces(typeof(MemberDto))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetMemberByUsername(string username)
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

/// <summary>
/// Retrieves Member By Member Color Identity
/// </summary>
/// <param name="id"></param>
/// <returns>returns a member by color Id</returns>
    [HttpGet]
    [Route("[action]")]
    [Produces(typeof(IEnumerable<MemberDto>))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetMemberByColorID(int id)
    {
       try
       {
            var MemberByMembername = await _authServices.GetMemberByColorID(id);
            if(MemberByMembername != null)
            {
                return Ok(MemberByMembername);
            }
            return NotFound($"Member with Color ID: {id} not found");
       }
       catch (Exception)
       {
          return BadRequest("Error!  Request cannot be completed, Please contact administrator");
       }
    }

    } 
}