<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Inmuebles.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Inmuebles" %>
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
                        <h2><strong>Administración de bienes inmuebles</strong></h2>
                    </div>
                    <div class="ibox-content">
                        <div class="col-sm-3"> <a class="btn btn-primary m-b" runat="server" id="BtnNuevo">Nuevo</a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label">Usuario:</label>
                            <div class="col-sm-8"><asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" Enabled="false"></asp:TextBox>
                                <asp:CheckBox runat="server" AutoPostBack="true" Text="Es Representante Legal de Persona Jurídica" ID="ChkOtraJur" Visible="false" />
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="DivJuridico" visible="false">
                        <div class="ibox-content">
                        <div><label class="col-sm-2 control-label centradolabel">Seleccione una personaría jurídica:</label>
                            <div class="col-sm-8"><telerik:RadComboBox ID="CboJuridico" Width="100%" runat="server"></telerik:RadComboBox>
                            </div>
                        </div>
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
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Representaciones y Propietarios</a>
                                                </h5>
                                            </div>
                                            <div id="collapseOne" class="panel-collapse collapse in">
                                                <div class="panel-body">
                                                    <div>
                                                        <label class="col-sm-1 control-label"></label>
                                                        <div class="col-sm-4">
                                                            <asp:CheckBox runat="server" AutoPostBack="true" Text="Es Representante Legal y Propietario"  ID="ChkRepreseanteyPropietario"/>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:CheckBox runat="server" AutoPostBack="true" Text="Es Representante Legal de una  o varias Personas Individuales" ID="ChkRepresentanteVariasPer"/>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivPropietario" visible="false">
                                                        <div>
                                                            <label class="col-sm-1 control-label centradolabel">DPI:</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox runat="server" Text="" ID="TxtDpi" class="form-control" data-mask="9999-99999-9999" placeholder=""></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnValidaPropietario">Validar DPI</a>
                                                            </div>
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivAddPropietario" visible="false">
                                                        <div>
                                                            <div class="col-sm-3" runat="server" visible="false" id="DivNombresProp">
                                                                <asp:TextBox runat="server" Text="" ID="TxtNombrePropietario" class="form-control" placeholder="Nombres"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3" runat="server" visible="false" id="DivApeProp">
                                                                <asp:TextBox runat="server" Text="" ID="TxtApellidoPropietario" class="form-control" placeholder="Apellidos"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2" runat="server" visible="false" id="DivAddProp">
                                                                <a class="btn btn-primary m-b" runat="server" id="BtnAddPropietario">Agregar</a>
                                                            </div>
                                                        </div> 
                                                    </div>
                                                    <div class="ibox-content" runat="server" id="DivAddPropietarioMensaje" visible="false">
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
                                                                <MasterTableView Caption="" DataKeyNames="Existe,PersonaId,Dpi,Nombres,Apellidos" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Existe" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="PersonaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Dpi" HeaderText="Dpi" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                    </div>
                </div>
                
                
                
                
                
                <div style="padding-bottom:2em;"></div>
                <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Direccion:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtDirccion" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                        <div><label class="col-sm-1 control-label centradolabel">Aldea:</label>
                            <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtAldea" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Finca:</label>
                            <div class="col-sm-5">
                                <asp:CheckBox runat="server" Text="Ingresar nombre de finca" AutoPostBack="true" ID="ChkIngNomFinca" />
                                <asp:TextBox runat="server" ID="TxtFinca" Text="SIN NOMBRE" Enabled="false" CssClass="form-control" required=""></asp:TextBox>

                            </div>
                        </div>
                        <div><label class="col-sm-2 control-label centradolabel">Departamento:</label>
                            <div class="col-sm-4"><telerik:RadComboBox ID="CboDepartamento" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:4em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-1 control-label centradolabel">Municipio:</label>
                            <div class="col-sm-5"><telerik:RadComboBox ID="CboMunicipio" Width="100%" runat="server"></telerik:RadComboBox></div>
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
                                <div class="col-sm-5"><asp:TextBox runat="server" ID="TxtFolio" step="0" min="0" type="number"  CssClass="form-control" required=""></asp:TextBox></div>
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
                                <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtNoCerti" CssClass="form-control" required=""></asp:TextBox></div>
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
                        <h4><strong>Para ser más eficiente en el tiempo de respuesta a su solicitud, por favor cargue su archivo digital en formato PDF (no es obligatorio) Nota: si la certificación de propiedad es extensa, se sugiere escanear solamente la primera y ultima hoja.</strong></h4>
                    </div>
                    <div class="ibox-content">
                            <div><label class="col-sm-2 control-label centradolabel">Subir Archivo PDF</label>
                                <div class="col-sm-8">
                                    <telerik:RadAsyncUpload DisableChunkUpload="true" runat="server" ID="RadUploadFile" Localization-Cancel="Cancelar" Localization-Select="Buscar" Localization-Remove="Quitar" MaxFileInputsCount="1" AllowedFileExtensions=".pdf" PostbackTriggers="BtnGrabar" DropZones=".DropZone1" />
                                    <%--<div style="padding-top:1em;padding-bottom:1em;" class="DropZone1">
                                        <p>Arrastre aqui su archivo</p>
                                    </div>--%>
                                    
                                </div>
                            </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
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
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div><label class="col-sm-4 control-label centradolabel">Área de la finca según documento propiedad/posesión: (ha)</label>
                            <div class="col-sm-4"><asp:TextBox runat="server" ID="TxtArea" step="any" type="number" min="0" Width="200px" CssClass="form-control" required=""></asp:TextBox></div>
                        </div>
                    </div>
                    <div style="padding-bottom:2em;"></div>
                    <div class="ibox-content">
                        <div class="col-sm-3"><asp:Button runat="server" Text="Grabar"  ID="BtnGrabar" class="btn btn-primary" />
                            
                        </div>
                        <div class="col-sm-8">
                            <div class="alert alert-danger alert-dismissable" runat="server" id="BtnBadInmueble" visible="false">
                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblBadInmueble" Font-Bold="true">Error</asp:Label>
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="BtnGoodInmueble" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                <asp:Label runat="server" ID="LblGoodInmueble" Font-Bold="true">Error</asp:Label>
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
                            <MasterTableView Caption="" DataKeyNames="InmuebleId,Departamento,Municipio,Direccion,Finca,Area,DocInmueble" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="InmuebleId" Visible="false" HeaderText="Departamnto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Departamento" HeaderText="Departamnto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Municipio" HeaderText="Municipio" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Direccion" HeaderText="Ubicación" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Finca" HeaderText="Finca" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Area" HeaderText="Área" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="" UniqueName="Gestiones" HeaderText="Gestiones realizadas" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DocInmueble" HeaderText="Doc. de Inmueble" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Vista Previa" Visible="true" UniqueName="Vista">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgPreview" CausesValidation="false" formnovalidate ImageUrl="~/Imagenes/24x24/preview.png" ToolTip="Ver Documento" CommandName="CmdPreview"/>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn> 
                                    <telerik:GridTemplateColumn HeaderText="Editar" Visible="true" UniqueName="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="ImgEdit" CausesValidation="false" ImageUrl="~/Imagenes/24x24/pencil.png" formnovalidate ToolTip="Editar" CommandName="CmdEdit"/>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn> 
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" Visible="false" UniqueName="Del">
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
        </div>
        </div>
        <asp:TextBox runat="server" ID="TxtInmuebleId" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
