using System;

namespace cmv.tecnologia.Entidades {
  public class LogMetadatos {
    public string RequestUri { get; set; }
    public string RequestMethod { get; set; }
    public DateTime? RequestTimestamp { get; set; }
    public object RequestBody { get; set; }
    public int? ResponseStatusCode { get; set; }
    public object ResponseBody { get; set; }
  }
}
