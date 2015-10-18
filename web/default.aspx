<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="web._default" Culture="de-DE" %>

<!DOCTYPE html>

<html lang="de-DE">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="StyleSheet.css" />
    <meta charset = "ISO-8859-1" />
    <title>Pizza Pizza!</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>

        <h1>Willkommen bei Heigert's Pizza!</h1></div>
    <p>
        <a href="Beispiele/default.aspx">Hier</a> gehts weiter zu einigen Beispielprogrammen ... <br />
        Hier gehts zu Andre's Testseiten: <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Andre/WebForm1.aspx">Klick!</asp:HyperLink>
    </p> 
        <p><i>Version: 1.10.2015</i></p>
    </form>
</body>
</html>
