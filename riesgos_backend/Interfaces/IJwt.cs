
using riesgos_backend.Models;

namespace backend.Interfaces
{
    public interface IJwt
    {
        public string createToken (UsuarioRiesgos payload);
        public dynamic verifyToken (string token);
    }
}
