<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-select.aspx.cs" Inherits="web.beispiele.user_select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Auswahl eines Users</title>
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>    
        <h1>Selektieren eines Users</h1>
    </div>
    <div>   
        <br />
        über DropDownList und Button:</div>
    
    </div>
    <asp:DropDownList ID="ddlUser" runat="server" DataSourceID="odsUsers" 
        DataTextField="Name" DataValueField="ID">
    </asp:DropDownList>
&nbsp;
    <asp:Button ID="btnDropDownSelect" runat="server" 
        onclick="btnDropDownSelect_Click" Text="aus DropDownList Auswählen" />
        <br />
        <br />
        oder über DropDownList und direktem Postback:<br />
        <asp:DropDownList ID="ddlUsersWithAutoPostback" runat="server" AutoPostBack="True" DataSourceID="odsUsers" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="ddlUsersWithAutoPostback_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
    <br />
    <asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="getAllUsers" 
        TypeName="bll.clsUserCollection"></asp:ObjectDataSource>
    <br />
    <br />
    <asp:Label ID="lblMsg" runat="server" Text="..." ForeColor="#0066CC"></asp:Label>
    <br />
            <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
    
        <br />
            <hr />
                    <br />
        <em><span class="style3">Implementierung mittels DropDownList </span>
    <span class="style4">ddlUser</span><span class="style3"> und ddlUsersWithAutoPostback, ObjectDataSource </span><span class="style4">odsUser</span><span class="style3">, BLL.clsUserFacade</span><span class="style3">, 
            Queries </span><span class="style4">QGetAllUsers</span><span 
        class="style3"> und </span><span class="style4">QGetUserByID</span><span 
        class="style3">, sowie Tabelle </span><span class="style4">TUser</span><br 
        class="style3" />
        
        <br class="style3" />
            <span class="style3">ddlUser.DataSourceID = odsUser</span><br 
        class="style3" />
            <span class="style3">ddlUser.DataTextField = Name // was in DDL angezeigt wird</span><br 
        class="style3" />
            <span class="style3">ddlUser.DataValueField = ID&nbsp; // Attribut, welches als selektierter 
            Wert weitergegeben wird. In Methode </span><span class="style4">btnDDLUser_Click()</span><span 
        class="style3"> Zugriff mittels </span><span class="style4">ddlUser.SelectedValue</span><br 
        class="style3" />
            <br />
        ddlUsersWithAutoPostback<span class="style3">.DataSourceID = odsUser</span><br 
        class="style3" />
            ddlUsersWithAutoPostback<span class="style3">.DataTextField = Name </span><br 
        class="style3" />
            ddlUsersWithAutoPostback<span class="style3">.DataValueField = ID&nbsp;<br />
        ddlUsersWithAutoPostback.AutoPostback = true </span>
        <br />
            <br class="style3" />
            <span class="style3">odsUser.TypeName = bll.clsUserFacade</span><br 
        class="style3" />
            <span class="style3">odsUser.SelectMethod = UserGetById
            </span></em>
    </form>
</body>
</html>
