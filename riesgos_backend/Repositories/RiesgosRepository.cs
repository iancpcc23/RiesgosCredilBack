using backend.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using riesgos_backend.Models;

namespace backend.Repositories
{
    public class RiesgosRepository : IRiesgosRepository
    {
        private SoftbankContext _dbContext;
        private String[] _nameListSP = { "SPEstudiantes", "LA_CLIENTEJURIDICO", "LA_CLIENTENATURAL", "LA_CLIENTES", "LA_CUENTA", "LA_CUENTA_MOVIMIENTO", "LA_EMPLEADO", "LA_INVERSION", "LA_INVERSION_MOVIMIENTO", "LA_PRESTAMO", "LA_PRESTAMO_MOVIMIENTO", "LA_VINCULADOS" };

        public RiesgosRepository(SoftbankContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> closeDay(DateTime date)
        {
            try
            {
                foreach (var sp in _nameListSP)
                {
                    var respuesta = await _dbContext.Database.ExecuteSqlInterpolatedAsync($"exec {sp} {date} ");
                }
                return 1;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
