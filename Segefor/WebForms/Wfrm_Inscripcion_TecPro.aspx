<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Inscripcion_TecPro.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Inscripcion_TecPro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function openRadWin() {
            var width = widthTextBox.get_value();
            var height = heightTextBox.get_value();
            var left = leftTextBox.get_value();
            var top = topTextBox.get_value();
            radopen("http://www.telerik.com", "RadWindow1", width, height, left, top);
        }
    </script>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h2><strong>FORMULARIO PARA INSCRIPCIÓN  DE TÉCNICOS Y PROFESIONALES QUE SE DEDICAN A LA ACTIVIDAD FORESTAL</strong></h2>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Región:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboRegion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Subregión:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboSubRegion" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>I.  TIPO DE ACTIVIDAD FORESTAL</strong></h3>
                            </div>
                        </div>
                        <div class="ibox-content">
                             <div><label class="col-sm-2 control-label centradolabel">Actividad:</label>
                                <div class="col-sm-8"><telerik:RadComboBox ID="CboActividad" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>II.  DESCRIPCION DE DATOS DE LA PERSONA INDIVIDUAL</strong></h3>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Nombres:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Apellidos:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtApellidos" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label runat="server" id="LblTipoId" class="col-sm-2 control-label centradolabel">DPI:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtDpi" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-3 control-label centradolabel">Fecha de Vencimiento:</label>
                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtFecVen" Enabled="false" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">NIT:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNit" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Teléfono:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTelefono" CssClass="form-control" data-mask="9999-9999"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Celular:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtCelular" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label">Correo Electrónico:</label>
                                <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtCorreo" CssClass="form-control" type="email" Enabled="false"></asp:TextBox></div>
                            </div>

                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Dirección:</label>
                                <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtDireccion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtDepartamento" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtMunicipio" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Profesión:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboProfesion" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Grado Académico:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboCategoriaProfesion" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Número de colegiado:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoCol" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Número de Diploma :</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtDiploma" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>III.  DATOS DE NOTIFICACIÓN</strong></h3>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Dirección de notificación:</label>
                                <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtDireccionNotifica" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Aldea, Caserío, Comunidad:</label>
                                <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtAldea" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamento" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipio" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>IV.  OBSERVACIONES GENERALES</strong></h3>
                            </div>
                            <div class="col-sm-12"><asp:TextBox runat="server" ID="TxtObservaciones" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>V.  NOMBRE Y FIRMA</strong></h3>
                            </div>
                            <div class="col-sm-12"><asp:TextBox runat="server" ID="TxtNomFirma" MaxLength="200" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                        <div class="col-sm-5">
                            <asp:Button runat="server" Text="Vista Previa"  ID="BtnVistaPrevia" class="btn btn-primary" />
                            <asp:Button runat="server" Text="Enviar Solicitud"  ID="BtnEnviar" data-loading-text="Enviando..." class="btn btn-primary" />
                        </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                        <div class="col-sm-9">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnEror" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        </div>
                    </div>
                </div>
            </div>
                <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                    <Windows>
                        <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                            Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                        </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function pageLoad() {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
            $('#<%=BtnEnviar.ClientID%>').click(function () {
                $(this).button('loading').delay(100000).queue(function () {
                    $(this).button('reset');
                    $(this).dequeue();
                    $(this).data('loading-text', 'Cargando...');
                });
            });
        }


        </script>
</asp:Content>
