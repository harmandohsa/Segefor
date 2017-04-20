<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Historial_Gestion.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Historial_Gestion" %>
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
                            <h2><strong>Historial de Gestiones</strong></h2>
                        </div>
                        <div class="ibox-content">
                             <h4><strong>Busquedas</strong></h4>
                             <div><label class="col-sm-2 control-label centradolabel">No. de Expediente</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoExpediente"  CssClass="form-control" ></asp:TextBox></div>
                            </div>
                            <div class="col-sm-2">
                                <a class="btn btn-primary m-b" runat="server" id="BtnBuscar">Buscar</a>
                            </div> 
                        </div>
                        <div runat="server" id="DivOpcionesINAB" visible="true">
                            <div class="ibox-content">
                                <telerik:RadGrid runat="server" ID="GrdSolicitudes" Skin="Telerik"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="GestionId,No_Resolucion,No_Dictamen_Juridico,No_Providencia,No_Expediente,Fecha_Exp,NUG,Fecha,nombres,EstatusGestion,ModuloId,dpi" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="GestionId" UniqueName="RegionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ModuloId" UniqueName="ModuloId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Constancia RNF." ShowFilterIcon="false" AllowFiltering="false" HeaderTooltip="Click para ver documento" Visible="true" UniqueName="Reg_RNF" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkRegRnf" Font-Underline="true"  CommandName="DocRnf" formnovalidate ToolTip="Clic para ver documento"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="No. de Resolución." ShowFilterIcon="false" AllowFiltering="false" HeaderTooltip="Click para ver documento" Visible="true" UniqueName="No_Resolucion" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkResolucion" Font-Underline="true"  CommandName="DocResolucion" formnovalidate ToolTip="Clic para ver documento"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="No. Oficio Devolución." ShowFilterIcon="false" AllowFiltering="false" HeaderTooltip="Click para ver documento" Visible="true" UniqueName="No_Oficio_Dev" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkOficioDev" Font-Underline="true"  CommandName="DocOficioDev" formnovalidate ToolTip="Clic para ver documento"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="No. de Dictamen Jurídico." ShowFilterIcon="false" AllowFiltering="false" HeaderTooltip="Click para ver documento" Visible="true" UniqueName="No_Dictamen_Juridico" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkDictamenJuridico" Font-Underline="true"  CommandName="DocJurudico" formnovalidate ToolTip="Clic para ver documento"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="No. de Oficio Enmiendas." ShowFilterIcon="false" AllowFiltering="false" HeaderTooltip="Click para ver documento" Visible="true" UniqueName="No_Oficio_Enmiendas" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkOficioEnmiendas" Font-Underline="true"  CommandName="DocEnmiendas" formnovalidate ToolTip="Clic para ver documento"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="No Providencia" ShowFilterIcon="false" AllowFiltering="false" HeaderTooltip="Click para ver documento" Visible="true" UniqueName="No_Providencia" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="LnkProvidencia" Font-Underline="true"  CommandName="DocProvidencia" formnovalidate ToolTip="Clic para ver documento"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridButtonColumn DataTextField="No_Expediente" HeaderTooltip="Click para ver documento" ButtonType="LinkButton" ItemStyle-Font-Underline="true" CommandName="DocAceptacion" UniqueName="No_Expediente" HeaderText="No. de Expediente." HeaderStyle-Width="150px"></telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="NUG" UniqueName="NUG" Visible="false" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" ShowFilterIcon="false" AllowFiltering="false" UniqueName="Fecha" HeaderText="Fecha de Gestión" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombres" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="nombres" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="dpi" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="dpi" HeaderText="No. Identificación" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="actividad" ShowFilterIcon="false" AllowFiltering="false" HeaderText="Actividad" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EstatusGestion" ShowFilterIcon="false" AllowFiltering="false" UniqueName="estatus" HeaderText="Estatus" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver Solicitud" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Seg" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Ver Solicitud Completación" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="SolComple"  ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgSolComple" ImageUrl="~/Imagenes/24x24/detalle.png" formnovalidate ToolTip="Seguimiento" CommandName="CmdSolComple"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    <%--<ClientSettings AllowExpandCollapse="true">
                                        <Scrolling AllowScroll="true" UseStaticHeaders="false" />
                                    </ClientSettings>--%>
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
                    <telerik:RadWindow runat="server" ID="RadWindowDetalle" Modal="true" Height="420px" Width="850px" Title="Detalle Solicitudes de Completación" Behaviors="Close">
                        <ContentTemplate>
                            <asp:Label ID="LblTitConfirmacion" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            <div class="ibox-content">
                                <telerik:RadGrid runat="server" ID="GrdDetalle" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Gestion_IncompletaId,GestionId,NUG,No_Solicitud_Completacion,Fecha" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Gestion_IncompletaId" UniqueName="Gestio_IncompletaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="GestionId" UniqueName="GestionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="No_Solicitud_Completacion" Visible="false" UniqueName="SCG" HeaderText="No. Completación gestión"  HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUG" UniqueName="NUG" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" UniqueName="Fecha" HeaderText="Fecha de Gestión" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver Constancia" Visible="true" UniqueName="Seg" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtGestionId" Visible="false"></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
