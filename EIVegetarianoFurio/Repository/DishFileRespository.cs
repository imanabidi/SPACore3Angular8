using EIVegetarianoFurio.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EIVegetarianoFurio.Repository
{
    public class DishFileRespository : IDishRepository
    {
        private readonly string _path;

        public DishFileRespository(IHostEnvironment env)
        {
            _path = env.ContentRootPath;
        }

        public Dish Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Dish GetDish(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dish> GetDishs()
        {
            var path = Path.Combine(_path,"data","dishes.json");
            var json = File.ReadAllText(path);

            var js = JsonSerializer.Deserialize<IEnumerable<Dish>>(json);

            return js;
        }

        public Dish Insert(Dish dish)
        {
            throw new NotImplementedException();
        }

        public Dish Update(Dish dish)
        {
            throw new NotImplementedException();
        }
    }


}
