<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Inscripcion_Motosierras.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Inscripcion_Motosierras" %>
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
                                <h2><strong>FORMULARIO PARA INSCRIPCIÓN DE COMERCIALIZADORES Y ARRENDADORES DE MOTOSIERRAS / MOTOSIERRAS</strong></h2>
                            </div>
                        </div>
                        <div class="ibox-content" runat="server" visible="false" id="Div_RegionComercializadora">
                            <div><label class="col-sm-2 control-label centradolabel">Región:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtRegion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Subregión:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtSubRegion" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="ibox-content" runat="server" visible="false" id="Div_RegionMoto">
                            <div><label class="col-sm-2 control-label centradolabel">Región:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboRegion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Subregión:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboSubRegion" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <h3><strong>I.  TIPO DE EMPRESA FORESTAL</strong></h3>
                        </div>
                        <div class="ibox-content">
                             <div><label class="col-sm-2 control-label centradolabel">Sub Categoría:</label>
                                <div class="col-sm-8"><telerik:RadComboBox ID="CboSubCategoria" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div runat="server" id="Div_Comercializadora" visible="false">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h3><strong>II.  DATOS DE LA EMPRESA</strong></h3>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Nombre de la empresa:</label>
                                    <div class="col-sm-6"><telerik:RadComboBox ID="CboEmpresa" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                    <div class="col-sm-2"><a class="btn btn-primary m-b" runat="server" id="BtnNuevaEmpresa">Nueva Empresa</a></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content" runat="server" id="DivErrGenEmpresa" >
                                <div class="col-sm-10">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrEmpresa" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblErrEmpresa" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content" runat="server" id="DivNomComercial">
                                <div><label class="col-sm-2 control-label centradolabel">Nombre Comercial:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombreComercial" Enabled="false" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">NIT:</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNIT" Enabled="false" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                Registro Mercantil 
                                <div><label runat="server" class="col-sm-1 control-label centradolabel">No:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtNoMercantil" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Folio:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtFolio" Enabled="false" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Libro:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtLibro" Enabled="false" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">De:</label>
                                    <div class="col-sm-2"><telerik:RadComboBox ID="CboDe"  Width="100%" runat="server" Enabled="false"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Categoría:</label>
                                    <div class="col-sm-2"><telerik:RadComboBox ID="CboCategoria" Width="100%" runat="server" Enabled="false"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Objeto:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtObjeto" CssClass="form-control" Enabled="false"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-3 control-label centradolabel">Turno de 8 hrs al día:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtHorasTurno" Enabled="false" step="any" type="number" min="0" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                
                                <div><label class="col-sm-3 control-label centradolabel">Días trabajados por año:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtDiasYear"  Enabled="false" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-3 control-label centradolabel">Número de personal administrativo:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtEmplFijo" step="any"  Enabled="false" type="number" min="0" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-3 control-label centradolabel">Número de personal Operativo:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtEmplNoFijo" step="any"  Enabled="false" type="number" min="0"  CssClass="form-control"></asp:TextBox></div>
                                </div>
                                
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-3 control-label centradolabel">Stock promedio de motosierras vendidas anualmente:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtStockMotoSierra"  Enabled="false" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                Ubicación de la empresa
                                <div><label class="col-sm-2 control-label centradolabel">Dirección:</label>
                                    <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtDireccion" Enabled="false" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboDepEmpresa" Enabled="false" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboMunEmpresa" AutoPostBack="true" Enabled="false"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Telefono 1:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtTelUnoEmpresa" Enabled="false" CssClass="form-control" data-mask="9999-9999"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Telefono 2:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtTelDosEmpresa" Enabled="false" CssClass="form-control"  data-mask="9999-9999"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Correo:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtCorreoEmpresa"  Enabled="false" CssClass="form-control" type="email" ></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                        <div id="Div_Motosierra" runat="server" visible="false">
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Marca:</label>
                                    <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtMarca" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Modelo:</label>
                                    <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtModelo" CssClass="form-control" required=""  ></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Cilindraje:</label>
                                    <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtCilindraje"  CssClass="form-control" required="" step="any" type="number" min="0" ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Potencia:</label>
                                    <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtPotencia" CssClass="form-control" step="any" required="" type="number" min="0" ></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">No. Serie:</label>
                                    <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtNoSerie" CssClass="form-control" required=""  ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Tipo Documento:</label>
                                    <div class="col-sm-3"><telerik:RadComboBox ID="CboTipoDocumento"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div runat="server" id="Div_DatFactura" visible="false"><label class="col-sm-1 control-label centradolabel">No. Factura:</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtNoFactura" CssClass="form-control" required=""  ></asp:TextBox></div>
                                </div>
                                <div runat="server" id="Div_DatEmpresa" visible="false"><label class="col-sm-1 control-label centradolabel">De que Empresa:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtEmpresa" CssClass="form-control" required=""  ></asp:TextBox></div>
                                </div>
                                <div runat="server" id="Div_DatEspecifique" visible="false"><label class="col-sm-1 control-label centradolabel">Especifique:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtEspecifique" CssClass="form-control" required=""  ></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Tipo de Propietario:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoPersona"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom: 2em;"></div>
                        <div runat="server" id="DivPropietarios" visible="false">
                            <div class="ibox-content" runat="server">
                                <div><label class="col-sm-2 control-label centradolabel">Tipo de Identificación:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoIdPropietario"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div runat="server" id="DivPropietarioNacional" visible="false">
                                <div class="ibox-content">
                                    <div>Propietarios:</div>   
                                    <label class="col-sm-1 control-label centradolabel">DPI:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" Text="" ID="TxtDpi" class="form-control" data-mask="9999-99999-9999" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <a class="btn btn-primary m-b" runat="server" id="BtnValidarDpi">Validar DPI</a>
                                    </div>
                                                           
                                </div>
                            </div>
                            <div runat="server" id="DivPropietarioInter" visible="false">
                                <div class="ibox-content">
                                    <div>Propietarios:</div>   
                                    <label class="col-sm-1 control-label centradolabel">Pasaporte:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox runat="server" Text="" ID="TxtPasaportePropietario" class="form-control"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 control-label centradolabel">País:</label>
                                    <div class="col-sm-3">
                                        <telerik:RadComboBox ID="CboPais"  Width="100%" runat="server"></telerik:RadComboBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <a class="btn btn-primary m-b" runat="server" id="BtnValidarPasaporte">Validar Pasporte</a>
                                    </div>
                                                           
                                </div>
                            </div>
                            <div class="ibox-content" runat="server" id="DivAddPropietario" >
                                <div>
                                    <div class="col-sm-3" runat="server" visible="false" id="DivNombresProp">
                                        <div>Nombres</div>
                                        <asp:TextBox runat="server" Text="" ID="TxtNombrePropietario" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" runat="server" visible="false" id="DivApeProp">
                                        <div>Apellidos</div>
                                        <asp:TextBox runat="server" Text="" ID="TxtApellidoPropietario" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3" runat="server" visible="false" id="DivFecVencimiento">
                                        <div>Fecha de Vencimiento</div>
                                        <telerik:RadDatePicker ID="TxtFecVenc" Width="100%" runat="server"></telerik:RadDatePicker>
                                    </div>
                                    <div class="col-sm-2" runat="server" visible="false" id="DivAddProp">
                                        <a class="btn btn-primary m-b" runat="server" id="BtnAddPropietario">Agregar</a>
                                    </div>
                                </div> 
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content" runat="server" id="DivAddPropietarioMensaje" >
                                <div class="col-sm-10">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivBadPropietario" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblMansajeBadPropietario" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodPropietario" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblMansajeGoodPropietario" Font-Bold="true">Error</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content" runat="server" id="DivGrigPropietarios"  visible="false">
                                <div class="col-sm-10">
                                    <telerik:RadGrid runat="server" ID="GrdPropietarios" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Existe,PersonaId,Dpi,Nombres,Apellidos,Fec_Venc_Id,PaisId" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Existe" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PersonaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Dpi" HeaderText="Dpi" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fec_Venc_Id" HeaderText="Fecha Vencimiento" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PaisId" HeaderText="Pais" Visible="true" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgDel" ImageUrl="~/Imagenes/24x24/trashcan.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
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
                        <div style="padding-bottom: 2em;"></div>
                        <div runat="server" id="DivJuridica" visible="false">
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Nombre de Empresa:</label>
                                    <div class="col-sm-7"><asp:TextBox runat="server" ID="TxtNombreEmpresaSocial"  CssClass="form-control"></asp:TextBox></div>
                                </div>
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
                        <div style="padding-bottom:2em;"></div>
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
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipio"   Width="100%" runat="server"></telerik:RadComboBox></div>
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
                        <div class="ibox float-e-margins" runat="server" visible="false" id="DivMovibles">
                            <div class="ibox-content">
                                Dirección de funcionamiento
                                <div><label class="col-sm-2 control-label centradolabel">Dirección:</label>
                                    <div class="col-sm-6"><asp:TextBox runat="server" ID="TxtDireccionFuncionamiento" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                    <div class="col-sm-4"><telerik:RadComboBox ID="CboDepFuncionamiento" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                    <div class="col-sm-5"><telerik:RadComboBox ID="CboMunFuncionamiento"  Width="100%" runat="server"></telerik:RadComboBox></div>
                                </div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins" runat="server" visible="false" id="DivExportadorImportador">
                            <div class="ibox-content">
                                Exportadores e Importadores de Productos Forestales
                                <div><label class="col-sm-2 control-label centradolabel">Fabrica Productos Forestales:</label>
                                    <div class="col-sm-4">
                                        <asp:RadioButtonList runat="server" ID="OptFabricaProductos" CssClass="radio radio-inline" AutoPostBack="true">
                                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div runat="server" visible="false" id="DivEsFabrica">
                                    <label class="col-sm-2 control-label centradolabel">Codigo de Industria Forestal (RNF):</label>
                                    <div class="col-sm-3"><asp:TextBox runat="server" ID="TxtRnf" CssClass="form-control"></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div>
                                    <label class="col-sm-1 control-label"></label>
                                        <div class="col-sm-10">
                                            <div class="panel-body">
                                                <div class="panel-group" id="accordion">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Productos de Exportación</a>
                                                            </h5>
                                                        </div>
                                                        <div id="collapseOne" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <div>
                                                                    <label class="col-sm-1 control-label">Producto</label>
                                                                    <div class="col-sm-5">
                                                                        <telerik:RadComboBox ID="CboProducto"  Width="100%" runat="server"></telerik:RadComboBox>	
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <a class="btn btn-primary m-b" runat="server" id="btnAddProducto">Agregar</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body">
                                                                <telerik:RadGrid runat="server" ID="GrdProductosExportacion" Skin="Telerik"
                                                                    AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                    GridLines="Both" >
                                                                    <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                        PrevPageText="Anterior" Position="Bottom" 
                                                                        PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                        PageSizeLabelText="Regitros"/>
                                                                    <MasterTableView Name="LabelsInventario" Caption="" DataKeyNames="Codigo_Producto,Nombre_Producto,CodigoFSC" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Codigo_Producto" Visible="false" UniqueName="Codigo_Producto" HeaderText="Codigo_Producto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Nombre_Producto" UniqueName="Nombre_Producto" HeaderText="Producto" HeaderStyle-Width="350px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CodigoFSC" UniqueName="CodigoFSC" HeaderText="Codigo FSC" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton runat="server" ID="ImgDel" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                                    </ItemTemplate>
                                                                            </telerik:GridTemplateColumn> 

                                                                        </Columns>        
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableTheming="true">
                                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="ibox-content">
                                                                    <div class="col-sm-9">
                                                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrProducto" visible="false">
                                                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                            <asp:Label runat="server" ID="LblMensajeProducto" Font-Bold="true">Error</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding-bottom:2em;"></div>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div>
                                    <label class="col-sm-1 control-label"></label>
                                        <div class="col-sm-10">
                                            <div class="panel-body">
                                                <div class="panel-group" id="accordion1">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion1" href="#collapse">Productos de Importación</a>
                                                            </h5>
                                                        </div>
                                                        <div id="collapse" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <div>
                                                                    <label class="col-sm-1 control-label">Producto</label>
                                                                    <div class="col-sm-5">
                                                                        <telerik:RadComboBox ID="CboProductoImportacion"  Width="100%" runat="server"></telerik:RadComboBox>	
                                                                    </div>
                                                                    <div class="col-sm-2">
                                                                        <a class="btn btn-primary m-b" runat="server" id="BtnAddProductoImporatcion">Agregar</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body">
                                                                <telerik:RadGrid runat="server" ID="GrdProductoImportacion" Skin="Telerik"
                                                                    AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                    GridLines="Both" >
                                                                    <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                        PrevPageText="Anterior" Position="Bottom" 
                                                                        PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                        PageSizeLabelText="Regitros"/>
                                                                    <MasterTableView Name="LabelsInventario" Caption="" DataKeyNames="Codigo_Producto,Nombre_Producto,CodigoFSC" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Codigo_Producto" Visible="false" UniqueName="Codigo_Producto" HeaderText="Codigo_Producto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Nombre_Producto" UniqueName="Nombre_Producto" HeaderText="Producto" HeaderStyle-Width="350px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CodigoFSC" UniqueName="CodigoFSC" HeaderText="Codigo FSC" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="true" UniqueName="Del">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton runat="server" ID="ImgDel" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                                                    </ItemTemplate>
                                                                            </telerik:GridTemplateColumn> 

                                                                        </Columns>        
                                                                    </MasterTableView>
                                                                    <FilterMenu EnableTheming="true">
                                                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                                                    </FilterMenu>
                                                                </telerik:RadGrid>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="ibox-content">
                                                                    <div class="col-sm-9">
                                                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrProductoImportacion" visible="false">
                                                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                            <asp:Label runat="server" ID="LblMensajeImportacion" Font-Bold="true">Error</asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div style="padding-bottom:2em;"></div>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content" runat="server" id="DivActividadPrincipal" visible="false">
                            <div><label class="col-sm-2 control-label centradolabel">Actividad Principal:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboActividadPrincipal" Width="100%" runat="server"></telerik:RadComboBox></div>
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
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                        <div class="col-sm-5">
                            <asp:Button runat="server" Text="Vista Previa"  ID="BtnVistaPrevia" class="btn btn-primary" />
                            <asp:Button runat="server" Text="Enviar Solicitud"  ID="BtnEnviar"  class="btn btn-primary" />
                        </div>
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
                                    <asp:Button runat="server" Text="Enviar"  ID="BtnYes" data-loading-text="Enviando..." class="btn btn-primary" />
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
            <asp:TextBox runat="server" ID="TxtEmpresaId" Visible="false"></asp:TextBox>
            
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
