<%@ Page Title="" Language="C#" MasterPageFile="~/Andre/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="web.Andre.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyc" runat="server">
    <h1>User-Einsicht</h1>
    <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" style="text-align: left">
        <Columns>
            <asp:BoundField DataField="UName" HeaderText="Name" SortExpression="UName" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="UAddress" HeaderText="Adresse" SortExpression="UAddress" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="UIsActive" HeaderText="Aktiv?" SortExpression="UIsActive" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="URole" HeaderText="Rolle" SortExpression="URole" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <br />

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT [UName], [UAddress], [UIsActive], [URole] FROM [QUGetAllUsers] ORDER BY [UID]"></asp:SqlDataSource>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyc2" runat="server">
    
    <asp:Label ID="Label2" runat="server" Text="Artikel 1"></asp:Label>
    
</asp:Content>
