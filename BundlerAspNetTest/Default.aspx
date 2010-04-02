<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BundlerAspNetTest._Default" %>
<%@ Import Namespace="Bundler.Framework"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <%= Bundle.JavaScript()
                .Add("~/js/jquery-1.4.2.js")
                .Add("~/js/jquery-ui-1.8rc3.js")
                .Render("~/js/combined.js") %>
    <%= Bundle.Css()
                .Add("~/css/jquery-ui-1.8rc3.css")
                .Add("~/css/CodeThinked.css")
                .Add("~/css/testdotless.css.less")
                .WithMedia("screen")
                .Render("~/css/combined.css") %>
    <%= Bundle.Resx()
                .Add("~/resx/Resource1.resx")
                .Add("~/resx/Resource2.resx")
                .Render("~/resx/combined.js")%>
    <form id="form1" runat="server">
    <div>
        <input type="button" onclick="javascript:alert(Resource1.String1 + ' ' + Resource1.String2 + ' ' + Resource2.String1 + ' ' + Resource2.String2)" value="ResxBundler Click Me!" />
    </div>
    </form>
</body>
</html>
