using A2_SinhToDau_QuanLyBenhVien.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace A2_SinhToDau_QuanLyBenhVien.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INewsRepository _newsRepository;
        public SearchController(ISpecialistRepository specialistRepository, IServiceRepository serviceRepository, INewsRepository newsRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository)
        {
            _specialistRepository = specialistRepository;
            _serviceRepository = serviceRepository;
            _newsRepository = newsRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
        }
        public async Task<IActionResult> Index()
        {
            var specialist = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string keyword)
        {
            var specialist = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            var services_search = await _serviceRepository.GetAllAsync();
            var news_search = await _newsRepository.GetAllAsync();
            if (keyword != null)
            {   
                news_search = news_search.Where(e => e.Title.ToLower().Contains(keyword.ToLower())).ToList();   
                services_search = services_search.Where(e => e.SVName.ToLower().Contains(keyword.ToLower())).ToList();
                ViewBag.news_search = news_search;
                ViewBag.services_search = services_search;
                ViewBag.service = new SelectList(service, "Id", "SVName");
                ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
                ViewBag.news = new SelectList(news, "Id", "Title");
                ViewBag.news_search = news_search.Any() ? news_search : null;
                ViewBag.services_search = services_search.Any() ? services_search : null;
                return View();
            }
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            ViewBag.news_search = news_search;
            ViewBag.services_search = services_search;
            return View();
        }
    }
}
