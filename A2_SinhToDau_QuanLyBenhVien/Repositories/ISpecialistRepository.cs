using A2_SinhToDau_QuanLyBenhVien.Models;
namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public interface ISpecialistRepository
    {
        Task<IEnumerable<Specialist>> GetAllAsync();
        Task<Specialist> GetByIdAsync(Guid id);
        Task AddAsync(Specialist specialist);
        Task UpdateAsync(Specialist specialist);
        Task DeleteAsync(Guid id);

    }
}
