using IntroASP.Models;
using IntroASP.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IntroASP.Controllers
{
	public class BeerController : Controller
	{
		private readonly CursoaspnetContext _context;
		public BeerController(CursoaspnetContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var beers = _context.Beers.Include(b => b.Brand);
			return View(await beers.OrderByDescending(b => b.Beerid).ToListAsync());
		}

		public IActionResult Create() 
		{
			ViewData["Brands"] = new SelectList(_context.Brands, "brandid", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BeerViewModel model)
		{
			if (ModelState.IsValid)
			{
				var beer = new Beer()
				{
					Name = model.Name,
					Brandid = model.brandid
				};

				_context.Beers.Add(beer);
				await _context.SaveChangesAsync();
				Console.WriteLine("Guardado");
				return RedirectToAction(nameof(Index));
			}


			ViewData["Brands"] = new SelectList(_context.Brands, "brandid", "Name", model.brandid);
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Console.WriteLine("Beer recibido: " + id);
			var selectedBeer = _context.Beers.FirstOrDefault(beer => beer.Beerid == id);
			if(selectedBeer == null)
			{
				return RedirectToAction(nameof(Index));
			}
			ViewData["Brands"] = new SelectList(_context.Brands, "brandid", "Name");
			ViewData["BeerName"] = selectedBeer.Name;
			ViewData["BeerId"] = selectedBeer.Beerid;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(BeerEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				var sentBeer = new Beer()
				{
					Name = model.Name,
					Brandid = model.brandid
				};

				var SQLBeer =  _context.Beers.FirstOrDefault(b => b.Beerid == int.Parse(model.Id));
				
				if(SQLBeer != null)
				{
					SQLBeer.Name = sentBeer.Name;
					SQLBeer.Brandid = sentBeer.Brandid;
				}

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));

			}

			ViewData["Brands"] = new SelectList(_context.Brands, "brandid", "Name",model.brandid);
			ViewData["BeerId"] = int.Parse(model.Id);
			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Delete(int Beerid)
		{
			Console.WriteLine(Beerid);
			var toDeleteBeer = _context.Beers.FirstOrDefault(b => b.Beerid == Beerid);

			if (toDeleteBeer != null)
			{
				_context.Remove(toDeleteBeer);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return NotFound();
		}

	}
}
