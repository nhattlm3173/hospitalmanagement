using Microsoft.AspNetCore.Components.Routing;
using System.ComponentModel.DataAnnotations;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class DrImg
    {
        public Guid Id { get; set; }
        [Required]
        public string UrlImage { get; set; }
        public Guid DoctorsId { get; set; }
        public virtual Doctor? Doctors { get; set; }
    }
}
