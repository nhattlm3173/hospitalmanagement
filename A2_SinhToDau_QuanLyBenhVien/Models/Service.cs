using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        [Required]
        public string SVName { get; set; }
        [Required]
        public string SVTeaser { get; set; }
        [Required]
        public string SVDescription { get; set; }
        [Required]
        public byte[] MainImg { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
