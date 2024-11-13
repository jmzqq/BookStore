using Bookstore.Controllers;
using Bookstore.Data;
using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using System.ComponentModel;

namespace Bookstore.Services
{
	public class GenreService
	{
		private readonly BookstoreContext _context;

		public GenreService(BookstoreContext context)
		{
			_context = context;
		}

		public async Task<List<Genre>> FindAllAsync()
		{
			return await _context.Genres.ToListAsync();
		}

		public async Task InsertAsync(Genre genre)
		{
			_context.Add(genre);
			await _context.SaveChangesAsync();
		}

		public async Task<Genre> FindByIdAsync(int id)
		{
			return await _context.Genres.FindAsync(id); //Aqui é só quando vc quer achar o id, mas pode usar o "FirstOrDefault" mas teria que usar a arrow function e é usada em situações mais especificas	
		}

		public async Task RemoveAsync(int id)
		{
			try
			{
				Genre genre = await _context.Genres.FindAsync(id);
				_context.Remove(genre);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new IntegrityException(ex.Message);
			}
		}
	}
}
