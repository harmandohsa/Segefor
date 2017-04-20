<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Inscripcion_Entidad.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Inscripcion_Entidad" %>
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
                                <h2><strong>INSCRIPCIÓN  DE ENTIDADES RELACIONADAS CON INVESTIGACIÓN, EXTENSIÓN Y CAPACITACIÓN FORESTAL Y AGROFORESTAL</strong></h2>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Región:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtRegion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Subregión:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtSubRegion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                             <div><label class="col-sm-2 control-label centradolabel">Tipo de Inscripción:</label>
                                <div class="col-sm-8"><telerik:RadComboBox ID="CboTipoInscripcion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <h3><strong>I.  DATOS DE LA ENTIDAD</strong></h3>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label runat="server" id="LblTituloEmpresa"  class="col-sm-2 control-label centradolabel">Nombre:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNomEmpresa"  required="" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">NIT:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNit"  required=""  CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label runat="server" class="col-sm-2 control-label centradolabel">Objeto:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtObjeto"  required="" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Dirección:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtDireccion"  required=""  CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamentoEntidad" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipioEntidad" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label runat="server" class="col-sm-2 control-label centradolabel">Teléfono Uno:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTelUno" data-mask="9999-9999"  required="" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Teléfono Dos:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTelefonoDos" data-mask="9999-9999"  CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label runat="server" id="Label1" class="col-sm-2 control-label centradolabel">Correo:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtCorreo"  required="" type="email" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div>
                        <div class="ibox-content">
                            <h2><strong>Representantes Legales</strong></h2>
                        </div>
                        <div runat="server" id="DivPropietariosRep">
                            <div class="ibox-content" runat="server">
                                <div><label class="col-sm-2 control-label centradolabel">Tipo de Identificación:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoIdentificacionRep"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div runat="server" id="DivPropietarioNacionalRepre" visible="false">
                                <div class="ibox-content">
                                    
                                    <label class="col-sm-1 control-label centradolabel">DPI:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" Text="" ID="TxtDpiRep" class="form-control" data-mask="9999-99999-9999" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <a class="btn btn-primary m-b" runat="server" id="BtnValidarDpiRep">Validar DPI</a>
                                    </div>
                                                           
                                </div>
                            </div>
                            <div runat="server" id="DivPropietarioInterRep" visible="false">
                                <div class="ibox-content">
                                    
                                    <label class="col-sm-1 control-label centradolabel">Pasaporte:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" Text="" ID="TxtPasaporteRep" class="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 control-label centradolabel">País:</label>
                                    <div class="col-sm-3">
                                        <telerik:RadComboBox ID="CboPaisRep"  Width="100%" runat="server"></telerik:RadComboBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <a class="btn btn-primary m-b" runat="server" id="BtnValidarPasaporteRep">Validar Pasporte</a>
                                    </div>
                                                           
                                </div>
                            </div>
                            <div class="ibox-content" runat="server" id="DivAddPropietarioRep" >
                                <div>
                                    <div class="col-sm-3" runat="server" visible="false" id="DivNombresPropRep">
                                        <div>Nombres</div>
                                        <asp:TextBox runat="server" Text="" ID="TxtNombresRep" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" runat="server" visible="false" id="DivApePropRep">
                                        <div>Apellidos</div>
                                        <asp:TextBox runat="server" Text="" ID="TxtApellidosRep" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" runat="server" visible="false" id="DivFecVencimientoRep">
                                        <div>Fecha de Vencimiento</div>
                                        <telerik:RadDatePicker ID="TxtFecVenceIdRep" Width="100%" runat="server"></telerik:RadDatePicker>
                                    </div>
                                    <div class="col-sm-2" runat="server" visible="false" id="DivAddPropRep">
                                        <a class="btn btn-primary m-b" runat="server" id="BtnAddRepresentante">Agregar</a>
                                    </div>
                                </div> 
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content" runat="server" id="DivAddRepresentanteMensaje" >
                                <div class="col-sm-10">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivBadRepresentante" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblMansajeBadRepresentante" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodRepresentante" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblMansajeGoodRepresentante" Font-Bold="true">Error</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content" runat="server" id="DivGrigRepresentantes"  visible="true">
                                <div class="col-sm-10">
                                    <telerik:RadGrid runat="server" ID="GrdRepresentantes" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="ExisteRep,PersonaIdRep,DpiRep,NombresRep,ApellidosRep,Fec_Venc_IdRep,PaisIdRep" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ExisteRep" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PersonaIdRep" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DpiRep" HeaderText="Dpi" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NombresRep" HeaderText="Nombres" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ApellidosRep" HeaderText="Apellidos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fec_Venc_IdRep" HeaderText="Fecha Vencimiento" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PaisIdRep" HeaderText="Pais" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgDelRep" ImageUrl="~/Imagenes/24x24/trashcan.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                        </div>

                        
                        <div runat="server" id="DivDatosInstiuciones" visible="false">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h3><strong>II.  DATOS PARA INSTITUCIONES</strong></h3>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Tipo:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoIns" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Cobertura:</label>
                                    <div class="col-sm-5"><telerik:RadComboBox ID="CboCoberturaIns"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Propiedad:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboPropiedadINs" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Actividades Principales:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtActividadesPrincipalesIns" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">No. Familias atendidas</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoFamIns" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="DivDatosOrga" visible="false">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h3><strong>II.  DATOS PARA ORGANIZACIONES</strong></h3>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Tipo:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoOrg" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Cobertura:</label>
                                    <div class="col-sm-5"><telerik:RadComboBox ID="CboCoberturaOrg"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Propiedad:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboPropiedadOrg" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">Tamaño:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboTamOrg" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Producción:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboProdOrg" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                 <div><label class="col-sm-1 control-label centradolabel">Actividades Principales:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtActividadesPrincipalesOrg" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                               
                                <div><label class="col-sm-2 control-label centradolabel">No. Familias atendidas</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoFamOrg" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="DivDatosAsociacion" visible="false">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h3><strong>II.  DATOS PARA ASOCIACIONES</strong></h3>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Grupo Étnico:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboGrupoEtnico" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Finalidad:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtFinalidad" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Actividades Principales:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtActividadesPrincialesAso" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">No de Integrantes:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoIntegrantes" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">No. Familias atendidas</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoFamAso" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">Total de bosque natural (ha)</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTotalBosque" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Total de reforestación (ha)</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTotalRefo" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="DivDatosMun" visible="false">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h3><strong>II.  DATOS PARA MUNICIPALIDADES</strong></h3>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Nombre de la oficina:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNomOficina" MaxLength="200" CssClass="form-control" required="" ></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Año de creación:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtAnisCrea" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Correo de oficina:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtCorreoOficina" MaxLength="200" CssClass="form-control" required="" type="email" ></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Telefono:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtTelefonoMun" required="" CssClass="form-control" data-mask="9999-9999" ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                Nombre del Encargado
                                <div><label class="col-sm-2 control-label centradolabel">Nombres</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombresMun" MaxLength="200" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">Apellidos</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtApellidosMun" MaxLength="200" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Correo del encargado:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtCorreoEncargado" MaxLength="200" CssClass="form-control" required="" type="email" ></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Celular:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtTelEncargado" CssClass="form-control" required="" data-mask="9999-9999" ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">No. Familias atendidas</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoFamMun" step="any" type="number" min="0" required="" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>III.  DATOS DE NOTIFICACIÓN</strong></h3>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel" runat="server" id="LblDirecNotifica">Dirección de notificación:</label>
                                <div class="col-sm-10"><asp:TextBox runat="server" ID="TxtDireccionNotifica" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamento" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipio"  Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Teléfono:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtTelefonoNotifica" CssClass="form-control" data-mask="9999-9999" ></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Celular:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtCelularNotifica"  required=""  data-mask="9999-9999" CssClass="form-control"></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Correo electrónico:</label>
                                <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtCorreoNotifica"  required="" CssClass="form-control" type="email" ></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>IV.  OBSERVACIONES GENERALES</strong></h3>
                            </div>
                            <div class="col-sm-12"><asp:TextBox runat="server" ID="TxtObservaciones" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>V.  NOMBRE Y FIRMA</strong></h3>
                            </div>
                            <div class="col-sm-12"><asp:TextBox runat="server" ID="TxtNomFirma" MaxLength="200" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></div>
                        </div>
                        <div class="ibox-content">
                        <div class="col-sm-5">
                            <asp:Button runat="server" Text="Vista Previa"  ID="BtnVistaPrevia" class="btn btn-primary" />
                            <asp:Button runat="server" Text="Enviar Solicitud"  ID="BtnEnviar" class="btn btn-primary" />
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                        <div class="col-sm-9">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnEror" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        </div>

                    </div>
                </div>
               
                <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                    <Windows>
                        <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                            Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                        </telerik:RadWindow>
                        <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="220px" Width="350px" Title="Confirmación" Behaviors="Close">
                        <ContentTemplate>
                            <asp:Label ID="LblTitConfirmacion" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            <div class="ibox-content">
                                <div class="col-sm-3">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ask.png" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button runat="server" Text="Enviar" data-loading-text="Enviando..."  ID="BtnYes" class="btn btn-primary" />
                                </div>
                                <%--<div class="col-sm-2">
                                    <asp:Button runat="server" Text="No"  ID="BtnNo" class="btn btn-primary" />
                                </div>--%>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtRepresentantePersonaId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtOrigenPersonaId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtSubRegionId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtTipoIdentificacion" Visible="false"></asp:TextBox>
            
            
            </div>
                
        </ContentTemplate>
    </asp:UpdatePanel>
     <script>
         function pageLoad() {
             $('.i-checks').iCheck({
                 checkboxClass: 'icheckbox_square-green',
                 radioClass: 'iradio_square-green',
             });

             $('#<%=BtnYes.ClientID%>').click(function () {
                 $(this).button('loading').delay(100000).queue(function () {
                     $(this).button('reset');
                     $(this).dequeue();
                     $(this).data('loading-text', 'Cargando...');
                 });
             });

         }
        </script>
</asp:Content>
