<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="web.Stats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p>
        <asp:Label ID="lblHeader" runat="server" Text="Auswertungen" Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </p>
    <br />
    <hr />
    <br />
    <p>
        <asp:Label ID="lblStatsRequired" runat="server" Text="Bitte geben Sie die gewünschte Auswertungsart an." Font-Bold="true" Font-Size="Medium"></asp:Label>
    </p>
    <br />
    <asp:Table runat="server" CellSpacing="10">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>
                <asp:Table runat="server" ID="tblStatistic" BorderStyle="Groove" CellSpacing="10">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server">
                            <asp:DropDownList ID="ddlStats" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlStats_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="- Daten wählen"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Bestellungen"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Produkte"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Kunden"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlStatsExtended" runat="server" Width="200px" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlStatsExtended_SelectedIndexChanged"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlUser" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" AutoPostBack="true"></asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnCreateStats" runat="server" Text="Auswertung erstellen" Height="50px" Width="170px" ForeColor="White" Enabled="false" BackColor="Gray" Font-Bold="true" OnClick="btnCreateStats_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableHeaderCell>
            <asp:TableHeaderCell>
                <asp:Table ID="stats" runat="server" BorderStyle="Groove" CellSpacing="10" Visible="false" AutoPostBack="true">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblRevenue" runat="server" Text="Gesamtumsatz:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblRevenueResult" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblAverage" runat="server" Text="Durchschnittsbestellwert:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblAverageResult" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Label ID="lblAnnotation" runat="server" Text="" Font-Italic="true" Font-Size="Small" Visible="false"></asp:Label>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>

    <asp:GridView ID="gvStats" runat="server" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="#CF323D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
</asp:Content>
