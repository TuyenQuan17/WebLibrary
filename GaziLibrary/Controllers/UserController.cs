using AspNetCoreHero.ToastNotification.Abstractions;
using GaziLibrary.Business.Abstract;
using GaziLibrary.Entities.Concrete;
using GaziLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        INotyfService _notyf;
        IBookService _bookService;
        IUserService _userService;
        IBorrowedBookService _borowwedBookService;
        IMessageService _messageService;
        ITypeService _typeService;
        public UserController(INotyfService notyf, IBookService bookService, IUserService userService, IBorrowedBookService borowwedBookService, IMessageService messageService, ITypeService typeService)
        {
            _notyf = notyf;
            _bookService = bookService;
            _userService = userService;
            _borowwedBookService = borowwedBookService;
            _messageService = messageService;
            _typeService = typeService;
        }

        // List Books
        public IActionResult Books(string searchString)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            var model = new BookModel();
            if(String.IsNullOrEmpty(searchString))
            {
                model.Books = _bookService.GetAllByStatus().Data;
            }
            else
            {
                model.Books = _bookService.GetAllBySearch(searchString).Data;
            }
            return View(model);
        }
        public IActionResult BorrowBook(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            if (_borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data != null)
            {
                _notyf.Error("Bạn hiện có sách mượn.", 3);
                return RedirectToAction("Books", "User");
            }
            var borrowedBook = new BorrowedBook
            {
                BorrowDate = DateTime.Parse(DateTime.Now.ToShortDateString()),
                ReturnDate = DateTime.Parse(DateTime.Now.AddDays(15).ToShortDateString()),
                Status = true,
                BookId = id,
                UserId = Convert.ToInt32(HttpContext.Session.GetString("id"))
            };
            var book = _bookService.GetById(id).Data;
            book.Status = false;
            _bookService.Update(book);
            _borowwedBookService.Add(borrowedBook);
            _notyf.Success("Cuốn sách đã được mượn.", 3);
            return RedirectToAction("Books", "User");
        }
        // Edit Profil
        public IActionResult EditProfil()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            BorrowedBook borrowedBook = null;
            if (_borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Success)
            {
                borrowedBook = _borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data;
            }
            var model = new UserModel
            {
                User = _userService.GetById(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data,
                BorrowedBook = borrowedBook
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProfil(User user)
        {
            BorrowedBook borrowedBook = null;
            if (_borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Success)
            {
                borrowedBook = _borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data;
            }
            if (ModelState.ErrorCount==1)
            {
                user.Status = true;
                HttpContext.Session.SetString("name", user.FirstName);
                HttpContext.Session.SetString("lastname", user.LastName);
                _userService.Update(user);
                _notyf.Success("Thông tin của bạn đã được cập nhật thành công.", 3);
                return RedirectToAction("EditProfil", "User");
            }
            var model = new UserModel
            {
                User = user,
                BorrowedBook = borrowedBook
            };
            _notyf.Error("Không thể cập nhật hồ sơ.", 3);
            return View(model);
        }
        public IActionResult ReturnBack(int id)
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            var result = _borowwedBookService.GetByUserId(Convert.ToInt32(HttpContext.Session.GetString("id"))).Data;
            result.Status = false;
            result.ReturnDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            var book = _bookService.GetById(id).Data;
            book.Status = true;
            _bookService.Update(book);
            _borowwedBookService.Update(result);
            _notyf.Warning("Cuốn sách đã được trả lại.", 3);
            return RedirectToAction("EditProfil", "User");
        }
        // Rules Of Library
        public IActionResult RulesOfLibrary()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "User");
            }
            return View();
        }
        // View
        public IActionResult MainScreen()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            return View();
        }
        // Contact 
        public IActionResult Contact()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            var model = new MessageModel
            {
                Message = new Message()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Contact(Message message)
        {
            if (!ModelState.IsValid)
            {
                var model = new MessageModel
                {
                    Message = message
                };
                _notyf.Error("Không thể gửi tin nhắn.", 3);
                return View(model);
            }
            message.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            message.UserID = Convert.ToInt32(HttpContext.Session.GetString("id"));
            message.Status = true;
            _messageService.Add(message);
            _notyf.Success("Đã gửi tin nhắn thành công.");
            return RedirectToAction("Contact");
        }
        public IActionResult Type()
        {
            if (!CheckUser())
            {
                return RedirectToAction("Books", "Admin");
            }
            return View();
        }
        // Check User
        private bool CheckUser()
        {
            if (HttpContext.Session.GetString("position") != "USER")
            {
                return false;
            }
            return true;
        }
    }
}