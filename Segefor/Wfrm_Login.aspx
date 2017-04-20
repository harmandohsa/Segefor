<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wfrm_Login.aspx.cs" Inherits="SEGEFOR.Wfrm_Login" %>
<html class="htmllogin">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

    <title>Sistema Eletrónico de Gestión Forestal -INAB-</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    

</head>
<body class="htmllogin" >
        <div runat="server" visible="false" style="position: absolute; z-index: 1000; top: 0; left: 0; width:50px ; height: 50px;">
    
    <img src="Imagenes/LOGO.PNG" width="150px"  style="display: block;" />
</div>
    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>
                            
                <h1 class="logo-name" runat="server"></h1>

            </div>
            <h3 style="color:white;font-weight:900">Sistema Eletrónico de Gestión Forestal -INAB-</h3>
            <form class="m-t" role="form" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                   <asp:UpdateProgress ID="upUpdateProgressFlotante" runat="server" DisplayAfter="3" DynamicLayout="false">
                <ProgressTemplate>
                    <div class="Transparencia"></div>
                    <div class="UpdateProgressFlotante">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Image ID="imgUpdateProgressFlotante"  ImageUrl="~/Imagenes/loading_page.gif"  runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress> 
                <asp:UpdatePanel runat="server">
                <ContentTemplate>
                
                <div class="form-group"> 
                    
                    <asp:TextBox runat="server" ID="TxtUsuario" placeholder="Usuario" Width="300px" required="" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" ID="TxtClave" TextMode="Password" placeholder="Clave" Width="300px" required="" class="form-control"></asp:TextBox>
                </div>
                <asp:Button runat="server" ID="BtnIngresar" CssClass="btn btn-primary m-b"  Text="Ingresar" />
                <a class="btn btn-primary m-b" href="WebForms/Wfrm_CrearUsuario.aspx">Crear   cuenta</a>
                
                <%--<button type="submit" class="btn btn-primary m-b">Login</button>--%>
                
                
                <div class="form-group">
                    <a class="btn btn-primary m-b" runat="server" id="BtnOlvio">¿Olvido Su Clave?</a>
                </div>
                <div class="form-group" runat="server" id="DivOlvidoClave" visible="false">
                    <asp:TextBox runat="server" ID="TxtUsuarioOlv" placeholder="Usuario" Width="300px" required="" class="form-control"></asp:TextBox>
                    <a class="btn btn-primary m-b"  runat="server" id="BtnEnviaClave">Enviar Clave</a>
                </div>
                <%--<p class="text-muted text-center"><small>Do not have an account?</small></p>
                <a class="btn btn-sm btn-white btn-block" href="register.html">Create an account</a>--%>
                <div class="alert alert-danger alert-dismissable" runat="server" id="BtnEror" visible="false">
                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                </div>
                
                </ContentTemplate>
                </asp:UpdatePanel>
                
            </form>
            <p class="m-t" style="color:white;font-weight:500"><small></small> </p>
        </div>
    </div>
    
 
    <!-- Mainly scripts -->
    
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>


</body>
</html>