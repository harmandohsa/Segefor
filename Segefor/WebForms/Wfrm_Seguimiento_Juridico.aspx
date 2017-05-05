<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Seguimiento_Juridico.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Seguimiento_Juridico" %>
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
                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Solicitud"/>
                                            <asp:Label runat="server" Text="Ver Solicitud"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:ImageButton runat="server" ID="ImgVerProvidencia" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Providencia"/>
                                            <asp:Label runat="server" Text="Ver Providencia"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:1em;"></div>
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label2" Text="Asunto"></asp:Label></strong></h4></div>
                                        
                                        <div><label class="col-sm-2 control-label centradolabel">Titulo:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" Text="" ID="TxtTitulo" class="form-control" required=""></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--<div><label class="col-sm-2 control-label">Cuerpo Asunto:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" ID="TxtCuerpoAsunto" TextMode="MultiLine" CssClass="form-control" required=""></asp:TextBox>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label1" Text="Antecedentes"></asp:Label></strong></h4></div>
                                        
                                        <div><label class="col-sm-2 control-label centradolabel">Titulo:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" Text="" ID="TxtTituloRegente" class="form-control" required=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label3" Text="Fundamento Legal"></asp:Label></strong></h4></div>
                                        
                                        <div><label class="col-sm-2 control-label centradolabel">Articulo:</label></div>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" Text="" ID="TxtArticulo" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1"><input type="button" runat="server" id="btnAddArticulo" title="Buscar" class="btn btn-primary" value="Agregar" /></div>
                                    </div>
                                    <div style="padding-bottom:1em;"></div>
                                    <div class="panel-body">
                                        <div>
                                            <telerik:RadGrid runat="server" ID="GrdArticulo" Skin="MetroTouch"
                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" DataKeyNames="Id_Articulo,Articulo" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Articulo" UniqueName="Id_Articulo" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Articulo" UniqueName="Articulo" HeaderText="Articulo" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
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
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label4" Text="Análisis"></asp:Label></strong></h4></div>
                                        <div><label class="col-sm-2 control-label centradolabel">Introducción:</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" Text="" ID="TxtAnalisisGen" class="form-control" required=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div><label class="col-sm-2 control-label centradolabel">Análisis:</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" Text="" ID="TxtAnalis" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1"><input type="button" runat="server" id="BtnAnalisis" title="Buscar" class="btn btn-primary" value="Agregar" /></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:1em;"></div>
                                    <div class="panel-body">
                                        <div>
                                            <telerik:RadGrid runat="server" ID="GrdAnalisis" Skin="MetroTouch"
                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" DataKeyNames="Id_Analisis,Analisis" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Id_Analisis" UniqueName="Id_Analisis" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Analisis" UniqueName="Analisis" HeaderText="Analisis" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
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
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label5" Text="Enmiendas"></asp:Label></strong></h4></div>
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
                                            <div class="col-sm-1"><input type="button" runat="server" id="BtnAddEnmienda" title="Buscar" class="btn btn-primary" value="Agregar" /></div>
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
                                            <MasterTableView Caption="" DataKeyNames="Id_Enmienda,Enmienda" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Id_Enmienda" UniqueName="Id_Enmienda" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Enmienda" UniqueName="Enmienda" HeaderText="Enmienda" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
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
                                    <div style="padding-bottom:4em;"></div>
                                    <div class="ibox-title">
                                        <div><label class="col-sm-2 control-label centradolabel">Considera:</label>
                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboConsidera" Width="100%" runat="server"></telerik:RadComboBox></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-title">
                                        <div><label class="col-sm-2 control-label centradolabel">Opinion:</label>
                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboOpinion" Width="100%" runat="server"></telerik:RadComboBox></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-title">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Dictamen"  ID="BtnVistaPrevia" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Dictamen"   ID="BtnEnviar" class="btn btn-primary" /></div>
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
            <asp:TextBox runat="server" ID="TxtIdArticulo" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtIdAnalisis" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtIdEnmienda" Visible="false"></asp:TextBox>
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
