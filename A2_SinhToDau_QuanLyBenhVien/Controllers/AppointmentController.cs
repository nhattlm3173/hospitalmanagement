using A2_SinhToDau_QuanLyBenhVien.Models;
using A2_SinhToDau_QuanLyBenhVien.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A2_SinhToDau_QuanLyBenhVien.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialistRepository _specialistRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly INewsRepository _newsRepository;
        public AppointmentController(ISpecialistRepository specialistRepository, IServiceRepository serviceRepository, INewsRepository newsRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository)
        {
            _specialistRepository = specialistRepository;
            _serviceRepository = serviceRepository;
            _newsRepository = newsRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
        }
        public async Task<IActionResult> MakeAppointment()
        {
            var specialist = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            ViewBag.doctor = new SelectList(doctors, "Id", "DrName");
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MakeAppointment(Appointment appointment)
        {
            var specialist = await _specialistRepository.GetAllAsync();
            var service = await _serviceRepository.GetAllAsync();
            var news = await _newsRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            var appointments = await _appointmentRepository.GetAllAsync();
            List<string> times = new List<string>();
            if (ModelState.IsValid)
            {
                if (appointments.Any(x => x.DoctorsId == appointment.DoctorsId && x.Date == appointment.Date && x.Time == appointment.Time))
                {
                    ModelState.AddModelError(string.Empty, "Thời gian bạn đặt cho bác sĩ " + appointments.FirstOrDefault(x => x.DoctorsId == appointment.DoctorsId).Doctors.DrName + " đã được đặt.");

                    // Hiển thị form với ModelState không hợp lệ
                    ViewBag.service = new SelectList(service, "Id", "SVName");
                    ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
                    ViewBag.news = new SelectList(news, "Id", "Title");
                    ViewBag.doctor = new SelectList(doctors, "Id", "DrName");
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
                //bool isTimeSlotAvailable = await _appointmentRepository.IsTimeSlotAvailableAsync(appointment.DoctorsId, appointment.Time);
                TempData["SuccessMessage"] = "Đăng ký thành công!";
                await _appointmentRepository.AddAsync(appointment);
                return RedirectToAction("Index", "Home");
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
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
            ViewBag.service = new SelectList(service, "Id", "SVName");
            ViewBag.specialist = new SelectList(specialist, "Id", "SpecialistName");
            ViewBag.news = new SelectList(news, "Id", "Title");
            ViewBag.doctor = new SelectList(doctors, "Id", "DrName");
            return View(appointment);
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorsBySpecialist(Guid specialistId)
        {
            try
            {
                // Lấy danh sách các bác sĩ thuộc khoa được chọn từ repository
                var doctors = await _doctorRepository.GetBySpecialistIdAsync(specialistId);

                // Chuyển đổi danh sách này sang một danh sách các đối tượng JSON tương tự như đã làm trong action GetDoctors
                var doctorList = new List<object>();
                foreach (var doctor in doctors)
                {
                    doctorList.Add(new { value = doctor.Id, text = doctor.DrName });
                }

                // Trả về danh sách các bác sĩ dưới dạng JSON
                return Json(doctorList);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
