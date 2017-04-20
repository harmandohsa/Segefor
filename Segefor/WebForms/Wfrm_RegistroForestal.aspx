<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_RegistroForestal.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_RegistroForestal" %>
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
                            <h2><strong>Registro Nacional Forestal</strong></h2>
                        </div>
                        <div class="row">
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Empresas Forestales</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImgEmpresas" PostBackUrl="~/WebForms/Wfrm_InscripcionEmpresas.aspx"  class="img-circle"  ImageUrl="~/Imagenes/empresasforestales.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content ">
                                        <h4 class="m-b-md">Plantaciones Forestales</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImgPlantacionFores" PostBackUrl="~/WebForms/Wfrm_Inscripcion_PlantacionForestal.aspx" class="img-circle"  ImageUrl="~/Imagenes/plantacionesforestales.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Sistemas Agroforestales</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" ID="ImgSistemaAgro" PostBackUrl="~/WebForms/Wfrm_Inscripcion_SistemaAgroForestal.aspx"  class="img-circle"  ImageUrl="~/Imagenes/sistemasagroforestales.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Plantaciones de Árboles Frutales</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" PostBackUrl="~/WebForms/Wfrm_Inscripcion_Arboles_Frutales.aspx" ID="ImgPlantacionArbol"  class="img-circle"  ImageUrl="~/Imagenes/plantacionesdearbolesfrutales.png" />
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
                                        <h4 class="m-b-md">Bosques Naturales y Tierras de Vocación Forestal</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImgBosqueNat" class="img-circle"  ImageUrl="~/Imagenes/bosquesnaturalesytierrasdevocacionforestal.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content ">
                                        <h4 class="m-b-md">Fuentes Semilleras y de Material Vegetativo</h4>
                                        <h2 class="text-navy">
                                            <asp:ImageButton runat="server" ID="ImgFuenteSemilla" PostBackUrl="~/WebForms/Wfrm_Inscripcion_FuentesSemillera.aspx"  class="img-circle"  ImageUrl="~/Imagenes/fuentesemilleras.PNG" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h4 class="m-b-md">Técnicos y Profesionales que se dedican a la Actividad Forestal</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton PostBackUrl="~/WebForms/Wfrm_Inscripcion_TecPro.aspx" runat="server" ID="ImgTecnico"  class="img-circle"  ImageUrl="~/Imagenes/tecnicosyprofesionalesque sededicanalaactividadforestal.png" />
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
                                            <asp:ImageButton runat="server" ID="ImgMoto" PostBackUrl="~/WebForms/Wfrm_Inscripcion_Motosierras.aspx"  class="img-circle"  ImageUrl="~/Imagenes/motosierras.png" />
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
                                        <h4 class="m-b-md">Entidades Relacionadas con Investigación,  Extensión y Capacitación  Forestal y Agroforestal</h4>
                                        <h2 class="text-danger">
                                            <asp:ImageButton runat="server" PostBackUrl="~/WebForms/Wfrm_Inscripcion_Entidad.aspx" ID="ImgEntidad"  class="img-circle"  ImageUrl="~/Imagenes/entidadesrelacionadasconinvestigacionextensionycapacitacion.png" />
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
                                            <asp:ImageButton runat="server" ID="ImgOtras"  class="img-circle" PostBackUrl="~/WebForms/Wfrm_EnConstruccion.aspx"  ImageUrl="~/Imagenes/otrasinscripcionesquepordisposicionlegalseordenen.png" />
                                        </h2>
                                        <small>Click para iniciar el tramite</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
