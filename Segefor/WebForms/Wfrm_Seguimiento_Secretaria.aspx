<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Seguimiento_Secretaria.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Seguimiento_Secretaria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
                <div class="wrapper wrapper-content animated fadeInRight">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h2><strong>Seguimiento de Gestión</strong></h2>
                                </div>
                                <div class="ibox-content">
                                        <div class="col-sm-4">
                                            <h4><strong><asp:Label runat="server" ID="LblNug"></asp:Label></strong></h4>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-10">
                                            <h4><strong><asp:Label runat="server" ID="LblSolicitante"></asp:Label></strong></h4>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-12">
                                            <h4><strong><asp:Label runat="server" ID="LblIdentificacion"></asp:Label></strong></h4>
                                        </div>
                                        
                                    </div>
                                <div runat="server" id="Requisitos_Profesional" visible="false" class="ibox-content">
                                    <div class="col-sm-10"><asp:CheckBox runat="server" AutoPostBack="true" Visible="false" CssClass="i-checks" ID="ChkTitulo" Text="Copia legalizada del título" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" AutoPostBack="true" Visible="false" CssClass="i-checks" ID="ChkColegiado" Text=" Constancia original de colegiado activo vigente" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" AutoPostBack="true" CssClass="i-checks" ID="ChkRtu" Text="Constancia de inscripción en el Registro Tributario Unificado (RTU)" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" AutoPostBack="true" CssClass="i-checks" ID="ChkId" Text="Copia de documento personal de identificación (DPI)." /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" AutoPostBack="true" CssClass="i-checks" Visible="false" ID="ChkPosgrado" Text="Documento extendido por la universidad que avala post grado en materia forestal" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" AutoPostBack="true" CssClass="i-checks" Visible="false" ID="ChkDiploma" Text="copia del diploma de aprobación del curso correspondiente." /></div>
                                </div>
                                <div runat="server" id="Div_Requisitos_Plantacion" visible="false" class="ibox-content">
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks"  ID="ChkCertificacionPV" Text="Certificación original o copia legalizada de dicha certificación que acredite la propiedad   del bien, consignando las anotaciones y gravámenes que contienen. En caso que la propiedad no esté inscrita en el Registro, se podrá aceptar, otro documento legal que acredite la propiedad o posesión; estos documentos no deben exceder de ciento veinte (120) días calendario de haber sido extendidas con relación a la fecha de presentación de la solicitud" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" Visible="false" CssClass="i-checks" ID="ChkIdNoRepresentantePV" Text="Copia del documento personal de identificación del propietario" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  Visible="false" CssClass="i-checks" ID="ChkIdSiRepresentantePV" Text="Copia del documento personal de identificación del Representante Legal" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" Visible="false" CssClass="i-checks" ID="ÇhkCopiaNombramientoPV" Text="Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks"  ID="ChkPoliginoPV" Text="Polígono georeferenciado a registrar, en coordenadas GTM. Es requisito de inscripción indispensable  de las plantaciones forestales tener como mínimo un año de haber sido establecidas y para efectos de inscripción, se establecerán  parámetros técnicos de evaluación" /></div>
                                </div>
                                <div runat="server" id="Div_Requisitos_Empresas" visible="false" class="ibox-content">
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks"  ID="ChkPatente" Text="Copia legalizada de la patente de comercio, con la especificación clara del objeto del negocio como actividad forestal " /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" ID="ChkRtuEmpresa" Text="Copia de constancia de inscripción en el Registro Tributario Unificado (RTU). Las sucursales deben contar con su propia patente de comercio" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  Visible="false" CssClass="i-checks" ID="ChkDocPropietario" Text="Copia del documento personal de identificación del propietario " /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false" ID="ChkDocRepresentante" Text="Copia del documento personal de identificación del Representante Legal" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false"  ID="ChkNomRepresentante" Text="Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente" /></div>
                                </div>
                                <div runat="server" id="Div_Requisitos_Entidades" visible="false" class="ibox-content">
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks"  ID="ChkDocConstitucion" Text="Copia del documento que ampare la constitución y objeto de la entidad" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" ID="ChkCarneSat" Text="Copia del carné de identificación tributaria" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false" ID="ChkRtuEntidad" Text="Constancia de inscripción en el Registro Tributario Unificado (RTU)" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  Visible="false" CssClass="i-checks" ID="ChkDocPropietarioEntidad" Text="Copia del documento personal de identificación del propietario" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false" ID="ChkDocRepresentanteEntidad" Text="Copia del documento personal de identificación del Representante Legal" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false"  ID="ChkNomRepresentanteEntidad" Text="Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false"  ID="ChkCopiaActa" Text="Copia simple del acta de toma de posesión o nombramiento según sea el caso" /></div>
                                </div>
                                <div runat="server" id="Div_Requisitos_MotoSierras" visible="false" class="ibox-content">
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false"  ID="ChkPatenteMoto" Text="Copia de la patente de comercio, la cual debe indicar el objeto de dicha actividad. Las sucursales deben contar con su propia patente de comercio " /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false" ID="ChkRtuMoto" Text="Constancia de inscripción en el Registro Tributario Unificado (RTU)"/></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false" ID="ChkDocPropiedad" Text="Documento original que acredite la propiedad del bien" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false" ID="ChkCopiaPropiedad" Text="Copia que acredite la propiedad del bien" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false" ID="ChkDocPropietarioMoto" Text="Copia del documento personal de identificación del propietario" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false" ID="ChkDocRepresentanteMoto" Text="Copia del documento personal de identificación del Representante Legal" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server" CssClass="i-checks" Visible="false"  ID="ChkCopiaLegalMoto" Text="Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente" /></div>
                                </div>
                                <div runat="server" id="Div_Requisitos_Aprovechamiento" visible="false" class="ibox-content">
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks"   ID="ChkSolicitud" Text="Solicitud Autenticada" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks"  ID="ChkDocPropiedadbien" Text="Documento original que acredite la propiedad del bien" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false"  ID="ChkDocPropietarioAprovechamiento" Text="Copia del documento personal de identificación del propietario " /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false"  ID="ChkCopiaDocRepresentanteAprovechamiento" Text="Copia del documento personal de identificación del Representante Legal" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks" Visible="false"  ID="ChkCopiaNombramientoAprovechamiento" Text="Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente" /></div>
                                    <div class="col-sm-10"><asp:CheckBox runat="server"  CssClass="i-checks"  ID="ChkPlanManejo" Text="Plan de Manejo Forestal" /></div>
                                </div>
                                <div class="ibox-content">
                                    <div class="col-sm-2">
                                        <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información"/>
                                        <asp:Label runat="server" Text="Ver Solicitud"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:ImageButton runat="server" ID="IngVerAnexos" ImageUrl="~/Imagenes/24x24/blank.png" formnovalidate ToolTip="Ver Anexos"/>
                                        <asp:Label runat="server" Text="Ver Anexos" ID="LblAnexos"></asp:Label>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    
                                    <div class="col-sm-2"><asp:Button runat="server" Text="Procesar Gestión"  ID="BtnProcesar" class="btn btn-primary" /></div>
                                </div>
                                <div class="ibox-content" runat="server" id="DivDatosFac" visible="false">
                                    <div><label class="col-sm-1 control-label centradolabel">No. Serie:</label>
                                        <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtSerie" CssClass="form-control" required=""></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">No. Factura:</label>
                                        <div class="col-sm-2"><asp:TextBox runat="server" ID="Txtfactura" step="0" min="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                    </div>
                                    <div class="col-sm-2"><asp:Button runat="server" Text="Enviar Gestión"  ID="BtnEnviaGestion" class="btn btn-primary" /></div>
                                </div>
                                <div class="ibox-content" runat="server" >
                                    <div class="col-sm-10">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivError" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblError" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodRepresentante" visible="false">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMansajeGoodRepresentante" Font-Bold="true">Error</asp:Label>
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
                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="220px" Width="350px" Title="Confirmación" Behaviors="None">
                        <ContentTemplate>
                            <asp:Label ID="LblTitConfirmacion" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            <div class="ibox-content">
                                <div class="col-sm-3">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ask.png" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" Text="Sí"  ID="BtnYes" class="btn btn-primary" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" Text="No"  ID="BtnNo" class="btn btn-primary" />
                                </div>
                            </div>
                            
                        </ContentTemplate>
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
        function Message(Mensaje) {
            bootbox.alert(Mensaje);
        }
    </script>
</asp:Content>
