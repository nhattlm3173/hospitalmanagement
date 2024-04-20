using A2_SinhToDau_QuanLyBenhVien.Data;
using A2_SinhToDau_QuanLyBenhVien.Models;
using Microsoft.EntityFrameworkCore;

namespace A2_SinhToDau_QuanLyBenhVien.Repositories
{
    public class EFNewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public EFNewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<New>> GetAllAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<New> GetByIdAsync(Guid id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task AddAsync(New news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(New news)
        {
            _context.Entry(news).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }
        public List<New> GetAll()
        {
            return _context.News.ToList();
        }
    }
}
