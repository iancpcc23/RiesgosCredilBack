using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class UsuarioRol1
{
    public int Id { get; set; }

    public string Usuarioregistro { get; set; } = null!;

    public string Codigorol { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual Rol1 CodigorolNavigation { get; set; } = null!;

    public virtual Usuario1 UsuarioregistroNavigation { get; set; } = null!;
}
