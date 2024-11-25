using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using System.Security.AccessControl;

namespace Bookstore.Controllers
{
	public class GenresController : Controller
	{
		private readonly GenreService _service;

		public GenresController(GenreService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _service.FindAllAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _service.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { Message = ex.Message });
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não foi fornecido" });
            }
            Genre genre = await _service.FindByIdAsync(id.Value);
            if (genre is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi encontrado" });
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Genre genre)
		{
			if (!ModelState.IsValid)
            {
                return View();
            }

			if (id != genre.Id)
			{
				return RedirectToAction(nameof(Error), new { message = "ID's não condizem" });
			}

			try
			{
				await _service.UpdateAsync(genre);
				return RedirectToAction(nameof(Index));
			}
			catch (ApplicationException ex) 
			{
				return RedirectToAction(nameof(Error), new { message = ex.Message}	);
			}
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não foi fornecido" });
            }
            Genre genre = await _service.FindByIdAsync(id.Value);
            if (genre is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi encontrado" });
            }

            return View(genre);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id não foi fornecido" });
            }
            Genre genre = await _service.FindByIdAsync(id.Value);
            if (genre is null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não foi encontrado" });
            }

            return View(genre);
        }

        public async Task<IActionResult> Create(Genre genre)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			await _service.InsertAsync(genre);

			return RedirectToAction(nameof(Index));
		}

        public IActionResult Error(string message)
        {
			var viewModel = new ErrorViewModel
			{
				Message = message,
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			};

			return View(viewModel);
        }

        
    }
}
