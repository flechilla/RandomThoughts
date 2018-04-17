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

namespace RandomThoughts.Controllers.Api
{
    [Produces("application/json")]
    public class ThoughtHolesController : BaseApiController
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
        // GET: api/ThoughtHoles
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ThoughtHoles/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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

                var createdThoughtHole = _thoughtHolesRepository.Add(thoughtHole);//TODO: add the auditable and trackable values!!!

                try
                {
                    _thoughtHolesRepository.SaveChanges();
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
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

       
    }
}
