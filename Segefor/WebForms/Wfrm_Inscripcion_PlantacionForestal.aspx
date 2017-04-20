<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Inscripcion_PlantacionForestal.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Inscripcion_PlantacionForestal" %>
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
                                <h2><strong>FORMULARIO PARA INSCRIPCIÓN  DE PLANTACIONES FORESTALES</strong></h2>
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
                            <h3><strong>I.  TIPO DE PLANTACIÓN</strong></h3>
                        </div>
                        <div class="ibox-content">
                             <div><label class="col-sm-2 control-label centradolabel">Tipo de Plantación:</label>
                                <div class="col-sm-8"><telerik:RadComboBox ID="CboTipoPlantacion" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                <div class="col-sm-1"><asp:CheckBox runat="server"  ID="ChkConIncentivos" AutoPostBack="true" Visible="false" Text="¿Con Incentivos?." /></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content" runat="server" id="DivProcedencia">
                             <div><label class="col-sm-2 control-label centradolabel">Procedencia:</label>
                                <div class="col-sm-8"><telerik:RadComboBox ID="CboProcedencia" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h3><strong>II.  DESCRIPCION DE LAS FINCAS</strong></h3>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Nombre de la finca:</label>
                                <div class="col-sm-8"><telerik:RadComboBox ID="CboFinca" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                                <div class="col-sm-3"><a class="btn btn-primary m-b"  runat="server" id="BtnAddFincaPlan" visible="false">Agregar Finca</a></div>    
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                                                        
                            <div class="col-sm-2"><a class="btn btn-primary m-b" runat="server" id="BtnNuevaFinca">Nueva Finca</a></div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-1 control-label centradolabel">Finca:</label>
                                <div class="col-sm-10">
                                    <asp:CheckBox runat="server" Text="Ingresar nombre de finca" AutoPostBack="true" ID="ChkIngNomFinca" />
                                    <asp:TextBox runat="server" ID="TxtFinca" Text="SIN NOMBRE" Enabled="false" CssClass="form-control" required=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="padding-bottom:4em;"></div>
                        <div class="ibox-content">
                            <h4>Ubicación GTM</h4>
                            <div><label class="col-sm-1 control-label centradolabel">X:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="TxtUbicacionOeste" min="0" type="number" CssClass="form-control" required=""></asp:TextBox>
                                </div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Y:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" ID="TxtUbicacionNorte"  min="0" type="number" CssClass="form-control" required=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label">Tipo de documento de propiedad / posesión:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboTipoDocumento" Width="100%" AutoPostBack="true" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-2 control-label centradolabel">Fecha de emisión:</label>
                                <div class="col-sm-4"><telerik:RadDatePicker ID="TxtFecEmi" Width="100%" runat="server"></telerik:RadDatePicker></div>
                            </div>
                        </div>
                        <div style="padding-bottom:3em;"></div>
                        <div runat="server" id="DivPropiedad" visible="false">
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Número de finca:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtNoFinca" step="0" min="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">Folio:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtFolio" required=""></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div><label class="col-sm-1 control-label centradolabel">Libro:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtLibro" step="0" type="number" min="0" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-1 control-label centradolabel">De:</label>
                                    <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDe" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                        </div>
                        <div runat="server" id="DivMun" visible="false">
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Número certificación:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoCerti" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">Municipalidad que emite:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtMunEmiteDoc" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                        </div>
                        <div runat="server" id="DiVPos" visible="false">
                            <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Número de escritura:</label>
                                    <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoEscritura" CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                                <div><label class="col-sm-2 control-label centradolabel">Nombre del notario:</label>
                                    <div class="col-sm-4">
                                        <telerik:RadComboBox ID="CboTitulo" Width="75px" runat="server"></telerik:RadComboBox>
                                        <asp:TextBox runat="server" ID="TxtNomNotario" Width="200px" CssClass="form-control" required=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                        </div>
                        <div class="ibox-title">
                            <h4><strong>Para ser más eficiente en el tiempo de respuesta a su solicitud, por favor cargue su archivo digital en formato PDF (no es obligatorio) Nota: si la certificación de propiedad es extensa, se sugiere escanear solamente la primera y última hoja.</strong></h4>
                        </div>
                        <div class="ibox-content">
                                <div><label class="col-sm-2 control-label centradolabel">Subir Archivo PDF:</label>
                                    <div class="col-sm-8">
                                        <telerik:RadAsyncUpload runat="server" ID="RadUploadFile" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".pdf" PostbackTriggers="btnGrabarFinca" DropZones=".DropZone1" />
                                    </div>
                                </div>
                        </div>
                        <div style="padding-bottom: 2em;"></div>
                        <div class="ibox-content">
                            <h4>Dirección de la Propiedad</h4>
                            <div><label class="col-sm-1 control-label centradolabel">Dirección:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDirccion" CssClass="form-control" ></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Aldea:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtAldea" CssClass="form-control" required=""></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                                <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamentoFinca" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                                <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipioFinca" Width="100%" runat="server"></telerik:RadComboBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <h4>Colindancias</h4>
                            <div><label class="col-sm-1 control-label centradolabel">Norte:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColNorte" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Sur:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColSur" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div><label class="col-sm-1 control-label centradolabel">Este:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColEste" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                            </div>
                            <div><label class="col-sm-1 control-label centradolabel">Oeste:</label>
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtColOeste" MaxLength="200" TextMode="MultiLine" CssClass="form-control" ></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                        <div class="ibox-content">
                            <div><label class="col-sm-4 control-label centradolabel">Área de la finca según documento propiedad/posesión: (ha)</label>
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtAreaFinca" step="any" type="number" min="0" Width="200px" CssClass="form-control" required=""></asp:TextBox></div>
                            </div>
                        </div>
                        <div style="padding-bottom:2em;"></div>
                            <div class="ibox-content">
                                <div>
                                    <label class="col-sm-1 control-label"></label>
                                        <div class="col-sm-10">
                                            <div class="panel-body">
                                                <div class="panel-group" id="accordion5">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5 class="panel-title">
                                                                <a data-toggle="collapse" data-parent="#accordion5" href="#collapseOne5">Polígono (Click para ingresar)</a>
                                                            </h5>
                                                        </div>
                                                        <div id="collapseOne5" class="panel-collapse collapse out">
                                                            <div class="panel-body">
                                                                <label class="col-sm-2 control-label">Archivo de Poligónos</label>
                                                                <div class="col-sm-5">
                                                                    <telerik:RadAsyncUpload runat="server" ID="UploadPolFinca" Culture="es-GT" MaxFileInputsCount="1"
                                                                            AllowedFileExtensions="xlsx">
                                                                    </telerik:RadAsyncUpload>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <a class="btn btn-primary m-b" runat="server" data-loading-text="Cargando..." id="BtnCargarPolFinca">Cargar</a>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body">
                                                                <telerik:RadGrid runat="server" ID="GrdPoligonoFinca" Skin="Telerik"
                                                                    AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                    GridLines="Both" PageSize="20" >
                                                                    <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                        PrevPageText="Anterior" Position="Bottom" 
                                                                        PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                        PageSizeLabelText="Regitros"/>
                                                                    <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
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
                                                                        <div class="alert  alert-success alert-dismissable" runat="server" id="Div1" visible="false">
                                                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                            <asp:Label runat="server" ID="Label1" Font-Bold="true">Error</asp:Label>
                                                                        </div>
                                                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPoligono" visible="false">
                                                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                            <asp:Label runat="server" ID="LblErrPoligino" Font-Bold="true">Error</asp:Label>
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
                                <div><label class="col-sm-3 control-label centradolabel">El inmueble se encuentra dentro de áreas protegidas:</label>
                                    <div class="col-sm-2"><asp:RadioButtonList runat="server" ID="OptAreasPro" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="No" Selected="true"></asp:ListItem>    
                                                            <asp:ListItem Value="1" Text="Si" Selected="False"></asp:ListItem>
                                                            </asp:RadioButtonList></div>
                                </div>
                                <div runat="server" id="DivArea" visible="false"><label class="col-sm-2 control-label centradolabel">Cual:</label>
                                    <div class="col-sm-4">
                                        <telerik:RadComboBox ID="CboArea" Width="100%" runat="server"></telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-5">
                                    <a class="btn btn-primary m-b" runat="server" data-loading-text="Cargando..." id="btnGrabarFinca">Grabar Finca</a>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="col-sm-8">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrFinca" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblErrFinca" Font-Bold="true">Error</asp:Label>
                                    </div>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodFinca" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblGoodFinca" Font-Bold="true">Error</asp:Label>
                                    </div>
                                </div>
                                    
                            </div>
                            <div style="padding-bottom:2em;"></div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdInmuebles" Skin="MetroTouch" PageSize="20" 
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                            AllowPaging="true" GridLines="Both" >
                                                                        
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="InmuebleId,Departamento,Municipio,Direccion,Finca,Area" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="InmuebleId" Visible="false" HeaderText="Departamnto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Departamento" HeaderText="Departamento" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Municipio" HeaderText="Municipio" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Direccion" HeaderText="Ubicación" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Finca" HeaderText="Finca" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Area" HeaderText="Área" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Propietarios" UniqueName="Prop">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgPropietarios" CausesValidation="false" ImageUrl="~/Imagenes/24x24/person.png" formnovalidate ToolTip="Editar" CommandName="CmdPropietarios"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Áreas Internas" UniqueName="Areas">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgAreas" CausesValidation="false" ImageUrl="~/Imagenes/24x24/ubication.png" formnovalidate ToolTip="Editar" CommandName="CmdAreas"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Editar Finca" Visible="true" UniqueName="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton runat="server" ID="ImgEditFinca" CausesValidation="false" ImageUrl="~/Imagenes/24x24/new.png" formnovalidate ToolTip="Editar" CommandName="CmdEdit"/>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Del">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgDelFinca" ImageUrl="~/Imagenes/24x24/delete.png" formnovalidate ToolTip="Eliminar" CommandName="CmdDel"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                                <div style="padding-bottom:3em;"></div>
                                <div runat="server" id="DivPropietariosFinca" visible="false">
                                <div class="ibox-title">
                                <h3><asp:Label runat="server" ID="TitPropietarios"></asp:Label></h3>
                                </div>
                                                    
                                <div class="ibox-content">
                                    <div><label class="col-sm-2 control-label centradolabel">Tipo de Persona:</label>
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
                                                <a class="btn btn-primary m-b"  data-loading-text="Cargando..." runat="server" id="BtnValidarDpi">Validar DPI</a>
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
                                                            
                                </div>
                                <div class="ibox-content" runat="server" id="DivGrigPropietarios" >
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
                                                    <telerik:GridBoundColumn DataField="Dpi" HeaderText="DPI/Pasaporte" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Fec_Venc_Id" HeaderText="Fecha Vencimiento" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PaisId" HeaderText="Pais" Visible="false" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
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
                            <div style="padding-bottom: 2em;"></div>
                            <div runat="server" id="DivJuridica" visible="false">
                                <div class="ibox-content">
                                    <div><label class="col-sm-2 control-label centradolabel">Nombre de Empresa:</label>
                                        <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNombreEmpresaSocial"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-1 control-label centradolabel">NIT:</label>
                                        <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtNit"  CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-1">
                                            <a class="btn btn-primary m-b" runat="server" id="BtnGrabarNomEmpresa">Grabar</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="padding-bottom:2em;"></div>
                        </div>
                        <div runat="server" id="DivUsosAreas" visible="false">
                            <div class="ibox-content" runat="server" id="DivAreaForestal">
                                <h3>Área de Plantación</h3>
                                <div><label class="col-sm-1 control-label centradolabel">Área (ha):</label>
                                    <div class="col-sm-2"><asp:TextBox runat="server" ID="TxtAreaForestal" step="any" min="0"  type="number"  CssClass="form-control" required=""></asp:TextBox></div>
                                </div>
                            </div>
                            <div class="ibox-content">
                            <div class="col-sm-5">
                                <a class="btn btn-primary m-b" runat="server" id="BtnGrabarAreas">Grabar Áreas</a>
                            </div>
                            </div>
                            <div class="ibox-content" runat="server">
                                <div class="col-sm-10">
                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrAreas" visible="false">
                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblErrAreas" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodAreas" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        <asp:Label runat="server" ID="LblGoodAreas" Font-Bold="true">Error</asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>


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
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Inventario Forestal</a>
                                                        </h5>
                                                    </div>
                                                    <div id="collapseOne" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <div>
                                                                <label class="col-sm-1 control-label">Rodal</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox runat="server" ID="TxtRodal" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <label class="col-sm-1 control-label">Área (ha)</label>
                                                                <div class="col-sm-2">
                                                                     <asp:TextBox runat="server" ID="TxtArea" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox>	
                                                                </div>
                                                                <label class="col-sm-1 control-label">Densidad / ha</label>
                                                                <div class="col-sm-2">
                                                                     <asp:TextBox runat="server" ID="TxtDensidad" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox>	
                                                                </div>
                                                                <label class="col-sm-1 control-label">DAP</label>
                                                                <div class="col-sm-2">
                                                                     <asp:TextBox runat="server" ID="TxtDap" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox>	
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div>
                                                                <label class="col-sm-1 control-label">Altura promedio</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox runat="server" ID="TxtAltura" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox>	
                                                                </div>
                                                                <label class="col-sm-1 control-label">Volumen</label>
                                                                <div class="col-sm-2">
                                                                    <asp:TextBox runat="server" ID="TxtVolumen" step="any" type="number" min="0"  CssClass="form-control"></asp:TextBox>	
                                                                </div>
                                                                <label class="col-sm-1 control-label">Especie</label>
                                                                <div class="col-sm-5">
                                                                    <telerik:RadComboBox ID="CboEspecie"  Width="100%" runat="server"></telerik:RadComboBox>	
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <label class="col-sm-2 control-label">Año Establecimiento</label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox runat="server" ID="TxtAnisEsta" step="any" MaxLength="4" type="number" min="0" CssClass="form-control"></asp:TextBox>	
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="btnAddEspecie">Agregar</a>
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <telerik:RadGrid runat="server" ID="GrdInventario" Skin="Telerik"
                                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                GridLines="Both" >
                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                    PageSizeLabelText="Regitros"/>
                                                                <MasterTableView Name="LabelsInventario" Caption="" DataKeyNames="Rodal,EspecieId,Especie,Area,Densidad,Dap,Altura,Volumen,YearEst" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="EspecieId" Visible="false" UniqueName="Especie" HeaderText="EspecieId" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Especie" UniqueName="Especie" HeaderText="Especie" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Area" UniqueName="Area" HeaderText="Área (ha)" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Densidad" UniqueName="Densidad" HeaderText="Densidad / ha" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Dap" UniqueName="DAP" HeaderText="DAP (cm) / Promedio" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura" HeaderText="Altura (m) / Promedio" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Volumen" UniqueName="Volumen" HeaderText="Volumen (m3)" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="YearEst" UniqueName="YearEst" HeaderText="Año de establecimiento" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Agregar Especie" Visible="true" UniqueName="Del">
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton runat="server" ID="ImgAdd" ImageUrl="~/Imagenes/24x24/Add.png" formnovalidate ToolTip="Agregar especie a este rodal" CommandName="CmdAdd"/>
                                                                                </ItemTemplate>
                                                                        </telerik:GridTemplateColumn> 
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
                                                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrEspecie" visible="false">
                                                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                        <asp:Label runat="server" ID="LblMensajeEspecie" Font-Bold="true">Error</asp:Label>
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
                            <div class="ibox-content">
                            <div>
                                <label class="col-sm-1 control-label"></label>
                                    <div class="col-sm-10">
                                        <div class="panel-body">
                                            <div class="panel-group" id="accordion2">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h5 class="panel-title">
                                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapseOne2">Poligono</a>
                                                        </h5>
                                                    </div>
                                                    <div id="collapseOne2" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <label class="col-sm-2 control-label">Archivo de Poligonos</label>
                                                            <div class="col-sm-5">
                                                                <telerik:RadAsyncUpload runat="server" ID="RadTxtFile" Culture="es-GT" MaxFileInputsCount="1"
                                                                     AllowedFileExtensions="xlsx">
                                                                </telerik:RadAsyncUpload>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargar">Cargar</a>
                                                            </div>
                                                        </div>
                                                        <div class="panel-body">
                                                            <telerik:RadGrid runat="server" ID="GrdPoligono" Skin="Telerik"
                                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                GridLines="Both" PageSize="20" >
                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                    PageSizeLabelText="Regitros"/>
                                                                <MasterTableView Caption="" DataKeyNames="Id,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Id" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
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
                                                                    <div class="alert  alert-success alert-dismissable" runat="server" id="DivOkPoligono" visible="false">
                                                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                        <asp:Label runat="server" ID="LblOkPoligino" Font-Bold="true">Error</asp:Label>
                                                                    </div>
                                                                    <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrPolBosque" visible="false">
                                                                        <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                                        <asp:Label runat="server" ID="LblErrPolBosuqe" Font-Bold="true">Error</asp:Label>
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
                        <div class="col-sm-5">
                            <asp:Button runat="server" Text="Vista Previa"  ID="BtnVistaPrevia" class="btn btn-primary" />
                            <asp:Button runat="server" Text="Enviar Solicitud"  ID="BtnEnviar" class="btn btn-primary" />
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
                            <div>
                                <telerik:RadWindow RenderMode="Lightweight" ID="RadWindowSending" Modal="true" runat="server" ShowContentDuringLoad="false" Width="100px"
                                    Height="100px" Title="Telerik RadWindow" Behaviors="Default">
                                </telerik:RadWindow>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>
                    
                    </Windows>
                </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtRepresentantePersonaId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtOrigenPersonaId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtSubRegionId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtTipoIdentificacion" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtInmuebleId" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtAreaFincaValida" Visible="false"></asp:TextBox>
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

<style type="text/css">
    .bs-example{
    	margin: 20px;
    }
</style>
       
</asp:Content>
