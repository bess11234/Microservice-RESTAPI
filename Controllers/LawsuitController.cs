using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LawsuitM.Models;


namespace LawsuitM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LawsuitController : ControllerBase
{
    private readonly LawsuitContext _context;

    public LawsuitController(LawsuitContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LawsuitItem>>> GetLawsuits()
    {
        return await _context.LawsuitItem.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LawsuitItem>> GetLawsuit(long id)
    {
        var lawsuit = await _context.LawsuitItem.FindAsync(id);

        if (lawsuit == null)
        {
            return NotFound();
        }

        return lawsuit;
    }

    [HttpPost]
    public async Task<ActionResult<LawsuitItem>> PostLawsuit(LawsuitItem lawsuit)
    {
        _context.LawsuitItem.Add(lawsuit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLawsuit), new { id = lawsuit.Id }, lawsuit);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLawsuit(long id, LawsuitItem lawsuit)
    {
        if (id != lawsuit.Id)
        {
            return BadRequest();
        }

        _context.Entry(lawsuit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LawsuitExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLawsuit(long id)
    {
        var lawsuit = await _context.LawsuitItem.FindAsync(id);
        if (lawsuit == null)
        {
            return NotFound();
        }

        _context.LawsuitItem.Remove(lawsuit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LawsuitExists(long id)
    {
        return _context.LawsuitItem.Any(e => e.Id == id);
    }
}
