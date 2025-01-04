using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPortalAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly SchoolManagementContext _context;
        public CompanyController(SchoolManagementContext context)
        {
            _context = context;
        }        

        // Create: POST api/companies
        [HttpPost]
        public async Task<ActionResult<CompanyMaster>> CreateCompany(CompanyMaster company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            company.CreatedDate = DateTime.UtcNow;
            _context.CompanyMasters.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompany), new { id = company.CmpId }, company);
        }

        // Read: GET api/companies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyMaster>> GetCompany(int id)
        {
            var company = await _context.CompanyMasters.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // Read: GET api/companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyMaster>>> GetCompanies()
        {
            return await _context.CompanyMasters.Where(c => !c.IsDeleted).ToListAsync(); // Optionally filter out deleted companies
        }

        // Update: PUT api/companies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyMaster company)
        {
            if (id != company.CmpId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCompany = await _context.CompanyMasters.FindAsync(id);
            if (existingCompany == null)
            {
                return NotFound();
            }

            // Update fields
            existingCompany.CmpName = company.CmpName;
            existingCompany.CmpDescription = company.CmpDescription;
            existingCompany.CmpAddress = company.CmpAddress;
            existingCompany.CmpCity = company.CmpCity;
            existingCompany.CmpState = company.CmpState;
            existingCompany.CmpCountry = company.CmpCountry;
            existingCompany.CmpEmail = company.CmpEmail;
            existingCompany.CmpStateId = company.CmpStateId;
            existingCompany.CmpCityId = company.CmpCityId;
            existingCompany.CmpEstablishmentYear = company.CmpEstablishmentYear;
            existingCompany.CmpJurisdictionArea = company.CmpJurisdictionArea;
            existingCompany.IsActive = company.IsActive;
            existingCompany.IsDeleted = company.IsDeleted;
            existingCompany.ModifiedBy = company.ModifiedBy;
            existingCompany.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete: DELETE api/companies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.CompanyMasters.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            company.IsDeleted = true; // Soft delete
            company.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
