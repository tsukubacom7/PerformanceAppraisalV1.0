<%@ Page Title="Performance Appraisal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerformanceAppraisalReview.aspx.cs" Inherits="PerformanceAppraisalProj.Form.PerformanceAppraisalReview" %>

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
            width: 118px;
        }

        .auto-style8 {
            height: 21px;
            width: 90px;
        }

        .TextArea {
            font-size: 1em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
            background-color: #e6e2e2;
        }

        .TextAreaRemark {
            font-size: 1em;
            font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
        }

        .auto-style13 {
            height: 21px;
            width: 158px;
        }

        .auto-style14 {
            width: 118px;
        }

        .auto-style15 {
            width: 90px;
        }

        .auto-style16 {
            width: 158px;
        }

        .auto-style11 {
            text-align: center;
        }

        .auto-style18 {
            text-align: center;
            height: 21px;
        }

        .TableCollaps {
            border-collapse: collapse;
        }

        .TableBorder {
            border: 1px solid black;
        }

        .auto-style19 {
            height: 23px;
            width: 453px;
        }

        .auto-style20 {
            text-align: center;
            height: 23px;
        }


        .auto-style21 {
            text-align: center;
            width: 453px;
        }

        .auto-style24 {
            text-align: center;
            height: 23px;
            width: 189px;
        }

        .auto-style25 {
            width: 453px;
        }

        .auto-style26 {
            text-align: center;
            width: 189px;
        }


        .auto-style27 {
            width: 379px;
        }

        .auto-style28 {
            width: 379px;
            height: 21px;
        }

        .auto-style29 {
            height: 21px;
            width: 266px;
        }

        .auto-style30 {
            width: 266px;
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
                    <td class="auto-style14">
                        <asp:Label ID="Label3" runat="server" Text="Name of Employee:"></asp:Label>
                    </td>
                    <td class="auto-style16">
                        <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style15">
                        <asp:Label ID="Label4" runat="server" Text="Position:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPosition" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style14">
                        <asp:Label ID="Label5" runat="server" Text="Department/Devision:"></asp:Label>
                    </td>
                    <td class="auto-style16">
                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style15">
                        <asp:Label ID="Label6" runat="server" Text="Date Joined:"></asp:Label>
                    </td>
                    <td>

                        <asp:Label ID="lblJoinDate" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style14">&nbsp;</td>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style14">
                        <asp:Label ID="Label23" runat="server" Text="Appraisal Period:"></asp:Label>
                    </td>
                    <td class="auto-style16">
                        <asp:Label ID="Label24" runat="server" Text="From:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style15">
                        <asp:Label ID="Label14" runat="server" Text="To:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateTo" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style14">
                        <asp:Label ID="Label9" runat="server" Text="Appraisal DocNo.:"></asp:Label>
                    </td>
                    <td class="auto-style16">
                        <asp:Label ID="lblAppraisalDocNo" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4"><strong>
                        <asp:Label ID="Label40" runat="server" Text="Performance Appraisal History:"></asp:Label>
                    </strong></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvEmployee" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No data found." EnableViewState="true" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvEmployee_PageIndexChanging" OnRowDataBound="gvEmployee_RowDataBound" PageSize="5" ShowHeaderWhenEmpty="True" Width="100%">
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
                                    HeaderText="Appraisal Date" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%" />
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
                    <td class="auto-style14"><strong>
                        <asp:Label ID="Label13" runat="server" Text="Action History:"></asp:Label>
                    </strong></td>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvActionHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No data found." EnableViewState="true" ForeColor="#333333" GridLines="Vertical" OnPageIndexChanging="gvActionHistory_PageIndexChanging" PageSize="5" ShowHeaderWhenEmpty="True" Width="100%" OnRowDataBound="gvActionHistory_RowDataBound">
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
                                    HeaderText="Date/Time" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" />
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
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" Text="Part I:  Working Performance (ผลการปฏิบัติงาน)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label18" runat="server" Text="ส่วนที่ 1 ผลการปฏบัติงาน (ผู้บังคับบัญชาประเมินในช่อง Result ทั้งนี้ระบบจะแปลงค่าผลการประเมินออกมาเป็นคะแนนตามค่าน้ำหนักในแต่ละข้อ)"></asp:Label>
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
                            EmptyDataText="No data found." ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="True"
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
                                <asp:BoundField HeaderText="Result" DataField="Score">
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Score" DataField="CalculatedScore">
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Comment" DataField="Remark">
                                    <ItemStyle Width="30%" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
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
                    <td class="auto-style13"></td>
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
                        <asp:Label ID="Label19" runat="server" Text="ส่วนที่ 2 พฤติกรรมการทำงาน (ผู้บังคับบัญชาประเมินในช่อง Result ทั้งนี้ระบบจะแปลงค่าผลการประเมินออกมาเป็นคะแนนตามค่าน้ำหนักในแต่ละข้อ)"></asp:Label>
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
                            EmptyDataText="No data found." ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="True"
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
                                <asp:BoundField HeaderText="Result" DataField="Score">
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Score" DataField="CalculatedScore">
                                    <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Comment" DataField="Remark">
                                    <ItemStyle Width="30%" HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
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
                        <asp:Label ID="Label39" runat="server" Text="Overall score:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="auto-style1" style="border-collapse: collapse; border: 1px solid black;">
                            <tr>
                                <td class="auto-style18" colspan="2" style="background-color: lavender; border: 1px solid black;">
                                    <asp:Label ID="Label31" runat="server" Font-Bold="True" Text="ค่าน้ำหนัก"></asp:Label>
                                </td>
                                <td class="auto-style18" style="border: 1px solid black;">
                                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Text="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style21" style="border: 1px solid black;">
                                    <asp:Label ID="Label25" runat="server" Text="รายการ"></asp:Label>
                                </td>
                                <td class="auto-style26" style="border: 1px solid black;">
                                    <asp:Label ID="Label26" runat="server" Text="คะแนนเต็ม"></asp:Label>
                                </td>
                                <td class="auto-style11" style="border: 1px solid black;">
                                    <asp:Label ID="Label27" runat="server" Text="คะแนนที่ได้"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style19" style="border: 1px solid black;">
                                    <asp:Label ID="Label28" runat="server" Text="Part I:  Working Performance (ผลการปฏิบัติงาน)"></asp:Label>
                                </td>
                                <td class="auto-style24" style="border: 1px solid black;">
                                    <asp:Label ID="lblTotalPart1" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style20" style="border: 1px solid black;">
                                    <asp:Label ID="lblActualPart1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style25">
                                    <asp:Label ID="Label29" runat="server" Text="Part II: Working Behavior (พฤติกรรมการทำงาน)"></asp:Label>
                                </td>
                                <td class="auto-style26" style="border: 1px solid black;">
                                    <asp:Label ID="lblTotalPart2" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style11" style="border: 1px solid black;">
                                    <asp:Label ID="lblActualPart2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style25">
                                    <asp:Label ID="Label30" runat="server" Text="คะแนนรวม"></asp:Label>
                                </td>
                                <td class="auto-style26" style="border: 1px solid black;">
                                    <asp:Label ID="lblTotalPoint" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style11" style="border: 1px solid black;">
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
                        <table class="auto-style1" style="border-collapse: collapse;">
                            <tr>
                                <td class="auto-style28"></td>
                                <td class="auto-style29">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Text="เกณฑ์การสรุปผลการปฏิบัติงาน"></asp:Label>
                                </td>
                                <td class="auto-style18" style="border: 1px solid black;"><strong>
                                    <asp:Label ID="Label36" runat="server" Text="สรุปผลการปฏิบัติงาน"></asp:Label>
                                </strong></td>
                            </tr>
                            <tr>
                                <td class="auto-style27">&nbsp;</td>
                                <td class="auto-style30">
                                    <asp:Label ID="Label38" runat="server" Text="มากกว่า 85 คะแนน = Band 4"></asp:Label>
                                </td>
                                <td class="auto-style11" rowspan="3" style="border: 1px solid black;">
                                    <asp:Label ID="lblResultScore" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28"></td>
                                <td class="auto-style29">
                                    <asp:Label ID="Label33" runat="server" Text="มากกว่า 70 คะแนนถึง 85 คะแนน = Band 3"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style27">&nbsp;</td>
                                <td class="auto-style30">
                                    <asp:Label ID="Label34" runat="server" Text="มากกว่า 55 คะแนนถึง 70 คะแนน = Band 2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style27">&nbsp;</td>
                                <td class="auto-style30">
                                    <asp:Label ID="Label35" runat="server" Text="น้อยกว่า/เท่ากับ 55 คะแนน = Band 1"></asp:Label>
                                </td>
                                <td class="auto-style11" style="border: 1px solid black;">
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
                        <asp:TextBox ID="txtEmpStrength" runat="server" MaxLength="250" Rows="3"
                            TextMode="MultiLine" Width="100%" ReadOnly="True" CssClass="TextArea"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label12" runat="server" Text="Employee's areas of improvement (จุดที่ควรพัฒนาหรือปรับปรุงของพนักงาน):"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmpImpovement" runat="server" MaxLength="250" Rows="3"
                            TextMode="MultiLine" Width="100%" ReadOnly="True" CssClass="TextArea"></asp:TextBox>
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
                                <td>
                                    <asp:Label ID="Label46" runat="server" Text="Select file:"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuAttachment" runat="server" Width="400px" />
                                    &nbsp;
                                    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload file" />
                                    &nbsp;<asp:Label ID="Label47" runat="server" Text="* สามารถอัพโหลดไฟล์ได้ไม่เกิน 5 ไฟล์"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                                        CssClass="GridView2" ForeColor="#333333" OnRowCommand="gvAttachFile_RowCommand" 
                                        OnRowDataBound="gvAttachFile_RowDataBound" OnRowDeleting="gvAttachFile_RowDeleting" 
                                        ShowHeaderWhenEmpty="True" Width="70%" EmptyDataText="No data found" OnPageIndexChanging="gvAttachFile_PageIndexChanging" PageSize="5">
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

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%">
                                                <ItemStyle Font-Size="12px" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False"
                                                        CommandArgument='<%# Eval("AttachFilePath") %>' CommandName="Download" OnClick="lnkDownload_Click"
                                                        Text="Download" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

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
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Width="100px" />
                        &nbsp;
                        <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Text="Reject" Width="100px" />
                        &nbsp;
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Approve" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label10" runat="server" Text="Approver's comment:"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                         <asp:TextBox ID="txtRemark" runat="server" CssClass="TextAreaRemark"
                            MaxLength="250" Rows="3" TextMode="MultiLine" Width="100%"
                            ToolTip="Maximum limit 250 characters." onkeyup="return validateRemark(this, 250)">
                        </asp:TextBox>
                    </td>
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
              Style="display: none" >
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
                                        <asp:Label ID="Label7" runat="server" Font-Size="Small">Worklist item could not be opened. Because task is already approved.</asp:Label>
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
            <!--     -->
            <asp:Panel ID="pnFuMsgWarning" runat="server" CssClass="PanelPopup" Width="40%" Height="30%"
                Style="display: none" >
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
                                        <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="Small">Warning</asp:Label>
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
            <!-- ----------------------------->

            <asp:HiddenField ID="hdfEmployeeID" runat="server" />
            <asp:HiddenField ID="hdfUserLogin" runat="server" />
            <asp:HiddenField ID="hdfAppraisalDocNo" runat="server" />
            <asp:HiddenField ID="hdfFirstManagerMail" runat="server" />
            <asp:HiddenField ID="hdfFirstManager" runat="server" />
            <asp:HiddenField ID="hdfSecondManager" runat="server" />

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOK" />
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
  
</asp:Content>
