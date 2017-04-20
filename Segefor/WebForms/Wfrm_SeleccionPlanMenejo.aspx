<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_SeleccionPlanMenejo.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_SeleccionPlanMenejo" %>
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
                                    <h2><strong>Seleccionar Plan de Manejo Forestal</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <div><label class="col-sm-2 control-label centradolabel">Seleccione el tipo de Plan de Manejo:</label>
                                        <div class="col-sm-8"><telerik:RadComboBox ID="CboTipoPlanManejo" Width="100%" runat="server"></telerik:RadComboBox></div>
                                        <div class="col-sm-2"><a class="btn btn-primary m-b" runat="server" id="BtnGrabar">Iniciar Llenado</a></div>
                                    </div>
                                </div>
                                <div class="ibox-content" runat="server">
                                    <div class="col-sm-10">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErr" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblMansajeErr" Font-Bold="true"></asp:Label>
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
