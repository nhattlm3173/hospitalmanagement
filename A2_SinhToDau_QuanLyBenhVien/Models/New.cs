using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A2_SinhToDau_QuanLyBenhVien.Models
{
    public class New
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string NewsTeaser { get; set; }
        [Required]
        public byte[] MainImg { get; set; }
        [Required]
        public string NewsContent { get; set;}
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
