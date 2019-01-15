<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="EmployeeManagement.aspx.cs" Inherits="PerformanceAppraisalProj.EmployeeManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .TextArea {
            font-size: 1em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .auto-style11 {
            height: 25px;
        }

        .auto-style14 {
            width: 296px;
        }

        .auto-style15 {
            width: 294px;
        }

         .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.85;
        }

        .PanelPopup {
            background-color: White;
            border-color: Black;
            border-style: solid;
            border-width: 2px;
        }
        .auto-style20 {
            width: 77px;
            height: 21px;
        }
        .auto-style21 {
            height: 21px;
        }
        .auto-style22 {
            width: 77px;
        }
        .auto-style24 {
            width: 159px;
        }
        .auto-style25 {
            width: 153px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table class="auto-style1">
                <tr>
                    <td colspan="2" class="auto-style11">
                        <asp:Label ID="Label8" runat="server" Text="Employee Management - Update manager data" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style24">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="auto-style1">
                            <tr>
                                <td width="110px">
                                    <asp:Label ID="Label3" runat="server" Text="Search Employee:"></asp:Label>
                                </td>
                                <td width="80px">
                                    <asp:ImageButton ID="ibtnSearch" runat="server" Height="22px" ImageUrl="~/Images/search_icon24.png" OnClick="ibtnSearch_Click" ToolTip="Search" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style24">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnSearchResult" runat="server">                            
                            <table class="auto-style1">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Employee Details"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Name of Employee:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Position:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPosition" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Department/Devision:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Text="Date Joined:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Text="Contract Start:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContractStart" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Text="Contract End:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContractEnd" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label19" runat="server" Text="First Manager Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt1stMgtName" runat="server" MaxLength="150" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Text="First Manager Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt1stMgtEmail" runat="server" MaxLength="150" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label21" runat="server" Text="Second Manager Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt2ndMgtName" runat="server" MaxLength="150" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Text="Second Manager Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt2ndMgtEmail" runat="server" MaxLength="150" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Update" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                            
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="hdfUserLogin" runat="server" />
                        <asp:HiddenField ID="hdfEmployeeID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>

            </table>

            <!-- Modal popup "Popup Search Name" -->
            <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupCtrl" Enabled="True" PopupControlID="pnPopup"
                TargetControlID="lbtnTargerCtrl">
            </cc1:ModalPopupExtender>
            <!--  -->
            <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="45%" Height="50%"
             Style="display: none">
                <table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style22" align="left">&nbsp;</td>
                                    <td align="left" class="auto-style15">&nbsp; </td>
                                    <td align="left" class="style2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style22" align="right">
                                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="Find :"></asp:Label>
                                    </td>
                                    <td align="left" class="auto-style15" colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                        &nbsp;
                                        <asp:ImageButton ID="ibtnFind" runat="server" ImageUrl="~/Images/search_icon24.png" OnClick="ibtnFind_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; vertical-align: top" class="auto-style22">
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Text="Result :"></asp:Label>
                                    </td>
                                    <td class="auto-style5" colspan="2">

                                        <asp:GridView ID="gvSearchUser" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            EmptyDataText="No data found." ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True"
                                            Width="90%" EnableViewState="true" PageSize="5"
                                            OnPageIndexChanging="gvSearchUser_PageIndexChanging"
                                            OnRowCommand="gvSearchUser_RowCommand">
                                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" Font-Size="12px" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                                            <Columns>
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="80%" />
                                                <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Font-Size="11px" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lblSelect"
                                                            CommandArgument='<%#Eval("EmployeeName")+","+ Eval("EmployeeID")%>'
                                                            CommandName="Select">Select</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="auto-style20"></td>
                                    <td class="auto-style21" colspan="2"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click" Text="Close" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!-- Modal popup "Popup Insert/Update Result" -->
            <asp:LinkButton ID="lbtnPopupAppCtrl" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargerAppCtrl" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="lbtnPopupApp_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupAppCtrl" Enabled="True" PopupControlID="pnPopupApp"
                TargetControlID="lbtnTargerAppCtrl">
            </cc1:ModalPopupExtender>
            <!--   -->
            <asp:Panel ID="pnPopupApp" runat="server" CssClass="PanelPopup" Width="35%" Height="40%"
                Style="display: none">
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style14" align="left">&nbsp;</td>
                        <td align="left" class="auto-style15">&nbsp; </td>
                        <td align="left" class="style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label23" runat="server" Font-Bold="True" Text="Update employee data "></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Result :"></asp:Label>
                            <asp:Label ID="lblResult" runat="server" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Button ID="btnClose" runat="server" CausesValidation="False"
                                OnClick="btnClose_Click" Style="height: 26px" Text="Close" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="auto-style14">&nbsp;</td>
                        <td class="auto-style5" colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- For Popup Message -->

        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
