<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_ManejoForestal.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_ManejoForestal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h2><strong>Manejo Forestal</strong></h2>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Aprovechamiento Forestales</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImageButton1" PostBackUrl="~/WebForms/Wfrm_GestionManejoForestal.aspx?taille=ctiVKPaat!o%3d&categorie=Y%2fU!OPphNSs%3d" class="img-circle"  ImageUrl="~/Imagenes/aprovechamientoforestales.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content ">
                                        <h4 class="m-b-md">Licencia con fines científicos</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="BtnImage1" PostBackUrl="~/WebForms/Wfrm_GestionManejoForestal.aspx?taille=ctiVKPaat!o%3d&categorie=k4f4YBgkUBg%3d" class="img-circle"  ImageUrl="~/Imagenes/Licenciaconfinescientificos.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Licencia de saneamiento</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" ID="ImageButton2" PostBackUrl="~/WebForms/Wfrm_GestionManejoForestal.aspx?taille=ctiVKPaat!o%3d&categorie=C30tI2NxL2A%3d" class="img-circle"  ImageUrl="~/Imagenes/licenciadesaneamiento.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Licencia de salvamento</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" PostBackUrl="~/WebForms/Wfrm_GestionManejoForestal.aspx?taille=ctiVKPaat!o%3d&categorie=YR96UUWnIOQ%3d" ID="ImageButton3"  class="img-circle"  ImageUrl="~/Imagenes/licenciadesalvamento.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Aprovechamientos con fines sanitarios</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImageButton4" PostBackUrl="~/WebForms/Wfrm_GestionManejoForestal.aspx?taille=ctiVKPaat!o%3d&categorie=IOYS23rg2D0%3d"  class="img-circle"  ImageUrl="~/Imagenes/Ir.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content ">
                                        <h4 class="m-b-md">Manejo Forestal de Cambio de Uso</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImageButton5" PostBackUrl="~/WebForms/Wfrm_GestionManejoForestal.aspx?taille=ctiVKPaat!o%3d&categorie=RtYhhgn%2foFc%3d"   class="img-circle"  ImageUrl="~/Imagenes/Ir.PNG" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Técnicos y Profesionales que se dedican a la Actividad Forestal</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton PostBackUrl="~/WebForms/Wfrm_Inscripcion_TecPro.aspx" runat="server" ID="ImageButton6"  class="img-circle"  ImageUrl="~/Imagenes/Ir.PNG" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Motosierras</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" ID="ImageButton7" PostBackUrl="~/WebForms/Wfrm_Inscripcion_Motosierras.aspx"  class="img-circle"  ImageUrl="~/Imagenes/Ir.PNG" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                     <%--   <div class="row">
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Entidades Relacionadas con Investigación,  Extensión y Capacitación  Forestal y Agroforestal</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" PostBackUrl="~/WebForms/Wfrm_Inscripcion_Entidad.aspx" ID="ImageButton8"  class="img-circle"  ImageUrl="~/Imagenes/Ir.PNG" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Otras Inscripciones que por Disposición Legal se Ordenen</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" ID="ImageButton9"  class="img-circle"  ImageUrl="~/Imagenes/Ir.PNG" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
