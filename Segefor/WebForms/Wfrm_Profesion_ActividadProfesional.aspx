<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Profesion_ActividadProfesional.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Profesion_ActividadProfesional" %>
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
                                    <h2><strong>Profesión por actividad profesional</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdProfesioActividadProfesioanal"  Skin="MetroTouch" PageSize="20" 
                                        AutoGenerateColumns="false" Width="100%" AllowFilteringByColumn="true" AllowSorting="true" 
                                            AllowPaging="true" GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="CategoriaProfesionId,CategoriaProfesion,ProfesionId,Profesion,RF,EPMF,EECUT,CFSS" NoMasterRecordsText="NO APARECE REGISTRO RELACIONADO A SUS DATOS" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CategoriaProfesionId" Visible="false" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CategoriaProfesion" HeaderText="CATEGORÍA" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ProfesionId" Visible="false" HeaderText="Estado" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Profesion" FilterControlWidth="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderText="PROFESIÓN" HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RF" Visible="false" HeaderText="RF" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EPMF" Visible="false" HeaderText="EPMF" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EECUT" Visible="false" HeaderText="EECUT" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CFSS" Visible="false" HeaderText="CFSS" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="REGENTE FORESTAL" AllowFiltering="false" ShowFilterIcon="false"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" UniqueName="RFEdit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgSiRF" CausesValidation="false" formnovalidate Visible="false" ImageUrl="~/Imagenes/24x24/check.png" ToolTip="Desactivar" CommandName="CmdSiRF"/>
                                                        <asp:ImageButton runat="server" ID="ImgNoRF" CausesValidation="false" formnovalidate  Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" ToolTip="Activar" CommandName="CmdNoRF"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="ELABORADOR DE PLANES DE MANEJO FORESTAL" AllowFiltering="false" ShowFilterIcon="false"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" UniqueName="EPMFEdit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgSiEPMF" CausesValidation="false" formnovalidate Visible="false" ImageUrl="~/Imagenes/24x24/check.png" ToolTip="Desactivar" CommandName="CmdSiEPMF"/>
                                                        <asp:ImageButton runat="server" ID="ImgNoEPMF" CausesValidation="false" formnovalidate  Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" ToolTip="Activar" CommandName="CmdNoEPMF"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" AllowFiltering="false" ShowFilterIcon="false"  ItemStyle-VerticalAlign="Middle" HeaderText="ELABORADOR DE PLANES DE CAPACIDAD DE USO DE LA TIERRA" Visible="true" UniqueName="EECUTEdit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgSiEECUT" CausesValidation="false" formnovalidate Visible="false" ImageUrl="~/Imagenes/24x24/check.png" ToolTip="Desactivar" CommandName="CmdSiEECUT"/>
                                                        <asp:ImageButton runat="server" ID="ImgNoEECUT" CausesValidation="false" formnovalidate  Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" ToolTip="Activar" CommandName="CmdNoEECUT"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="CERTIFICADOR DE FUENTES SEMILLERAS Y SEMILLAS FORESTALES" AllowFiltering="false" ShowFilterIcon="false"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" Visible="true" UniqueName="CFSSEdit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgSiCFSS" CausesValidation="false" formnovalidate Visible="false" ImageUrl="~/Imagenes/24x24/check.png" ToolTip="Desactivar" CommandName="CmdSiCFSS"/>
                                                        <asp:ImageButton runat="server" ID="ImgNoCFSS" CausesValidation="false" formnovalidate  Visible="false" ImageUrl="~/Imagenes/24x24/cancel.png" ToolTip="Activar" CommandName="CmdNoCFSS"/>
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
            <asp:TextBox runat="server" ID="TxtEditar" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
