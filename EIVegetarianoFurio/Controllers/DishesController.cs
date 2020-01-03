using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIVegetarianoFurio.Models;
using EIVegetarianoFurio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIVegetarianoFurio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IDishRepository _repository;

        public DishesController(IDishRepository repo)
        {
            _repository = repo;
        }


        public IActionResult Get()
        {
            var res= _repository.GetDishs();
            if (res==null)
                return NotFound();
        
            return Ok(res);
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var res = _repository.GetDish(Id);
            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Post(Dish dish)
        {
            var res = _repository.CreateDish(dish);
            if (res == null)
                return NotFound();

            return CreatedAtAction("get", new { dish.Id },res);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int Id,Dish dish)
        {
            if (dish?.Id != Id)
                return BadRequest();
                        
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);

            if (_repository.GetDish(Id) == null)
                return NotFound();


            var result=_repository.UpdateDish(dish);

            return Ok(result);
        }
    }
}