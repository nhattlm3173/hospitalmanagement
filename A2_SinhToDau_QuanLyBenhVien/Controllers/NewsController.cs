using A2_SinhToDau_QuanLyBenhVien.Models;
using A2_SinhToDau_QuanLyBenhVien.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A2_SinhToDau_QuanLyBenhVien.Controllers
{
    public class NewsController : Controller
    {
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INewsRepository _newsRepository;
        public NewsController(ISpecialistRepository specialistRepository, IServiceRepository serviceRepository, INewsRepository newsRepository)
        {
            _specialistRepository = specialistRepository;
            _serviceRepository = serviceRepository;
            _newsRepository = newsRepository;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            var specialists = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            var @new = _newsRepository.GetAll();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialists, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            ViewBag.news_all = news;
            const int pageSize = 7;
            if (pg < 1)
                pg = 1;
            int recsCount = @new.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = @new.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(products);
            return View(data);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var newsOfId = await _newsRepository.GetByIdAsync(id);
            if (newsOfId == null)
            {
                return NotFound();
            }
            var specialists = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialists, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            return View(newsOfId);
        }
    }
}
