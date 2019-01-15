<%@ Page Title="Performance Appraisal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompletePerformanceAppraisal.aspx.cs" Inherits="PerformanceAppraisalProj.Form.CompletePerformanceAppraisal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        .auto-style6 {
            height: 21px;
            width: 190px;
        }

        .auto-style9 {
        }

        .auto-style10 {
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

        .auto-style14 {
            text-align: center;
        }

        .auto-style17 {
            text-align: center;
            width: 480px;
        }

        .auto-style18 {
            width: 480px;
        }

        .auto-style20 {
            width: 408px;
        }

        .auto-style24 {
            width: 293px;
        }

        .auto-style27 {
            width: 145px;
        }

        .auto-style28 {
            height: 21px;
            width: 145px;
        }
    </style>

    <script type="text/javascript">
        function CloseWindow() {
            window.close();
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
                    <td class="auto-style27">
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
                    <td class="auto-style27">
                        <asp:Label ID="Label6" runat="server" Text="Date Joined:"></asp:Label>
                    </td>
                    <td>

                        <asp:Label ID="lblJoinDate" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label45" runat="server" Text="Contract Start:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblContractStart" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style27">
                        <asp:Label ID="Label46" runat="server" Text="Contract End:"></asp:Label>
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
                        <asp:Label ID="Label9" runat="server" Text="Appraisal DocNo.:"></asp:Label>
                    </td>
                    <td class="auto-style9" colspan="3">
                        <asp:Label ID="lblAppraisalDocNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label14" runat="server" Text="Appraisal Period:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="Label15" runat="server" Text="From:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style27">
                        <asp:Label ID="Label16" runat="server" Text="To:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateTo" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label7" runat="server" Text="Date of Appraisal:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblAppraisalDate" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style27">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label41" runat="server" Text="First Manager Name:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lbl1StManagerName" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style27">
                        <asp:Label ID="Label42" runat="server" Text="Second Manager Name:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl2StManagerName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10"><strong>
                        <asp:Label ID="Label40" runat="server" Text="Appraisal Status:"></asp:Label>
                    </strong></td>
                    <td colspan="3">
                        <asp:Label ID="lblAppraisalStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10"><strong>
                        <asp:Label ID="Label13" runat="server" Text="Action History:"></asp:Label>
                    </strong></td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style27">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvActionHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                            EmptyDataText="No data found." EnableViewState="true" ForeColor="#333333" GridLines="Vertical" 
                            OnPageIndexChanging="gvActionHistory_PageIndexChanging" PageSize="5" ShowHeaderWhenEmpty="True" Width="100%" 
                            OnRowDataBound="gvActionHistory_RowDataBound">
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

                               <%-- <asp:BoundField DataField="Comment" HeaderText="Comment">
                                    <ItemStyle Width="40%" />
                                </asp:BoundField>--%>

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
                        <asp:Label ID="Label17" runat="server" Font-Size="Medium" Text="Part I:  Working Performance (ผลการปฏิบัติงาน)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label18" runat="server" Text="ส่วนที่ 1 ผลการปฏบัติงาน (ผู้บังคับบัญชาประเมินในช่อง Result ทั้งนี้ระบบจะแปลงค่าผลการประเมินออกมาเป็นคะแนนตามค่าน้ำหนักในแต่ละข้อ)">
                        </asp:Label>
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
                    <td class="auto-style6"></td>
                    <td class="auto-style28"></td>
                    <td class="auto-style2"></td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="4">
                        <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="Part II: Working Behavior (พฤติกรรมการทำงาน)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="4">
                        <asp:Label ID="Label19" runat="server" Text="ส่วนที่ 2 พฤติกรรมการทำงาน (ผู้บังคับบัญชาประเมินในช่อง Result ทั้งนี้ระบบจะแปลงค่าผลการประเมินออกมาเป็นคะแนนตามค่าน้ำหนักในแต่ละข้อ)">
                        </asp:Label>
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
                        <asp:Label ID="Label11" runat="server" Text="Employee's strenghts (จุดเด่นของพนักงาน):"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmpStrength" runat="server" MaxLength="200" Rows="3"
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
                        <asp:TextBox ID="txtEmpImpovement" runat="server" MaxLength="200" Rows="3"
                            TextMode="MultiLine" Width="100%" ReadOnly="True" CssClass="TextArea"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style27">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label39" runat="server" Text="Overall score:"></asp:Label>
                    </td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style27">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <!-- Table 1 -->
                        <table style="border-collapse: collapse; border: 1px solid black;" width="100%">
                            <tr>
                                <td colspan="2" style="background-color: lavender; border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="Label31" runat="server" Font-Bold="True" Text="ค่าน้ำหนัก"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Text="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style17">
                                    <asp:Label ID="Label25" runat="server" Text="รายการ"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="Label26" runat="server" Text="คะแนนเต็ม"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="Label27" runat="server" Text="คะแนนที่ได้"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style18">
                                    <asp:Label ID="Label28" runat="server" Text="Part I:  Working Performance (ผลการปฏิบัติงาน)"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblTotalPart1" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblActualPart1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style18">
                                    <asp:Label ID="Label29" runat="server" Text="Part II: Working Behavior (พฤติกรรมการทำงาน)"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblTotalPart2" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblActualPart2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid black;" class="auto-style18">
                                    <asp:Label ID="Label30" runat="server" Text="คะแนนรวม"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblTotalPoint" runat="server"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblActualPoint" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <!-- Table 2 -->
                        <table style="border-collapse: collapse;" width="100%">
                            <tr>
                                <td class="auto-style20"></td>
                                <td class="auto-style24">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Text="เกณฑ์การสรุปผลการปฏิบัติงาน"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14"><strong>
                                    <asp:Label ID="Label36" runat="server" Text="สรุปผลการปฏิบัติงาน"></asp:Label>
                                </strong></td>
                            </tr>
                            <tr>
                                <td class="auto-style20">&nbsp;</td>
                                <td class="auto-style24">
                                    <asp:Label ID="Label38" runat="server" Text="มากกว่า 85 คะแนน = Band 4"></asp:Label>
                                </td>
                                <td rowspan="3" style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblResultScore" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style20"></td>
                                <td class="auto-style24">
                                    <asp:Label ID="Label33" runat="server" Text="มากกว่า 70 คะแนนถึง 85 คะแนน = Band 3"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style20">&nbsp;</td>
                                <td class="auto-style24">
                                    <asp:Label ID="Label34" runat="server" Text="มากกว่า 55 คะแนนถึง 70 คะแนน = Band 2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style20">&nbsp;</td>
                                <td class="auto-style24">
                                    <asp:Label ID="Label35" runat="server" Text="น้อยกว่า/เท่ากับ 55 คะแนน = Band 1"></asp:Label>
                                </td>
                                <td style="border: 1px solid black;" class="auto-style14">
                                    <asp:Label ID="lblResultText" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label44" runat="server" Font-Bold="True" Text="Attachment :"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvAttachFile" runat="server" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="false"
                            CssClass="GridView2" EmptyDataText="No data found" EnableViewState="true" ForeColor="#3333333" GridLines="Both"
                            OnRowDataBound="gvAttachFile_RowDataBound" ShowHeader="true" ShowHeaderWhenEmpty="true" Width="70%"
                            OnPageIndexChanging="gvAttachFile_PageIndexChanging" PageSize="5">
                            <AlternatingRowStyle BackColor="#f9f9f9" ForeColor="Black" />
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="4" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" Font-Bold="false" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" ItemStyle-Font-Size="12px"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%" />
                                <asp:TemplateField>
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" Width="10%" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False"
                                            CommandArgument='<%#Eval("Attachment1")%>' CommandName="Download" 
                                            OnClick="lnkDownload_Click" Text="Download" />
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
                    <td colspan="2">
                        &nbsp;</td>
                    <td class="auto-style27">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnBack" runat="server" OnClientClick="window.close(); return false;" Text="Close" Width="100px" />
                        &nbsp;</td>
                    <td class="auto-style27">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdfEmployeeID" runat="server" />
    <asp:HiddenField ID="hdfUserLogin" runat="server" />
    <asp:HiddenField ID="hdfAppraisalDocNo" runat="server" />
    <asp:HiddenField ID="hdfAppraisalYear" runat="server" />
    <asp:HiddenField ID="hdfEmployeeType" runat="server" />
</asp:Content>
