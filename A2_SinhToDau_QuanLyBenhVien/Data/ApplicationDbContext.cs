using A2_SinhToDau_QuanLyBenhVien.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace A2_SinhToDau_QuanLyBenhVien.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DrImg> DrImgs { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Specialist> Specialists { get; set; }

    }
}
