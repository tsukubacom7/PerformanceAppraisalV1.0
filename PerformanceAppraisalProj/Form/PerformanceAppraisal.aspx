<%@ Page Title="Performance Appraisal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PerformanceAppraisal.aspx.cs" Inherits="PerformanceAppraisalProj.Form.PerformanceAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        .auto-style6 {
            height: 21px;
            width: 190px;
        }

        .auto-style7 {
            width: 92px;
        }

        .auto-style8 {
            height: 21px;
            width: 92px;
        }

        .TextAreaRemark {
            font-size: 1em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }
         .TextArea {
            font-size: 1em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            background-color: #e6e2e2;
        }

        .auto-style9 {
            width: 190px;
        }

        .auto-style10 {
        }


        .auto-style25 {
            width: 453px;
        }

        .auto-style28 {
            text-align: center;
        }

        .auto-style30 {
            width: 422px;
        }

        .auto-style31 {
            width: 267px;
        }

        .failureNotification {
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            font-size: 1.0em;
            color: Red;
        }
        .auto-style32 {
            width: 61px;
        }
        .auto-style33 {
            height: 29px;
        }
    </style>

   <script type="text/javascript">
        function validateLimit(obj, divID, maxchar) {
            objDiv = get_object(divID);

            if (this.id) obj = this;

            var remaningChar = maxchar - trimEnter(obj.value).length;

            if (objDiv.id) {
                objDiv.innerHTML = remaningChar + " characters left";
            }
            if (remaningChar <= 0) {
                obj.value = obj.value.substring(maxchar, 0);
                if (objDiv.id) {
                    objDiv.innerHTML = "0 characters left";
                }
                return false;
            }
            else { return true; }
        }

        function get_object(id) {
            var object = null;
            if (document.layers) {
                object = document.layers[id];
            } else if (document.all) {
                object = document.all[id];
            } else if (document.getElementById) {
                object = document.getElementById(id);
            }
            return object;
        }

        function trimEnter(dataStr) {
            return dataStr.replace(/(\r\n|\r|\n)/g, "");
        }

        function validateRemark(obj, maxchar)
        { 
            if (this.id) obj = this;

            var remaningChar = maxchar - trimEnter(obj.value).length;          
            if (remaningChar <= 0) {
                obj.value = obj.value.substring(maxchar, 0);
                return false;
            }
            else {
                return true;
            }
        }      
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table class="auto-style1">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label8" runat="server" Text="Performance Appraisal for periodical contract" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label3" runat="server" Text="Name of Employee:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Label4" runat="server" Text="Position:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPosition" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label5" runat="server" Text="Department/Devision:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Label6" runat="server" Text="Date Joined:"></asp:Label>
                    </td>
                    <td>

                        <asp:Label ID="lblJoinDate" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label15" runat="server" Text="Contract Start:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblContractStart" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Label16" runat="server" Text="Contract End:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblContractEnd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label7" runat="server" Text="Appraisal Period:"></asp:Label>
                    </td>
                    <td colspan="2" style="vertical-align: middle; text-align: left">
                        <asp:Label ID="Label13" runat="server" Text="From:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; text-align: left">
                        <asp:Label ID="Label14" runat="server" Text="To:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateTo" runat="server"></asp:Label>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4"><strong>
                        <asp:Label ID="Label40" runat="server" Text="Performance Appraisal History:"></asp:Label>
                    </strong></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                            EmptyDataText="No data found." EnableViewState="true" ForeColor="#333333" GridLines="Vertical" 
                            OnPageIndexChanging="gvEmployee_PageIndexChanging" OnRowDataBound="gvEmployee_RowDataBound" 
                            PageSize="5" ShowHeaderWhenEmpty="True" Width="100%">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" Font-Size="12px" ForeColor="White" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompanyID" HeaderText="Company">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Position" HeaderText="Position">
                                    <ItemStyle Width="23%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AppraisalDate" DataFormatString="{0:dd\/MM\/yyyy}"
                                    HeaderText="Appraisal Date" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" 
                                    ItemStyle-Width="12%" />
                                <asp:TemplateField HeaderText="Link">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfEmployeeID" runat="server" Value='<%# Eval("EmployeeID") %>' />
                                        <asp:HiddenField ID="hdfAppraisalDate" runat="server" Value='<%# Eval("AppraisalDate") %>' />
                                        <asp:HyperLink ID="hplClick" runat="server" Target="_blank"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
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
                    <td colspan="4"><strong>
                        <asp:Label ID="Label17" runat="server" Text="Action History:"></asp:Label>
                    </strong></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvActionHistory" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" EmptyDataText="No data found." EnableViewState="true" 
                            ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvActionHistory_PageIndexChanging" 
                            PageSize="5" ShowHeaderWhenEmpty="True" Width="100%" OnRowDataBound="gvActionHistory_RowDataBound">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" Font-Size="12px" ForeColor="White" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                 <asp:BoundField DataField="UserID" HeaderText="CreatedBy">
                                    <ItemStyle HorizontalAlign="Left" Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Action" HeaderText="Action">
                                    <ItemStyle Width="20%" />
                                </asp:BoundField>                             

                                 <asp:TemplateField HeaderText="Comment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("Comment") %>' 
                                            TextMode="MultiLine" Rows="3" ReadOnly="true" 
                                            CssClass="TextArea" Width="80%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="TransactionDate" DataFormatString="{0:dd\/MM\/yyyy hh:mm:ss}" 
                                    HeaderText="Date/Time" HtmlEncode="false" 
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />

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
                    <td colspan="4">&nbsp; 
                        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                            ValidationGroup="RegisterUserValidationGroup" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" Text="Part I:  Working Performance (ผลการปฏิบัติงาน)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label18" runat="server" Text="ส่วนที่ 1 ผลการปฏบัติงาน (ผู้บังคับบัญชาประเมินในช่อง Result ทั้งนี้ระบบจะแปลงค่าผลการประเมินออกมาเป็นคะแนนตามค่าน้ำหนักในแต่ละข้อ)"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label22" runat="server" Text="5 = ดีเยี่ยม/ 4 = ดีมาก/ 3 = ดี /2 = พอใช้/ 1 = ต้องปรับปรุง"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvAppraisalPart1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="No data found." ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True"
                            Width="100%" OnRowCommand="gvAppraisalPart1_RowCommand"
                            OnRowDataBound="gvAppraisalPart1_RowDataBound" EnableViewState="true">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" Font-Size="12px" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="No." DataField="QuestionLineNo" ItemStyle-Width="5%">
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Criteria" DataField="QuestionDesc" ItemStyle-Width="15%">
                                    <ItemStyle Width="36%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Result">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPart1Result" runat="server" Width="90%" CssClass="TextAreaRemark"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlPart1Result_SelectedIndexChanged1">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfPart1QuestionDesc" runat="server" Value='<%# Eval("QuestionDesc") %>' />
                                        <asp:HiddenField ID="hdfPart1QuestionLineNo" runat="server" Value='<%# Eval("QuestionLineNo") %>' />
                                        <asp:HiddenField ID="hdfPart1QuestionType" runat="server" Value='<%# Eval("QuestionType") %>' />
                                        <asp:HiddenField ID="hdfPart1QuestionWeight" runat="server" Value='<%# Eval("QuestionWeight") %>' />
                                        <asp:Label ID="lblPart1Score" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPart1Comment" runat="server" CssClass="TextAreaRemark"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" HorizontalAlign="Center" />
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
                    <td class="auto-style4"></td>
                    <td class="auto-style6"></td>
                    <td class="auto-style8"></td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="4">
                        <asp:Label ID="Label2" runat="server" Text="Part II: Working Behavior (พฤติกรรมการทำงาน)" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="4">
                        <asp:Label ID="Label19" runat="server" Text="ส่วนที่ 2 พฤติกรรมการทำงาน (ผู้บังคับบัญชาประเมินในช่อง Result ทั้งนี้ระบบจะแปลงค่าผลการประเมินออกมาเป็นคะแนนตามค่าน้ำหนักในแต่ละข้อ)"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="4">
                        <asp:Label ID="Label21" runat="server" Text="5 = ดีเยี่ยม/ 4 = ดีมาก/ 3 = ดี /2 = พอใช้/ 1 = ต้องปรับปรุง"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvAppraisalPart2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="No data found." ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True"
                            Width="100%" OnRowDataBound="gvAppraisalPart2_RowDataBound">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" Font-Size="12px" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField HeaderText="No." DataField="QuestionLineNo" ItemStyle-Width="5%">
                                    <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Criteria" DataField="QuestionDesc" ItemStyle-Width="15%">
                                    <ItemStyle Width="36%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Result">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPart2Result" runat="server" Width="90%" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlPart2Result_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdfPart2QuestionDesc" runat="server" Value='<%# Eval("QuestionDesc") %>' />
                                        <asp:HiddenField ID="hdfPart2QuestionLineNo" runat="server" Value='<%# Eval("QuestionLineNo") %>' />
                                        <asp:HiddenField ID="hdfPart2QuestionType" runat="server" Value='<%# Eval("QuestionType") %>' />
                                        <asp:HiddenField ID="hdfPart2QuestionWeight" runat="server" Value='<%# Eval("QuestionWeight") %>' />
                                        <asp:Label ID="lblPart2Score" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPart2Comment" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" HorizontalAlign="Center" />
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
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" 
                            Text="Calculate Score" Width="110px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label39" runat="server" Text="Overall score:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <!-- Table 1 -->
                        <table style="border-collapse: collapse; border: 1px solid black;" width="100%">
                            <tr>
                                <td colspan="2" style="background-color: lavender; border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="Label31" runat="server" Font-Bold="True" Text="ค่าน้ำหนัก"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Text="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="Label25" runat="server" Text="รายการ"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="Label26" runat="server" Text="คะแนนเต็ม"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="Label27" runat="server" Text="คะแนนที่ได้"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;">
                                    <asp:Label ID="Label28" runat="server" Text="Part I:  Working Performance (ผลการปฏิบัติงาน)"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblTotalPart1" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblActualPart1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style25">
                                    <asp:Label ID="Label29" runat="server" Text="Part II: Working Behavior (พฤติกรรมการทำงาน)"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblTotalPart2" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblActualPart2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style25">
                                    <asp:Label ID="Label30" runat="server" Text="คะแนนรวม"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblTotalPoint" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblActualPoint" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <!-- Table 2 -->
                        <table style="border-collapse: collapse;" width="100%">
                            <tr>
                                <td class="auto-style30"></td>
                                <td class="auto-style31">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Text="เกณฑ์การสรุปผลการปฏิบัติงาน"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28"><strong>
                                    <asp:Label ID="Label36" runat="server" Text="สรุปผลการปฏิบัติงาน"></asp:Label>
                                </strong></td>
                            </tr>
                            <tr>
                                <td class="auto-style30">&nbsp;</td>
                                <td class="auto-style31">
                                    <asp:Label ID="Label38" runat="server" Text="มากกว่า 85 คะแนน = Band 4"></asp:Label>
                                </td>
                                <td rowspan="3" style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblResultScore" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style30"></td>
                                <td class="auto-style31">
                                    <asp:Label ID="Label33" runat="server" Text="มากกว่า 70 คะแนนถึง 85 คะแนน = Band 3"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style30">&nbsp;</td>
                                <td class="auto-style31">
                                    <asp:Label ID="Label34" runat="server" Text="มากกว่า 55 คะแนนถึง 70 คะแนน = Band 2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style30">&nbsp;</td>
                                <td class="auto-style31">
                                    <asp:Label ID="Label35" runat="server" Text="น้อยกว่า/เท่ากับ 55 คะแนน = Band 1"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style28">
                                    <asp:Label ID="lblResultText" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label11" runat="server" Text="Employee's strenghts (จุดเด่นของพนักงาน):"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmpStrength" runat="server" CssClass="TextAreaRemark"
                            MaxLength="250" Rows="3" TextMode="MultiLine" Width="100%"
                            ToolTip="Maximum limit 250 characters." onkeyup="return validateRemark(this, 250)"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label12" runat="server"
                            Text="Employee's areas of improvement (จุดที่ควรพัฒนาหรือปรับปรุงของพนักงาน):"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmpImpovement" runat="server" CssClass="TextAreaRemark"
                            MaxLength="250" Rows="3" TextMode="MultiLine" Width="100%"
                            ToolTip="Maximum limit 250 characters." onkeyup="return validateRemark(this, 250)"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label44" runat="server" Font-Bold="True" Text="Attachment :"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style32">
                                    <asp:Label ID="Label46" runat="server" Text="Select file:"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuAttachment" runat="server" Width="400px" />
                                    &nbsp;
                                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload file" />
                                    &nbsp;
                                    <asp:Label ID="Label47" runat="server" Text="* สามารถอัพโหลดไฟล์ได้ไม่เกิน 5 ไฟล์"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                        CssClass="GridView2" ForeColor="#333333" OnRowCommand="gvAttachFile_RowCommand" 
                                        OnRowDataBound="gvAttachFile_RowDataBound" OnRowDeleting="gvAttachFile_RowDeleting" 
                                        ShowHeaderWhenEmpty="True" Width="70%" EmptyDataText="Select file" PageSize="5" OnPageIndexChanging="gvAttachFile_PageIndexChanging">
                                        <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="FileName" HeaderText="File Name" ItemStyle-Font-Size="12px"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" Width="50%" />
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemStyle Font-Size="12px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblDelete" runat="server"
                                                         CommandArgument='<%#Eval("AttachFilePath") + ";" +Eval("EmployeeID")%>'  
                                                        CommandName="Delete" Text="Delete" />
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
                        </table>
                    </td>
                </tr>
                <caption>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Width="100px" />
                            &nbsp; <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="RegisterUserValidationGroup" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="Label10" runat="server" Text="Approver's comment:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="TextAreaRemark" MaxLength="250" 
                                onkeyup="return validateRemark(this, 250)" Rows="3" TextMode="MultiLine" 
                                ToolTip="Maximum limit 250 characters." Width="100%"> </asp:TextBox>
                        </td>
                    </tr>
                </caption>
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
                                         <asp:CustomValidator runat="server" ID="ValidateTxt" ValidationGroup="RegisterUserValidationGroup"
                                             CssClass="failureNotification" OnServerValidate="ValidateTxt_ServerValidate">*</asp:CustomValidator>
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
            <!-- --------------------------- -->


            <!-- Modal popup "Popup Message Warning" -->
            <asp:LinkButton ID="lbtnPopupErrCtrl" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargerErrCtrl" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="lbtnPopupErr_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupErrCtrl" DynamicServicePath="" Enabled="True" PopupControlID="pnMsgError"
                TargetControlID="lbtnTargerErrCtrl">
            </cc1:ModalPopupExtender>
            <!--       -->
            <asp:Panel ID="pnMsgError" runat="server" CssClass="PanelPopup" Width="35%" Height="30%"
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
                                    <td align="center">
                                        <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Size="Small">Warning</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblWarningMsg" runat="server" Font-Size="Small">Worklist item could not be opened. Because task is already approved.</asp:Label>
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
                            <asp:Button ID="btnCloseMsgErr" runat="server" CausesValidation="False"
                                OnClick="btnCloseMsgErr_Click" Text="Close" Visible="true" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- --------------------------- -->

            <!-- Modal popup "Popup Message File Upload Warning" -->
            <asp:LinkButton ID="lbtnPopupFUWarning" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargetPopupFUWarning" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="lbtnPopupFuErr_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupFUWarning" DynamicServicePath="" Enabled="True" PopupControlID="pnFuMsgWarning"
                TargetControlID="lbtnTargetPopupFUWarning">
            </cc1:ModalPopupExtender>
            <!--       -->
            <asp:Panel ID="pnFuMsgWarning" runat="server" CssClass="PanelPopup" Width="40%" Height="30%"
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
                                    <td align="center">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Small">Warning</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblFuWarning" runat="server" Font-Size="Small">Could not upload. File exceeds maximum allowed size of 5MB.</asp:Label>
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
                            <asp:Button ID="btnFuClose" runat="server" CausesValidation="False"
                                OnClick="btnFuClose_Click" Text="Close" Visible="true" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- --------------------------- -->


             <!-- Modal popup "Popup Message Warning Employee" -->
            <asp:LinkButton ID="lbtnPopupEmployee" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lbtnTargetPopupEmployee" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="lbtnPopupEmployee_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="lbtnPopupEmployee" DynamicServicePath="" Enabled="True" PopupControlID="pnPopupEmployee"
                TargetControlID="lbtnTargetPopupEmployee">
            </cc1:ModalPopupExtender>
            <!--   -->
            <asp:Panel ID="pnPopupEmployee" runat="server" CssClass="PanelPopup" Width="85%" Height="75%"
                 Style="display: none">
                <table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="auto-style33" align="left">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">
                                        <asp:Label ID="lblPUSubject" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label23" runat="server" Font-Size="Small">ทาง HR ขอแจ้งพนักงาน Periodical Contract ในสังกัดของท่าน ครบกำหนดสัญญาจ้าง มีรายละเอียด ดังนี้</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:GridView ID="gvPopEmployee" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" CssClass="GridView2" EmptyDataText="Select file"
                                            ForeColor="#333333" OnRowDataBound="gvPopEmployee_RowDataBound" PageSize="5"
                                            ShowHeaderWhenEmpty="True" Width="100%">
                                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Name-Surname"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%"></asp:BoundField>
                                                <asp:BoundField DataField="Position" HeaderText="Position"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"></asp:BoundField>
                                                <asp:BoundField DataField="Company" HeaderText="Company"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%"></asp:BoundField>
                                                <asp:BoundField DataField="DateHired" HeaderText="Date Hired"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"></asp:BoundField>
                                                <asp:BoundField DataField="DateEnded" HeaderText="Date Ended"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%"></asp:BoundField>
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
                                    <td align="center" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label48" runat="server" Font-Size="Small" Style="text-align: left">ในการนี้จึงขอให้ท่านพิจารณาว่าจะมีความประสงค์จะต่อสัญญาพนักงานดังกล่าวหรือไม่  หากต้องการจะต่อสัญญาจ้างต่อไป ขอให้ท่านส่งบันทึกภายในซึ่งได้รับการอนุมัติจากผู้มีอำนาจ ส่งมายัง คุณธวัชชัย ก่อนวันที่จะครบสัญญาเพื่อดำเนินการในขั้นตอนต่อไป</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label49" runat="server" Font-Size="Small">ทั้งนี้ ขอให้ท่านประเมินผลการปฏิบัติงานของพนักงานเพื่อประกอบการพิจารณาการต่อสัญญาจ้างในครั้งนี้ด้วย</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label50" runat="server" Font-Size="Small"> จึงเรียนมาเพื่อทราบและโปรดพิจารณาดำเนินการ</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label51" runat="server" Font-Size="Small">หากมีข้อสงสัยเพิ่มเติม กรุณาติดต่อ คุณธวัชชัย </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="center" style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label52" runat="server" Font-Size="Small">ฝ่ายทรัพยากรบุคคล</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnCloseEmp" runat="server" CausesValidation="False" OnClick="btnCloseEmp_Click" Text="OK" Visible="true" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- --------------------------- -->

            <asp:HiddenField ID="hdfEmployeeID" runat="server" />
            <asp:HiddenField ID="hdfCompanyID" runat="server" />
            <asp:HiddenField ID="hdfUserLogin" runat="server" />
            <asp:HiddenField ID="hdfSecondManager" runat="server" />
            <asp:HiddenField ID="hdfAppraisalDocNo" runat="server" />
            <asp:HiddenField ID="hdfFirstManager" runat="server" />
            <asp:HiddenField ID="hdfFirstManagerMail" runat="server" />
            <asp:HiddenField ID="hdfSecondManagerMail" runat="server" />
            <asp:HiddenField ID="hdfGrade" runat="server" />
            <asp:HiddenField ID="hdfEmployeeType" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOK" />
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
