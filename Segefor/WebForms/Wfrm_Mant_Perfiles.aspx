<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Mant_Perfiles.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Mant_Perfiles" %>
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
                                    <h2><strong>Perfiles</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <asp:Button runat="server" Text="Nuevo"  ID="btnNuevo" class="btn btn-primary" />
                                </div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-2 control-label centradolabel">Perfil:</label>
                                        <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtPerfil" CssClass="form-control"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Nivel:</label>
                                        <div class="col-sm-3">
                                            <telerik:RadComboBox ID="CboAmbito" Width="100%" runat="server"></telerik:RadComboBox>
                                            
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button runat="server" Text="Grabar"  ID="BtnModificar" class="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-9">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivError" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblErorr" Font-Bold="true">Error</asp:Label>
                                        </div>
                                        <div class="alert alert-success alert-dismissable" runat="server" id="BtnMensajeMod" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeMod" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdPerfiles"  Skin="MetroTouch" PageSize="20" 
                                        AutoGenerateColumns="false" Width="100%" AllowFilteringByColumn="true" AllowSorting="true" 
                                            AllowPaging="true" GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Tipo_UsuarioId,Tipo_Usuario,AmbitoId,Ambito" NoMasterRecordsText="Sin Perfiles" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Tipo_UsuarioId" Visible="false" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tipo_Usuario" HeaderText="Perfil" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AmbitoId" Visible="false" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Ambito" HeaderText="Nivel" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Editar" AllowFiltering="false" ShowFilterIcon="false"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" UniqueName="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgEdit" CausesValidation="false" formnovalidate ImageUrl="~/Imagenes/24x24/pencil.png" ToolTip="Editar" CommandName="CmdEdit"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <%--<telerik:GridTemplateColumn HeaderText="Permisos" AllowFiltering="false" ShowFilterIcon="false"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" UniqueName="Permisos">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgPermisos" CausesValidation="false" formnovalidate ImageUrl="~/Imagenes/24x24/list.png" ToolTip="Permisos" CommandName="CmdPermisos"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> --%>
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                
                            </div>
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h2><strong><asp:Label runat="server" id="LblPerfil"></asp:Label></strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadTreeList ID="TreePermisos" runat="server" AllowLoadOnDemand="true"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="1000" Skin="Metro" AllowSorting="true"
                                    DataKeyNames="FormaId" ParentDataKeyNames="FormaId_Padre" >
                                     <Columns>
                                        <telerik:TreeListBoundColumn DataField="ModuloId" HeaderStyle-Width="250px" HeaderText="Modulo" UniqueName="ModuloId" Visible="false" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListBoundColumn DataField="FormaId" HeaderStyle-Width="250px" HeaderText="Modulo" UniqueName="FormaId" Visible="false" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListBoundColumn DataField="Modulo" HeaderStyle-Width="250px" HeaderText="Modulo" UniqueName="Modulo" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListBoundColumn DataField="Nombre" HeaderStyle-Width="250px" HeaderText="Página" UniqueName="Pagina" ReadOnly="false"></telerik:TreeListBoundColumn>
                                         <telerik:TreeListBoundColumn DataField="Consultar" Visible="false" HeaderStyle-Width="50px" HeaderText="Página" UniqueName="Consultar" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListTemplateColumn HeaderText="Consultar"  DataField="Consultar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" UniqueName="Cons">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="ChkConsultar" AutoPostBack="true" OnCheckedChanged="ChkConsultar_CheckedChanged" />
                                            </ItemTemplate>
                                        </telerik:TreeListTemplateColumn>
                                        <telerik:TreeListBoundColumn DataField="Insertar" Visible="false" HeaderStyle-Width="50px"  UniqueName="Insertar" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListTemplateColumn HeaderText="Insertar" DataField="Insertar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" UniqueName="Cons">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="ChkInsertar" AutoPostBack="true" OnCheckedChanged="ChkInsertar_CheckedChanged" />
                                            </ItemTemplate>
                                        </telerik:TreeListTemplateColumn>
                                        <telerik:TreeListBoundColumn DataField="Editar" Visible="false" HeaderStyle-Width="250px"  UniqueName="Editar" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListTemplateColumn HeaderText="Editar" DataField="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" UniqueName="Cons">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="ChkEditar" AutoPostBack="true" OnCheckedChanged="ChkEditar_CheckedChanged" />
                                            </ItemTemplate>
                                        </telerik:TreeListTemplateColumn>
                                        <telerik:TreeListBoundColumn DataField="Eliminar" Visible="false" HeaderStyle-Width="250px"  UniqueName="Eliminar" ReadOnly="false"></telerik:TreeListBoundColumn>
                                        <telerik:TreeListTemplateColumn HeaderText="Eliminar" DataField="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" UniqueName="Cons">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="ChkEliminar" AutoPostBack="true" OnCheckedChanged="ChkEliminar_CheckedChanged" />
                                            </ItemTemplate>
                                        </telerik:TreeListTemplateColumn>
                                        <telerik:TreeListBoundColumn DataField="FormaId_Padre" HeaderText="Padre" UniqueName="Padre" Visible="false"></telerik:TreeListBoundColumn>
                                    </Columns>
                                </telerik:RadTreeList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <asp:TextBox runat="server" ID="TxtTipo_UsuarioId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtEditar" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
