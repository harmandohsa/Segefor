<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Seguimiento_SubRegional.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Seguimiento_SubRegional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
                <div class="wrapper wrapper-content animated fadeInRight">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-content">
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
                                    <div style="padding-bottom:1em;"></div>
                                    <div id="DivResolucion" runat="server" visible="false">
                                        <div class="ibox-title">
                                            <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Resolución"  ID="BtnVistaPreviaResolucion" class="btn btn-primary" /></div>
                                            <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Resolución"   ID="BtnEnviarRes" class="btn btn-primary" /></div>
                                        </div>
                                    </div>
                                    <div id="DivProvidencia" runat="server" visible="false">
                                        <div class="ibox-title">
                                        <div><label class="col-sm-2 control-label centradolabel">Seleccione Jurídico:</label>
                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboJuridico" Width="100%" runat="server"></telerik:RadComboBox></div>
                                        </div>
                                        </div>
                                        <div style="padding-bottom:1em;"></div>
                                        <div class="ibox-title">
                                            <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Providencia"  ID="BtnVistaPrevia" class="btn btn-primary" /></div>
                                            <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Providencia"   ID="BtnEnviar" class="btn btn-primary" /></div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="col-sm-9">
                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivError" visible="false">
                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                    <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                                                </div>
                                            </div>
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
                                    <asp:Button runat="server" Text="Sí"  ID="BtnYes" data-loading-text="Enviando..."  class="btn btn-primary" />
                                </div>
                                <%--<div class="col-sm-2">
                                    <asp:Button runat="server" Text="No"  ID="BtnNo" class="btn btn-primary" />
                                </div>--%>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function pageLoad() {
            $('#<%=BtnYes.ClientID%>').click(function () {
                $(this).button('loading').delay(100000).queue(function () {
                    $(this).button('reset');
                    $(this).dequeue();
                    $(this).data('loading-text', 'Cargando...');
                });
            });
        }
    </script>
</asp:Content>
