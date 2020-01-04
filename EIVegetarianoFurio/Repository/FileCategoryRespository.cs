using EIVegetarianoFurio.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EIVegetarianoFurio.Repository
{
    public class FileCategoryRespository : ICategoryRepository
    {
        private readonly string _path;
        private readonly string _pathDishes;

        public FileCategoryRespository(IHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "data", "categories.json");
            _pathDishes = Path.Combine(env.ContentRootPath, "data", "dishes.json");
        }

        public void DeleteCategory(int id)
        {
            var Categories = GetCategories().Where(x => x.Id != id);
            WriteToFile(Categories);
        }

        public Category GetCategoryById(int id)
        {
            return GetCategories()?.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> GetCategories()
        {
            var json = File.ReadAllText(_path);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
            var categories = JsonSerializer.Deserialize<IEnumerable<Category>>(json, options);

            var jsonDishes = File.ReadAllText(_pathDishes);
            var dishes = JsonSerializer.Deserialize<IEnumerable<Dish>>(jsonDishes, options);

            foreach (var item in categories)
            {
                item.Dishes = dishes.Where(x => x.CategoryId == item.Id).ToList();
            }

            return categories;
        }

        public Category CreateCategory(Category category)
        {
            var Categories = GetCategories().ToList() ?? new List<Category>();
            if (Categories.Count == 0)
            {
                category.Id = 1;
            }
            else             
                category.Id = Categories.Max(x => x.Id) + 1  ;
            
            Categories.Add(category);
            WriteToFile(Categories);
            return category;
        }

        private void WriteToFile(IEnumerable<Category> Categories)
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            var jsontring = JsonSerializer.Serialize(Categories, options);
            File.WriteAllText(_path, jsontring);
        }

        public Category UpdateCategory(Category category)
        {
            var Categories = GetCategories().ToList();
            var CategoryToUpdate = Categories.SingleOrDefault(x => x.Id == category.Id);
            CategoryToUpdate.Description = category.Description;
            CategoryToUpdate.Name = category.Name;
          

            WriteToFile(Categories);
            return CategoryToUpdate;
        }
    }


}
