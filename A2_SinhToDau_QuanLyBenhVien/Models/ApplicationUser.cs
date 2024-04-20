using Microsoft.AspNetCore.Identity;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public List<New>? News { get; set; }
        public List<Service>? Services { get; set; }
        public List<Specialist>? Specialists { get; set; }
        public List<Doctor>? Doctors { get; set; }
    }
}
