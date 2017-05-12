using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libros.Models
{
    public class Cuenta
    {
        public string Email { set; get; }
        public string Contrasena { set; get; }

        public bool InicioSesion()
        {
            Logs.IniciaMetodo("Cuenta.InicioSesion", "Email: " + this.Email);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Cuenta_Inicio_Sesion", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Email });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inContrasena", Direction = System.Data.ParameterDirection.Input, Value = this.Contrasena });

                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.InfoResult("Cuenta.IniciarSesion", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.IniciarSesion");
            }
            return false;
        }

        public bool RecuperarContrasena() { throw new System.NotImplementedException(); }
        public bool CerrarSesion() { throw new System.NotImplementedException(); }

        public List<Perfil> Perfiles()
        {
            Logs.IniciaMetodo("Cuenta.Perfiles", this.Email);

            List<Perfil> perfiles = new List<Perfil>();
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Cuenta_Seleccionar_Perfiles", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Email });

                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Perfil p = new Perfil();
                        p.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Id"]));
                        p.Nacionalidad = datos.Tables[0].Rows[i]["Nacionalidad"].ToString();
                        p.Nombre = datos.Tables[0].Rows[i]["Nombre"].ToString();
                        p.Cuenta = this;
                        p.UrlImagen = datos.Tables[0].Rows[i]["UrlImagen"].ToString();
                        Ubicacion u = new Ubicacion();
                        u.lat = (float.Parse(datos.Tables[0].Rows[i]["Lat"].ToString()));
                        u.lon = (float.Parse(datos.Tables[0].Rows[i]["Lon"].ToString()));
                        u.Perfil_Id = p.Id;
                        p.Ubicacion = u;
                        perfiles.Add(p);
                        Logs.InfoResult("Cuenta.Perfiles", "Id " + p.Id);
                    }

                    return perfiles;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.Perfiles");
            }
            return perfiles;
        }

        public bool Agregar(Perfil p)
        {
            Logs.IniciaMetodo("Cuenta.Agregar", string.Empty);
            try
            {
                p.Cuenta = this;
                p.Crear();
                return true;
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.Agregar");
            }
            return false;
        }

        public bool Remover(Perfil p)
        {
            Logs.IniciaMetodo("Cuenta.Remover", "Id: " + p.Id);
            try
            {
                p.Eliminar();
                return true;
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.Remover");
            }
            return false;
        }

        public bool Crear()
        {
            Logs.IniciaMetodo("Cuenta.Crear", "Email: " + this.Email);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Cuenta_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Email });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inContrasena", Direction = System.Data.ParameterDirection.Input, Value = this.Contrasena });
                var datos = DB.QueryCommand(command);

                if (datos == 1)
                {
                    Logs.InfoResult("Cuenta.Crear", "true - Email: " + this.Email);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.Crear");
            }
            return false;
        }

        public bool Seleccionar(string Email)
        {
            Logs.IniciaMetodo("Cuenta.Seleccionar", "Email: " + this.Email);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Cuenta_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = Email });

                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.InfoResult("Cuenta.Seleccionar", "true - Email: " + this.Email);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.Seleccionar");
            }
            return false;
        }

        public bool CambiarContrasena(string nuevaPass)
        {
            Logs.IniciaMetodo("Cuenta.CambiarContrasena", "?????");
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Cuenta_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Email });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inContrasena", Direction = System.Data.ParameterDirection.Input, Value = nuevaPass });
                var datos = DB.QueryCommand(command);
                if (datos == 1)
                {
                    Logs.InfoResult("Cuenta.CambiarContrasena", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.CambiarContrasena");
            }
            return false;
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Cuenta.Eliminar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Cuenta_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Email });
                var temp = DB.QueryCommand(command);

                if (temp == 1)
                {
                    Logs.InfoResult("Cuenta.Eliminar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Cuenta.Eliminar");
            }
            return false;
        }

        private void SetDesde(DataRow dr)
        {
            this.Email = dr["Email"].ToString();
            this.Contrasena = dr["Contrasena"].ToString();
        }

        public string toString()
        {
            return string.Format("Email {0}", this.Email);
        }
    }
}
