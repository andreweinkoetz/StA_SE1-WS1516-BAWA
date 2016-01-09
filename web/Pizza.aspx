<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="pizza.aspx.cs" Inherits="web.PizzaCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #Button1 {
            text-align: left;
        }

        #TextArea1 {
            height: 167px;
            width: 412px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p style="font-family: 'Segoe UI'; font-weight: bold">Unsere Pizza</p>
    <p><asp:Label ID="lblInfoPizza" Font-Size="Small" runat="server" Text="Frisch aus dem Steinofen - direkt zu Ihnen nach Hause!"></asp:Label></p>
    <hr /><br />
    <p style="font-family: 'Segoe UI'; font-weight: bold; text-align: left">
        <asp:GridView ID="gvPizza" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource2" OnDataBound="gvPizza_DataBound" ForeColor="#333333" GridLines="None" Style="text-align: center" Width="100%" OnSelectedIndexChanged="gvPizza_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image" SelectImageUrl="~/img/marker20-2.png" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="PricePerUnit" HeaderText="Preis pro cm" SortExpression="PricePerUnit" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" Visible="False" />
                <asp:BoundField DataField="CUnit" HeaderText="CUnit" SortExpression="CUnit" Visible="False" />
                <asp:TemplateField HeaderText="Größe Pizza">
                    <ItemTemplate>
                        <asp:DropDownList ID="sizeSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="sizeSelect_SelectedIndexChanged" DataSourceID="objSizeSource" DataValueField="Value" DataTextField="Name">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objSizeSource" runat="server" SelectMethod="getSizesByCategory" TypeName="bll.clsSizeFacade">
                            <SelectParameters>
                                <asp:SessionParameter Name="_category" SessionField="category" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Extras" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="gvPizza">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="ExtrasCheckBoxList" runat="server" AutoPostBack="False" DataSourceID="ExtrasDataSource" DataTextField="Name" DataValueField="ID">
                        </asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ExtrasDataSource" runat="server" SelectMethod="GetAllActiveExtras" TypeName="bll.clsExtraFacade"></asp:ObjectDataSource>
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
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="ProductsGetAllByCategory" TypeName="bll.clsProductFacade">
            <SelectParameters>
                <asp:SessionParameter Name="_category" SessionField="category" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>



    </p>

    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                &nbsp;
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblChooseSize" ForeColor="Red" runat="server"></asp:Label>
            </td>
        </tr>
    </table>



</asp:Content>

