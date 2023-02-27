using backend.Interfaces;
using backendv2.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using riesgos_backend.Models;

namespace backend.Repositories
{
    public class UserRiesgosRepository : IUsuarioRiesgosRepository
    {
        private SoftbankContext _dbContext;
        private String[] _nameListSP = { "SPEstudiantes" };

        public UserRiesgosRepository(SoftbankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> saveUser(UsuarioRiesgos user)

        {
            try
            {
                _dbContext.UsuarioRiesgos.Add(user);
                var result = await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<UsuarioRiesgos> findUserById(string username)

        {
            try
            {
                return await _dbContext.UsuarioRiesgos.Where(user => user.Usuario == username).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }
        }


}
