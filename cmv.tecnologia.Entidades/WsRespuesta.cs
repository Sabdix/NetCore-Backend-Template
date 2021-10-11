using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace cmv.tecnologia.Entidades {
  public class WsRespuesta<T> {
    [JsonPropertyName("Entidad")]
    public T Entidad { get; set; }
    [JsonPropertyName("Codigo")]
    public string Codigo { get; set; }
    [JsonPropertyName("Mensaje")]
    public string Mensaje { get; set; }
    [JsonPropertyName("Estado")]
    public bool Estado { get; set; }
  }
}
