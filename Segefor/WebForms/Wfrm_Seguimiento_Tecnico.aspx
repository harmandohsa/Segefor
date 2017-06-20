<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_Seguimiento_Tecnico.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_Seguimiento_Tecnico" %>
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
                                    <div class="ibox-title">
                                        <h2><strong>Seguimiento de Gestión</strong></h2>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-4">
                                            <h4><strong><asp:Label runat="server" ID="LblNug"></asp:Label></strong></h4>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-10">
                                            <h4><strong><asp:Label runat="server" ID="LblSolicitante"></asp:Label></strong></h4>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-12">
                                            <h4><strong><asp:Label runat="server" ID="LblIdentificacion"></asp:Label></strong></h4>
                                        </div>
                                        
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-2">
                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Solicitud"/>
                                            <asp:Label runat="server" Text="Ver Solicitud"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:ImageButton runat="server" ID="IngVerAnexos" ImageUrl="~/Imagenes/24x24/blank.png" formnovalidate ToolTip="Ver Anexos"/>
                                            <asp:Label runat="server" Text="Ver Anexos" ID="LblAnexos"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:ImageButton runat="server" ID="ImgVerProvidencia" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Providencia"/>
                                            <asp:Label runat="server" Text="Ver Providencia"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="padding-bottom:2em;"></div>
                                    <div><h4><strong><asp:Label runat="server" ID="Label2" Text="INFORMACIÓN GENERAL"></asp:Label></strong></h4></div>
                                    <div>
                                        
                                        <div><label class="col-sm-2 control-label centradolabel">Tipo de Plan:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="lblTipoPlan"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Propietarios:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblPropietarios"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Área con bosque:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblAreaBosque"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Área a Intervenir:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblAreaIntervenir"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Área de protección:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblAreaProteccion"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Otros Usos:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblOtrosUsos">0</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Zona de Vida:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblZonaVida"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Fuentes de Agua:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblFuentesAgua"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Elaborador:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblNomElaborador"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div><label class="col-sm-2 control-label centradolabel">Código de elaborador:</label>
                                            <div class="col-sm-10">
                                                <label class="col-sm-10 control-label centradolabel" runat="server" id="LblCodigo"></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <telerik:RadGrid runat="server" ID="GrdInmuebles" Skin="MetroTouch" PageSize="20" 
                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                AllowPaging="true" GridLines="Both" >
                                                                        
                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                PrevPageText="Anterior" Position="Bottom" 
                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                PageSizeLabelText="Regitros"/>
                                            <MasterTableView Caption="" DataKeyNames="InmuebleId,Finca,Area,UbicacionGeo,UbPol,Colindancias" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="InmuebleId" Visible="false" HeaderText="Departamnto" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Finca" HeaderText="Finca" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Area" HeaderText="Área" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UbicacionGeo" HeaderText="Ubicación geográfica" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="UbPol" HeaderText="Ubicación política" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Colindancias" HeaderText="Colindancias" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                </Columns>        
                                            </MasterTableView>
                                            <FilterMenu EnableTheming="true">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label1" Text="Antecedentes"></asp:Label></strong></h4></div>
                                        <telerik:RadGrid runat="server" ID="GrdAntecedentes" Skin="MetroTouch" PageSize="20" 
                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                                AllowPaging="true" GridLines="Both" >
                                                                        
                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                PrevPageText="Anterior" Position="Bottom" 
                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                PageSizeLabelText="Regitros"/>
                                            <MasterTableView Caption="" DataKeyNames="No,Doc,Fec_Doc,Fec_PreEnmienda" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="No" Visible="false" HeaderText="No" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Doc" HeaderText="Documento" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Fec_Doc" HeaderText="Fecha Documento" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Fec_PreEnmienda" HeaderText="Fecha presentación enmienda" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                </Columns>        
                                            </MasterTableView>
                                            <FilterMenu EnableTheming="true">
                                                <CollapseAnimation Duration="200" Type="OutQuint" />
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                        
                                    </div>


                                    <div class="ibox-content">
                                        <div><h4><strong><asp:Label runat="server" ID="Label5" Text="Enmiendas"></asp:Label></strong></h4></div>
                                        <div><label class="col-sm-2 control-label centradolabel">Tiene Enmiendas:</label>
                                            <div class="col-sm-8">
                                                <asp:RadioButtonList runat="server" ID="OptEnmiendas" CssClass="radio radio-inline" AutoPostBack="true">
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div runat="server" id="DivEnmiendas" class="panel-body" visible="false">
                                        <div><label class="col-sm-3 control-label centradolabel">Ingrese enmiendas:</label>
                                            <div class="col-sm-7">
                                                <telerik:RadComboBox ID="CboEnmienda" AutoPostBack="true" Width="100%" runat="server"></telerik:RadComboBox>
                                            </div>
                                            <div class="col-sm-1"><input type="button" runat="server" id="BtnAddEnmienda" title="Buscar" class="btn btn-primary" value="Agregar" /></div>
                                        </div>
                                        <div runat="server" id="DivOtra" visible="false"><label class="col-sm-3 control-label centradolabel">Otra:</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox runat="server" Text="" ID="TxtOtra" class="form-control"></asp:TextBox>    
                                            </div>
                                            
                                        </div>
                                    </div>
                                    <div runat="server" id="DivEnmiendaGrid" class="panel-body" visible="false">
                                        <telerik:RadGrid runat="server" ID="GrdEnmiendas" Skin="MetroTouch"
                                            AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                            GridLines="Both" >
                                            <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                PrevPageText="Anterior" Position="Bottom" 
                                                PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                PageSizeLabelText="Regitros"/>
                                            <MasterTableView Caption="" DataKeyNames="Id_Enmienda,EnmiendaTecId,Enmienda,Otra" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Id_Enmienda" UniqueName="Id_Enmienda" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="EnmiendaTecId" UniqueName="EnmiendaTecId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Enmienda" UniqueName="Enmienda" HeaderText="Enmienda" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Otra" UniqueName="Otra" HeaderText="Otra" Visible="false" HeaderStyle-Width="675px"></telerik:GridBoundColumn>
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
                                     <div class="ibox-content" runat="server" id="DivErrEnmineda" visible="false">
                                        <div class="col-sm-9">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                <asp:Label runat="server" ID="LblErrEnmienda" Font-Bold="true">Error</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="DivDictamenTec">
                                        <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel"><h3>Evaluación de Campo</h3></label></div>
                                        </div>
                                        <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Describir la metodología utilizada para la corroboración del inventario y plan de manejo, (recorrido de linderos, mojones, rodales, etc.):</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" placeholder="Si está planteado para varios turnos de corta se deben evaluar todos los turnos, 	haciendo especial énfasis en el primer turno." ID="TxtMetodologia" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                         <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Incluir la metodología y los resultados de la comprobación del Inventario Forestal:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtMetodologiaResultados" placeholder="Debe requerirse al profesional o técnico forestal que elabora el estudio el marcaje, numeración y ubicación geográfica de las parcelas levantadas para realizar el inventario forestal, deberá muestrearse un 30% de estas parcelas, previamente seleccionadas al azar en gabinete).  Para hacer esta comprobación es necesario que el Inventario Forestal tenga un buen diseño estadístico (al azar o sistemático). " TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Forma de Evaluación</label>
                                                <div class="col-sm-5">
                                                    <telerik:RadComboBox ID="CboTipoInventario" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div>
                                                <label class="col-sm-1 control-label"></label>
                                                    <div class="col-sm-10">
                                                        <div class="panel-body">
                                                            <div class="panel-group" id="accordionCenso">
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading">
                                                                        <h5 class="panel-title">
                                                                            <a data-toggle="collapse" data-parent="#accordionCenso" href="#collapseOneCenso"><asp:Label runat="server" ID="LbltitPanCenso" Text="Censo"></asp:Label> </a>
                                                                        </h5>
                                                                    </div>
                                                                    <div id="collapseOneCenso" class="panel-collapse collapse out">
                                                                        <div class="panel-body">
                                                                            <label class="col-sm-2 control-label"><asp:Label runat="server" ID="LblCargueCenso" Text="Censo"></asp:Label></label>
                                                                            <div class="col-sm-5">
                                                                                <telerik:RadAsyncUpload runat="server" ID="RadUploadBoleta" Culture="es-GT" MaxFileInputsCount="1"
                                                                                        AllowedFileExtensions="xls,xlsx">
                                                                                </telerik:RadAsyncUpload>
                                                                            </div>
                                                                            <div class="col-sm-2">
                                                                                <a class="btn btn-primary m-b" runat="server" id="BtnCargarBoleta">Cargar</a>
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel-body">
                                                                            <telerik:RadGrid runat="server" ID="GrdBoleta" Skin="Telerik"
                                                                                AutoGenerateColumns="false" Width="100%" AllowSorting="true"  
                                                                                GridLines="Both" PageSize="20" >
                                                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                                                    PrevPageText="Anterior" Position="Bottom" 
                                                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                                                    PageSizeLabelText="Regitros"/>
                                                                                <MasterTableView Caption="" DataKeyNames="Turno,Rodal,No,Dap,Altura,Nombre_Cientifico,Troza,X,Y" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                                                    <Columns>
                                                                                        <telerik:GridBoundColumn DataField="Turno" Visible="false" UniqueName="Turno" HeaderText="Turno" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Parcela" Visible="false" UniqueName="Parcela" HeaderText="Parcela" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="No" UniqueName="No" HeaderText="No" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Dap" UniqueName="Dap" HeaderText="Dap" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura" HeaderText="Altura" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Troza" UniqueName="Troza" HeaderText="Troza" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="X" UniqueName="X" HeaderText="X" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                                                        <telerik:GridBoundColumn DataField="Y" UniqueName="Y" HeaderText="Y" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
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
                                            <div class="col-sm-10">
                                                <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrCargaCenso" visible="false">
                                                    <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                    <asp:Label runat="server" ID="LblErrCargaCenso" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="alert alert-success alert-dismissable" runat="server" id="DivGoodCargaCenso" visible="false">
                                                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                    <asp:Label runat="server" ID="LblGoodCargaCenso" Font-Bold="true">Error</asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivEvalMuestreo" visible="false">
                                            <div><label class="col-sm-2 control-label centradolabel">Tamaño</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server" MinValue="1" ID="TxtSize" Width="60px" >
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-3 control-label centradolabel">Forma de Parcelas</label>
                                                <div class="col-sm-3">
                                                    <telerik:RadComboBox ID="CboFormaParcela" EnableCheckAllItemsCheckBox="true" Localization-AllItemsCheckedString="Todos los items seleccionados" Localization-CheckAllString="Seleccionar Todos" Localization-ItemsCheckedString="Seleccionados" CheckBoxes="true"  Width="100%" runat="server"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivEvalCenso" visible="false">
                                            <div><label class="col-sm-2 control-label centradolabel">Total de Rodales</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server" MinValue="1" ID="TxtTotRodales" Width="60px" >
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-3 control-label centradolabel">Rodales Muestreados</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server" MinValue="1" ID="TxtRodalesMuestreados" Width="60px" >
                                                        <NumberFormat DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <telerik:RadGrid runat="server" ID="GrdResumen" Skin="Telerik" CssClass="AddBorders"
                                                AutoGenerateColumns="false" Width="100%"   
                                                GridLines="Both" PageSize="20" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Rodal,EspecieId,Nombre_Cientifico,AreaRodal,Clase_Desarrollo,Edad,Tratamiento,Dap,Altura,Densidad,AreaBasal,VolTroza,VolLena,VolOtro,VolTotal,sumadap,sumaaltura,arboles,SumBa,volumen,Troza,Pendiente,INC,VolHa,VolRodal,AreaBasalRodal" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                                    
                                                        <telerik:GridBoundColumn DataField="AreaRodal" UniqueName="AreaRodal"  HeaderText="Area del Rodal (ha)" Visible="true" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Clase_Desarrollo" UniqueName="Clase_Desarrollo"  HeaderText="Clase_Desarrollo" Visible="true" HeaderStyle-Width="150px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Edad" UniqueName="Edad"  HeaderText="Edad" Visible="true" HeaderStyle-Width="50px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Pendiente" UniqueName="Pendiente"  HeaderText="Pendiente" Visible="true" HeaderStyle-Width="50px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="INC" UniqueName="INC"  HeaderText="INC" Visible="true" HeaderStyle-Width="50px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Tratamiento"  UniqueName="Tratamiento"  HeaderText="Tratamiento" Visible="false" HeaderStyle-Width="50px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EspecieId" Visible="false" UniqueName="EspecieId" HeaderText="EspecieId" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Dap" UniqueName="Dap" DataFormatString="{0:F2}"  HeaderText="Dap Medio (cm)" Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Altura" UniqueName="Altura" DataFormatString="{0:F2}"   HeaderText="Altura Media (m)" Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Densidad" UniqueName="Densidad" DataFormatString="{0:F2}"  HeaderText="Densidad arboles/ha" Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AreaBasal" UniqueName="AreaBasal" DataFormatString="{0:F2}"  HeaderText="Area Basal m2/ha" Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolTroza" UniqueName="VolTroza"  HeaderText="VolTroza" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolLena" UniqueName="VolLena"  HeaderText="VolLena" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolOtro" UniqueName="VolOtro"  HeaderText="VolOtro" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolTotal" UniqueName="VolTotal"  HeaderText="VolTotal" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AreaBasalRodal" UniqueName="AreaBasalRodal" DataFormatString="{0:F2}"  HeaderText="Area Basal m2/Rodal" Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolHa" UniqueName="VolHa"  HeaderText="Vol/Ha. (M3)" DataFormatString="{0:F2}"  Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolRodal" UniqueName="VolRodal"  HeaderText="Vol/Rodal" DataFormatString="{0:F2}"  Visible="true" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="sumadap" UniqueName="sumadap"  HeaderText="sumadap"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="sumaaltura" UniqueName="sumaaltura"  HeaderText="sumaaltura"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="arboles" UniqueName="arboles"  HeaderText="arboles"  HeaderStyle-Width="75px" Visible="false" ></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SumBa" UniqueName="SumBa"  HeaderText="SumBa"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="volumen" UniqueName="volumen"  HeaderText="volumen"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Troza" UniqueName="Troza"  HeaderText="Troza"  HeaderStyle-Width="75px" Visible="false"></telerik:GridBoundColumn>
                                                    </Columns>        
                                                </MasterTableView>
                                                <FilterMenu EnableTheming="true">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                        <div class="panel-body">
                                            <h3>Conclusiones</h3>
                                            <div><label class="col-sm-5 control-label centradolabel">Conclusiones sobre las características biofísicas (suelo, agua,pendiente, etc) :</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtConclusionCaracBio" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Conclusiones sobre los resultados y veracidad de la información presentada en el inventario forestal, estratificación y/o rodalización del bosque:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtConclusionInventario" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Conclusiones sobre la propuesta de manejo forestal, haciendo especial énfasis sobre la Corta Anual Permisible:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtConcluManejo" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Conclusión sobre la propuesta de tratamiento integrando características biofísicas, inventario forestal.</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtCncluPropuesta" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <h3>DICTAMEN TECNICO</h3>
                                            <div><label class="col-sm-5 control-label centradolabel">Dictamina</label>
                                                <div class="col-sm-5">
                                                    <telerik:RadComboBox ID="CboDictamina" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba" visible="false">
                                           <telerik:RadGrid runat="server" ID="GrdSilvicultural" Skin="Telerik" CssClass="AddBorders"
                                                AutoGenerateColumns="false" Width="100%"   
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Correlativo,Rodal,EspecieId,Nombre_Cientifico,AreaRodal,Clase_Desarrollo,Edad,Tratamiento,Dap,Altura,Densidad,AreaBasal,VolTroza,VolLena,VolOtro,VolTotal,sumadap,sumaaltura,arboles,SumBa,volumen,Troza,Pendiente,INC,VolHa,VolRodal,AreaBasalRodal" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Correlativo" UniqueName="Correlativo" HeaderText="Correlativo" Visible="false"  HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="" UniqueName="Turno" HeaderText="Turno" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AreaRodal" DataFormatString="{0:F2}"  UniqueName="AreaRodal"  HeaderText="Area del Rodal (ha)" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Edad" UniqueName="Edad"  HeaderText="Edad" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Tratamiento" UniqueName="Tratamiento"  HeaderText="Tratamiento" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Otro" UniqueName="Otro"  HeaderText="Otro"  HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Densidad" UniqueName="Densidad" DataFormatString="{0:F2}"   HeaderText="Densidad arboles/ha" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AreaBasal" UniqueName="AreaBasal" DataFormatString="{0:F2}"  HeaderText="Area Basal m2/ha" HeaderStyle-Width="125px"  ></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AreaBasalRodal" UniqueName="AreaBasalRodal" DataFormatString="{0:F2}"   HeaderText="Area basal  m2/rodal" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolTroza" UniqueName="VolTroza" DataFormatString="{0:F2}"  HeaderText="Vol. Troza" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolLena" UniqueName="VolLena" DataFormatString="{0:F2}"   HeaderText="Vol. Lena"  HeaderStyle-Width="125px" ></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolTotal" UniqueName="VolTotal" DataFormatString="{0:F2}"  HeaderText="Vol. Total"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    </Columns>        
                                                </MasterTableView>
                                                <FilterMenu EnableTheming="true">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba2" visible="false">
                                            <h3>Compromiso de repoblación forestal</h3>
                                            <div><label class="col-sm-3 control-label centradolabel">Calculo de compromiso en base a:</label>
                                                <div class="col-sm-3">
                                                    <telerik:RadComboBox ID="CboAreaCompromiso"  Width="100%" runat="server"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-2 control-label centradolabel">Área de Compromiso</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server"  ID="TxtAreaCompromiso" Width="60px" Enabled="false" >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba3" visible="false">
                                            <div><label class="col-sm-2 control-label centradolabel">Especies:</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtEspeciesCompromiso" TextMode="MultiLine" Width="500px" Height="150px" Enabled="false" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba4" visible="false">
                                            <div><label class="col-sm-2 control-label centradolabel">Densidad Inicial</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server" ID="TxtDensidadInicial" Width="100%" Enabled="false" >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-3 control-label centradolabel">Sistema de Repoblación</label>
                                                <div class="col-sm-3">
                                                   <asp:TextBox runat="server" Text="" ID="TxtSistemaRepoblacion" TextMode="MultiLine" Width="100%" Enabled="false" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba5" visible="false">
                                            <div><label class="col-sm-2 control-label centradolabel">Lugar (Finca/municipio):</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtLugarFinca" TextMode="MultiLine" Width="500px" Height="150px" Enabled="false" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba6" visible="false">
                                            <div><label class="col-sm-2 control-label centradolabel">Tipo de Garantia:</label>
                                                <div class="col-sm-5">
                                                    <telerik:RadComboBox ID="CboGarantia" AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba7" visible="false">
                                            <telerik:RadGrid runat="server" ID="GrdEtapa" Skin="Telerik" CssClass="AddBorders"
                                                AutoGenerateColumns="false" Width="100%"   
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="EtapaId,Etapa" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="EtapaId" UniqueName="EtapaId" HeaderText="EtapaId" Visible="false"  HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Etapa" UniqueName="Etapa" HeaderText="Etapa" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn DataField="Fecha_Inicio" HeaderText="Fecha Inicio"  UniqueName="FecIni" HeaderStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <telerik:RadDatePicker ID="TxtFecIni" Width="100%" runat="server"></telerik:RadDatePicker>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn DataField="Fecha_Final" HeaderText="Fecha Final"  UniqueName="FecFin" HeaderStyle-Width="75px">
                                                            <ItemTemplate>
                                                                <telerik:RadDatePicker ID="TxtFecFin" Width="100%" runat="server"></telerik:RadDatePicker>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                    </Columns>        
                                                </MasterTableView>
                                                <FilterMenu EnableTheming="true">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                </ClientSettings>
                                            </telerik:RadGrid>    
                                        </div>   
                                        <div class="panel-body" runat="server" id="DivAprueba8" visible="false">
                                            <div><label class="col-sm-3 control-label centradolabel">Monto:</label>
                                                <div class="col-sm-3">
                                                    <telerik:RadNumericTextBox runat="server" ID="TxtMonto" Width="100%" Enabled="false" >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-2 control-label centradolabel">Porcentaje de la garantía</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server" ID="TxtPorcentajeGarantia" Width="100%" Enabled="false" >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba9" visible="false">
                                           <telerik:RadGrid runat="server" ID="GrdMaderaPie" Skin="Telerik" CssClass="AddBorders"
                                                AutoGenerateColumns="false" Width="100%"   
                                                GridLines="Both" >
                                                <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                                    PrevPageText="Anterior" Position="Bottom" 
                                                    PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                                    PageSizeLabelText="Regitros"/>
                                                <MasterTableView Caption="" Name="LabelsResumen" DataKeyNames="Correlativo,Rodal,EspecieId,Nombre_Cientifico,AreaRodal,Clase_Desarrollo,Edad,Tratamiento,Dap,Altura,Densidad,AreaBasal,VolTroza,VolLena,VolOtro,VolTotal,sumadap,sumaaltura,arboles,SumBa,volumen,Troza,Pendiente,INC,VolHa,VolRodal,AreaBasalRodal" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="Correlativo" UniqueName="Correlativo" HeaderText="Correlativo" Visible="false"  HeaderStyle-Width="45px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="" UniqueName="Turno" HeaderText="Turno" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Rodal" UniqueName="Rodal" HeaderText="Rodal" HeaderStyle-Width="75px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Nombre_Cientifico" UniqueName="Nombre_Cientifico" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EspecieId" Visible="false" UniqueName="EspecieId" HeaderText="Nombre Cientifico" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolTroza" UniqueName="VolTroza" HeaderText="Vol. Troza" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolLena" UniqueName="VolLena" HeaderText="Vol. Lena"  HeaderStyle-Width="125px" ></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="VolTotal" UniqueName="VolTotal"  HeaderText="Vol. Total"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="" UniqueName="ValTroza"  HeaderText="Valor Madera Troza"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="" UniqueName="ValLena"  HeaderText="ValLena"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="" UniqueName="ValPagar"  HeaderText="Valor Total (Q)"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="" UniqueName="PorPagar"  HeaderText="10 % a pagar (Q)"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                    </Columns>        
                                                </MasterTableView>
                                                <FilterMenu EnableTheming="true">
                                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                                </FilterMenu>
                                                <ClientSettings>
                                                    <Scrolling AllowScroll="true" UseStaticHeaders="True"  />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </div>   
                                        <div class="panel-body"  runat="server" id="DivAprueba10" visible="false">
                                            <h3>Recomendaciones</h3>
                                            <div><label class="col-sm-3 control-label centradolabel">Vigencia del Aprovechamiento:</label>
                                                <div class="col-sm-3">
                                                    <telerik:RadNumericTextBox runat="server" ID="TxtVigenciaApro" Value="9" Width="100%" MinValue="1"  >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-2 control-label centradolabel">Cantidad Notas Autorizadas</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox runat="server" ID="TxtCantidadAutorizadas" onkeyup="Notas()" step="any"  min="1"  type="number"  CssClass="form-control" ClientIDMode="Static" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body"  runat="server" id="DivAprueba11" style="display: none;" ClientIDMode="Static">
                                            <div><label class="col-sm-3 control-label centradolabel">Tipo de Usuario:</label>
                                                <div class="col-sm-3">
                                                    <telerik:RadComboBox ID="CboTipoUsuario"  AutoPostBack="true"  Width="100%" runat="server"></telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div><label class="col-sm-2 control-label centradolabel">Cantidad de notas a Entregar</label>
                                                <div class="col-sm-2">
                                                    <telerik:RadNumericTextBox runat="server" ID="TxtNotasEntregar"  MinValue="1" Width="100%" Enabled="false" >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body"  runat="server" id="DivAprueba12" style="display: none;" ClientIDMode="Static">
                                            <div><label class="col-sm-3 control-label centradolabel">Cantidad de notas restantes</label>
                                                <div class="col-sm-3">
                                                    <telerik:RadNumericTextBox runat="server" ID="TXtCantidadRestante" MinValue="1" Width="100%" Enabled="false" >
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body" runat="server" id="DivAprueba13" visible="false">
                                            <div><label class="col-sm-5 control-label centradolabel">Otras Recomendaciones</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TxtOtrasReco" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding-bottom:2em;"></div>
                                    </div>
                                    

                                    
                                    <div style="padding-bottom:2em;"></div>
                                    <div class="ibox-title" runat="server" id="DivDictamen" visible="false">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Dictamen"  ID="BtnVistaPrevia" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Dictamen"   ID="BtnEnviar" class="btn btn-primary" /></div>
                                    </div>
                                    <div class="ibox-title" runat="server" id="DivEnmiendasBotonos" visible="false">
                                        <div class="col-sm-3"><asp:Button runat="server" Text="Vista Previa Enmiendas"  ID="BtVistaPreviaEnminada" class="btn btn-primary" /></div>
                                        <div class="col-sm-1"><asp:Button runat="server" Text="Grabar Enmienadas"   ID="BtnEnviarEnmienda" class="btn btn-primary" /></div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="col-sm-9">
                                            <div class="alert alert-danger alert-dismissable" runat="server" id="DivError" visible="false">
                                                <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                                <asp:Label runat="server" ID="LblMensaje" Font-Bold="true">Error</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-10">
                                        <div class="alert alert-danger alert-dismissable" runat="server" id="DivErrDictamenGen" visible="false">
                                            <button runat="server" aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="LblErrDictamenGen" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="alert alert-success alert-dismissable" runat="server" id="Div2" visible="false">
                                            <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                            <asp:Label runat="server" ID="Label4" Font-Bold="true">Error</asp:Label>
                                        </div>
                                    </div>
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
                                    <asp:Button runat="server" Text="Sí"  ID="BtnYes" data-loading-text="Enviando..."  class="btn btn-primary" />
                                </div>
                                <%--<div class="col-sm-2">
                                    <asp:Button runat="server" Text="No"  ID="BtnNo" class="btn btn-primary" />
                                </div>--%>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
            <asp:TextBox runat="server" ID="TxtIdArticulo" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtIdAnalisis" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtIdEnmienda" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtTotMaderaPie" Visible="false"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function pageLoad() {
            $('#<%=BtnYes.ClientID%>').click(function () {
                $(this).button('loading').delay(100000).queue(function () {
                    $(this).button('reset');
                    $(this).dequeue();
                    $(this).data('loading-text', 'Cargando...');
                });
            });
        }

        function Notas() {
            var NotasAutorizadas = document.getElementById('<%=TxtCantidadAutorizadas.ClientID%>').value
            if (NotasAutorizadas > 10) {
                $('#DivAprueba11').fadeIn('slow');
                $('#DivAprueba12').fadeIn('slow');
                }
            else {
                $('#DivAprueba11').fadeOut('slow');
                $('#DivAprueba12').fadeOut('slow');
            }
        }

        <%--function Calculo() {
            var NotasAutorizadas = document.getElementById('<%=TxtCantidadAutorizadas.ClientID%>').value
            alert(NotasAutorizadas);
            var combo = $find('<%=CboTipoUsuario.ClientID %>');
            alert(combo.get_selectedItem().get_value());
            $('#TxtNotasEntregar').val((NotasAutorizadas / 100) * 75);
            $('#TXtCantidadRestante').val(((NotasAutorizadas / 100) * 75) - NotasAutorizadas)
        }--%>

      
    </script>
</asp:Content>
