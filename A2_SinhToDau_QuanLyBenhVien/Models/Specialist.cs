using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class Specialist
    {
        public Guid Id { get; set; }
        [Required]
        public string SpecialistName { get; set; }
        [Required]
        public string SpecialistDescription { get; set; }
        [Required]
        public byte[] SpecialistImg {  get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
        public virtual List<Doctor>? Doctors { get; set; }
    }
}
