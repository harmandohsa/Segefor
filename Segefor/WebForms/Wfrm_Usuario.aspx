<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Usuario.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Usuario" %>
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
                                    <h2><strong>Creación de Usuarios</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <div class="col-sm-1">
                                        <input type="button" runat="server" id="btnNuevo" title="Buscar" class="btn btn-primary" value="Nuevo" />
                                    </div>
                                    <div><label class="col-sm-2 control-label centradolabel">Usuario:</label>
                                        <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoContratacion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <h4><strong>Buscar Empleado</strong></h4>
                                     <div><label class="col-sm-1 control-label centradolabel">DPI:</label>
                                        <div class="col-sm-3"><asp:TextBox runat="server" Text="" ID="TxtDpi" class="form-control" data-mask="9999-99999-9999" ></asp:TextBox></div>
                                    </div>
                                    <div>
                                        <div class="col-sm-1"><input type="button" runat="server" id="BtnBuscar" title="Buscar" class="btn btn-primary" value="Buscar" /> </div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Empleado:</label>
                                        <div class="col-sm-6"><telerik:RadComboBox Filter="Contains" AllowCustomText="true" ID="CboEmpleado" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-9">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="BtnErrorDpi" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeErrorDpi" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Nombres:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Apellidos:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtApellidos" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Puesto:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtPuesto" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Código:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtCodEmpl" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Usuario:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtUsuario" CssClass="form-control" required=""></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Correo:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtCorreo" type="email" CssClass="form-control" required=""></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Región:</label>
                                        <div class="col-sm-4"><telerik:RadComboBox ID="CboRegion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">SubRegión:</label>
                                        <div class="col-sm-4"><telerik:RadComboBox ID="CboSubregion" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                    <div><div class="col-sm-1"><input type="button" runat="server" id="btnAddRegion" title="Buscar" class="btn btn-primary" value="Agregar" /></div></div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-9">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrReg" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblRegionError" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdRel_Region" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="RegionId,RegionNombre,SubRegionId,SubRegionNombre" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="RegionId" UniqueName="RegionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SubRegionId" UniqueName="SubRegionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RegionNombre" UniqueName="RegionNombre" HeaderText="Región" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SubRegionNombre" UniqueName="SubRegionNombre" HeaderText="Subregión" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
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
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-1 control-label centradolabel">Perfil:</label>
                                        <div class="col-sm-4"><telerik:RadComboBox ID="CboPerfil" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Modulos:</label>
                                        <div class="col-sm-4">
                                            <telerik:RadGrid runat="server" ID="GrdModulos" Skin="MetroTouch"
                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" DataKeyNames="ModuloId,Modulo" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="25px" Visible="true" UniqueName="Selecionar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" ID="ChkModulo" />
                                                                </ItemTemplate>
                                                        </telerik:GridTemplateColumn> 
                                                        <telerik:GridBoundColumn DataField="ModuloId" UniqueName="ModuloId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Modulo" UniqueName="Modulo" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        
                                                    </Columns>        
                                                </MasterTableView>
                                                <FilterMenu EnableTheming="true">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-1" ><asp:Button runat="server" ID="BtnGrabar" class="btn btn-primary" Text="Grabar" />
                                    </div>
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
                                    <telerik:RadGrid runat="server" ID="GrdUsuarios" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" PageSize="20" AllowFilteringByColumn="true" AllowPaging="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Estatus_UsrId,UsuarioId,nombres,Usuario,Correo,Tipo_Usuario" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Estatus_UsrId" UniqueName="Estatus_UsrId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="UsuarioId" UniqueName="UsuarioId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombres" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="nombres" HeaderText="Nombre" HeaderStyle-Width="255px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Usuario" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Usuario" HeaderText="Usuario" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Correo" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Correo" HeaderText="Correo" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tipo_Contratacion" ShowFilterIcon="false" AllowFiltering="false" UniqueName="Tipo_Contratacion" HeaderText="Tipo de Usuario" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Tipo_Usuario" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Tipo_Usuario" HeaderText="Perfil" HeaderStyle-Width="170px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Editar" Visible="true" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgEdit"  ImageUrl="~/Imagenes/24x24/editar.png" formnovalidate ToolTip="Editar" CommandName="CmdEditar"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Password" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="true" UniqueName="Pass">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgPass" ImageUrl="~/Imagenes/24x24/lock.png" formnovalidate ToolTip="Resetear Clave" CommandName="CmdPass"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Activar / Desactivar" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="true" UniqueName="ActDes">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgAct" ImageUrl="~/Imagenes/24x24/cancel.png" formnovalidate ToolTip="Editar" CommandName="CmdAct"/>
                                                            <asp:ImageButton runat="server" ID="ImgDes" ImageUrl="~/Imagenes/24x24/check.png" formnovalidate ToolTip="Editar" CommandName="CmdDes"/>
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
            <asp:TextBox runat="server" ID="txtDpitrue" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtCodPuesto" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtUsuarioId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtUsuarioAntes" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtCorreoAntes" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtPerfilId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtGeneroID" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
