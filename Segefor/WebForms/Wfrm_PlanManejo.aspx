<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_PlanManejo.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_PlanManejo" %>
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
                                    <h2><strong>Administración Planes de Manejo</strong></h2>
                                </div>
                                <div class="ibox-title">
                                    <h4><strong>Planes de Manejo Solicitados</strong></h4>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdPlanesSolicitados" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Fec,Solicitante,Estatus,AsignacionId,Area,Regente" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="AsignacionId" UniqueName="AsignacionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fec" UniqueName="Fec" HeaderText="Fecha Solicitud" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitante" UniqueName="Solicitante" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Estatus" UniqueName="Estatus" HeaderText="Estatus" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Area" UniqueName="Area"  DataFormatString="{0:#,##0.00}" HeaderText="Área" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Regente" UniqueName="Regente" HeaderText="Regente" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                                <div class="ibox-title" runat="server" id="DivTitPlanProceso">
                                    <h4><strong>Planes de Manejo en Proceso</strong></h4>
                                </div>
                                <div class="ibox-content"  runat="server" id="DivPlanProceso">
                                    <telerik:RadGrid runat="server" ID="GrdPlanesSolicitadosComoRegente" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Fec,Solicitante,Estatus,AsignacionId,Area,FechaAcepta,UsuarioId,SubCategoriaId,CategoriaId,Categoria" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="UsuarioId" UniqueName="UsuarioId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SubCategoriaId" UniqueName="SubCategoriaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CategoriaId" UniqueName="CategoriaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AsignacionId" UniqueName="AsignacionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fec" UniqueName="Fec" ShowFilterIcon="false" AllowFiltering="false" HeaderText="Fecha Solicitud" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitante" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Solicitante" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Categoria" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Categoria" HeaderText="Categoria" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Estatus" ShowFilterIcon="false" AllowFiltering="false" UniqueName="Estatus" HeaderText="Estatus" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Area" ShowFilterIcon="false" AllowFiltering="false"  DataFormatString="{0:#,##0.00}" UniqueName="Area" HeaderText="Área" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaAcepta" ShowFilterIcon="false" AllowFiltering="false" UniqueName="FechaAcepta" HeaderText="Fecha de Aceptación" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Aceptar" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Ok">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgAceptar" ImageUrl="~/Imagenes/24x24/ok.png" formnovalidate ToolTip="Aceptar" CommandName="CmdOk"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Rechazar" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="No">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgRechazar" ImageUrl="~/Imagenes/24x24/cancel.png" formnovalidate ToolTip="Aceptar" CommandName="CmdNo"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Ir al Plan" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Go">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgIrPlan" ImageUrl="~/Imagenes/24x24/go.png" formnovalidate ToolTip="Ir al Plan" CommandName="CmdGo"/>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
