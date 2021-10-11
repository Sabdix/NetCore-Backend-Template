using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmv.tecnologia.Entidades.Catalogos {
  public class CatalogoRespuestas : TipoRespuesta {
    public static readonly CatalogoRespuestas SOLICITUD_CORRECTA = new CatalogoRespuestas("200", "Operación correcta");
    public static readonly CatalogoRespuestas CREACION_CORRECTA = new CatalogoRespuestas("201", "Creacion Correcta");
    public static readonly CatalogoRespuestas ERROR_INTERNO = new CatalogoRespuestas("500", "Error interno del servidor");
    public static readonly CatalogoRespuestas EXCEPCION_BASEDATOS = new CatalogoRespuestas("1500", "Error en SP de la BD");
    public static readonly CatalogoRespuestas ERROR_CORE = new CatalogoRespuestas("501", "Error del Core");
    public static readonly CatalogoRespuestas NO_AUTORIZADO = new CatalogoRespuestas("401", "Usuario no autorizado");
    public static readonly CatalogoRespuestas NO_AUTORIZADO_CORE = new CatalogoRespuestas("402", "Usuario no autorizado core");
    public static readonly CatalogoRespuestas SOLICITUD_INCORRECTA = new CatalogoRespuestas("400", "Solicitud incorrecta");
    public static readonly CatalogoRespuestas NO_ENCONTRADO = new CatalogoRespuestas("404", "No se encontró recurso");
    public static readonly CatalogoRespuestas ERROR_VALIDACION = new CatalogoRespuestas("422", "Error en validaciones");
    public CatalogoRespuestas(string codigo, string mensaje) : base(codigo, mensaje) { }
  }

  public class TipoRespuesta {
    public string Mensaje { get; set; }
    public string Codigo { get; set; }
    public TipoRespuesta(string codigo, string mensaje) {
      Mensaje = mensaje;
      Codigo = codigo;
    }
  }
}
