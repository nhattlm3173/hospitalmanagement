using A2_SinhToDau_QuanLyBenhVien.Models;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(Guid id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Guid id);
        List<Service> GetAll();
    }
}
