using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IWordDictionaryRepository _repository;

        public AdminController(IWordDictionaryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Words() => View(_repository.GetAll());
    }
}