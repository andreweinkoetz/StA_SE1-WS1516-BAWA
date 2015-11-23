<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="web.default_code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .p-start{
            margin-top:20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <h1>Startseite</h1>
    <h2>Einstieg für Dozent</h2>
    <p class="p-start">Hier befindet sich die Startseite von BAWA</p>
    <p class="p-start">Diese wird im weiteren Verlauf des Projekts durch eine Willkommensseite ersetzt.</p>
    <p class="p-start"><u>Zugänge lauten:</u></p>
    <table style="width:400px">
        <tr>
            <td><b>Username</b></td>
            <td><b>Password</b></td>
            <td><b>Role</b></td>
        </tr>
        <tr>
            <td>admin@pizzapizza.de</td>
            <td>admin</td>
            <td>Administrator</td>
        </tr>
        <tr>
            <td>service@pizzapizza.de</td>
            <td>admin</td>
            <td>Service-Mitarbeiter</td>
        </tr>
        <tr>
            <td>kunde@pizzapizza.de</td>
            <td>admin</td>
            <td>Kunde</td>
        </tr>
    </table>
    <p class="p-start">Für den Gastzugang wählen Sie einfach die gewünschte Seite.</p>
</asp:Content>
