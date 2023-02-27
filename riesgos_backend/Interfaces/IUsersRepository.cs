
using riesgos_backend.Models;

namespace backend.Interfaces
{
    public interface IUsersRepository
    {
        public Task<List<Usuario1>> getAllUsers();
        public Task <Usuario1> getUserById(string id);
        public Task<Usuario1> getUserByName(String name);
        public Task<Usuario1> getUserByEmail(String email);
    }
}
