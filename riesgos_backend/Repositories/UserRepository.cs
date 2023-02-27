using backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using riesgos_backend.Models;

namespace backend.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private SoftbankContext _dbContext;

        public UserRepository(SoftbankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario1>> getAllUsers()
            
        {
            try
            {
                return await _dbContext.Usuarios1.ToListAsync();
            }
            catch (Exception ex)
            {

            throw new Exception(ex.Message);
           
            }

        }

        public async Task<Usuario1> getUserByEmail(string email)
        {

            try
            {
                return await _dbContext.Usuarios1.Where(user=> user.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public Task<Usuario1> getUserById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario1> getUserByName(string username)
        {
           try
            {
                Usuario1? usuario = await _dbContext.Usuarios1.Where(user => user.Usuario == username).FirstOrDefaultAsync();
                return usuario;
            }
            catch (Exception ex)
            {

            throw new Exception(ex.Message);
           
            }
        }
    }
}
