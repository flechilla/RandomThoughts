using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomThoughts.DataAccess.Repositories.Thoughts;
using RandomThoughts.Domain;
using RandomThoughts.Models.ThoughtViewModels;

namespace RandomThoughts.Controllers
{
    public class ThoughtsController : BaseController
    {
        private readonly IThoughtsRepository _thoughtsRepository;
        private readonly IMapper _mapper;

        public ThoughtsController(IHttpContextAccessor httpContextAccessor,
            IThoughtsRepository thoughtsRepository,
            IMapper mapper) : base(httpContextAccessor)
        {
            _thoughtsRepository = thoughtsRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///     Returns all the thoughts that belongs to the current user.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["Title"] = "My Thoughts";
            var userThoughts = _thoughtsRepository.ReadAll(thought => thought.ApplicationUserId == this.CurrentUserId).ToList();

            var userThoughtsVM = _mapper.Map<IEnumerable<Thought>, IEnumerable<ThoughtIndexViewModel>>(userThoughts);

            return View(userThoughtsVM);
        }

        /// <summary>
        ///     Returns all the thoughts that belongs to the current user.
        /// </summary>
        /// <returns></returns>
        public IActionResult PublicThoughts()
        {
            ViewData["Title"] = "Public Thoughts";

            var userThoughts = _thoughtsRepository.ReadAll(_ => true).ToList();

            var userThoughtsVM = _mapper.Map<IEnumerable<Thought>, IEnumerable<ThoughtIndexViewModel>>(userThoughts);

            return View("Index", userThoughtsVM);
        }
    }
}