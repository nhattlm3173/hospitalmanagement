using A2_SinhToDau_QuanLyBenhVien.Models;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(Guid id);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Doctor>> GetBySpecialistIdAsync(Guid specialistId);
        Task<List<Doctor>> GetAllDoctorSpecialistAsync();
    }
}
