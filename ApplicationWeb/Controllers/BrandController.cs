using ApplicationWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWeb.Controllers
{
    public class BrandController : Controller
    {
        private readonly StoreContext _storeContext;

        public BrandController(StoreContext storeContext) 
        {
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _storeContext.Brands.ToListAsync());
        }
    }
}
