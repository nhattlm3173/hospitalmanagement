using Microsoft.AspNetCore.Mvc;

namespace A2_SinhToDau_QuanLyBenhVien.Controllers
{
    public class CustomerGuideController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}