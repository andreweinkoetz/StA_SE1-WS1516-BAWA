<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="beverages.aspx.cs" Inherits="web.Beverage_Code" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .headLines{
            font-family: 'Segoe UI';
            font-weight: bold;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <asp:ScriptManager runat="server" AjaxFrameworkMode="Enabled"></asp:ScriptManager>
    <p class="headLines">Unsere Getränke</p>
    <p><asp:Label ID="lblInfoBeverages" Font-Size="Small" runat="server" Text="Zu einem guten Essen gehört immer ein passendes Getränk."></asp:Label></p>
    <hr /><br />
    <p class="headLines">
        <asp:GridView ID="gvBeverages" OnDataBound="gvBeverages_DataBound" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource2" ForeColor="#333333" GridLines="None" Style="text-align: center" Width="100%" OnSelectedIndexChanged="gvBeverage_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/img/select_icon.png" HeaderText="Wählen" />
                <asp:BoundField DataField="Id" HeaderText="Nr." SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="PricePerUnit" HeaderText="Preis pro Liter" SortExpression="PricePerUnit" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" Visible="False" />
                <asp:BoundField DataField="CUnit" HeaderText="CUnit" SortExpression="CUnit" Visible="False" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="ProductsGetAllByCategory" TypeName="bll.clsProductFacade">
            <SelectParameters>
                <asp:SessionParameter Name="_category" SessionField="category" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </p>
    <br /> <br />
    <p>
        <asp:Label ID="lblGvToOrder" runat="server" Text="Zum Warenkorb hinzufügen" Visible="false" CssClass="headLines"></asp:Label>
        <asp:GridView ID="gvBeverageToOrder" Visible="false" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: center" Width="100%" OnSelectedIndexChanged="gvBeverageToOrder_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/img/marker20-2.png" />
                <asp:BoundField DataField="Id" HeaderText="Nr." SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:TemplateField HeaderText="Größe Getränk">
                    <ItemTemplate>
                        <asp:DropDownList ID="sizeSelect" runat="server" AutoPostBack="false" DataSourceID="objSizeSource" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objSizeSource" runat="server" SelectMethod="getSizesByCategory" TypeName="bll.clsSizeFacade">
                            <SelectParameters>
                                <asp:SessionParameter Name="_category" SessionField="category" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Anzahl">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                        <ajaxToolkit:NumericUpDownExtender ID="numericAmount" Width="50" runat="server" Maximum="25" Minimum="1" TargetControlID="txtAmount" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
    </p>

    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                &nbsp;
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblChooseSize" ForeColor="Red" runat="server" Text="Bitte wählen Sie eine Größe!" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
