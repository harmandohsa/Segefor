<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_BosqueNatural.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_BosqueNatural" %>
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
                                    <h2><strong>Profesion por actividad profesional</strong></h2>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadComboBox ID="CboMapa" AutoPostBack="true" Width="100%" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione un mapa" Value="0" />
                                            <telerik:RadComboBoxItem Text="Bosque Natural" Value="1" />
                                            <telerik:RadComboBoxItem Text="Tierras de Vocación Forestal" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
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
