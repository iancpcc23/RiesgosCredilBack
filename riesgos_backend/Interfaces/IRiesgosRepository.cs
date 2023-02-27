
namespace backend.Interfaces
{
    public interface IRiesgosRepository
    {
        public Task<int> closeDay(DateTime date);
        //public Task<UsuarioRlaRlm> finUserById(string user);
    }
}
