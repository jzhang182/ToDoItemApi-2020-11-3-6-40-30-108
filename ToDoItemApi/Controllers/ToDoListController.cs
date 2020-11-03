using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoItemApi.Models;
using ToDoItemApi.Interfaces;

namespace ToDoItemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private IRepository _repository;

        public ToDoItemController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPut]
        public async Task<ActionResult<ToDoItem>> AddAsync(ToDoItem newItem)
        {
            //Check Id
            if (newItem.Id == 0)
                return BadRequest(new Dictionary<string, string>() { { "message", "ID cannot be empty" } });

            //Upsert
            await _repository.UpsertAsync(newItem);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoItem>>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetAsync(long id)
        {
            //Check Id
            if (id == 0)
                return BadRequest(new Dictionary<string, string>() { { "message", "ID cannot be empty" } });

            var item = await _repository.GetAsync(id);
            if (item == null)
                return NotFound(new Dictionary<string, string>() { { "message", $"{id} does not exist" } });

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItem>> DeleteAsync(long id)
        {
            //Check Id
            if (id == 0)
                return BadRequest(new Dictionary<string, string>() { { "message", "ID cannot be empty" } });

            var modelInDb = await _repository.GetAsync(id);
            if (modelInDb == null)
                return NotFound(new Dictionary<string, string>() { { "message", $"{id} does not exist" } });

            //Delete
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}
