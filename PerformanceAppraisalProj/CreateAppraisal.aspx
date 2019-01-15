<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CreateAppraisal.aspx.cs" Inherits="PerformanceAppraisalProj.CreateAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
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

        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            height: 21px;
        }

        .auto-style4 {
            height: 21px;
            width: 132px;
        }

        .auto-style7 {
            width: 92px;
        }

        .TextArea {
            font-size: 1em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .auto-style9 {
            width: 190px;
        }

        .auto-style10 {
            width: 132px;
        }

        .auto-style11 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table class="auto-style1">
                <tr>
                    <td colspan="4" class="auto-style11">
                        <asp:Label ID="Label8" runat="server" Text="Performance Appraisal - Select Employee  " Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label3" runat="server" Text="Name of Employee:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:TextBox ID="txtEmpName" runat="server" Width="165px" MaxLength="150"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label4" runat="server" Text="Employee ID:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmpID" runat="server" Width="165px" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label6" runat="server" Text="Company:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:DropDownList ID="ddlCompany" runat="server" Width="120px"
                            DataTextField="CompanyAD" DataValueField="CompanyAD">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label5" runat="server" Text="Department/Devision:"></asp:Label>
                    </td>
                    <td>

                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="250px"
                            DataTextField="DepartmentName" DataValueField="RowID">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td class="auto-style2" colspan="3">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" Width="100px" />
                        &nbsp;<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label9" runat="server" Text="List of Employee:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="No data found." ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="True"
                            Width="100%" EnableViewState="true" PageSize="20" OnPageIndexChanging="gvEmployee_PageIndexChanging" 
                            OnRowDataBound="gvEmployee_RowDataBound" CssClass="GridView">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings Mode="NumericFirstLast"
                                PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White"/>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfFirstManager" runat="server" Value='<%# Eval("FirstManager") %>' />
                                        <asp:HiddenField ID="hdfSecondManager" runat="server" Value='<%# Eval("SecondManager") %>' />
                                        <asp:HiddenField ID="hdfAppraisalDate" runat="server" Value='<%# Eval("AppraisalDate") %>' />
                                        <asp:HiddenField ID="hdfEmployeeID" runat="server" Value='<%# Eval("EmployeeID") %>' />
                                        <asp:HiddenField ID="hdfHRStaff" runat="server" Value='<%# Eval("HRStaff") %>' />
                                        <asp:HiddenField ID="hdfCreatedDate" runat="server" Value='<%# Eval("CreatedDate") %>' />
                                        <asp:HiddenField ID="hdfFirstManagerMail" runat="server" Value='<%# Eval("FirstManagerMail") %>' />
                                        <asp:HiddenField ID="hdfSecondManagerMail" runat="server" Value='<%# Eval("SecondManagerMail") %>' />
                                        <asp:HiddenField ID="hdfEmployeeName" runat="server" Value='<%# Eval("EmployeeName") %>' />
                                        <asp:HiddenField ID="hdfPosition" runat="server" Value='<%# Eval("Position") %>' />
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Employee ID" DataField="EmployeeID">
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Employee Name" DataField="EmployeeName">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Company" DataField="CompanyID">
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Department" DataField="DepartmentName">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Position" DataField="Position">
                                    <ItemStyle Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AppraisalDate" HeaderText="Appraisal Date" ItemStyle-Width="10%"
                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy}" HtmlEncode="false" />
                                <asp:TemplateField HeaderText="Detail">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplSelectProj" runat="server" Text="Click"
                                            NavigateUrl='<%#String.Format("~/Form/PerformanceAppraisalDetail.aspx?EmployeeID={0}", Eval("EmployeeID"))%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
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
                    <td class="auto-style10">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Button ID="btnSubmit" runat="server"
                            OnClick="btnSubmit_Click" Text="Create Appraisal" Width="120px" />
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <!-- Modal popup "Popup Message" -->
            <asp:LinkButton ID="lbtnPopupCtrl" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargerCtrl" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="lbtnPopup_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnPopup"
                TargetControlID="lbtnTargerCtrl">
            </cc1:ModalPopupExtender>
            <!--      -->
            <asp:Panel ID="pnPopup" runat="server" CssClass="PanelPopup" Width="35%" Height="30%"
                Style="display: none">
                <table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style2" align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMsgResult" runat="server" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnOK" runat="server" CausesValidation="False" Text="OK" Width="100px" OnClick="btnOK_Click" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                OnClick="btnCancel_Click" Text="Close" Visible="False" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- ----------------------------->

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdfUserLogin" runat="server" />
</asp:Content>
