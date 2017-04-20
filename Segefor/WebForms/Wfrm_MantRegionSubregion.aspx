<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_MantRegionSubregion.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_MantRegionSubregion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
        
                <div class="wrapper wrapper-content animated fadeInRight">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h2><strong>Asignación de Titulares a regiones</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdRegiones" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="RegionId,Nombre,PersonaId,Persona,Ubicacion,No_Region" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="No_Region" UniqueName="No_Region" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RegionId" UniqueName="RegionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PersonaId" UniqueName="PersonaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Nombre" HeaderText="Región"  HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Persona" UniqueName="Persona" HeaderText="Persona" Visible="false"  HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Persona" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="PersonaEdit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <telerik:RadComboBox runat="server" ID="CboPersona" Width="510px" AutoPostBack="true" OnSelectedIndexChanged="CboPersona_SelectedIndexChanged"></telerik:RadComboBox>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridBoundColumn DataField="Ubicacion" UniqueName="Ubicacion" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderText="Ubicacion" HeaderStyle-Width="400px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Subregiones" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="Subregiones" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgEdit" ImageUrl="~/Imagenes/24x24/detalle.png" formnovalidate CommandName="CmdSubRegiones" />
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Poligono" ShowFilterIcon="false" AllowFiltering="false"  Visible="false" UniqueName="Poligono" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgPoli" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate CommandName="CmdPoligono" />
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                                <div class="ibox-title" runat="server" id="DivSubRegiones" visible="false">
                                    <h2><strong>Asignación de Titulares a SubRegiones</strong></h2>
                                    <input type="button" runat="server" id="btnNuevo" title="Nueva" class="btn btn-primary" value="Nueva" />
                                </div>
                                <div runat="server" id="DivAddSubRegion" visible="false">
                                    <div class="ibox-content">
                                        <div><label class="col-sm-1 control-label centradolabel">No. Sub región:</label>
                                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoSubRegion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                        </div>
                                        <div><label class="col-sm-1 control-label centradolabel">Sub región:</label>
                                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtSubRegion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-content">
                                         <div><label class="col-sm-1 control-label centradolabel">Persona:</label>
                                            <div class="col-sm-4"><telerik:RadComboBox runat="server" ID="CboPersona"  Width="300px" Enabled="false"></telerik:RadComboBox></div>
                                        </div>
                                        <div><label class="col-sm-1 control-label centradolabel">Ubicación:</label>
                                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtUbicacion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-content">
                                         <div><label class="col-sm-1 control-label centradolabel">Departamento:</label>
                                            <div class="col-sm-4"><telerik:RadComboBox runat="server" ID="CboDepartamentoSubRegion" AutoPostBack="true"  Width="300px" Enabled="false"></telerik:RadComboBox></div>
                                        </div>
                                        <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                            <div class="col-sm-4"><telerik:RadComboBox runat="server" ID="CboMunicipioSubRegion"   Width="300px" Enabled="false"></telerik:RadComboBox></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-content">
                                        <input type="button" runat="server" id="BtnGrabar" title="Grabar" class="btn btn-primary" value="Grabar" />
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-8">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErr" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeErr" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="alert alert-success alert-dismissable" runat="server" id="DivNoErr" visible="false">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeNoErr" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdSubRegiones" Skin="MetroTouch" Visible="false"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="SubRegionId,Nombre,PersonaId,Persona,Ubicacion,EstadoSubregionId,DepMun" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="SubRegionId" UniqueName="RegionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PersonaId" UniqueName="PersonaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Nombre" HeaderText="Sub región"  HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Persona" UniqueName="Persona" HeaderText="Persona" Visible="false"  HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Persona" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="PersonaEdit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <telerik:RadComboBox runat="server" ID="CboPersonaSubReg" Width="310px" AutoPostBack="true" OnSelectedIndexChanged="CboPersonaSubReg_SelectedIndexChanged"></telerik:RadComboBox>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridBoundColumn DataField="Ubicacion" UniqueName="Ubicacion" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderText="Ubicacion" HeaderStyle-Width="400px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DepMun" UniqueName="DepMun" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderText="Departamento / Municipio" HeaderStyle-Width="400px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EstadoSubregionId" UniqueName="EstadoSubregionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Activar/Inactivar" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgInactivar" Visible="false" ImageUrl="~/Imagenes/24x24/ok.png" formnovalidate ToolTip="Inactivar" CommandName="CmdInac"/>
                                                            <asp:ImageButton runat="server" ID="ImgActivar" Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" formnovalidate ToolTip="Activar" CommandName="CmdActivar"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Municipios Asignados" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Municipios">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgMunicipios" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Municipios" CommandName="CmdMunicipios"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>

                                <div class="ibox-title" runat="server" id="DivMunicipios" visible="false">
                                    <h2><strong>Asignación de Municipios a SubRegiones</strong></h2>
                                    <input type="button" runat="server" id="BtnNuevoMun" title="Nueva" class="btn btn-primary" value="Nueva" />
                                </div>
                                <div runat="server" id="DivAddMunicipios" visible="false">
                                    <div class="ibox-content">
                                        <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                            <div class="col-sm-3"><telerik:RadComboBox runat="server" ID="CboDepartamento" AutoPostBack="true" Width="200px" Enabled="false"></telerik:RadComboBox></div>
                                        </div>
                                        <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                            <div class="col-sm-4"><telerik:RadComboBox runat="server" ID="CboMunicipio"  Width="300px" Enabled="false"></telerik:RadComboBox></div>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-content">
                                        <input type="button" runat="server" id="BtnGrabarMunicipio" title="Grabar" class="btn btn-primary" value="Grabar" />
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-8">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrMunicipio" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeErrMun" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="alert alert-success alert-dismissable" runat="server" id="DivNoErrMunicipio" visible="false">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeNoErrMun" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdMunicipios" Skin="MetroTouch" Visible="false"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="MunicipioId,SubRegionId,Municipio,Departamento,EstadoSubregionMunicipioId " NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="MunicipioId" UniqueName="MunicipioId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SubRegionId" UniqueName="SubRegionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Departamento" UniqueName="Departamento" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderText="Ubicacion" HeaderStyle-Width="400px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Municipio" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Municipio" HeaderText="Sub región"  HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EstadoSubregionMunicipioId" UniqueName="EstadoSubregionMunicipioId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Activar/Inactivar" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgInactivarMun" Visible="false" ImageUrl="~/Imagenes/24x24/ok.png" formnovalidate ToolTip="Inactivar" CommandName="CmdInac"/>
                                                            <asp:ImageButton runat="server" ID="ImgActivarMun" Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" formnovalidate ToolTip="Activar" CommandName="CmdActivar"/>
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
                <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Windows>
                    <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                        Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWindowDetalle" Modal="true"  Height="420px" Width="850px"  ShowContentDuringLoad="false" Title="Polígono" Behaviors="Close">
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtRegionId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtNoRegion" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtSubRegionId" Visible="false"></asp:TextBox>

 <%--       </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
