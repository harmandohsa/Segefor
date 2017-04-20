<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpresionPoligono.aspx.cs" Inherits="SEGEFOR.Mapas.ImpresionPoligono" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>                    
    <script type="text/javascript">
        var map2;
        var contador;

        //-	Función para cargar el polígono en el mapa en el bing
        function ver_mapa() {
            map2 = new VEMap("Mapa2");
            map2.LoadMap();
            var request = "WSMapas.svc/obtenerPoligonos/" + document.getElementById('idproyecto').value + "," + document.getElementById('Nopoligono').value;
            var layer = new VEShapeLayer();

            var layerSpec = new VEShapeSourceSpecification(
                VEDataType.GeoRSS, request, layer);

            //-	La Función que dentro de este proceso es para eliminar el identificador o chincheta del polígono  
            map2.ImportShapeLayerData(layerSpec, function () {
                var numshape = layer.GetShapeCount();
                for (var i = 0; i < numshape; i++) {
                    var s = layer.GetShapeByIndex(i);
                    s.SetCustomIcon("");
                }

            }, true);

            return false;
        }

        //-	Función para colocarle los números a los vértices del polígono
        function insertar_vertice() {
            contador = document.getElementById('cantidadf').value;
            /*Colocar chicheta en el vertice del poligono*/
            for (i = 0; i <= contador; i++) {

                var pushipin = new VEShape(VEShapeType.Pushpin, new VELatLong(tposix[i], tposiy[i]));
                pushipin.SetCustomIcon("<div style='color:black'> <div class='text'><b>" + tnumero[i] + " </b></ div> </ div> ");
                map2.AddShape(pushipin);
            }
        }

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
        .style1
        {
            width: 867px;
        }
        .style2
        {
            height: 25px;
            width: 625px;
        }
        .style3
        {
            width: 215px;
        }
        .style4
        {
            width: 625px;
        }
    </style>
</head>
<body>
    <form id="ImpresionPoligono" runat="server">
        <input type="hidden" id="Nopoligono" runat="server" />
        <input type="hidden" id="idproyecto" runat="server"/>          
       <input type="hidden" id="cantidadf"  runat="server"/>                                  
       <input type="hidden" id="cantidadg"  runat="server"/> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                            <asp:ScriptReference Path="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3" />
                <asp:ScriptReference Path="~/Mapas/ImpresionPoligono.aspx" />
            </Scripts>
        </asp:ScriptManager>

