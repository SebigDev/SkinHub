using Microsoft.EntityFrameworkCore;
using SkinHubApp.Models;

namespace SkinHubApp.Data
{
    public class SkinHubAppDbContext : DbContext
    {
        public SkinHubAppDbContext(DbContextOptions<SkinHubAppDbContext> options): base(options)
        { }

        public DbSet<GenderType> GenderType {get; set;}

        public DbSet<ColorType> ColorType {get; set;}

        public DbSet<ProductType> ProductType {get; set;}

        public DbSet<ProductListType> ProductListType {get; set;}
    }
}