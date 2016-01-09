<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="web.default_code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <asp:Label runat="server" Font-Bold="true" Font-Size="X-Large" Font-Names="Calibri" ForeColor="Red">Willkommen bei Pizza Pizza München!</asp:Label>

    <br />
    <hr />
    <br />

    <p>
        <asp:Label runat="server" Font-Size="Large" Font-Names="Calibri" ForeColor="Red">Lust auf gutes Essen? Wir bieten Ihnen italienischen Genuss ganz in Ihrer Nähe!</asp:Label>
    </p>
    <br />
    <p>
        <asp:Label runat="server" Font-Size="Large" Font-Names="Calibri" ForeColor="Green"> Traditionell zubereitete, italienische Spezialitäten - direkt auf den Tisch, köstlich und preiswert.</asp:Label>
    </p>
    <p>
        <asp:Label runat="server" Font-Size="Large" Font-Names="Calibri" ForeColor="Green">Dazu gibt es schmackhafte Getränke aus unserem umfangreichen Sortiment.</asp:Label>
    </p>
    <br />
    <p>
        <asp:Label runat="server" Font-Size="Large" Font-Bold="true" Font-Names="Calibri" ForeColor="Red">Wir freuen uns auf Sie!</asp:Label>
    </p>

    <br />
    <hr />

    <asp:Table runat="server" CellSpacing="20">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblOpen" runat="server" Font-Size="Medium" Font-Names="Calibri" Text="Öffnungszeiten"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblHome" runat="server" Font-Size="Medium" Font-Names="Calibri" Text="Heimservice"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblContact" runat="server" Font-Size="Medium" Font-Names="Calibri" Text="Kontakt"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblDays" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Montag - Sonntag"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblMinimum" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Mindestbestellwert: 20€"></asp:Label>
                </p>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="lblPhone" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Telefon: 089 1265-3714"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblTimes1" runat="server" Font-Size="Small" Font-Names="Calibri" Text="11:30 bis 13:30 Uhr"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblTimes2" runat="server" Font-Size="Small" Font-Names="Calibri" Text="17:00 bis 22:00 Uhr"></asp:Label>
                </p>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblMinKm" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Lieferungen bei einer Distanz unter 20 Kilometer"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblService" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Größere Distanz: Nur auf Anfrage"></asp:Label>
                </p>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblEmail" runat="server" Font-Size="Small" Font-Names="Calibri" Text="E-Mail: bestellung@pizzapizza.de"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblFax" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Fax: 089 1265-3715"></asp:Label>
                </p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table runat="server" CellSpacing="20">
        <asp:TableRow>
            <asp:TableCell>
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2661.6697136640114!2d11.553505115561654!3d48.15517247922479!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x479e760a9c40e47d%3A0xe80334f6cdb68334!2sLothstra%C3%9Fe+64%2C+80335+M%C3%BCnchen!5e0!3m2!1sde!2sde!4v1452335728674" style="border-style: none; border-color: inherit; border-width: 0; height: 280px; width: 380px;"" ></iframe>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:Label ID="lblAdress" runat="server" Font-Size="Medium" Font-Names="Calibri" Text="Adresse:"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblStreet" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Lothstraße 64, 80335 München"></asp:Label>
                </p>
                <br />
                <p>
                    <asp:Label ID="lblFast" runat="server" Font-Size="Small" Font-Names="Calibri" Text="Nur 5 Minuten vom Hauptbahnhof München!"></asp:Label>
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
