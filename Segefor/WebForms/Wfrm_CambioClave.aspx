<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_CambioClave.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_CambioClave" %>
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
                                <h2><strong>Cambio de Clave</strong></h2>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label">Contraseña actual:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtClaveActual" CssClass="form-control" TextMode="Password" required></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label">Nueva contraseña:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNuevaClave" CssClass="form-control" TextMode="Password" required></asp:TextBox></div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label">Confirmar nueva contraseña:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtConfClave" CssClass="form-control" TextMode="Password" required></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div class="col-sm-2">
                                    <asp:Button runat="server" Text="Cambiar Clave"  ID="BtnCambia" class="btn btn-primary" />
                                </div>
                                <div class="col-sm-9">
                                <div class="alert alert-danger alert-dismissable" runat="server" id="BtnEror" visible="false">
                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                    <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
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
