using Microsoft.AspNetCore.Mvc;
using SimpleStoreFront.Data;
using SimpleStoreFront.Services;
using SimpleStoreFront.ViewModels;

namespace SimpleStoreFront.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly StoreFrontContext _context;

        public AppController(IMailService mailService, StoreFrontContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //throw new InvalidOperationException("Bad things happen all the time");
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Send the email
                _mailService.SendMessage("RandomEmail@Email.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail Sent!";
                ModelState.Clear();
            }
            else
            {
                // Show the errors
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        public IActionResult Shop()
        {
            var results = from p in _context.Products
                          orderby p.Category
                          select p;

            return View(results.ToList());
        }
    }
}
