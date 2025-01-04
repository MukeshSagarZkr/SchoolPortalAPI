using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPortalAPI.Models;

namespace SchoolPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolContactMastersController : ControllerBase
    {
        private readonly SchoolManagementContext _context;

        public SchoolContactMastersController(SchoolManagementContext context)
        {
            _context = context;
        }

        // CREATE: POST api/schoolcontactmasters
        [HttpPost]
        public async Task<ActionResult<SchoolContactMaster>> CreateSchoolContact(SchoolContactMaster schoolContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            schoolContact.CreatedDate = DateTime.UtcNow;
            _context.SchoolContactMasters.Add(schoolContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchoolContact), new { id = schoolContact.SconmId }, schoolContact);
        }

        // READ: GET api/schoolcontactmasters/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolContactMaster>> GetSchoolContact(int id)
        {
            var schoolContact = await _context.SchoolContactMasters.FindAsync(id);

            if (schoolContact == null)
            {
                return NotFound();
            }

            return schoolContact;
        }

        // READ: GET api/schoolcontactmasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolContactMaster>>> GetSchoolContacts()
        {
            return await _context.SchoolContactMasters.Where(sc => !sc.IsDeleted).ToListAsync(); // Optionally filter out deleted contacts
        }

        // UPDATE: PUT api/schoolcontactmasters/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchoolContact(int id, SchoolContactMaster schoolContact)
        {
            if (id != schoolContact.SconmId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSchoolContact = await _context.SchoolContactMasters.FindAsync(id);
            if (existingSchoolContact == null)
            {
                return NotFound();
            }

            // Update fields
            existingSchoolContact.SconmSchId = schoolContact.SconmSchId;
            existingSchoolContact.SconmFirstName = schoolContact.SconmFirstName;
            existingSchoolContact.SconmLastName = schoolContact.SconmLastName;
            existingSchoolContact.SconmEmail = schoolContact.SconmEmail;
            existingSchoolContact.SconmMobilePhone = schoolContact.SconmMobilePhone;
            existingSchoolContact.SconmAddressLine1 = schoolContact.SconmAddressLine1;
            existingSchoolContact.SconmAddressLine2 = schoolContact.SconmAddressLine2;
            existingSchoolContact.SconmPhone = schoolContact.SconmPhone;
            existingSchoolContact.SconmCity = schoolContact.SconmCity;
            existingSchoolContact.SconmState = schoolContact.SconmState;
            existingSchoolContact.SconmCountry = schoolContact.SconmCountry;
            existingSchoolContact.SconmCityId = schoolContact.SconmCityId;
            existingSchoolContact.SconmStateId = schoolContact.SconmStateId;
            existingSchoolContact.IsActive = schoolContact.IsActive;
            existingSchoolContact.IsDeleted = schoolContact.IsDeleted;
            existingSchoolContact.ModifiedBy = schoolContact.ModifiedBy;
            existingSchoolContact.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: DELETE api/schoolcontactmasters/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolContact(int id)
        {
            var schoolContact = await _context.SchoolContactMasters.FindAsync(id);

            if (schoolContact == null)
            {
                return NotFound();
            }

            schoolContact.IsDeleted = true; // Soft delete
            schoolContact.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
