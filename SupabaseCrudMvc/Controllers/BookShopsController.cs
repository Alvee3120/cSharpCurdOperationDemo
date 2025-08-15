using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupabaseCrudMvc.Data;
using SupabaseCrudMvc.Models;

namespace SupabaseCrudMvc.Controllers
{
    public class BookShopsController : Controller
    {
        private readonly SupabaseCrudMvcContext _context;

        public BookShopsController(SupabaseCrudMvcContext context)
        {
            _context = context;
        }

        // GET: BookShops
        public async Task<IActionResult> Index()
        {
            var supabaseCrudMvcContext = _context.BookShop.Include(b => b.Book).Include(b => b.Shop);
            return View(await supabaseCrudMvcContext.ToListAsync());
        }

        // GET: BookShops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookShop = await _context.BookShop
                .Include(b => b.Book)
                .Include(b => b.Shop)
                .FirstOrDefaultAsync(m => m.BookShopId == id);
            if (bookShop == null)
            {
                return NotFound();
            }

            return View(bookShop);
        }

        // GET: BookShops/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Author");
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "Name");
            return View();
        }

        // POST: BookShops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookShopId,BookId,ShopId,Quantity")] BookShop bookShop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookShop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Author", bookShop.BookId);
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "Name", bookShop.ShopId);
            return View(bookShop);
        }

        // GET: BookShops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookShop = await _context.BookShop.FindAsync(id);
            if (bookShop == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Author", bookShop.BookId);
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "Name", bookShop.ShopId);
            return View(bookShop);
        }

        // POST: BookShops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookShopId,BookId,ShopId,Quantity")] BookShop bookShop)
        {
            if (id != bookShop.BookShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookShop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookShopExists(bookShop.BookShopId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "BookId", "Author", bookShop.BookId);
            ViewData["ShopId"] = new SelectList(_context.Shop, "ShopId", "Name", bookShop.ShopId);
            return View(bookShop);
        }

        // GET: BookShops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookShop = await _context.BookShop
                .Include(b => b.Book)
                .Include(b => b.Shop)
                .FirstOrDefaultAsync(m => m.BookShopId == id);
            if (bookShop == null)
            {
                return NotFound();
            }

            return View(bookShop);
        }

        // POST: BookShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookShop = await _context.BookShop.FindAsync(id);
            if (bookShop != null)
            {
                _context.BookShop.Remove(bookShop);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookShopExists(int id)
        {
            return _context.BookShop.Any(e => e.BookShopId == id);
        }
    }
}
