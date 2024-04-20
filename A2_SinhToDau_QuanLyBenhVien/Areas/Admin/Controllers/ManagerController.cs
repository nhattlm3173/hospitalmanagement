using A2_SinhToDau_QuanLyBenhVien.Data;
using A2_SinhToDau_QuanLyBenhVien.Models;
using A2_SinhToDau_QuanLyBenhVien.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using A2_SinhToDau_QuanLyBenhVien.Controllers;
namespace A2_SinhToDau_QuanLyBenhVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IDegreeRepository _degreeRepository;
        public ManagerController(ISpecialistRepository specialistRepository, IServiceRepository serviceRepository, INewsRepository newsRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IDegreeRepository degreeRepository)
        {
            _specialistRepository = specialistRepository;
            _serviceRepository = serviceRepository;
            _newsRepository = newsRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _degreeRepository = degreeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Doctor()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            ViewBag.NumberOfDoctors = doctors.Count();
            ViewBag.Spec = GetDoctorsWithSpecialistsAsync();
            return View(doctors);
        }
        public async Task<List<Doctor>> GetDoctorsWithSpecialistsAsync()
        {
            var spec = await _doctorRepository.GetAllDoctorSpecialistAsync();
            return spec;
        }

        public async Task<IActionResult> Service()
        {
            var services = await _serviceRepository.GetAllAsync();
            ViewBag.NumberOfServices = services.Count();
            return View(services);
        }

        public async Task<IActionResult> Specialist()
        {
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.NumberOfSpecialists = specialists.Count();
            return View(specialists);
        }
        public async Task<IActionResult> Appointment()
        {
            var appointment = await _appointmentRepository.GetAllAsync();
            ViewBag.NumberOfAppointments = appointment.Count();
            return View(appointment);
        }
        public async Task<IActionResult> Degree()
        {
            var degree = await _degreeRepository.GetAllAsync();
            ViewBag.NumberOfDegrees = degree.Count();
            return View(degree);
        }
        public async Task<IActionResult> New()
        {
            var news = await _newsRepository.GetAllAsync();
            ViewBag.NumberOfNew = news.Count();
            return View(news);
        }
        public async Task<IActionResult> Create()
        {
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor, IFormFile AvtImage)
        {
            ModelState.Remove("AvtImage");
            IsImageValid(AvtImage, "AvtImage");
            if (ModelState.IsValid)
            {
                byte[] arrImage = await ConvertImageToByteArrayAsync(AvtImage);
                doctor.AvtImage = arrImage;
                await _doctorRepository.AddAsync(doctor);
                return RedirectToAction(nameof(Doctor));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View(doctor);
        }

        public async Task<IActionResult> CreateNews()
        {
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews(New news, IFormFile MainImg)
        {
            ModelState.Remove("MainImg");
            ModelState.Remove("NewsContent");
            IsImageValid(MainImg, "MainImg");
            if (ModelState.IsValid)
            {
                byte[] arrImage = await ConvertImageToByteArrayAsync(MainImg);
                news.MainImg = arrImage;
                await _newsRepository.AddAsync(news);
                return RedirectToAction(nameof(New));
            }
            return View(news);
        }

        public async Task<IActionResult> CreateService()
        {
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(Service service, IFormFile MainImg)
        {
            ModelState.Remove("MainImg");
            IsImageValid(MainImg, "MainImg");
            if (ModelState.IsValid)
            {
                byte[] arrImage = await ConvertImageToByteArrayAsync(MainImg);
                service.MainImg = arrImage;
                await _serviceRepository.AddAsync(service);
                return RedirectToAction(nameof(Service));
            }
            return View(service);
        }

        public async Task<IActionResult> CreateSpecialist()
        {
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialist(Specialist specialist, IFormFile SpecialistImg)
        {
            ModelState.Remove("SpecialistImg");
            IsImageValid(SpecialistImg, "SpecialistImg");
            if (ModelState.IsValid)
            {
                byte[] arrImage = await ConvertImageToByteArrayAsync(SpecialistImg);
                specialist.SpecialistImg = arrImage;
                await _specialistRepository.AddAsync(specialist);
                return RedirectToAction(nameof(Specialist));
            }
            return View(specialist);
        }

        public async Task<IActionResult> CreateAppointment()
        {
            AppointmentController a = new AppointmentController(_specialistRepository, _serviceRepository, _newsRepository, _doctorRepository, _appointmentRepository);
            return await a.MakeAppointment();
            //var specialists = await _specialistRepository.GetAllAsync();
            //ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(Appointment appointment)
        {
            AppointmentController a = new AppointmentController(_specialistRepository, _serviceRepository, _newsRepository, _doctorRepository, _appointmentRepository);
            return await a.MakeAppointment(appointment);
            //if (ModelState.IsValid)
            //{
            //    await _appointmentRepository.AddAsync(appointment);
            //    return RedirectToAction(nameof(Appointment));
            //}
            //return View(appointment);
        }

        public async Task<IActionResult> CreateDegree()
        {
            var doctor = await _doctorRepository.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctor, "Id", "DrName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDegree(Degree degree)
        {
            if (ModelState.IsValid)
            {
                await _degreeRepository.AddAsync(degree);
                return RedirectToAction(nameof(Degree));
            }
            var doctor = await _doctorRepository.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctor, "Id", "DrName");
            return View(degree);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            await _serviceRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Service));
        }
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            await _newsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(New));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSpecialist(Guid id)
        {
            var specialist = await _specialistRepository.GetByIdAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }
            await _specialistRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Specialist));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            await _doctorRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Doctor));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDegree(Guid id)
        {
            var degree = await _degreeRepository.GetByIdAsync(id);
            if (degree == null)
            {
                return NotFound();
            }
            await _degreeRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Degree));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            await _appointmentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Appointment));
        }
        public async Task<IActionResult> EditAppointment(Guid id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            List<string> times = new List<string>();
            for (int hour = 7; hour <= 20; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    string timeString = $"{hour}h";
                    if (minute != 0)
                    {
                        timeString += $"{minute:D2}";
                    }
                    times.Add(timeString);
                }
            }
            ViewBag.time = new SelectList(times);
            return View(appointment);
        }
        [HttpPost]
        public async Task<IActionResult> EditAppointment(Guid id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingAppointment = await _appointmentRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                existingAppointment.Date = appointment.Date;
                existingAppointment.Time = appointment.Time;
                existingAppointment.CustomerName = appointment.CustomerName;
                existingAppointment.CustomerEmail = appointment.CustomerEmail;
                existingAppointment.CustomerPhone = appointment.CustomerPhone;
                existingAppointment.CustomerAddress = appointment.CustomerAddress;
                existingAppointment.Note = appointment.Note;
                existingAppointment.Reason = appointment.Reason;
                await _appointmentRepository.UpdateAsync(existingAppointment);
                return RedirectToAction(nameof(Appointment));
            }
            List<string> times = new List<string>();
            for (int hour = 7; hour <= 20; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    string timeString = $"{hour}h";
                    if (minute != 0)
                    {
                        timeString += $"{minute:D2}";
                    }
                    times.Add(timeString);
                }
            }
            ViewBag.time = new SelectList(times);
            return View(appointment);
        }
        public async Task<IActionResult> EditDegree(Guid id)
        {
            var degree = await _degreeRepository.GetByIdAsync(id);
            if (degree == null)
            {
                return NotFound();
            }
            var doctor = await _doctorRepository.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctor, "Id", "DrName");
            return View(degree);
        }
        [HttpPost]
        public async Task<IActionResult> EditDegree(Guid id, Degree degree)
        {
            if (id != degree.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDegree = await _degreeRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                existingDegree.DeContent = degree.DeContent;
                existingDegree.DoctorsId = degree.DoctorsId;
                await _degreeRepository.UpdateAsync(existingDegree);
                return RedirectToAction(nameof(Degree));
            }
            var doctor = await _doctorRepository.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctor, "Id", "DrName");
            return View(degree);
        }
        public async Task<IActionResult> EditService(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        public async Task<IActionResult> EditNews(Guid id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }
        [HttpPost]
        public async Task<IActionResult> EditNews(Guid id, New news, IFormFile MainImg)
        {
            ModelState.Remove("MainImg");
            if (id != news.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingNews = await _newsRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                if (MainImg == null)
                {
                    news.MainImg = existingNews.MainImg;
                }
                else
                {
                    news.MainImg = await ConvertImageToByteArrayAsync(MainImg);
                }
                existingNews.Title = news.Title;
                existingNews.MainImg = news.MainImg;
                existingNews.NewsTeaser = news.NewsTeaser;
                existingNews.NewsContent = news.NewsContent;
                await _newsRepository.UpdateAsync(existingNews);
                return RedirectToAction(nameof(New));
            }
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(Guid id, Service service, IFormFile MainImg)
        {
            ModelState.Remove("MainImg");
            if (id != service.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingService = await _serviceRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                if (MainImg == null)
                {
                    service.MainImg = existingService.MainImg;
                }
                else
                {
                    service.MainImg = await ConvertImageToByteArrayAsync(MainImg);
                }
                existingService.SVName = service.SVName;
                existingService.MainImg = service.MainImg;
                existingService.SVDescription = service.SVDescription;
                existingService.SVTeaser = service.SVTeaser;
                await _serviceRepository.UpdateAsync(existingService);
                return RedirectToAction(nameof(Service));
            }
            return View(service);
        }
        public async Task<IActionResult> EditSpecialist(Guid id)
        {
            var specialist = await _specialistRepository.GetByIdAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }
            return View(specialist);
        }
        [HttpPost]
        public async Task<IActionResult> EditSpecialist(Guid id, Specialist specialist, IFormFile SpecialistImg)
        {
            ModelState.Remove("SpecialistImg");
            if (id != specialist.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingSpecialist = await _specialistRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                if (SpecialistImg == null)
                {
                    specialist.SpecialistImg = existingSpecialist.SpecialistImg;
                }
                else
                {
                    specialist.SpecialistImg = await ConvertImageToByteArrayAsync(SpecialistImg);
                }
                existingSpecialist.SpecialistName = specialist.SpecialistName;
                existingSpecialist.SpecialistImg = specialist.SpecialistImg;
                existingSpecialist.SpecialistDescription = specialist.SpecialistDescription;
                await _specialistRepository.UpdateAsync(existingSpecialist);
                return RedirectToAction(nameof(Specialist));
            }
            return View(specialist);
        }
        public async Task<IActionResult> EditDoctor(Guid id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> EditDoctor(Guid id, Doctor doctor, IFormFile AvtImage)
        {
            ModelState.Remove("AvtImage"); // Loại bỏ xác thực ModelState cho

            if (id != doctor.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDoctor = await _doctorRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                if (AvtImage == null)
                {
                    doctor.AvtImage = existingDoctor.AvtImage;
                }
                else
                {
                    doctor.AvtImage = await ConvertImageToByteArrayAsync(AvtImage);
                }
                existingDoctor.DrName = doctor.DrName;
                existingDoctor.PhoneNumber = doctor.PhoneNumber;
                existingDoctor.Email = doctor.Email;
                existingDoctor.Experience = doctor.Experience;
                existingDoctor.AvtImage = doctor.AvtImage;
                existingDoctor.SpecialistId = doctor.SpecialistId;
                await _doctorRepository.UpdateAsync(existingDoctor);
                return RedirectToAction(nameof(Doctor));
            }
            var specialists = await _specialistRepository.GetAllAsync();
            ViewBag.Specialists = new SelectList(specialists, "Id", "SpecialistName");
            return View(doctor);
        }

        private bool IsImageValid(IFormFile image, String model)
        {
            if (image == null)
            {
                ModelState.AddModelError(model, "Please select an image.");
                return false;
            }
            if (image.Length > 5 * 1024 * 1024) // 5MB
            {
                ModelState.AddModelError(model, "The image size must be less than 5MB.");
                return false;
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError(model, "Only JPG, JPEG, and PNG formats are allowed.");
                return false;
            }
            try
            {
                using (var imageStream = image.OpenReadStream())
                {
                    var imageFormat = Image.FromStream(imageStream).RawFormat;
                    if (ImageFormat.Jpeg.Equals(imageFormat) || ImageFormat.Png.Equals(imageFormat))
                    {
                        return true;
                    }
                    else
                    {
                        ModelState.AddModelError(model, "Invalid image format.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(model, $"An error occurred while validating the image: {ex.Message}");
                return false;
            }
        }

        private async Task<byte[]> ConvertImageToByteArrayAsync(IFormFile image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
