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
    public class EfDishRespository : IDishRepository
    {        
        public VegiContext _context { get; }

        public EfDishRespository(VegiContext context)
        {
            _context = context;
        }

        public void DeleteDish(int id)
        {
            var dish = _context.Dishes.Find(id);
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
        }

        public Dish GetDishById(int id)
        {
            var dish = _context.Dishes.Find(id);            
            return dish;
        }

        public IEnumerable<Dish> GetDishes()
        {
            IEnumerable<Dish> categories = _context.Dishes.AsNoTracking().ToList();
            return categories;
        }

        public Dish CreateDish(Dish Dish)
        {
            _context.Add(Dish);
            _context.SaveChanges();
            return Dish;
        }


        public Dish UpdateDish(Dish dish)
        {
            var dishToUpdate = _context.Dishes.Find(dish.Id);
            dishToUpdate.Description = dish.Description;
            dishToUpdate.Name = dish.Name;
            dishToUpdate.Price = dish.Price;
            dishToUpdate.CategoryId = dish.CategoryId;
            _context.SaveChanges();

            return dishToUpdate;
        }
    }


}
