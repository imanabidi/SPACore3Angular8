using EIVegetarianoFurio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIVegetarianoFurio.Repository
{
    public interface IDishRepository
    {
        public Dish GetDish(int id);
        public IEnumerable<Dish> GetDishs();

        Dish Insert(Dish dish);

        Dish Update(Dish dish);

        Dish Delete(int id);

    }


}
