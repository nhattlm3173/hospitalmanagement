using A2_SinhToDau_QuanLyBenhVien.Data;
using A2_SinhToDau_QuanLyBenhVien.Models;
using Microsoft.EntityFrameworkCore;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public class EFDoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public EFDoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(Guid id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Doctor>> GetBySpecialistIdAsync(Guid specialistId)
        {
            return await _context.Doctors.Where(x => x.SpecialistId == specialistId).ToListAsync();
        }
        public async Task<List<Doctor>> GetAllDoctorSpecialistAsync()
        {
            return await _context.Doctors.Include(d => d.Specialist).ToListAsync();
        }

    }
}
