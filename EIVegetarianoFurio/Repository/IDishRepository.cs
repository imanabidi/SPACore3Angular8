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

        Dish CreateDish(Dish dish);

        Dish UpdateDish(Dish dish);

        Dish DeleteDish(int id);

    }


}
