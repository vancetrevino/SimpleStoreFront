using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStoreFront.Data;
using SimpleStoreFront.Services;
using SimpleStoreFront.ViewModels;

namespace SimpleStoreFront.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IStoreFrontRepository _repository;

        public AppController(IMailService mailService, IStoreFrontRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
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

        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();

            return View(results);
        }
    }
}
