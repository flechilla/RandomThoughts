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

        /// <summary>
        ///     Returns all the thoughts that belongs to the current user.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var currentUserId = HttpContext.User.Identity.Name;

            var userThoughts = _thoughtsRepository.ReadAll(thought => thought.ApplicationUserId == currentUserId);

            return View(userThoughts);
        }
    }
}