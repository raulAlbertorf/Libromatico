using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Libros.Models
{
    public class Item
    {
        public Int64 Id { set; get; }
        public string Titulo { set; get; }
        public Perfil Propietario { set; get; }
        public string Resumen { set; get; }
        public Int64 VecesVisto { set; get; }
        public string UrlImagen { set; get; }

        public Int64 Prestamo_Aceptado_Id_Item()
        {
            Logs.IniciaMetodo("Item.Prestamo_Id_Item", "Id: " + Propietario.Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Aceptado_Prestamo", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = Propietario.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    Int64 id = Convert.ToInt64(datos.Tables[0].Rows[datos.Tables[0].Rows.Count - 1]["Prestamo_Id"]);
                    Logs.InfoResult("Item.Prestamo_Id_Item", "Id: " + id);
                    return (id);
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Prestamo_Id_Item");
            }
            return 0;
        }

        public Int64 Prestamo_Id_Item(Int64 Solicitante_Id)
        {
            Logs.IniciaMetodo("Item.Prestamo_Id_Item", "Id: "+Solicitante_Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Solicitado_Prestamo", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = Solicitante_Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    Int64 id = Convert.ToInt64(datos.Tables[0].Rows[datos.Tables[0].Rows.Count -1]["Prestamo_Id"]);
                    Logs.InfoResult("Item.Prestamo_Id_Item", "Id: " + id);
                    return (id);
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Prestamo_Id_Item");
            }
            return 0;
        }

        public bool estaSolicitado(Int64 Solicitante_Id)
        {
            Logs.IniciaMetodo("Item.estaSolicitado", "Solicitante_Id: "+Solicitante_Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Solicitado_Prestamo", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = Solicitante_Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    Logs.InfoResult("Item.estaSolicitado", true.ToString());
                    return true;
                }
                else
                {
                    Logs.InfoResult("Item.estaSolicitado", false.ToString());
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.estaSolicitado");
            }
            return false;
        }

        public bool CambiarImagen()
        {
            Logs.IniciaMetodo("Item.CambiarImagen", string.Format("Id: {0} - UrlImagen: {1}", Id, UrlImagen));
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Modificar_Imagen", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUrlImagen", Direction = System.Data.ParameterDirection.Input, Value = this.UrlImagen });
                var temp = DB.QueryCommand(command);

                if (temp == 1)
                {
                    Logs.InfoResult("Item.CambiarImagen", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.CambiarImagen");
            }
            return false;
        }

        public bool Visitado()
        {
            Logs.IniciaMetodo("Item.Visitado", String.Empty);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Visitado", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.QueryCommand(command);
                if (datos == 1)
                {
                    Logs.InfoResult("Item.Visitado", "+1");
                    return true;
                }
            }
            catch (Exception Ex)
            {
                Logs.Error(Ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Visitado");
            }
            return false;
        }

        public bool Crear(Autor autor)
        {
            Logs.IniciaMetodo("Item.Crear", autor.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Item_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inAutor_Id", Direction = System.Data.ParameterDirection.Input, Value = autor.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "InItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.QueryCommand(command);
                if (datos == 1)
                {
                    Logs.InfoResult("Item.Crear", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Crear");
            }
            return false;
        }

        public bool Eliminar(Autor autor)
        {
            Logs.IniciaMetodo("Item.Eliminar (Autor)", autor.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Item_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inAutor_Id", Direction = System.Data.ParameterDirection.Input, Value = autor.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Item.Eliminar (Autor)", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Eliminar (Autor)");
            }
            return false;
        }

        public List<Autor> Autores()
        {
            Logs.IniciaMetodo("Item.Autores", string.Empty);
            List<Autor> autores = new List<Autor>();

            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Autor_Item_Seleccionar_Item", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Autor a = new Autor();
                        a.Id = Convert.ToInt64(datos.Tables[0].Rows[i]["Autor_Id"]);
                        a.Nombre = datos.Tables[0].Rows[i]["Autor_Nombre"].ToString();
                        autores.Add(a);
                        Logs.InfoResult("Item.Autores", a.toString());
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
                Logs.SalirMetodo("Item.Autores");
            }
            return autores;
        }

        public bool Crear()
        {
            Logs.IniciaMetodo("Item.Crear", "Titulo: " + this.Titulo);
            bool completado = false;
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inTitulo", Direction = System.Data.ParameterDirection.Input, Value = this.Titulo });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inResumen", Direction = System.Data.ParameterDirection.Input, Value = this.Resumen });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inVeces_Visto", Direction = System.Data.ParameterDirection.Input, Value = this.VecesVisto });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_ID", Direction = System.Data.ParameterDirection.Input, Value = this.Propietario.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUrlImagen", Direction = System.Data.ParameterDirection.Input, Value = this.UrlImagen });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "outId", Direction = System.Data.ParameterDirection.Output });
                var temp = DB.QueryCommand(command);

                if (temp == 1)
                {
                    this.Id = Convert.ToInt64(command.Parameters["outId"].Value);
                    Logs.InfoResult("Item.Crear", this.toString());
                    completado = true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Crear");
            }
            return completado;
        }

        public bool Seleccionar(Int64 Id)
        {
            Logs.IniciaMetodo("Item.Seleccionar", "Id: " + Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.InfoResult("Item.Seleccionar", this.toString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Seleccionar");
            }
            return false;
        }

        public bool Modificar()
        {
            Logs.IniciaMetodo("Item.Modificar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inTitulo", Direction = System.Data.ParameterDirection.Input, Value = this.Titulo });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inResumen", Direction = System.Data.ParameterDirection.Input, Value = this.Resumen });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inVecesVisto", Direction = System.Data.ParameterDirection.Input, Value = this.VecesVisto });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Propietario.Id });
                var temp = DB.QueryCommand(command);

                if (temp == 1)
                {
                    Logs.InfoResult("Item.Modificar", string.Format("{0} - Item: {1}", true.ToString(), this.toString()));
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Modificar");
            }
            return false;
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Item.Eliminar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Item.Eliminar", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Eliminar");
            }
            return false;
        }

        public Dictionary<string, string> Propiedades()
        {
            Logs.IniciaMetodo("Item.Propiedades", string.Empty);
            Dictionary<string, string> Prop = new Dictionary<string, string>();
            string key = "";
            string value = "";
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Propiedades_seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);
                if (datos.Tables[0].Rows.Count > 0)
                {
                    int cont = 0;
                    while (cont < datos.Tables[0].Rows.Count)
                    {
                        key = datos.Tables[0].Rows[cont]["NombrePropiedad"].ToString();
                        value = datos.Tables[0].Rows[cont]["ValorPropiedad"].ToString();
                        Prop.Add(key, value);
                        cont = cont + 1;
                        Logs.InfoResult("Item.Propiedades", string.Format("Key: {0} - Value: {1}", key, value));
                    }
                    return Prop;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Propiedades");
            }
            return Prop;
        }

        public bool Agregar(string Key, string Value)
        {
            Logs.IniciaMetodo("Item.Agregar", String.Format("Key: {0} - Value: {1}", Key, Value));
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Propiedades_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombrePropiedad", Direction = System.Data.ParameterDirection.Input, Value = Key });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inValorPropiedad", Direction = System.Data.ParameterDirection.Input, Value = Value });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Item.Agregar", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Agregar");
            }
            return false;
        }

        public bool Modificar(string Key, string Value)
        {
            Logs.IniciaMetodo("Item.Modificar", String.Format("Key: {0} - Value: {1}", Key, Value));
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Propiedades_modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombrePropiedad", Direction = System.Data.ParameterDirection.Input, Value = Key });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inValorPropiedad", Direction = System.Data.ParameterDirection.Input, Value = Value });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Item.Modificar", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Modificar");
            }
            return false;
        }

        public bool Remove(string Key)
        {
            Logs.IniciaMetodo("Item.Remove", "key: " + Key);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Propiedades_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombrePropiedad", Direction = System.Data.ParameterDirection.Input, Value = Key });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    Logs.InfoResult("Item.Remove", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Item.Remove");
            }
            return false;
        }

        private void SetDesde(DataRow dr)
        {
            this.Id = Convert.ToInt64(dr["Id"]);
            this.Titulo = dr["Titulo"].ToString();
            this.Propietario = new Perfil();
            this.Propietario.Seleccionar(Convert.ToInt64(dr["Perfil_ID"]));
            this.Resumen = dr["Resumen"].ToString();
            this.VecesVisto = Convert.ToInt64(dr["vecesVisto"]);
            this.UrlImagen = dr["UrlImagen"].ToString();
        }

        public string toString()
        {
            return string.Format("Id: {0} - Propietario: {1} - Titulo: {2}", this.Id, this.Propietario.Id, Titulo);
        }

    }
}