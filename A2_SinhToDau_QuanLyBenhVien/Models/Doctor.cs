using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        [Required]
        public string DrName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] AvtImage { get; set; }
        [Required]
        public string Experience { get; set; }
        public Guid SpecialistId { get; set; }
        public virtual Specialist? Specialist { get; set; }
        public virtual List<Degree>? Degree { get; set; }
        public virtual List<DrImg>? DrImgs { get; set; }
        public virtual List<Appointment>? Appointment { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
