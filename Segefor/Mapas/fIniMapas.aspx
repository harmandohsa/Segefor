<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fIniMapas.aspx.cs" Inherits="SEGEFOR.Mapas.fIniMapas" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnVerPuntos").click(function () {
                $.ajax(
                {
                    url: "wfrmRequestMapas.aspx",
                    data: { id: $("#txtPoligono").val() },
                    cache: false,
                    success: function (data) {
                        $("#dTablaPuntos").html(data);
                        $("#dPuntos").show();
                    }
                });
            });
            $("#aClose").click(function () {
                $("#dPuntos").hide();
            });
        });

        $(document).ready(function () {
            $("#Btninformacion").click(function () {
                $.ajax(
                {
                    url: "DatosPoligono.aspx",
                    data: { id: $("#txtPoligono").val() },
                    cache: false,
                    success: function (data) {
                        $("#dTablaPuntos").html(data);
                        $("#dPuntos").show();
                    }
                });
            });
            $("#aClose").click(function () {
                $("#dPuntos").hide();
            });
        });


    </script>
    <style type="text/css">
        #dPuntos  
        {
            position:absolute; top:100%; left:100%; margin-left:-350px; margin-top:-350px;
            width: 300px; height:300px; border:4px solid gray;  z-index:109;
            border-radius:8px; 
            display:none;
            background-color:#f9f9f9; 
            font-family:Trebuchet MS; 
            font-size:9pt; 
        }
        #dTablaPuntos 
        {
            margin-left:30px;
            margin-top:30px;
            display:block;
            width:250px;
            height:250px;
            overflow-x:hidden;
            overflow-y:auto;
        }
        .tPuntos { width: 250px; }
        .tPuntos tr td, th { border:1px solid #c9c9c9; padding:8px; } 

        #aClose  
        {
            z-index:999;
            position:absolute; 
            top:-5px; 
            left:100%; 
            margin-left:-15px;
            background-image:url('../imagenes/close.png'); 
            width:25px; 
            height:25px;
        }
        #Btninformacion
        {
            width: 155px;
        }
        #btnVerPuntos
        {
            width: 128px;
        }
        #btnImportarExcel
        {
            width: 158px;
        }
    </style>
</head>
<body>
    <form id="frmMapas" runat="server">
    <div style="width:1200px; height:500px; margin-right:auto; margin-left:auto;">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.1"  />
                <asp:ScriptReference Path="../Scripts/jSMapas.js" />
            </Scripts>
        </asp:ScriptManager>
                

        <div id="Mapa" style="position: relative; width: 1200px; height: 650px; left: 0px; top: 0px;">
        </div>
        <div id="dPuntos">
            <a href="#" id="aClose"></a>
            <div id="dTablaPuntos"></div>
        </div>
        <div style="display:block;" >
            <input type="button" value="Mostrar polígonos" onclick="btnPoligono_click();" 
                style="width: 141px" />
            <input type="text" id="txtPoligono" runat="server"  />
            <input type="button" value="Tabla de puntos" id="btnVerPuntos" />
            <input type="button" value="Importar desde Excel" id="btnImportarExcel" onclick="return btnImportarExcel_onclick()" />&nbsp;
            <input type="button" value="Información Poligono"  id="Btninformacion" />&nbsp;&nbsp;
        </div>
    </div>

  

    </form>
</body>
</html>