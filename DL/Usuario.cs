using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidopaterno { get; set; } = null!;

    public string Apellidomaterno { get; set; } = null!;

    public int Edad { get; set; }

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;
}
