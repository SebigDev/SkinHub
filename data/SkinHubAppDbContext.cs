using Microsoft.EntityFrameworkCore;
using SkinHubApp.Models;

namespace SkinHubApp.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SkinHubAppDbContext : DbContext
    {
    
        public SkinHubAppDbContext(DbContextOptions<SkinHubAppDbContext> options): base(options)
        { }

        public DbSet<GenderType> GenderType {get; set;}

        public DbSet<ColorType> ColorType {get; set;}

        public DbSet<ProductType> ProductType {get; set;}

        public DbSet<ProductListType> ProductListType {get; set;}


        public DbSet<Post> Post {get; set;}

        public DbSet<Comment> Comment {get; set;}

        public DbSet<Reply> Reply {get; set;}


        public DbSet<Member> Member {get; set;}

    }
}