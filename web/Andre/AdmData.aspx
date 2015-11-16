<%@ Page Title="" Language="C#" MasterPageFile="~/Andre/default_layout.Master" AutoEventWireup="true" CodeBehind="AdmData.aspx.cs" Inherits="web.Andre.AdmData_Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
     <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblAdmData" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ProductsGetAll" TypeName="bll.clsProductFacade" DataObjectTypeName="bll.clsProductExtended" UpdateMethod="UpdateProduct"></asp:ObjectDataSource>
                <asp:Button ID="btLogout" runat="server" Text="Logout" OnClick="btLogout_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>

    <hr />

    <p>
        <asp:GridView ID="gvAdmData" runat="server" Width="100%" DataSourceID="ObjectDataSource1" OnRowUpdating="gvAdmData_RowUpdating" AutoGenerateColumns="False">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="PricePerUnit" HeaderText="PricePerUnit" SortExpression="PricePerUnit" />
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                <asp:BoundField DataField="CUnit" HeaderText="CUnit" SortExpression="CUnit" />
                <asp:CheckBoxField DataField="ToSell" HeaderText="ToSell" SortExpression="ToSell" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
