using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomThoughts.DataAccess.Repositories.Thoughts;

namespace RandomThoughts.Controllers
{
    public class ThoughtsController : Controller
    {
        private readonly IThoughtsRepository _thoughtsRepository;

        public ThoughtsController(IThoughtsRepository thoughtsRepository)
        {
            _thoughtsRepository = thoughtsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}