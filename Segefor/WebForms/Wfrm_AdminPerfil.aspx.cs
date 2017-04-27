using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SEGEFOR.Clases;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using System.Text;

namespace SEGEFOR.WebForms
{
    public partial class Wfrm_AdminPerfil : System.Web.UI.Page
    {
        Cl_Utilitarios ClUtilitarios;
        Cl_Usuario ClUsuario;
        Cl_Registro ClRegistro;
        Cl_Persona_Juridica ClPersonaJuridica;
        Cl_Persona ClPersona;
        Cl_Catalogos ClCatalogos;

        DataSet ds = new DataSet();
        DataSet dsDatosEspe = new DataSet();
        DataSet dsDatosRegistro = new DataSet();
        DataSet dsPersona = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            ClUsuario = new Cl_Usuario();
            ClUtilitarios = new Cl_Utilitarios();
            ClRegistro = new Cl_Registro();
            ClPersonaJuridica = new Cl_Persona_Juridica();
            ClPersona = new Cl_Persona();
            ClCatalogos = new Cl_Catalogos();

            BtnModDatos.Click += BtnModDatos_Click;
            BtnModDatosEspe.Click += BtnModDatosEspe_Click;
            BtnNuevoJuridico.Click += BtnNuevoJuridico_Click;
            GrdRegistros.NeedDataSource += GrdRegistros_NeedDataSource;
            GrdJuridico.NeedDataSource += GrdJuridico_NeedDataSource;
            CboTipo.TextChanged += CboTipo_TextChanged;
            BtnGrabarJur.Click += BtnGrabarJur_Click;
            GrdJuridico.ItemCommand += GrdJuridico_ItemCommand;
            GrdRegistros.ItemCommand += GrdRegistros_ItemCommand;
            GrdRegistros.ItemDataBound += GrdRegistros_ItemDataBound;
            BtnModFoto.Click += BtnModFoto_Click;
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("~/Wfrm_Login.aspx");
            }
            else if (!IsPostBack)
            {
                ClUsuario.Insertar_Ingreso_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_IngresoPagina());
                ClUtilitarios.Permisos(Master, Convert.ToInt32(Session["UsuarioId"]));
                System.Web.UI.WebControls.Label LblPerfil;
                LblPerfil = (System.Web.UI.WebControls.Label)Master.FindControl("LblTipoUsuario");
                LblPerfil.Text = ClUsuario.Get_Tipo_Usuario(Convert.ToInt32(Session["UsuarioId"]));

                System.Web.UI.WebControls.Label LblUsuario;
                LblUsuario = (System.Web.UI.WebControls.Label)Master.FindControl("lblUsuario");
                LblUsuario.Text = ClPersona.Nombre_Usuario(Convert.ToInt32(Session["PersonaId"]));

