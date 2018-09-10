using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public interface IMemberServices : IDisposable
    {
        Task<Member> Register(Member member, string password);
        Task<Member> Login(string username, string emailAddress, string password);
        Task<bool> MemberExists(string emailAddress, string username);
        Task<MemberDto> GetMemberByID(long ID);
        Task<MemberDto> GetMemberByUsername(string username);

        Task<IEnumerable<MemberDto>> GetMemberByColorID(int id);
    }
}