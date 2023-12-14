using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookShoppingCartMvcUI.Controllers
{
    public class BookController : Controller
    {
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            // Logic để xử lý việc thêm sách vào cơ sở dữ liệu
            // Sau đó, chuyển hướng hoặc hiển thị thông báo thành công
            return RedirectToAction("Index");
        }
    }
}
