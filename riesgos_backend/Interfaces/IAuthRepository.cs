
using riesgos_backend.Models;

namespace backend.Interfaces
{
    public interface IAuthRepository
    {

        public Task<int> register(UsuarioRiesgos user);
    }
}
