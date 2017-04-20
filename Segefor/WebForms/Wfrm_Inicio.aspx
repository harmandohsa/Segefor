<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Inicio.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-content">
                                <div><h2 style="font-weight:400"><asp:Label runat="server" id="Mensaje" Visible="false" class="col-sm-10 control-label centradolabel"></asp:Label></h2>
                                
                            </div>
                            <div class="ibox-content">
                                <div><h2 style="font-weight:400"><asp:LinkButton ID="LnkLink" runat="server" Text="Mis Gestiones" Visible="false" PostBackUrl="~/WebForms/Wfrm_SeguimientoUsuario.aspx"></asp:LinkButton></h2>
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
                </Windows>
            </telerik:RadWindowManager>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
