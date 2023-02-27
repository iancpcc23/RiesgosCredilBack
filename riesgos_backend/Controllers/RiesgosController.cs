using backend.Interfaces;
using backendv2.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using riesgos_backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class RiesgosController : Controller
    {
        private readonly IRiesgosRepository _riesgosRepository;
        private readonly IUsuarioRiesgosRepository _usuarioRiesgosRepository;
        private readonly IJwt _jwt;
        public RiesgosController(IRiesgosRepository riesgosRepository, IJwt jwt, IUsuarioRiesgosRepository usuarioLAyLRMRepository )
        {
            this._riesgosRepository = riesgosRepository;
            _jwt = jwt;
            _usuarioRiesgosRepository = usuarioLAyLRMRepository;
        }



        [HttpPost("copyInformation")]
        public async Task<IActionResult> copyInformation(String date)
        {
            try
            {
                var dateFormatted = Convert.ToDateTime(date);
                string authHeader = HttpContext.Request.Headers["Authorization"];
                if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest(new HttpResponseApi("Operación cancelada. Acceso no autorizado", ((int)HttpCode.UNAUTHORIZED_REQUEST), HttpStatusCode.UNAUTHORIZED_REQUEST.ToString(), null, "Token inválido"));
                }
                authHeader = authHeader.Split(" ")[1];

                var userId= _jwt.verifyToken(authHeader);
                 
                var userValidated = await _usuarioRiesgosRepository.findUserById(userId);

                if (userValidated == null)
                {
                    return BadRequest(new HttpResponseApi("Operación cancelada.  Acceso no autorizado", ((int)HttpCode.UNAUTHORIZED_REQUEST), HttpStatusCode.UNAUTHORIZED_REQUEST.ToString(), null, "Token inválido"));
                }

                var data = await this._riesgosRepository.closeDay(dateFormatted);
                return Ok(new HttpResponseApi(message: "Carga Exitosa",code: (int)HttpCode.OK,status: HttpStatusCode.OK.ToString() )) ;

            }
            catch (Exception ex)
            {

                return StatusCode(500, new HttpResponseApi("Error interno en el servidor", ((int)HttpCode.INTERNAL_SERVER_ERROR), HttpStatusCode.INTERNAL_SERVER_ERROR.ToString(), null, ex.Message));

            }
        }
    }
}
