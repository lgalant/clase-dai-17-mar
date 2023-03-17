using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;

namespace Pizzas.API.Utils;

public static class BD {
private static string CONNECTION_STRING = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";
private static string CONNECTION_STRINGmaster = @"Persist Security Info=False;User ID=sa;password=Admin123;Initial Catalog=master;Data Source=.;";
  
  public static List<Pizza> GetAll() {
    string          sqlQuery;
    List<Pizza>     returnList;
    
    returnList = new List<Pizza>();
    using (SqlConnection db = new SqlConnection(CONNECTION_STRING)) {
      sqlQuery    = "SELECT Id, Nombre, LibreGluten, Importe, Descripcion FROM Pizzas";
      returnList  = db.Query<Pizza>(sqlQuery).ToList();
    }
    return returnList;
  }


    public static Pizza GetById(int id) {
        //
        // Obtiene un registro de la base de datos segun el Id
        //
        string  sqlQuery;
        Pizza   returnEntity = null;
        
        sqlQuery  = "SELECT Id, Nombre, LibreGluten, Importe, Descripcion ";
        sqlQuery += "FROM Pizzas ";
        sqlQuery += "WHERE Id = @idPizza";
        using (SqlConnection db = new SqlConnection(CONNECTION_STRING)) {
            returnEntity  = db.QueryFirstOrDefault<Pizza>(sqlQuery, new { idPizza = id });
        }
        return returnEntity;
    }


        public static int Insert(Pizza pizza) {
            // 
            // Inserta un registro y retorna los RowsAffected.
            //
            string  sqlQuery;
            int     intRowsAffected = 0;
                
            sqlQuery  = "INSERT INTO Pizzas (";
            sqlQuery += "   Nombre  , LibreGluten   , Importe   , Descripcion";
            sqlQuery += ") VALUES (";
            sqlQuery += "   @nombre , @libreGluten  , @importe  , @descripcion";
            sqlQuery += ")";
            using (SqlConnection db = new SqlConnection(CONNECTION_STRING)) {
                intRowsAffected = db.Execute(sqlQuery, new {  
                                                            nombre      = pizza.Nombre, 
                                                            libreGluten = pizza.LibreGluten, 
                                                            importe     = pizza.Importe,
                                                            descripcion = pizza.Descripcion 
                                                            }
                                            );
            }
            return intRowsAffected;
        }
}

