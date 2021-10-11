using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmv.tecnologia.DAL.Herramientas {
  public static class ConstructorDapper {

    public static dynamic ConsultaDapperEstatus(string SP, DynamicParameters parametros, string Usuario, string Contrasena) {
      using (var conexion = new SqlConnection(Conexion.ObtenerConexion(Usuario, Contrasena))) {
        return conexion.QuerySingleOrDefault(SP, parametros, commandType: CommandType.StoredProcedure);
      }
    }

    public static dynamic ConsultaListaDapper(string SP, DynamicParameters param, out dynamic Estatus, string Usuario, string Contrasena) {
      using (IDbConnection conexion = new SqlConnection(Conexion.ObtenerConexion(Usuario, Contrasena))) {
        var result =
          conexion.QueryMultiple(SP,
          param: param,
          commandType: CommandType.StoredProcedure);
        Estatus = result.Read().First();
        if (Estatus.codigo == 200 && !result.IsConsumed)
          return result.Read();
        return null;
      }
    }

    public static List<dynamic> ConsultaListaDinamicaDapper(string SP, DynamicParameters param, out dynamic Estatus, string Usuario, string Contrasena) {
      using (IDbConnection conexion = new SqlConnection(Conexion.ObtenerConexion(Usuario, Contrasena))) {
        List<dynamic> resultsets = new List<dynamic>();
        var result =
          conexion.QueryMultiple(SP,
          param: param,
          commandType: CommandType.StoredProcedure);
        Estatus = result.Read().First();
        if (Estatus.codigo == 200)
          while (!result.IsConsumed)
            resultsets.Add(result.Read());
        return resultsets;
      }
    }

    public static dynamic ConsultaDapperObject(string SP, DynamicParameters param, out dynamic Estatus, string Usuario, string Contrasena) {
      using (var conexion = new SqlConnection(Conexion.ObtenerConexion(Usuario, Contrasena))) {
        var result = conexion.QueryMultiple(SP, param, commandType: CommandType.StoredProcedure);
        Estatus = result.Read().First();
        if (Estatus.codigo == 200 && !result.IsConsumed)
          return result.Read().First();
        return null;
      }
    }
  }
}
