using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomThoughts.DataAccess.Repositories.ThoughtHoles;
using RandomThoughts.Domain;
using RandomThoughts.Models.ThoughtHoleViewModels;

namespace RandomThoughts.Controllers
{
    public class ThoughtHolesController : BaseController
    {
        private readonly IThoughtHolesRepository _thoughtHolesRepository;
        private readonly IMapper _mapper;

        public ThoughtHolesController(IHttpContextAccessor httpContextAccessor,
            IThoughtHolesRepository thoughtHolesRepository,
            IMapper mapper) : base(httpContextAccessor)
        {
            _thoughtHolesRepository = thoughtHolesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Public Holes";//TODO: Send the amount of thoughts related with each hole
            var thoughtHolesVM = _thoughtHolesRepository.
                ReadAll(_ => true).
                Select(th => new ThoughtHoleIndexViewModel
                {
                    Name = th.Name,
                    Description = th.Description.Substring(0, 50) + "...",
                    AmountOfThought = th.Thoughts.Count(),
                    Likes = th.Likes,
                    Views = th.Views
                }).
                ToList();

            return View(thoughtHolesVM);
        }

        [HttpGet]
        public IActionResult MyHoles()
        {
            ViewData["Title"] = "My Holes";//TODO: Send the amount of thoughts related with each hole
            var thoughtHolesVM = _thoughtHolesRepository.
                ReadAll(th => th.CreatedBy == this.CurrentUserId).
                Select(th => new ThoughtHoleIndexViewModel
                {
                    Name = th.Name,
                    Description = th.Description.Substring(0, 50) + "...",
                    AmountOfThought = th.Thoughts.Count(),
                    Likes = th.Likes,
                    Views = th.Views
                }).
                ToList();

            return View("Index" ,thoughtHolesVM);
        }


    }
}