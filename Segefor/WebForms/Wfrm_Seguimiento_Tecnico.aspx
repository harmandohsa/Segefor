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
                                                    <asp:TextBox runat="server" Text="" ID="TxtMetodologia" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
                                                </div>
                                            </div>
                                        </div>
                                         <div class="panel-body">
                                            <div><label class="col-sm-5 control-label centradolabel">Describir la metodología utilizada para la corroboración del inventario y plan de manejo, (recorrido de linderos, mojones, rodales, etc.):</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox runat="server" Text="" ID="TextBox1" TextMode="MultiLine" Width="500px" Height="150px" class="form-control"></asp:TextBox>   
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
    </script>
</asp:Content>
