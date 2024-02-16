using ApplicationWeb.Models;
using ApplicationWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ApplicationWeb.Controllers
{
    public class BeerController : Controller
    {

        private readonly StoreContext _storeContext;

        public BeerController(StoreContext storeContext) 
        {
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Index()
        {
            var beers = _storeContext.Beers.Include(b => b.Brand);
            return View(await beers.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create() 
        {

            ViewData["Brands"] = new SelectList(_storeContext.Brands, "BrandId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel beerViewModel)
        {

            if (ModelState.IsValid) 
            {
                var beer = new Beer()
                {
                    Name = beerViewModel.Name,
                    BrandId = beerViewModel.BrandID
                };
                 _storeContext.Beers.Add(beer);
                await _storeContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["Brands"] = new SelectList(_storeContext.Brands, "BrandId", "Name", beerViewModel.BrandID);

            return View();
        }

    }
}
