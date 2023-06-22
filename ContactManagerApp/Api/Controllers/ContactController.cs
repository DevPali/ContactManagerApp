using ContactManagerApp.Api.Filters;
using ContactManagerApp.Api.Repositories;
using ContactManagerApp.Api.Responses;
using ContactManagerApp.Api.Services;
using ContactManagerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace ContactManagerApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtValidation]
    public class ContactController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IContactRepository _contactRepository;
        private readonly ApplicationDBContext _context;

        public ContactController(IContactRepository contactRepository, ApplicationDBContext context, IAuthService authService)
        {
            _authService = authService;
            _contactRepository = contactRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            if (_context.Contacts == null)
                return NotFound();
            else
            {
                var contacts = await _context.Contacts.ToListAsync();
                return Ok(contacts);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            if (!isAuthorized())
                return Unauthorized();

            var response = new ContactResponse();
            try
            {
                if (string.IsNullOrWhiteSpace(contact.FirstName))
                {
                    var property = "FirstNameError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The First Name is required.");
                }
                if (string.IsNullOrWhiteSpace(contact.LastName))
                {
                    var property = "LastNameError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The Last Name is required.");
                }
                if (!string.IsNullOrWhiteSpace(contact.Mobile) && !Regex.IsMatch(contact.Mobile, @"^(69)[0-9]{8}$"))
                {
                    var property = "MobileError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The Mobile must begin with 69 and contain 10 digits.");
                }
                if (!string.IsNullOrWhiteSpace(contact.DateofBirth.ToString()) && contact.DateofBirth > DateTime.Now.Date)
                {
                    var property = "DoBError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The Date of Birth must not be a future date.");
                }

                if (response.Errors.Count > 0)
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, response);

                await _contactRepository.AddContact(contact);

                return Ok();
            }
            catch (Exception ex)
            {
                response.Errors.Add("GeneralError", new List<string> { "Something went wrong in add contact. " + ex.Message });
                return StatusCode((int)HttpStatusCode.Unauthorized, response);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, Contact contact)
        {
            if (!isAuthorized())
                return Unauthorized();

            contact.ID = id;
            var response = new ContactResponse();
            try
            {
                if (string.IsNullOrWhiteSpace(contact.FirstName))
                {
                    var property = "FirstNameError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The First Name is required.");
                }
                if (string.IsNullOrWhiteSpace(contact.LastName))
                {
                    var property = "LastNameError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The Last Name is required.");
                }
                if (!string.IsNullOrWhiteSpace(contact.Mobile) && !Regex.IsMatch(contact.Mobile, @"^(69)[0-9]{8}$"))
                {
                    var property = "MobileError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The Mobile must begin with 69 and contain 10 digits.");
                }
                if (!string.IsNullOrWhiteSpace(contact.DateofBirth.ToString()) && contact.DateofBirth > DateTime.Now.Date)
                {
                    var property = "DoBError";
                    response.Errors.Add(property, new List<string>());
                    response.Errors[property].Add("The Date of Birth must not be a future date.");
                }

                if (response.Errors.Count > 0)
                    return StatusCode((int)HttpStatusCode.UnprocessableEntity, response);

                _contactRepository.UpdateContact(contact);

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                response.Errors.Add("GeneralError", new List<string> { "Something went wrong in add contact. " + ex.Message });
                return StatusCode((int)HttpStatusCode.Unauthorized, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (!isAuthorized())
                return Unauthorized();

            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                return NotFound();

            await _contactRepository.DeleteContact(contact);

            return Ok();
        }

        private bool isAuthorized()
        {
            var jwt = _authService.GetJwtFromCookies(HttpContext);
            if (_authService.GetUserRoleIdFromJwt(jwt) == "1")
                return true;
            else
                return false;
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }
    }
}