-	En maquetado de la presentación de impresión

        <table border="1" style=" width: 1239px; height: 786px; left: 0px; top: 0px;">
            <tr>
                <td>
                    <table border="0">
                        <tr>
                            <td>
                                <table style="width: 869px; height: 580px; left: 0px; top: 0px;">
                                    <tr>
                                        <td align ="left" class="style1">
                                            <table border="1"style="position:relative;  width: 850px; height: 575px; left: 0px; top: 0px;" >
                                                <tr>
                                                    <td>
                                          <div id='Mapa2' style="position:relative; width: 850px; height: 575px; left: 0px; top: 0px;">
                                            </div>
                                            </td>
                                            </tr>
                                            </table>               
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <div style="height: 45px; width: 855px">
                                                <table >
                                                    <tr>
                                                        <td style="width:150px; height: 44px;">
                                                            
                                                            <asp:Image ID="Image1" runat="server" Height="39px" 
                                                                ImageUrl="~/imagenes/Norte.png" Width="42px" />
                                                            
                                                        </td>
                                                        <td style="width:695px; height: 44px;">                                                                                                                                                                                  
                                                           <asp:Label ID="Label1" runat="server" Text="Poligono No:   " 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>  
                                                            <asp:Label ID="Lblpoligno" runat="server" Font-Italic="False" Font-Bold="True" 
                                                                Font-Size="Large"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                 <asp:Label ID="Label2" runat="server" Text="Area Total del Proyecto:" 
                                                                  Font-Bold="True" Font-Italic="False" ></asp:Label>&nbsp; 
                                                                  <%--<asp:Label ID="lblareatotal" runat="server" Font-Italic="False" Font-Bold="True" 
                                                                Font-Size="Large"></asp:Label>--%>
                                                                    
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                           <%-- <table border="1">
                                                <tr >
                                                    <td style="width: 215px; height: 25px; ">
                                                     <asp:Label ID="propiedad" runat="server" Text="TITULAR DEL PROYECTO" 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>                                                        
                                                    </td>
                                                    <td class="style2">
                                                       <asp:Label ID="Lbltitular" runat="server" Font-Italic="False" ></asp:Label> 
                                                    </td>
                                                </tr>
                                                <tr >
                                                    <td style="width: 215px; height: 25px; ">
                                                     <asp:Label ID="ubicacion" runat="server" Text="UBICACIÓN" 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>     
                                                    </td>
                                                    <td class="style2">
                                                       <asp:Label ID="lbldepto" runat="server" Font-Italic="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr >
                                                    <td style="width: 215px; height: 25px; ">
                                                     <asp:Label ID="tipoproyecto" runat="server" Text="TIPO COMPROMISO" 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>     
                                                    </td>
                                                    <td class="style2">
                                                       <asp:Label ID="lbltipoproyecto" runat="server" Font-Italic="False"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 215px; height: 25px; ">
                                                     <asp:Label ID="expediente" runat="server" Text="No. DE EXPEDIENTE" 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>     
                                                    </td>
                                                    <td class="style2">
                                                       <asp:Label ID="Lblexpediente" runat="server" Font-Italic="False" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 215px; height: 25px; ">
                                                     <asp:Label ID="lblfase1" runat="server" Text="FASE" 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>     
                                                    </td>
                                                    <td class="style2">
                                                       <asp:Label ID="Lblfaseproyecto" runat="server" Font-Italic="False" ></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td class="style3">
                                                     <asp:Label ID="Lblarea1" runat="server" Text="ÁREA (Has.)" 
                                                            Font-Bold="True" Font-Italic="False" ></asp:Label>     
                                                    </td>
                                                    <td class="style4">
                                                        <table >
                                                            <tr>
                                                                <td>
                                                                <asp:Label ID="lbltotalarea" runat="server" Font-Italic="False" ></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;&nbsp;                                                                                                                               
                                                                </td>
                                                                <td>                                                                    
                                                                    <asp:Label ID="Lblareadesct" runat="server" Text="ÁREA Descuento (Has.)" 
                                                                     Font-Bold="True" Font-Italic="False" Font-Size="Medium" Visible="False"  ></asp:Label> &nbsp;&nbsp;     
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Lblareades" runat="server" Font-Italic="False" Visible="False" ></asp:Label>                                                                   
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;&nbsp;                                                                                                                                 
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Lblareaefectiva" runat="server" Text="ÁREA Efectiva (Has.)" 
                                                                     Font-Bold="True" Font-Italic="False" Font-Size="Medium" Visible="False"></asp:Label> &nbsp;&nbsp;     
                                                                </td>
                                                                <td>
                                                                   <asp:Label ID="Lblareaefecto" runat="server" Font-Italic="False" 
                                                                        Visible="False" ></asp:Label> 
                                                                </td>
                                                            </tr>
                                                        </table>                                                       
                                                    </td>
                                                </tr>
                                            </table>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           <td>
                                <table style="width: 354px">
                                    <tr>
                                        <td>
                                        <table>
                                            <tr>
                                            <td style="width:250px;height:85px;" align="left" >
                                         <asp:Label ID="titulo" runat="server" Text="PLANO DE UBICACIÓN" Font-Bold="True" Font-Size="Large"></asp:Label>
                                         <%--<div><asp:Label ID="tipoincentivo" runat="server" Text="Tipo de incentivo: PINPEP" Font-Bold="True" Font-Italic="False"></asp:Label></div>  --%>
                                          <div><asp:Label ID="proyeccion" runat="server" Text="Proyección: GTM" Font-Bold="False"></asp:Label></div>  
                                           <div><asp:Label ID="datun" runat="server" Text="Datum: WGS84" Font-Bold="False"></asp:Label></div>   
                                        </td>  
                                           <td style="width:90px;height:85px;" align="center" >
                                               <asp:Image ID="logo" runat="server" style="width:89px;height:84px;" 
                                                   ImageUrl="~/imagenes/logoPeq.jpg"  />
                                           </td>                                                                                             
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                           <table border="1" style="width:350px; height:575px;" >
                                               <tr>
                                                   <td>
                                                    <asp:GridView ID="GvistaPuntos" runat="server" CaptionAlign="Top" HorizontalAlign="Center" 
                                                        EnableModelValidation="True" Font-Size="Medium" Height="264px" Width="258px"> 
                                                        <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle 
                                                        HorizontalAlign="Center" VerticalAlign="Middle" /> 
                                                        <HeaderStyle BorderStyle="Solid" HorizontalAlign="Center" 
                                                            VerticalAlign="Middle" />
                                                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <SelectedRowStyle 
                                                        HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>   
                                                   </td>
                                               </tr>
                                           </table>                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="1" style="width:350px;height:75px;" align="left">
                                             <tr>
                                                 <td>
                                           <div align="left">    
                                          <%--<asp:Label ID="verificapor" runat="server" Text="Verificado Por:" Font-Bold="True" Font-Italic="False"></asp:Label>&nbsp;&nbsp;
                                           <asp:Label ID="lbltecnico" runat="server" Font-Bold="True" Font-Italic="False" 
                                                   style="font-size: small"></asp:Label>--%>
                                          </div>                                          
                                          </td>                                            
                                          </tr>                                                                                                                                                                                                                         
                                          </table>                                                                                                                                                                                                                                                                             
                                        </td>
                                    </tr>
                                </table> 
                                <%--<div>&nbsp;<asp:Label ID="fechade" runat="server" Font-Bold="True" 
                                        Font-Italic="False"></asp:Label></div>    --%>                                                                                                                    
                            </td>                             
                        </tr>                        
                    </table>
                </td>
            </tr>            
          </table>     
    </form>
</body>
</html>
