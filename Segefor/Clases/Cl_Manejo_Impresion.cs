using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SEGEFOR.Data_Set;

namespace SEGEFOR.Clases
{
    public class Cl_Manejo_Impresion
    {
        OleDbConnection cn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conexion"]);
        DataSet ds = new DataSet();
        SqlConnection cnSql = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConexionSql"]);
        Cl_Gestion_Registro ClGestionRegistro;
        
        public DataSet Impresion_PlanManejo(int Id, int Origen, int SubCategoriaId)
        {
            
            Ds_PlanManejo Ds_PlanManejo = new Ds_PlanManejo();
            //Llenar Encabezado
            DataSet dsDatos = Get_CategoriaCompletaPlanManejo(Id, Origen);
            Ds_PlanManejo.Tables["Dt_Encabezado"].Clear();
            DataRow row = Ds_PlanManejo.Tables["Dt_Encabezado"].NewRow();
            row["CategoriaManejo"] = dsDatos.Tables["Datos"].Rows[0]["Categoria"];
            row["SubCategoriaManejo"] = dsDatos.Tables["Datos"].Rows[0]["SubCategoria"];
            row["Regente"] = dsDatos.Tables["Datos"].Rows[0]["Regente"];
            row["CodRegente"] = dsDatos.Tables["Datos"].Rows[0]["CodReg"];
            row["TieneRepresentantes"] = TieneRepresentantes_PlanManejo(Id, Origen);
            row["TieneProdNoMaderables"] = TieneProdNoMaderables_PlanManejo(Id,Origen);
            row["TipoPropiedadId"] = Get_TipoPropiedadId(Id, Origen);
            row["TipoInv"] = GetTipoInventario(Id, Origen);
            
            Ds_PlanManejo.Tables["Dt_Encabezado"].Rows.Add(row);
            dsDatos.Clear();
            
            return Ds_PlanManejo;

        }

