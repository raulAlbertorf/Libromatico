using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Libros.Models
{
    public class Ubicacion
    {
       public float lat { set; get; }
        public float lon { set; get; }
        public Int64 Perfil_Id { set; get; } //Es su id a la vez

        public bool Crear()
        {
            Logs.IniciaMetodo("Ubicacion.Crear", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Ubicacion_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLatitude", Direction = System.Data.ParameterDirection.Input, Value = this.lat }); 
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLongitude", Direction = System.Data.ParameterDirection.Input, Value = this.lon });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Perfil_Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Ubicacion.Crear", true.ToString());
                    return true;
                }
            }
            catch(Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Ubicacion.Crear");
            }
            return false;
        }

        public bool Seleccionar(Int64 Id)
        {
            Logs.IniciaMetodo("Ubicacion.Seleccionar", "Id: " + Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Ubicacion_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.Info("Ubicacion.Seleccionar", this.toString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Ubicacion.Seleccionar");
            }
            return false;
        }

        public bool Modificar()
        {
            Logs.Info("Ubicacion.Modificar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Ubicacion_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLatitude", Direction = System.Data.ParameterDirection.Input, Value = this.lat });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLongitude", Direction = System.Data.ParameterDirection.Input, Value = this.lon });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Perfil_Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.Info("Ubicacion.Modificar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Ubicacion.Modificar");
            }
            return false;
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Ubicacion.Eliminar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Ubicacion_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Perfil_Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.Info("Ubicacion.Eliminar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Ubicacion.Eliminar");
            }
            return false;
        }

        private void SetDesde(DataRow dataRow)
        {
            var latitud =  Convert.ToDouble( dataRow[ "Latitude" ] ).ToString( System.Globalization.CultureInfo.InvariantCulture );
            var longitud = Convert.ToDouble( dataRow[ "Longitude" ] ).ToString( System.Globalization.CultureInfo.InvariantCulture );
            this.lat = float.Parse( latitud , System.Globalization.CultureInfo.InvariantCulture );
            this.lon = float.Parse( longitud , System.Globalization.CultureInfo.InvariantCulture );
            this.Perfil_Id = Convert.ToInt64(dataRow["Perfil_Id"]);
        }

        public string toString()
        {
            return string.Format("Perfil_Id: {0} - lat: {1} - lon: {2}", this.Perfil_Id, this.lat, this.lon);
        }
    }
}