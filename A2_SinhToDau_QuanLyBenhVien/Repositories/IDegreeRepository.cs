using A2_SinhToDau_QuanLyBenhVien.Models;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public interface IDegreeRepository
    {
        Task<IEnumerable<Degree>> GetAllAsync();
        Task<Degree> GetByIdAsync(Guid id);
        Task AddAsync(Degree degree);
        Task UpdateAsync(Degree degree);
        Task DeleteAsync(Guid id);
        List<Degree> GetAll();
    }
}
