using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class Usuario
{
    public string Codigo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Cambioclaveproximoingreso { get; set; }

    public bool Interno { get; set; }

    public DateTime Fechacreacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<UsuarioRol> UsuarioRols { get; } = new List<UsuarioRol>();
}
