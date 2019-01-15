<%@ Page Title="Performance Appraisal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerformanceAppraisalDetail.aspx.cs" Inherits="PerformanceAppraisalProj.Form.PerformanceAppraisalDetail" %>

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

        .auto-style7 {
            width: 92px;
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
            width: 132px;
        }
    </style>
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
                        <asp:Label ID="Label12" runat="server" Text="Contract Start:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblContractStart" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Label13" runat="server" Text="Contract End:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblContractEnd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label23" runat="server" Text="Appraisal Period:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="Label24" runat="server" Text="From:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Label14" runat="server" Text="To:"></asp:Label>
                        &nbsp;<asp:Label ID="lblDateTo" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label9" runat="server" Text="Appraisal DocNo.:"></asp:Label>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblDocNo" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <strong>
                            <asp:Label ID="Label40" runat="server" Text="Appraisal Status:"></asp:Label>
                        </strong>
                    </td>
                    <td class="auto-style9">
                        <asp:Label ID="lblApprovalStatus" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10"><strong>
                        <asp:Label ID="Label10" runat="server" Text="Route Approver:"></asp:Label>
                        </strong></td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblCreator" runat="server"></asp:Label>
                        &nbsp;--&gt;
                        <asp:Label ID="lblFirstManager" runat="server"></asp:Label>
                        &nbsp;--&gt;
                        <asp:Label ID="lblSecondManager" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <strong>
                            <asp:Label ID="Label11" runat="server" Text="Action History:"></asp:Label>
                        </strong>
                    </td>
                    <td class="auto-style9">&nbsp;</td>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvActionHistory" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="No data found." EnableViewState="true" ForeColor="#333333" GridLines="Vertical"
                            PageSize="5" ShowHeaderWhenEmpty="True" Width="100%" 
                            OnPageIndexChanging="gvActionHistory_PageIndexChanging" 
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
                                
                                  <asp:TemplateField HeaderText="Comment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtComments" runat="server" Text='<%# Eval("Comment") %>' 
                                            TextMode="MultiLine" Rows="3" ReadOnly="true" 
                                            CssClass="TextArea" Width="80%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="TransactionDate" HeaderText="Date/Time" ItemStyle-Width="15%"
                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd\/MM\/yyyy hh:mm:ss}" HtmlEncode="false" />
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
                    <td class="auto-style9">
                        <asp:HiddenField ID="hdfAppraisalDocNo" runat="server" />
                        <asp:HiddenField ID="hdfFirstManager" runat="server" />
                        <asp:HiddenField ID="hdfSecondManager" runat="server" />
                         <asp:HiddenField ID="hdfFirstManagerMail" runat="server" />
                    </td>
                    <td class="auto-style7">
                        <asp:HiddenField ID="hdfEmployeeID" runat="server" />
                    </td>
                    <td>
                        <asp:HiddenField ID="hdfUserLogin" runat="server" />
                        <asp:HiddenField ID="hdfAppraisalStatus" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Width="100px" />
                        &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Create Appraisal" Width="120px" />
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
            <!--     -->
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
</asp:Content>
