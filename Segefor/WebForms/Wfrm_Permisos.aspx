<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Permisos.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Permisos" %>
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
                                    <h2><strong>Permisos</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdUsuarios" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" PageSize="20" AllowFilteringByColumn="true" AllowPaging="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Estatus_UsrId,UsuarioId,nombres,Usuario,Correo" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Estatus_UsrId" UniqueName="Estatus_UsrId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="UsuarioId" UniqueName="UsuarioId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombres" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="nombres" HeaderText="Nombre" HeaderStyle-Width="255px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Usuario" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Usuario" HeaderText="Usuario" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Correo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Correo" HeaderText="Correo" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tipo_Contratacion" ShowFilterIcon="false" AllowFiltering="false" UniqueName="Tipo_Contratacion" HeaderText="Tipo de Usuario" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tipo_Usuario" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Tipo_Usuario" HeaderText="Perfil" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Permisos" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="true" UniqueName="Permisos">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgPermisos" ImageUrl="~/Imagenes/24x24/lock.png" formnovalidate ToolTip="Permisos" CommandName="CmdPermisos"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="ibox-title">
                                    <h2><strong><asp:Label runat="server" id="LblUsuario"></asp:Label></strong></h2>
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
            <asp:TextBox runat="server" ID="TxtUsuarioId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtEditar" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
