using backend.Interfaces;
using backendv2.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using riesgos_backend.Models;
using System.Text.Json.Serialization;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IJwt _jwt;
        private readonly ICrypto _crypto;
        private readonly IUsuarioRiesgosRepository _usuarioRiesgosRepository;
        private readonly IUsersRepository _usuarioRepository;
        public AuthController(
            IAuthRepository authRepository
            , IUsersRepository usuarioRepository
            , IUsuarioRiesgosRepository usuarioRiesgosRepository
            , IJwt jsonToken
            , ICrypto crypto
            )
        {
            _authRepo = authRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioRiesgosRepository = usuarioRiesgosRepository;
            _crypto = crypto;
            _jwt = jsonToken;
        }


        [HttpPost("login")]
        public async Task<IActionResult> loginWithUsernameAndPassword([FromBody] Object req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<UsuarioRiesgos>(req.ToString());
                if (String.IsNullOrEmpty(data.Usuario))
                {
                    return BadRequest(new HttpResponseApi("Error de parámetros", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(), null, $"No existe el campo usuario"));
                }

                if (String.IsNullOrEmpty(data.Clave))
                {
                    return BadRequest(new HttpResponseApi("Error de parámetros", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(), null, $"No existe el campo clave"));

                }
                var userexist = await this._usuarioRiesgosRepository.findUserById (data.Usuario);
                if (userexist == null)
                {
                    return BadRequest(new HttpResponseApi($"Usuario no existe", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(), null, "Usuario y/o contraseña incorrectos"));
                }

                var matchpassword = this._crypto.verify(data.Clave, userexist.Clave);

                if (!matchpassword)
                {
                    return BadRequest(new HttpResponseApi($"Contraseña Incorrecta", (int)HttpCode.BAD_REQUEST, HttpStatusCode.BAD_REQUEST.ToString(), null, "Usuario y/o contraseña incorrectos"));
                }
                //retrieve json token 
                var token = this._jwt.createToken(userexist);

                return Ok(new HttpResponseApi("Autenticación Existosa", (int)HttpCode.OK, HttpStatusCode.OK.ToString(), new {accessToken = token, user= userexist} ));

            }
            catch (Exception ex)
            {

                return StatusCode(500, new HttpResponseApi("Error interno en el servidor", ((int)HttpCode.INTERNAL_SERVER_ERROR), HttpStatusCode.INTERNAL_SERVER_ERROR.ToString(), null, ex.Message));
            }
}


        [HttpPost("register")]
        public async Task<IActionResult> register([FromBody] Object req)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<UsuarioRiesgos>(req.ToString());
                if (String.IsNullOrEmpty(data.Usuario))
                {
                    return BadRequest(new HttpResponseApi("Error de parámetros", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(), null, $"No existe el campo usuario"));
                }

                    if (String.IsNullOrEmpty(data.Clave))
                    {
                        return BadRequest(new HttpResponseApi("Error de parámetros", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(), null, $"No existe el campo clave"));

                    }
                    var userExist = await this._usuarioRepository.getUserByName(data.Usuario);
                //Verify si user exist on finance core
                if (userExist == null)
                {
                    return BadRequest(new HttpResponseApi("No se puede continuar con el registro", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(),null, $"Usuario {data.Usuario} no existe. Asegurese de estar registrado en el core financiero o contáctese con el adminsitrador"));
                }
                // Verify if user is already registerd in system 
                var userLaRlm = await this._usuarioRiesgosRepository.findUserById(data.Usuario);

                if (userLaRlm != null)
                {
                    return BadRequest(new HttpResponseApi($"No se puede continuar con el registro", ((int)HttpCode.BAD_REQUEST), HttpStatusCode.BAD_REQUEST.ToString(), null, $"Usuario {data.Usuario} ya se encuentra registrado en la aplicación"));
                }

                // Verify if password is valid One Uppercase letter, LowerCase, Special characters and numbers
                //var isValidPassword = this._userService.verifySecurePassword(password);
                //Encript Password 
                var passwordEncripted = this._crypto.encrypt(data.Clave);
                var user = new UsuarioRiesgos(data.Usuario, passwordEncripted);
                // saven in the database
                var res = await this._authRepo.register(user);
                return Ok(new HttpResponseApi("Usuario registrado correctamente. Inicie sesión con sus credenciales", (int)HttpCode.OK, HttpStatusCode.OK.ToString()));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new HttpResponseApi("Error interno en el servidor", ((int)HttpCode.INTERNAL_SERVER_ERROR), HttpStatusCode.INTERNAL_SERVER_ERROR.ToString(), null, ex.Message));
            }
        }


        [HttpPost("getInfoUser")]
        public async Task<IActionResult> getInfoUser()
        {
            try
            {
                string authHeader = HttpContext.Request.Headers["Authorization"];

                    if (String.IsNullOrEmpty(authHeader))
                {
                    return BadRequest(new HttpResponseApi("Operación cancelada. Acceso no autorizado", ((int)HttpCode.UNAUTHORIZED_REQUEST), HttpStatusCode.UNAUTHORIZED_REQUEST.ToString(), null, "Token inválido"));
                }
                authHeader = authHeader.Split(" ")[1];

                var userId = _jwt.verifyToken(authHeader);

                var userValidated = await _usuarioRiesgosRepository.findUserById(userId);

                if (userValidated == null)
                {
                    return BadRequest(new HttpResponseApi("Operación cancelada.  Acceso no autorizado", ((int)HttpCode.UNAUTHORIZED_REQUEST), HttpStatusCode.UNAUTHORIZED_REQUEST.ToString(), null, "Token inválido"));
                }

                return Ok(new HttpResponseApi("Información obtenida exitosamente", (int)HttpCode.OK, HttpStatusCode.OK.ToString(), userValidated));

            }
            catch (Exception ex)
            {
                return StatusCode(500, new HttpResponseApi("Error interno en el servidor", ((int)HttpCode.INTERNAL_SERVER_ERROR), HttpStatusCode.INTERNAL_SERVER_ERROR.ToString(), null, ex.Message));
            }
        }


    }
}
