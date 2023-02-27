using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class Rol1
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Nivel { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<UsuarioRol1> UsuarioRol1s { get; } = new List<UsuarioRol1>();
}
