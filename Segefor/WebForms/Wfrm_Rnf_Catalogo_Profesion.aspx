<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Rnf_Catalogo_Profesion.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Rnf_Catalogo_Profesion" %>
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
                                <h2><strong>Catálogo de Profesiones</strong></h2>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label">Profesión</label>
                                    <div class="col-sm-11"><asp:TextBox runat="server" ID="TxtProfesion" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Grado Académico:</label>
                                    <div class="col-sm-5"><telerik:RadComboBox ID="CboCategoria" Width="100%" runat="server" required=""></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Estatus:</label>
                                    <div class="col-sm-5"><telerik:RadComboBox ID="CboEstatus" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div class="col-sm-1">
                                    <a class="btn btn-primary m-b"  runat="server" id="BtnNuevo">Nuevo</a>
                                </div>
                                <div class="col-sm-1">
                                    <asp:Button runat="server" Text="Grabar"  ID="BtnGrabar" class="btn btn-primary" />
                                </div>
                                
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div class="col-sm-9">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="BtnEror" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                                    </div>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="BtnGood" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblGood" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>	
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                               <telerik:RadGrid runat="server" ID="GrdProfesion"  Skin="MetroTouch" PageSize="10" 
                                    AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        AllowPaging="true" GridLines="Both" AllowFilteringByColumn="true" >
                                    <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                        PrevPageText="Anterior" Position="Bottom" 
                                        PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                        PageSizeLabelText="Regitros"/>
                                    <MasterTableView Caption="" DataKeyNames="ProfesionId,Profesion,CategoriaProfesion,EstatusProfesion" NoMasterRecordsText="Sin Registros" ShowFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ProfesionId" Visible="false" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Profesion" FilterControlWidth="500px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"  HeaderText="Profesión" HeaderStyle-Width="525px"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CategoriaProfesion" AllowFiltering="false" ShowFilterIcon="false"  HeaderText="Grado Académico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EstatusProfesion" HeaderText="Estatus" AllowFiltering="false" ShowFilterIcon="false"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Editar" Visible="true" AllowFiltering="false" ShowFilterIcon="false"  UniqueName="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgEdit" CausesValidation="false" formnovalidate ImageUrl="~/Imagenes/24x24/pencil.png" ToolTip="Editar" CommandName="CmdEdit"/>
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
            <asp:TextBox runat="server" Visible="false" ID="TxtProfesionId"></asp:TextBox>
            <asp:TextBox runat="server" Visible="false" ID="TxtOriginalProfesion"></asp:TextBox>
            <asp:TextBox runat="server" Visible="false" ID="TxtOriginalCategoriaId"></asp:TextBox>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        <Windows>
            <telerik:RadWindow runat="server" ID="WindowAsk" Modal="true" Height="250px" Width="400px" Title="Confirmación Modificación Profesión" Behaviors="None">
                <ContentTemplate>
                    <asp:Label ID="Label9" Text="Está seguro de este cambio ya que afectara a todos los registros que tengan este dato asignado" runat="server" />
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ask.png" />
                            </td>
                            <td>
                                <asp:Button ID="BtnYes" Text="Si" runat="server" Width="40px" />
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text="        "></asp:Label>   
                            </td>
                            <td>
                                <asp:Button ID="BtnNo" Text="No" runat="server" Width="40px" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
