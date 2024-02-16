using System;
using System.Collections.Generic;

namespace ApplicationWeb.Models;

public partial class Log
{
    public int? IdLog { get; set; }

    public string? Accion { get; set; }

    public byte[]? FechaLog { get; set; }
}
