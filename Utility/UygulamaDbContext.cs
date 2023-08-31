using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUygulamaProje1.Models;

namespace WebUygulamaProje1.Utility
{
    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {
            
        }
        public DbSet<KitapTuru> kitap_turleri { get; set; }
        public DbSet<Kitap> kitaplar { get; set; }
        public DbSet<Kiralama> kiralamalar { get; set; }
        public DbSet<ApplicationUser> applicationUser { get; set; }
    }
}
