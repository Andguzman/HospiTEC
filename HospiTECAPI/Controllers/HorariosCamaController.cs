using HospiTECAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospiTECAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HorariosCamaController : ControllerBase
{
    private readonly HospitecContext _context;

    public HorariosCamaController(HospitecContext context)
    {
        _context = context;
    }
    // GET: api/Horarioscama
[HttpGet]
public async Task<IActionResult> GetAllHorariosCama()
{
    var horarios = await _context.Horarioscamas
        .Select(h => new {
            idHorario = h.Idhorario,
            idCama = h.Idcama,
            diaInicio = h.Dialinicio,
            diaFinal = h.Diafinal,
            dni = h.Dni
        })
        .ToListAsync();

    if (horarios == null || !horarios.Any())
        return NotFound();

    return Ok(horarios);
}

// GET: api/Horarioscama/{idHorario}
[HttpGet("{idHorario}")]
public async Task<IActionResult> GetHorarioCama(int idHorario)
{
    var horario = await _context.Horarioscamas.FindAsync(idHorario);

    if (horario == null)
        return NotFound($"No se encontró un horario con el id {idHorario}.");

    return Ok(horario);
}

// POST: api/Horarioscama
[HttpPost]
public async Task<IActionResult> PostHorarioCama([FromBody] Horarioscama dto)
{
    var nuevoHorario = new Horarioscama
    {
        Idcama = dto.Idcama,
        Dialinicio = dto.Dialinicio,
        Diafinal = dto.Diafinal,
        Dni = dto.Dni
    };
    _context.Horarioscamas.Add(nuevoHorario);
    await _context.SaveChangesAsync();
    return CreatedAtAction("GetAllHorariosCama", new { idHorario = nuevoHorario.Idhorario }, nuevoHorario);
}

// PUT: api/Horarioscama/{idHorario}
[HttpPut("{idHorario}")]
public async Task<IActionResult> UpdateHorarioCama(int idHorario, [FromBody] Horarioscama horarioUpdated)
{
    var horario = await _context.Horarioscamas.FindAsync(idHorario);
    if (horario == null)
    {
        return NotFound($"No se encontró un horario con el id {idHorario}.");
    }
    if (horarioUpdated.Idcama != null) horario.Idcama = horarioUpdated.Idcama;
    if (horarioUpdated.Dialinicio != null) horario.Dialinicio = horarioUpdated.Dialinicio;
    if (horarioUpdated.Diafinal != null) horario.Diafinal = horarioUpdated.Diafinal;
    if (horarioUpdated.Dni != null) horario.Dni = horarioUpdated.Dni;
    _context.Horarioscamas.Update(horario);
    await _context.SaveChangesAsync();
    return NoContent();
}

// DELETE: api/Horarioscama/{idHorario}
[HttpDelete("{idHorario}")]
public async Task<IActionResult> DeleteHorarioCama(int idHorario)
{
    var horario = await _context.Horarioscamas.FindAsync(idHorario);
    if (horario == null)
    {
        return NotFound($"No se encontró un horario con el id {idHorario}.");
    }

    _context.Horarioscamas.Remove(horario);
    await _context.SaveChangesAsync();
    return NoContent();
}

    
}