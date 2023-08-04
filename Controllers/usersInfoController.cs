using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IES_project.Data;
using IES_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace IES_project.Controllers
{
    
    public class usersInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public usersInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: usersInfo
        [Authorize(Roles = "Admin,Users")]
        public async Task<IActionResult> Index()
        {
              return _context.usersdata != null ? 
                          View(await _context.usersdata.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.usersdata'  is null.");
        }

        [Authorize(Roles = "Admin,Users")]
        // GET: usersInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.usersdata == null)
            {
                return NotFound();
            }

            var usersEntity = await _context.usersdata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersEntity == null)
            {
                return NotFound();
            }

            return View(usersEntity);
        }

        [Authorize(Roles = "Admin,Users")]
        // GET: usersInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: usersInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] usersEntity usersEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersEntity);
        }

        [Authorize(Roles = "Admin,Users")]
        // GET: usersInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.usersdata == null)
            {
                return NotFound();
            }

            var usersEntity = await _context.usersdata.FindAsync(id);
            if (usersEntity == null)
            {
                return NotFound();
            }
            return View(usersEntity);
        }

        [Authorize(Roles = "Admin,Users")]
        // POST: usersInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] usersEntity usersEntity)
        {
            if (id != usersEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usersEntityExists(usersEntity.Id))
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
            return View(usersEntity);
        }

        [Authorize(Roles = "Admin")]
        // GET: usersInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.usersdata == null)
            {
                return NotFound();
            }

            var usersEntity = await _context.usersdata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersEntity == null)
            {
                return NotFound();
            }

            return View(usersEntity);
        }

        [Authorize(Roles = "Admin")]
        // POST: usersInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usersdata == null)
            {
                return Problem("Entity set 'ApplicationDbContext.usersdata'  is null.");
            }
            var usersEntity = await _context.usersdata.FindAsync(id);
            if (usersEntity != null)
            {
                _context.usersdata.Remove(usersEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usersEntityExists(int id)
        {
          return (_context.usersdata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
