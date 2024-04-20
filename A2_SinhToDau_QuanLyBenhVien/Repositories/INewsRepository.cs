using A2_SinhToDau_QuanLyBenhVien.Models;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public interface INewsRepository
    {
        Task<IEnumerable<New>> GetAllAsync();
        Task<New> GetByIdAsync(Guid id);
        Task AddAsync(New news);
        Task UpdateAsync(New news);
        Task DeleteAsync(Guid id);
        List<New> GetAll();
    }
}
