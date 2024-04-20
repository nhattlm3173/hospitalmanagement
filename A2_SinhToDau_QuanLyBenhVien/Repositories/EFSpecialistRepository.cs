using A2_SinhToDau_QuanLyBenhVien.Data;
using A2_SinhToDau_QuanLyBenhVien.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public class EFSpecialistRepository : ISpecialistRepository
    {
        private readonly ApplicationDbContext _context;

        public EFSpecialistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialist>> GetAllAsync()
        {
            return await _context.Specialists.ToListAsync();
        }

        public async Task<Specialist> GetByIdAsync(Guid id)
        {
            return await _context.Specialists.FindAsync(id);
        }

        public async Task AddAsync(Specialist specialist)
        {
            _context.Specialists.Add(specialist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialist specialist)
        {
            _context.Entry(specialist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            _context.Specialists.Remove(specialist);
            await _context.SaveChangesAsync();
        }
    }
}
