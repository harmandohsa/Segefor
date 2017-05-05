<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_SeguimientoUsuario.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_SeguimientoUsuario" %>
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
                            <h2><strong>Mis Gestiones</strong></h2>
                        </div>
                        <div runat="server" id="DivOpcionesINAB">
                            <div class="ibox-content">
                                <telerik:RadGrid runat="server" ID="GrdSolicitudes" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="GestionId,NUG,Fecha,nombres,ModuloId,Actividad" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="GestionId" UniqueName="RegionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ModuloId" UniqueName="ModuloId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUG" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="NUG" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" UniqueName="Fecha" ShowFilterIcon="false" AllowFiltering="false"  HeaderText="Fecha de Gestión" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Actividad" UniqueName="Actividad" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderText="Actividad" HeaderStyle-Width="400px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver información" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="VerInfo" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Anexos" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="Anexos" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgAnexos" Visible="false" ImageUrl="~/Imagenes/24x24/blank.png" formnovalidate ToolTip="Anexos" CommandName="CmdAnexos"/>
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
                    <telerik:RadWindow runat="server" ID="RadWinAnexos" Modal="true" Height="850px" Width="850px" Title="Censo/Muestro" Behaviors="Default">

                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtGestionId" Visible="false"></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
