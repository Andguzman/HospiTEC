using HospiTECAPI.Models;
using HospiTECAPI.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospiTECAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservaController : ControllerBase
{
    private readonly HospitecContext _context;

    public ReservaController(HospitecContext context)
    {
        _context = context;
    }
    // GET: api/Reserva
[HttpGet]
public async Task<IActionResult> GetAllReservas()
{
    var reservas = await _context.Reservas
        .Select(r => new {
            idReservacion = r.Idreservacion,
            pacienteCedula = r.Pacientecedula,
            idCama = r.Idcama,
            idProced = r.Idproced,
            fechaIngreso = r.Fechaingreso,
            fechaSalida = r.Fechasalida
        })
        .ToListAsync();

    if (reservas == null || !reservas.Any())
        return NotFound();

    return Ok(reservas);
}

// GET: api/Reserva/{idReservacion}
[HttpGet("{idReservacion}")]
public async Task<IActionResult> GetReserva(int idReservacion)
{
    var reserva = await _context.Reservas.FindAsync(idReservacion);

    if (reserva == null)
        return NotFound($"No se encontró una reserva con el id {idReservacion}.");

    return Ok(reserva);
}

// POST: api/Reserva
[HttpPost]
public async Task<IActionResult> PostReserva([FromBody] ReservaDTO dto)
{
    if (!DateTime.TryParse(dto.Fechaingreso, out var fechanacimientoParsed))
    {
        return BadRequest("Fecha inválida.");
    }
    if (!DateTime.TryParse(dto.Fechasalida, out var fechasalidaParsed))
    {
        return BadRequest("Fecha inválida.");
    }
    

    var nuevoReserva = new Reserva
    {
       Pacientecedula = dto.Pacientecedula,
       Idcama = dto.Idcama,
       Idproced = dto.Idproced,
       Fechaingreso = DateOnly.FromDateTime(fechanacimientoParsed),
       Fechasalida = DateOnly.FromDateTime(fechasalidaParsed)
    };
    _context.Reservas.Add(nuevoReserva);
    await _context.SaveChangesAsync();
    return CreatedAtAction("GetAllReservas", new { id = nuevoReserva.Idreservacion}, nuevoReserva);
}

// PUT: api/Reserva/{idReservacion}
[HttpPut("{idReservacion}")]
public async Task<IActionResult> UpdateReserva(int idReservacion, [FromBody] Reserva reservaUpdated)
{
    var reserva = await _context.Reservas.FindAsync(idReservacion);
    if (reserva == null)
    {
        return NotFound($"No se encontró una reserva con el id {idReservacion}.");
    }
    if (reservaUpdated.Pacientecedula != null) reserva.Pacientecedula = reservaUpdated.Pacientecedula;
    if (reservaUpdated.Idcama != null) reserva.Idcama = reservaUpdated.Idcama;
    if (reservaUpdated.Idproced != null) reserva.Idproced = reservaUpdated.Idproced;
    if (reservaUpdated.Fechaingreso != null) reserva.Fechaingreso = reservaUpdated.Fechaingreso;
    if (reservaUpdated.Fechasalida != null) reserva.Fechasalida = reservaUpdated.Fechasalida;
    _context.Reservas.Update(reserva);
    await _context.SaveChangesAsync();
    return NoContent();
}

// DELETE: api/Reserva/{idReservacion}
[HttpDelete("{idReservacion}")]
public async Task<IActionResult> DeleteReserva(int idReservacion)
{
    var reserva = await _context.Reservas.FindAsync(idReservacion);
    if (reserva == null)
    {
        return NotFound($"No se encontró una reserva con el id {idReservacion}.");
    }

    _context.Reservas.Remove(reserva);
    await _context.SaveChangesAsync();
    return NoContent();
}

    
}