using backend.Interfaces;
using backendv2.Interfaces;
using Microsoft.EntityFrameworkCore;
using riesgos_backend.Models;

namespace backend.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private IUsuarioRiesgosRepository   _usuarioRiesgosRepository;
        public AuthRepository(IUsuarioRiesgosRepository usuarioLAyLRMRepository)
        {
            _usuarioRiesgosRepository = usuarioLAyLRMRepository;
        }

        public async Task<int> register(UsuarioRiesgos user)
        {
            
            try
            {
                return await _usuarioRiesgosRepository.saveUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }


    }
    }
