using EIVegetarianoFurio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIVegetarianoFurio.Repository
{
    public interface ICategoryRepository
    {
        public Category GetCategoryById(int id);
        public IEnumerable<Category> GetCategories();

        Category CreateCategory(Category Category);

        Category UpdateCategory(Category Category);

        void DeleteCategory(int id);

    }


}
