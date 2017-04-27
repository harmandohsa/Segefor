<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_AdminPerfil.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_AdminPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">

        <ContentTemplate>
             <script type="text/javascript">
                 Telerik.Web.UI.RadAsyncUpload.prototype.getUploadedFiles = function () {
                     var files = [];
                     // debugger;
                     $telerik.$(".ruUploadSuccess", this.get_element()).each(function (index, value) {
                         files[index] = $telerik.$(value).text();
                     });

                     return files;
                 }

                 function validateUpload(sender, args) {
                     var upload = $find("<%=UploadFoto.ClientID %>");
                    // debugger;

                     args.IsValid = upload.getUploadedFiles().length != 0;
                 }

                 function quito() {
                     var CustomValidator = document.getElementById("<%=CustomValidator.ClientID %>");
                     CustomValidator.textContent = "";
                 }
</script>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h2><strong>Administración del Perfil (Datos Generales)</strong></h2>
                    </div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Nombres:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">Apellidos:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtApellido" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label runat="server" id="lblTipoId" class="col-sm-1 control-label centradolabel">DPI:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDpi" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                        </div>
                        <div><label runat="server" id="LblFecVen" class="col-sm-3 control-label centradolabel">Fecha de Vencimiento:</label>
                            <div class="col-sm-3">
                                <telerik:RadDatePicker Enabled="false" ID="TxtFecVenId" Width="100%" runat="server"></telerik:RadDatePicker>
                            </div>
                            
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label">Fecha de Nacimiento:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtFecNac" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">Género:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtGenero" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Usuario:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtUsuario" Enabled="false" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label">Correo:</label>
                            <div class="col-sm-5"><asp:TextBox Enabled="false" runat="server" ID="TxtCorreo" type="mail" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Foto de perfil:</label>
                            <div class="col-sm-4"><telerik:RadAsyncUpload MaxFileSize="524288" DisableChunkUpload="true"  runat="server" ID="UploadFoto" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".jpg" PostbackTriggers="BtnModFoto" DropZones=".DropZone1" />
                                <asp:CustomValidator runat="server" ID="CustomValidator" Font-Bold="true" ForeColor="Red" ClientValidationFunction="validateUpload" ErrorMessage="Archivo mayor a 512Kb o extensión diferente a .jpg">
                                </asp:CustomValidator>
                            </div>
                            <div class="col-sm-1">
                                <asp:Button runat="server" Text="Modificar Foto"   ID="BtnModFoto" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="col-sm-3"><asp:Button runat="server" Text="Modificar Datos Personales" CausesValidation="false"  ID="BtnModDatos" class="btn btn-primary" />
                            
                        </div>
                        <div class="col-sm-8">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="Btnbaddatgen" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="Btnsuccesdatgen" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblMensajeGodDatGen" Font-Bold="true">Error</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="ibox-content">
                    <div class="ibox-title">
                        <h2><strong>Datos Específicos</strong></h2>
                    </div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Grado Académico:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboGrado" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">NIT:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNit"  CssClass="form-control"></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Etnia:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboEtnia" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">Grupo Lingüístico:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboGrupo" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Ocupación:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboOcupacion" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div class="col-sm-3"><asp:Button runat="server" Text="Modificar Datos Específicos" CausesValidation="false"  ID="BtnModDatosEspe" class="btn btn-primary" />
                            
                        </div>
                        <div class="col-sm-8">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnBadDatEspe" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblBadDatosEsp" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="BtnGoodDatosEspe" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblGoodDatosEspe" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                    <div runat="server" id="DivRnf">
                        <div class="ibox-title">
                        <h2><strong>RNF</strong></h2>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                       <telerik:RadGrid runat="server" ID="GrdRegistros"  Skin="MetroTouch" PageSize="20" 
                            AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                AllowPaging="true" GridLines="Both" >
                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                PrevPageText="Anterior" Position="Bottom" 
                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                PageSizeLabelText="Regitros"/>
                            <MasterTableView Caption="" DataKeyNames="Reg,Ven,Estado,Actividad,NoReg,RegistroId,Fec_Rel" NoMasterRecordsText="NO APARECE REGISTRO RELACIONADO A SUS DATOS" ShowFooter="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="RegistroId" HeaderText="RegistroId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Reg" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ven" HeaderText="Fecha de vencimiento" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Estado" HeaderText="Estado" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Actividad" HeaderText="Actividad" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NoReg" HeaderText="Código  RNF" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Agregar" Visible="true" UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton runat="server" ID="ImgAgregar" CausesValidation="false" formnovalidate ImageUrl="~/Imagenes/24x24/Add.png" ToolTip="Agregar" CommandName="CmdAdd"/>
                                            </ItemTemplate>
                                    </telerik:GridTemplateColumn> 
                                    <telerik:GridBoundColumn DataField="Fec_Rel" UniqueName="FechaAdd" HeaderText="Fecha Agregado" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                </Columns>        
                            </MasterTableView>
                            <FilterMenu EnableTheming="true">
                                <CollapseAnimation Duration="200" Type="OutQuint" />
                            </FilterMenu>
                        </telerik:RadGrid>
                    </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div class="col-sm-8">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnGoodRegistro" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblGoodRegistro" Font-Bold="true">Error</asp:Label>
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="BtnBadRegistro" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblBadRegistro" Font-Bold="true">Error</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content" runat="server" visible="false">
                    <div class="ibox-title">
                        <h2><strong>Personerías Jurídicas</strong></h2>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                       <telerik:RadGrid runat="server" ID="GrdJuridico" Skin="MetroTouch"  PageSize="20" 
                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                            AllowPaging="true" GridLines="Both" >
                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                            PrevPageText="Anterior" Position="Bottom" 
                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                            PageSizeLabelText="Regitros"/>
                        <MasterTableView Caption="" DataKeyNames="PersonaJuridicaId,FECHA,NOMBRE,Tipo_Juridico,Numero,Libro,Folio,Nit" NoMasterRecordsText="No hay registros" ShowFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn DataField="PersonaJuridicaId" Visible="false" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FECHA" HeaderText="Fecha de registro" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOMBRE" HeaderText="Nombre" HeaderStyle-Width="325px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tipo_Juridico" HeaderText="Tipo" HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Numero" HeaderText="Número" HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Libro" HeaderText="Libro" HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Folio" HeaderText="Folio" HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nit" HeaderText="Nit" HeaderStyle-Width="225px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Editar" Visible="true" UniqueName="Edit">
                                        <ItemTemplate> 
                                            <asp:ImageButton runat="server" ID="ImgEdit" ImageUrl="~/Imagenes/24x24/pencil.png" CausesValidation="false" formnovalidate ToolTip="Editar" CommandName="CmdEdit"/>
                                        </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                                <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgDel" ImageUrl="~/Imagenes/24x24/trashcan.png" CausesValidation="false" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                        </ItemTemplate>
                                </telerik:GridTemplateColumn> 
                            </Columns>        
                        </MasterTableView>
                        <FilterMenu EnableTheming="true">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                        </telerik:RadGrid>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div class="col-sm-3"><asp:Button runat="server"  CausesValidation="false" Text="Nuevo Registro"  ID="BtnNuevoJuridico" class="btn btn-primary" />
                            
                        </div>
                    </div>
                    <div runat="server" id="DivJuridico" visible="false">
                        <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Tipo:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboTipo" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                        <div><label runat="server" id="LblTipo" class="col-sm-1 control-label centradolabel">Nombre:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNombreJur" CssClass="form-control"></asp:TextBox></div>
                        </div>
                        </div>
                        <div style="padding-bottom:3em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-3 control-label">Fecha de Representación Legal:</label>
                                <div class="col-sm-3"><telerik:RadDatePicker ID="TxtFecRepre" Width="100%" runat="server"></telerik:RadDatePicker></div>
                            </div>
                            <div runat="server" id="DivActa" visible="false"><label class="col-sm-2 control-label">Número de acta de poseción:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtActa" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:3em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-3 control-label">Número:</label>
                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtNumero" min="0" step="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-2 control-label">Folio:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtFolio" min="0" step="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-3 control-label">Libro:</label>
                                <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtLibro" min="0" step="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-2 control-label">Nit:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNitJur" CssClass="form-control" required=""></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:3em;"></div>
                        <div class="ibox-content">
                        <div class="col-sm-3"><asp:Button runat="server"  CausesValidation="false" Text="Grabar Jurídico"  ID="BtnGrabarJur" class="btn btn-primary" />
                            
                        </div>
                        
                        </div>
                    </div>
                    <div style="padding-bottom:3em;"></div>
                    <div class="ibox-content">
                        <div class="col-sm-8">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnBadJuridico" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblBadJuridico" Font-Bold="true">Error</asp:Label>
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="BtnGoodJuridico" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblGoodJuridico" Font-Bold="true">Error</asp:Label>
                            </div>
                        </div>
                    </div>
                
            </div>
        </div>
        </div>
        <asp:TextBox runat="server" ID="TxtUsuarioHide" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TxtCorreoHide" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TxtDpiCompleto" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TxtPersonaJuridicaId" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TxtEstadoJur" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="TxtNumeroHide" Visible="false"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnModFoto" />
        </Triggers>
    </asp:UpdatePanel>

   
</asp:Content>
