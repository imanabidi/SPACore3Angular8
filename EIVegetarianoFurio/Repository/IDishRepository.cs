using EIVegetarianoFurio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIVegetarianoFurio.Repository
{
    public interface IDishRepository
    {
        public Dish GetDishById(int id);
        public IEnumerable<Dish> GetDishes();

        Dish CreateDish(Dish dish);

        Dish UpdateDish(Dish dish);

        void DeleteDish(int id);

    }


}
