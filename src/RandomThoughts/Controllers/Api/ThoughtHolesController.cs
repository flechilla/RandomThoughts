using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomThoughts.Business.ApplicationServices.ThoughtHole;
using RandomThoughts.DataAccess.Repositories.ThoughtHoles;
using RandomThoughts.Domain;
using RandomThoughts.Domain.Enums;
using RandomThoughts.Models.ThoughtHoleViewModels;

namespace RandomThoughts.Controllers.Api
{
    [Produces("application/json")]
    public class ThoughtHolesController : BaseApiController
    {
        private readonly IThoughtHolesApplicationService _thoughtHolesApplicationService;
        private readonly IMapper _mapper;

        public ThoughtHolesController(IHttpContextAccessor httpContextAccessor,
            IThoughtHolesApplicationService thoughtHolesApplicationService,
            IMapper mapper) : base(httpContextAccessor)
        {
            _thoughtHolesApplicationService = thoughtHolesApplicationService;
            _mapper = mapper;
        }

        // GET: api/ThoughtHoles
        /// <summary>
        /// Get all <see cref="ThoughtHole"/> that are public.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ThoughtHoleIndexViewModel> GetAllPublic()
        {
            var thoughtHoles = _thoughtHolesApplicationService.ReadAll(thoughtHole => thoughtHole.Visibility == Visibility.Public).ToList();

            var thoughtHolesVM = _mapper.Map<IEnumerable<ThoughtHole>, IEnumerable<ThoughtHoleIndexViewModel>>(thoughtHoles);

            return thoughtHolesVM;
        }

          // GET: api/ThoughtHoles
        /// <summary>
        /// Get all <see cref="ThoughtHole"/> that are public.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ThoughtHoleIndexViewModel> GetAllPersonalHoles()
        {
            // TODO: Get the user ID and filter the Holes that belongs to the user
            var thoughtHoles = _thoughtHolesApplicationService.ReadAll(thoughtHole => thoughtHole.Visibility == Visibility.Public).ToList();

            var thoughtHolesVM = _mapper.Map<IEnumerable<ThoughtHole>, IEnumerable<ThoughtHoleIndexViewModel>>(thoughtHoles);

            return thoughtHolesVM;
        }

        // GET: api/ThoughtHoles/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var thoughtHole = _thoughtHolesApplicationService.SingleOrDefault(id);

            if (thoughtHole != null)
                return Ok(_mapper.Map<ThoughtHole, ThoughtHoleIndexViewModel>(thoughtHole));

            return NotFound(id);
        }

        // POST: api/ThoughtHoles
        [HttpPost]
        public IActionResult Post([FromBody]ThoughtHoleCreateViewModel newThoughtHole)
        {
            if (ModelState.IsValid)
            {
                var thoughtHole = _mapper.Map<ThoughtHoleCreateViewModel, ThoughtHole>(newThoughtHole);

                thoughtHole.CreatedBy = this.CurrentUserId;
                //thoughtHole.ApplicationUserId = this.CurrentUserId; TODO: adds the relation between the Users and the Holes

                var createdThoughtHole = _thoughtHolesApplicationService.Add(thoughtHole);//TODO: add the auditable and trackable values!!!

                try
                {
                    _thoughtHolesApplicationService.SaveChanges();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

                return Created("", createdThoughtHole);
            }

            return BadRequest(ModelState);
        }
        
        // PUT: api/ThoughtHoles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ThoughtHoleEditViewModel editedThoughtHole)
        {
            if (id != editedThoughtHole.Id)
            {
                ModelState.AddModelError("Id", "The given Id of the edited model doesn't match with the route Id");
                return BadRequest(ModelState);
            }

            if (!_thoughtHolesApplicationService.Exists(id))
                return NotFound(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var originalThoughtHole = _thoughtHolesApplicationService.SingleOrDefault(id);
            originalThoughtHole.Name = editedThoughtHole.Name;
            originalThoughtHole.Description = editedThoughtHole.Description;
            originalThoughtHole.Visibility = editedThoughtHole.Visibility;

            originalThoughtHole.ModifiedBy = this.CurrentUserId;

            _thoughtHolesApplicationService.Update(originalThoughtHole);

            try
            {
                _thoughtHolesApplicationService.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok(originalThoughtHole);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

       
    }
}
