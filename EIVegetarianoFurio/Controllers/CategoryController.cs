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
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _repository;
        private string   _path;

        public CategoryController(ICategoryRepository repo,IHostingEnvironment env)
        {
            _repository = repo;
            _path = System.IO.Path.Combine(env.ContentRootPath, "data", "images", "categories");

        }


        public IActionResult Get()
        {
            var res= _repository.GetCategories();
            if (res==null)
                return NotFound();
        
            return Ok(res);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _repository.GetCategoryById(id);
            if (res == null)
                return NotFound();

            return Ok(res);
        } 
       

        [HttpPost]
        public IActionResult Post(Category category)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);
            
            var res = _repository.CreateCategory(category);
          
            return CreatedAtAction("get", new { category.Id },res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_repository.GetCategoryById(id) == null)            
                return NotFound();            

            _repository.DeleteCategory(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Category category)
        {
            if (category?.Id != id)
                return BadRequest();
                        
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);

            if (_repository.GetCategoryById(id) == null)
                return NotFound();


            var result=_repository.UpdateCategory(category);

            return Ok(result);
        }


        [HttpGet("{Id}/image")]
        public IActionResult Image(int id)
        {
            var res = _repository.GetCategoryById(id);
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