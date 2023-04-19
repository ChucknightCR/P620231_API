using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P620231_API.Models;

namespace P620231_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoveryCodesController : ControllerBase
    {
        private readonly P620231_AutoAppoContext _context;

        public RecoveryCodesController(P620231_AutoAppoContext context)
        {
            _context = context;
        }

        // GET: api/RecoveryCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecoveryCode>>> GetRecoveryCodes()
        {
            if (_context.RecoveryCodes == null)
            {
                return NotFound();
            }
            return await _context.RecoveryCodes.ToListAsync();
        }

        // GET: api/RecoveryCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecoveryCode>> GetRecoveryCode(int id)
        {
            if (_context.RecoveryCodes == null)
            {
                return NotFound();
            }
            var recoveryCode = await _context.RecoveryCodes.FindAsync(id);

            if (recoveryCode == null)
            {
                return NotFound();
            }

            return recoveryCode;
        }

        // GET: api/RecoveryCodes/ValidateCode
        [HttpGet("ValidateCode")]
        public async Task<ActionResult<RecoveryCode>> ValidateCode(string pEmail, string pRecoveryCode)
        {
            if (_context.RecoveryCodes == null)
            {
                return NotFound();
            }

            var recoveryCode = await _context.RecoveryCodes.SingleOrDefaultAsync(e => e.Email == pEmail &&  e.RecoveryCode1 == pRecoveryCode && e.IsUsed == false);

            if (recoveryCode == null)
            {
                return NotFound();
            }

            return recoveryCode;
        }


        // PUT: api/RecoveryCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecoveryCode(int id, RecoveryCode recoveryCode)
        {
            if (id != recoveryCode.RecoveryCodeId)
            {
                return BadRequest();
            }

            _context.Entry(recoveryCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecoveryCodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecoveryCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecoveryCode>> PostRecoveryCode(RecoveryCode recoveryCode)
        {
            if (_context.RecoveryCodes == null)
            {
                return Problem("Entity set 'P620231_AutoAppoContext.RecoveryCodes'  is null.");
            }
            _context.RecoveryCodes.Add(recoveryCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecoveryCode", new { id = recoveryCode.RecoveryCodeId }, recoveryCode);
        }

        // DELETE: api/RecoveryCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecoveryCode(int id)
        {
            if (_context.RecoveryCodes == null)
            {
                return NotFound();
            }
            var recoveryCode = await _context.RecoveryCodes.FindAsync(id);
            if (recoveryCode == null)
            {
                return NotFound();
            }

            _context.RecoveryCodes.Remove(recoveryCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecoveryCodeExists(int id)
        {
            return (_context.RecoveryCodes?.Any(e => e.RecoveryCodeId == id)).GetValueOrDefault();
        }
    }
}