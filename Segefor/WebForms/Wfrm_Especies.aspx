<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Especies.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Especies" %>
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
                                    <h2><strong>Catalogo de Especies</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <div class="col-sm-1">
                                        <input type="button" runat="server" id="btnNuevo" title="Nueva" class="btn btn-primary" value="Nuevo" />
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Codígo:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtCodigo" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Nombre Cientifico:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNomCientifico" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Familia:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtFamilia" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Genero:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtGenero" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Autores:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtAutores" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Nombre Comun:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombreComun" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Sinonimos:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtSinonimo" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Nombre Comercial:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombreComercial" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-1">
                                        <input type="button" runat="server" id="BtnGrabar" title="Grabar" class="btn btn-primary" value="Grabar" />
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-8">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrGrabar" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblErrGrabar" Font-Bold="true">Error</asp:Label>
                                        </div>
                                        <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodGrabar" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblGoodGrabar" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdEspecies" Skin="Vista" PageSize="15"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  AllowPaging="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="EspecieId,Codigo_Especie,Nombre_Cientifico,Familia,Genero,Autores,Nombre_Comun,Sinonimos,Nombre_Comercial,Estado_EspecieId" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="EspecieId" UniqueName="EspecieId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Codigo_Especie" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Codigo_Especie" HeaderText="Código" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre_Cientifico" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Familia" AllowFiltering="false" UniqueName="Familia" HeaderText="Nombre Familia" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Genero" AllowFiltering="false" UniqueName="Genero" HeaderText="Genero" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Autores" AllowFiltering="false" UniqueName="Autores" HeaderText="Autores" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre_Comun" AllowFiltering="false" UniqueName="Nombre_Comun" HeaderText="Nombre_Comun" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Sinonimos" AllowFiltering="false" UniqueName="Sinonimos" HeaderText="Sinonimos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre_Comercial" AllowFiltering="false" UniqueName="Nombre_Comercial" HeaderText="Nombre_Comercial" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Estado_EspecieId" UniqueName="Estado_EspecieId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Editar" AllowFiltering="false" Visible="true" UniqueName="Editar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgEdit" ImageUrl="~/Imagenes/24x24/new.png" formnovalidate ToolTip="Inactivar" CommandName="CmdEdit"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Activar/Inactivar" AllowFiltering="false" Visible="true" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgInactivar" Visible="false" ImageUrl="~/Imagenes/24x24/ok.png" formnovalidate ToolTip="Inactivar" CommandName="CmdInac"/>
                                                            <asp:ImageButton runat="server" ID="ImgActivar" Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" formnovalidate ToolTip="Activar" CommandName="CmdActivar"/>
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
                        </div>
                    </div>
                </div>
            <asp:TextBox runat="server" Visible="false" id="TxtEspecieId"></asp:TextBox>
            <asp:TextBox runat="server" Visible="false" id="TxtCodigoEspecieOrignial"></asp:TextBox>
            <asp:TextBox runat="server" Visible="false" id="TxtNombreCientificoOriginal"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
