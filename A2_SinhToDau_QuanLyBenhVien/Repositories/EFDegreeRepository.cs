using A2_SinhToDau_QuanLyBenhVien.Data;
using A2_SinhToDau_QuanLyBenhVien.Models;
using Microsoft.EntityFrameworkCore;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public class EFDegreeRepository : IDegreeRepository
    {
        private readonly ApplicationDbContext _context;

        public EFDegreeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Degree>> GetAllAsync()
        {
            return await _context.Degrees.ToListAsync();
        }

        public async Task<Degree> GetByIdAsync(Guid id)
        {
            return await _context.Degrees.FindAsync(id);
        }

        public async Task AddAsync(Degree degree)
        {
            _context.Degrees.Add(degree);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Degree degree)
        {
            _context.Entry(degree).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var degree = await _context.Degrees.FindAsync(id);
            _context.Degrees.Remove(degree);
            await _context.SaveChangesAsync();
        }
        public List<Degree> GetAll()
        {
            return _context.Degrees.ToList();
        }
    }
}
