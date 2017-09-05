<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Enmiendas.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Enmiendas" %>
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
                            <h2><strong>Enmiendas </strong></h2>
                        </div>
                        <div class="ibox-content">
                            <telerik:RadGrid runat="server" ID="GrdSolicitudes" Skin="Telerik"
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
                                            <telerik:GridBoundColumn DataField="" UniqueName="No_Exp" HeaderText="No. de Expediente."  HeaderStyle-Width="150px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="" UniqueName="Fecha_Exp" HeaderText="Fecha adm. Expediente."  HeaderStyle-Width="125px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NUG" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="NUG" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Fecha" UniqueName="Fecha" HeaderText="Fecha de Gestión" HeaderStyle-Width="175px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="nombres"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="nombres" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="dpi"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  UniqueName="dpi" HeaderText="Doc. Identificación" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="" UniqueName="actividad" HeaderText="Actividad" HeaderStyle-Width="300px" ShowFilterIcon="false" AllowFiltering="false"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Ver información" Visible="false" UniqueName="VerInfo" ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                    </ItemTemplate>
                                            </telerik:GridTemplateColumn> 
                                            <telerik:GridTemplateColumn HeaderText="Enviar a Propietario" Visible="false" UniqueName="Seg"  ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false" AllowFiltering="false">
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
        <asp:TextBox runat="server" ID="TxtGestionId" Visible="false"></asp:TextBox>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                    Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                </telerik:RadWindow>
                <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="220px" Width="350px" Title="Confirmación" Behaviors="Close">
                <ContentTemplate>
                    <asp:Label ID="LblTitConfirmacion" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                    <br />
                    <br />
                    <div class="ibox-content">
                        <div class="col-sm-3">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ask.png" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Button runat="server" Text="Sí"  ID="BtnYes" data-loading-text="Enviando..."  class="btn btn-primary" />
                        </div>
                        <%--<div class="col-sm-2">
                            <asp:Button runat="server" Text="No"  ID="BtnNo" class="btn btn-primary" />
                        </div>--%>
                    </div>
                            
                </ContentTemplate>
            </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </ContentTemplate>
</asp:UpdatePanel>
    <script>
        function pageLoad() {
            $('#<%=BtnYes.ClientID%>').click(function () {
                $(this).button('loading').delay(100000).queue(function () {
                    $(this).button('reset');
                    $(this).dequeue();
                    $(this).data('loading-text', 'Cargando...');
                });
            });
        }
    </script>
</asp:Content>
