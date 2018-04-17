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
            ViewData["Title"] = "Public Thoughts";
            var thoughtHoles = _thoughtHolesRepository.ReadAll(_ => true).Select(th=>new ThoughtHole{Name = th.Name, Description = th.Description.Substring(0, 50) + "..."}).ToList();
            var thoughtHolesVM = _mapper.Map<IEnumerable<ThoughtHole>, IEnumerable<ThoughtHoleIndexViewModel>>(thoughtHoles);
            return View(thoughtHolesVM);
        }

        [HttpGet]
        public IActionResult MyHoles()
        {
            ViewData["Title"] = "Public Thoughts";
            var thoughtHoles = _thoughtHolesRepository.ReadAll(th => th.CreatedBy == this.CurrentUserId).Select(th => new ThoughtHole { Name = th.Name, Description = th.Description.Substring(0, 50) + "..." }).ToList();
            var thoughtHolesVM = _mapper.Map<IEnumerable<ThoughtHole>, IEnumerable<ThoughtHoleIndexViewModel>>(thoughtHoles);
            return View("Index" ,thoughtHolesVM);
        }


    }
}