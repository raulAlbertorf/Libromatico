using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libros.Models
{
    public class Autor
    {
        public Int64 Id { set; get; }
        public string Nombre { set; get; }

        public List<Autor> Buscar_Autores(string Nombre)
        {
            Logs.IniciaMetodo("Autor.Buscar_Autores", "Nombre: " + Nombre);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Buscar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombre", Direction = System.Data.ParameterDirection.Input, Value = Nombre });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    List<Autor> autores = new List<Autor>();
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Autor a = new Autor();
                        a.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["AutorId"]));
                        a.Nombre = datos.Tables[0].Rows[i]["Nombre"].ToString();
                        autores.Add(a);
                        Logs.InfoResult("Autor.Buscar_Autores", a.toString());
                    }
                    return autores;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Autor.Buscar_Autores");
            }
            return null;
        }

        public bool Crear()
        {
            Logs.InfoResult("Autor.Crear", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombre", Direction = System.Data.ParameterDirection.Input, Value = this.Nombre });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "outId", Direction = System.Data.ParameterDirection.Output });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    this.Id = Convert.ToInt64(command.Parameters["outId"].Value);
                    Logs.InfoResult("Autor.Crear", "Id: " + this.Id);
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Autor.Crear");
            }
            return false;
        }

        public bool Seleccionar(Int64 Id)
        {
            Logs.IniciaMetodo("Autor.Seleccionar", "Id: " + Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.InfoResult("Autor.Seleccionar", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Autor.Seleccionar");
            }
            return false;
        }

        public bool Modificar()
        {
            Logs.IniciaMetodo("Autor.Modificar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombre", Direction = System.Data.ParameterDirection.Input, Value = this.Nombre });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Autor.Modificar", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Autor.Modificar");
            }
            return false;
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Autor.Eliminar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Autor.Eliminar", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Autor.Eliminar");
            }
            return false;
        }

        private void SetDesde(DataRow dr)
        {
            this.Id = Convert.ToInt64(dr["Id"]);
            this.Nombre = dr["Nombre"].ToString();
        }

        public string toString()
        {
            return String.Format("Id: {0}", Id);
        }
    }
}
