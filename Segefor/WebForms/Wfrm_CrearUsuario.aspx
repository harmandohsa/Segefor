<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_CrearUsuario.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_CrearUsuario" %>
<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Toastr style -->
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h2><strong>Creación de usuario</strong></h2>
                    </div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Nombres:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">Apellidos:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtApellido" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label">Correo Electrónico:</label>
                            <div class="col-sm-11"><asp:TextBox runat="server" ID="TxtCorreo" CssClass="form-control" type="email" required=""></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label">Vuelva a escribir su correo Electrónico:</label>
                            <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtCorreoVal" CssClass="form-control" type="email" required=""></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Fecha de Nacimiento:</label>
                            <div class="col-sm-4">
                                <telerik:RadDatePicker ID="TxtFecNac" Width="100%" runat="server"></telerik:RadDatePicker>
                            </div>
                            
                        </div>
                        
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Tipo de Identificación:</label>
                            <div class="col-sm-4">
                                <telerik:RadComboBox ID="CboTipoIdentificacion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox>
                            </div>
                            
                        </div>
                        <div runat="server" id="DivDpi" visible="false"><label class="col-sm-2 control-label">Documento Personal de Identificación DPI:</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" Text="" ID="TxtDpi" class="form-control" data-mask="9999-99999-9999" placeholder="" required=""></asp:TextBox>
                            </div>
                        </div>
                        <div runat="server" id="DivPasaporte" visible="false"><label class="col-sm-2 control-label">Número de Pasaporte:</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" Text="" ID="TxtPasaporte" class="form-control" required=""></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Fecha de Vencimiento:</label>
                            <div class="col-sm-4">
                                <telerik:RadDatePicker ID="TxtFecVenId" Width="100%" runat="server"></telerik:RadDatePicker>
                            </div>
                            
                        </div>
                        <div runat="server" id="DivPais" visible="false"><label class="col-sm-2 control-label">País de Origen:</label>
                            <div class="col-sm-4">
                                <telerik:RadComboBox ID="CboPais" Width="100%" runat="server"></telerik:RadComboBox>
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Genero:</label>
                            <div class="col-sm-5">
                                <telerik:RadComboBox ID="CboGenero" Width="100%" runat="server"></telerik:RadComboBox>
                                 
                            </div>
                            
                        </div>
                        <div><label class="col-sm-2 control-label">Número de Teléfono Celular:</label>
                            <div class="col-sm-4">
                                <asp:TextBox runat="server" Text="" ID="TxtCelular" class="form-control" data-mask="9999-9999"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label runat="server" id="LblDireccion" class="col-sm-1 control-label centradolabel">Dirección:</label>
                            <div class="col-sm-11"><asp:TextBox runat="server" ID="TxtDireccion" CssClass="form-control" required></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                            <div class="col-sm-4"><telerik:RadComboBox ID="CboDep" AutoPostBack="true" Width="100%" runat="server" required></telerik:RadComboBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">Muncipio:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboMun" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Escribe los caracteres que veas:</label>
                            <div class="col-sm-10"><telerik:RadCaptcha ID="CapValidate" runat="server" CaptchaImage-TextChars="LettersAndNumbers" CaptchaImage-EnableAudioNoise="true" CaptchaTextBoxLabel="" CaptchaLinkButtonText="Refrescar"   EnableRefreshImage="true"></telerik:RadCaptcha></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel"></label>
                            <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" ID="ChkAcepto" Text="He leído y acepto los términos y condiciones de  Uso " />
                                <asp:LinkButton runat="server" Text="(Leer Términos y Condiciones de Uso)" ID="LnkTerminos"></asp:LinkButton> 
                            </div>
                            
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel"></label>
                            <div class="col-sm-2"></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div class="col-sm-2"><asp:Button runat="server" Text="Crear Usuario"  ID="BtnCrearUsuario" class="btn btn-primary" />
                        </div>
                        <div class="col-sm-9">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnEror" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                            </div>
                        </div>
                        
                </div>
            </div>
        </div>
    </div>
    </div>
             <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                    <Windows>
                        <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                            Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                            <ContentTemplate>
                            <br />
                            <br />
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="PRIMERO: Que voluntariamente me adhiero al Sistema de Notificaciones Electrónicas del Instituto Nacional de Bosques, enterado de las normas vigentes al respecto, Decreto 15-2011 del Congreso de la República, Ley Reguladora de las Notificaciones Electrónicas en el Organismo Judicial y Acuerdo 11-2012 de la Corte Suprema de Justicia, Reglamento para la Implementación del Sistema de Notificaciones Electrónicas en el Organismo Judicial."></asp:Label>
                                </div>
                                
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="SEGUNDO: Que estoy de acuerdo en que se envíen a mi dirección electrónica proporcionada por el Instituto Nacional de Bosques, todas las notificaciones de los procesos judiciales o procedimientos administrativos, en los que la señale como lugar para recibir notificaciones, inclusive las notificaciones de las resoluciones enumeradas en los artículos 67 del Código Procesal Civil y Mercantil y 328 del Código de Trabajo."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="TERCERO: Expresamente acepto que las notificaciones se tendrán por realizadas en la hora y día en que sean puestas en mi casillero electrónico, de conformidad con el artículo 8 del referido Reglamento."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="CUARTO: Manifiesto que soy el único responsable por el uso que yo o un tercero dé al usuario y contraseña que en este momento se genera a mi favor en el INAB y que me permitirá acceder al Sistema de Notificaciones Electrónicas."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="QUINTO: Estoy enterado que en las notificaciones a las cuales deban acompañarse otros documentos, además de la resolución, debo acudir al órgano jurisdiccional o dependencia administrativa a retirar las copias correspondientes, en el plazo señalado por el artículo 10 del referido Reglamento."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="SEXTO: Estoy enterado que el Sistema de Notificaciones Electrónicas se implementará en forma gradual, de acuerdo a lo que disponga el INAB, por lo que estoy de acuerdo en que podré señalar mi dirección electrónica para recibir notificaciones, únicamente en los órganos jurisdiccionales habilitados para el efecto."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="SEPTIMO: Que estoy enterado que de incumplir o violar los reglamentos vigentes por el INAB se me inhabilite parcial o totalmente en el uso de los sistemas electrónicos del INAB, y que en el caso de no solventar dicha situación no poder realizar trámite alguno ante dicha institución."></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                        </telerik:RadWindow>
                        <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="220px" Width="350px" Title="Terminos y condiciones de uso" Behaviors="None">
                        
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
          }
    </script>

</asp:Content>
