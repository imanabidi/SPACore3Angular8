using EIVegetarianoFurio.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EIVegetarianoFurio.Repository
{
    public class FileDishRespository : IDishRepository
    {
        private readonly string _path;

        public FileDishRespository(IHostEnvironment env)
        {
            _path = env.ContentRootPath;
        }

        public Dish DeleteDish(int id)
        {
            throw new NotImplementedException();
        }

        public Dish GetDish(int id)
        {
            return GetDishs()?.SingleOrDefault(x=>x.Id==id);
        }

        public IEnumerable<Dish> GetDishs()
        {
            var path = Path.Combine(_path, "data", "dishes.json");
            var json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
            var js = JsonSerializer.Deserialize<IEnumerable<Dish>>(json, options);

            return js;
        }

        public Dish CreateDish(Dish dish)
        {
            var dishes = GetDishs().ToList() ?? new List<Dish>();

            if (dishes.Count == 0)
            {
                dish.Id = 1;
            }
            else
            {
                var maxid = dishes.Max(x => x.Id) + 1;
                dish.Id = maxid + 1;
            }

            dishes.Add(dish);

            WriteToFile(dishes);

            return dish;
        }

        private void WriteToFile(List<Dish> dishes)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            var jsontring = JsonSerializer.Serialize(dishes, options);
            File.WriteAllText(_path, jsontring);
        }

        public Dish UpdateDish(Dish dish)
        {
           var dishes= GetDishs().ToList();
           var dishToUpdate = dishes.SingleOrDefault(x=>x.Id  == dish.Id);
           dishToUpdate.Description = dish.Description;
           dishToUpdate.Name = dish.Name;
           dishToUpdate.Price = dish.Price;
           dishToUpdate.CategoryId = dish.CategoryId;
            
           WriteToFile(dishes);

            return dishToUpdate;
        }
    }


}