        public DataSet Get_CategoriaCompletaPlanManejo(int Id, int Origen)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_Get_CategoriaCompletaPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public DataSet GetReporte_PlanManejo_VistaPrevia(int Id, int Origen)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("sp_GetReporte_PlanManejo_VistaPrevia", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public DataSet Bosques_PlanManejo(int Id, int Origen)
        {
            Ds_PlanManejo Ds_PlanManejo = new Ds_PlanManejo();
            DataSet DsFincas = GetFincasPlanManejo(Id, Origen);
            Ds_PlanManejo.Tables["DtBosque"].Clear();
            for (int i = 0; i < DsFincas.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowBosque = Ds_PlanManejo.Tables["DtBosque"].NewRow();
                rowBosque["TipoBosque"] = GetTipo_BosqueManejo(Id, Convert.ToInt32(DsFincas.Tables["Datos"].Rows[i]["InmuebleId"]), Origen);
                rowBosque["ClaseDesarrollo"] = GetClaseDesarrolloFinca(Id, Convert.ToInt32(DsFincas.Tables["Datos"].Rows[i]["InmuebleId"]), Origen);
                rowBosque["Especies"] = GetEspecies_BosqueManejo(Id, Convert.ToInt32(DsFincas.Tables["Datos"].Rows[i]["InmuebleId"]), Origen);
                rowBosque["Finca"] = DsFincas.Tables["Datos"].Rows[i]["Finca"];
                Ds_PlanManejo.Tables["DtBosque"].Rows.Add(rowBosque);
            }
            DsFincas.Clear();
            return Ds_PlanManejo;
        }


        public DataSet VistaPrevia_PlanManejo(int Id, int Origen)
        {
            Ds_PlanManejo Ds_PlanManejo = new Ds_PlanManejo();
            //LLenar Fincas
            DataSet dsDatos = GetReporte_PlanManejo_VistaPrevia(Id, Origen);
            Ds_PlanManejo.Tables["DtFinca"].Clear();
            //DataSet0
            for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowFinca = Ds_PlanManejo.Tables["DtFinca"].NewRow();
                rowFinca["Nombre"] = dsDatos.Tables["Datos"].Rows[i]["Finca"];
                rowFinca["GTMNorte"] = dsDatos.Tables["Datos"].Rows[i]["Gtm_Norte"];
                rowFinca["GTMOEste"] = dsDatos.Tables["Datos"].Rows[i]["Gtm_Oeste"];
                rowFinca["TipoDocuementoPosecion"] = dsDatos.Tables["Datos"].Rows[i]["TipoDocPropiedad"];
                rowFinca["TipoDocumentoId"] = dsDatos.Tables["Datos"].Rows[i]["TipoDoc_PropiedadId"];
                rowFinca["FecEmision"] = dsDatos.Tables["Datos"].Rows[i]["FecEmi"];
                if (dsDatos.Tables["Datos"].Rows[i]["Nofinca"].ToString() == "")
                    rowFinca["No_Finca"] = "";
                else
                    rowFinca["No_Finca"] = dsDatos.Tables["Datos"].Rows[i]["Nofinca"];
                if (dsDatos.Tables["Datos"].Rows[i]["Folio"].ToString() == "")
                    rowFinca["Folio"] = "";
                else
                    rowFinca["Folio"] = dsDatos.Tables["Datos"].Rows[i]["Folio"];
                if (dsDatos.Tables["Datos"].Rows[i]["Libro"].ToString() == "")
                    rowFinca["Libro"] = "";
                else
                    rowFinca["Libro"] = dsDatos.Tables["Datos"].Rows[i]["Libro"];
                if (dsDatos.Tables["Datos"].Rows[i]["De"].ToString() == "")
                    rowFinca["De"] = "";
                else
                    rowFinca["De"] = dsDatos.Tables["Datos"].Rows[i]["De"];
                if (dsDatos.Tables["Datos"].Rows[i]["NoCertificacion"].ToString() == "")
                    rowFinca["No_Certificacion"] = "";
                else
                    rowFinca["No_Certificacion"] = dsDatos.Tables["Datos"].Rows[i]["NoCertificacion"];
                if (dsDatos.Tables["Datos"].Rows[i]["Municipalidad"].ToString() == "")
                    rowFinca["Municipalidad"] = "";
                else
                    rowFinca["Municipalidad"] = dsDatos.Tables["Datos"].Rows[i]["Municipalidad"];
                if (dsDatos.Tables["Datos"].Rows[i]["NoEscritura"].ToString() == "")
                    rowFinca["No_Escritura"] = "";
                else
                    rowFinca["No_Escritura"] = dsDatos.Tables["Datos"].Rows[i]["NoEscritura"];
                if (dsDatos.Tables["Datos"].Rows[i]["TituloNotario"].ToString() == "")
                    rowFinca["TituloNotario"] = "";
                else
                    rowFinca["TituloNotario"] = dsDatos.Tables["Datos"].Rows[i]["TituloNotario"];
                if (dsDatos.Tables["Datos"].Rows[i]["Notario"].ToString() == "")
                    rowFinca["NombreNotario"] = "";
                else
                    rowFinca["NombreNotario"] = dsDatos.Tables["Datos"].Rows[i]["Notario"];
                rowFinca["Direccion"] = dsDatos.Tables["Datos"].Rows[i]["Direccion"];
                rowFinca["Aldea"] = dsDatos.Tables["Datos"].Rows[i]["Aldea"];
                rowFinca["Departamento"] = dsDatos.Tables["Datos"].Rows[i]["Departamento"];
                rowFinca["Municipio"] = dsDatos.Tables["Datos"].Rows[i]["Municipio"];
                rowFinca["ColNorte"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Norte"];
                rowFinca["ColSur"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Sur"];
                rowFinca["ColEste"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Este"];
                rowFinca["ColOeste"] = dsDatos.Tables["Datos"].Rows[i]["Colindancia_Oeste"];
                rowFinca["AreaFinca"] = dsDatos.Tables["Datos"].Rows[i]["Area"];
                rowFinca["NomPropietario"] = dsDatos.Tables["Datos"].Rows[i]["Propietario"];
                rowFinca["TipoIdPropietario"] = dsDatos.Tables["Datos"].Rows[i]["TipoIdPropietario"];
                rowFinca["IdPropietario"] = dsDatos.Tables["Datos"].Rows[i]["DPI"];
                rowFinca["InmuebleId"] = dsDatos.Tables["Datos"].Rows[i]["InmuebleId"];
                rowFinca["TipoPropiedad"] = dsDatos.Tables["Datos"].Rows[i]["Tipo_PersonaId"];
                rowFinca["NombreEmpresa"] = dsDatos.Tables["Datos"].Rows[i]["Nombre_Empresa"];
                Ds_PlanManejo.Tables["DtFinca"].Rows.Add(rowFinca);
            }
            //DataSet1
            for (int j = 0; j < dsDatos.Tables["Datos1"].Rows.Count; j++)
            {
                DataRow rowRepresentante = Ds_PlanManejo.Tables["Dt_Representates"].NewRow();
                rowRepresentante["Nombre"] = dsDatos.Tables["Datos1"].Rows[j]["Nombres"];
                rowRepresentante["TipoIdentificacion"] = dsDatos.Tables["Datos1"].Rows[j]["TipoIdPropietario"];
                rowRepresentante["IdIdentificacion"] = dsDatos.Tables["Datos1"].Rows[j]["DPI"];
                Ds_PlanManejo.Tables["Dt_Representates"].Rows.Add(rowRepresentante);
            }
            ////DataSet2
            for (int k = 0; k < dsDatos.Tables["Datos2"].Rows.Count; k++)
            {
                DataRow rowAreas = Ds_PlanManejo.Tables["Dt_Areas"].NewRow();
                rowAreas["Forestal"] = dsDatos.Tables["Datos2"].Rows[k]["Forestal"];
                rowAreas["PorForestal"] = dsDatos.Tables["Datos2"].Rows[k]["PorForestal"];
                rowAreas["Agricultura"] = dsDatos.Tables["Datos2"].Rows[k]["Agricultura"];
                rowAreas["PorAgricultura"] = dsDatos.Tables["Datos2"].Rows[k]["PorAgricultura"];
                rowAreas["Ganaderia"] = dsDatos.Tables["Datos2"].Rows[k]["Ganaderia"];
                rowAreas["PorGanaderia"] = dsDatos.Tables["Datos2"].Rows[k]["PorGanaderia"];
                rowAreas["Agroforestal"] = dsDatos.Tables["Datos2"].Rows[k]["Agroforestal"];
                rowAreas["PorAgroforestal"] = dsDatos.Tables["Datos2"].Rows[k]["PorAgroforestal"];
                rowAreas["Otro"] = dsDatos.Tables["Datos2"].Rows[k]["Otro"];
                rowAreas["PorOtro"] = dsDatos.Tables["Datos2"].Rows[k]["PorOtro"];
                rowAreas["AreaBosque"] = dsDatos.Tables["Datos2"].Rows[k]["AreaBosque"];
                rowAreas["AreaIntervenir"] = dsDatos.Tables["Datos2"].Rows[k]["AreaIntervenir"];
                rowAreas["AreaProteccion"] = dsDatos.Tables["Datos2"].Rows[k]["AreaProteccion"];
                rowAreas["Pendiente"] = dsDatos.Tables["Datos2"].Rows[k]["Pendiente"];
                rowAreas["PorPendiente"] = dsDatos.Tables["Datos2"].Rows[k]["PorPendiente"];
                rowAreas["Profundidad"] = dsDatos.Tables["Datos2"].Rows[k]["Profundidad"];
                rowAreas["PorProfundidad"] = dsDatos.Tables["Datos2"].Rows[k]["PorProfundidad"];
                rowAreas["Pedregosidad"] = dsDatos.Tables["Datos2"].Rows[k]["Pedregosidad"];
                rowAreas["PorPedregosidad"] = dsDatos.Tables["Datos2"].Rows[k]["PorPedregosidad"];
                rowAreas["Anegamiento"] = dsDatos.Tables["Datos2"].Rows[k]["Anegamiento"];
                rowAreas["PorAnegamiento"] = dsDatos.Tables["Datos2"].Rows[k]["PorAnegamiento"];
                rowAreas["BosqueGaleria"] = dsDatos.Tables["Datos2"].Rows[k]["BosqueGaleria"];
                rowAreas["PorBosqueGaleria"] = dsDatos.Tables["Datos2"].Rows[k]["PorBosqueGaleria"];
                rowAreas["EspeciesProtegidas"] = dsDatos.Tables["Datos2"].Rows[k]["EspeciesProtegidas"];
                rowAreas["PorEspeciesProtegidas"] = dsDatos.Tables["Datos2"].Rows[k]["PorEspeciesProtegidas"];
                rowAreas["OtroProteccion"] = dsDatos.Tables["Datos2"].Rows[k]["OtroProteccion"];
                rowAreas["PorOtrosProteccion"] = dsDatos.Tables["Datos2"].Rows[k]["PorOtrosProteccion"];
                //rowAreas["AreaTotal"] = dsDatos.Tables["Datos2"].Rows[k]["AreaTotal"];
                Ds_PlanManejo.Tables["Dt_Areas"].Rows.Add(rowAreas);
            }
            ////DataSet3
            for (int i = 0; i < dsDatos.Tables["Datos3"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_InfoGeneralPlan"].NewRow();
                rowData["TiempoEjecucion"] = dsDatos.Tables["Datos3"].Rows[i]["TiempoEjecucion"] + " años";
                rowData["TiempoExtraccion"] = dsDatos.Tables["Datos3"].Rows[i]["TiempoExtraccion"] + " años";
                rowData["VolExtraer"] = dsDatos.Tables["Datos3"].Rows[i]["VolExtraer"] + " m3/rodal";
                rowData["SistemaCorta"] = dsDatos.Tables["Datos3"].Rows[i]["SistemaCorta"];
                rowData["IncrementoAnual"] = dsDatos.Tables["Datos3"].Rows[i]["IncrementoAnual"] + " m3/ha/año";
                rowData["TipoGarantia"] = dsDatos.Tables["Datos3"].Rows[i]["Tipo_Garantia"];
                rowData["SistemaRepo"] = dsDatos.Tables["Datos3"].Rows[i]["SistemaRepo"];
                rowData["CortaAnual"] = dsDatos.Tables["Datos3"].Rows[i]["CortaAnual"] + " m3/ha/año";
                rowData["Especies"] = GetEspecies_Manejar(Id, Origen);
                Ds_PlanManejo.Tables["Dt_InfoGeneralPlan"].Rows.Add(rowData);
            }

            ////DataSet4
            for (int i = 0; i < dsDatos.Tables["Datos4"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_DatosNotifica"].NewRow();
                rowData["Direccion"] = dsDatos.Tables["Datos4"].Rows[i]["Direccion"];
                rowData["Departamento"] = dsDatos.Tables["Datos4"].Rows[i]["Departamento"];
                rowData["Municipio"] = dsDatos.Tables["Datos4"].Rows[i]["Municipio"];
                rowData["TelefonoDomicilio"] = dsDatos.Tables["Datos4"].Rows[i]["Telefono_Domicilio"];
                rowData["Telefono"] = dsDatos.Tables["Datos4"].Rows[i]["Telefono"];
                rowData["Celular"] = dsDatos.Tables["Datos4"].Rows[i]["Celular"];
                rowData["Correo"] = dsDatos.Tables["Datos4"].Rows[i]["Correo"];
                Ds_PlanManejo.Tables["Dt_DatosNotifica"].Rows.Add(rowData);
            }

            ////DataSet5
            for (int i = 0; i < dsDatos.Tables["Datos5"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_Caracbiofisica"].NewRow();
                rowData["Altitud"] = dsDatos.Tables["Datos5"].Rows[i]["Altitud"];
                rowData["Topografia"] = dsDatos.Tables["Datos5"].Rows[i]["Topografia"];
                rowData["Suelos"] = dsDatos.Tables["Datos5"].Rows[i]["Suelos"];
                rowData["Hidrografia"] = dsDatos.Tables["Datos5"].Rows[i]["Hidrografia"];
                rowData["Zona_Vida"] = dsDatos.Tables["Datos5"].Rows[i]["Zona_Vida"];
                Ds_PlanManejo.Tables["Dt_Caracbiofisica"].Rows.Add(rowData);
            }

            ////DataSet6 --Aprovechamiento Forestal
            string AreaMuestreada = "0";
            string IntensidadMuestreo = "0";
            for (int i = 0; i < dsDatos.Tables["Datos6"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_Aprovechamiento"].NewRow();
                rowData["Tipo_Inventario"] = dsDatos.Tables["Datos6"].Rows[i]["Tipo_Inventario"];
                rowData["DiametroMin"] = dsDatos.Tables["Datos6"].Rows[i]["DiametroMin"] + " cm";
                rowData["TotRodal"] = dsDatos.Tables["Datos6"].Rows[i]["TotRodal"];
                rowData["Datos_Regresion"] = dsDatos.Tables["Datos6"].Rows[i]["Datos_Regresion"];
                rowData["Ecuaciones"] = dsDatos.Tables["Datos6"].Rows[i]["Ecuaciones"];
                if (dsDatos.Tables["Datos6"].Rows[i]["Tipo_InventarioId"].ToString() == "2")
                {
                    AreaMuestreada = dsDatos.Tables["Datos6"].Rows[i]["AreaMuestreada"].ToString();
                    IntensidadMuestreo = dsDatos.Tables["Datos6"].Rows[i]["IntensidadMuestreo"].ToString(); 
                }
                Ds_PlanManejo.Tables["Dt_Aprovechamiento"].Rows.Add(rowData);
            }

            ////DataSet7  --Resumen Inventario
            for (int i = 0; i < dsDatos.Tables["Datos7"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_ResumenInv"].NewRow();
                rowData["Rodal"] = dsDatos.Tables["Datos7"].Rows[i]["Rodal"];
                rowData["AreaRodal"] = dsDatos.Tables["Datos7"].Rows[i]["AreaRodal"];
                rowData["ClaseDesarrollo"] = dsDatos.Tables["Datos7"].Rows[i]["Clase_Desarrollo"];
                rowData["Edad"] = dsDatos.Tables["Datos7"].Rows[i]["Edad"];
                rowData["Pendiente"] = dsDatos.Tables["Datos7"].Rows[i]["Pendiente"];
                rowData["Codigo_Especie"] = dsDatos.Tables["Datos7"].Rows[i]["Codigo_Especie"];
                rowData["DAP"] = dsDatos.Tables["Datos7"].Rows[i]["DAP"];
                rowData["Altura"] = dsDatos.Tables["Datos7"].Rows[i]["Altura"];
                rowData["Densidad"] = dsDatos.Tables["Datos7"].Rows[i]["Densidad"];
                rowData["AreaBasal"] = dsDatos.Tables["Datos7"].Rows[i]["AreaBasal"];
                rowData["VolHa"] = dsDatos.Tables["Datos7"].Rows[i]["VolHa"];
                rowData["VolRodal"] = dsDatos.Tables["Datos7"].Rows[i]["VolRodal"];
                rowData["AreaBasalRodal"] = dsDatos.Tables["Datos7"].Rows[i]["AreaBasalRodal"];
                Ds_PlanManejo.Tables["Dt_ResumenInv"].Rows.Add(rowData);
            }

            ////DataSet8
            for (int i = 0; i < dsDatos.Tables["Datos8"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["DtProductosNoMaderables"].NewRow();
                rowData["Turno"] = dsDatos.Tables["Datos8"].Rows[i]["Turno"];
                rowData["Rodal"] = dsDatos.Tables["Datos8"].Rows[i]["Rodal"];
                rowData["Anis"] = dsDatos.Tables["Datos8"].Rows[i]["Anis"];
                rowData["Area"] = dsDatos.Tables["Datos8"].Rows[i]["Area"];
                rowData["Producto"] = dsDatos.Tables["Datos8"].Rows[i]["Nombre_Producto"];
                rowData["Peso"] = dsDatos.Tables["Datos8"].Rows[i]["Peso"];
                Ds_PlanManejo.Tables["DtProductosNoMaderables"].Rows.Add(rowData);
            }


            ////DataSet9
            for (int i = 0; i < dsDatos.Tables["Datos9"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_Planificacion"].NewRow();
                rowData["Creiterio_Regulacion"] = dsDatos.Tables["Datos9"].Rows[i]["Creiterio_Regulacion"];
                rowData["Formula"] = dsDatos.Tables["Datos9"].Rows[i]["Formula"];
                rowData["CAP"] = dsDatos.Tables["Datos9"].Rows[i]["CAP"];
                rowData["Justificacion"] = dsDatos.Tables["Datos9"].Rows[i]["Justificacion"];
                rowData["ActividadAprovechamiento"] = dsDatos.Tables["Datos9"].Rows[i]["ActividadAprovechamiento"];
                rowData["ObjetivoRecuperacion"] = dsDatos.Tables["Datos9"].Rows[i]["ObjetivoRecuperacion"];
                rowData["JutificacionEspecie"] = dsDatos.Tables["Datos9"].Rows[i]["JutificacionEspecie"];
                rowData["SistemaRepoblacionDesc"] = dsDatos.Tables["Datos9"].Rows[i]["SistemaRepoblacionDesc"];
                Ds_PlanManejo.Tables["Dt_Planificacion"].Rows.Add(rowData);
            }


            ////DataSet10
            for (int i = 0; i < dsDatos.Tables["Datos10"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_Silvicultura"].NewRow();
                rowData["Rodal"] = dsDatos.Tables["Datos10"].Rows[i]["Rodal"];
                rowData["Turno"] = dsDatos.Tables["Datos10"].Rows[i]["Turno"];
                rowData["Area"] = dsDatos.Tables["Datos10"].Rows[i]["AreaRodal"];
                rowData["Tratamiento"] = dsDatos.Tables["Datos10"].Rows[i]["Tratamiento"];
                rowData["Nombre_Cientifico"] = dsDatos.Tables["Datos10"].Rows[i]["Nombre_Cientifico"];
                rowData["VolTroza"] = dsDatos.Tables["Datos10"].Rows[i]["VolTroza"];
                rowData["VolLena"] = dsDatos.Tables["Datos10"].Rows[i]["VolLena"];
                rowData["VolTotal"] = dsDatos.Tables["Datos10"].Rows[i]["VolTotal"];
                Ds_PlanManejo.Tables["Dt_Silvicultura"].Rows.Add(rowData);
            }

            ////DataSet11
            for (int i = 0; i < dsDatos.Tables["Datos11"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_ProdNoMaderableExtrae"].NewRow();
                rowData["Turno"] = dsDatos.Tables["Datos11"].Rows[i]["Turno"];
                rowData["Rodal"] = dsDatos.Tables["Datos11"].Rows[i]["Rodal"];
                rowData["Anis"] = dsDatos.Tables["Datos11"].Rows[i]["Anis"];
                rowData["Area"] = dsDatos.Tables["Datos11"].Rows[i]["Area"];
                rowData["Nombre_Producto"] = dsDatos.Tables["Datos11"].Rows[i]["Nombre_Producto"];
                rowData["Peso"] = dsDatos.Tables["Datos11"].Rows[i]["Peso"];
                Ds_PlanManejo.Tables["Dt_ProdNoMaderableExtrae"].Rows.Add(rowData);
            }

            ////DataSet12
            for (int i = 0; i < dsDatos.Tables["Datos12"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_SistemaRepoblacion"].NewRow();
                rowData["Turno"] = dsDatos.Tables["Datos12"].Rows[i]["Turno"];
                rowData["Rodal"] = dsDatos.Tables["Datos12"].Rows[i]["Rodal"];
                rowData["Anis"] = dsDatos.Tables["Datos12"].Rows[i]["Anis"];
                rowData["Area"] = dsDatos.Tables["Datos12"].Rows[i]["Area"];
                rowData["Nombre_Cientifico"] = dsDatos.Tables["Datos12"].Rows[i]["Nombre_Cientifico"];
                rowData["Densidad"] = dsDatos.Tables["Datos12"].Rows[i]["Densidad"];
                rowData["SistemaRepoblacion"] = dsDatos.Tables["Datos12"].Rows[i]["SistemaRepoblacion"];

                rowData["Etapa"] = dsDatos.Tables["Datos12"].Rows[i]["Etapa"];
                rowData["Tratamiento"] = dsDatos.Tables["Datos12"].Rows[i]["Tratamiento"];
                rowData["Descripcion"] = dsDatos.Tables["Datos12"].Rows[i]["Descripcion"];
                rowData["TiempoEje"] = dsDatos.Tables["Datos12"].Rows[i]["TiempoEje"];
                rowData["OtrasActividades"] = dsDatos.Tables["Datos12"].Rows[i]["OtrasActividades"];

                Ds_PlanManejo.Tables["Dt_SistemaRepoblacion"].Rows.Add(rowData);
            }


            ////DataSet13
            for (int i = 0; i < dsDatos.Tables["Datos13"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_ProteccionForestal"].NewRow();
                rowData["Medida_Prevencion"] = dsDatos.Tables["Datos13"].Rows[i]["Medida_Prevencion"];
                rowData["Prevencion_ControlPlagas"] = dsDatos.Tables["Datos13"].Rows[i]["Prevencion_ControlPlagas"];
                rowData["Justificacion_PrevencionPF"] = dsDatos.Tables["Datos13"].Rows[i]["Justificacion_PrevencionPF"];
                rowData["Linea_Control_Ronda"] = dsDatos.Tables["Datos13"].Rows[i]["Linea_Control_Ronda"];
                rowData["Vigilancia_Puesto_Control"] = dsDatos.Tables["Datos13"].Rows[i]["Vigilancia_Puesto_Control"];
                rowData["Manejo_Combustibles"] = dsDatos.Tables["Datos13"].Rows[i]["Manejo_Combustibles"];
                rowData["Identificacion_Area_Critica"] = dsDatos.Tables["Datos13"].Rows[i]["Identificacion_Area_Critica"];
                rowData["Respuesta_CasoIF"] = dsDatos.Tables["Datos13"].Rows[i]["Respuesta_CasoIF"];
                rowData["Justificacion_PrevencionIF"] = dsDatos.Tables["Datos13"].Rows[i]["Justificacion_PrevencionIF"];
                rowData["Monitoreo_Plaga_Forestal"] = dsDatos.Tables["Datos13"].Rows[i]["Monitoreo_Plaga_Forestal"];
                rowData["Control_Plaga_Forestal"] = dsDatos.Tables["Datos13"].Rows[i]["Control_Plaga_Forestal"];
                rowData["Ampliacion_Ronda"] = dsDatos.Tables["Datos13"].Rows[i]["Ampliacion_Ronda"];
                rowData["Ronda_Cortafuego_Intermedia"] = dsDatos.Tables["Datos13"].Rows[i]["Ronda_Cortafuego_Intermedia"];
                rowData["Brigada_Incencio"] = dsDatos.Tables["Datos13"].Rows[i]["Brigada_Incencio"];
                rowData["Identificacion_RutaEscape"] = dsDatos.Tables["Datos13"].Rows[i]["Identificacion_RutaEscape"];
                rowData["Proteccion_FuenteAgua"] = dsDatos.Tables["Datos13"].Rows[i]["Proteccion_FuenteAgua"];
                rowData["Proteccion_Otros_Factores"] = dsDatos.Tables["Datos13"].Rows[i]["Proteccion_Otros_Factores"];
                Ds_PlanManejo.Tables["Dt_ProteccionForestal"].Rows.Add(rowData);
            }

            ////DataSet14
            for (int i = 0; i < dsDatos.Tables["Datos14"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_Cronograma"].NewRow();
                rowData["Actividad"] = dsDatos.Tables["Datos14"].Rows[i]["Actividad"];
                rowData["Fec_Ini"] = dsDatos.Tables["Datos14"].Rows[i]["Fec_Ini"];
                rowData["Fec_Fin"] = dsDatos.Tables["Datos14"].Rows[i]["Fec_Fin"];
                
                Ds_PlanManejo.Tables["Dt_Cronograma"].Rows.Add(rowData);
            }

            ////DataSet15
            for (int i = 0; i < dsDatos.Tables["Datos15"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["DtPropietarios"].NewRow();
                rowData["Nombre"] = dsDatos.Tables["Datos15"].Rows[i]["Propietario"];
                rowData["TipoIdentificacion"] = dsDatos.Tables["Datos15"].Rows[i]["TipoIdPropietario"];
                rowData["IdIdentificacion"] = dsDatos.Tables["Datos15"].Rows[i]["DPI"];

                Ds_PlanManejo.Tables["DtPropietarios"].Rows.Add(rowData);
            }

            ////DataSet16
            for (int i = 0; i < dsDatos.Tables["Datos16"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["Dt_PropEmpresa"].NewRow();
                rowData["NombreEmpresa"] = dsDatos.Tables["Datos16"].Rows[i]["Nombre_empresa"];
                Ds_PlanManejo.Tables["Dt_PropEmpresa"].Rows.Add(rowData);
            }


            ////DataSet17 Datos calculos Repblacion
            for (int i = 0; i < dsDatos.Tables["Datos17"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["DtCalculoRepoblacion"].NewRow();
                if (dsDatos.Tables["Datos17"].Rows[i]["AreaBasal_Extrae"] == "")
                    rowData["AreaBasal_Extrae"] = "";
                else
                    rowData["AreaBasal_Extrae"] = dsDatos.Tables["Datos17"].Rows[i]["AreaBasal_Extrae"];
                if (dsDatos.Tables["Datos17"].Rows[i]["AreaBasalExistente"] == "")
                    rowData["AreaBasalExistente"] = "";
                else
                    rowData["AreaBasalExistente"] = dsDatos.Tables["Datos17"].Rows[i]["AreaBasalExistente"];
                if (dsDatos.Tables["Datos17"].Rows[i]["AreaTotalIntervenir"] == "")
                    rowData["AreaTotalIntervenir"] = "";
                else
                    rowData["AreaTotalIntervenir"] = dsDatos.Tables["Datos17"].Rows[i]["AreaTotalIntervenir"];
                if (dsDatos.Tables["Datos17"].Rows[i]["AreaCompromiso"] == "")
                    rowData["AreaCompromiso"] = "";
                else
                    rowData["AreaCompromiso"] = dsDatos.Tables["Datos17"].Rows[i]["AreaCompromiso"];
                Ds_PlanManejo.Tables["DtCalculoRepoblacion"].Rows.Add(rowData);
            }

            ////DataSet18 Analisis MUestre0
            for (int i = 0; i < dsDatos.Tables["Datos18"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["DtAnalisisMuestreo"].NewRow();
                rowData["Rodal"] = dsDatos.Tables["Datos18"].Rows[i]["Rodal"];
                rowData["Area"] = dsDatos.Tables["Datos18"].Rows[i]["Area"];
                rowData["Parcela"] = dsDatos.Tables["Datos18"].Rows[i]["Parcela"];
                rowData["Volha"] = dsDatos.Tables["Datos18"].Rows[i]["Volaha"];
                rowData["MediaVolParcela"] = dsDatos.Tables["Datos18"].Rows[i]["MediaVolParcela"];
                rowData["DesvEst"] = dsDatos.Tables["Datos18"].Rows[i]["DesviacionEstandard"];
                rowData["CoeficienteVar"] = dsDatos.Tables["Datos18"].Rows[i]["CoeficienteVariacion"];
                rowData["ErrEstandard"] = dsDatos.Tables["Datos18"].Rows[i]["ErrorEstandardMedia"];
                rowData["ErrMuestreo"] = dsDatos.Tables["Datos18"].Rows[i]["ErrorMuestreo"];
                rowData["ErrMuestreoPor"] = dsDatos.Tables["Datos18"].Rows[i]["PorErrorMuestreo"];
                rowData["IntervaloConf"] = dsDatos.Tables["Datos18"].Rows[i]["IntervaloConfianza"];
                Ds_PlanManejo.Tables["DtAnalisisMuestreo"].Rows.Add(rowData);
            }

            ////DataSet19 Descripcion MUestreo
            for (int i = 0; i < dsDatos.Tables["Datos19"].Rows.Count; i++)
            {
                DataRow rowData = Ds_PlanManejo.Tables["DtDescripcionAnalisis"].NewRow();
                rowData["Analisis"] = dsDatos.Tables["Datos19"].Rows[i]["AnaDesc"];
                Ds_PlanManejo.Tables["DtDescripcionAnalisis"].Rows.Add(rowData);
            }
            
            
            dsDatos.Clear();

            //DataSet 18a
           
            DataRow rowAprovechamientoMuestreo = Ds_PlanManejo.Tables["DtAprovechamientoMuestreo"].NewRow();
            rowAprovechamientoMuestreo["FormaParcela"] = GetAprovechamiento_FormaParcela(Id, Origen);
            rowAprovechamientoMuestreo["AreaMuestreada"] = AreaMuestreada;
            rowAprovechamientoMuestreo["TipoMuestreo"] = GetAprovechamiento_TipoMuestreo(Id, Origen);
            rowAprovechamientoMuestreo["IntensidadMuestreo"] = IntensidadMuestreo;
            Ds_PlanManejo.Tables["DtAprovechamientoMuestreo"].Rows.Add(rowAprovechamientoMuestreo);
                        
            
            ////DataSet19a
            DataSet DsFincas = GetFincasPlanManejo(Id, Origen);
            Ds_PlanManejo.Tables["DtBosque"].Clear();
            for (int i = 0; i < DsFincas.Tables["Datos"].Rows.Count; i++)
            {
                DataRow rowBosque = Ds_PlanManejo.Tables["DtBosque"].NewRow();
                rowBosque["TipoBosque"] = GetTipo_BosqueManejo(Id, Convert.ToInt32(DsFincas.Tables["Datos"].Rows[i]["InmuebleId"]), Origen);
                rowBosque["ClaseDesarrollo"] = GetClaseDesarrolloFinca(Id, Convert.ToInt32(DsFincas.Tables["Datos"].Rows[i]["InmuebleId"]), Origen);
                rowBosque["Especies"] = GetEspecies_BosqueManejo(Id, Convert.ToInt32(DsFincas.Tables["Datos"].Rows[i]["InmuebleId"]), Origen);
                rowBosque["Finca"] = DsFincas.Tables["Datos"].Rows[i]["Finca"];
                Ds_PlanManejo.Tables["DtBosque"].Rows.Add(rowBosque);
            }
            DsFincas.Clear();
            return Ds_PlanManejo;

        }

        public DataSet GetFincasPlanManejo(int Id, int Origen)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetFincasPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public string GetTipo_BosqueManejo(int Id, int InmuebleId, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetTipo_BosqueManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value =InmuebleId;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string GetEspecies_BosqueManejo(int Id, int InmuebleId, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetEspecies_BosqueManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string GetClaseDesarrolloFinca(int Id, int InmuebleId, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetClaseDesarrolloFinca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@InmuebleId", OleDbType.Integer).Value = InmuebleId;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public double GetAreaTotalPlanManejo(int Id, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetAreaTotalPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@Resul", OleDbType.Double).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToDouble(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string GetEspecies_Manejar(int Id, int Origen)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetEspecies_Manejar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Origen", OleDbType.Integer).Value = Origen;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Nombre_EncargadoRegionSubRegion(int RegionId, int SubRegionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Nombre_EncargadoRegionSubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RegionId", OleDbType.Integer).Value = RegionId;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string Perfil_EncargadoRegionSubRegion(int RegionId, int SubRegionId)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Perfil_EncargadoRegionSubRegion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@RegionId", OleDbType.Integer).Value = RegionId;
                cmd.Parameters.Add("@SubRegionId", OleDbType.Integer).Value = SubRegionId;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public DataSet Get_Propietarios_PlanManejo(int Tipo, int Id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Propietarios_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public string JuntarPropietariosPlanManejo(int Tipo, int Id)
        {
            string PropietariosAll = "";
            DataSet Propietarios = Get_Propietarios_PlanManejo(Tipo,Id);
            for (int i = 0; i < Propietarios.Tables["Datos"].Rows.Count; i++)
			{
			    if (i == 0)
                    PropietariosAll = Propietarios.Tables["Datos"].Rows[i]["Nombre"].ToString() + " de " + Propietarios.Tables["Datos"].Rows[i]["edad"].ToString() + " años de edad, " + Propietarios.Tables["Datos"].Rows[i]["EstadoCivil"].ToString() + ", " + Propietarios.Tables["Datos"].Rows[i]["Ocupacion"].ToString() + ", de este domicilio, me identifico con " + Propietarios.Tables["Datos"].Rows[i]["TipoIdentificacion"].ToString() + " " + Propietarios.Tables["Datos"].Rows[i]["DPI"].ToString() + ".";
                else
                    PropietariosAll = PropietariosAll + ", " + Propietarios.Tables["Datos"].Rows[i]["Nombre"].ToString() + " de " + Propietarios.Tables["Datos"].Rows[i]["edad"].ToString() + " años de edad, " + Propietarios.Tables["Datos"].Rows[i]["EstadoCivil"].ToString() + ", " + Propietarios.Tables["Datos"].Rows[i]["Ocupacion"].ToString() + ", de este domicilio, me identifico con " + Propietarios.Tables["Datos"].Rows[i]["TipoIdentificacion"].ToString() + " " + Propietarios.Tables["Datos"].Rows[i]["DPI"].ToString() + ".";
			}
            return PropietariosAll;
        }

        public string Get_NotificacionPlanManejo(int Tipo, int Id)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_NotificacionPlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar,5000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }

        }

        public int Get_TipoInmueblePlanManejo(int Tipo, int Id)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_TipoInmueblePlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 5000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return  Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }

        }


        public DataSet Get_Inmuebles_PlanManejo(int Tipo, int Id)
        {
            try
            {
                if (ds.Tables["DATOS"] != null)
                    ds.Tables.Remove("DATOS");
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SP_Get_Inmuebles_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                adp.Fill(ds, "DATOS");
                cn.Close();
                return ds;

            }
            catch (Exception ex)
            {
                cn.Close();
                return ds;
            }
        }

        public string JuntarInmueblesPlanManejo(int Tipo, int Id)
        {
            string InmueblesAll = "";
            DataSet Inmuebles = Get_Inmuebles_PlanManejo(Tipo, Id);
            int TipoInmueblePlanManejo = Get_TipoInmueblePlanManejo(Tipo, Id);
            if (TipoInmueblePlanManejo == 1)
            {
                for (int i = 0; i < Inmuebles.Tables["Datos"].Rows.Count; i++)
                {
                    if (i == 0)
                        InmueblesAll = "número de finca " + Inmuebles.Tables["Datos"].Rows[i]["NoFinca"].ToString() + " folio " + Inmuebles.Tables["Datos"].Rows[i]["Folio"].ToString() + " libro " + Inmuebles.Tables["Datos"].Rows[i]["Libro"].ToString() + " de " + Inmuebles.Tables["Datos"].Rows[i]["De"].ToString() + ", ubicadca en el lugar denominado " + Inmuebles.Tables["Datos"].Rows[i]["Aldea"].ToString() + " del municipio de " + Inmuebles.Tables["Datos"].Rows[i]["municipio"].ToString() + " departamento de " + Inmuebles.Tables["Datos"].Rows[i]["departamento"].ToString();
                    else
                        InmueblesAll = InmueblesAll + ", " + "número de finca " + Inmuebles.Tables["Datos"].Rows[i]["NoFinca"].ToString() + " folio " + Inmuebles.Tables["Datos"].Rows[i]["Folio"].ToString() + " libro " + Inmuebles.Tables["Datos"].Rows[i]["Libro"].ToString() + " de " + Inmuebles.Tables["Datos"].Rows[i]["De"].ToString() + ", ubicadca en el lugar denominado " + Inmuebles.Tables["Datos"].Rows[i]["Aldea"].ToString() + " del municipio de " + Inmuebles.Tables["Datos"].Rows[i]["municipio"].ToString() + " departamento de " + Inmuebles.Tables["Datos"].Rows[i]["departamento"].ToString();
                }
            }
            else if (TipoInmueblePlanManejo == 3)
            {
                for (int i = 0; i < Inmuebles.Tables["Datos"].Rows.Count; i++)
                {
                    if (i == 0)
                        InmueblesAll = "ubicada en el lugar denominado " + Inmuebles.Tables["Datos"].Rows[i]["Direccion"].ToString() + ", " + Inmuebles.Tables["Datos"].Rows[i]["Aldea"].ToString() + " del municipio de " + Inmuebles.Tables["Datos"].Rows[i]["municipio"].ToString() + ", departamento de " + Inmuebles.Tables["Datos"].Rows[i]["departamento"].ToString() + " lo cual acredito con testimonio de escritura pública número " + Inmuebles.Tables["Datos"].Rows[i]["NoEscritura"].ToString() + " autorizada por el notario " + Inmuebles.Tables["Datos"].Rows[i]["notario"].ToString() + " en fecha " + Inmuebles.Tables["Datos"].Rows[i]["Fec_Emision"].ToString();
                    else
                        InmueblesAll = InmueblesAll + ", " + "ubicada en el lugar denominado " + Inmuebles.Tables["Datos"].Rows[i]["Direccion"].ToString() + ", " + Inmuebles.Tables["Datos"].Rows[i]["Aldea"].ToString() + " del municipio de " + Inmuebles.Tables["Datos"].Rows[i]["municipio"].ToString() + ", departamento de " + Inmuebles.Tables["Datos"].Rows[i]["departamento"].ToString() + " lo cual acredito con testimonio de escritura pública número " + Inmuebles.Tables["Datos"].Rows[i]["NoEscritura"].ToString() + " autorizada por el notario " + Inmuebles.Tables["Datos"].Rows[i]["notario"].ToString() + " en fecha " + Inmuebles.Tables["Datos"].Rows[i]["Fec_Emision"].ToString();
                }
            }
            
            return InmueblesAll;
        }

        public string ApartadoInmueblesSolicitdPlanManejo(int Tipo, int Id)
        {
            string Apartado = "";
            int TipoInmueblePlanManejo = Get_TipoInmueblePlanManejo(Tipo,Id);
            if (TipoInmueblePlanManejo == 1)
                Apartado = "Que soy propietario de la(s) finca(s) inscrita(s) en el Registro de la Propiedad bajo el ";
            else if (TipoInmueblePlanManejo == 2)
                Apartado = "1.	Que soy poseedor de la(s) finca(s)  ";
            Apartado = Apartado + JuntarInmueblesPlanManejo(Tipo, Id);
            if (TipoInmueblePlanManejo == 1)
                Apartado = Apartado + " cuyas área medidas y colindancias constan en su primera inscripción de dominio y en las certificación que  extendida por el Registro General de la Propiedad que acompaño.";
            else if (TipoInmueblePlanManejo == 2)
                Apartado = Apartado + "que adjunto al presente.";
            return Apartado;
        }


        public DataSet Impresion_Solicitud(int AsignacionId, int RegionId, int SubRegionId)
        {

            Ds_PlanManejo Ds_Solicitud = new Ds_PlanManejo();
            Ds_Solicitud.Tables["DtSolicitud"].Clear();
            DataRow row = Ds_Solicitud.Tables["DtSolicitud"].NewRow();
            row["Director"] = Nombre_EncargadoRegionSubRegion(RegionId, SubRegionId);
            row["Solicitante"] = JuntarPropietariosPlanManejo(1, AsignacionId) + " Señalo lugar para recibir notificaciones en  " + Get_NotificacionPlanManejo(1, AsignacionId) + ", comparezco ante usted y,";
            row["Propiedades"] = ApartadoInmueblesSolicitdPlanManejo(1, AsignacionId).ToString().Trim();
            row["Tipo"] = Get_TipoInmueblePlanManejo(1, AsignacionId);
            row["Puesto"] = Perfil_EncargadoRegionSubRegion(RegionId, SubRegionId);
            Ds_Solicitud.Tables["DtSolicitud"].Rows.Add(row);

            return Ds_Solicitud;

        }

        public int TieneRepresentantes_PlanManejo(int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_TieneRepresentantes_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Resul"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }

        public string TieneProdNoMaderables_PlanManejo(int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_TieneProdNoMaderables_PlanManejo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public int Get_TipoPropiedadId(int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Get_TipoPropiedadId", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Tipo_PersonaId", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@Tipo_PersonaId"].Value);
            }
            catch (Exception ex)
            {
                cn.Close();
                return 1;
            }
        }


        public DataSet Boleta(int Id, int Origen)
        {
            Cl_Manejo ClManejo;
            ClManejo = new Cl_Manejo();

            Ds_PlanManejo Ds_BoletaPrint = new Ds_PlanManejo();
            DataSet dsDatos = ClManejo.Get_Datos_Boleta(Id, 2);
            Ds_BoletaPrint.Tables["Dt_Censo"].Clear();
            for (int i = 0; i < dsDatos.Tables["Datos"].Rows.Count; i++)
            {
                DataRow row = Ds_BoletaPrint.Tables["Dt_Censo"].NewRow();
                row["Turno"] = 0;
                row["Rodal"] = dsDatos.Tables["Datos"].Rows[i]["Rodal"];
                row["No"] = dsDatos.Tables["Datos"].Rows[i]["No"];
                row["DAP"] = dsDatos.Tables["Datos"].Rows[i]["DAP"];
                row["Altura"] = dsDatos.Tables["Datos"].Rows[i]["Altura"];
                row["NombreCientifico"] = dsDatos.Tables["Datos"].Rows[i]["Nombre_Cientifico"];
                row["Troza"] = dsDatos.Tables["Datos"].Rows[i]["Troza"];
                row["X"] = dsDatos.Tables["Datos"].Rows[i]["X"];
                row["Y"] = dsDatos.Tables["Datos"].Rows[i]["Y"];
                Ds_BoletaPrint.Tables["Dt_Censo"].Rows.Add(row);
            }
            dsDatos.Clear();

            return Ds_BoletaPrint;

        }

        public int GetTipoInventario(int Tipo, int Id)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetTipoInventario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@TipoInv", OleDbType.Integer).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return Convert.ToInt32(cmd.Parameters["@TipoInv"].Value.ToString());
            }
            catch (Exception ex)
            {
                cn.Close();
                return 0;
            }
        }


        public string GetAprovechamiento_FormaParcela(int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetAprovechamiento_FormaParcela", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

        public string GetAprovechamiento_TipoMuestreo(int Id, int Tipo)
        {
            try
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("Sp_GetAprovechamiento_TipoMuestreo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", OleDbType.Integer).Value = Id;
                cmd.Parameters.Add("@tipo", OleDbType.Integer).Value = Tipo;
                cmd.Parameters.Add("@Resul", OleDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                cn.Close();
                return cmd.Parameters["@Resul"].Value.ToString();
            }
            catch (Exception ex)
            {
                cn.Close();
                return "";
            }
        }

    }
}