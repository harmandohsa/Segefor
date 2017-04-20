<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_CatalogoImgBosque.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_CatalogoImgBosque" %>
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
                                    <h2><strong>Imagenes Bosques Naturales</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-2 control-label centradolabel">Mapa Bosque Natural</label>
                                        <div class="col-sm-4"><telerik:RadAsyncUpload MaxFileSize="1048576" Culture="es-GT"  runat="server" ID="UploadMapaBosque" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".jpg" PostbackTriggers="BtnSubirFoto" DropZones=".DropZone1" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button runat="server" Text="Subir Mapa"  ID="BtnSubirFoto" class="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content" id="DivNoErrMapaBosque" runat="server" visible="false">
                                    <div class="col-sm-8">
                                        <div class="alert alert-success alert-dismissable" runat="server" id="Btnsuccesdatgen">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeNoErrMapaBosque" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <asp:Image runat="server" ID="ImgMapaBosque" Height="500px" Width="500px" />
                                </div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-2 control-label centradolabel">Mapa Tierras de Vocación Forestal</label>
                                        <div c  lass="col-sm-4"><telerik:RadAsyncUpload MaxFileSize="1048576" Culture="es-GT" runat="server" ID="UploadMapaVocacion" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".jpg" PostbackTriggers="BtnUploadMapaVocacion" />
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button runat="server" Text="Subir Mapa"  ID="BtnUploadMapaVocacion" class="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content" id="DivNoErrMapaVocacion" runat="server" visible="false">
                                    <div class="col-sm-8">
                                        <div class="alert alert-success alert-dismissable" runat="server" id="Div2">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeNoErrMapaVocacion" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <asp:Image runat="server" ID="ImgMapaVoca" Height="500px" Width="500px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
