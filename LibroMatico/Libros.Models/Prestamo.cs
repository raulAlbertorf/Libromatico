using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Libros.Models
{
    public class Prestamo
    {
        public Int64 Id { set; get; }
        public Item Item { set; get; }
        public DateTime FechaEnvio { set; get; }
        public DateTime FechaRecepcion { set; get; }
        public Perfil Prestamista { set; get; }
        public Perfil Receptor { set; get; }
        public EstadoPrestamo Estado { set; get; }
        public DateTime FechaExpiracion { set; get; }
        public DateTime FechaUltimaModificacion { set; get; }
        public int CalificacionAlReceptor { set; get; }
        public int CalificacionAlPrestamista { set; get; }

        public bool Agregar(Comentario inComentario)
        {
            Logs.IniciaMetodo("Prestamo.Agregar (Comentario)", string.Empty);
            try
            {
                inComentario.Prestamo = this;
                return inComentario.Crear();
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Agregar (Comentario)");
            }
            return false;
        }


        public List<Comentario> Comentarios()
        {
            Logs.IniciaMetodo("Prestamo.Comentarios", string.Empty);
            List<Comentario> comentarios = new List<Comentario>();

            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Prestamo_Seleccionar_Comentarios", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPrestamo_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Comentario c = new Comentario();
                        Perfil p = new Perfil();

                        p.Id = Convert.ToInt64(datos.Tables[0].Rows[i]["Perfil_Id"]);
                        p.Nombre = datos.Tables[0].Rows[i]["Perfil_Nombre"].ToString();
                        p.UrlImagen = datos.Tables[ 0 ].Rows[ i ][ "Perfil_URL" ].ToString( );
                        c.Id = Convert.ToInt64(datos.Tables[0].Rows[i]["Comentario_Id"]);
                        c.Texto = datos.Tables[0].Rows[i]["Comentario_Texto"].ToString();
                        c.FechaCreacion = Convert.ToDateTime(datos.Tables[0].Rows[i]["Comentario_Fecha"]);
                        c.Prestamo = this;
                        c.Perfil = p;

                        Logs.Info("Prestamo.Comentarios", c.toString());

                        comentarios.Add(c);
                    }
                    return comentarios;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Comentarios");
            }
            return comentarios;
        }


        public bool Eliminar(Comentario Comentario_Eliminar)
        {
            Logs.IniciaMetodo("Prestamo.Eliminar (Comentario)", string.Empty);
            try
            {
                return Comentario_Eliminar.Eliminar();
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Eliminar (Comentario)");
            }
            return false;
        }


        public bool Crear()
        {
            Logs.IniciaMetodo("Prestamo.Crear", this.toString());
            bool success = false;
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Prestamo_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEstado", Direction = System.Data.ParameterDirection.Input, Value = this.Estado });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaEnvio", Direction = System.Data.ParameterDirection.Input, Value = this.FechaEnvio });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaRecepcion", Direction = System.Data.ParameterDirection.Input, Value = this.FechaRecepcion });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaExpiracion", Direction = System.Data.ParameterDirection.Input, Value = this.FechaExpiracion });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inCalificacionalReceptor", Direction = System.Data.ParameterDirection.Input, Value = this.CalificacionAlReceptor });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inCalificacionalPrestamista", Direction = System.Data.ParameterDirection.Input, Value = this.CalificacionAlPrestamista });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Item.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPrestamista_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Prestamista.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inReceptor_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Receptor.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "outId", Direction = System.Data.ParameterDirection.Output });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    this.Id = Convert.ToInt64(command.Parameters["outId"].Value);
                    Logs.Info("Prestamo.Crear", true.ToString());
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Crear");
            }
            return success;
        }

        public bool Seleccionar(Int64 Id)
        {
            Logs.IniciaMetodo("Prestamo.Seleccionar", "Id: " + Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Prestamo_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.Info("Prestamo.Seleccionar", this.toString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Seleccionar");
            }
            return false;
        }

        public bool Modificar()
        {
            Logs.IniciaMetodo("Prestamo.Modificar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Prestamo_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEstado", Direction = System.Data.ParameterDirection.Input, Value = this.Estado });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaEnvio", Direction = System.Data.ParameterDirection.Input, Value = this.FechaEnvio });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaRecepcion", Direction = System.Data.ParameterDirection.Input, Value = this.FechaRecepcion });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inFechaExpiracion", Direction = System.Data.ParameterDirection.Input, Value = this.FechaExpiracion });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inCalificacionalReceptor", Direction = System.Data.ParameterDirection.Input, Value = this.CalificacionAlReceptor });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inCalificacionalPrestamista", Direction = System.Data.ParameterDirection.Input, Value = this.CalificacionAlPrestamista });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Item.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPrestamista_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Prestamista.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inReceptor_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Receptor.Id });
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "outUltimaModificacion" , Direction = System.Data.ParameterDirection.Output} );
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    this.FechaUltimaModificacion = Convert.ToDateTime( command.Parameters[ "outUltimaModificacion" ].Value );
                    Logs.Info("Prestamo.Modificar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Modificar");
            }
            return false;
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Prestamo.Eliminar", this.toString());
            bool success = false;
            try 
            {
                var command = new MySqlCommand() { CommandText = "sp_Prestamo_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.Info("Prestamo.Eliminar", true.ToString());
                    success = true;
                }
            } 
            catch(Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Prestamo.Eliminar");
            }
            return success;
        }

        private void SetDesde(DataRow dr)
        {
            this.Id = Convert.ToInt64(dr["Id"]);
            this.Estado = (EstadoPrestamo)Convert.ToInt16(dr["Estado"]);
            this.FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]);
            this.FechaRecepcion = Convert.ToDateTime(dr["FechaRecepcion"]);
            this.FechaExpiracion = Convert.ToDateTime(dr["FechaExpiracion"]);
            this.CalificacionAlReceptor = Convert.ToInt16(dr["CalificacionAlReceptor"]);
            this.CalificacionAlPrestamista = Convert.ToInt16(dr["CalificacionAlPrestamista"]);
            this.FechaUltimaModificacion = Convert.ToDateTime( dr[ "UltimaModificacion" ] );

            this.Item = new Item();
            this.Item.Seleccionar(Convert.ToInt64(dr["Item_Id"]));

            this.Receptor = new Perfil();
            this.Receptor.Seleccionar(Convert.ToInt64(dr["Receptor_Id"]));

            this.Prestamista = new Perfil();
            this.Prestamista.Seleccionar(Convert.ToInt64(dr["Prestamista_Id"]));
        }

        public string toString()
        {
            return String.Format("Id: {0} - Estado: {1} - Item_Id: {2} - Receptor_Id: {3} - Prestamista_Id: {4}", this.Id, this.Estado, this.Item.Id, this.Receptor.Id, this.Prestamista.Id);
        }
    }
}