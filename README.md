# StA_SE1-WS1516-BAWA

<h2>Termine</h2>
<ul>
<li>Meilenstein 1: bis spätestens Donnerstag, 26.11 .2015, 17 Uhr.</li>
<li>Meilenstein 2: bis spätestens Donnerstag, 17.12.2015, 17 Uhr.</li>
<li>Meilenstein 3: bis spätestens Freitag 21.01.2016, 17 Uhr.</li>
</ul>

<h2>Gruppe</h2>
<ul>
<li>Alexander Baum</li>
<li>Andre Weinkötz</li>
</ul>
Gruppenname: BAWA

<h2>Bewertung</h2>
Detail-Bewertung:
<ul>
<li>Meilenstein M1 wird nicht bewertet, falls ausreichende Leistung. Ansonsten gibt
es Abzug auf die Gesamtnote.</li>
<li>Meilenstein M2 (Pflichtenheft) sowie Meilenstein M3 wird bewertet.</li>
<li>Für eine sehr gute Note müssen in M3 einige weitere Funktionen über Version
V2 hinaus implementiert werden.</li>
<li>Programmierung (Funktionalität, Qualität) und Dokumentation (Statusbericht,
Pflichtenheft) werden bei der Note gleich stark gewichtet.</li>
</ul>

<h2>Umfang Meilensteine</h2>
**Version V1 (Minimalfunktionalität, zu implementieren für Meilenstein M1):**
<ul>
<li>Homepage mit Produktliste, Registrierung, Login</li>
<li>Navigation entsprechend Rolle,</li>
<li>Bestellen (inklusive Preisberechnung) und eigene Bestellungen anschauen.</li>
</ul>

**Version V3 (Minimalfunktionalität, zusätzlich zu V1, zu implementieren für Meilenstein M3):**
<ul>
<li>Funktionen für Gast, Kunde, Service-Mitarbeiter. Ausnahmen:</li>
<li>Ein sicheres Login-Verfahren mit Passwort ist nicht notwendig: Auswahl aus
einer Drop-Down-Liste ausreichend.</li>
<li>Pro Bestellung kann nur ein Produkt bestellt werden (dieses aber in größerer
Anzahl)</li>
<li>Stornieren von Bestellungen nicht notwendig.</li>
 <li>Produkte und Kategorien müssen nur angezeigt, aber noch nicht über eine
Benutzerschnittstelle verwaltet werden können (sondern nur direkt in der Datenbank)</li>
 <li>Kunden, Produkte, Kategorien müssen nicht gelöscht werden können.</li>
 <li>Auswertungen für Manager: nur die Listen aller Bestellungen, Produkte, Nutzer
notwendig.</li>
 <li>Externe Schnittstellen nicht notwendig.</li>
 <li>Gutscheine nicht notwendig.</li>
 <li>Die Optik der Anwendung kann einfach gehalten werden.</li>
</ul>

**Version VV (volle Funktionalität, zu spezifizieren in Lastenheft und Pflichtenheft):**
Im Pizzashop werden verschiedene Produkte angeboten (z.B. Pizza Margerita, Pizza Funghi,
Cola, Tiramisu). Diese Produkte sind nach Kategorien eingeteilt (z.B. Pizza, Getränke,
Desserts).<br />
Nutzer können diese Produkte online bestellen. Dazu können sich Nutzer auf der Seite
registrieren. Es gibt verschiedene Arten von Benutzern (Rollen):<br />
<ul>
<li>Gast: nicht angemeldeter Nutzer.
Als Gast kann ich mich über die angebotenen Produkte informieren und ich kann
mich neu registrieren.</li>
<li>Kunde: angemeldeter Nutzer.
Als Kunde kann ich Produkte bestellen und diese liefern lassen oder selbst abholen.
Die Kunden sollen sich auch über ihre aktuellen und bisherigen Bestellungen
informieren können. Dabei soll auch der bisherige Gesamtumsatz ausgegeben
werden. Auch das Stornieren von Bestellungen soll unter bestimmten Umständen
möglich sein.</li>
<li>Manager: Manager des Pizza-Shops
siehe unten.</li>
<li>Service Mitarbeiter: Mitarbeiter, die backen, verpacken und liefern
Diese Mitarbeiter können sich informieren über die aktuell anstehenden Bestellungen
und sie können melden wenn die Pizza einer Bestellung fertig gebacken ist, bzw.
wenn die Bestellung ausgeliefert ist.</li>
</ul>
Die Pizza-Manager können zusätzliche Funktionen nutzen:
<ul>
<li>Produkte verwalten: anlegen, löschen, anzeigen, ändern (Löschen nur, wenn keine
Bestellung zu dem Produkt vorhanden ist), Produkte können aus dem Sortiment
herausgenommen werden, ohne dass sie gelöscht werden.</li>
<li>Kunden verwalten: Kunden sollen angelegt, angezeigt, geändert und gelöscht werden
können (Löschen jedoch nur, wenn der Kunde noch keine Bestellungen getätigt hat).
Kunden können auch auf „inaktiv“ gesetzt werden, sodass sie sich nicht mehr
anmelden können.</li>
<li>Status von Bestellungen verfolgen.</li>
<li>Auswertungen: Den Managern werden verschiedene Auswertungen zur Verfügung
gestellt:</li><ul>
<li>alle Bestellungen nach Datum sortiert anzeigen (mit Gesamtumsatz und
Durchschnittsbestellwert),</li>
<li>alle Bestellungen pro Kategorie anzeigen (mit Umsatz und
Durchschnittsbestellwert für diese Kategorie),</li>
<li>alle Bestellungen eines Kunden anzeigen (mit Umsatz und
Durchschnittsbestellwert),</li>
<li>beliebteste Produkte, umsatzstärkste Produkte, umsatzstärkste Kunden.</li>
<li>und ggf. andere</li>
</ul>
<li>Gutscheine ausgeben, die Kunden dann einlösen können.</li>
</ul>
Die Preisberechnung für eine Bestellung hat folgende Grundidee: Ein Produkt kann eine
Größe (Size) haben, diese wird in Units gemessen. Zum Beispiel kann eine Pizza einen
Durchmesser (Size) in cm (Unit) haben, eine Cola eine Flaschengröße (Size) in Litern (Unit).
Jede Unit hat einen Preis. Zusätzlich können Produkte noch Extras haben (z.B. Pizza: extra
Käse, extra Soße), Jedes Extra kosten einen zusätzlichen (einheitlichen) Preis.
Beispiel: Pizza Capriciosa: Kostet pro cm 0,5 €, jedes Extra auch 0,5 €. Eine
Bestellung mit 2 Pizzen à 20 cm Durchmesser und je 2 Extras kostet 22 €.
Zusätzlich soll bei einer Bestellung auch die geschätzte Lieferzeit berechnet werden. Diese
errechnet sich aus der Distanz zum Kunden. Die Pizzabackzeit ist 10 min. Pro Kilometer
kommen 2 min Fahrzeit hinzu. Es werden nur Kunden bis zu einer Distanz von 20 km
bedient.
Es ist weiterhin geplant, Schnittstellen zu anderen Informationssystemen zu realisieren:
<ul>
<li>Nutzung von externen Lieferdiensten als Alternative dazu, selbst auszufahren</li>
<li>Automatischer Abgleich mit einem ERP-System (Finanzbuchhaltung, ggf.
Warenwirtschaft)</li>
</ul>
