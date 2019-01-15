<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AppraisalTask.aspx.cs" Inherits="PerformanceAppraisalProj.AppraisalTask" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
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
                        <asp:Label ID="Label8" runat="server" Text="Performance Appraisal - Task list  " Font-Size="Medium"></asp:Label>
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
                                <asp:BoundField HeaderText="Employee ID" DataField="EmployeeID">
                                    <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Employee Name" DataField="EmployeeName">
                                    <ItemStyle Width="18%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Company" DataField="CompanyID">
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Department" DataField="DepartmentName">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Position" DataField="Position">
                                    <ItemStyle Width="27%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Appraisal Status" DataField="AppraisalStatus">
                                    <ItemStyle Width="17%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Link">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfFirstManager" runat="server" Value='<%# Eval("FirstManager") %>' />
                                        <asp:HiddenField ID="hdfSecondManager" runat="server" Value='<%# Eval("SecondManager") %>' />
                                        <asp:HiddenField ID="hdfEmployeeID" runat="server" Value='<%# Eval("EmployeeID") %>' />
                                        <asp:HiddenField ID="hdfHRStaff" runat="server" Value='<%# Eval("HRStaff") %>' />
                                        <asp:HiddenField ID="hdfCreatedDate" runat="server" Value='<%# Eval("CreatedDate") %>' />
                                        <asp:HiddenField ID="hdfAppraisalDocNo" runat="server" Value='<%# Eval("AppraisalDocNo") %>' />
                                        <asp:HiddenField ID="hdfCreatedBy" runat="server" Value='<%# Eval("CreatedBy") %>' />
                                        <asp:HiddenField ID="hdfResponsibility" runat="server" Value='<%# Eval("Responsibility") %>' />
                                        <asp:HiddenField ID="hdfAppraisalStatus" runat="server" Value='<%# Eval("AppraisalStatus") %>' />
                                        <asp:HyperLink ID="hplClick" runat="server"></asp:HyperLink>
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
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdfUserLogin" runat="server" />
</asp:Content>
