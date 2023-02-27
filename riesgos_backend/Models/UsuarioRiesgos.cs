using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class UsuarioRiesgos
{
    public UsuarioRiesgos()
    {
       
    }

    public UsuarioRiesgos(string usuario, string? clave)
    {
        Usuario = usuario;
        Clave = clave;
    }

    public string Usuario { get; set; } = null!;

    public string? Clave { get; set; }

    public virtual Usuario1 UsuarioNavigation { get; set; } = null!;
}
