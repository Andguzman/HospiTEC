﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HospiTECAPI.Models;

public partial class Personal
{
    public string Cedula { get; set; } = null!;

    public DateOnly? Fechanacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido1 { get; set; }

    public string? Apellido2 { get; set; }

    public DateOnly? Fechaingreso { get; set; }

    [JsonIgnore] //Funciona para que se ignore y no aparezca en el request del POST
    public virtual ICollection<PersonalTelefono> PersonalTelefonos { get; set; } = new List<PersonalTelefono>();

    [JsonIgnore] //Funciona para que se ignore y no aparezca en el request del POST
    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
