<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuMapas.aspx.cs" Inherits="SEGEFOR.MapasPlanManejo.MenuMapas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   
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
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" >
    </telerik:RadWindowManager>
    <form id="frmMapas" runat="server">
    
   
    <div style="width:1200px; height:500px; margin-right:auto; margin-left:auto;">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3"/> 
               <%-- <asp:ScriptReference Path="../Mapas/MenuMapas.aspx" />--%>
                <%--<asp:ScriptReference Path="../Scripts/jSMapas.js" />--%>
                <asp:ScriptReference Path="~/Scripts/JSMapas.js" />
            </Scripts>
        </asp:ScriptManager>
               

    <script src="../Scripts/jquery-1.8.2.js" type="text/javascript"></script>

<%--
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

        
    </script>--%>


   <%-- <script type="text/javascript">
        /* funcion para cargar el mapa de bing */
        function pagina() {
            var map;
            var Guate = new VELatLong(15.5, -90.25);

            map = new VEMap("Mapa");
            map.LoadMap(Guate, 8, VEMapStyle.Road);
            return false;
        }

        /* mensajes de alerta para cada una de las funciones que se ejecutan en la pagina*/
        function eliminaPol() {

            alert("El Poligono fue eliminado con éxito");
            document.location.reload();
        }

        function respuesta() {

            alert("No a seleccionado el poligono a imprimir");
        }
        function respuesta2() {

            alert("No a seleccionado el poligono a Eliminar");
        }

        /* llama al proceso de carga de poligonos para que sean visualizados manda a llamar a Mapas.svc donde carga los datos para generar los poligonos*/
           function btnPoligono_click() {
               var map1;               
                   map1 = new VEMap("Mapa");                  
               map1.LoadMap();
               var request = "WSMapas.svc/obtenerPoligonos/" + $("#txtPoligono").val() ;
               var layer = new VEShapeLayer();
               

               var layerSpec = new VEShapeSourceSpecification(
                   VEDataType.GeoRSS,
                   request,
                   layer);

               /*la funcion que se encuentra adentro de ese pocedimiento es para cambiar el identificador o chincheta del poligono y reemplazarlo con un número*/
                 map1.ImportShapeLayerData(layerSpec, function () {
                   var numshape = layer.GetShapeCount();
                   for (var i = 0; i < numshape; i++) {
                       var t = i + 1;
                       var s = layer.GetShapeByIndex(i);
                       s.SetCustomIcon("<div style='color:red'> <div class='text'><b>" + t + "</b></ div> </ div> ");
                   }

               }, true);                 

           }


        

        





    </script>--%>



    

  <%--  *************** INICIO********--/*de aquí es codigo donde se enmaqueta la forma del formulario */--%>

        <table border="10">
            <tr>

                <td align="center">
                 <label >MAPAS  DE  POLIGONOS</label>  
                 <div id='Mapa' style="position: relative; width: 800px; height: 700px; left: 0px; top: 0px;"></div>             
                </td>
                
                <td align="center">
                    <table border="10">
                        <tr>
                            <td>
                                <div id="info"style="width:400px;height:350px;" align="center" >
                                 <label >!-- Información del Proyecto --!</label>    
                                  <div>&nbsp;&nbsp;&nbsp;</div>  
                                  <div align="left">                                
                            <asp:Label ID="Lblexpediente" runat="server" Font-Italic="True" ></asp:Label>                                                                                                                                          
                            </div>                          
                             <div align="left">
                            <asp:Label ID="Lbltitular" runat="server" Font-Italic="True"></asp:Label>
                            </div>                            
                             <div align="left">
                            <asp:Label ID="lblfase" runat="server" Font-Italic="True" ></asp:Label>
                             </div>                            
                            <div align="left">
                            <asp:Label ID="lbldepto" runat="server" Font-Italic="True"></asp:Label>
                            </div>
                            
                            <div align="left">
                            <asp:Label ID="lblmuni" runat="server" Font-Italic="True" ></asp:Label>
                            </div>
                            &nbsp;&nbsp;
                            <div align="center">
                            <asp:Label ID="lbltotalarea" runat="server" Font-Bold="True" Font-Italic="True" 
                                    Font-Size="Medium" ></asp:Label>                            
                            </div>
                            &nbsp;&nbsp;
                            <div><asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" 
                                    Font-Overline="False" Font-Underline="True" >Poligonos y sus Áreas</asp:Label> </div>
                            &nbsp;&nbsp;&nbsp;                            
                            <div align="center">
                                    
                                <asp:ListBox ID="lstpoligonos" runat="server" Height="92px" AutoPostBack="True" 
                                    Width="239px"></asp:ListBox>                                                          
                            </div>
                            <label >Seleccione Poligono</label> 
                            </div>                            
                            </td>
                        </tr>
                        <tr>
                            <td align ="center">                             
                        <label >!-- Coordenadas de Poligonos --!</label>&nbsp;&nbsp;
                        <div>&nbsp;</div>
                              <div style="overflow:auto; width:390px; height:350px;" >                                                                                                                                       
                                <asp:GridView ID="GvistaPuntos" runat="server" 
                                 CaptionAlign="Top"  ShowHeader="true" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" 
                                    EnableModelValidation="True" Font-Size="Small" ShowFooter="True">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" HorizontalAlign="Center" 
                                        VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" 
                                        HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle BackColor="#1C5E55" BorderStyle="Solid" Font-Bold="True" 
                                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" 
                                        HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView> 
                                </div> 
                        </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>


        &nbsp;&nbsp; 
    <div style="width:1200px; height:30px; margin-right:auto; margin-left:auto;">
     <div id="Div2">
            <a href="#" id="a1"></a>
            <div id="Div3"></div>
        </div>
        <div style="display:block;" >
            <table >
                <tr>
                    <td>
                    
                  <%--    <input type="button" id="verpol" value="Mostrar polígonos" onclick="btnPoligono_click();" style="width: 141px" runat="server" />--%>
                                    
                      <asp:Button ID="BtnVerPuntos" runat="server" style="width: 141px" 
                            Text="Ver polígonos"     /> 
                   <%--   <asp:Button ID="verpol" runat="server" style="width: 141px" Text="Mostrar polígonos"  onclick="verpol_Click" /> &nbsp;     --%>  
                            
                    </td>
                    <td>
                      <input type="text" id="txtPoligono" runat="server" value =""  />&nbsp;&nbsp;            
                    <input type="text" id="TxtTipoMapa" runat="server" value =""  />&nbsp;&nbsp;            
                        <%--<input type="button" value="Tabla de puntos" id="btnVerPuntos" onclick="return btnVerPuntos_onclick()" />--%>             
                    </td>
                   
                    <td>
                        <asp:Button ID="btnimprimir" runat="server" Text="Imprimir Poligono" onclick="btnimprimir_Click"/>                       
                    </td>
                    <td>
                    <asp:Button ID="btnelimina" runat="server" Visible="false" Text="Eliminar Poligono" 
                             Width="121px" />&nbsp;&nbsp;
                    </td>
                    <td>
                     <asp:Button ID="btnimprimirpuntos" runat="server" 
                            Text="Imprimir Puntos de Poligono" onclick="btnimprimirpuntos_Click" 
                            Visible="False" />                                           
                    </td>
                   <td>
                      <asp:Button ID="BtnCargaExcel" runat="server" visible ="false" style="width: 141px" Text="Importar desde Excel"   /> 
              
                    </td>
                    
                   <%--  <td>
                     <asp:Button ID="btnelimina" runat="server" Text="Eliminar Poligono" 
                            onclick="btnelimina_Click" Width="121px" Visible="False" />&nbsp;&nbsp;
                        <asp:Button ID="Btninformacion" runat="server" onclick="Btninformacion_Click" Text="Información del Poligono" style="width:170px" Visible="False" />&nbsp;                
                        <%--<asp:Button ID="Btnpuntos" runat="server" onclick="BtnPuntos_Click" Text="Tabla de Puntos" Visible="False" /> &nbsp;                      <%--<input type="button" value="Información Poligono"  id="Btninformacion" />             
                    </td>
                     <td>
                     <input type="button" value="Ingreso Poligonos Descuento" Visible="False" onclick="btnImportarExcel2_onclick()"/>&nbsp;&nbsp;                     
                    </td>--%>
                </tr>                
                <tr>
                    <td>
                    &nbsp;&nbsp;&nbsp;No. de poligono:                
                    </td>
                    <td>
                     <input type="text" id= "Txtnopoligono" runat="server" value="0" /> &nbsp;              
                    </td>
                    <td>
                     <input type="text" id= "TxtCompromiso" runat="server" value="" /> &nbsp; 
                    </td>
                    <td class="style1">
                     
                    </td>
                </tr>
            </table>                                                                    
        </div>
    </div>



  <%--************** FIN ****************--%>

  </div>
  </form>
</body>
</html>