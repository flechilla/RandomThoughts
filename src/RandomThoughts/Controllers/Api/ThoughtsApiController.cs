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

namespace RandomThoughts.Controllers.Api
{
    [Produces("application/json")]
    [Route("Thoughts")]
    public class ThoughtsApiController : BaseApiController
    {
        private readonly IThoughtsRepository _thoughtsRepository;
        private readonly IMapper _mapper;

        public ThoughtsApiController(IHttpContextAccessor httpContextAccessor,
            IThoughtsRepository thoughtsRepository,
            IMapper mapper) : base(httpContextAccessor)
        {
            _thoughtsRepository = thoughtsRepository;
            _mapper = mapper;
        }

        // GET: api/Thoughts
        /// <summary>
        ///     Get the <see cref="Thought"/> related to the current user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ThoughtIndexViewModel> GetUserThoughts()
        {
            var userThoughts = _thoughtsRepository.ReadAll(thought => thought.ApplicationUserId == CurrentUserId).ToList();

            var userThoughtsVM = _mapper.Map<IEnumerable<Thought>, IEnumerable<ThoughtIndexViewModel>>(userThoughts);

            return userThoughtsVM;
        }

    // GET: api/Thoughts/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var thought = _thoughtsRepository.Entities.Find(id);

            if (thought != null)
                return Ok(_mapper.Map<Thought, ThoughtIndexViewModel>(thought));

            return NotFound(id);
        }
        
        // POST: api/Thoughts
        [HttpPost]
        public void Post([FromBody]Thought newThought)
        {

        }
        
        // PUT: api/Thoughts/5
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
