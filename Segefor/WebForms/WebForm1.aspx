<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SEGEFOR.WebForm1" %>

<!DOCTYPE html>

<html>
<head>
   <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyAysF0fCHiqJj9s0d8iUNVhcnEMVlrAMbU"></script>
    
    
    <script src="../js/jquery-2.1.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        var map;
        var MapCitys = [];

        function CreatingPolygon(GrpName, ArName) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "WebForm1.aspx/BindMapPoints",
                data: '{name: "' + GrpName + '",name1: "' + ArName + '" }',
                dataType: "json",
                success: function (data) {
                    var mapProp = {
                        center: new google.maps.LatLng(data.d[0].Latitude, data.d[0].Longitude),
                        zoom: 14,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    map = new google.maps.Map(document.getElementById("idgoogleMap"), mapProp);

                    var TriangleCoordList = [];
                    for (var i = 0; i < data.d.length; i++) {

                        var triangleCoords = new google.maps.LatLng(data.d[i].Latitude, data.d[i].Longitude);
                        TriangleCoordList.push(triangleCoords);
                    }
                    bermudaTriangle = new google.maps.Polygon({
                        paths: TriangleCoordList,
                        strokeColor: "#FF0000",
                        strokeOpacity: 0.8,
                        strokeWeight: 3,
                        fillColor: "#FF0000",
                        fillOpacity: 0.35
                    });
                    bermudaTriangle.setMap(map);
                    MapCitys.push(bermudaTriangle);
                },
                error: function (result) {
                    alert("Error");
                }
            });
        };
        google.maps.event.addDomListener(window, 'load', CreatingPolygon('', ''));

    </script>
</head>
<body>
    <form runat="server">
        <table width="100%">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnPoly" Text="Generate Polygon" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="idgoogleMap" style="width: 925px; height: 600px" />
            </td>
        </tr>
    </table>
    </form>
   
</body>
</html>
