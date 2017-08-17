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
                                    <div class="col-sm-2" runat="server" id="DivVerDicTec" >
                                        <asp:ImageButton runat="server" ID="ImgVerDictamenTecnico" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen Técnico"/>
                                        <asp:Label runat="server" Text="Ver Dictamen Técnico"></asp:Label>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="DivEnmiendasTec" visible="false" >
                                        <asp:ImageButton runat="server" ID="ImgVerEnmiendasTec" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen Técnico"/>
                                        <asp:Label runat="server" Text="Ver Enmiendas Técnico"></asp:Label>
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
                                <div runat="server" id="DivDictamenManejo" visible="false">
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label5" Text="Desea agregar enmiendas"></asp:Label></strong></h4></div>
                                        <div><label class="col-sm-2 control-label centradolabel">Tiene Enmiendas:</label>
                                            <div class="col-sm-8">
                                                <asp:RadioButtonList runat="server" ID="OptEnmiendas" CssClass="radio radio-inline" AutoPostBack="true">
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="DivEnmiendas" class="panel-body" visible="false">
                                        <div><label class="col-sm-3 control-label centradolabel">Ingrese enmiendas:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" Text="" ID="TxtEnmienda" class="form-control"></asp:TextBox>    
                                            </div>
                                            <div class="col-sm-1"><input type="button" runat="server" id="BtnAddEnmienda" title="Agregar" class="btn btn-primary" value="Agregar" /></div>
                                        </div>
                                    </div>
                                    <div runat="server" id="DivEnmiendaGrid" class="panel-body" visible="false">
                                        <telerik:RadGrid runat="server" ID="GrdEnmiendas" Skin="MetroTouch"
                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                            GridLines="Both" >
                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                PrevPageText="Anterior" Position="Bottom" 
                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                PageSizeLabelText="Regitros"/>
                                            <MasterTableView Caption="" DataKeyNames="Enmienda" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Enmienda" UniqueName="Enmienda" HeaderText="Enmienda" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ID="ImgDel" ImageUrl="~/Imagenes/24x24/trashcan.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                            </ItemTemplate>
                                                    </telerik:GridTemplateColumn> 
                                                </Columns>        
                                            </MasterTableView>
                                            <FilterMenu EnableTheming="true">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                    </div>
                                     <div class="ibox-content" runat="server" id="DivErrEnmineda" visible="false">
                                        <div class="col-sm-9">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                <asp:Label runat="server" ID="LblErrEnmienda" Font-Bold="true">Error</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content" runat="server" id="DivDictamenTecUno">
                                        <div><label class="col-sm-2 control-label centradolabel">Considera:</label>
                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboConsidera" Width="100%" runat="server"></telerik:RadComboBox></div>
                                        </div>
                                    </div>
                                    <div class="ibox-content" runat="server" id="DivDictamenTecDos">
                                        <div><label class="col-sm-2 control-label centradolabel">Número de Folios:</label>
                                            <div class="col-sm-5">
                                                <telerik:RadNumericTextBox runat="server" MinValue="1" ID="TxtFolios" Width="60px" >
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Dictamen"  ID="BtnVistaPreviaDictamen" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Dictamen"   ID="BtnGrabarDictamen" class="btn btn-primary" /></div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-9">
                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrDictamen" visible="false">
                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                <asp:Label runat="server" ID="LblErrDictamen" Font-Bold="true">Error</asp:Label>
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
            <asp:TextBox runat="server" ID="TxtTieneEnmiendas" Text="0" Visible="false"></asp:TextBox>
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
