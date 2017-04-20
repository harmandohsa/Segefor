<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatosPoligono.aspx.cs" Inherits="SEGEFOR.Mapas.DatosPoligono" %>

<head>
    <style type="text/css">
        .style1
        {
            text-decoration: underline;
            }
        #form1
        {
            height: 329px;
            width: 232px;
        }
        #form2
        {
            width: 212px;
        }
    </style>
</head>

<form id="form2" runat="server">
<span class="style1"><b>Datos del Proyecto<br />
</b>
</span>
<br />
<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
<br />
<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
<br />
<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
<br />
<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
<br />
<asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
<br />
<br />
<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
<br />
<br />
<asp:TextBox ID="txtdatopoli" runat="server" Height="87px" ReadOnly="True" 
    Rows="2" style="font-family: Arial" TextMode="MultiLine" Width="202px"></asp:TextBox>
</form>