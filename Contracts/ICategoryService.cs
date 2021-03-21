using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        void Create(CategoryDTO category);

        void Create(List<CategoryDTO> categories);

        bool ExistsByName(string name);

        List<Category> GetAllCategories(List<string> categoryNames);
    }
}
