using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPortalAPI.Models;

namespace SchoolPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolManagementContext _context;
        public SchoolController(SchoolManagementContext context)
        {
            _context = context;
        }

        // CREATE: POST api/schools
        [HttpPost]
        public async Task<ActionResult<SchoolMaster>> CreateSchool(SchoolMaster school)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            school.CreatedDate = DateTime.UtcNow;
            _context.SchoolMasters.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchool), new { id = school.SchId }, school);
        }

        // READ: GET api/schools/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolMaster>> GetSchool(int id)
        {
            var school = await _context.SchoolMasters.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }

        // READ: GET api/schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolMaster>>> GetSchools()
        {
            return await _context.SchoolMasters.Where(s => !s.IsDeleted).ToListAsync(); // Optionally filter out deleted schools
        }

        // UPDATE: PUT api/schools/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, SchoolMaster school)
        {
            if (id != school.SchId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSchool = await _context.SchoolMasters.FindAsync(id);
            if (existingSchool == null)
            {
                return NotFound();
            }

            // Update fields
            existingSchool.SchName = school.SchName;
            existingSchool.SchDescription = school.SchDescription;
            existingSchool.SchEmail = school.SchEmail;
            existingSchool.SchAddress = school.SchAddress;
            existingSchool.SchCity = school.SchCity;
            existingSchool.SchState = school.SchState;
            existingSchool.SchCountry = school.SchCountry;
            existingSchool.SchZipcode = school.SchZipcode;
            existingSchool.SchPhone = school.SchPhone;
            existingSchool.SchMobile = school.SchMobile;
            existingSchool.SchCmpId = school.SchCmpId;
            existingSchool.SchStateId = school.SchStateId;
            existingSchool.SchCityId = school.SchCityId;
            existingSchool.SchEstablishmentYear = school.SchEstablishmentYear;
            existingSchool.SchJurisdictionArea = school.SchJurisdictionArea;
            existingSchool.SchBankAddress = school.SchBankAddress;
            existingSchool.SchBankAccountNumber = school.SchBankAccountNumber;
            existingSchool.IsActive = school.IsActive;
            existingSchool.IsDeleted = school.IsDeleted;
            existingSchool.ModifiedBy = school.ModifiedBy;
            existingSchool.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: DELETE api/schools/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.SchoolMasters.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            school.IsDeleted = true; // Soft delete
            school.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
