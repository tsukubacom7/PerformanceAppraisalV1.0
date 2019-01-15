<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" 
    Inherits="PerformanceAppraisalProj.Unauthorized" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            font-size: 1.1em;
            display: block;
            text-align: right;
            padding: 10px;
            color: White;
            height: 26px;
        }

        .auto-style2 {
            height: 60px;
        }

        .auto-style3 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="page">
            <div class="header">
                <div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ebidding2.jpg" Width="100%" />
                </div>
                <div class="title">
                    <h1>Gulf - Performance Appraisal
                    </h1>
                </div>
                <div class="auto-style1">
                </div>
            </div>
            <div class="main">
                <p style="font-family: Arial; font-size: 14px">
                    You are not have permission for this page. Please contact administrator. 
                </p>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="footer">
        </div>
    </form>
</body>
</html>
