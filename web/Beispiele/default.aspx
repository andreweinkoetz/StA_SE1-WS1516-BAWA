<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="web.beispiele._default" %>

<!DOCTYPE html>

<html lang="de-DE">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <title>Pizza Pizza!</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Pizza! Verschiedene Beispielprogramme</h1>
        </div>
                <p>die Seiten hier sind nur als Beispielprogramme gedacht, um zu zeigen wie verschiedene Aufgaben gelöst werden können.</p>
        <p>
            <a href="user-read.aspx">Lesen aus User Collection</a></p>
        <p>
            <a href="user-insert.aspx">Einfügen neuer User in User Collection</a></p>
        <p>
            <asp:HyperLink ID="HyperLink7" runat="server"
                NavigateUrl="user-aendern.aspx">User Ändern/Löschen</asp:HyperLink>
        </p>
        <p>
            <a href="user-select.aspx">mit DropDownList arbeiten</a></p>
        <p>
            <asp:HyperLink ID="HyperLink6" runat="server"
                NavigateUrl="user-zaehlen.aspx">User Zählen</asp:HyperLink>
        </p>   
                <p>
            <asp:HyperLink ID="HyperLink8" runat="server"
                NavigateUrl="order-read.aspx">Order Read</asp:HyperLink>
        </p>

                <p>
            <asp:HyperLink ID="HyperLink9" runat="server"
                NavigateUrl="~/Beispiele/order-insert.aspx">Order Insert</asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Beispiele/session1.aspx">Arbeiten mit Session Variable </asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Beispiele/Seite1mitMaster.aspx">Seite mit Master-Page</asp:HyperLink>
        </p>
             <p>
            <asp:HyperLink ID="HyperLink1" runat="server"
                NavigateUrl="~/default.aspx">Home</asp:HyperLink>
        </p>
        
    </form>
</body>
</html>
