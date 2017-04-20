<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_SeleccionaPersona.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_SeleccionaPersona" %>
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
                                    <h2><strong>Seleccione Persona</strong></h2>
                                </div>
                                <div class="ibox-content">
                                     <div class="ibox-content">
                                        <h4><strong>Buscar Persona</strong></h4>
                                         <div><label class="col-sm-1 control-label centradolabel">DPI:</label>
                                            <div class="col-sm-3"><asp:TextBox runat="server" Text="" ID="TxtDpi" class="form-control" data-mask="9999-99999-9999" ></asp:TextBox></div>
                                        </div>
                                        <div>
                                            <div class="col-sm-1"><input type="button" runat="server" id="BtnBuscar" title="Buscar" class="btn btn-primary" value="Buscar" /> </div>
                                        </div>
                                        <div><label class="col-sm-1 control-label centradolabel">Persona:</label>
                                            <div class="col-sm-6"><telerik:RadComboBox Filter="Contains" AllowCustomText="true" ID="CboPersona" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                        </div>
                                    </div>
                                </div>
                                <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <div class="col-sm-9">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="BtnErrorDpi" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMensajeErrorDpi" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div class="col-sm-1" ><asp:Button runat="server" ID="BtnGrabar" class="btn btn-primary" Text="Ingresar Solicitud" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
