using A2_SinhToDau_QuanLyBenhVien.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A2_SinhToDau_QuanLyBenhVien.Controllers
{
    public class SpecialistController : Controller
    {
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INewsRepository _newsRepository;
        public SpecialistController(ISpecialistRepository specialistRepository, IServiceRepository serviceRepository, INewsRepository newsRepository)
        {
            _specialistRepository = specialistRepository;
            _serviceRepository = serviceRepository;
            _newsRepository = newsRepository;
        }
        public async Task<IActionResult> Index()
        {
            var specialist = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            return View(specialist);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var specialist = await _specialistRepository.GetByIdAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }
            var specialists = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialists, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            return View(specialist);
        }
    }
}
