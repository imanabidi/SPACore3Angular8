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
            {
                //return EmptyResult();
                 throw new Exception("empty result");
            }

            return Ok(res);
        }

    }
}