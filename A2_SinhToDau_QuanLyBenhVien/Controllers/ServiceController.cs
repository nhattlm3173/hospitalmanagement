using A2_SinhToDau_QuanLyBenhVien.Models;
using A2_SinhToDau_QuanLyBenhVien.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A2_SinhToDau_QuanLyBenhVien.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INewsRepository _newsRepository;
        public ServiceController(ISpecialistRepository specialistRepository, IServiceRepository serviceRepository, INewsRepository newsRepository)
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
            var services = _serviceRepository.GetAll();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialists, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            ViewBag.service_all = service;
            const int pageSize = 7;
            if (pg < 1)
                pg = 1;
            int recsCount = services.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = services.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            //return View(products);
            return View(data);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            var specialist = await _specialistRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            ViewBag.service = new SelectList(services, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            return View(service);
        }
    }
}
