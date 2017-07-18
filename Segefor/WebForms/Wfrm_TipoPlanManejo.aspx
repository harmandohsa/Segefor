<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_TipoPlanManejo.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_TipoPlanManejo" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        div.AddBorders .rgHeader,
        div.AddBorders th.rgResizeCol,
        div.AddBorders .rgFilterRow td,
        div.AddBorders .rgRow td,
        div.AddBorders .rgAltRow td,
        div.AddBorders .rgEditRow td,
        div.AddBorders .rgFooter td
        {
        border-style:solid;
        border-color:#aaa;
        border-width:001px1px; /*top right bottom left*/
        }
 
        div.AddBorders .rgHeader:first-child,
        div.AddBorders th.rgResizeCol:first-child,
        div.AddBorders .rgFilterRow td:first-child,
        div.AddBorders .rgRow td:first-child,
        div.AddBorders .rgAltRow td:first-child,
        div.AddBorders .rgEditRow td:first-child,
        div.AddBorders .rgFooter td:first-child,
        border-left-width:0;
        }
    </style>
      
    <script type="text/javascript">
        function Total() {
            var Forestal = document.getElementById('<%=TxtUsoForestal.ClientID%>').value;
            if (Forestal == "")
                Forestal = 0;
            var Agricultura = document.getElementById('<%=TxtUsoAgricultora.ClientID%>').value;
            if (Agricultura == "")
                Agricultura = 0;
            var Ganaderia = document.getElementById('<%=TxtUsoGanaderia.ClientID%>').value;
            if (Ganaderia == "")
                Ganaderia = 0;
            var AgroForestal = document.getElementById('<%=TxtUsoAgroForestal.ClientID%>').value;
            if (AgroForestal == "")
                AgroForestal = 0;
            var OtrosUsos = document.getElementById('<%=TxtUsoOtros.ClientID%>').value;
            if (OtrosUsos == "")
                OtrosUsos = 0;
            return parseFloat(Forestal) + parseFloat(Agricultura) + parseFloat(Ganaderia) + parseFloat(AgroForestal) + parseFloat(OtrosUsos);
        }

        function TotalSuperficie() {
            var AreaBosque = document.getElementById('<%=TxtAreaBosque.ClientID%>').value;
            if (AreaBosque == "")
                AreaBosque = 0;
            var AreaIntervenir = document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value;
            if (AreaIntervenir == "")
                AreaIntervenir = 0;
            var AreaProteccion = document.getElementById('<%=TxtAreaProteccion.ClientID%>').value;
            if (AreaProteccion == "")
                AreaProteccion = 0;
            //return parseFloat(AreaBosque) + parseFloat(AreaIntervenir) + parseFloat(AreaProteccion);
            return parseFloat(AreaBosque) + parseFloat(AreaProteccion);
        }

        function VerificaUsoForestal() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value;
            if (Area == "") {
                document.getElementById('<%=TxtUsoForestal.ClientID%>').value = "";
                document.getElementById('<%=TxtUsoPorForestal.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área total de la finca antes de ingresar el área Forestal");
            }
            else {
                var Forestal = document.getElementById('<%=TxtUsoForestal.ClientID%>').value;
                if (parseFloat(Forestal) > parseFloat(Area)) {
                    bootbox.alert("El Área Forestal no puede ser mayor que el área total");
                    document.getElementById('<%=TxtUsoForestal.ClientID%>').value = "";
                    document.getElementById('<%=TxtUsoPorForestal.ClientID%>').value = "";
                }
                else {
                    if (Total() > parseFloat(Area)) {
                        bootbox.alert("El Área de todos los usos no puede ser mayor que el área total");
                        document.getElementById('<%=TxtUsoForestal.ClientID%>').value = "";
                        document.getElementById('<%=TxtUsoPorForestal.ClientID%>').value = "";
                    }
                    else {
                        var Por = (parseFloat(Forestal) / parseFloat(Area));
                        document.getElementById('<%=TxtUsoPorForestal.ClientID%>').value = (parseFloat(Por) * 100).toFixed(2);
                    }
                }
            }
        }

        function VerificaUsoAgricultura() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value;
            if (Area == "") {
                document.getElementById('<%=TxtUsoAgricultora.ClientID%>').value = "";
                document.getElementById('<%=TxtUsoPorAgricultura.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área total de la finca antes de ingresar el área de Agricultura");
            }
            else {
                var Agricultura = document.getElementById('<%=TxtUsoAgricultora.ClientID%>').value;
                if (parseFloat(Agricultura) > parseFloat(Area)) {
                    bootbox.alert("El Área de Agricultura no puede ser mayor que el área total");
                    document.getElementById('<%=TxtUsoAgricultora.ClientID%>').value = "";
                    document.getElementById('<%=TxtUsoPorAgricultura.ClientID%>').value = "";
                }
                else {
                    if (Total() > parseFloat(Area)) {
                        bootbox.alert("El Área de todos los usos no puede ser mayor que el área total");
                        document.getElementById('<%=TxtUsoAgricultora.ClientID%>').value = "";
                        document.getElementById('<%=TxtUsoPorAgricultura.ClientID%>').value = "";
                    }
                    else {
                        var Por = (parseFloat(Agricultura) / parseFloat(Area));
                        document.getElementById('<%=TxtUsoPorAgricultura.ClientID%>').value = (parseFloat(Por) * 100).toFixed(2);
                    }
                }
            }
        }

        function VerificaUsoGanaderia() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (Area == "") {
                document.getElementById('<%=TxtUsoGanaderia.ClientID%>').value = "";
                document.getElementById('<%=TxtUsoPorGanaderia.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área total de la finca antes de ingresar el área de Ganadería");
            }
            else {
                var Ganaderia = document.getElementById('<%=TxtUsoGanaderia.ClientID%>').value;
                if (parseFloat(Ganaderia) > parseFloat(Area)) {
                    bootbox.alert("El Área de Ganaderia no puede ser mayor que el área total");
                    document.getElementById('<%=TxtUsoGanaderia.ClientID%>').value = "";
                    document.getElementById('<%=TxtUsoPorGanaderia.ClientID%>').value = "";
                }
                else {
                    if (Total() > parseFloat(Area)) {
                        bootbox.alert("El Área de todos los usos no puede ser mayor que el área total");
                        document.getElementById('<%=TxtUsoGanaderia.ClientID%>').value = "";
                        document.getElementById('<%=TxtUsoPorGanaderia.ClientID%>').value = "";
                    }
                    else {
                        var Por = (parseFloat(Ganaderia) / parseFloat(Area));
                        document.getElementById('<%=TxtUsoPorGanaderia.ClientID%>').value = (parseFloat(Por) * 100).toFixed(2);
                    }
                }
            }
        }
        function VerificaUsoAgroforestal() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (Area == "") {
                document.getElementById('<%=TxtUsoAgroForestal.ClientID%>').value = "";
                document.getElementById('<%=TxtUsoPorAgroforestal.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área total de la finca antes de ingresar el área Agroforestal");
            }
            else {
                var Agroforestal = document.getElementById('<%=TxtUsoAgroForestal.ClientID%>').value;
                if (parseFloat(Agroforestal) > parseFloat(Area)) {
                    bootbox.alert("El Área Agroforestal no puede ser mayor que el área total");
                    document.getElementById('<%=TxtUsoAgroForestal.ClientID%>').value = "";
                    document.getElementById('<%=TxtUsoPorAgroforestal.ClientID%>').value = "";
                }
                else {
                    if (Total() > parseFloat(Area)) {
                        bootbox.alert("El Área de todos los usos no puede ser mayor que el área total");
                        document.getElementById('<%=TxtUsoAgroForestal.ClientID%>').value = "";
                        document.getElementById('<%=TxtUsoPorAgroforestal.ClientID%>').value = "";
                    }
                    else {
                        var Por = (parseFloat(Agroforestal) / parseFloat(Area));
                        document.getElementById('<%=TxtUsoPorAgroforestal.ClientID%>').value = (parseFloat(Por) * 100).toFixed(2);
                    }
                }
            }
        }

        function VerificaUsoOtro() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (Area == "") {
                document.getElementById('<%=TxtUsoOtros.ClientID%>').value = "";
                document.getElementById('<%=TxtUsoPorOtro.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área total de la finca antes de ingresar el área Agroforestal");
            }
            else {
                var OtrosUsos = document.getElementById('<%=TxtUsoOtros.ClientID%>').value;
                if (parseFloat(OtrosUsos) > parseFloat(Area)) {
                    bootbox.alert("El Área de Otros no puede ser mayor que el área total");
                    document.getElementById('<%=TxtUsoOtros.ClientID%>').value = "";
                    document.getElementById('<%=TxtUsoPorOtro.ClientID%>').value = "";
                }
                else {
                    if (Total() > parseFloat(Area)) {
                        bootbox.alert("El Área de todos los usos no puede ser mayor que el área total");
                        document.getElementById('<%=TxtUsoOtros.ClientID%>').value = "";
                        document.getElementById('<%=TxtUsoPorOtro.ClientID%>').value = "";
                    }
                    else {
                        var Por = (parseFloat(OtrosUsos) / parseFloat(Area));
                        document.getElementById('<%=TxtUsoPorOtro.ClientID%>').value = (parseFloat(Por) * 100).toFixed(2);
                    }
                }
            }
        }

        function VerificaAreaBosque() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (Area == "") {
                document.getElementById('<%=TxtAreaBosque.ClientID%>').value = "";
                document.getElementById('<%=TxtAreaBosque.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área total de la finca antes de ingresar el área con Bosque");
            }
            else {
                var AreaBosque = document.getElementById('<%=TxtAreaBosque.ClientID%>').value;
                if (parseFloat(AreaBosque) > parseFloat(Area)) {
                    bootbox.alert("El Área de bosque no puede ser mayor que el área total");
                    document.getElementById('<%=TxtAreaBosque.ClientID%>').value = "";
                    document.getElementById('<%=TxtAreaBosque.ClientID%>').value = "";
                }
                else {
                    if (TotalSuperficie() > parseFloat(Area)) {
                        bootbox.alert("La Suma de lás áreas de superficies no puede ser mayor al área total");
                        document.getElementById('<%=TxtAreaBosque.ClientID%>').value = "";
                        document.getElementById('<%=TxtAreaBosque.ClientID%>').value = "";
                    }
                }
            }

            var UsoForestal = document.getElementById('<%=TxtUsoForestal.ClientID%>').value
            if (UsoForestal != "")
            {
                
                var AreaBosque = document.getElementById('<%=TxtAreaBosque.ClientID%>').value;
                if (parseFloat(AreaBosque) > parseFloat(UsoForestal)) {
                    bootbox.alert("El área de bosque no puede ser mayor a el área de uso forestal");
                }
            }
        }

        function VerificaAreaIntervenir() {
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            var AreaBosque = document.getElementById('<%=TxtAreaBosque.ClientID%>').value
            if (AreaBosque == "") {
                document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value = "";
                document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value = "";
                bootbox.alert("Debe ingresar el área de bosque antes de ingresar el área a intervenir");
            }
            else {
                var AreaIntervenir = document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value;
                if (parseFloat(AreaIntervenir) > parseFloat(AreaBosque)) {
                    bootbox.alert("El Área a Intervenir no puede ser mayor que el área de bosque");
                    document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value = "";
                    document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value = "";
                }
                else {
                    if (TotalSuperficie() > parseFloat(Area)) {
                        bootbox.alert("La Suma de lás áreas de superficies no puede ser mayor al área total");
                        document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value = "";
                        document.getElementById('<%=TxtAreaIntervenir.ClientID%>').value = "";
                    }
                }
            }
        }

        function TotalCriterios() {
            var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
            if (Pendiente == "")
                Pendiente = 0;
            var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
            if (Profundidad == "")
                Profundidad = 0;
            var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
            if (Pedregosidad == "")
                Pedregosidad = 0;
            var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
            if (Anegamiento == "")
                Anegamiento = 0;
            var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
            if (BosqueGaleria == "")
                BosqueGaleria = 0;
            var EspeciesProtegidas = document.getElementById('<%=TxtEspeciesProtegidas.ClientID%>').value;
            if (EspeciesProtegidas == "")
                EspeciesProtegidas = 0;
            var OtrosEspecifique = document.getElementById('<%=TxtValEspecifiqueProteccion.ClientID%>').value;
            if (OtrosEspecifique == "")
                OtrosEspecifique = 0;
            return (parseFloat(Pendiente) + parseFloat(Profundidad) + parseFloat(Pedregosidad) + parseFloat(Anegamiento) + parseFloat(BosqueGaleria) + parseFloat(EspeciesProtegidas) + parseFloat(OtrosEspecifique)).toFixed(2);
        }

        function VerificaCriterioPendiente() {
            
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área pendiente hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtPendiente.ClientID%>').value = "";
                document.getElementById('<%=TxtPorPendiente.ClientID%>').value = "";
            }
            else {
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                if (Profundidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                }
                if (Pedregosidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                }
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                if (Anegamiento != "") {
                    var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtAnegamiento.ClientID%>').value = Por;
                }
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                if (BosqueGalera = ! "") {
                    var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                }
                if (EspecieProtegida != "") {
                    var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                }
                if (Otros != "") {
                    var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                }
            }
        }

        function VerificaCriterioProfundidad() {
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área de profundidad hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = "";
                document.getElementById('<%=TxtProfundidad.ClientID%>').value = "";
            }
            else {
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                if (Pendiente != "") {
                    var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                }
                var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
                if (Pedregosidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                }
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                if (Anegamiento != "") {
                    var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = Por;
                }
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                if (BosqueGalera = ! "") {
                    var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                }
                if (EspecieProtegida != "") {
                    var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                }
                if (Otros != "") {
                    var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                }
            }
        }

        function VerificaCriterioPedregosidad() {
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área de pedregosidad hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtPedregosidad.ClientID%>').value = "";
                document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = "";
            }
            else {
                var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
                var Por = ((parseFloat(Pedregosidad) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                if (Pendiente != "") {
                    var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                }
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                if (Profundidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                }
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                if (Anegamiento != "") {
                    var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = Por;
                }
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                if (BosqueGalera = ! "") {
                    var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                }
                if (EspecieProtegida != "") {
                    var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                }
                if (Otros != "") {
                    var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                }
            }
        }

        function VerificaCriterioAnegamiento() {
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área de Anegamiento hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtAnegamiento.ClientID%>').value = "";
                document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = "";
            }
            else {
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = Por;
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                if (Pendiente != "") {
                    var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                }
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                if (Profundidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                }
                var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
                if (Pedregosidad != "") {
                    var Por = ((parseFloat(Pedregosidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                }
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                if (BosqueGalera = ! "") {
                    var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                }
                if (EspecieProtegida != "") {
                    var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                }
                if (Otros != "") {
                    var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                }
            }
        }

        function VerificaCriterioBosqueGaleria() {
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área de Bosque de galería hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value = "";
                document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = "";
            }
            else {
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                if (Pendiente != "") {
                    var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                }
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                if (Profundidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                }
                var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
                if (Pedregosidad != "") {
                    var Por = ((parseFloat(Pedregosidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                }
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                if (Anegamiento != "") {
                    var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = Por;
                }
                var EspecieProtegida = document.getElementById('<%=TxtEspeciesProtegidas.ClientID%>').value;
                if (EspecieProtegida != "") {
                    var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                }
                if (Otros != "") {
                    var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                }
            }
        }

        function VerificaCriterioEspecieProtegida() {
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área de Especies protegidas hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtEspeciesProtegidas.ClientID%>').value = "";
                document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = "";
            }
            else {
                var EspecieProtegida = document.getElementById('<%=TxtEspeciesProtegidas.ClientID%>').value;
                var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                if (Pendiente != "") {
                    var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                }
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                if (Profundidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                }
                var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
                if (Pedregosidad != "") {
                    var Por = ((parseFloat(Pedregosidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                }
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                if (Anegamiento != "") {
                    var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = Por;
                }
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                if (BosqueGalera = ! "") {
                    var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                }
                var Otros = document.getElementById('<%=TxtValEspecifiqueProteccion.ClientID%>').value;
                if (Otros != "") {
                    var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                }

            }
        }

        function VerificaCriterioOtro() {
            document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = TotalCriterios();
            var Area = document.getElementById('<%=TxtAreaInmueble.ClientID%>').value
            if (TotalSuperficie() > parseFloat(Area)) {
                bootbox.alert("El área de otros hace que la suma de lás áreas de superficies sea ser mayor al área total");
                document.getElementById('<%=TxtAreaProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtValEspecifiqueProteccion.ClientID%>').value = "";
                document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = "";
            }
            else {
                var Otros = document.getElementById('<%=TxtValEspecifiqueProteccion.ClientID%>').value;
                var Por = ((parseFloat(Otros) / TotalCriterios()) * 100).toFixed(2);
                document.getElementById('<%=TxtPorEspecifiqueProteccion.ClientID%>').value = Por;
                var Pendiente = document.getElementById('<%=TxtPendiente.ClientID%>').value;
                if (Pendiente != "") {
                    var Por = ((parseFloat(Pendiente) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPendiente.ClientID%>').value = Por;
                }
                var Profundidad = document.getElementById('<%=TxtProfundidad.ClientID%>').value;
                if (Profundidad != "") {
                    var Por = ((parseFloat(Profundidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorProfundidad.ClientID%>').value = Por;
                }
                var Pedregosidad = document.getElementById('<%=TxtPedregosidad.ClientID%>').value;
                if (Pedregosidad != "") {
                    var Por = ((parseFloat(Pedregosidad) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorPedregosidad.ClientID%>').value = Por;
                }
                var Anegamiento = document.getElementById('<%=TxtAnegamiento.ClientID%>').value;
                if (Anegamiento != "") {
                    var Por = ((parseFloat(Anegamiento) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorAnegamiento.ClientID%>').value = Por;
                }
                var BosqueGaleria = document.getElementById('<%=TxtBosqueGaleria.ClientID%>').value;
                if (BosqueGalera = ! "") {
                    var Por = ((parseFloat(BosqueGaleria) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorBosqueGaleria.ClientID%>').value = Por;
                }
                var EspecieProtegida = document.getElementById('<%=TxtEspeciesProtegidas.ClientID%>').value;
                if (EspecieProtegida != "") {
                    var Por = ((parseFloat(EspecieProtegida) / TotalCriterios()) * 100).toFixed(2);
                    document.getElementById('<%=TxtPorEspeciesProtegidas.ClientID%>').value = Por;
                }
            }
        }

        function CopiaCap() {
            var Cap = document.getElementById('<%=TxtCap.ClientID%>').value;
            
            document.getElementById('<%=TxtCortaAnual.ClientID%>').value = Cap;
        }
    </script>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
                <div class="wrapper wrapper-content animated fadeInRight">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h1><strong>Elaboración del Plan de Manejo Forestal</strong></h1>
                                    <h2><strong><asp:Label runat="server" ID="LblTipoPlan"></asp:Label></strong></h2>
                                    <asp:Button runat="server" Text="Vista Previa"   ID="BtnVistaPrevia" class="btn btn-primary" />
                                    <a class="btn btn-primary m-b" runat="server" id="BtnPrintSolicitud" visible="false">Imprimir Solicitud</a>
                                    <a class="btn btn-primary m-b" runat="server" id="BtnIrAnexos">Imprimir Anexos</a>
                                </div >
                                <div class="ibox-title">
                                    <a class="btn btn-primary m-b" runat="server" id="BtnEnviarSol">Enviar plan de manejo a solicitante</a>
                                </div>
                                <div class="ibox-title">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrorGeneral" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="lblMensajeErrorGen" Font-Bold="true">Error</asp:Label>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div>
                                        
                                        <div class="col-sm-12">
                                            <telerik:RadTabStrip id="RadTabStrip1" SelectedIndex="0" runat="server" MultiPageID="RadMultiPage1" Skin="Silk">
                                                <Tabs>
                                                    <telerik:RadTab runat="server" Text="1. Información General (Fincas y Propietarios)" Selected="true"></telerik:RadTab> <%--0--%>
                                                    <telerik:RadTab runat="server" Text="1.1 Información General (Representantes Legales)"></telerik:RadTab> <%--1--%>
                                                    <telerik:RadTab runat="server" Text="1.2 Información General (Datos de Notificación)"></telerik:RadTab> <%--2--%>
                                                    
                                                    <telerik:RadTab runat="server" Text="2 Características Biofísicas"></telerik:RadTab> <%--4--%>  <%--3--%>
                                                    <telerik:RadTab runat="server" Text="Plan de Investigación" Visible="false"></telerik:RadTab> <%--5--%>  <%--4--%>
                                                    <telerik:RadTab runat="server" Text="Descripción de la Plaga" Visible="false"></telerik:RadTab> <%--6--%>  <%--5--%>
                                                    <telerik:RadTab runat="server" Text="Medidas de Control" Visible="false"></telerik:RadTab> <%--7--%>  <%--6--%>
                                                    <telerik:RadTab runat="server" Text="3 Inventario Forestal"></telerik:RadTab> <%--8--%>  <%--7--%>
                                                    <telerik:RadTab runat="server" Text="Actividades de Aprovechamiento" Visible="false"></telerik:RadTab> <%--9--%>  <%--8--%>
                                                    <telerik:RadTab runat="server" Text="Acciones de repoblación forestal del área boscosa dañada" Visible="false"></telerik:RadTab> <%--10--%>  <%--9--%>
                                                    <telerik:RadTab runat="server" Text="4 Planificación del Manejo" Visible="false"></telerik:RadTab> <%--11--%>   <%--10--%>
                                                    <telerik:RadTab runat="server" Text="4.1 Información General (Datos Plan Manejo)"></telerik:RadTab> <%--3--%>  <%--11--%>
                                                    <telerik:RadTab runat="server" Text="5 Medidas de protección" Visible="false"></telerik:RadTab> <%--12--%>
                                                    <telerik:RadTab runat="server" Text="6 Cronograma de Actividades"></telerik:RadTab> <%--13--%> 
                                                    <telerik:RadTab runat="server" Text="7 Anexos (Mapas, Otros)"></telerik:RadTab> <%--13--%> 
                                                </Tabs>
                                            </telerik:RadTabStrip>
                                            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0"  Width="100%"> 
                                                <telerik:RadPageView ID="RadPageFincas" runat="server"> <%--Información General (Fincas y Propietarios)--%>
                                                    <div class="ibox-title">
                                                        <h2><strong>1. Información General (Fincas y Propietarios)</strong></h2>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Nombre de la finca:</label>
                                                            <div class="col-sm-8"><telerik:RadComboBox ID="CboFinca" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            <div class="col-sm-3"><a class="btn btn-primary m-b" runat="server" id="BtnAddFincaPlan" visible="false">Agregar Finca</a></div>    
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        
                                                        <div class="col-sm-2"><a class="btn btn-primary m-b" runat="server" id="BtnNuevaFinca">Nueva Finca</a></div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Finca:</label>
                                                            <div class="col-sm-10">
                                                                <asp:CheckBox runat="server" Text="Ingresar nombre de finca" AutoPostBack="true" ID="ChkIngNomFinca" />
                                                                <asp:TextBox runat="server" ID="TxtFinca" Text="SIN NOMBRE" Enabled="false" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:4em;"></div>
                                                    <div class="ibox-content">
                                                        <h4>Ubicación GTM</h4>
                                                        <div><label class="col-sm-1 control-label centradolabel">X:</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="TxtUbicacionOeste" step="Any" min="0" type="number" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Y:</label>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox runat="server" ID="TxtUbicacionNorte" step="Any" min="0" type="number" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label">Tipo de documento de propiedad / posesión:</label>
                                                            <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoDocumento" Width="100%" AutoPostBack="true" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Fecha de emisión:</label>
                                                            <div class="col-sm-4"><telerik:RadDatePicker ID="TxtFecEmi" Width="100%" runat="server"></telerik:RadDatePicker></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:3em;"></div>
                                                    <div runat="server" id="DivPropiedad" visible="false">
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-1 control-label centradolabel">Número de finca:</label>
                                                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNoFinca" step="0" min="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-1 control-label centradolabel">Folio:</label>
                                                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtFolio" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-1 control-label centradolabel">Libro:</label>
                                                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtLibro" step="0" type="number" min="0" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-1 control-label centradolabel">De:</label>
                                                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDe" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                    </div>
                                                    <div runat="server" id="DivMun" visible="false">
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-2 control-label centradolabel">Número certificación:</label>
                                                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoCerti" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-2 control-label centradolabel">Municipalidad que emite:</label>
                                                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtMunEmiteDoc" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                    </div>
                                                    <div runat="server" id="DiVPos" visible="false">
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-2 control-label centradolabel">Número de escritura:</label>
                                                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoEscritura" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-2 control-label centradolabel">Nombre del notario:</label>
                                                                <div class="col-sm-4">
                                                                    <telerik:RadComboBox ID="CboTitulo" Width="75px" runat="server"></telerik:RadComboBox>
                                                                    <asp:TextBox runat="server" ID="TxtNomNotario" Width="200px" CssClass="form-control" required=""></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                    </div>
                                                    <div class="ibox-title">
                                                        <h4><strong>Para ser más eficiente en el tiempo de respuesta a su solicitud, por favor cargue su archivo digital en formato PDF (no es obligatorio) Nota: si la certificación de propiedad es extensa, se sugiere escanear solamente la primera y última hoja.</strong></h4>
                                                    </div>
                                                    <div class="ibox-content">
                                                            <div><label class="col-sm-2 control-label centradolabel">Subir Archivo PDF:</label>
                                                                <div class="col-sm-8">
                                                                    <telerik:RadAsyncUpload runat="server" ID="RadUploadFile" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".pdf" PostbackTriggers="btnGrabarFinca" DropZones=".DropZone1" />
                                                                </div>
                                                            </div>
                                                    </div>
                                                    <div style="padding-bottom: 2em;"></div>
                                                    <div class="ibox-content">
                                                        <h4>Dirección de la Propiedad</h4>
                                                        <div><label class="col-sm-1 control-label centradolabel">Dirección:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDirccion" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Aldea:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtAldea" CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                                            <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamento" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipio" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <h4>Colindancias</h4>
                                                        <div><label class="col-sm-1 control-label centradolabel">Norte:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColNorte" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Sur:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColSur" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Este:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColEste" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Oeste:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColOeste" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-4 control-label centradolabel">Área de la finca según documento propiedad/posesión: (ha)</label>
                                                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtArea" step="any" type="number" min="0" Width="200px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordion2">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordion2" href="#collapseOne2">Polígono</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOne2" class="panel-collapse collapse in">
                                                                                    <div class="panel-body">
                                                                                        <label class="col-sm-2 control-label">Archivo de Poligónos</label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadAsyncUpload runat="server" ID="UploadPolFinca" Culture="es-GT" MaxFileInputsCount="1"
                                                                                                 AllowedFileExtensions="xlsx">
                                                                                            </telerik:RadAsyncUpload>
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" id="BtnCargar">Cargar</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdPoligono" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="DivOkPoligono" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblOkPoligino" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPoligono" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrPoligino" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                            </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-3 control-label centradolabel">El inmueble se encuentra dentro de áreas protegidas:</label>
                                                            <div class="col-sm-2"><asp:RadioButtonList runat="server" ID="OptAreasPro" AutoPostBack="true">
                                                                                    <asp:ListItem Value="0" Text="No" Selected="true"></asp:ListItem>    
                                                                                    <asp:ListItem Value="1" Text="Si" Selected="False"></asp:ListItem>
                                                                                  </asp:RadioButtonList></div>
                                                        </div>
                                                        <div runat="server" id="DivArea" visible="false"><label class="col-sm-2 control-label centradolabel">Cual:</label>
                                                            <div class="col-sm-4">
                                                                <telerik:RadComboBox ID="CboArea" Width="100%" runat="server"></telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar Finca"  ID="btnGrabarFinca" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-8">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrFinca" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrFinca" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodFinca" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodFinca" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                    
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <telerik:RadGrid runat="server" ID="GrdInmuebles" Skin="MetroTouch" PageSize="20" 
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
                                                                        <telerik:GridTemplateColumn HeaderText="Propietarios" UniqueName="Prop">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="ImgPropietarios" CausesValidation="false" ImageUrl="~/Imagenes/24x24/person.png" formnovalidate ToolTip="Editar" CommandName="CmdPropietarios"/>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn> 
                                                                        <telerik:GridTemplateColumn HeaderText="Áreas Internas" UniqueName="Areas">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="ImgAreas" CausesValidation="false" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Editar" CommandName="CmdAreas"/>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn> 
                                                                        <telerik:GridTemplateColumn HeaderText="Editar Finca" Visible="true" UniqueName="Edit">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="ImgEditFinca" CausesValidation="false" ImageUrl="~/Imagenes/24x24/new.png" formnovalidate ToolTip="Editar" CommandName="CmdEdit"/>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn> 
                                                                        <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgDelFinca" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                                </ItemTemplate>
                                                                        </telerik:GridTemplateColumn> 
                                                                        
                                                                    </Columns>        
                                                                </MasterTableView>
                                                                <FilterMenu EnableTheming="true">
                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                </FilterMenu>
                                                            </telerik:RadGrid>
                                                        </div>
                                                    <div style="padding-bottom:3em;"></div>
                                                    <div runat="server" id="DivPropietariosFinca" visible="false">
                                                        <div class="ibox-title">
                                                        <h3><asp:Label runat="server" ID="TitPropietarios"></asp:Label></h3>
                                                        </div>
                                                    
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-2 control-label centradolabel">Tipo de Persona:</label>
                                                                <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoPersona"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom: 2em;"></div>
                                                        <div runat="server" id="DivPropietarios" visible="false">
                                                            <div class="ibox-content" runat="server">
                                                                <div><label class="col-sm-2 control-label centradolabel">Tipo de Identificación:</label>
                                                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoIdPropietario"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                                </div>
                                                            </div>
                                                            <div style="padding-bottom:2em;"></div>
                                                            <div runat="server" id="DivPropietarioNacional" visible="false">
                                                                <div class="ibox-content">
                                                                    <div>Propietarios:</div>   
                                                                    <label class="col-sm-1 control-label centradolabel">DPI:</label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox runat="server" Text="" ID="TxtDpi" class="form-control" data-mask="9999-99999-9999" placeholder=""></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <a class="btn btn-primary m-b" runat="server" id="BtnValidarDpi">Validar DPI</a>
                                                                    </div>
                                                           
                                                                </div>
                                                            </div>
                                                            <div runat="server" id="DivPropietarioInter" visible="false">
                                                                <div class="ibox-content">
                                                                    <div>Propietarios:</div>   
                                                                    <label class="col-sm-1 control-label centradolabel">Pasaporte:</label>
                                                                    <div class="col-sm-3">
                                                                        <asp:TextBox runat="server" Text="" ID="TxtPasaportePropietario" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <label class="col-sm-1 control-label centradolabel">País:</label>
                                                                    <div class="col-sm-3">
                                                                        <telerik:RadComboBox ID="CboPais"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <a class="btn btn-primary m-b" runat="server" id="BtnValidarPasaporte">Validar Pasporte</a>
                                                                    </div>
                                                           
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="ibox-content" runat="server" id="DivAddPropietario" >
                                                                <div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivNombresProp">
                                                                        <div>Nombres</div>
                                                                        <asp:TextBox runat="server" Text="" ID="TxtNombrePropietario" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivApeProp">
                                                                        <div>Apellidos</div>
                                                                        <asp:TextBox runat="server" Text="" ID="TxtApellidoPropietario" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivFecVencimiento">
                                                                        <div>Fecha de Vencimiento</div>
                                                                        <telerik:RadDatePicker ID="TxtFecVenc" Width="100%" runat="server"></telerik:RadDatePicker>
                                                                    </div>
                                                                    
                                                                </div> 
                                                                <div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivGeneroProp">
                                                                        <div>Genero</div>
                                                                        <telerik:RadComboBox ID="CboGenero"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivFecNacProp">
                                                                        <div>Fecha de Nacimiento</div>
                                                                        <telerik:RadDatePicker ID="TxtFecNac" Width="100%" runat="server"></telerik:RadDatePicker>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivEstadoCivilProp">
                                                                        <div>Estado Civil</div>
                                                                        <telerik:RadComboBox ID="CboEstadoCivil"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivOcupacionProp">
                                                                        <div>Ocupación</div>
                                                                        <telerik:RadComboBox ID="CboOcupacion"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                    <div class="col-sm-2" runat="server" visible="false" id="DivAddProp">
                                                                        <a class="btn btn-primary m-b" runat="server" id="BtnAddPropietario">Agregar</a>
                                                                    </div>
                                                                </div>
                                                                
                                                                
                                                            </div>
                                                            <div style="padding-bottom:2em;"></div>
                                                            <div class="ibox-content" runat="server" id="DivAddPropietarioMensaje" >
                                                                <div class="col-sm-10">
                                                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivBadPropietario" visible="false">
                                                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                        <asp:Label runat="server" ID="LblMansajeBadPropietario" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodPropietario" visible="false">
                                                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                        <asp:Label runat="server" ID="LblMansajeGoodPropietario" Font-Bold="true">Error</asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="ibox-content" runat="server" id="DivGrigPropietarios" >
                                                                <div class="col-sm-10">
                                                                    <telerik:RadGrid runat="server" ID="GrdPropietarios" Skin="MetroTouch"
                                                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                                        GridLines="Both" >
                                                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                            PrevPageText="Anterior" Position="Bottom" 
                                                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                            PageSizeLabelText="Regitros"/>
                                                                        <MasterTableView Caption="" DataKeyNames="Existe,PersonaId,Dpi,Nombres,Apellidos,Fec_Venc_Id,PaisId,GeneroId,Fec_Nac,EstadoCivilId,OcupacionId" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="Existe" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="PersonaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Dpi" HeaderText="DPI/Pasaporte" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Fec_Venc_Id" HeaderText="Fecha Vencimiento" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="PaisId" HeaderText="Pais" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="GeneroId" HeaderText="Genero" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="Fec_Nac" HeaderText="FecNac" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="EstadoCivilId" HeaderText="EstadoCivil" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="OcupacionId" HeaderText="Ocupacion" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                                <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton runat="server" ID="ImgDel" ImageUrl="~/Imagenes/24x24/trashcan.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
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
                                                        <div style="padding-bottom: 2em;"></div>
                                                        <div runat="server" id="DivJuridica" visible="false">
                                                            <div class="ibox-content">
                                                                <div><label class="col-sm-2 control-label centradolabel">Nombre de Empresa:</label>
                                                                    <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtNombreEmpresaSocial"  CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <a class="btn btn-primary m-b" runat="server" id="BtnGrabarNomEmpresa">Grabar</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                    </div>
                                                    <div runat="server" id="DivUsosAreas" visible="false">
                                                        <div class="ibox-content" runat="server" id="DivAreaForestal">
                                                            <h3>Uso de la Finca</h3>
                                                            <div><label class="col-sm-1 control-label centradolabel">Forestal (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtUsoForestal"  onkeyup="VerificaUsoForestal()" step="any" min="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtUsoPorForestal" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivSeparador1" style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content"  runat="server" id="DivAreaAgricultura">
                                                            <div><label class="col-sm-1 control-label centradolabel">Agricultura (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtUsoAgricultora" onkeyup="VerificaUsoAgricultura()" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtUsoPorAgricultura" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivSeparador2" style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaGanaderia">
                                                            <div><label class="col-sm-1 control-label centradolabel">Ganadería (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtUsoGanaderia" onkeyup="VerificaUsoGanaderia()" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtUsoPorGanaderia" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivSeparador3" style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaAgroforestal">
                                                            <div><label class="col-sm-1 control-label centradolabel">Agroforestal (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtUsoAgroForestal" onkeyup="VerificaUsoAgroforestal()" step="any" min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtUsoPorAgroforestal" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivSeparador4" style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaOtros">
                                                            <div><label class="col-sm-2 control-label centradolabel">Otros (Especifique) (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtUsoOtrosEspecifique" CssClass="form-control"></asp:TextBox></div>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtUsoOtros" step="any" onkeyup="VerificaUsoOtro()" min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtUsoPorOtro" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivSeparador5" style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-1 control-label centradolabel">Tipo de Bosque:</label>
                                                                <div class="col-sm-5"><telerik:RadComboBox ID="CboTipoBosque"   Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                            <div runat="server" id="DivAreaClaseDesarrollo"><label class="col-sm-1 control-label centradolabel">Clase de Desarrollo:</label>
                                                                <div class="col-sm-5"><telerik:RadComboBox ID="CboClaseDesarrollo" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los items seleccionados" Localization-CheckAllString="Seleccionar Todos" Localization-ItemsCheckedString="Seleccionados" CheckBoxes="true"   Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                        </div>
                                                        <div class="ibox-content">
                                                            <div>
                                                                <label class="col-sm-1 control-label"></label>
                                                                    <div class="col-sm-10">
                                                                        <div class="panel-body">
                                                                            <div class="panel-group" id="accordion3">
                                                                                <div class="panel panel-default">
                                                                                    <div class="panel-heading">
                                                                                        <h5 class="panel-title">
                                                                                            <a data-toggle="collapse" data-parent="#accordion3" href="#collapseOne3">Especies Existentes</a>
                                                                                        </h5>
                                                                                    </div>
                                                                                    <div id="collapseOne3" class="panel-collapse collapse in">
                                                                                        <div class="panel-body">
                                                                                            <label class="col-sm-2 control-label">Seleccione Especie</label>
                                                                                            <div class="col-sm-5">
                                                                                                <telerik:RadComboBox ID="CboEspecie" AllowCustomText="true" Filter="Contains"  Width="100%" runat="server"></telerik:RadComboBox>	
                                                                                            </div>
                                                                                            <div class="col-sm-2">
                                                                                                <a class="btn btn-primary m-b" runat="server" id="BtnAddEspecie">Agregar Especie</a>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="panel-body">
                                                                                            <telerik:RadGrid runat="server" ID="GrdEspecies" Skin="Telerik"
                                                                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                                GridLines="Both" PageSize="20" >
                                                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                    PageSizeLabelText="Regitros"/>
                                                                                                <MasterTableView Caption="" DataKeyNames="EspecieId,Especie" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                    <Columns>
                                                                                                        <telerik:GridBoundColumn DataField="EspecieId" UniqueName="EspecieId" HeaderText="EspecieId"  Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                        <telerik:GridBoundColumn DataField="Especie" UniqueName="Especie" HeaderText="Especie" HeaderStyle-Width="325px"></telerik:GridBoundColumn>
                                                                                                        <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del"  HeaderStyle-Width="75px">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:ImageButton runat="server" ID="ImgDelEspecie" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                                                                </ItemTemplate>
                                                                                                        </telerik:GridTemplateColumn> 
                                                                                                    </Columns>        
                                                                                                </MasterTableView>
                                                                                                <FilterMenu EnableTheming="true">
                                                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                                </FilterMenu>
                                                                                            </telerik:RadGrid>
                                                                                        </div>
                                                                                        <div class="panel-body">
                                                                                            <div class="ibox-content">
                                                                                                <div class="col-sm-9">
                                                                                                    <div class="alert  alert-success alert-dismissable" runat="server" id="DivErrEspecie" visible="false">
                                                                                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                        <asp:Label runat="server" ID="LblErrEspecie" Font-Bold="true">Error</asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div style="padding-bottom:2em;"></div>
                                                                                                </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" id="DivSuperficieBosque">
                                                            <h3>SUPERFICIE CON BOSQUE</h3>
                                                            <div><label runat="server" id="LblAreaBosque" class="col-sm-3 control-label centradolabel">Área con bosque (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaBosque" step="any" min="0" onkeyup="VerificaAreaBosque()"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivSeparador6" style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" id="DivPoligonoAraBosque">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordion">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordion2" href="#collapseOne">Polígono Área con Bosque</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOne" class="panel-collapse collapse in">
                                                                                    <div class="panel-body">
                                                                                        <label class="col-sm-2 control-label">Archivo de Poligónos</label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadAsyncUpload runat="server" ID="RadUploadoPolBosque" Culture="es-GT" MaxFileInputsCount="1"
                                                                                                 AllowedFileExtensions="xlsx">
                                                                                            </telerik:RadAsyncUpload>
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" id="BtnCargaPolBosque">Cargar</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdPolBoque" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="ErrPolBosque" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblMensajeErrBosque" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPolBosque" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrPolBosque" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                            </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="DivSeparador7" style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" id="DivAreaIntervenir">
                                                        <div><label class="col-sm-3 control-label centradolabel">Área Intervenir (ha):</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaIntervenir" onkeyup="VerificaAreaIntervenir()" step="any" min="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div  runat="server" id="DivSeparador8" style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content"  runat="server" id="DivPolAreaIntervenir">
                                                    <div>
                                                        <label class="col-sm-1 control-label"></label>
                                                            <div class="col-sm-10">
                                                                <div class="panel-body">
                                                                    <div class="panel-group" id="accordion4">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-heading">
                                                                                <h5 class="panel-title">
                                                                                    <a data-toggle="collapse" data-parent="#accordion4" href="#collapseOne4">Poligóno Área Intervenir (ha)</a>
                                                                                </h5>
                                                                            </div>
                                                                            <div id="collapseOne4" class="panel-collapse collapse in">
                                                                                <div class="panel-body">
                                                                                    <label class="col-sm-2 control-label">Archivo de Poligónos</label>
                                                                                    <div class="col-sm-5">
                                                                                        <telerik:RadAsyncUpload runat="server" ID="RadUploadPolIntervenir" Culture="es-GT" MaxFileInputsCount="1"
                                                                                                AllowedFileExtensions="xlsx">
                                                                                        </telerik:RadAsyncUpload>
                                                                                    </div>
                                                                                    <div class="col-sm-2">
                                                                                        <a class="btn btn-primary m-b" runat="server" id="BtnCargaPolIntervenir">Cargar</a>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <telerik:RadGrid runat="server" ID="GrdPolIntervenir" Skin="Telerik"
                                                                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                        GridLines="Both" PageSize="20" >
                                                                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                            PrevPageText="Anterior" Position="Bottom" 
                                                                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                            PageSizeLabelText="Regitros"/>
                                                                                        <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                            </Columns>        
                                                                                        </MasterTableView>
                                                                                        <FilterMenu EnableTheming="true">
                                                                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                        </FilterMenu>
                                                                                    </telerik:RadGrid>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="ibox-content">
                                                                                        <div class="col-sm-9">
                                                                                            <div class="alert  alert-success alert-dismissable" runat="server" id="Div1" visible="false">
                                                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                <asp:Label runat="server" ID="Label1" Font-Bold="true">Error</asp:Label>
                                                                                            </div>
                                                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPolIntervencion" visible="false">
                                                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                <asp:Label runat="server" ID="LblErrPolIntervencion" Font-Bold="true">Error</asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div style="padding-bottom:2em;"></div>
                                                                                        </div>
                                                                                </div>
                                                                                
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;" runat="server" id="DivSeparador9"></div>
                                                    <div class="ibox-content" runat="server" id="DivAreaProteccion">
                                                        <div><label class="col-sm-3 control-label centradolabel">Área Protección (ha):</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaProteccion" onchange="" Enabled="false" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"  runat="server" id="DivSeparador10"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaPendiente">
                                                            <div><label class="col-sm-2 control-label centradolabel">Pendiente (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtPendiente" onkeyup="VerificaCriterioPendiente()" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorPendiente" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador11"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaProfundidad">
                                                            <div><label class="col-sm-2 control-label centradolabel">Profundidad (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtProfundidad" onkeyup="VerificaCriterioProfundidad()" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorProfundidad" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador12"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaPedrogosidad">
                                                            <div><label class="col-sm-2 control-label centradolabel">Pedregosidad (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtPedregosidad" onkeyup="VerificaCriterioPedregosidad()" step="any" min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorPedregosidad" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador13"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaAnegamiento">
                                                            <div><label class="col-sm-2 control-label centradolabel">Anegamiento (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server"  ID="TxtAnegamiento" onkeyup="VerificaCriterioAnegamiento()"  step="any" min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorAnegamiento" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador14"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaBosqueGalaria">
                                                            <div><label class="col-sm-2 control-label centradolabel">Bosque de galería (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Text="0" ID="TxtBosqueGaleria" onkeyup="VerificaCriterioBosqueGaleria()" step="any" min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorBosqueGaleria" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador15"></div>
                                                        <div class="ibox-content" runat="server" id="DivAreaEspeciesProtegidas">
                                                            <div><label class="col-sm-2 control-label centradolabel">Especies Protegidas (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtEspeciesProtegidas" onkeyup="VerificaCriterioEspecieProtegida()" step="any" min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorEspeciesProtegidas" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador16"></div>
                                                        <div class="ibox-content"  runat="server" id="DivAreaOtrosProteccion">
                                                            <div><label class="col-sm-2 control-label centradolabel">Otros (Especifique) (ha):</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtEspecifiqueProteccion" CssClass="form-control"></asp:TextBox></div>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtValEspecifiqueProteccion" onkeyup="VerificaCriterioOtro()" step="any"  min="0"  type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                                %
                                                                <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtPorEspecifiqueProteccion" step="any" min="0"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"  runat="server" id="DivSeparador17"></div>
                                                        <div class="ibox-content" runat="server" id="DivPoligonoProteccion"> 
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordion5">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordion5" href="#collapseOne5">Poligóno Área Protección (ha)</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOne5" class="panel-collapse collapse in">
                                                                                    <div class="panel-body">
                                                                                        <label class="col-sm-2 control-label">Archivo de Poligónos</label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadAsyncUpload runat="server" ID="RadUloadPolProteccion" Culture="es-GT" MaxFileInputsCount="1"
                                                                                                    AllowedFileExtensions="xlsx">
                                                                                            </telerik:RadAsyncUpload>
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" id="BtncargarPolProteccion">Cargar</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdPolProteccion" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="Div2" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="Label2" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPolProteccion" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrPolProteccion" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="ibox-content">
                                                            <div class="col-sm-5">
                                                                <asp:Button runat="server" Text="Grabar Áreas"  ID="BtnGrabarAreas" class="btn btn-primary" />
                                                            </div>
                                                        </div>
                                                        <div class="ibox-content" runat="server">
                                                            <div class="col-sm-10">
                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrAreas" visible="false">
                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                    <asp:Label runat="server" ID="LblErrAreas" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodAreas" visible="false">
                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                    <asp:Label runat="server" ID="LblGoodAreas" Font-Bold="true">Error</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageRepresentantes" runat="server" Visible="false"> <%--Información General (Representantes Legales)--%>
                                                    <div class="ibox-title">
                                                        <h2><strong>1.1 Información General (Representantes Legales)</strong></h2>
                                                    </div>
                                                    <div runat="server" id="DivPropietariosRep">
                                                        <div class="ibox-content" runat="server">
                                                            <div><label class="col-sm-2 control-label centradolabel">Tipo de Identificación:</label>
                                                                <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoIdentificacionRep"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div runat="server" id="DivPropietarioNacionalRepre" visible="false">
                                                            <div class="ibox-content">
                                                                
                                                                <label class="col-sm-1 control-label centradolabel">DPI:</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox runat="server" Text="" ID="TxtDpiRep" class="form-control" data-mask="9999-99999-9999" placeholder=""></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <a class="btn btn-primary m-b" runat="server" id="BtnValidarDpiRep">Validar DPI</a>
                                                                </div>
                                                           
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="DivPropietarioInterRep" visible="false">
                                                            <div class="ibox-content">
                                                                
                                                                <label class="col-sm-1 control-label centradolabel">Pasaporte:</label>
                                                                <div class="col-sm-3">
                                                                    <asp:TextBox runat="server" Text="" ID="TxtPasaporteRep" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-1 control-label centradolabel">País:</label>
                                                                <div class="col-sm-3">
                                                                    <telerik:RadComboBox ID="CboPaisRep"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <a class="btn btn-primary m-b" runat="server" id="BtnValidarPasaporteRep">Validar Pasporte</a>
                                                                </div>
                                                           
                                                            </div>
                                                        </div>
                                                        <div class="ibox-content" runat="server" id="DivAddPropietarioRep" >
                                                            <div>
                                                                <div class="col-sm-3" runat="server" visible="false" id="DivNombresPropRep">
                                                                    <div>Nombres</div>
                                                                    <asp:TextBox runat="server" Text="" ID="TxtNombresRep" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-3" runat="server" visible="false" id="DivApePropRep">
                                                                    <div>Apellidos</div>
                                                                    <asp:TextBox runat="server" Text="" ID="TxtApellidosRep" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-3" runat="server" visible="false" id="DivFecVencimientoRep">
                                                                    <div>Fecha de Vencimiento</div>
                                                                    <telerik:RadDatePicker ID="TxtFecVenceIdRep" Width="100%" runat="server"></telerik:RadDatePicker>
                                                                </div>
                                                                
                                                            </div> 
                                                            <div>
                                                                <div class="col-sm-3" runat="server" visible="false" id="DivGeneroRep">
                                                                        <div>Genero</div>
                                                                        <telerik:RadComboBox ID="CboGeneroRep"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivFecNacRep">
                                                                        <div>Fecha de Nacimiento</div>
                                                                        <telerik:RadDatePicker ID="TxtFecNacRep" Width="100%" runat="server"></telerik:RadDatePicker>
                                                                    </div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivEstadoCivilRep">
                                                                        <div>Estado Civil</div>
                                                                        <telerik:RadComboBox ID="CboEstadoCivilRep"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                </div>
                                                                <div>
                                                                    <div class="col-sm-3" runat="server" visible="false" id="DivOcupacionRep">
                                                                        <div>Ocupación</div>
                                                                        <telerik:RadComboBox ID="CboOcupacionRep"  Width="100%" runat="server"></telerik:RadComboBox>
                                                                    </div>
                                                                    <div class="col-sm-2" runat="server" visible="false" id="DivAddPropRep">
                                                                    <a class="btn btn-primary m-b" runat="server" id="BtnAddRepresentante">Agregar</a>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" id="DivAddRepresentanteMensaje" >
                                                            <div class="col-sm-10">
                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivBadRepresentante" visible="false">
                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                    <asp:Label runat="server" ID="LblMansajeBadRepresentante" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodRepresentante" visible="false">
                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                    <asp:Label runat="server" ID="LblMansajeGoodRepresentante" Font-Bold="true">Error</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="ibox-content" runat="server" id="DivGrigRepresentantes"  visible="true">
                                                            <div class="col-sm-10">
                                                                <telerik:RadGrid runat="server" ID="GrdRepresentantes" Skin="MetroTouch"
                                                                    AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                                    GridLines="Both" >
                                                                    <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                        PrevPageText="Anterior" Position="Bottom" 
                                                                        PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                        PageSizeLabelText="Regitros"/>
                                                                    <MasterTableView Caption="" DataKeyNames="ExisteRep,PersonaIdRep,DpiRep,NombresRep,ApellidosRep,Fec_Venc_IdRep,PaisIdRep" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="ExisteRep" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="PersonaIdRep" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="DpiRep" HeaderText="Dpi" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="NombresRep" HeaderText="Nombres" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="ApellidosRep" HeaderText="Apellidos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Fec_Venc_IdRep" HeaderText="Fecha Vencimiento" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="PaisIdRep" HeaderText="Pais" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton runat="server" ID="ImgDelRep" ImageUrl="~/Imagenes/24x24/trashcan.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
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
                                                </telerik:RadPageView>
                                                  <telerik:RadPageView ID="RadPageDatosNotifica" runat="server" Visible="false"> <%--Información General (Datos de Notificación)--%>
                                                    <div class="ibox float-e-margins">
                                                        <div class="ibox-title">
                                                            <h2><strong>1.3 Información General (Datos de Notificación)</strong></h2>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel" runat="server" id="LblDirecNotifica">Dirección de notificación:</label>
                                                            <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtDireccionNotifica" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                                            <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamentoNotifica" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipioNotifica"   Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Teléfono domicilio:</label>
                                                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTelDomicilio" CssClass="form-control" data-mask="9999-9999" ></asp:TextBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Teléfono:</label>
                                                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTelefonoNotifica" CssClass="form-control" data-mask="9999-9999" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        
                                                        <div><label class="col-sm-1 control-label centradolabel">Celular:</label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtCelularNotifica"  required=""  data-mask="9999-9999" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Correo electrónico:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtCorreoNotifica"  required="" CssClass="form-control" type="email" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarDatosNotifica" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrDatosNotifica" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrDatosNotifica" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodDatosNotifica" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodDatosNotifica" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                
                                                <telerik:RadPageView ID="RadPageCaracBio" runat="server" Visible="false"> <%-- Caracterisicas Biofisicas--%>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Altitud msnm</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtAltitud" step="any" min="0"  type="number" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Topografía</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtTopografia"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Suelos</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtSuelos"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Hidrografía</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtHidrografia"  TextMode="MultiLine" Height="100px" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Zona de Vida (Holdrige)</label>
                                                            <div class="col-sm-7"><telerik:RadComboBox ID="CboZonaVida"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                             <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarCarcBiofisicas" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrorBio" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrorBio" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodBio" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="lblGoodBio" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPagePlanInvestigacion" runat="server" Visible="false"> <%-- Plan de Investigación--%>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Indice</label>
                                                            <div class="col-sm-7">
                                                                (Presentación ordenada de cada uno de los capítulos y los apartados del plan con el número de las páginas en donde se encuentran.)
                                                                <asp:TextBox runat="server" ID="TxtIndice"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Introducción</label>
                                                            <div class="col-sm-7">
                                                                El alcance de la investigación, contribución al sector en función del tema a investigar, entidad ejecutora, participación institucional
                                                                <asp:TextBox runat="server" ID="TxtIntro"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Delimitación del Tema</label>
                                                            <div class="col-sm-7">
                                                                Se describe el tema y subtemas, objeto de investigación
                                                                <asp:TextBox runat="server" ID="TxtDelimitacion"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Antecedentes de Otras Investigaciones</label>
                                                            <div class="col-sm-7">
                                                                Lugar, similitud, resultados, relaciónados con el tema
                                                                <asp:TextBox runat="server" ID="TxtAntecedentes"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Objetivos (General y Especificos)</label>
                                                            <div class="col-sm-7">
                                                                <asp:TextBox runat="server" ID="TxtObjetivos"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Alcances y Contribución</label>
                                                            <div class="col-sm-7">
                                                                Se elaboran respondiendo a las siguientes preguntas: (¿Por qué  decidimos sobre esta temática?, ¿Por qué estamos realizando esta investigación?, ¿Para qué pueden servir los resultados de nuestra investigación? 
                                                                <asp:TextBox runat="server" ID="TxtAlcances"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Plantamiento del problema</label>
                                                            <div class="col-sm-7">
                                                                Definir la problemática del porque la investigación
                                                                <asp:TextBox runat="server" ID="TxtPlanteamiento"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Justificación</label>
                                                            <div class="col-sm-7">
                                                                Se elaborará con desarrollo de los motivos que nos impulsaron a realizar el plan (personales, sociales y científicas).
                                                                <asp:TextBox runat="server" ID="TxtJustificacion"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Metodología</label>
                                                            <div class="col-sm-7">
                                                                Hacer referencia al conjunto de procedimientos racionales utilizados para alcanzar una gama de objetivos
                                                                <asp:TextBox runat="server" ID="TxtMetodologia"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Recursos</label>
                                                            <div class="col-sm-7">
                                                                Definir los recursos a utilizar (financieros, equipos, maquinaria, humanos entre otros)
                                                                <asp:TextBox runat="server" ID="TxtRecursos"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Resultados Esperados</label>
                                                            <div class="col-sm-7">
                                                                Resultados que se pretenden alcanzar de comienzo y final.
                                                                <asp:TextBox runat="server" ID="TxtResultados"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Cronograma de Actividades</label>
                                                            <div class="col-sm-7">
                                                                Definir a través de una lista  todos los elementos  del  proyecto con sus fechas previstas de comienzo y final.
                                                                <asp:TextBox runat="server" ID="TxtCronograma"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Bibliografía</label>
                                                            <div class="col-sm-7">
                                                                Hacer referencia a los textos utilizados de apoyo en el plan de investigación.
                                                                <asp:TextBox runat="server" ID="TxtBibliografia"  TextMode="MultiLine" Height="150px" CssClass="form-control" required=""></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarPlanCientifico" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPlanCientifico" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrPlanCientifico" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodPlanCientifico" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodPlanCientifico" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPagePlaga" Visible="false" runat="server"> <%-- Descripcion de la Plaga--%>
                                                    <asp:Label runat="server" ID="LblTitPagePlaga" Text="DESCRIPCION DEL AGENTE CAUSAL Y ESTIMACION DEL DAÑO CAUSADO"></asp:Label>
                                                    <div class="ibox-content" runat="server" id="DivAgenteCausal">
                                                        <div><label class="col-sm-2 control-label centradolabel">Descripción del agente causal</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtAgenteCausal"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivSintomologia">
                                                        <div><label class="col-sm-2 control-label centradolabel">Sintomología basica</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtSintomologia"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivFenNatural"  visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Descripción del fenomeno natural</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtFenomenoNatural"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" id="DivDescDamage" runat="server">
                                                        <div><label class="col-sm-2 control-label centradolabel"  >Descripción del daño</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtDano"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" id="DivEstimacionMadera" visible="false" runat="server">
                                                        <div><label class="col-sm-2 control-label centradolabel"  >Estimación maderable del daño causado</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtEstimacionMadera"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="btnGrabarPlaga" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="Div3" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label3" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodPlaga" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodPlaga" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageMedidasdeControl" Visible="false" runat="server"> <%-- Medidas de Control--%>
                                                    DESCRIPCIÓN DE LAS MEDIDAS DE CONTROL A APLICAR Y SU JUSTIFICACIÓN

                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Descripción de las medidas, tipo de control y su justificación (Cómo se detendrá la expansión del foco de infestación):</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtDescripcionMedidas"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Tratamiento de los productos forestales previo a su extracción</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="Txttratamiento"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label centradolabel">Manejo de residuos del aprovechamiento</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtResiduos"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarMedidasControl" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="Div6" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label6" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodMediadasControl" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodMedidasControl" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageAprovechamiento" runat="server" Visible="false"> <%-- Aprovechamiento Foresal--%>
                                                  <div class="ibox-content">
                                                        <div><label class="col-sm-3 control-label centradolabel">Tipo de Ingreso de Datos</label>
                                                            <div class="col-sm-3"><telerik:RadComboBox ID="CboTipoIngresoDatos" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-3 control-label centradolabel">Tipo de Inventario</label>
                                                            <div class="col-sm-3"><telerik:RadComboBox ID="CboTipoInventario" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordionCenso">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordionCenso" href="#collapseOneCenso"><asp:Label runat="server" ID="LbltitPanCenso" Text="Censo"></asp:Label> </a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOneCenso" class="panel-collapse collapse out">
                                                                                    <div class="panel-body">
                                                                                        <label class="col-sm-2 control-label"><asp:Label runat="server" ID="LblCargueCenso" Text="Censo"></asp:Label></label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadAsyncUpload runat="server" ID="RadUploadBoleta" Culture="es-GT" MaxFileInputsCount="1"
                                                                                                 AllowedFileExtensions="xls,xlsx">
                                                                                            </telerik:RadAsyncUpload>
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" id="BtnCargarBoleta">Cargar</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdBoleta" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="Turno,Rodal,No,Dap,Altura,Nombre_Cientifico,Troza,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="Turno" Visible="false" UniqueName="Turno" HeaderText="Turno" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Parcela" Visible="false" UniqueName="Parcela" HeaderText="Parcela" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="No" UniqueName="No" HeaderText="No" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Dap" UniqueName="Dap" HeaderText="Dap" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura" HeaderText="Altura" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Troza" UniqueName="Troza" HeaderText="Troza" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="Div11" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="Label11" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                            </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrBoleta" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrBoleta" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="Div14" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label15" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <telerik:RadGrid runat="server" ID="GrdResumen" Skin="Telerik" CssClass="AddBorders"
                                                            AutoGenerateColumns="false" Width="100%"   
                                                            GridLines="Both" PageSize="20" >
                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                PageSizeLabelText="Regitros"/>
                                                            <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Rodal,EspecieId,Nombre_Cientifico,AreaRodal,Clase_Desarrollo,Clase_DesarrolloId,Edad,Tratamiento,Dap,Altura,Densidad,AreaBasal,VolTroza,VolLena,VolOtro,VolTotal,sumadap,sumaaltura,arboles,SumBa,volumen,Troza,Pendiente,INC,VolHa,VolRodal,Extrae,VolTrozaExtrae,VolLenaExtrae,VolOtroExtrae,VolTotalExtrae,AreaBasalRodal" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    
                                                                    <telerik:GridBoundColumn DataField="AreaRodal" UniqueName="AreaRodal"  HeaderText="Area del Rodal (ha)" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="AreaRodal" HeaderText="Area del Rodal (ha)"  UniqueName="AreaRodalEdit" HeaderStyle-Width="75px">
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox runat="server" MinValue="0" ID="TxtAreaRodal" Width="60px" >
                                                                                <NumberFormat  DecimalDigits="2" />
                                                                            </telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="Clase_Desarrollo" UniqueName="Clase_Desarrollo"  HeaderText="Clase_Desarrollo" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Clase_DesarrolloIs" UniqueName="Clase_DesarrolloId"  HeaderText="Clase_DesarrolloId" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Clase_Desarrollo" HeaderText="Clase Desarrollo"  UniqueName="Clase_Desarrollo_Edit" HeaderStyle-Width="130px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadComboBox runat="server" ID="CboClaseDesarrollo" Width="110px"></telerik:RadComboBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>  

                                                                    <telerik:GridBoundColumn DataField="Edad" UniqueName="Edad"  HeaderText="Edad" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Edad" HeaderText="Edad Promedio"  UniqueName="EdadEdit" HeaderStyle-Width="80px" >
                                                                        <ItemTemplate>
                                                                            <asp:TextBox runat="server" ID="TxtEdad" CssClass="form-control"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>


                                                                    <telerik:GridBoundColumn DataField="Pendiente" UniqueName="Pendiente"  HeaderText="Pendiente" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Pendiente" HeaderText="Pendiente %"  UniqueName="PendienteEdit" HeaderStyle-Width="80px" Visible="false" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox runat="server"  MinValue="0" ID="TxtPendiente" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>

                                                                    <telerik:GridBoundColumn DataField="INC" UniqueName="INC"  HeaderText="INC" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="INC" HeaderText="INC"  UniqueName="INCEdit" HeaderStyle-Width="80px" Visible="false" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox runat="server"  MinValue="0" ID="TxtINC" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>

                                                                    <telerik:GridBoundColumn DataField="Tratamiento" UniqueName="Tratamiento"  HeaderText="Tratamiento" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Tratamiento" HeaderText="Tratamiento Silvicultural"  UniqueName="Tratamiento_Edit" HeaderStyle-Width="300px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadComboBox runat="server" ID="CboTratamiento" Width="300px"></telerik:RadComboBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>  

                                                                    <telerik:GridBoundColumn DataField="EspecieId" Visible="false" UniqueName="EspecieId" HeaderText="EspecieId" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Dap" UniqueName="Dap"  HeaderText="Dap Medio (cm)" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Dap" HeaderText="Dap Medio (cm)"  UniqueName="DapEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtDap" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura"  HeaderText="Dap Media (cm)" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Altura" HeaderText="Altura Media (m)"  UniqueName="AlturaEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox runat="server"  MinValue="0" ID="TxtAltura" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="Densidad" UniqueName="Densidad"  HeaderText="Densidad arboles/ha" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="Densidad" HeaderText="Densidad arboles/ha"  UniqueName="DensidadEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtDensidad" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="SumBa" UniqueName="AreaBasal"  HeaderText="Area Basal m2/ha" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="SumBa" HeaderText="Area Basal m2/ha"  UniqueName="AreaBasalEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtAreaBasal" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="VolTroza" UniqueName="VolTroza"  HeaderText="VolTroza" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="VolTroza" HeaderText="Vol. Troza"  UniqueName="VolTrozaEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtVolTroza" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="VolLena" UniqueName="VolLena"  HeaderText="VolLena" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="VolLena" HeaderText="Vol. Leña"  UniqueName="VolLenaEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox   MinValue="0" runat="server" ID="TxTVolLena" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="VolOtro" UniqueName="VolOtro"  HeaderText="VolOtro" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="VolOtro" HeaderText="Vol. Otro"  UniqueName="VolOtroEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtVolOtro" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="VolTotal" UniqueName="VolTotal"  HeaderText="VolTotal" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="VolTotal" HeaderText="Vol. Total"  UniqueName="VolTotalEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtVolTotal" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>

                                                                    <telerik:GridBoundColumn DataField="AreaBasalRodal" UniqueName="AreaBasalRodal"  HeaderText="AreaBasalRodal" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="AreaBasalRodal" HeaderText="Area basal  m2/rodal"  UniqueName="AreaBasalRodalEdit" HeaderStyle-Width="75px" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtAreaBasalRodal" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    
                                                                    
                                                                    <telerik:GridBoundColumn DataField="VolHa" UniqueName="VolHa"  HeaderText="VolHa" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="VolHa" HeaderText="Vol/Ha.(M3)"  UniqueName="VolHaEdit" HeaderStyle-Width="75px" Visible="false" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtVolHa" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>

                                                                    <telerik:GridBoundColumn DataField="volumen" UniqueName="VolRodal"  HeaderText="VolRodal" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn DataField="volumen" HeaderText="Vol/Rodal(M3)"  UniqueName="VolRodalEdit" HeaderStyle-Width="75px" Visible="false" >
                                                                        <ItemTemplate>
                                                                            <telerik:RadNumericTextBox  MinValue="0" runat="server" ID="TxtVolRodal" Width="60px"></telerik:RadNumericTextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>


                                                                    <telerik:GridBoundColumn DataField="sumadap" UniqueName="sumadap"  HeaderText="sumadap"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="sumaaltura" UniqueName="sumaaltura"  HeaderText="sumaaltura"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="arboles" UniqueName="arboles"  HeaderText="arboles"  HeaderStyle-Width="75px" Visible="false" ></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="SumBa" UniqueName="SumBa"  HeaderText="SumBa"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="volumen" UniqueName="volumen"  HeaderText="volumen"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Troza" UniqueName="Troza"  HeaderText="Troza"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Extrae" UniqueName="Extrae"  HeaderText="Extrae" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="VolTrozaExtrae" UniqueName="VolTrozaExtrae"  HeaderText="VolTrozaExtrae" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="VolLenaExtrae" UniqueName="VolLenaExtrae"  HeaderText="VolLenaExtrae" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="VolOtroExtrae" UniqueName="VolOtroExtrae"  HeaderText="VolOtroExtrae" Visible="false"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="VolTotalExtrae" UniqueName="VolTotalExtrae"  HeaderText="VolTotalExtrae" Visible="false"></telerik:GridBoundColumn>
                                                                </Columns>        
                                                            </MasterTableView>
                                                            <FilterMenu EnableTheming="true">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            </FilterMenu>
                                                            <ClientSettings>
                                                                <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <a class="btn btn-primary m-b" runat="server" id="BtnGeneraCalculos">Generar Calculos</a>
                                                        </div>
                                                    </div>


                                                    <div class="ibox-content" runat="server" id="DivDiametroMinRodal" visible="false">
                                                        <div><label class="col-sm-3 control-label centradolabel">Diámetro Mínimo del Inventario</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtDiametroMinimo"  step="1" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Total de Rodales</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtTotRodal"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" id="DivEcuacion" runat="server" visible="false">
                                                        <div><label class="col-sm-6 control-label centradolabel">Ecuación utilizada para el cálculo de Volumen y fuente Bibliográfica</label>
                                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboEcuacion" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los items seleccionados" Localization-CheckAllString="Seleccionar Todos" Localization-ItemsCheckedString="Seleccionados" CheckBoxes="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" id="DivOtraEcuacion" runat="server" visible="false">
                                                        <div><label class="col-sm-6 control-label centradolabel"><asp:Label runat="server" ID="LblEcuacion"></asp:Label></label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtOtraEcuacion"  TextMode="MultiLine" Height="100px" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-6 control-label centradolabel">Datos de regresión utilizados para la estimación de las alturas (R2 Y CME) </label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDatosRegresion"  TextMode="MultiLine" Height="100px" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" id="DivMuestroUno" visible="false">
                                                      <div><label class="col-sm-3 control-label centradolabel">Forma de Parcela</label>
                                                            <div class="col-sm-3"><telerik:RadComboBox ID="CboFormaParcela" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los items seleccionados" Localization-CheckAllString="Seleccionar Todos" Localization-ItemsCheckedString="Seleccionados" CheckBoxes="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Área muestreada ha</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaMuestreada"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" id="DivMuestroDos" visible="false">
                                                      <div><label class="col-sm-3 control-label centradolabel">Tipo de Muestreo</label>
                                                            <div class="col-sm-3"><telerik:RadComboBox ID="CboTipoMuestreo" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los items seleccionados" Localization-CheckAllString="Seleccionar Todos" Localization-ItemsCheckedString="Seleccionados" CheckBoxes="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Intensidad de muestreo</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtIntensidadMuestreo"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox>
                                                                <label class="centradolabel">%</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="btnGrabarAprovechamiento" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrAprovechamiento" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrAprovechamiento" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodAprovechamiento" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodAprovechamiento" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivAnaEstadistico" visible="false">
                                                        Análisis-Muestreo
                                                        <div class="ibox-content">
                                                            <telerik:RadGrid runat="server" ID="GrdMuestreo" Skin="Telerik" CssClass="AddBorders"
                                                                AutoGenerateColumns="false" Width="100%"   
                                                                GridLines="Both" >
                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                    PageSizeLabelText="Regitros"/>
                                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Rodal,Area,Parcela,Volaha,MediaVolParcela,DesviacionEstandard,CoeficienteVariacion,ErrorEstandardMedia,ErrorMuestreo,PorErrorMuestreo,IntervaloConfianza" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Area" UniqueName="Area" HeaderText="Area" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Area" HeaderText="Área"  UniqueName="AreaEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtArea" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Parcela" UniqueName="Parcela" HeaderText="Parcela" HeaderStyle-Width="45px"></telerik:GridBoundColumn>

                                                                        <telerik:GridBoundColumn DataField="Volaha" UniqueName="Volaha" HeaderText="Volaha" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Volaha" HeaderText="Vol./ha (m3)"  UniqueName="VolahaEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtVolaha" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="MediaVolParcela" UniqueName="MediaVolParcela" HeaderText="MediaVolParcela" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="MediaVolParcela" HeaderText="MediaVol./ parcela"  UniqueName="MediaVolParcelaEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtMediaVolParcela" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="DesviacionEstandard" UniqueName="DesviacionEstandard" HeaderText="DesviacionEstandard" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="DesviacionEstandard" HeaderText="Desviación Estándar"  UniqueName="DesviacionEstandardEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtDesviacionEstandard" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="CoeficienteVariacion" UniqueName="CoeficienteVariacion" HeaderText="CoeficienteVariacion" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="CoeficienteVariacion" HeaderText="Coeficiente de variación"  UniqueName="CoeficienteVariacionEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtCoeficienteVariacion" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="ErrorEstandardMedia" UniqueName="ErrorEstandardMedia" HeaderText="ErrorEstandardMedia" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="ErrorEstandardMedia" HeaderText="Error estándar de la media"  UniqueName="ErrorEstandardMediaEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtErrorEstandardMedia" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="ErrorMuestreo" UniqueName="ErrorMuestreo" HeaderText="ErrorMuestreo" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="ErrorMuestreo" HeaderText="Error de muestreo"  UniqueName="ErrorMuestreoEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtErrorMuestreo" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="PorErrorMuestreo" UniqueName="PorErrorMuestreo" HeaderText="PorErrorMuestreo" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="PorErrorMuestreo" HeaderText="Error de muestreo en %"  UniqueName="PorErrorMuestreoEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtPorErrorMuestreo" Width="60px">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="IntervaloConfianza" UniqueName="IntervaloConfianza" HeaderText="IntervaloConfianza" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="IntervaloConfianza" HeaderText="Intervalo de Confianza"  UniqueName="IntervaloConfianzaEdit" HeaderStyle-Width="165px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox runat="server" ID="TxtIntervaloConfianza" Width="150px"></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>        
                                                                </MasterTableView>
                                                                <FilterMenu EnableTheming="true">
                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                </FilterMenu>
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </div>
                                                        <div class="ibox-content">
                                                            <label class="col-sm-6 control-label centradolabel">ANALISIS DESCRIPTIVO (Realizar un breve análisis de los resultados obtenidos): </label>
                                                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtAnalisisDescriptivo"  TextMode="MultiLine" Height="100px" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                        </div>
                                                         <div class="ibox-content">
                                                            <div class="col-sm-5">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnGrabarAnalisis" visible="false">Grabar Ánalisis</a>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" >
                                                            <div class="col-sm-10">
                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrAnalisis" visible="false">
                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                    <asp:Label runat="server" ID="LblErrAnalisis" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <div class="alert alert-success alert-dismissable" runat="server" id="DivGodAnalisis" visible="false">
                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                    <asp:Label runat="server" ID="LblGodAnalisis" Font-Bold="true">Error</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProdNoForesalUno" visible="false">
                                                        Productos Forestales No Maderables
                                                        <div>
                                                            <div class="col-sm-2"><telerik:RadComboBox ID="CboTurno" Visible="false" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Rodal</label>
                                                            <div class="col-sm-2"><telerik:RadComboBox ID="CboRodal" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Año</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtYearNoForestal"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Área</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaNoForesal"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" id="DivProdNoForesalDos" visible="false">
                                                        <div><label class="col-sm-1 control-label centradolabel">Producto</label>
                                                            <div class="col-sm-4"><telerik:RadComboBox ID="CboProducto" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Unidad de medida</label>
                                                            <div class="col-sm-2"><telerik:RadComboBox ID="CboUMedida" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Unidad</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtPesoNoForestal"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" id="DivProdNoForesalTres" visible="false">
                                                        <div class="col-sm-5">
                                                            <a class="btn btn-primary m-b" runat="server" id="BtnAddProductoNoForestal">Agregar Producto</a>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrProdNoMaderable" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrProdNoMaderable" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodProdMaderable" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodProdMaderable" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <telerik:RadGrid runat="server" ID="GrdProdNoForestal" Skin="Telerik" CssClass="AddBorders"
                                                            AutoGenerateColumns="false" Width="100%"   
                                                            GridLines="Both" PageSize="20" >
                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                PageSizeLabelText="Regitros"/>
                                                            <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Turno,Rodal,Anis,Area,Codigo_Producto,Producto,Unidad_MedidaId,Unidad_Medida,Peso" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="Turno" UniqueName="Turno" HeaderText="Turno" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Anis" UniqueName="Anis" HeaderText="Año" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Area" UniqueName="Area" HeaderText="Área" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Codigo_Producto" UniqueName="Codigo_Producto" HeaderText="Codigo_Producto" Visible="false" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Producto" UniqueName="Producto" HeaderText="Producto" HeaderStyle-Width="365px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Unidad_MedidaId" UniqueName="Unidad_MedidaId" HeaderText="Unidad_MedidaId" Visible="false" HeaderStyle-Width="165px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Unidad_Medida" UniqueName="Unidad_Medida" HeaderText="Unidad de Medida" HeaderStyle-Width="165px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Peso" UniqueName="Peso" HeaderText="Unidad" HeaderStyle-Width="105px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelProdNoMaderable" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
                                                                </Columns>        
                                                            </MasterTableView>
                                                            <FilterMenu EnableTheming="true">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            </FilterMenu>
                                                            <ClientSettings>
                                                                <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <a class="btn btn-primary m-b" runat="server" id="BtnGrabarProdNoMaderables">Grabar Productos No Maderables</a>
                                                        </div>
                                                    </div>
                                                  
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageActividadesApro" Visible="false" runat="server"> <%-- Actividades de Aprovechamiento--%>
                                                    Descripción general de las actividades de aprovechamiento productos maderables/no maderables
                                                    Actividades de Pre-aprovechamiento
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Tipo de Producto:</label>
                                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboTipoProducto" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Tipo de Aprovechamiento:</label>
                                                            <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoAprovechamiento" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Actividad:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtActividadAprovechamiento" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnAddActividadApro" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="Div7" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label7" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodActividadAprovechamiento" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodActividadAprovechamiento" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" >
                                                        <telerik:RadGrid runat="server" ID="GrdActividadAprovechamiento" Skin="MetroTouch"
                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                            GridLines="Both" PageSize="20" >
                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                PageSizeLabelText="Regitros"/>
                                                            <MasterTableView Caption="" DataKeyNames="ActividadId,Descripcion_Actividad,Tipo_Aprovechamiento,Actividad" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="ActividadId" UniqueName="ActividadId" Visible="false" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Descripcion_Actividad" UniqueName="Descripcion_Actividad" HeaderText="Descripción Actividad" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Tipo_Aprovechamiento" UniqueName="Tipo_Aprovechamiento" HeaderText="Tipo Aprovechamiento" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Actividad" UniqueName="Actividad" HeaderText="Actividad" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividadAprovechamiento" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
                                                                </Columns>        
                                                            </MasterTableView>
                                                            <FilterMenu EnableTheming="true">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            </FilterMenu>
                                                        </telerik:RadGrid>
                                                    </div>
                                                    <div class="ibox-content">
                                                        Red de Caminos
                                                        <div><label class="col-sm-1 control-label centradolabel"></label>
                                                            <div class="col-sm-3"><label></label></div>
                                                            <div class="col-sm-3"><label>Existentes (km.)</label></div>
                                                            <div class="col-sm-3"><label>Por Construir (km.)</label></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Primario:</label>
                                                            <div class="col-sm-3"><label></label></div>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtPrimarioExistente"  step="any" min="0" type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtPrimarioConstruir"  step="any" min="0" type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Secundario:</label>
                                                            <div class="col-sm-3"><label></label></div>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtSecundarioExistente"  step="any" min="0" type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtSecundarioConstruir"  step="any" min="0" type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-1 control-label centradolabel">Otros:</label>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtOtroEspecifique" placeHolder="Especifique" CssClass="form-control" ></asp:TextBox></div>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtOtroExistente"  step="any" min="0" type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtOtroConstruir"  step="any" min="0" type="number"  CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <a class="btn btn-primary m-b" runat="server" id="BtnGrabarCaminos">Grabar</a>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="Div8" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label8" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodCaminos" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodCaminos" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageRepoblacion" Visible="false" runat="server"> <%-- Acciones de repoblación forestal del área boscosa dañada--%>
                                                    Acciones de repoblación forestal del área boscosa dañada
                                                    <div class="ibox-content">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordionRepo">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordionRepo" href="#collapseOneRepo">Especies</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOneRepo" class="panel-collapse collapse in">
                                                                                    <div class="panel-body">
                                                                                        <label class="col-sm-2 control-label">Seleccione Especie</label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadComboBox ID="CboEspecieRepoblacion" AllowCustomText="true" Filter="Contains"  Width="100%" runat="server"></telerik:RadComboBox>	
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" id="BtnAddEspecieRepo">Agregar Especie</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdEspecieRepo" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="EspecieId,Especie" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="EspecieId" UniqueName="EspecieId" HeaderText="EspecieId"  Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Especie" UniqueName="Especie" HeaderText="Especie" HeaderStyle-Width="325px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del"  HeaderStyle-Width="75px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton runat="server" ID="ImgDelEspeciePlan" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                                                            </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn> 
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="DivErrEspecieRepo" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrEspecieRepo" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-1 control-label centradolabel">Densidad Incial:</label>
                                                                <div class="col-sm-7"><asp:TextBox runat="server" ID="txtDensidadIni"  step="any" min="0" type="number"   CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="Div9" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label9" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodRepo" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodRepo" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <a class="btn btn-primary m-b" runat="server" id="btnGrabarRepo">Grabar</a>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPagePlanificacionManejo" Visible="false" runat="server"> <%-- Planificación del Manejo--%>
                                                        <div style="padding-bottom:2em;"></div>    
                                                        
                                                        <div class="ibox-content">
                                                            <telerik:RadGrid runat="server" ID="GrdSilvicultural" Skin="Telerik" CssClass="AddBorders"
                                                                AutoGenerateColumns="false" Width="100%"   
                                                                GridLines="Both" >
                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                    PageSizeLabelText="Regitros"/>
                                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Turno,Correlativo,Rodal,EspecieId,Nombre_Cientifico,AreaRodal,Clase_Desarrollo,Edad,Tratamiento,Dap,Altura,Densidad,AreaBasal,VolTroza,VolLena,VolOtro,VolTotal,sumadap,sumaaltura,arboles,SumBa,volumen,Troza,Pendiente,INC,VolHa,VolRodal,AreaBasalRodal" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Correlativo" UniqueName="Correlativo" HeaderText="Correlativo" Visible="false"  HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Turno" UniqueName="Turno" HeaderText="Turno" Visible="false" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Turno" HeaderText="Turno"  UniqueName="TurnoEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="1"  ID="TxtTurno" Width="60px">
                                                                                    <NumberFormat DecimalDigits="0" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    
                                                                        <telerik:GridBoundColumn DataField="AreaRodal" UniqueName="AreaRodal"  HeaderText="Area del Rodal (ha)" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="AreaRodal" HeaderText="Area del Rodal (ha)"  UniqueName="AreaRodalEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox MinValue="0"  runat="server" ID="TxtAreaRodal" Width="60px" Enabled="false">
                                                                                    <NumberFormat DecimalDigits="2" />
                                                                                </telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Clase_Desarrollo" UniqueName="Clase_Desarrollo"  HeaderText="Clase_Desarrollo" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Clase_Desarrollo" HeaderText="Clase Desarrollo"  UniqueName="Clase_Desarrollo_Edit" HeaderStyle-Width="130px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadComboBox runat="server" ID="CboClaseDesarrollo" Width="110px"></telerik:RadComboBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>  

                                                                        <telerik:GridBoundColumn DataField="Edad" UniqueName="Edad"  HeaderText="Edad" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Edad" HeaderText="Año"  UniqueName="EdadEdit" HeaderStyle-Width="80px" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="TxtEdad" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>


                                                                        <telerik:GridBoundColumn DataField="Pendiente" UniqueName="Pendiente"  HeaderText="Pendiente" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Pendiente" HeaderText="Pendiente %"  UniqueName="PendienteEdit" HeaderStyle-Width="80px" Visible="false" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox  MinValue="0"  runat="server" ID="TxtPendiente" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridBoundColumn DataField="INC" UniqueName="INC"  HeaderText="INC" Visible="false" HeaderStyle-Width="1px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="INC" HeaderText="INC"  UniqueName="INCEdit" HeaderStyle-Width="80px" Visible="false" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtINC" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridBoundColumn DataField="Tratamiento" UniqueName="Tratamiento"  HeaderText="Tratamiento" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Tratamiento" HeaderText="Tratamiento Silvicultural"  UniqueName="Tratamiento_Edit" HeaderStyle-Width="250px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadComboBox runat="server" ID="CboTratamiento" AutoPostBack="true" Width="230px" OnSelectedIndexChanged="CboTratamiento_SelectedIndexChanged"></telerik:RadComboBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>  

                                                                        <telerik:GridBoundColumn DataField="Otro" UniqueName="Otro"  HeaderText="Otro" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Otro" HeaderText="Otro Tratamiento"  UniqueName="Otro_Edit" HeaderStyle-Width="150px" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="TxtOtro" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>  


                                                                        <telerik:GridBoundColumn DataField="Existe" UniqueName="Existe"  HeaderText="Existe" Visible="false" HeaderStyle-Width="0px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Existe" HeaderText="Extrae"  UniqueName="ExtraeEdit" HeaderStyle-Width="40px" >
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="ChkExtrae" />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn DataField="" HeaderText="Arboles"  UniqueName="Arboles" HeaderStyle-Width="40px" >
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton runat="server" ID="ImgArboles" Visible="false" ImageUrl="~/Imagenes/24x24/treeinab.png" formnovalidate CommandName="CmdArboles" />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="EspecieId" Visible="false" UniqueName="EspecieId" HeaderText="EspecieId" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Dap" UniqueName="Dap"  HeaderText="Dap Medio (cm)" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Dap" HeaderText="Dap Medio (cm)"  UniqueName="DapEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox MinValue="0"  runat="server" ID="TxtDap" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura"  HeaderText="Dap Media (cm)" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Altura" HeaderText="Altura Media (m)"  UniqueName="AlturaEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtAltura" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Densidad" UniqueName="Densidad"  HeaderText="Densidad arboles/ha" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Densidad" HeaderText="Densidad arboles/ha"  UniqueName="DensidadEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtDensidad" Enabled="false" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="AreaBasal" UniqueName="AreaBasal"  HeaderText="Area Basal m2/ha" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="AreaBasal" HeaderText="Area Basal m2/ha"  UniqueName="AreaBasalEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtAreaBasal" Enabled="false" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="AreaBasalRodal" UniqueName="AreaBasalRodal"  HeaderText="AreaBasalRodal" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="AreaBasalRodal" HeaderText="Area basal  m2/rodal"  UniqueName="AreaBasalRodalEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtAreaBasalRodal" Width="60px" Enabled="false"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="VolTroza" UniqueName="VolTroza"  HeaderText="VolTroza" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="VolTroza" HeaderText="Vol. Troza"  UniqueName="VolTrozaEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtVolTroza" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="VolLena" UniqueName="VolLena" Visible="false" HeaderText="VolLena"  HeaderStyle-Width="125px" ></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="VolLena" HeaderText="Vol. Leña"  UniqueName="VolLenaEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxTVolLena" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="VolTotal" UniqueName="VolTotal" Visible="false" HeaderText="VolTotal"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="VolTotal" HeaderText="Vol. Total"  UniqueName="VolTotalEdit" HeaderStyle-Width="75px" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0" Enabled="false"  ID="TxtVolTotal" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridBoundColumn DataField="VolHa" UniqueName="VolHa"  HeaderText="VolHa" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="VolHa" HeaderText="Vol/Ha.(M3)"  UniqueName="VolHaEdit" HeaderStyle-Width="75px" Visible="false" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtVolHa" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                        <telerik:GridBoundColumn DataField="VolRodal" UniqueName="VolRodal"  HeaderText="VolRodal" Visible="false"  HeaderStyle-Width="75px" ></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="VolRodal" HeaderText="Vol/Rodal(M3)"  UniqueName="VolRodalEdit" HeaderStyle-Width="75px" Visible="false" >
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" MinValue="0"  ID="TxtVolRodal" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>


                                                                        <telerik:GridBoundColumn DataField="sumadap" UniqueName="sumadap"  HeaderText="sumadap" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="sumaaltura" UniqueName="sumaaltura"  HeaderText="sumaaltura" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="arboles" UniqueName="arboles"  HeaderText="arboles" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="SumBa" UniqueName="SumBa"  HeaderText="SumBa" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="volumen" UniqueName="volumen"  HeaderText="volumen" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Troza" UniqueName="Troza"  HeaderText="Troza" Visible="false"></telerik:GridBoundColumn>
                                                                    </Columns>        
                                                                </MasterTableView>
                                                                <FilterMenu EnableTheming="true">
                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                </FilterMenu>
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div class="col-sm-5">
                                                                <asp:Button runat="server" Text="Grabar Productos Maderables" formnovalidate  ID="BtnGrabarSilvicultura" class="btn btn-primary" />
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrSilvicultura" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrSilvicultura" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGodSilvicultura" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodSilvicultura" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        Productos Forestales No Maderables
                                                        <div class="ibox-content">
                                                            <telerik:RadGrid runat="server" ID="GrdProdNoMaderablesExtrae" Skin="Telerik" CssClass="AddBorders"
                                                                AutoGenerateColumns="false" Width="100%"   
                                                                GridLines="Both" PageSize="20" >
                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                    PageSizeLabelText="Regitros"/>
                                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Turno,Rodal,Anis,Area,Codigo_Producto,Producto,Unidad_MedidaId,Unidad_Medida,Peso" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn DataField="Turno" HeaderText="Turno"  UniqueName="TurnoEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtTurno" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridBoundColumn DataField="Turno" UniqueName="Turno" HeaderText="Turno" HeaderStyle-Width="45px" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Anis" UniqueName="Anis" HeaderText="Año" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Area" UniqueName="Area" HeaderText="Área" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Codigo_Producto" UniqueName="Codigo_Producto" HeaderText="Codigo_Producto" Visible="false" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Producto" UniqueName="Producto" HeaderText="Producto" HeaderStyle-Width="365px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Unidad_MedidaId" UniqueName="Unidad_MedidaId" HeaderText="Unidad_MedidaId" Visible="false" HeaderStyle-Width="165px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Unidad_Medida" UniqueName="Unidad_Medida" HeaderText="Unidad de Medida" HeaderStyle-Width="165px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Peso" UniqueName="Peso" HeaderText="Peso" HeaderStyle-Width="105px" Visible="false"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn DataField="Peso" HeaderText="Peso"  UniqueName="PesoEdit" HeaderStyle-Width="75px">
                                                                            <ItemTemplate>
                                                                                <telerik:RadNumericTextBox runat="server" ID="TxtPeso" Width="60px"></telerik:RadNumericTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>        
                                                                </MasterTableView>
                                                                <FilterMenu EnableTheming="true">
                                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                </FilterMenu>
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                                </ClientSettings>
                                                            </telerik:RadGrid>
                                                        </div>
                                                        <div class="ibox-content">
                                                            <div class="col-sm-5">
                                                                <asp:Button runat="server" Text="Grabar Productos No Maderables" formnovalidate ID="BtnGrabarProdNoMaderablesExtraeSave" class="btn btn-primary" />
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrNoMaderableExtrae" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrNoMaderableExtrae" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodMaderableExtrae" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodNoMaderableExtrae" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-3 control-label centradolabel">Criterio de Regulación de Corta:</label>
                                                                <div class="col-sm-3"><telerik:RadComboBox ID="CboCriterioReg" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                            <div><label class="col-sm-3 control-label centradolabel">Fórmula Utilizada:</label>
                                                                <div class="col-sm-3"><telerik:RadComboBox ID="CboFormula" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-3 control-label centradolabel">CAP m3:</label>
                                                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtCap" onkeyup="CopiaCap()" step="any" min="0" type="number" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-3 control-label centradolabel">Justificación de la fórmula Utilizada :</label>
                                                                <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtJustificacionFormula"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>    
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-3 control-label centradolabel">Actividades de Aprovechamiento:</label>
                                                                <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtActividadesApro"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><h3>RECUPERACIÓN DE LA MASA FORESTAL</h3></div>
                                                            <div><label class="col-sm-3 control-label centradolabel">Objetivos de la recuperación del Bosque:</label>
                                                                <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtObjetivosRecuperacion"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div><label class="col-sm-3 control-label centradolabel">Justificación de la especie a utilizar:</label>
                                                                <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtJustificacionEspecies"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" visible="false">
                                                            <div><label class="col-sm-3 control-label centradolabel">Sistema de Repoblación Forestal :</label>
                                                                <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtSistemaRepo"  TextMode="MultiLine" Height="100px" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div class="col-sm-5">
                                                                <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarOtrosAprovecha" class="btn btn-primary" />
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPlanificacion" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrPlanificacion" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodPlanificacion" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodPlanificacion" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordionRepoblacion">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordionRepoblacion" href="#collapseOneRepoblacion">Compromiso de Repoblación Forestal</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOneRepoblacion" class="panel-collapse collapse in">
                                                                                    <div class="panel-body" >
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-2">
                                                                                                <a class="btn btn-primary m-b" runat="server" id="BtnGeneraRepo">Generar datos en base a Silvicultura</a>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="ibox-content">
                                                                                            <label class="col-sm-2 control-label">Área basal a extraer (ha)</label>
                                                                                            <div class="col-sm-3">
                                                                                                <asp:TextBox runat="server" ID="TxtAreaBasalExtrae" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                            </div>
                                                                                            <label class="col-sm-2 control-label">Área basal a existente (ha):</label>
                                                                                            <div class="col-sm-3">
                                                                                                <asp:TextBox runat="server" ID="TxtAreaBasalExis" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                            </div>
                                                                                            
                                                                                        </div>
                                                                                        <div class="ibox-content">
                                                                                            <label class="col-sm-2 control-label">Área total a  intervenir (ha):</label>
                                                                                            <div class="col-sm-3">
                                                                                                <asp:TextBox runat="server" ID="TxtAreaTotIntervenir" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                            </div>
                                                                                            <label class="col-sm-2 control-label">Área de compromiso (ha):</label>
                                                                                            <div class="col-sm-3">
                                                                                                <asp:TextBox runat="server" ID="TxtAreaCompromiso" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-sm-2">
                                                                                                <a class="btn btn-primary m-b" visible="false" runat="server" id="BtnCalcularCompromisoSilvi">Calcular Área</a>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-2">
                                                                                                <a class="btn btn-primary m-b" runat="server" id="BtnGrabarCompromisoRepo">Grabar</a>
                                                                                            </div>
                                                                                        </div>
                                                                                        
                                                                                    </div>
                                                                                    
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="DivErrCalculoRepo" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrCalculoRepo" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                                <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodCalculoRepo" visible="false">
                                                                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblGoodCalculoRepo" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                            </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server">
                                                            <div><h3>DESCRIPCIÓN DEL MÉTODO</h3></div>
                                                            <div><label class="col-sm-1 control-label centradolabel">Turno</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtTurnoRepo"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-1 control-label centradolabel">Rodal</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtRodalRepo"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-1 control-label centradolabel">Área</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaRepo"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server">
                                                            <div><label class="col-sm-1 control-label centradolabel">Etapa</label>
                                                                <div class="col-sm-3"><telerik:RadComboBox ID="CboEtapa" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                                
                                                            </div>
                                                            <div><label class="col-sm-2 control-label centradolabel">Actividades Silviculturales</label>
                                                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtTrataminetoRepo" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            
                                                            <div><label class="col-sm-1 control-label centradolabel">Año</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAnisRepo"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            
                                                            
                                                           
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server">
                                                            <div><label class="col-sm-1 control-label centradolabel">Especie</label>
                                                                <div class="col-sm-4"><telerik:RadComboBox ID="CboEspecieRepo" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                            <div><label class="col-sm-1 control-label centradolabel">Descripción</label>
                                                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtDescripcion" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server">   
                                                            <div><label class="col-sm-1 control-label centradolabel">Densidad</label>
                                                                <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtDensidadRepo"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                            </div> 
                                                            <div><label class="col-sm-2 control-label centradolabel">Tiempo de ejecución</label>
                                                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtTiempoEjecucion" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                           
                                                        </div>
                                                        <div class="ibox-content" runat="server">   
                                                             <div><label class="col-sm-2 control-label centradolabel">Otras Actividades Silviculturales</label>
                                                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtOtrasActividades" CssClass="form-control"></asp:TextBox></div>
                                                            </div>
                                                            <div><label class="col-sm-2 control-label centradolabel">Sistema de Repoblación</label>
                                                                <div class="col-sm-3"><telerik:RadComboBox ID="CboSistemaRepo" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content">
                                                            <div class="col-sm-5">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnAddEspecieRepoPlanifica">Agregar Especie</a>
                                                            </div>
                                                        </div>
                                                        <div style="padding-bottom:2em;"></div>
                                                        <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrEspecieRepoPlanifica" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrEspecieRepoPlanifica" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodEspecieRepo" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodEspecieRepo" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                        </div>
                                                        <div class="ibox-content">
                                                        <telerik:RadGrid runat="server" ID="GrdEspeciesRepoblacion" Skin="Telerik" CssClass="AddBorders"
                                                            AutoGenerateColumns="false" Width="100%"   
                                                            GridLines="Both" PageSize="20" >
                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                PageSizeLabelText="Regitros"/>
                                                            <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="TurnoRepo,RodalRepo,EtapaRepo,EtapaIdRepo,AreaRepo,Tratamiento,AnisRepo,EspecieRepoId,EspecieRepo,Descripcion,TiempoEje,OtrasActividades,DensidadRepo,SistemaRepoId,SistemaRepo" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="TurnoRepo" UniqueName="Turno" HeaderText="Turno" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="RodalRepo" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="EtapaIdRepo" UniqueName="EtapaIdRepo" HeaderText="EtapaIdRepo" Visible="false" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="AreaRepo" UniqueName="Area" HeaderText="Área" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="EtapaRepo" UniqueName="EtapaRepo" HeaderText="Etapa" HeaderStyle-Width="165px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Tratamiento" UniqueName="Tratamiento" HeaderText="Tratamiento" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="AnisRepo" UniqueName="Anis" HeaderText="Año" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="EspecieRepoId" UniqueName="EspecieRepoId" HeaderText="Especie" Visible="false" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="EspecieRepo" UniqueName="EspecieRepo" HeaderText="Especie" HeaderStyle-Width="265px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Descripcion" UniqueName="Descripcion" HeaderText="Descripcion" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="DensidadRepo" UniqueName="Densidad" HeaderText="Densidad" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="TiempoEje" UniqueName="TiempoEje" HeaderText="Tiempo Ejecución" HeaderStyle-Width="65px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="OtrasActividades" UniqueName="OtrasActividades" HeaderText="Otras Actividades silviculturales" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="SistemaRepoId" UniqueName="SistemaRepoId" HeaderText="SistemaRepoId" Visible="false" HeaderStyle-Width="165px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="SistemaRepo" UniqueName="SistemaRepo" HeaderText="Sistema de Repoblación" HeaderStyle-Width="105px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del"  HeaderStyle-Width="50px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelEspeciesRepoblacion" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
                                                                </Columns>        
                                                            </MasterTableView>
                                                            <FilterMenu EnableTheming="true">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            </FilterMenu>
                                                            <ClientSettings>
                                                                <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                            </ClientSettings>
                                                        </telerik:RadGrid>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <a class="btn btn-primary m-b" runat="server" id="BtnSaveEspeciesRepo">Guardar Especies</a>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>

                                                    <div class="ibox-content" runat="server" >
                                                    <div class="col-sm-10">
                                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrEspeciesRepo" visible="false">
                                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                            <asp:Label runat="server" ID="LblErrEspeciesRepo" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodEspeciesRepo" visible="false">
                                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                            <asp:Label runat="server" ID="LblGoodEspeciesRepo" Font-Bold="true">Error</asp:Label>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageDatosPlan" runat="server" Visible="false"> <%--Información General ((Datos Plan Manejo))--%>
                                                    <div class="ibox-title">
                                                        <h2><strong>1.4 Información General (Datos Plan Manejo)</strong></h2>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Tiempo de Ejecución del Plan de Manejo Forestal (Años):</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtTiempoPlanManejo" CssClass="form-control" type="number" min="1" max="5"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Tiempo de Ejecución de la extracción: (Meses)</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtTiempoExtraccion"  min="1" max="12"  type="number"  CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <h3>Propuesta de Manejo</h3>
                                                        <div><label class="col-sm-5 control-label centradolabel">Volumen a extraer (metros cubicos)</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtVolExtraer" Enabled="false"  CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Sistema de Corta</label>
                                                            <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtSistemaCorta" Enabled="false" CssClass="form-control" ></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Incremento Anual (M3 /Ha/Año)</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtIncrementoAnual" Enabled="false"  step="any" min="0" type="number" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Corta Anual Permisible (Metros Cúbicos )</label>
                                                            <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtCortaAnual" Enabled="false"  CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>

                                                    <div class="ibox-content" runat="server" id="DivCambioUso" visible="false">
                                                        <div><label class="col-sm-3 control-label centradolabel">Cambio de Uso Forestal a:</label>
                                                            <div class="col-sm-3"><telerik:RadComboBox ID="CboCambioUsoForestal" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div runat="server" visible="false" id="DivEspecifiqueCambioUso"><label class="col-sm-3 control-label centradolabel">Especifique:</label>
                                                            <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtCambioUsoEspecifique"  step="any" min="0" type="number"  CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>

                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordion6">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordion6" href="#collapseOne6">Especies a Manejar</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOne6" class="panel-collapse collapse in">
                                                                                    <div class="panel-body" runat="server" visible="false">
                                                                                        <label class="col-sm-2 control-label">Seleccione Especie</label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadComboBox ID="CboEspeciePlanManejo" AllowCustomText="true" Filter="Contains"  Width="100%" runat="server"></telerik:RadComboBox>	
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" visible="false" id="BtnAddEspeciePlanManejo">Agregar Especie</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdEspeciePLanManejo" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="EspecieId,Especie" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="EspecieId" UniqueName="EspecieId" HeaderText="EspecieId"  Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Especie" UniqueName="Especie" HeaderText="Especie" HeaderStyle-Width="325px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="false" UniqueName="Del"  HeaderStyle-Width="75px">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton runat="server" ID="ImgDelEspeciePlan" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                                                            </ItemTemplate>
                                                                                                    </telerik:GridTemplateColumn> 
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="DivErrEspeciePlan" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrEspeciePlan" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Sistema de Repoblación Forestal</label>
                                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboSistemaRepoblacion" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los items seleccionados" Localization-CheckAllString="Seleccionar Todos" Localization-ItemsCheckedString="Seleccionados" CheckBoxes="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div>
                                                            <label class="col-sm-1 control-label"></label>
                                                                <div class="col-sm-10">
                                                                    <div class="panel-body">
                                                                        <div class="panel-group" id="accordion7">
                                                                            <div class="panel panel-default">
                                                                                <div class="panel-heading">
                                                                                    <h5 class="panel-title">
                                                                                        <a data-toggle="collapse" data-parent="#accordion7" href="#collapseOne7">Poligóno Área Repoblación Forestal</a>
                                                                                    </h5>
                                                                                </div>
                                                                                <div id="collapseOne7" class="panel-collapse collapse in">
                                                                                    <div class="panel-body">
                                                                                        <label class="col-sm-2 control-label">Archivo de Poligónos</label>
                                                                                        <div class="col-sm-5">
                                                                                            <telerik:RadAsyncUpload runat="server" ID="UploadPolAreaRepo" Culture="es-GT" MaxFileInputsCount="1"
                                                                                                    AllowedFileExtensions="xlsx">
                                                                                            </telerik:RadAsyncUpload>
                                                                                        </div>
                                                                                        <div class="col-sm-2">
                                                                                            <a class="btn btn-primary m-b" runat="server" id="btnCargarPolAreaRepo">Cargar</a>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <telerik:RadGrid runat="server" ID="GrdPolAreaRepo" Skin="Telerik"
                                                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                            GridLines="Both" PageSize="20" >
                                                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                                PageSizeLabelText="Regitros"/>
                                                                                            <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                                </Columns>        
                                                                                            </MasterTableView>
                                                                                            <FilterMenu EnableTheming="true">
                                                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                                            </FilterMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="ibox-content">
                                                                                            <div class="col-sm-9">
                                                                                                <div class="alert  alert-success alert-dismissable" runat="server" id="Div4" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="Label4" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPolAreaRepo" visible="false">
                                                                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                                                    <asp:Label runat="server" ID="LblErrAreaRepo" Font-Bold="true">Error</asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div style="padding-bottom:2em;"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Tipo de Garantía</label>
                                                            <div class="col-sm-5"><telerik:RadComboBox ID="CboTipoGarantia"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarInfoGenPlan" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrorInfoGenPlan" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrorInfoGenPlan" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodInfoGenPlan" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodInfoGenPlan" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageProteccionForestal" Visible="false" runat="server"> <%-- Proteccion Forestal--%>
                                                    <div class="ibox-content">
                                                        <div><h3><label class="col-sm-9 control-label centradolabel">Medidas de prevención y control contra incendios forestales.</label></h3>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtMedidasPrevencion" Text="Datos" Visible="false"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionAgua" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Protección de fuentes de agua:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtProteccionAgua"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForJustPrev" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Justificación de la Prevención contra Incendios Forestales:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtJustificacionPrevencionIF"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForLineaControlRonda" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Línea(s) de control y Ronda(s) cortafuego:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtLineasControlRonda"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForVigilanciaPuestoPunto" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Vigilancia: Puesto(s) o punto(s) de control y recorridos por el área:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtVigilanciaPuestoPunto"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForManejoCombustible" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Manejo de combustibles/Silvicultura Preventiva (combustibles vivos y/o muertos):</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtManejoCombustible"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForAreasCriticas" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Identificación de áreas críticas (topografía, combustibles, áreas mayormente susceptibles a Incendios Forestales en la periferia):</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtIdentificacionAreaCritica"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForRespuestaCasoIF" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Respuesta en caso de Incendios Forestales:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtRespuestaCasoIf"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivAmpliacionRonda" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Ampliación de ronda donde existen mayores cargas de combustibles:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtAmpliacionRonda"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivRondasCortaFuego" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Rondas cortafuego intermedias (anexar mapa):</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TXtRondasCortaFuego"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivBrigadaIncendio" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Brigada de Incendios Forestales:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtBrigadaIncendio"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivIdentificacionRutaEscape" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Identificación de Rutas de Escape y Zonas de Seguridad:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtIdentificacionRutaEscape"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><h3><label class="col-sm-9 control-label centradolabel"><asp:Label runat="server" ID="LblPrevecionPlaga" Text="Prevención y control de plagas y enfermedades"></asp:Label></label></h3>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtPrevencionControlPlaga" Text="Datos" Visible="false"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionOtrosFac" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Protección  contra otros factores:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtProteccionOtrosFac"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForJustificacionPF" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Justificación de la Prevención contra Plagas Forestales:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtJustificacionPf"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForMonitoreoPlaga" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Monitoreo de plagas forestales:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtMonitoreoPlaga"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivProteccionForControlPlaga" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Control de plagas forestales:</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtControlPlaga"  TextMode="MultiLine" Height="100px" CssClass="form-control" required=""></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarProteccionForestal" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="Div5" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="Label5" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodProteccionForestal" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodProteccionForestal" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </telerik:RadPageView>

                                                <telerik:RadPageView ID="RadPageCronograma" Visible="false" runat="server"> <%-- Cronograma--%>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label">Tipo Actividad</label>
                                                            <div class="col-sm-3"><telerik:RadComboBox ID="CboTipoActividad" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                                        </div>
                                                        <div><label class="col-sm-1 control-label centradolabel">Sub Actividad:</label>
                                                            <div class="col-sm-6">
                                                                <telerik:RadComboBox ID="CboActividad" Width="100%" AutoPostBack="true" runat="server"></telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" id="DivOtros" visible="false">
                                                        <div><label class="col-sm-2 control-label centradolabel">Otros</label>
                                                            <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtOtros" CssClass="form-control"></asp:TextBox></div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label">Fecha de Inicio</label>
                                                            <div class="col-sm-4"><telerik:RadDatePicker ID="TxtFecIni" Width="100%" runat="server"></telerik:RadDatePicker></div>
                                                        </div>
                                                        <div><label class="col-sm-2 control-label centradolabel">Fecha de Finalización:</label>
                                                            <div class="col-sm-4"><telerik:RadDatePicker ID="TXtFecFin" Width="100%" runat="server"></telerik:RadDatePicker></div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-2 control-label">Cantidad de años</label>
                                                            <telerik:RadNumericTextBox runat="server" Value="1" MinValue="0" ID="TxtCantYear" Width="60px" >
                                                                <NumberFormat  DecimalDigits="0" />
                                                            </telerik:RadNumericTextBox>
                                                        </div>
                                                        
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-sm-5">
                                                            <asp:Button runat="server" Text="Grabar"  ID="BtnGrabarActicidad" class="btn btn-primary" />
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrActividad" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrActividad" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodActividad" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodActividad" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" >
                                                        <telerik:RadGrid runat="server" ID="GrdActividades" Skin="MetroTouch"
                                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                            GridLines="Both" PageSize="20" >
                                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                PrevPageText="Anterior" Position="Bottom" 
                                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                PageSizeLabelText="Regitros"/>
                                                            <MasterTableView Caption="" DataKeyNames="Id,Tipo_Actividad,ActividadId,Actividad,Fec_Ini,Fec_Fin" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="Id" UniqueName="Id" Visible="false" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Tipo_Actividad" UniqueName="Tipo_Actividad" HeaderText="Actividad" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Actividad" UniqueName="Actividad" HeaderText="Sub Actividad" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="ActividadId" Visible="false" UniqueName="ActividadId" HeaderText="Actividad" HeaderStyle-Width="425px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Fec_Ini" UniqueName="Fec_Ini" HeaderText="Fecha de Inicio" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Fec_Fin" UniqueName="Fec_Ini" HeaderText="Fecha de Finalización" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividad" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
                                                                </Columns>        
                                                            </MasterTableView>
                                                            <FilterMenu EnableTheming="true">
                                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                                            </FilterMenu>
                                                        </telerik:RadGrid>
                                                    </div>
                                                </telerik:RadPageView>
                                                <telerik:RadPageView ID="RadPageAnexo" runat="server"> <%-- Anexos--%>
                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Croquis de acceso a la finca desde el casco municipal</label>
                                                            <div class="col-sm-5">
                                                                <telerik:RadAsyncUpload runat="server" ID="UploadAnexoCroquis" Culture="es-GT" MaxFileInputsCount="1"
                                                                        AllowedFileExtensions="pdf">
                                                                </telerik:RadAsyncUpload>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargarCroquis">Cargar</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
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
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividad" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
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
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>

                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Mapa de uso actual y recursos hidricos</label>
                                                            <div class="col-sm-5">
                                                                <telerik:RadAsyncUpload runat="server" ID="UploadAnexoMapaUsoActual" Culture="es-GT" MaxFileInputsCount="1"
                                                                        AllowedFileExtensions="pdf">
                                                                </telerik:RadAsyncUpload>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargarMapaUsoActual">Cargar</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
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
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividadMapa" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
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
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>


                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Mapa de pendientes</label>
                                                            <div class="col-sm-5">
                                                                <telerik:RadAsyncUpload runat="server" ID="UploadAnexoMapaPendiente" Culture="es-GT" MaxFileInputsCount="1"
                                                                        AllowedFileExtensions="pdf">
                                                                </telerik:RadAsyncUpload>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargarMapaPendiente">Cargar</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
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
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividadMapaPendiente" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
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
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>

                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Mapa  Integral (rodalización del área de manejo y ubicación de parcelas de muestreo y protección)</label>
                                                            <div class="col-sm-5">
                                                                <telerik:RadAsyncUpload runat="server" ID="UploadAnexoMapaUbicacion" Culture="es-GT" MaxFileInputsCount="1"
                                                                        AllowedFileExtensions="pdf">
                                                                </telerik:RadAsyncUpload>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargarMapaUbicacion">Cargar</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
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
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividadMapaUbicacion" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
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
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>

                                                    <div class="ibox-content">
                                                        <div><label class="col-sm-5 control-label centradolabel">Mapa de rondas corta fuegos perimetrales e intermedias dentro del area de compromiso de repoblacion forestal</label>
                                                            <div class="col-sm-5">
                                                                <telerik:RadAsyncUpload runat="server" ID="UploadAnexoMapaRonda"  Culture="es-GT" MaxFileInputsCount="1"
                                                                        AllowedFileExtensions="pdf">
                                                                </telerik:RadAsyncUpload>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargarMapaRonda">Cargar</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>
                                                    <div class="ibox-content" runat="server" >
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
                                                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton runat="server" ID="ImgDelActividadMapaRonda" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn> 
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
                                                    </div>
                                                    <div style="padding-bottom:2em;"></div>

                                                    <div class="ibox-content" runat="server" >
                                                        <div class="col-sm-10">
                                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrAnexo" visible="false">
                                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblErrAnexo" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodAnexo" visible="false">
                                                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                <asp:Label runat="server" ID="LblGoodAnexo" Font-Bold="true">Error</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" >
                                                        <div><h3><label class="col-sm-5 control-label centradolabel">Poligonos</label></h3></div>

                                                    </div>
                                                    <div class="ibox-content">
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
                                                        </div>
                                                        <div class="ibox-content" runat="server" >
                                                            <div class="col-sm-6">
                                                                <h4><label class="col-sm-6 control-label centradolabel">Poligono de Repoblación</label></h4>
                                                                <asp:ImageButton runat="server" ID="ImgPolRepoblacion" ImageUrl="~/Imagenes/24x24/ubication.png" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                
                                                            </div>
                                                            
                                                        </div>
                                                        <div class="ibox-content" runat="server" >
                                                            <div class="col-sm-5">
                                                                <h4><label class="col-sm-5 control-label centradolabel">Censo / Muestreo</label></h4>
                                                                <asp:ImageButton runat="server" ID="ImgPrintCenso" ImageUrl="~/Imagenes/24x24/blank.png" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                
                                                            </div>
                                                            
                                                        </div>
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
                                                </telerik:RadPageView>
                                            </telerik:RadMultiPage>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                    <Windows>
                        <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                            Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                        </telerik:RadWindow>
                        <telerik:RadWindow RenderMode="Lightweight" ID="RadVerAnexo" runat="server" Modal="true" Width="800px"
                            Height="600px" Title="Anexos" Behaviors="Default">
                        </telerik:RadWindow>
                        <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="220px" Width="250px" Title="Confirmación" Behaviors="Close">
                        <ContentTemplate>
                            <asp:Label ID="LblTitConfirmacion" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            
                            <div class="ibox-content">
                                <div class="col-sm-3">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ask.png" />
                                </div>
                                <div class="col-sm-3">
                                    

                                </div>
                                <div class="col-sm-2">
                                    <a class="btn btn-primary m-b" runat="server" id="BtnYes" data-loading-text="Enviando...">Enviar</a>
                                </div>
                                <%--<div class="col-sm-2">
                                    <a class="btn btn-primary m-b" runat="server" id="BtnNo">No</a>
                                </div>--%>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWindowDetalle" Modal="true" Height="420px" Width="850px" Title="Detalle de Arboles" Behaviors="Close">
                        <ContentTemplate>
                            <asp:Label ID="Label10" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            <div class="ibox-content">
                                <div class="col-sm-2">
                                    <a class="btn btn-primary m-b" runat="server" id="BtnGrabarArboles">Grabar</a>
                                </div>
                                <div class="col-sm-8">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrArboles" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblErrArboles" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <telerik:RadGrid runat="server" ID="GrdArboles" Skin="Vista"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Rodal,No,Dap,Altura,Nombre_Cientifico,areabasal,Volumen,VolTroza,VolLena,Extrae,EspecieId" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal"  HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="No" UniqueName="No" HeaderText="No"  HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Dap" UniqueName="Dap" HeaderText="Dap"  HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura" HeaderText="Altura" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Seleccionar"  Visible="true" UniqueName="Seleccionar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" id="ChkSeleccionaArbol" />
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridBoundColumn DataField="areabasal" UniqueName="areabasal" HeaderText="AB" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Volumen" UniqueName="Volumen" HeaderText="Volumen" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VolTroza" UniqueName="VolTroza" HeaderText="Troza" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VolLena" UniqueName="VolLena" HeaderText="Leña" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Extrae" UniqueName="Extrae" HeaderText="Extrae" Visible="false" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EspecieId" UniqueName="EspecieId" HeaderText="EspecieId" Visible="false" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWinCenso" Modal="true" Height="420px" Width="650px" Title="Censo/Muestro" Behaviors="Default">

                    </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtUsrOwnPlan" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtEsNuevaFinca" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtInmuebleId" Text="0" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtAsignacionId" Text="0" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtAreaInmueble" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtRodalArbol" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtEspecieIdArbol" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtSubCategoria" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtSubRegionId" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtRegion" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtSubRegion" Text="0" style="display:none;"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtRegionId" Text="0" style="display:none;"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function pageLoad() {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
            $('#<%=BtnYes.ClientID%>').click(function () {
                 $(this).button('loading').delay(100000).queue(function () {
                     $(this).button('reset');
                     $(this).dequeue();
                     $(this).data('loading-text', 'Cargando...');
                 });
             });

         }
        </script>
</asp:Content>
