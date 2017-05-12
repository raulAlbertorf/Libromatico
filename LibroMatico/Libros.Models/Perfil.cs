using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Libros.Models
{
    public class Perfil
    {
        public Int64 Id { set; get; }
        public string Nombre { set; get; }
        public Ubicacion Ubicacion { set; get; }
        public string UrlImagen { set; get; }
        public string Nacionalidad { set; get; }
        public Cuenta Cuenta { set; get; }

        public List<Prestamo> SolicitudesPrestamoEntrantes()
        {
            Logs.IniciaMetodo("Perfil.SolicitudesPrestamoEntrantes", string.Empty);
            List<Prestamo> solicitudesPrestamo = new List<Prestamo>();
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Solicitudes_Prestamo", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Prestamo prestamo = new Prestamo();
                        Item item = new Item();
                        Perfil prestamista = new Perfil();
                        Perfil receptor = new Perfil();

                        prestamo.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Prestamo_Id"]));

                        item.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_Id"]));
                        item.Titulo = (datos.Tables[0].Rows[i]["Item_Titulo"].ToString());
                        item.UrlImagen = (datos.Tables[0].Rows[i]["Item_UrlImagen"].ToString());

                        receptor.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Perfil_Id"]));
                        receptor.Nombre = (datos.Tables[0].Rows[i]["Perfil_Nombre"].ToString());
                        receptor.UrlImagen = (datos.Tables[0].Rows[i]["Perfil_UrlImagen"].ToString());

                        prestamo.Item = item;
                        prestamo.Prestamista = this;
                        prestamo.Receptor = receptor;
                        solicitudesPrestamo.Add(prestamo);
                        Logs.InfoResult("Perfil.SolicitudesPrestamoEntrantes", prestamo.toString());

                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.SolicitudesPrestamoEntrantes");
            }
            return solicitudesPrestamo;
        }

        public bool SeleccionarPrestamo(Int64 Receptor_Id, Int64 Item_Id)
        {
            Logs.IniciaMetodo("Perfil.SeleccionarPrestamo", string.Format("Receptor_Id: {0} - Item_Id: {1}", Receptor_Id, Item_Id ));
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Seleccionar_Prestamo", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inReceptor_Id", Direction = System.Data.ParameterDirection.Input, Value = Receptor_Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = Item_Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    Logs.InfoResult("Perfil.SeleccionarPrestamo", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.SeleccionarPrestamo");
            }
            return false;
        }

        public bool CambiarImagen()
        {
            Logs.IniciaMetodo("Perfil.CambiarImagen", string.Format("Id: {0} - UrlImagen: {1}", this.Id, this.UrlImagen));
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Modificar_Imagen", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUrlImagen", Direction = System.Data.ParameterDirection.Input, Value = this.UrlImagen });
                var temp = DB.QueryCommand(command);

                if (temp == 1)
                {
                    Logs.InfoResult("Perfil.CambiarImagen", true.ToString());
                    return true;
                }

            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.CambiarImagen");
            }
            return false;
        }

        public bool Agregar(Prestamo p)
        {
            p.Prestamista = this;
            Logs.IniciaMetodo("Perfil.Agregar", p.toString());
            try
            {
                return p.Crear();
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Agregar (Prestamo)");
            }
            return false;
        }

        public bool Agregar(Item inItem)
        {
            try
            {
                inItem.Propietario = this;
                Logs.IniciaMetodo("Perfil.Agregar", inItem.toString());
                return inItem.Crear();
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Agregar");
            }
            return false;
        }

        public bool Disponibilidad(Item inItem, bool inDisponibilidad)
        {
            Logs.IniciaMetodo("Perfil.Disponibilidad", string.Format("Item: {0} - Disponibilidad: {1}", inItem.toString(), inDisponibilidad.ToString()));
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Disponibilidad_Item", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inItem_Id", Direction = System.Data.ParameterDirection.Input, Value = inItem.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inDisponibilidad", Direction = System.Data.ParameterDirection.Input, Value = inDisponibilidad });
                var datos = DB.QueryCommand(command);
                if (datos == 1)
                {
                    Logs.InfoResult("Perfil.Disponibilidad", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Disponibilidad");
            }
            return false;
        }

        public bool Remover(Item outItem)
        {
            Logs.IniciaMetodo("Perfil.Remover (Item)", outItem.toString());
            try
            {
                return outItem.Eliminar();
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Remover (Item)");
            }

            return false;
        }

        public List<Prestamo> HistorialPrestamista(int Pagina = 1, int NumeroDeResultados = int.MaxValue)
        {
            Logs.IniciaMetodo("Perfil.HistorialPrestamista", string.Format("Pagina: {0} - Numero de Resultados: {1}", Pagina, NumeroDeResultados));
            List<Prestamo> HistorialPrestamos = new List<Prestamo>();
            int index = NumeroDeResultados * (Pagina - 1);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Historial_Prestamista", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inOffset", Direction = System.Data.ParameterDirection.Input, Value = index });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = NumeroDeResultados });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Prestamo pres = new Prestamo();
                        Perfil re = new Perfil();
                        setHistorial(ref pres, ref re, datos.Tables[0].Rows[i]);
                        pres.Receptor = re;
                        pres.Prestamista = this;
                        HistorialPrestamos.Add(pres);
                        Logs.InfoResult("Perfil.HistorialPrestamista", pres.toString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.HistorialPrestamista");
            }

            return HistorialPrestamos;
        }

        public List<Prestamo> HistorialReceptor(int Pagina = 1, int NumeroDeResultados = int.MaxValue)
        {
            Logs.IniciaMetodo("Perfil.HistorialReceptor", string.Format("Pagina: {0} - Numero de Resultados: {1}", Pagina, NumeroDeResultados));
            List<Prestamo> HistorialPrestamos = new List<Prestamo>();
            int index = NumeroDeResultados * (Pagina - 1);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Historial_Receptor", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inPerfil_Id", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inOffset", Direction = System.Data.ParameterDirection.Input, Value = index });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = NumeroDeResultados });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Prestamo pres = new Prestamo();
                        Perfil re = new Perfil();
                        setHistorial(ref pres, ref re, datos.Tables[0].Rows[i]);
                        pres.Receptor = this;
                        pres.Prestamista = re;
                        HistorialPrestamos.Add(pres);
                        Logs.InfoResult("Perfil.HistorialReceptor", pres.toString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.HistorialReceptor");
            }

            return HistorialPrestamos;
        }

        public int ReputacionPrestamista()
        {
            Logs.IniciaMetodo("Perfil.ReputacionPrestamista", string.Empty);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Reputacion_Prestamista", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    Int16 rep = Convert.ToInt16(datos.Tables[0].Rows[0]["Reputacion"]);
                    Logs.InfoResult("Perfil.ReputacionPrestamista", "Reputacion: " + rep);
                    return rep;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.ReputacionPrestamista");
            }
            return 0;
        }

        public int ReputacionReceptor()
        {
            Logs.IniciaMetodo("Perfil.ReputacionReceptor", string.Empty);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Reputacion_Receptor", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });

                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    Int16 rep = Convert.ToInt16(datos.Tables[0].Rows[0]["Reputacion"]);
                    Logs.InfoResult("Perfil.ReputacionReceptor", "Reputacion: "+ rep);
                    return rep;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.ReputacionReceptor");
            }
            return 0;
        }

        public Dictionary<Item, bool> MisItems()
        {
            Logs.IniciaMetodo("Perfil.MisItems", string.Empty);

          Dictionary <Item, bool> items = new Dictionary<Item, bool>();
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Seleccionar_Items", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_Id"]));
                        item.Titulo = (datos.Tables[0].Rows[i]["Titulo"].ToString());
                        item.Resumen = (datos.Tables[0].Rows[i]["Resumen"].ToString());
                        item.VecesVisto = (Convert.ToInt64(datos.Tables[0].Rows[i]["VecesVisto"]));
                        item.UrlImagen = (datos.Tables[0].Rows[i]["UrlImagen"].ToString());
                        item.Propietario = this;

                        bool esPrestable = Convert.ToBoolean(datos.Tables[0].Rows[i]["Disponibilidad"]);
                        items.Add(item, esPrestable);
                        Logs.InfoResult("Perfil.MisItems", string.Format("Item: {0} - EsPrestable: {1}", item.toString(), esPrestable.ToString()));
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.MisItems");
            }
            return items;
        }

        public bool Crear()

        {
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Crear", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombre", Direction = System.Data.ParameterDirection.Input, Value = this.Nombre });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUrl", Direction = System.Data.ParameterDirection.Input, Value = this.UrlImagen });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNacionalidad", Direction = System.Data.ParameterDirection.Input, Value = this.Nacionalidad });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Cuenta.Email });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "outId", Direction = System.Data.ParameterDirection.Output });
                var datos = DB.QueryCommand(command);
                if (datos == 1)
                {
                    this.Id = Convert.ToInt64(command.Parameters["outId"].Value);
                    Ubicacion ub = this.Ubicacion;
                    ub.Perfil_Id = this.Id;
                    ub.Crear();
                    Logs.InfoResult("Perfil.Crear", true.ToString() + " - Id: "+this.Id);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Crear");
            }
            return false;
        }

        public bool Seleccionar(Int64 Id)
        {
            Logs.IniciaMetodo("Perfil.Seleccionar", "Id: " + Id);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Seleccionar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = Id });
                var datos = DB.GetDataSet(command);
                Logs.Info(this.ToString() + ".Selecionar", "Id: " + Id.ToString());
                if (datos.Tables[0].Rows.Count > 0)
                {
                    this.SetDesde(datos.Tables[0].Rows[0]);
                    Logs.InfoResult("Perfil.", this.toString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Seleccionar");
            }
            return false;
        }

        public bool Modificar()
        {
            Logs.IniciaMetodo("Perfil.Modificar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNombre", Direction = System.Data.ParameterDirection.Input, Value = this.Nombre });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUrl", Direction = System.Data.ParameterDirection.Input, Value = this.UrlImagen });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inNacionalidad", Direction = System.Data.ParameterDirection.Input, Value = this.Nacionalidad });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inEmail", Direction = System.Data.ParameterDirection.Input, Value = this.Cuenta.Email });
                var temp = DB.QueryCommand(command);
                if (temp == 1)
                {
                    this.Ubicacion.Perfil_Id = this.Id;
                    this.Ubicacion.Modificar();
                    Logs.InfoResult("Perfil.Modificar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Modificar");
            }
            return false;
        }

        public bool Eliminar()
        {
            Logs.IniciaMetodo("Perfil.Eliminar", this.toString());
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Perfil_Eliminar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inId", Direction = System.Data.ParameterDirection.Input, Value = this.Id });
                var datos = DB.QueryCommand(command);
                if (datos == 1)
                {
                    Logs.InfoResult("Perfil.Eliminar", true.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Perfil.Eliminar");
            }
            return false;
        }

        private void SetDesde(DataRow dr)
        {
            this.Id = Convert.ToInt64(dr["Id"]);
            this.Nombre = dr["Nombre"].ToString();
            this.UrlImagen = dr["UrlImagen"].ToString();
            this.Nacionalidad = dr["Nacionalidad"].ToString();
            this.Cuenta = new Cuenta() { Email = dr["Cuenta_Email"].ToString() };
            this.Ubicacion = new Ubicacion();
            this.Ubicacion.Seleccionar(Convert.ToInt64(dr["Id"]));
        }

        private void setHistorial(ref Prestamo pre, ref Perfil pe, DataRow dr)
        {
            pre.Id = Convert.ToInt64(dr["Id"]);
            pre.Estado = (EstadoPrestamo)Convert.ToInt16(dr["Estado"]);
            pre.FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]);
            pre.FechaRecepcion = Convert.ToDateTime(dr["FechaRecepcion"]);
            pre.FechaExpiracion = Convert.ToDateTime(dr["FechaExpiracion"]);
            pre.CalificacionAlReceptor = Convert.ToInt16(dr["CalificacionAlReceptor"]);
            pre.CalificacionAlPrestamista = Convert.ToInt16(dr["CalificacionAlPrestamista"]);
            pre.FechaUltimaModificacion = Convert.ToDateTime(dr["UltimaModificacion"]);

            Item itm = new Item();
            itm.Id = Convert.ToInt64(dr["Item_Id"]);
            itm.Titulo = dr["Titulo"].ToString();
            itm.UrlImagen = dr["Item_UrlImagen"].ToString();
            pre.Item = itm;

            pe.Id = Convert.ToInt64(dr["Perfil_Id"]);
            pe.Nombre = dr["Nombre"].ToString();
            pe.UrlImagen = dr["Perfil_UrlImagen"].ToString();
        }

        public string toString()
        {
            return string.Format("Id: {0} - Email: {1}", Id, Cuenta.Email);
        }
    }

}