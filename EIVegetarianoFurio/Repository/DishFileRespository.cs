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
            _path = Path.Combine(env.ContentRootPath, "data", "dishes.json");
        }

        public void DeleteDish(int id)
        {
            var dishes = GetDishes().Where(x => x.Id != id);
            WriteToFile(dishes);
        }

        public Dish GetDishById(int id)
        {
            return GetDishes()?.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Dish> GetDishes()
        {
            var json = File.ReadAllText(_path);
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
            var dishes = GetDishes().ToList() ?? new List<Dish>();
            if (dishes.Count == 0)
            {
                dish.Id = 1;
            }
            else             
                dish.Id = dishes.Max(x => x.Id) + 1  ;
            
            dishes.Add(dish);
            WriteToFile(dishes);
            return dish;
        }

        private void WriteToFile(IEnumerable<Dish> dishes)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            var jsontring = JsonSerializer.Serialize(dishes, options);
            File.WriteAllText(_path, jsontring);
        }

        public Dish UpdateDish(Dish dish)
        {
            var dishes = GetDishes().ToList();
            var dishToUpdate = dishes.SingleOrDefault(x => x.Id == dish.Id);
            dishToUpdate.Description = dish.Description;
            dishToUpdate.Name = dish.Name;
            dishToUpdate.Price = dish.Price;
            dishToUpdate.CategoryId = dish.CategoryId;

            WriteToFile(dishes);
            return dishToUpdate;
        }
    }


}
