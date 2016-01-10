<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="web.default_code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .lbl-font-small{
            font-family:Calibri;
            font-size:small
        }
        .lbl-font-medium{
            font-family:Calibri;
            font-size:medium
        }
        .lbl-font-large{
            font-family:Calibri;
            font-size:large
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <asp:Label runat="server" Font-Bold="true" Font-Size="X-Large" Font-Names="Calibri" ForeColor="#CF323D">Willkommen bei Pizza Pizza München!</asp:Label>

    <br />
    <hr />
    <br />

    <p>
        <asp:Label runat="server" CssClass="lbl-font-large" ForeColor="#CF323D">Lust auf gutes Essen? Wir bieten Ihnen italienischen Genuss ganz in Ihrer Nähe!</asp:Label>
    </p>
    <br />
    <p>
        <asp:Label runat="server" CssClass="lbl-font-large" ForeColor="Green"> Traditionell zubereitete, italienische Spezialitäten - direkt auf den Tisch, köstlich und preiswert.</asp:Label>
    </p>
    <p>
        <asp:Label runat="server" CssClass="lbl-font-large" ForeColor="Green">Dazu gibt es schmackhafte Getränke aus unserem umfangreichen Sortiment.</asp:Label>
    </p>
    <br />
    <p>
        <asp:Label runat="server" CssClass="lbl-font-large" Font-Bold="true" ForeColor="#CF323D">Wir freuen uns auf Sie!</asp:Label>
    </p>

    <br />
    <hr />

    <asp:Table runat="server" CellSpacing="20">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblOpen" runat="server" CssClass="lbl-font-medium" Text="Öffnungszeiten"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblHome" runat="server" CssClass="lbl-font-medium" Text="Heimservice"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblContact" runat="server" CssClass="lbl-font-medium" Text="Kontakt"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblDays" runat="server" CssClass="lbl-font-small" Text="Montag - Sonntag"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblMinimum" runat="server" CssClass="lbl-font-small" Text="Mindestbestellwert: 20€"></asp:Label>
                </p>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblPhone" runat="server" CssClass="lbl-font-small" Text="Telefon: 089 1265-3714"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblTimes1" runat="server" CssClass="lbl-font-small" Text="11:30 bis 13:30 Uhr"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblTimes2" runat="server" CssClass="lbl-font-small" Text="17:00 bis 22:00 Uhr"></asp:Label>
                </p>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblMinKm" runat="server" CssClass="lbl-font-small" Text="Lieferungen bei einer Distanz unter 20 Kilometer"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblService" runat="server" CssClass="lbl-font-small" Text="Größere Distanz: Nur auf Anfrage"></asp:Label>
                </p>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblEmail" runat="server" CssClass="lbl-font-small" Text="E-Mail: bestellung@pizzapizza.de"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblFax" runat="server" CssClass="lbl-font-small" Text="Fax: 089 1265-3715"></asp:Label>
                </p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table runat="server" CellSpacing="20">
        <asp:TableRow>
            <asp:TableCell>
                <img src="img/pizza-1562028.jpg" />
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblAdress" runat="server" CssClass="lbl-font-medium" Text="Adresse:"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblStreet" runat="server" CssClass="lbl-font-small" Text="Lothstraße 64, 80335 München"></asp:Label>
                </p>
                <br />
                <p>
                    <asp:Label ID="lblFast" runat="server" CssClass="lbl-font-small" Text="Nur 5 Minuten vom Hauptbahnhof München!"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblFastExtended1" runat="server" Font-Size="Smaller" Font-Names="Calibri" Text="Tram: Linie 20, 21, 22"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblFastExtended2" runat="server" Font-Size="Smaller" Font-Names="Calibri" Text="Haltestelle: Hochschule München (Lothstraße)"></asp:Label>
                </p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
