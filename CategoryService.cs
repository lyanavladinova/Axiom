using Data;
using Data.Entities;
using Data.Models;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private AxiomDbContext context;

        public CategoryService(AxiomDbContext context)
        {
            this.context = context;
        }

        public void Create(CategoryDTO category)
        {
            Category newCategory = new Category();
            newCategory.Name = category.Name;

            this.context.Categories.Add(newCategory);
            this.context.SaveChanges();
        }

        public void Create(List<CategoryDTO> categories)
        {
            foreach (var category in categories) 
            {
                this.Create(category);
            }
        }

        public bool ExistsByName(string name)
        {
            return this.context.Categories.FirstOrDefault(x => x.Name == name) == null ? false : true;
            //if (... == null) => ... = false;
            //else ... = true;
        }

        public List<Category> GetAllCategories(List<string> categoryNames)
        {
            return this.context.Categories
                .Where(x => categoryNames.Contains(x.Name))
                .ToList();
        }
    }
}
