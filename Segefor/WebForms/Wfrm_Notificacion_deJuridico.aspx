<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Notificacion_deJuridico.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Notificacion_deJuridico" %>
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
                                    <h2><strong>Notificaciones de Jurídico/Técnico</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdSolicitudes" Skin="Telerik"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="GestionId,NUG,Fecha,nombres,ModuloId" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="GestionId" UniqueName="RegionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ModuloId" UniqueName="ModuloId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="DictamenTecnicoId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="Dictamen_JuridicoId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="No_Dictamen_Juridico" ShowFilterIcon="false" AllowFiltering="false"  HeaderText="No. de Dictamen Jurídico."  HeaderStyle-Width="150px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="No_Dictamen_Técnico" ShowFilterIcon="false" AllowFiltering="false"  HeaderText="No. de Dictamen Técnico."  HeaderStyle-Width="150px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="No_Exp" ShowFilterIcon="false" AllowFiltering="false"  HeaderText="No. de Expediente."  HeaderStyle-Width="150px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="Fecha_Exp" HeaderText="Fecha adm. Expediente." ShowFilterIcon="false" AllowFiltering="false"   HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUG" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="NUG" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" ShowFilterIcon="false" AllowFiltering="false"  UniqueName="Fecha" HeaderText="Fecha de Gestión" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombres" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="nombres" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="actividad" ShowFilterIcon="false" AllowFiltering="false"  HeaderText="Actividad" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver información" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="Seg" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Seguimiento" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="Seg"  ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgSeg" ImageUrl="~/Imagenes/24x24/next.png" formnovalidate ToolTip="Seguimiento" CommandName="CmdSeg"/>
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
                    
                </Windows>
            </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtGestionId" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
