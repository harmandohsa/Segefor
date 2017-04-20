//<reference path="js/VeJavaScriptIntellisenseHelper.js" />

var map ;

/* funcion para cargar el mapa de bing */
function pagina() {
    
    //var Guate = new VELatLong(15.5, -90.25);

    //map = new VEMap("Mapa");
    //map.LoadMap(Guate, 8, VEMapStyle.Road);
    //return false;
}


//function pageLoad() {
////    map = new VEMap("Mapa");
////    map.LoadMap();

//    var Guate = new VELatLong(15.5, -90.25);

//    map = new VEMap("Mapa");
//    map.LoadMap(Guate, 8, VEMapStyle.Road);

//}

function AddPin() {
    // Add a new pushpin to the center of the map.
    pinPoint = map.GetCenter();
    pinPixel = map.LatLongToPixel(pinPoint);
    var pin = map.AddPushpin(pinPoint);
    pin.SetTitle("First Pin <span style=color:red>Demo Title<>/span>");
    pin.SetDescription("This comes to the <b>description</b> of pushpin.");
    pin.SetCustomIcon("<img src='logo.jpg' />");
    pin.SetMoreInfoURL("http://dotnetricks.blogspot.com");

}

function btnPoligono_click() {
    var Guate = new VELatLong(15.5, -90.25);
    map = new VEMap("Mapa");
    map.LoadMap(Guate, 8, VEMapStyle.Road);

    //var request = "WSMapas.svc/obtenerPoligonos/" + $("#txtPoligono").val();
    //var request = "WSMapas.svc/obtenerPoligonos/" + $("#txtPoligono").val() + "," + 0;
    var request = "WSMapas.svc/obtenerPoligonos/" + $("#txtPoligono").val() + "," + $("#Txtnopoligono").val();
    var layer = new VEShapeLayer();
    

    var layerSpec = new VEShapeSourceSpecification(
                            VEDataType.GeoRSS,
                            request,
                            layer);

    map.ImportShapeLayerData(layerSpec);


    /*la funcion que se encuentra adentro de ese pocedimiento es para cambiar el identificador o chincheta del poligono y reemplazarlo con un número*/
                 map.ImportShapeLayerData(layerSpec, function ()
                  {
                   var numshape = layer.GetShapeCount();
                   for (var i = 0; i < numshape; i++) 
                   {
                       var t = i + 1;
                       var s = layer.GetShapeByIndex(i);
                       s.SetCustomIcon("<div style='color:red'> <div class='text'><b>" + t + "</b></ div> </ div> ");
                   }

               }, true);    



    return false;
}

function btnImportarExcel_onclick() {
    var iResultado = window.showModalDialog("wfrmImportarFromExcel.aspx?ID=" + $("#txtPoligono").val(), "", "dialogHeight:100px; dialogWidth:600px; edge: Raised;location:No; ");
    alert(iResultado.val);
    if (iResultado.val > 0) {
        alert(iResultado.Mes);

//        var request = "MisMapas.svc/obtenerPoligonos/" + iResultado.val;
        var request = "MisMapas.svc/obtenerPoligonos/" + $("#txtPoligono").val() + "," + 0;
        var layer = new VEShapeLayer();

        var layerSpec = new VEShapeSourceSpecification(
                        VEDataType.GeoRSS,
                        request,
                        layer);

        map.ImportShapeLayerData(layerSpec);

        alert(iResultado.Pol);
        var iPrevPoints = iResultado.Pol.split(" ");
        var points = [];
        for (var i = 0; i < iPrevPoints.length; i++) {
            points.push(new VELatLong(iPrevPoints[i], iPrevPoints[i + 1]));
            i++;
        }

        var outlineColor = new VEColor(0, 0, 255, 1);
        var fillColor = new VEColor(0, 0, 255, .2);
        var outlineWidth = 5;
        var id = 'Nuevo';

        var poly = new VEPolygon(id, points, fillColor, outlineColor, outlineWidth)
        map.AddPolygon(poly);

    } else alert(iResultado.Mes);
}



function respuesta() {

    alert("No a seleccionado el poligono a imprimir");
}

function respuesta2() {

    alert("No a seleccionado el poligono a Eliminar");
}    
