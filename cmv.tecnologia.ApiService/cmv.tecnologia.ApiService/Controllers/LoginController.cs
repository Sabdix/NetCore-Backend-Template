using cmv.tecnologia.Entidades;
using cmv.tecnologia.Entidades.Herramientas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cmv.tecnologia.ApiService.Controllers {
  [Route("login")]
  [ApiController]
  public class LoginController : ControllerBase {
    private readonly GeneradorTokens generador;
    private readonly JwtTokenConfig jwtTokenConfig;

    public LoginController(GeneradorTokens generador, JwtTokenConfig jwtTokenConfig) {
      this.generador = generador;
      this.jwtTokenConfig = jwtTokenConfig;
    }

    /// <summary>
    /// Endpoint para verificar la salud del Servicio
    /// </summary>
    /// <returns></returns>
    [HttpGet("ping")]
    public WsRespuesta<dynamic> Ping() {
      return ConstructorRespuestas<dynamic>.CrearRespuestaExitosa(true);
    }

    /// <summary>
    /// Endpoint para autenticarse en el servicio
    /// </summary>
    /// <param name="Usuario"></param>
    /// <param name="Contrasena"></param>
    /// <returns></returns>
    [HttpGet("autenticar")]
    public WsRespuesta<dynamic> Auth([FromQuery] string Usuario, [FromQuery] string Contrasena) {
      try {
        var claims = new[] {
          new Claim(ClaimTypes.Name, Usuario),
          new Claim(ClaimTypes.Rsa, Encryptor.EncryptString(jwtTokenConfig.Secret, Contrasena)),
        };
        return ConstructorRespuestas<dynamic>.CrearRespuestaExitosa(generador.GeneraToken(Usuario, claims, DateTime.Now));
      } catch (Exception e) {
        return ConstructorRespuestas<dynamic>.CrearRespuestaErronea(e);
      }
    }
  }
}
