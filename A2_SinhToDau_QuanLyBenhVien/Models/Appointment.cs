using Microsoft.AspNetCore.Components.Routing;
using System.ComponentModel.DataAnnotations;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string Reason { get; set; }

        public string Note { get; set; }
        public Guid DoctorsId { get; set; }
        public virtual Doctor? Doctors { get; set; }
    }
}