                DataSet dsPermisos = ClUsuario.Get_Roles(Convert.ToInt32(Session["UsuarioId"]), 14);
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Editar"]) == 0)
                {
                    BtnModDatos.Visible = false;
                    GrdRegistros.Columns[5].Visible = false;
                    GrdJuridico.Columns[4].Visible = false;
                    GrdJuridico.Columns[5].Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Insertar"]) == 0)
                {
                    BtnNuevoJuridico.Visible = false;
                }
                if (Convert.ToInt32(dsPermisos.Tables["Datos"].Rows[0]["Eliminar"]) == 0)
                {

                }
                dsPermisos.Clear();
                
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoGrado_Academico(), CboGrado, "GradoAcademicoid", "GradoAcademico");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoEtnia(), CboEtnia, "Etniaid", "Etnia");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoGrupoLin(), CboGrupo, "Grupo_LinguisticoId", "GrupoLinguistico");
                ClUtilitarios.LlenaCombo(ClCatalogos.ListadoOcupacion(),CboOcupacion, "OcupacionID", "Ocupacion");
                ClUtilitarios.LlenaCombo(ClCatalogos.Tipo_Juridico_Get(1), CboTipo, "Tipo_JuridicoId", "Tipo_Juridico");
                
                CboEtnia.Items.Insert(0, "");
                CboGrado.Items.Insert(0, "");
                CboGrupo.Items.Insert(0, "");
                CboOcupacion.Items.Insert(0, "");
                CboTipo.Items.Insert(0, "");
                CargaDatos();
                TxtFecRepre.MinDate = new DateTime(1920, 1, 1);
                TxtFecRepre.SelectedDate = DateTime.Now;
                //TxtFecVenId.MinDate = DateTime.Now;
            }
            System.Web.UI.HtmlControls.HtmlImage ImgPerfilFake;
            System.Web.UI.WebControls.Image ImgPerfil;
            ImgPerfilFake = (System.Web.UI.HtmlControls.HtmlImage)Master.FindControl("ImgPerfilFake");
            ImgPerfil = (System.Web.UI.WebControls.Image)Master.FindControl("ImgPerfil");

            if (ClPersona.Existe_FotoPerfil(Convert.ToInt32(Session["PersonaId"])) == true)
            {
                ImgPerfilFake.Visible = false;
                ImgPerfil.Visible = true;
            }
            else
            {
                ImgPerfilFake.Visible = true;
                ImgPerfil.Visible = false;
                if (ClPersona.Genero_Usuario(Convert.ToInt32(Session["PersonaId"])) == 1)
                    ImgPerfilFake.Src = "../imagenes/male.png";

                else
                    ImgPerfilFake.Src = "../imagenes/female.png";
            }
        }

        bool ValidarNit(string nit)
        {
            bool Valido = true;
            for (int i = 0; i < nit.Length; i++)
            {
                if (nit[i].ToString() == "-")
                    Valido = true;
                else
                    Valido = false;
            }
            return Valido;
        }

        void BtnModFoto_Click(object sender, EventArgs e)
        {
            ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 2);
            if (UploadFoto.UploadedFiles.Count > 0)
            {
                Stream fileStream = UploadFoto.UploadedFiles[0].InputStream;
                byte[] attachmentBytes = new byte[fileStream.Length];
                fileStream.Read(attachmentBytes, 0, Convert.ToInt32(fileStream.Length));
                ClPersona.Actualiza_Foto(Convert.ToInt32(Session["PersonaId"]), attachmentBytes, UploadFoto.UploadedFiles[0].ContentType, UploadFoto.UploadedFiles[0].FileName);
                fileStream.Close();
            }
            LblMensajeGodDatGen.Text = "Foto actualizada exitosamente";
            Btnsuccesdatgen.Visible = true;
            StringBuilder sbScript = new StringBuilder();

            sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
            sbScript.Append("<!--\n");
            sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
            sbScript.Append("// -->\n");
            sbScript.Append("</script>\n");

            this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
        }

        void GrdRegistros_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                if (dataItem["FechaAdd"].Text == "&nbsp;")
                    dataItem["Add"].FindControl("ImgAgregar").Visible = true;
                else
                    dataItem["Add"].FindControl("ImgAgregar").Visible = false;
            }
        }

        void GrdRegistros_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            
            if (e.CommandName == "CmdAdd")
            {
                if (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Estado"].ToString() == "Activo")
                {
                    ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 4);
                    ClRegistro.Insertar_Relacion_Registro(Convert.ToInt32(Session["PersonaId"]), Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RegistroId"].ToString()));
                    GrdRegistros.Rebind();
                    BtnBadRegistro.Visible = true;
                    LblBadRegistro.Text = "Registro agregado exitosamente";
                    GrdRegistros.Rebind();
                }
                else
                {
                    BtnGoodRegistro.Visible = true;
                    LblGoodRegistro.Text = "Solamente se pueden agregar registros activos";
                }

            }
        }

        void GrdJuridico_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            BtnGoodJuridico.Visible = false;
            LblGoodJuridico.Text = "";
            if (e.CommandName == "CmdEdit")
            {
                TxtEstadoJur.Text = "2";
                TxtPersonaJuridicaId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PersonaJuridicaId"].ToString();
                ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 2);
                ds = ClPersonaJuridica.Persona_Juridica_Get_CorrJur(Convert.ToInt32(TxtPersonaJuridicaId.Text));
                CboTipo.SelectedValue = ds.Tables["DATOS"].Rows[0]["Tipo_JuridicoId"].ToString();
                CboTipo.SelectedItem.Text = ds.Tables["DATOS"].Rows[0]["Tipo_Juridico"].ToString();
                TxtNombreJur.Text = ds.Tables["DATOS"].Rows[0]["Nombre"].ToString();
                TxtActa.Text = ds.Tables["DATOS"].Rows[0]["No_Acta"].ToString();
                TxtNumero.Text = ds.Tables["DATOS"].Rows[0]["Numero"].ToString();
                TxtNumeroHide.Text = ds.Tables["DATOS"].Rows[0]["Numero"].ToString();
                TxtFolio.Text = ds.Tables["DATOS"].Rows[0]["Folio"].ToString();
                TxtLibro.Text = ds.Tables["DATOS"].Rows[0]["Libro"].ToString();
                TxtNitJur.Text = ds.Tables["DATOS"].Rows[0]["Nit"].ToString();
                DivJuridico.Visible = true;
                if (CboTipo.SelectedValue == "10")
                    DivActa.Visible = true;
                else
                    DivActa.Visible = false;
                if (CboTipo.SelectedValue != "7")
                    TxtFecRepre.SelectedDate = Convert.ToDateTime(ds.Tables["DATOS"].Rows[0]["Fecha"]);
                else
                    TxtFecRepre.Clear();
            }
            else if (e.CommandName == "CmdDel")
            {
                ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 3);
                ClPersonaJuridica.Persona_Juridica_Delete(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PersonaJuridicaId"]));
                GrdJuridico.Rebind();
                TxtEstadoJur.Text = "1";
                DivJuridico.Visible = false;
                DivActa.Visible = false;
                BtnGoodJuridico.Visible = true;
                LblGoodJuridico.Text = "Persona Jurídica eliminada";
            }
        }

        void BtnGrabarJur_Click(object sender, EventArgs e)
        {
            BtnGoodJuridico.Visible =false;
            LblGoodJuridico.Text = "";
            if (ValidaJur() == true)
            {
                if (TxtEstadoJur.Text == "1")
                {
                    ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 4);
                    ClPersonaJuridica.Insertar_Persona_Juridica(ClPersonaJuridica.Max_Persona_Juridica(), Convert.ToInt32(Session["UsuarioId"]), Convert.ToInt32(CboTipo.SelectedValue), TxtNombreJur.Text, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecRepre.SelectedDate)), TxtActa.Text, Convert.ToInt32(TxtNumero.Text), Convert.ToInt32(TxtFolio.Text), Convert.ToInt32(TxtLibro.Text), TxtNitJur.Text);
                    GrdJuridico.Rebind();
                    DivActa.Visible = false;
                    DivJuridico.Visible = false;
                    Limpiar();
                    BtnGoodJuridico.Visible = true;
                    LblGoodJuridico.Text = "Persona Jurídica agregada correctamente";
                }
                else
                {
                    ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 2);
                    ClPersonaJuridica.Actualiza_Persona_Juridica(Convert.ToInt32(TxtPersonaJuridicaId.Text), Convert.ToInt32(CboTipo.SelectedValue), TxtNombreJur.Text, Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecRepre.SelectedDate)), TxtActa.Text, Convert.ToInt32(TxtNumero.Text), Convert.ToInt32(TxtFolio.Text), Convert.ToInt32(TxtLibro.Text), TxtNitJur.Text);
                    GrdJuridico.Rebind();
                    DivActa.Visible = false;
                    DivJuridico.Visible = false;
                    Limpiar();
                    BtnGoodJuridico.Visible = true;
                    LblGoodJuridico.Text = "Persona Jurídica modificada correctamente";
                }
                
            }
        }

        void Limpiar()
        {
            CboTipo.Text = "";
            TxtNombreJur.Text = "";
            TxtFecRepre.SelectedDate = DateTime.Now;
            TxtActa.Text = "";
            TxtNumero.Text = "";
            TxtFolio.Text = "";
            TxtLibro.Text = "";
            TxtNit.Text = "";
        }

        bool ValidaJur()
        {
            bool HayError = false;
            LblBadJuridico.Text = "";
            BtnBadJuridico.Visible = false;
            if (CboTipo.SelectedItem.Text == "")
            {
                if (LblBadJuridico.Text == "")
                    LblBadJuridico.Text = LblBadJuridico.Text + "Debe seleccionar el tipo juridico";
                else
                    LblBadJuridico.Text = LblBadJuridico.Text + ", Debe seleccionar el tipo juridico";
                HayError = true;
            }
            if (CboTipo.SelectedValue == "10" && TxtActa.Text == "")
            {
                if (LblBadJuridico.Text == "")
                    LblBadJuridico.Text = LblBadJuridico.Text + "Debe Ingresar el número de acta de poseción";
                else
                    LblBadJuridico.Text = LblBadJuridico.Text + ", Debe Ingresar el número de acta de poseción";
                HayError = true;
            }
            
            if (EsMayor10Anis() == true)
            {
                if (LblBadJuridico.Text == "")
                    LblBadJuridico.Text = LblBadJuridico.Text + "La Fecha de representación legal no puede ser mayor a 10 años";
                else
                    LblBadJuridico.Text = LblBadJuridico.Text + ", La Fecha de representación legal no puede ser mayor a 10 años";
                HayError = true;
            }
            if (Convert.ToDateTime(TxtFecRepre.SelectedDate.Value.ToString("dd/MM/yyyy")) < Convert.ToDateTime(ClUtilitarios.FechaDB().ToString("dd/MM/yyyy")))
            {
                if (LblBadJuridico.Text == "")
                    LblBadJuridico.Text = LblBadJuridico.Text + "La fecha de representación no puede ser anterior al día de hoy";
                else
                    LblBadJuridico.Text = LblBadJuridico.Text + ", La fecha de representación no puede ser anterior al día de hoy";
                HayError = true;
            }
            if (ClUtilitarios.TieneGuion(TxtNitJur.Text) != true)
            {
                if (LblBadJuridico.Text == "")
                    LblBadJuridico.Text = LblBadJuridico.Text + "El Nit no tiene el formato correcto";
                else
                    LblBadJuridico.Text = LblBadJuridico.Text + ", El Nit no tiene el formato correcto";
                HayError = true;
            }
            if ((TxtNumero.Text != TxtNumeroHide.Text) && (ClPersonaJuridica.Existe_NumeroJuridico(Convert.ToInt32(TxtNumero.Text))))
            {
                if (LblBadJuridico.Text == "")
                    LblBadJuridico.Text = LblBadJuridico.Text + "El número de registro mercantil ya existe en nuestros registros";
                else
                    LblBadJuridico.Text = LblBadJuridico.Text + ", El número de registro mercantil ya existe en nuestros registros";
                HayError = true;
            }
            if (HayError == true)
            {
                BtnBadJuridico.Visible = true;
                return false;
            }
            else
                return true;
        }

        int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        bool EsMayor10Anis()
        {
            DateTime fecha1 = Convert.ToDateTime(DateTime.Now);
            DateTime fecha2 = Convert.ToDateTime(TxtFecRepre.SelectedDate);
            int CntAnis = Years(fecha1, fecha2);
            if (CntAnis > 10)
                return true;
            else
                return false;
        }

        void CboTipo_TextChanged(object sender, EventArgs e)
        {
            if (CboTipo.Text != "")
            {
                LblTipo.InnerText = "Nombre: " + CboTipo.SelectedItem.Text;
            }
            if (CboTipo.SelectedValue == "10")
                DivActa.Visible = true;
            else
                DivActa.Visible = false;
        }

        void GrdJuridico_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //ClUtilitarios.LlenaGrid(ClPersonaJuridica.Persona_Juridica_Get(Convert.ToInt32(Session["UsuarioId"])), GrdJuridico);
        }

        void BtnNuevoJuridico_Click(object sender, EventArgs e)
        {
            TxtEstadoJur.Text = "1";
            DivJuridico.Visible = true;
        }

        void GrdRegistros_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClUtilitarios.LlenaGrid(ClRegistro.Get_Categoria_Registro_Usuario(Convert.ToInt32(Session["PersonaId"])), GrdRegistros);
            DataSet dsDatos = ClRegistro.Get_Categoria_Registro_Usuario(Convert.ToInt32(Session["PersonaId"]));
            if (dsDatos.Tables[0].Rows.Count == 0)
                DivRnf.Visible = false;
        }

        bool ValidaDatosEspe()
        {
            LblBadDatosEsp.Text = "";
            BtnBadDatEspe.Visible = false;
            BtnGoodDatosEspe.Visible = false;
            LblGoodDatosEspe.Text = "";
            bool HayError = false;
            if ((TxtNit.Enabled != false) && (TxtNit.Text != "") && (ClPersona.Existe_Nit(TxtNit.Text)))
            {
                if (LblBadDatosEsp.Text == "")
                    LblBadDatosEsp.Text = LblBadDatosEsp.Text + "Nit incorrecto, favor de comunicarse con la oficina del INAB correspondiente a su localidad.";
                else
                    LblBadDatosEsp.Text = LblBadDatosEsp.Text + ", Nit incorrecto, favor de comunicarse con la oficina del INAB correspondiente a su localidad.";
                HayError = true;
            }
            if ((TxtNit.Text != "") && (ClUtilitarios.TieneGuion(TxtNit.Text) != true))
            {
                if (LblBadDatosEsp.Text == "")
                    LblBadDatosEsp.Text = LblBadDatosEsp.Text + "El Nit no tiene el formato correcto";
                else
                    LblBadDatosEsp.Text = LblBadDatosEsp.Text + ", El Nit no tiene el formato correcto";
                HayError = true;
            }
            
            if (HayError == true)
            {
                BtnBadDatEspe.Visible = true;
                return false;
            }

            else
                return true;
        }

        void BtnModDatosEspe_Click(object sender, EventArgs e)
        {
            if (ValidaDatosEspe() == true)
            {
                LblGoodDatosEspe.Text = "";
                BtnGoodDatosEspe.Visible = false;
                int Grado_Academico = 0;
                string NIT = "";
                int EtniaId = 0;
                int Grupo_LinId = 0;
                int OcupacionId = 0;
                if (CboGrado.Text != "")
                    Grado_Academico = Convert.ToInt32(CboGrado.SelectedValue);
                if (TxtNit.Text != "")
                    NIT = TxtNit.Text;
                if (CboEtnia.Text != "")
                    EtniaId = Convert.ToInt32(CboEtnia.SelectedValue);
                if (CboGrupo.Text != "")
                    Grupo_LinId = Convert.ToInt32(CboGrupo.SelectedValue);
                if (CboOcupacion.Text != "")
                    OcupacionId = Convert.ToInt32(CboOcupacion.SelectedValue);
                ClUsuario.Insertar_Actividad_Pagina(14, Convert.ToInt32(Session["UsuarioId"]), ClUsuario.CorrId_ActividadPagina(), 2);
                ClPersona.Actualiza_DatosEspecificos(Convert.ToInt32(Session["PersonaId"]), Grado_Academico, NIT, EtniaId, Grupo_LinId, OcupacionId);
                LblGoodDatosEspe.Text = "Datos especificos modificados con éxito";
                BtnGoodDatosEspe.Visible = true;
                
                if (TxtNit.Text != "")
                    TxtNit.Enabled = false;
            }
        }

        void BtnModDatos_Click(object sender, EventArgs e)
        {
            Btnsuccesdatgen.Visible = false;
            LblMensajeGodDatGen.Text = "";
            if (ValidaDatosGen() == true)
            {
                ClUsuario.Actualiza_DatosUsuario(Convert.ToInt32(Session["UsuarioId"]), TxtUsuario.Text, TxtCorreo.Text, Convert.ToInt32(Session["TipoUsuarioId"]), Convert.ToDateTime(string.Format("{0:dd/MM/yyyy}", TxtFecVenId.SelectedDate)),Convert.ToInt32(Session["PersonaId"]),1);
                LblMensajeGodDatGen.Text = "Datos Generales actualizados exitosamente";
                Btnsuccesdatgen.Visible = true;   
            }
        }

        bool ValidaDatosGen()
        {
            LblMensaje.Text = "";
            Btnbaddatgen.Visible = false;
            bool HayError = false;
            if (TxtUsuario.Text != TxtUsuarioHide.Text)
            {
                if (ClUsuario.Existe_Usuario(TxtUsuario.Text))
                {
                    if (LblMensaje.Text == "")
                        LblMensaje.Text = LblMensaje.Text + "El usuario ya esta siendo utilizado";
                    else
                        LblMensaje.Text = LblMensaje.Text + ", El usuario ya esta siendo utilizado";
                    HayError = true;
                }
            }
            if (TxtCorreo.Text != TxtCorreoHide.Text)
            {
                if (ClUsuario.Existe_Correo(TxtCorreo.Text))
                {
                    if (LblMensaje.Text == "")
                        LblMensaje.Text = LblMensaje.Text + "El correo ya esta siendo utilizado";
                    else
                        LblMensaje.Text = LblMensaje.Text + ", El correo ya esta siendo utilizado";
                    HayError = true;
                }
            }
            if (TxtFecVenId.DateInput.SelectedDate < DateTime.Now)
            {
                if (LblMensaje.Text == "")
                    LblMensaje.Text = LblMensaje.Text + "Documento De Identificación Vencido";
                else
                    LblMensaje.Text = LblMensaje.Text + ", documento De Identificación Vencido";
                HayError = true;
            }
            if (HayError == true)
            {
                Btnbaddatgen.Visible = true;
                return false;
            }

            else
                return true;
        }

        void CargaDatos()
        {
            ds = ClUsuario.Datos_UsuarioId(Convert.ToInt32(Session["UsuarioId"]));
            dsPersona = ClPersona.Datos_Persona(Convert.ToInt32(Session["PersonaId"]));
            TxtNombre.Text = dsPersona.Tables["DATOS"].Rows[0]["Nombres"].ToString();
            TxtApellido.Text = dsPersona.Tables["DATOS"].Rows[0]["Apellidos"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["Origen_PersonaId"].ToString() == "2")
                TxtDpi.Text = dsPersona.Tables["DATOS"].Rows[0]["dpi"].ToString();
            else
            {
                if (dsPersona.Tables["DATOS"].Rows[0]["dpi"].ToString().Length == 13)
                    TxtDpi.Text = dsPersona.Tables["DATOS"].Rows[0]["dpi1"].ToString() + "-" + dsPersona.Tables["DATOS"].Rows[0]["dpi2"].ToString() + "-" + dsPersona.Tables["DATOS"].Rows[0]["dpi3"].ToString();

                else
                    TxtDpi.Text = "";
            }
            
            
            TxtFecNac.Text = dsPersona.Tables["DATOS"].Rows[0]["fechanac"].ToString();
            TxtGenero.Text = dsPersona.Tables["DATOS"].Rows[0]["genero"].ToString();
            TxtUsuario.Text = ds.Tables["DATOS"].Rows[0]["Usuario"].ToString();
            TxtCorreo.Text = ds.Tables["DATOS"].Rows[0]["Correo"].ToString();
            TxtUsuarioHide.Text = ds.Tables["DATOS"].Rows[0]["Usuario"].ToString();
            TxtCorreoHide.Text = ds.Tables["DATOS"].Rows[0]["Correo"].ToString();
            TxtDpiCompleto.Text = dsPersona.Tables["DATOS"].Rows[0]["Dpi"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["GradoAcademicoId"].ToString() != "")
                CboGrado.SelectedValue = dsPersona.Tables["DATOS"].Rows[0]["GradoAcademicoId"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["EtniaId"].ToString() != "")
                CboEtnia.SelectedValue = dsPersona.Tables["DATOS"].Rows[0]["EtniaId"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["Grupo_LinguisticoId"].ToString() != "")
                CboGrupo.SelectedValue = dsPersona.Tables["DATOS"].Rows[0]["Grupo_LinguisticoId"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["OcupacionId"].ToString() != "")
                CboOcupacion.SelectedValue = dsPersona.Tables["DATOS"].Rows[0]["OcupacionId"].ToString();
            if (dsPersona.Tables["DATOS"].Rows[0]["Nit"].ToString() != "")
            {
                TxtNit.Text = dsPersona.Tables["DATOS"].Rows[0]["Nit"].ToString();
                TxtNit.Enabled = false;
            }
            if (dsPersona.Tables["DATOS"].Rows[0]["Origen_PersonaId"].ToString() == "1")
            {
                lblTipoId.InnerText = "DPI:";
                LblFecVen.InnerText = "Fecha de Vencimiento DPI:";
            }
            else
            {
                lblTipoId.InnerText = "Número de Pasaporte:";
                LblFecVen.InnerText = "Fecha de Vencimiento de Pasaporte:";
            }
            if (dsPersona.Tables["DATOS"].Rows[0]["FecVenId"].ToString() == "")
                TxtFecVenId.DateInput.Text = "";
            else
                TxtFecVenId.SelectedDate = Convert.ToDateTime(dsPersona.Tables["DATOS"].Rows[0]["FecVenId"]);
        }
    }
}