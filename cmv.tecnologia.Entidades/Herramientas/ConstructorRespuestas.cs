using cmv.tecnologia.Entidades.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmv.tecnologia.Entidades.Herramientas {
  public class ConstructorRespuestas<T> {
    public static WsRespuesta<T> CrearRespuestaExitosa(T Entidad, string Mensaje = null) {
      return new WsRespuesta<T>() {
        Codigo = CatalogoRespuestas.SOLICITUD_CORRECTA.Codigo,
        Entidad = Entidad,
        Estado = true,
        Mensaje = Mensaje ?? CatalogoRespuestas.SOLICITUD_CORRECTA.Mensaje
      };
    }

    public static WsRespuesta<T> CrearRespuestaErronea(T e) {
      return new WsRespuesta<T>() {
        Codigo = CatalogoRespuestas.ERROR_CORE.Codigo,
        Entidad = e,
        Estado = false,
        Mensaje = CatalogoRespuestas.ERROR_CORE.Mensaje
      };
    }
  }
}
