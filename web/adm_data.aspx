<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="adm_data.aspx.cs" Inherits="web.AdmData_Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
     <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblAdmData" runat="server" Text="" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <p style="font-size:small">Stammdatenadministration, ausschließlich für berechtigte Personen vorgesehen.</p>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>

    <hr />

    <p>
        <asp:GridView ID="gvAdmData" runat="server" Width="100%" OnSelectedIndexChanged="gvAdmData_SelectedIndexChanged" AutoGenerateColumns="False">
        </asp:GridView>
    </p>
    <p>
        <asp:Button ID="btCreateNew" runat="server" Text="" OnClick="btCreateNew_Click" Height="50px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" /> 
    </p>
</asp:Content>
