<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_GestionManejoForestal.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_GestionManejoForestal" %>
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
                                    <h2><strong><asp:Label runat="server" ID="LblTitulo"></asp:Label></strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-4 control-label centradolabel">Cantidad de Área para aprovechar (ha):</label>
                                        <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaAprovecha" required=""  step="any" type="number" min="0" CssClass="form-control"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <asp:Button runat="server" Text="Siguiente"  ID="BtnSiguiente" class="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
