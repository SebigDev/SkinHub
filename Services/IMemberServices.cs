using System;
using System.Threading.Tasks;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public interface IMemberServices : IDisposable
    {
        Task<Member> Register(Member member, string password);
        Task<Member> Login(string username, string emailAddress, string password);
        Task<bool> MemberExists(string emailAddress, string username);
        Task<Member> GetMemberByID(long ID);
        Task<Member> GetMemberByUsername(string username);
    }
}