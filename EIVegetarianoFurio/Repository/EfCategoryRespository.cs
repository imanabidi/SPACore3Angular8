using EIVegetarianoFurio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EIVegetarianoFurio.Repository
{
    public class EfCategoryRespository : ICategoryRepository
    {
        public VegiContext _context { get; }

        public EfCategoryRespository(VegiContext context)
        {
            _context = context;
        }

        public void DeleteCategory(int id)
        {
            var cat = _context.Categories.Find(id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            var cat=  _context.Categories.Find(id);
            _context.Entry(cat).Collection(x => x.Dishes).Load();
            return cat;
        }

        public IEnumerable<Category> GetCategories()
        {
            IEnumerable<Category> categories = _context.Categories.AsNoTracking().Include(c => c.Dishes).ToList();
            return categories;
        }

        public Category CreateCategory(Category Category)
        {
             _context.Add(Category);
            _context.SaveChanges();
            return Category;
        }
 

        public Category UpdateCategory(Category category)
        {
            var categoryToUpdate = _context.Categories.Find(category.Id);
            categoryToUpdate.Description = category.Description;
            categoryToUpdate.Name = category.Name;
            _context.SaveChanges();
            
            return categoryToUpdate;
        }
    }


}
