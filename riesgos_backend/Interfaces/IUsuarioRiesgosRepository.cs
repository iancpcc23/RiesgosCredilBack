using riesgos_backend.Models;

namespace backendv2.Interfaces
{
    public interface IUsuarioRiesgosRepository
    {
        public Task<int> saveUser(UsuarioRiesgos user);
        public Task<UsuarioRiesgos> findUserById(string username);
    }
}
