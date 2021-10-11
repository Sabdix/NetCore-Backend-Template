using cmv.tecnologia.Entidades.Catalogos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmv.tecnologia.DAL {
  public static class Conexion {
    private static string Server = string.Empty;
    private static string Bd = string.Empty;
    private static string Usuario = string.Empty;
    private static string Contrasena = string.Empty;

    public static string ObtenerConexion() {
      try {
        if (Environment.GetEnvironmentVariable("Environment") == Variablesentorno.PRODUCCION) {
          RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM");
          if (key != null) {
            Bd = key.GetValue("1").ToString();
            Usuario = key.GetValue("2").ToString();
            Contrasena = key.GetValue("3").ToString();
            Server = key.GetValue("4").ToString();
            key.Close();
          } else {
            return string.Empty;
          }
          return @"Server=" + Server + ";Database=" + Bd + ";User Id=" + Usuario + ";Password=" + Contrasena;
        } else return Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("Environment") + "BD");

      } catch (Exception ex) {
        throw ex;
      }
    }

    public static string ObtenerConexion(string Usuario, string Contrasena) {
      try {
        if (Environment.GetEnvironmentVariable("Environment") == Variablesentorno.PRODUCCION) {
          RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM");
          if (key != null) {
            Bd = key.GetValue("1").ToString();
            Server = key.GetValue("4").ToString();
            key.Close();
          } else {
            return string.Empty;
          }
          return @"Server=" + Server + ";Database=" + Bd + ";User Id=" + Usuario + ";Password=" + Contrasena;
        } else return Environment.GetEnvironmentVariable(Environment.GetEnvironmentVariable("Environment") + "BD") + ";User Id=" + Usuario + ";Password=" + Contrasena;
      } catch (Exception ex) {
        throw ex;
      }
    }
  }
}
