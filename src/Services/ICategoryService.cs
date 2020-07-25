using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Saves the user object.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>A user object after saving it in the db.</returns>
        Category SaveCategory(Category category);
    }
}
