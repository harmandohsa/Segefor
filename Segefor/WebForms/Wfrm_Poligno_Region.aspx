<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wfrm_Poligno_Region.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Poligno_Region" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Label ID="LblTitConfirmacion" Font-Bold="true" Text="" runat="server" />
        <asp:Label ID="LblRegionId" Font-Bold="true" Text="" runat="server" Visible="false" />
        <br />
        <br />
        <div>
            <telerik:RadAsyncUpload runat="server" ID="UploadPolFinca" Culture="es-GT" MaxFileInputsCount="1" AllowedFileExtensions="xls,xlsx"></telerik:RadAsyncUpload>
            <telerik:RadButton runat="server" ID="BtnCargar" Text="Cargar"></telerik:RadButton>
            <telerik:RadButton runat="server" ID="BtnGrabar" Text="Grabar"></telerik:RadButton>
        </div>
        <div>
            <telerik:RadGrid runat="server" ID="GrdPoligono" Skin="MetroTouch"
                    AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                    GridLines="Both" >
                    <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                        PrevPageText="Anterior" Position="Bottom" 
                        PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                        PageSizeLabelText="Regitros"/>
                    <MasterTableView Caption="" DataKeyNames="X,Y" NoMasterRecordsText="No Hay Registros">
                        <Columns>
                            <telerik:GridBoundColumn DataField="X" HeaderText="X" UniqueName="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Y" HeaderText="Y" UniqueName="Y"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                        </Columns>        
                    </MasterTableView>
                    <FilterMenu EnableTheming="true">
                        <CollapseAnimation Duration="200" Type="OutQuint" />
                    </FilterMenu>
                </telerik:RadGrid>
        </div>
    </div>
    </form>
</body>
</html>
