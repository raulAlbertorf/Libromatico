using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libros.Models
{
    public class Estanteria
    {

        public List<Item> Buscar(string termino, int pagina, int cantResult)
        {
            Logs.IniciaMetodo("Estanteria.Buscar", string.Format("Termino: {0} - Paguina: {1} - CantResult: {2}", termino, pagina, cantResult));

            List<Item> items = new List<Item>();
            int index = cantResult * ( pagina - 1 );
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Buscar", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inTermino", Direction = System.Data.ParameterDirection.Input, Value = termino });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inOffset", Direction = System.Data.ParameterDirection.Input, Value = index });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = cantResult });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i=0; i<datos.Tables[0].Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Propietario = new Perfil();

                        item.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_Id"]));
                        item.Titulo = (datos.Tables[0].Rows[i]["Item_Titulo"].ToString());
                        item.Resumen = (datos.Tables[0].Rows[i]["Item_Resumen"].ToString());
                        item.VecesVisto = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_VecesVisto"]));
                        item.UrlImagen = ( datos.Tables[ 0 ].Rows[ i ][ "Item_UrlImagen" ].ToString( ) );
                        item.Propietario.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Perfil_Id"]));
                        item.Propietario.Nombre = (datos.Tables[0].Rows[i]["Perfil_Nombre"].ToString());
                        item.Propietario.UrlImagen = (datos.Tables[0].Rows[i]["Perfil_UrlImagen"].ToString());
                        item.Propietario.Nacionalidad = (datos.Tables[0].Rows[i]["Perfil_Nac"].ToString());

                        items.Add(item);
                        Logs.InfoResult("Estanteria.Buscar", "Item: " + item.toString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.Buscar");
            }
            return items;
        }

        public List<Item> Buscar(string termino, Ubicacion ubicacion, int pagina, int cantidadResultados, int radio)
        {
            Logs.IniciaMetodo("Estanteria.Buscar", string.Format("Termino: {0} - Paguina: {1} - CantResult: {2} - Radio: {3}", termino, pagina, cantidadResultados, radio));

            var items = new List<Item>();
            int index = cantidadResultados * (pagina - 1);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Seleccionar_Items_Termino_Ubicacion", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inTermino", Direction = System.Data.ParameterDirection.Input, Value = termino });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUbicacion_Latitude", Direction = System.Data.ParameterDirection.Input, Value = ubicacion.lat });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inUbicacion_Longitude", Direction = System.Data.ParameterDirection.Input, Value = ubicacion.lon });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inRadio", Direction = System.Data.ParameterDirection.Input, Value = radio });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inOffset", Direction = System.Data.ParameterDirection.Input, Value = index });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = cantidadResultados});
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                   for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_Id"]));
                        item.Titulo = (datos.Tables[0].Rows[i]["Item_Titulo"].ToString());
                        item.Resumen = (datos.Tables[0].Rows[i]["Item_Resumen"].ToString());
                        item.UrlImagen = (datos.Tables[0].Rows[i]["Item_UrlImagen"].ToString());
                        item.VecesVisto = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_VecesVisto"]));
                        item.Propietario = new Perfil();
                        item.Propietario.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Perfil_Id"]));
                        item.Propietario.Nombre = (datos.Tables[0].Rows[i]["Perfil_Nombre"].ToString());
                        item.Propietario.UrlImagen = (datos.Tables[0].Rows[i]["Perfil_UrlImagen"].ToString());
                        item.Propietario.Nacionalidad = (datos.Tables[0].Rows[i]["Perfil_Nacionalidad"].ToString());
                        item.Propietario.Cuenta = new Cuenta();
                        item.Propietario.Cuenta.Email = (datos.Tables[0].Rows[i]["Cuenta_Email"].ToString());
                        item.Propietario.Cuenta.Contrasena = (datos.Tables[0].Rows[i]["Cuenta_Contrasena"].ToString());
                        items.Add(item);
                        Logs.InfoResult("Estanteria.Buscar", "Item: " + item.toString());

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
                Logs.SalirMetodo("Estanteria.Buscar");
            }
            return items;
        }

        public List<Item> UltimosAgregados(int cantidad)
        {
            Logs.IniciaMetodo("Estanteria.UltimosAgregados", "Cantidad: " + cantidad);
            List<Item> items = new List<Item>();

            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Item_Ultimos_Agregados", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inCantidad", Direction = System.Data.ParameterDirection.Input, Value = cantidad });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i=0; i<datos.Tables[0].Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Propietario = new Perfil();

                        item.Id = Convert.ToInt64(datos.Tables[0].Rows[i]["Id"]);
                        item.Titulo = (datos.Tables[0].Rows[i]["Titulo"].ToString());
                        item.Resumen = (datos.Tables[0].Rows[i]["Resumen"].ToString());
                        item.UrlImagen = (datos.Tables[0].Rows[i]["UrlImagen"].ToString());
                        item.VecesVisto = (Convert.ToInt64(datos.Tables[0].Rows[i]["VecesVisto"]));
                        item.Propietario.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Perfil_Id"]));

                        items.Add(item);
                        Logs.InfoResult("Estanteria.UltimosAgregados", "Item: " + item.toString());

                    }
                }
            }
            catch(Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.UltimosAgregados");
            }

            return items;
        }

        public List<Perfil> LosMasGenerosos(int cant)
        {
            Logs.IniciaMetodo("Estanteria.LosMasGenerosos", "Cantidad: " + cant);
            List<Perfil> lista_perfiles = new List<Perfil>();
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Estanteria_Perfiles_Mas_Generosos", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = cant });
                var datos = DB.GetDataSet(command);
                
                foreach(DataRow dr in datos.Tables[0].Rows)
                {
                    Perfil p = new Perfil() { Id = Convert.ToInt64(dr["Id"]), Nombre = dr["Nombre"].ToString(), UrlImagen= dr["UrlImagen"].ToString() };
                    lista_perfiles.Add(p);
                    Logs.InfoResult("Estanteria.LosMasGenerosos", "Id: "+ p.Id);
                }
                
            }
            catch (Exception Ex)
            {
                Logs.Error(Ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.LosMasGenerosos");
            }
            return lista_perfiles;
        }

        public List<Perfil> LosMasCabrones(int cant)
        {
            Logs.IniciaMetodo("Estanteria.LosMasCabrones", "Cantidad: " + cant);

            List<Perfil> lista_perfiles = new List<Perfil>();
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Estanteria_Perfiles_Mas_Cabrones", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = cant });
                var datos = DB.GetDataSet(command);

                foreach (DataRow dr in datos.Tables[0].Rows)
                {
                    Perfil p = new Perfil() { Id = Convert.ToInt64(dr["Id"]), Nombre = dr["Nombre"].ToString() };
                    lista_perfiles.Add(p);
                    Logs.InfoResult("Estanteria.LosMasCabrones", "Id: " + p.Id);
                }
            }
            catch (Exception Ex)
            {
                Logs.Error(Ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.LosMasCabrones");

            }
            return lista_perfiles;
        }

        public List<Item> BuscarPorAutor( string termino , int pagina , int cantResult )
        {
            Logs.IniciaMetodo("Estanteria.BuscarPorAutor", string.Format("Termino: {0} - Paguina: {1} - CantResult: {2}", termino, pagina, cantResult));

            List<Item> items = new List<Item>( );
            int index = cantResult * ( pagina - 1 );
            try
            {
                var command = new MySqlCommand( ) { CommandText = "sp_Estanteria_Buscar_Por_Autor" , CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inTermino" , Direction = System.Data.ParameterDirection.Input , Value = termino } );
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inPage" , Direction = System.Data.ParameterDirection.Input , Value = index } );
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inCantResult" , Direction = System.Data.ParameterDirection.Input , Value = cantResult } );
                var datos = DB.GetDataSet( command );

                if ( datos.Tables[ 0 ].Rows.Count > 0 )
                {
                    for ( int i = 0 ; i < datos.Tables[ 0 ].Rows.Count ; i++ )
                    {
                        Item item = new Item( );
                        item.Propietario = new Perfil( );
                        item.Id = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Item_Id" ] ) );
                        item.Titulo = ( datos.Tables[ 0 ].Rows[ i ][ "Item_Titulo" ].ToString( ) );
                        item.Resumen = ( datos.Tables[ 0 ].Rows[ i ][ "Item_Resumen" ].ToString( ) );
                        item.UrlImagen = ( datos.Tables[ 0 ].Rows[ i ][ "Item_UrlImagen" ].ToString( ) );
                        item.VecesVisto = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Item_VecesVisto" ] ) );
                        item.Propietario.Id = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Perfil_Id" ] ) );
                        item.Propietario.Nombre = ( datos.Tables[ 0 ].Rows[ i ][ "Perfil_Nombre" ].ToString( ) );

                        items.Add( item );
                        Logs.InfoResult("Estanteria.BuscarPorAutor", "Item: " + item.toString());

                    }
                }
            }
            catch ( Exception ex )
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.BuscarPorAutor");
            }
            return items;
        }

        public List<Item> BuscarPorPropiedad(string propiedad, int pagina, int cantResult)
        {
            Logs.IniciaMetodo("Estanteria.BuscarPorPropiedad", string.Format("Propiedad: {0} - Paguina: {1} - CantResult: {2}", propiedad, pagina, cantResult));

            List<Item> items = new List<Item>();
            int index = cantResult * (pagina - 1);
            try
            {
                var command = new MySqlCommand() { CommandText = "sp_Estanteria_Buscar_Propiedad", CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inValue", Direction = System.Data.ParameterDirection.Input, Value = propiedad });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inOffset", Direction = System.Data.ParameterDirection.Input, Value = index });
                command.Parameters.Add(new MySqlParameter() { ParameterName = "inLimit", Direction = System.Data.ParameterDirection.Input, Value = cantResult });
                var datos = DB.GetDataSet(command);

                if (datos.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < datos.Tables[0].Rows.Count; i++)
                    {
                        Item item = new Item();
                        item.Propietario = new Perfil();

                        item.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_Id"]));
                        item.Titulo = (datos.Tables[0].Rows[i]["Item_Titulo"].ToString());
                        item.Resumen = (datos.Tables[0].Rows[i]["Item_Resumen"].ToString());
                        item.UrlImagen = ( datos.Tables[ 0 ].Rows[ i ][ "Item_UrlImagen" ].ToString( ) );
                        item.VecesVisto = (Convert.ToInt64(datos.Tables[0].Rows[i]["Item_VecesVisto"]));
                        item.Propietario.Id = (Convert.ToInt64(datos.Tables[0].Rows[i]["Perfil_Id"]));
                        item.Propietario.Nombre = (datos.Tables[0].Rows[i]["Perfil_Nombre"].ToString());
                        item.Propietario.UrlImagen = (datos.Tables[0].Rows[i]["Perfil_UrlImagen"].ToString());
                        item.Propietario.Nacionalidad = (datos.Tables[0].Rows[i]["Perfil_Nacionalidad"].ToString());

                        items.Add(item);
                        Logs.InfoResult("Estanteria.BuscarPorPropiedad", "Item: " + item.toString());

                    }
                }
            }
            catch (Exception ex)
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.BuscarPorPropiedad");
            }
            return items;
        }

        public List<Item> BuscarPorAll( string termino , int pagina , int cantResult )
        {
            Logs.IniciaMetodo("Estanteria.BuscarPorAll", string.Format("Termino: {0} - Paguina: {1} - CantResult: {2}", termino, pagina, cantResult));
            List<Item> items = new List<Item>( );
            int index = cantResult * ( pagina - 1 );
            try
            {
                var command = new MySqlCommand( ) { CommandText = "sp_Estanteria_Buscar_All" , CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inTermino" , Direction = System.Data.ParameterDirection.Input , Value = termino } );
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inPage" , Direction = System.Data.ParameterDirection.Input , Value = index } );
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inCantResult" , Direction = System.Data.ParameterDirection.Input , Value = cantResult } );
                var datos = DB.GetDataSet( command );

                if ( datos.Tables[ 0 ].Rows.Count > 0 )
                {
                    for ( int i = 0 ; i < datos.Tables[ 0 ].Rows.Count ; i++ )
                    {
                        Item item = new Item( );
                        item.Propietario = new Perfil( );
                        item.Id = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Item_Id" ] ) );
                        item.Titulo = ( datos.Tables[ 0 ].Rows[ i ][ "Item_Titulo" ].ToString( ) );
                        item.Resumen = ( datos.Tables[ 0 ].Rows[ i ][ "Item_Resumen" ].ToString( ) );
                        item.UrlImagen = ( datos.Tables[ 0 ].Rows[ i ][ "Item_UrlImagen" ].ToString( ) );
                        item.VecesVisto = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Item_VecesVisto" ] ) );
                        item.Propietario.Id = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Perfil_Id" ] ) );
                        item.Propietario.Nombre = ( datos.Tables[ 0 ].Rows[ i ][ "Perfil_Nombre" ].ToString( ) );

                        items.Add( item );
                        Logs.InfoResult("Estanteria.BuscarPorAll", "Item: " + item.toString());

                    }
                }
            }
            catch ( Exception ex )
            {
                Logs.Error(ex);
            }
            finally
            {
                Logs.SalirMetodo("Estanteria.BuscarPorAll");
            }
            return items;
        }

        public List<Item> LosMasVistos( int cant )
        {
            Logs.IniciaMetodo( "Estanteria.LosMasVisto" , "Cantidad: " + cant );
            List<Item> items = new List<Item>( );

            try
            {
                var command = new MySqlCommand( ) { CommandText = "sp_Item_LosMasVisto" , CommandType = System.Data.CommandType.StoredProcedure };
                command.Parameters.Add( new MySqlParameter( ) { ParameterName = "inCantidad" , Direction = System.Data.ParameterDirection.Input , Value = cant } );
                var datos = DB.GetDataSet( command );

                if ( datos.Tables[ 0 ].Rows.Count > 0 )
                {
                    for ( int i = 0 ; i < datos.Tables[ 0 ].Rows.Count ; i++ )
                    {
                        Item item = new Item( );
                        item.Propietario = new Perfil( );

                        item.Id = Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Id" ] );
                        item.Titulo = ( datos.Tables[ 0 ].Rows[ i ][ "Titulo" ].ToString( ) );
                        item.Resumen = ( datos.Tables[ 0 ].Rows[ i ][ "Resumen" ].ToString( ) );
                        item.UrlImagen = ( datos.Tables[ 0 ].Rows[ i ][ "UrlImagen" ].ToString( ) );
                        item.VecesVisto = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "VecesVisto" ] ) );
                        item.Propietario.Id = ( Convert.ToInt64( datos.Tables[ 0 ].Rows[ i ][ "Perfil_Id" ] ) );

                        items.Add( item );
                        Logs.InfoResult( "Estanteria.UltimosAgregados" , "Item: " + item.toString( ) );

                    }
                }
            }
            catch ( Exception ex )
            {
                Logs.Error( ex );
            }
            finally
            {
                Logs.SalirMetodo( "Estanteria.LosMasVisto" );
            }

            return items;
        }
    }
}
