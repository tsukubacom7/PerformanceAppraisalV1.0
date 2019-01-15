<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PerformanceAppraisalProj.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%--<script type = "text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <asp:UpdatePanel ID="updGrid" UpdateMode="Conditional" runat="server">
        <ContentTemplate>

            <h2>Main Menu</h2>
            <p></p>
            <%--
     <p>
        <asp:LinkButton ID="lbtnTaskList" runat="server" OnClick="lbtnTaskList_Click">- Task List</asp:LinkButton>
        <br />
        <asp:LinkButton ID="lbtnSelEmployee" runat="server" OnClick="lbtnSelEmployee_Click">- Create Appraisal Employee</asp:LinkButton>
        <br />
        <asp:LinkButton ID="lblEmployeeMgt" runat="server" OnClick="lblEmployeeMgt_Click">- Appraisal History</asp:LinkButton>
        <br />
        <asp:LinkButton ID="lbtnPrintForm" runat="server" OnClick="lbtnPrintForm_Click">- Print Appraisal Form</asp:LinkButton>
        <br />
        <asp:LinkButton ID="lbtnReportSummary" runat="server" OnClick="lbtnReportSummary_Click">- Performance Appraisal Summary</asp:LinkButton>       
    </p>
            --%>
            <div id="TaskList">
                <asp:LinkButton ID="lbtnTaskList" runat="server" OnClick="lbtnTaskList_Click">- Task List</asp:LinkButton>
            </div>
            <div id="SelEmployee">
                <asp:LinkButton ID="lbtnSelEmployee" runat="server" OnClick="lbtnSelEmployee_Click">- Create Appraisal Employee</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="lblEmployeeMgt" runat="server" OnClick="lblEmployeeMgt_Click">- Appraisal History</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="lbtnPrintForm" runat="server" OnClick="lbtnPrintForm_Click">- Print Appraisal Form</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="lbtnReportSummary" runat="server" OnClick="lbtnReportSummary_Click">- Performance Appraisal Summary</asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
