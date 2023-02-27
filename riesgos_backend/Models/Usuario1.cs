using System;
using System.Collections.Generic;

namespace riesgos_backend.Models;

public partial class Usuario1
{
    public string Usuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public int Idagencia { get; set; }

    public string Email { get; set; } = null!;

    public DateTime Fechacreacion { get; set; }

    public DateTime Fechacambioclave { get; set; }

    public bool Cambiaclave { get; set; }

    public int Diascambioclave { get; set; }

    public bool Tienebloqueo { get; set; }

    public bool Puedeingresarsistema { get; set; }

    public bool Activo { get; set; }

    public bool Usadispositivomovil { get; set; }

    public virtual UsuarioRiesgos? UsuarioRiesgosapp { get; set; }

    public virtual ICollection<UsuarioRol1> UsuarioRol1s { get; } = new List<UsuarioRol1>();
}
