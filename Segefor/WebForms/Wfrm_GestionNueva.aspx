<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_GestionNueva.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_GestionNueva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
&nbsp;&nbsp;&nbsp;
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h2><strong>Gestiones nuevas</strong></h2>
                        </div>
                        <div runat="server" id="DivOpcionesPubGen" visible="false">
                            <div class="ibox-content">
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;">Manejo Forestal</label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;">Incentivos Forestales</label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;">Registro Nacional Forestal</label>
                                </div>
                            </div>
                            <div class="ibox-content">
                               <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;"><asp:ImageButton runat="server" PostBackUrl="~/WebForms/Wfrm_ManejoForestal.aspx" Width="200px" Height="200px" ImageUrl="~/Imagenes/manejoforestal.jpg" ID="ImgApro"  class="img-circle"/></label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;"><asp:ImageButton runat="server" Width="200px" Height="200px" ImageUrl="~/Imagenes/incentivosforestales.jpg" ID="ImgIncentivos"  class="img-circle" PostBackUrl="~/WebForms/Wfrm_EnConstruccion.aspx" /></label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;"><asp:ImageButton runat="server" PostBackUrl="~/WebForms/Wfrm_RegistroForestal.aspx" Width="200px" Height="200px" ImageUrl="~/Imagenes/registronacionalforestal.jpg" ID="ImageButton2"  class="img-circle" /></label>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;">Exportación de productos forestales (VUPE)</label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;">Notas de Envio</label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;">Gestiones varias</label>
                                </div>
                            </div>
                            <div class="ibox-content">
                               <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;"><asp:ImageButton runat="server" Width="200px" Height="200px" ImageUrl="~/Imagenes/exportaciondeproductosforestales_vupe.jpg" ID="ImgExportacion"  class="img-circle" PostBackUrl="~/WebForms/Wfrm_EnConstruccion.aspx"/></label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;"><asp:ImageButton runat="server" Width="200px" Height="200px" ImageUrl="~/Imagenes/notasdeenvio.jpg" ID="ImgNotaEnvio"  class="img-circle" PostBackUrl="~/WebForms/Wfrm_EnConstruccion.aspx" /></label>
                                </div>
                                <div>
                                    <label class="col-sm-4 control-label centradolabel" style="text-align:center;"><asp:ImageButton runat="server" Width="200px" Height="200px" ImageUrl="~/Imagenes/gestionesvarias.jpg" ID="ImgGestionVaria"  class="img-circle" PostBackUrl="~/WebForms/Wfrm_EnConstruccion.aspx" /></label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="DivOpcionesINAB" visible="false">
                            <div class="ibox-content">
                                <telerik:RadGrid runat="server" ID="GrdSolicitudes" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  AllowFilteringByColumn="true"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="GestionId,NUG,Fecha,nombres,ModuloId,dpi" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="GestionId" UniqueName="RegionId" Visible="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ModuloId" UniqueName="ModuloId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="No_Dictamen_Juridico" HeaderText="No. de Dictamen Jurídico."  ShowFilterIcon="false" AllowFiltering="false" HeaderStyle-Width="150px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="No_Providencia" HeaderText="No. de Providencia."  HeaderStyle-Width="150px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="No_Exp" HeaderText="No. de Expediente."  HeaderStyle-Width="150px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="Fecha_Exp" HeaderText="Fecha adm. Expediente."  HeaderStyle-Width="125px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUG" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="NUG" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" UniqueName="Fecha" HeaderText="Fecha de Gestión" HeaderStyle-Width="175px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombres"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="nombres" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="dpi"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="dpi" HeaderText="Doc. Identificación" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="" UniqueName="actividad" HeaderText="Actividad" HeaderStyle-Width="300px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver información" Visible="true" UniqueName="VerInfo" ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Seguimiento" Visible="true" UniqueName="Seg"  ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgSeg" ImageUrl="~/Imagenes/24x24/next.png" formnovalidate ToolTip="Seguimiento" CommandName="CmdSeg"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Ver Solicitud Completación" Visible="true" UniqueName="SolComple"  ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgSolComple" ImageUrl="~/Imagenes/24x24/detalle.png" formnovalidate ToolTip="Seguimiento" CommandName="CmdSolComple"/>
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
                </Windows>
            </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtGestionId" Visible="false"></asp:TextBox>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
