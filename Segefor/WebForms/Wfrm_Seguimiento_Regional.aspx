﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Seguimiento_Regional.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Seguimiento_Regional" %>
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
                                    <div class="col-sm-2" runat="server"  id="DivResolucion" visible="false">
                                        <asp:ImageButton runat="server" ID="ImgVerResolucion" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Resolución de Aprobación"/>
                                        <asp:Label runat="server" Text="Ver Resolución de Aprobación"></asp:Label>
                                    </div>
                                    <div class="col-sm-2" runat="server" id="DivVerDicTec" visible="false">
                                        <asp:ImageButton runat="server" ID="ImgVerDictamenTecnico" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen Técnico"/>
                                        <asp:Label runat="server" Text="Ver Dictamen Técnico"></asp:Label>
                                    </div>
                                    <%--<div class="col-sm-2" runat="server" id="DivEnmiendasTec" visible="false" >
                                        <asp:ImageButton runat="server" ID="ImgVerEnmiendasTec" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen Técnico"/>
                                        <asp:Label runat="server" Text="Ver Enmiendas Técnico"></asp:Label>
                                    </div>--%>
                                    <div class="col-sm-2" runat="server" id="DivVerDicSubRegional" visible="false">
                                        <asp:ImageButton runat="server" ID="ImgVerDictamenSubRegional" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen SubRegional"/>
                                        <asp:Label runat="server" Text="Ver Dictamen Subregional"></asp:Label>
                                    </div>
                                    <%--<div class="col-sm-2" runat="server" id="DivVerEnmiendasSubRegional" visible="false">
                                        <asp:ImageButton runat="server" ID="ImgVerEnmiendasSubReg" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Dictamen SubRegional"/>
                                        <asp:Label runat="server" Text="Ver Enmiendas Subregional"></asp:Label>
                                    </div>--%>
                                </div>
                                <div class="ibox-content">
                                    <div><h4><strong><asp:Label runat="server" ID="Label2" Text="Estado"></asp:Label></strong></h4></div>
                                        
                                    <div>
                                        <asp:Label runat="server" ID="LblEstado" Text="Dictamen Jurídico sin enmiendas"></asp:Label>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div runat="server" id="DivRegionalRegistro" visible="false">
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
                                    <div runat="server" id="DivNoAprueba">
                                        <div class="ibox-content" >
                                            <div><label class="col-sm-2 control-label centradolabel">Dirigido a</label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox runat="server" ID="TxtDirigido" CssClass="form-control"  required=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding-bottom:2em;"></div>
                                        <div class="ibox-content">
                                            <div><h4><strong><asp:Label runat="server" ID="Label3" Text="Motivos"></asp:Label></strong></h4></div>
                                        
                                            <div><label class="col-sm-2 control-label centradolabel">Motivo:</label></div>
                                                <div class="col-sm-8">
                                                    <asp:TextBox runat="server" Text="" ID="TxtMotivo" TextMode="MultiLine" Height="100px" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-1"><input type="button" runat="server" id="btnAddMotivo" title="Buscar" class="btn btn-primary" value="Agregar" /></div>
                                        </div>
                                        <div style="padding-bottom:6em;"></div>
                                        <div class="panel-body">
                                            <telerik:RadGrid runat="server" ID="GrdMotivo" Skin="MetroTouch"
                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" DataKeyNames="IdMotivo,Motivo" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="IdMotivo" UniqueName="IdMotivo" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Motivo" UniqueName="Motivo" HeaderText="Articulo" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Editar" Visible="true" UniqueName="Del">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton runat="server" ID="ImgEdit" ImageUrl="~/Imagenes/24x24/editar.png" formnovalidate ToolTip="Editar" CommandName="CmdEditar"/>
                                                                </ItemTemplate>
                                                        </telerik:GridTemplateColumn> 
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
                                    </div>
                                    <div runat="server" id="DivSiAprueba" visible="false">
                                        <div class="ibox-content" >
                                            <div><label class="col-sm-2 control-label centradolabel">Fecha de Inscripción</label>
                                                <div class="col-sm-4">
                                                    <telerik:RadDatePicker ID="TxtFecIncripcion" Width="100%" runat="server"></telerik:RadDatePicker>
                                                </div>
                                            </div>
                                             <div><label class="col-sm-2 control-label centradolabel">Fecha de Vencimiento</label>
                                                <div class="col-sm-4">
                                                    <telerik:RadDatePicker ID="TxtFecVencimiento" Width="100%" runat="server"></telerik:RadDatePicker>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding-bottom:2em;"></div>
                                        <div class="ibox-content" >
                                            <div><label class="col-sm-2 control-label centradolabel">No Expediente</label>
                                                <div class="col-sm-4">
                                                    <asp:TextBox runat="server" ID="TxtNoExpediente" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div><label class="col-sm-2 control-label centradolabel">Fecha de Ultima Actualizacion</label>
                                                <div class="col-sm-4">
                                                    <telerik:RadDatePicker ID="TxtFecUltActualizacion" Enabled="false" Width="100%" runat="server"></telerik:RadDatePicker>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-title" runat="server" id="DivNoApruebaBotones">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Oficio de Devolución"  ID="BtnVPOficio" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Oficio de Devolución"   ID="BtnGrabaOficio" class="btn btn-primary" /></div>
                                    </div>
                                    <div class="ibox-title" runat="server" id="DivSiApruebaBotones" visible="false">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Constancia RRF"  ID="BtnVPConstancia" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Constancia RRF"   ID="BtnGrabarConstancia" class="btn btn-primary" /></div>
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
                                <div runat="server" id="DivRegionalManejo" visible="false">
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
                                    <div class="ibox-content"  runat="server" id="DivApruebaLicenciaUno">
                                        <div><label class="col-sm-2 control-label centradolabel">Aprueba:</label>
                                            <div class="col-sm-5">
                                                <telerik:RadComboBox ID="CboAprueba" Width="100%" runat="server">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="No" Value="0" />
                                                        <telerik:RadComboBoxItem Text="Si" Value="1" Selected="true" />
                                                    </Items>
                                                </telerik:RadComboBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content"  runat="server" id="DivApruebaLicenciaDos">
                                        <div><label class="col-sm-2 control-label centradolabel">Periodo de licencia:</label>
                                            <div class="col-sm-5">
                                                <telerik:RadNumericTextBox runat="server" MinValue="1" ID="TxtPeriodo" Width="60px" >
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content"  runat="server" id="DivApruebaLicenciaTres">
                                        <div><label class="col-sm-2 control-label centradolabel">Fecha de inicio</label>
                                            <div class="col-sm-2">
                                               <telerik:RadDatePicker ID="TxtFecIni" Width="100%" runat="server"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                        <div><label class="col-sm-2 control-label centradolabel">Fecha de final</label>
                                            <div class="col-sm-2">
                                               <telerik:RadDatePicker ID="TxtFecFin" Width="100%" runat="server"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Licencia"  ID="BtnVistaPreviaLicencia" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Licencia"   ID="BtnGrabarLicencia" class="btn btn-primary" /></div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-9">
                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrLicencia" visible="false">
                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                <asp:Label runat="server" ID="LblErrLicencia" Font-Bold="true">Error</asp:Label>
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
                <asp:TextBox runat="server" ID="TxtMotivoId" Visible="false"></asp:TextBox>
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
