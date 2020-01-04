using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Threading.Tasks;
using EIVegetarianoFurio.Models;
using EIVegetarianoFurio.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIVegetarianoFurio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IDishRepository _repository;
        private string   _path;

        public DishesController(IDishRepository repo, IWebHostEnvironment env)
        {
            _repository = repo;
            _path = System.IO.Path.Combine(env.ContentRootPath, "data", "images", "dishes");

        }


        public IActionResult Get()
        {
            var res= _repository.GetDishes();
            if (res==null)
                return NotFound();
        
            return Ok(res);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _repository.GetDishById(id);
            if (res == null)
                return NotFound();

            return Ok(res);
        } 
       

        [HttpPost]
        public IActionResult Post(Dish dish)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);
            
            var res = _repository.CreateDish(dish);
          
            return CreatedAtAction("get", new { dish.Id },res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.GetDishById(id) == null)            
                return NotFound();            

            _repository.DeleteDish(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Dish dish)
        {
            if (dish?.Id != id)
                return BadRequest();
                        
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);

            if (_repository.GetDishById(id) == null)
                return NotFound();


            var result=_repository.UpdateDish(dish);

            return Ok(result);
        }


        [HttpGet("{id}/image")]
        public IActionResult Image(int id)
        {
            var res = _repository.GetDishById(id);
            if (res == null)
                return NotFound();

            var file = System.IO.Path.Combine(_path,  $"{id}.jpg");

            if (System.IO.File.Exists(file))
            {
                var bytes = System.IO.File.ReadAllBytes(file);
               return File(bytes,"image/jpeg");
            }

            return NotFound();
        }
    }
}