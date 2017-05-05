<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Seguimiento_Notificacion_deJuridico_SubRegional.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Seguimiento_Notificacion_deJuridico_SubRegional" %>
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
                                <div class="ibox-content">
                                    <div class="col-sm-2">
                                        <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información"/>
                                        <asp:Label runat="server" Text="Ver Solicitud"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:ImageButton runat="server" ID="ImgVerProvidencia" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Providencia"/>
                                        <asp:Label runat="server" Text="Ver Providencia"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:ImageButton runat="server" ID="ImgVerDictamenJuridico" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen Jurídico"/>
                                        <asp:Label runat="server" Text="Ver Dictamen Jurídico"></asp:Label>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div><h4><strong><asp:Label runat="server" ID="Label2" Text="Estado"></asp:Label></strong></h4></div>
                                        
                                    <div>
                                        <asp:Label runat="server" ID="LblEstado" Text="Dictamen Jurídico sin enmiendas"></asp:Label>
                                    </div>
                                </div>
                                <div runat="server" id="DivConEnmiendas" visible="false">
                                    <div class="ibox-title">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Oficio de Enmiendas"  ID="BtnVistaPreviaOficio" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Oficio de Enmiendas"   ID="BtnEnviarOficio" class="btn btn-primary" /></div>
                                    </div>
                                </div>
                                <div runat="server" id="DivSinEnmiendas" visible="false">
                                    <div class="ibox-title">
                                        <div><label class="col-sm-2 control-label centradolabel">¿Aprueba Inscripción?:</label>
                                            <div class="col-sm-8">
                                                <asp:RadioButtonList runat="server" ID="OptApruebaInscripción" CssClass="radio radio-inline" AutoPostBack="true">
                                                    <asp:ListItem Text="No" Value="2" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="ibox-content" runat="server" id="DivCarga" visible="false">
                                        <div><label class="col-sm-3 control-label centradolabel">Cargar PDF (Expediente escaneado)</label>
                                            <div class="col-sm-7">
                                                <telerik:RadAsyncUpload DisableChunkUpload="true" runat="server" ID="RadUploadExp" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".pdf" PostbackTriggers="BtnGrabaResolucion,BtnYes" DropZones=".DropZone1" />
                                                <%--<div style="padding-top:1em;padding-bottom:1em;" class="DropZone1">
                                                    <p>Arrastre aqui su archivo</p>
                                                </div>--%>
                                    
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-title">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Resolución"  ID="BtnVPResolucion" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Resolución"   ID="BtnGrabaResolucion" class="btn btn-primary" /></div>
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
            <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                    <Windows>
                        <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                            Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                        </telerik:RadWindow>
                        <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="220px" Width="350px" Title="Confirmación" Behaviors="Close">
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
