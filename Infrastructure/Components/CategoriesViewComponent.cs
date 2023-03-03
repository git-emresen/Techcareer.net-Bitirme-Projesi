using Bitirme_Projesi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitirme_Projesi.Infrastructure.Components
{
        public class CategoriesViewComponent : ViewComponent
        {
                private readonly ApplicationDbContext _context;

                public CategoriesViewComponent(ApplicationDbContext context)
                {
                        _context = context;
                }

                public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.ToListAsync());
        }
}
