<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_GestionManejoForestalAsocRegente.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_GestionManejoForestalAsocRegente" %>
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
                                        <div class="col-sm-2"><asp:TextBox runat="server" Enabled="false" ID="TxtAreaAprovecha" required=""  step="any" type="number" min="0" CssClass="form-control"></asp:TextBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <h4><strong>Buscar Elaborador de Plan de Manejo</strong></h4>
                                     <div><label class="col-sm-2 control-label centradolabel">Código RNF:</label>
                                        <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtRnf" ReadOnly="true" class="form-control"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Nombre:</label>
                                        <div class="col-sm-6"><telerik:RadComboBox Filter="Contains" AutoPostBack="true" AllowCustomText="true" ID="CboNombre" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                </div>
                                <div class="ibox-content" runat="server" id="DivEcut" visible="false">
                                    <h4><strong>Buscar Elaborador de Estudios de Capacidad de Uso de la Tierra</strong></h4>
                                     <div><label class="col-sm-2 control-label centradolabel">Código RNF:</label>
                                        <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtRnfEcut" ReadOnly="true" class="form-control"></asp:TextBox></div>
                                    </div>
                                    <div><label class="col-sm-1 control-label centradolabel">Nombre:</label>
                                        <div class="col-sm-6"><telerik:RadComboBox Filter="Contains" AutoPostBack="true" AllowCustomText="true" ID="CboNombreEcut" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <asp:Button runat="server" Text="Regresar"  ID="BtnRegresar" class="btn btn-primary" />
                                    <asp:Button runat="server" Text="Asignar Regente"  ID="BtnAsignaRegente" class="btn btn-primary" />
                                </div>
                                <div class="ibox-content" runat="server" id="DivErr" visible="false" >
                                <div class="col-sm-10">
                                    <div class="alert alert-danger alert-dismissable" runat="server">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblMensaje" Font-Bold="true"></asp:Label>
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
