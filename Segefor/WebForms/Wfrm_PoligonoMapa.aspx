<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wfrm_PoligonoMapa.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_PoligonoMapa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyAysF0fCHiqJj9s0d8iUNVhcnEMVlrAMbU"></script>
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var map;
        var MapCitys = [];

        function attachPolygonInfoWindow(polygon,Correlativo) {
            var infoWindow = new google.maps.InfoWindow();
            google.maps.event.addListener(polygon, 'mouseover', function (e) {
                infoWindow.setContent(Correlativo);
                var latLng = e.latLng;
                infoWindow.setPosition(latLng);
                infoWindow.open(map);
            });
        }


        function CreatingPolygon(GrpName, ArName) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Wfrm_PoligonoMapa.aspx/BindMapPoints",
                data: '{name: "' + GrpName + '",name1: "' + ArName + '"}',
                dataType: "json",
                success: function (data) {
                    var mapProp = {
                        center: new google.maps.LatLng(data.d[0].Latitude, data.d[0].Longitude),
                        zoom: 14,
                        mapTypeId: google.maps.MapTypeId.SATELLITE
                    };
                    map = new google.maps.Map(document.getElementById("idgoogleMap"), mapProp);

                    var TriangleCoordList = [];
                    var Correlativo = 0;
                    var CorrelativoTemp = 0;

                    for (var i = 0; i < data.d.length; i++) {
                        Correlativo = data.d[i].Correlativo;
                        if ((Correlativo != CorrelativoTemp) && (CorrelativoTemp > 0))
                        {
                            
                            TriangleCoordList.push(triangleCoords);
                            bermudaTriangle = new google.maps.Polygon({
                                paths: TriangleCoordList,
                                strokeColor: "#100E49",
                                strokeOpacity: 0.8,
                                strokeWeight: 3,
                                fillColor: "#5E7B0D",
                                fillOpacity: 0.35
                                
                            });
                            attachPolygonInfoWindow(bermudaTriangle, CorrelativoTemp)
                            bermudaTriangle.setMap(map);
                            MapCitys.push(bermudaTriangle);
                            CorrelativoTemp = Correlativo;
                            TriangleCoordList = [];
                            var triangleCoords = new google.maps.LatLng(data.d[i].Latitude, data.d[i].Longitude);
                            TriangleCoordList.push(triangleCoords);
                        }
                        else
                        {
                            var triangleCoords = new google.maps.LatLng(data.d[i].Latitude, data.d[i].Longitude);
                            TriangleCoordList.push(triangleCoords);
                            CorrelativoTemp = Correlativo;
                            if (i + 1 == data.d.length)
                            {
                                TriangleCoordList.push(triangleCoords);
                                bermudaTriangle = new google.maps.Polygon({
                                    paths: TriangleCoordList,
                                    strokeColor: "#100E49",
                                    strokeOpacity: 0.8,
                                    strokeWeight: 3,
                                    fillColor: "#5E7B0D",
                                    fillOpacity: 0.35,
                                    text: "prueba"
                                });
                                attachPolygonInfoWindow(bermudaTriangle, CorrelativoTemp)
                                bermudaTriangle.setMap(map);
                                MapCitys.push(bermudaTriangle);
                            }
                        }
                        
                    }

                    
                },
                error: function (result) {
                    alert("Error");
                }
            });
        };
        
        google.maps.event.addDomListener(window, 'load', CreatingPolygon('', ''));

    </script>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
            
        </asp:ScriptManager>
        
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
                                                        <h3><asp:Label runat="server" ID="LblTitulo" Text="Poligono de Finca"></asp:Label></h3>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="idgoogleMap" style="width: 925px; height: 600px" />
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
                                                        <td>
                                                            <asp:Label runat="server" ID="LblFinca" Text="Finca:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="LblArea" Text="Área:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
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
                                                  
                                                   </td>
                                               </tr>
                                           </table>                                           
                                        </td>
                                    </tr>
                                </table> 
                            </td>                             
                        </tr>                        
                    </table>
                </td>
            </tr>            
          </table>    
        
    </form>
</body>
</html>
