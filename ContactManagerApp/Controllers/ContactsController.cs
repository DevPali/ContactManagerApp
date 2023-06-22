using Microsoft.AspNetCore.Mvc;
using ContactManagerApp.Api.Filters;
using ContactManagerApp.Api.Services;

namespace ContactManagerApp.Controllers
{
    [JwtValidation]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ContactsController : Controller
    {
        //private readonly ApplicationDBContext _context;
        private readonly IAuthService _authService;

        public ContactsController(IAuthService authService)
        {
            //_context = context;
            _authService = authService;
        }
                
        // GET: Contacts
        public IActionResult Index()
        {
            //return _context.Contacts != null ?
            //              View(await _context.Contacts.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDBContext.Contacts'  is null.");
            return View();
        }

        // GET: Contacts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Contacts == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contacts
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contact);
        //}

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Contacts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,City,Prefecture,PostalCode,DateofBirth,Mobile")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contact);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contact = await _context.Contacts.FindAsync(id);
            //if (contact == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        //// POST: Contacts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,City,Prefecture,PostalCode,DateofBirth,Mobile")] Contact contact)
        //{
        //    if (id != contact.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contact);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContactExists(contact.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        // GET: Contacts/Delete/5
        public IActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var contact = await _context.Contacts
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (contact == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        //// POST: Contacts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Contacts == null)
        //    {
        //        return Problem("Entity set 'ApplicationDBContext.Contacts'  is null.");
        //    }
        //    var contact = await _context.Contacts.FindAsync(id);
        //    if (contact != null)
        //    {
        //        _context.Contacts.Remove(contact);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContactExists(int id)
        //{
        //  return (_context.Contacts?.Any(e => e.ID == id)).GetValueOrDefault();
        //}
    }
}
