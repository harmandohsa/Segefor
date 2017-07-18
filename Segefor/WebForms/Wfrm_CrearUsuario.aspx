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
                                    <asp:Label runat="server" Font-Bold="true" Text="DECLARACIONES EN CUANTO A LOS TÉRMINOS Y CONDICIONES DE USO DEL PORTAL DEL SISTEMA ELECTRÓNICO DE GESTIÓN FORESTAL -SEGEFOR-."></asp:Label>
                                </div>
                                
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="Los términos y condiciones del presente acuerdo en adelante simplemente llamado ”El Acuerdo”, gobiernan y regulan las condiciones de acceso y uso del portal del Sistema Electrónico de Gestión Forestal en adelante denominado ”SEGEFOR”, por mi persona. Este es un acuerdo entre mi persona y El Instituto Nacional de Bosques en adelante denominado ”INAB”, el cual podrá ser sujeto a cambios por parte del INAB a su absoluta discreción en cualquier momento y sin necesidad de darme aviso previo. Desde ya declaro: a) ser mayor de edad y ser civilmente capaz; b) que he leído y comprendo el contenido integro de este Acuerdo y que me obligo bajo los términos y condiciones acá establecidas; c) que por este medio celebro acuerdo al cual me adhiero entre mi persona y el INAB. El INAB se reserva en todo momento el derecho de aceptar o rechazar sin expresión de causa, el contenido y administración de la información proporcionada por mi persona, quedando a absoluta discreción por parte del INAB la confirmación de la misma. Asimismo acepto que el INAB podrá en cualquier momento retirar el contenido, suspender, restringir y/o terminar los servicios proporcionados bajo este acuerdo sin ningún tipo de aviso a mi persona y sin su responsabilidad, por cualquier motivo."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="USO DEL CONTENIDO:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Al aceptar este acuerdo acepto que estoy adhiriéndome a las políticas de creación y administración del Portal del SEGEFOR."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="RESPONSABILIDAD:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Yo el USUARIO, en adición a cualquier otra obligación que hubiere adquirido en este acuerdo, me comprometo a indemnizar, defender y mantener al INAB libre y a salvo de cualquier responsabilidad, daño o perjuicio y de todas las responsabilidades, pérdidas, penalidades y costos ocasionados por demandas legales o quejas que surjan como resultado relacionado con el uso de los Términos y Condiciones del Portal del SEGEFOR y resultado relacionado con el incumplimiento o violación ocasionadas por mi persona a cualquiera de las Políticas de Uso contenidas en los “Términos y Condiciones” en este acuerdo."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Font-Bold="true" Text="ACUERDO DE ADHESIÓN QUE CONTIENE LOS TÉRMINOS Y CONDICIONES DE USO DE PORTAL DEL SISTEMA ELECTRÓNICO DE GESTIÓN FORESTAL -SEGEFOR-."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="Este Acuerdo de Adhesión que contiene los Términos y Condiciones de Uso del Portal del Sistema Electrónico de Gestión Forestal, en adelante SEGEFOR, describe los derechos y responsabilidades, y establece los términos y condiciones bajo las cuales usted puede utilizar este Sistema. Por favor, lea atentamente este documento. El término ”usted” como se utiliza aquí se refiere a todas las personas y/o entidades que acceden al SEGEFOR por cualquier razón. Al continuar utilizando el SEGEFOR, usted indica que acepta obligarse por los términos y condiciones de este Acuerdo. Si usted no acepta los términos y condiciones establecidos aquí, no podrá hacer uso del Sistema Electrónico de Gestión Forestal -SEGEFOR-. Es su responsabilidad revisar este Acuerdo periódicamente. El INAB se reserva el derecho de modificar este Acuerdo periódicamente sin necesidad de previo aviso y a su sola discreción en cualquier momento mediante la actualización del Sistema, y su uso continuado después de cualquier modificación constituirá su aceptación de dichas modificaciones. Antes de continuar, le recomendamos imprimir o guardar una copia local de las condiciones universales contenidas en el presente acuerdo a efectos de control personal. Los Términos y condiciones de uso son los siguientes:"></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="PRIMERA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Que voluntariamente me adhiero al Sistema Electrónico de Gestión Forestal del Instituto Nacional de Bosques, enterado de las normativa forestal vigente aplicable. "></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="SEGUNDA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Manifiesto que soy el único responsable por el uso que yo o un tercero dé al Usuario y contraseña que en este momento se genera a mi favor y que me permitirá acceder al Sistema Electrónico de Gestión Forestal -SEGEFOR-."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="TERCERA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Que estoy de acuerdo en que se envíe a la dirección electrónica que proporcione, todas las notificaciones de los procedimientos administrativos y resoluciones, en los que la señale como lugar para recibir notificaciones, inclusive las notificaciones de las resoluciones enumeradas en el Decreto 119-96 del Congreso de la República de Guatemala, Ley de lo Contencioso Administrativo; Decreto 101-96 del Congreso de la República de Guatemala, Ley Forestal y su Reglamento; Decreto Ley 107, Código Procesal Civil y Mercantil y demás legislación Forestal en que el INAB tenga competencia. "></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="CUARTA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Expresamente acepto que las notificaciones se tendrán por realizadas en la hora y día en que sean puestas en mi la pestaña de notificaciones de la página web del SEGEFOR y en la Bandeja de entrada del correo electrónico que proporcione como lugar para recibir notificaciones."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="QUINTA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" El Usuario desde ya manifiesta que está enterado de que si ingresa una gestión al SEGEFOR dentro de la cual existen requisitos faltantes, al término de treinta días calendario no ha presentado los mismos, la gestión será eliminada automáticamente, debiendo iniciar una nueva gestión en el SEGEFOR."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="SEXTA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Estoy enterado que el Sistema Electrónico de Gestión Forestal se implementará en forma gradual, de acuerdo a lo que disponga el INAB, por lo que estoy de acuerdo en que se podrá utilizar la misma dirección electrónica para recibir notificaciones, únicamente en el sitio web del SEGEFOR."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="SEPTIMA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" El Usuario desde ya manifiesta que la información otorgada es verídica, y por lo tanto autoriza al INAB, para que pueda corroborar la veracidad de toda la información proporcionada, por cualquier medio legal, siendo responsable de lo relativo a los delitos de perjurio y falsedad en caso se llegará a constatar que la información relacionada es falsa parcial o totalmente."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="OCTAVA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Este es un servicio gratuito y exclusivo, que se otorga para todos los Usuarios del INAB."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="NOVENA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" El plazo de este acuerdo de adhesión se interpreta como indefinido, hasta que el INAB decida darlo por terminado, en cualquier momento, sin necesidad de dar aviso, sin expresión de causa y sin necesidad de declaratoria judicial alguna. El Usuario  podrá dar por terminado el acuerdo con manifestación expresa y expresión de causa por cualquier medio electrónico o por escrito. "></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="DÉCIMA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" El Usuario y el INAB hacen constar que tratarán de encontrar una solución amigable a cualquier controversia que pudiere surgir relativa a la aplicación, interpretación, contravención, terminación o invalidez del presente acuerdo de adhesión. En el caso que las partes no pudieren llegar a una solución amigable dentro de los treinta (30) días siguientes a la notificación efectuada por una de las partes a la otra para tratar de solucionar la controversia, ésta se someterá a la jurisdicción de los Tribunales competentes."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="DÉCIMA PRIMERA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" Este Acuerdo de Adhesión será regido e interpretado por las leyes de la República de Guatemala. El conocimiento y resolución de los conflictos que surjan con motivo de este Acuerdo de Adhesión serán competencia exclusiva de los Tribunales de Justicia del departamento de Guatemala, por lo que el Usuario declara: a) que renuncia al fuero de su domicilio y al de cualquier otra competencia que pudiere corresponderle y se somete a los tribunales competentes del Departamento de Guatemala o cualquier otro tribunal que el INAB elija con motivo de la interpretación y cumplimiento del presente Acuerdo de Adhesión."></asp:Label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-12" style="text-align:justify;">
                                    <asp:Label runat="server" Text="DÉCIMA SEGUNDA:" Font-Bold="true"></asp:Label>
                                    <asp:Label runat="server" Text=" El Usuario acepta el contenido íntegro del presente Acuerdo de Adhesión al Sistema Electrónico de Gestión y de todos los términos que en el constan, por lo que bien enterado, de su contenido, objeto, validez y efectos legales lo acepta y ratifica adhiriéndose  a los términos pactados. "></asp:Label>
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
