<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site1.Master" AutoEventWireup="true" CodeBehind="Wfrm_PlanManejo.aspx.cs" Inherits="SEGEFOR.WebForms.Wfrm_PlanManejo" %>
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
                                    <h2><strong>Administración Planes de Manejo</strong></h2>
                                </div>
                                <div class="ibox-title">
                                    <h4><strong>Planes de Manejo Solicitados</strong></h4>
                                </div>
                                <div class="ibox-content">
                                    <telerik:RadGrid runat="server" ID="GrdPlanesSolicitados" Skin="MetroTouch" AllowPaging="true"  PageSize="6"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente"  
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Fec,Solicitante,Estatus,AsignacionId,Area,Regente,EstatusId" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="AsignacionId" UniqueName="AsignacionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fec" UniqueName="Fec" HeaderText="Fecha Solicitud" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitante" UniqueName="Solicitante" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Estatus" UniqueName="Estatus" HeaderText="Estatus" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EstatusId" UniqueName="EstatusId" Visible="false" HeaderText="Estatus" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Area" UniqueName="Area"  DataFormatString="{0:#,##0.00}" HeaderText="Área" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Regente" UniqueName="Regente" HeaderText="Elaborador" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver información" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="VerInfo" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" Visible="false" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Anexos" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="Anexos" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgAnexos" Visible="false" ImageUrl="~/Imagenes/24x24/blank.png" formnovalidate ToolTip="Anexos" CommandName="CmdAnexos"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Devolver a Elaborador" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="DevElab" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgDevElab" Visible="false" ImageUrl="~/Imagenes/24x24/person.png" formnovalidate ToolTip="Devolver" CommandName="CmdDevElb"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Imprimir solicitud" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="PrintSol" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgPrintSol" Visible="false" ImageUrl="~/Imagenes/24x24/folder.png" formnovalidate ToolTip="Enviar a INAB" CommandName="CmdPrintSol"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn>  
                                                <telerik:GridTemplateColumn HeaderText="Enviar a INAB" ShowFilterIcon="false" AllowFiltering="false"  Visible="true" UniqueName="EnvINAB" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgEnvINAB" Visible="false" ImageUrl="~/Imagenes/24x24/go.png" formnovalidate ToolTip="Enviar a INAB" CommandName="CmdEnvInab"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                                </div>
                                <div class="ibox-title" runat="server" id="DivTitPlanProceso">
                                    <h4><strong>Planes de Manejo en Proceso</strong></h4>
                                </div>
                                <div class="ibox-content"  runat="server" id="DivPlanProceso">
                                    <telerik:RadGrid runat="server" ID="GrdPlanesSolicitadosComoRegente" AllowPaging="true"  Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" AllowFilteringByColumn="true" PageSize="6"
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Fec,Solicitante,Estatus,AsignacionId,Area,FechaAcepta,UsuarioId,SubCategoriaId,CategoriaId,Categoria" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="UsuarioId" UniqueName="UsuarioId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SubCategoriaId" UniqueName="SubCategoriaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CategoriaId" UniqueName="CategoriaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AsignacionId" UniqueName="AsignacionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fec" UniqueName="Fec" ShowFilterIcon="false" AllowFiltering="false" HeaderText="Fecha Solicitud" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Solicitante" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Solicitante" HeaderText="Solicitante" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Categoria" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" UniqueName="Categoria" HeaderText="Categoria" HeaderStyle-Width="300px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Estatus" ShowFilterIcon="false" AllowFiltering="false" UniqueName="Estatus" HeaderText="Estatus" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Area" ShowFilterIcon="false" AllowFiltering="false"  DataFormatString="{0:#,##0.00}" UniqueName="Area" HeaderText="Área" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaAcepta" ShowFilterIcon="false" AllowFiltering="false" UniqueName="FechaAcepta" HeaderText="Fecha de Aceptación" HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Aceptar" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Ok">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgAceptar" ImageUrl="~/Imagenes/24x24/ok.png" formnovalidate ToolTip="Aceptar" CommandName="CmdOk"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Rechazar" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="No">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgRechazar" ImageUrl="~/Imagenes/24x24/cancel.png" formnovalidate ToolTip="Aceptar" CommandName="CmdNo"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                                <telerik:GridTemplateColumn HeaderText="Ir al Plan" ShowFilterIcon="false" AllowFiltering="false" Visible="true" UniqueName="Go">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgIrPlan" ImageUrl="~/Imagenes/24x24/go.png" formnovalidate ToolTip="Ir al Plan" CommandName="CmdGo"/>
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
            <asp:TextBox runat="server" ID="TxtAsignacionId" Visible="false"></asp:TextBox>
            <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
                <Windows>
                    <telerik:RadWindow RenderMode="Lightweight" ID="RadWindow1" runat="server" ShowContentDuringLoad="false" Width="800px"
                        Height="600px" Title="Telerik RadWindow" Behaviors="Default">
                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWindowDetalle" Modal="true" Height="420px" Width="850px" Title="Detalle Solicitudes de Completación" Behaviors="Close">
                        <ContentTemplate>
                            <asp:Label ID="LblTitConfirmacion" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            <div class="ibox-content">
                                <telerik:RadGrid runat="server" ID="GrdDetalle" Skin="MetroTouch"
                                        AutoGenerateColumns="false" Width="100%" AllowSorting="true" 
                                        GridLines="Both" >
                                        <PagerStyle Mode="NumericPages" NextPageText="Siguiente" 
                                            PrevPageText="Anterior" Position="Bottom" 
                                            PagerTextFormat="Change page: {4} &amp;nbsp;Pagina &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, registros &lt;strong&gt;{2}&lt;/strong&gt; a &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;." 
                                            PageSizeLabelText="Regitros"/>
                                        <MasterTableView Caption="" DataKeyNames="Gestion_IncompletaId,GestionId,NUG,No_Solicitud_Completacion,Fecha" NoMasterRecordsText="No Hay Registros" ShowFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Gestion_IncompletaId" UniqueName="Gestio_IncompletaId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="GestionId" UniqueName="GestionId" Visible="false" HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="No_Solicitud_Completacion" Visible="false" UniqueName="SCG" HeaderText="No. Completación gestión"  HeaderStyle-Width="200px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NUG" UniqueName="NUG" HeaderText="No. Gestión"  HeaderStyle-Width="125px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Fecha" UniqueName="Fecha" HeaderText="Fecha de Gestión" HeaderStyle-Width="175px"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Ver Constancia" Visible="true" UniqueName="Seg" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="ImgVerinfo" ImageUrl="~/Imagenes/24x24/pdf.png" formnovalidate ToolTip="Ver Información" CommandName="CmdVer"/>
                                                        </ItemTemplate>
                                                </telerik:GridTemplateColumn> 
                                            </Columns>        
                                        </MasterTableView>
                                        <FilterMenu EnableTheming="true">
                                            <CollapseAnimation Duration="200" Type="OutQuint" />
                                        </FilterMenu>
                                    </telerik:RadGrid>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWinAnexos" Modal="true" Height="850px" Width="850px" Title="Censo/Muestro" Behaviors="Default">

                    </telerik:RadWindow>
                    <telerik:RadWindow runat="server" ID="RadWindowConfirm" Modal="true" Height="420px" Width="450px" Title="Confirmación" Behaviors="Close">
                        <ContentTemplate>
                            <asp:Label ID="Label1" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
                            <br />
                            <br />
                            
                            <div class="ibox-content">
                                <div class="col-sm-3">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ask.png" />
                                </div>
                            <div class="ibox-content">
                                <asp:Label runat="server" Text="Debera adjuntar la siguiente papeleria"></asp:Label>
                                    <li>
                                        <asp:Label runat="server" Text="Solicitud Autenticada"></asp:Label>
                                    </li>
                                    <li>
                                        <asp:Label runat="server" Text="Documento original que acredite la propiedad del bien"></asp:Label>
                                    </li>
                                    <li runat="server" visible="false" id="DocPropietario">
                                        <asp:Label runat="server" Text="Copia del documento personal de identificación del propietario "></asp:Label>
                                    </li>
                                    <li runat="server" visible="false" id="DocRepresentante">
                                        <asp:Label runat="server" Text="Copia del documento personal de identificación del Representante Legal"></asp:Label>
                                    </li>
                                    <li runat="server" visible="false" id="DocRepresentanteDos">
                                        <asp:Label runat="server" Text="Copia legalizada del nombramiento  de representante legal, inscrito en el Registro correspondiente; "></asp:Label>
                                    </li>
                                    <li>
                                        <asp:Label runat="server" Text="Plan de Manejo Forestal"></asp:Label>
                                    </li>
	                            </div>
                                <div class="ibox-content">
                                    <a class="btn btn-primary m-b" runat="server" id="BtnYes" data-loading-text="Enviando...">Enviar</a>
                                    </div>
                                    
                                <%--<div class="col-sm-2">
                                    <a class="btn btn-primary m-b" runat="server" id="BtnNo">No</a>
                                </div>--%>
                            </div>
                            
                        </ContentTemplate>
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
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
