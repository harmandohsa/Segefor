<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wfrm_AnexosPlanManejo.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_AnexosPlanManejo" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SEGEFOR</title>
    

    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table>
            <tr>
                <td>
                    <h3><asp:Label runat="server" ID="LblNug"></asp:Label></h3>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Croquis de acceso a la finca desde el casco municipal</label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="GrdAnexoCroquia" Skin="MetroTouch"
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                        GridLines="Both" PageSize="1" >
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="NombreAnexoCroquis,PathAnexoCroquis" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NombreAnexoCroquis" UniqueName="NombreAnexoCroquis" HeaderText="Archivo" HeaderStyle-Width="725px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PathAnexoCroquis" UniqueName="PathAnexoCroquis" Visible="false" HeaderText="FullPath" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Ver" UniqueName="Ver">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgverDoc" ImageUrl="~/Imagenes/24x24/search.png" formnovalidate ToolTip="Ver" CommandName="CmdVer"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Mapa de uso actual y recursos hidricos</label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="GrdAnexoMapaUsoActual" Skin="MetroTouch"
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                        GridLines="Both" PageSize="1" >
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="NombreAnexoMapaUsoActual,PathAnexoMapaUsoActual" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NombreAnexoMapaUsoActual" UniqueName="NombreAnexoMapaUsoActual" HeaderText="Archivo" HeaderStyle-Width="725px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PathAnexoMapaUsoActual" UniqueName="PathAnexoMapaUsoActual" Visible="false" HeaderText="FullPath" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Ver" UniqueName="Ver">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgverDocMapa" ImageUrl="~/Imagenes/24x24/search.png" formnovalidate ToolTip="Ver" CommandName="CmdVer"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Mapa de pendientes</label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="GrdAnexoMapaPendiente" Skin="MetroTouch"
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                        GridLines="Both" PageSize="1" >
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="NombreAnexoMapaPendiente,PathAnexoMapaPendiente" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NombreAnexoMapaPendiente" UniqueName="NombreAnexoMapaPendiente" HeaderText="Archivo" HeaderStyle-Width="725px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PathAnexoMapaPendiente" UniqueName="PathAnexoMapaPendiente" Visible="false" HeaderText="FullPath" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Ver" UniqueName="Ver">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgverDocMapaPendiente" ImageUrl="~/Imagenes/24x24/search.png" formnovalidate ToolTip="Ver" CommandName="CmdVer"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Mapa  Integral (rodalización del área de manejo y ubicación de parcelas de muestreo y protección)</label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="GrdAnexoMapaUbicacion" Skin="MetroTouch"
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                        GridLines="Both" PageSize="1" >
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="NombreAnexoMapaUbicacion,PathAnexoMapaUbicacion" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NombreAnexoMapaUbicacion" UniqueName="NombreAnexoMapaUbicacion" HeaderText="Archivo" HeaderStyle-Width="725px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PathAnexoMapaUbicacion" UniqueName="PathAnexoMapaUbicacion" Visible="false" HeaderText="FullPath" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Ver" UniqueName="Ver">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgverDocMapaUbicacion" ImageUrl="~/Imagenes/24x24/search.png" formnovalidate ToolTip="Ver" CommandName="CmdVer"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Mapa de rondas corta fuegos perimetrales e intermedias dentro del area de compromiso de repoblacion forestal</label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="GrdAnexoMapaRonda" Skin="MetroTouch"
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                        GridLines="Both" PageSize="1" >
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="NombreAnexoMapaRonda,PathAnexoMapaRonda" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NombreAnexoMapaRonda" UniqueName="NombreAnexoMapaRonda" HeaderText="Archivo" HeaderStyle-Width="725px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PathAnexoMapaRonda" UniqueName="PathAnexoMapaRonda" Visible="false" HeaderText="FullPath" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Ver" UniqueName="Ver">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgverDocMapaRonda" ImageUrl="~/Imagenes/24x24/search.png" formnovalidate ToolTip="Ver" CommandName="CmdVer"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <h3><label class="col-sm-5 control-label centradolabel">Poligonos</label></h3>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="GrdInmueblePol" Skin="MetroTouch" PageSize="20" 
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                            AllowPaging="true" GridLines="Both" >
                                                                        
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="InmuebleId,Departamento,Municipio,Direccion,Finca,Area" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="InmuebleId" Visible="false" HeaderText="Departamnto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Departamento" HeaderText="Departamento" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Municipio" HeaderText="Municipio" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Direccion" HeaderText="Ubicación" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Finca" HeaderText="Finca" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Area" HeaderText="Área" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Polígono Finca" Visible="true" UniqueName="PolFinca">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPolFinca" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Polígono Finca" CommandName="CmdPolFinca"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                                <telerik:GridTemplateColumn HeaderText="Polígono Área Bosque" Visible="true" UniqueName="PolFincaBosque">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPolBosque" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Polígono Área Bosque" CommandName="PolFincaBosque"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                                <telerik:GridTemplateColumn HeaderText="Polígono Área Intervenir" Visible="true" UniqueName="PolFincaIntervenir">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPolIntervenir" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Polígono Área Intervenir" CommandName="PolFincaIntervenir"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                                <telerik:GridTemplateColumn HeaderText="Polígono Área Protección" Visible="true" UniqueName="PolFincaProteccion">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPolProteccion" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Polígono Área Protección" CommandName="PolFincaProteccion"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <h4><label class="col-sm-6 control-label centradolabel">Poligono de Repoblación</label></h4>
                    <asp:ImageButton runat="server" ID="ImgPolRepoblacion" ImageUrl="~/Imagenes/24x24/ubication.png" />
                </td>
            </tr>
            <tr>
                <td>
                    <h4><label class="col-sm-5 control-label centradolabel">Censo / Muestreo</label></h4>
                    <asp:ImageButton runat="server" ID="ImgPrintCenso" ImageUrl="~/Imagenes/24x24/blank.png" />
                </td>
            </tr>
        </table>
    
        <div class="ibox-content" runat="server" >
            <div class="col-sm-10">
                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPoligonoPrint" visible="false">
                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    <asp:Label runat="server" ID="LblErrPoligono" Font-Bold="true"></asp:Label>
                </div>
                <div class="alert alert-success alert-dismissable" runat="server" id="Div13" visible="false">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    <asp:Label runat="server" ID="Label14" Font-Bold="true">Error</asp:Label>
                </div>
            </div>
        </div>
        <asp:TextBox runat="server" ID="TxtId" Visible="false"></asp:TextBox>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="RadVerAnexo" runat="server" Modal="true" Width="800px"
                    Height="600px" Title="Anexos" Behaviors="Default">
                </telerik:RadWindow>
                <telerik:RadWindow runat="server" ID="RadWinCenso" Modal="true" Height="420px" Width="650px" Title="Censo/Muestro" Behaviors="Default">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <asp:TextBox runat="server" ID="TxtTipo" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
