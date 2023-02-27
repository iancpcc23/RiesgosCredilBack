using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class UsuarioRol
{
    public int Id { get; set; }

    public string Codigousuario { get; set; } = null!;

    public string Codigorol { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual Rol CodigorolNavigation { get; set; } = null!;

    public virtual Usuario CodigousuarioNavigation { get; set; } = null!;
}
