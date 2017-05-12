using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Libros.Models
{
    public class Comentario
    {
        public Int64 Id { set; get; }
        public string Texto { set; get; }
        public DateTime FechaCreacion { set; get; }
        public Prestamo Prestamo { set; get; } 
        public Perfil Perfil { set; get; }

        public bool Crear()
        {
            Logs.IniciaMetodo("Comentario.Crear", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Comentario_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inTexto", Direction = System.Data.ParameterDirection.Input, Value = this.Texto });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaCreacion", Direction = System.Data.ParameterDirection.Input, Value = this.FechaCreacion });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPrestamo_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Prestamo.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Perfil.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "outId", Direction = System.Data.ParameterDirection.Output });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    this.Id = Convert.ToInt64(command.Parameters["outId"].Value);
                    Logs.InfoResult("Comentario.Crear", true.ToString() + " Id: "+this.Id);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Comentario.Crear");
            }
            return false;
        }

        public bool Seleccionar(Int64 Id)
        {
            Logs.IniciaMetodo("Comentario.Seleccionar", "Id: " + Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Comentario_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.Info("Comentario.Seleccionar", this.toString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Comentario.Seleccionar");
            }
            return false;
        }

        public bool Modificar()
        {
            Logs.IniciaMetodo("Comentario.Modificar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Comentario_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inTexto", Direction = System.Data.ParameterDirection.Input, Value = this.Texto });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaCreacion", Direction = System.Data.ParameterDirection.Input, Value = this.FechaCreacion });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPrestamo_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Prestamo.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Perfil.Id });
                var temp = DB.QueryCommand(command);

                if (temp == 1)
                {
                    Logs.InfoResult("Comentario.Modificar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Comentario.Modificar");
            }
            return false;
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Comentario.Eliminar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Comentario_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Comentario.Eliminar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Comentario.Eliminar");
            }
            return false;
        }
        
        private void SetDesde(DataRow dr)
        {
            this.Id = Convert.ToInt64(dr["Id"]);
            this.Texto = dr["Texto"].ToString();
            this.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]);
            this.Prestamo = new Prestamo();
            this.Prestamo.Seleccionar(Convert.ToInt64(dr["Prestamo_Id"]));
            this.Perfil = new Perfil();
            this.Perfil.Seleccionar(Convert.ToInt64(dr["Perfil_Id"]));
        }

        public string toString()
        {
            return String.Format("Id:{0} - Prestamo_Id: {1} - Perfil_Id: {2}", this.Id, this.Prestamo.Id, this.Perfil.Id);
        }
    }
}
