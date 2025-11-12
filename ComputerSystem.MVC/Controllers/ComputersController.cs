using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerSystem.Infrastructure;
using ComputerSystem.Infrastructure.Models;

namespace ComputerSystem.MVC.Controllers
{
    public class ComputersController : Controller
    {
        private readonly ComputerSystemContext _context;

        public ComputersController(ComputerSystemContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Computers.ToListAsync());
        }

        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computerModel = await _context.Computers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computerModel == null)
            {
                return NotFound();
            }

            return View(computerModel);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt")] ComputerModel computerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(computerModel);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computerModel = await _context.Computers.FindAsync(id);
            if (computerModel == null)
            {
                return NotFound();
            }
            return View(computerModel);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedAt")] ComputerModel computerModel)
        {
            if (id != computerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerModelExists(computerModel.Id))
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
            return View(computerModel);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computerModel = await _context.Computers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computerModel == null)
            {
                return NotFound();
            }

            return View(computerModel);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computerModel = await _context.Computers.FindAsync(id);
            if (computerModel != null)
            {
                _context.Computers.Remove(computerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerModelExists(int id)
        {
            return _context.Computers.Any(e => e.Id == id);
        }
    }
}
