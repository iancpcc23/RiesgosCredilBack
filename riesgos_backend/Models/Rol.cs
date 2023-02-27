using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class Rol
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<UsuarioRol> UsuarioRols { get; } = new List<UsuarioRol>();
}
