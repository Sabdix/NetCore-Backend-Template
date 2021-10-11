using cmv.tecnologia.Entidades;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cmv.tecnologia.ApiService.Interceptores {
  public class LogInterceptor : IActionFilter {

    public LogInterceptor(string UrlLog) {
      Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.File(UrlLog, rollingInterval: RollingInterval.Day)
              .CreateLogger();
    }

    public void OnActionExecuted(ActionExecutedContext context) {
      var result = context.Result;
      var syncIOFeature = context.HttpContext.Features.Get<IHttpBodyControlFeature>();
      if (syncIOFeature != null) {
        syncIOFeature.AllowSynchronousIO = true;
      }
      StreamReader stream = new StreamReader(context.HttpContext.Request.Body);
      string body = stream.ReadToEndAsync().GetAwaiter().GetResult();
      if (result is ObjectResult json) {
        var x = json.Value;
        Log.Information(JsonConvert.SerializeObject(new LogMetadatos() {
          RequestMethod = context.HttpContext.Request.Method,
          RequestTimestamp = DateTime.Now,
          RequestUri = context.HttpContext.Request.Path,
          RequestBody = body,
          ResponseStatusCode = json.StatusCode,
          ResponseBody = x,
        }, Formatting.Indented));
      }
    }

    public void OnActionExecuting(ActionExecutingContext context) {
    }
  }
}
