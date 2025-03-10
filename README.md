# Travel agency system
### Korisničko uputstvo


---
## Opis

Aplikacija razvijena kao projekat na predmetu Interakcija čovjek-računar na Elektrotehničkom fakultetu u Banjoj Luci. Omogućava olakšan i pregledan rad zaposlenima u
turističkoj agenciji.

Aplikacija je dizajnirana tako da omogućava rad sa dva tipa korisnika: agentima prodaje i administratorima. Sastoji se od tri prozora: prozor za prijavu, prozor za agenta prodaje i prozor za administratora. U prozorima za administratora i agenta prodaje sa lijeve strane nalazi se navigacioni meni, u sklopu kojeg je i opcija za promjenu lozinke, kao i odjavu iz aplikacije. 

---
Sadržaj:

1. [Prijava na aplikaciju](#prijava-na-aplikaciju)
2. [Korisnici](#korisnici)  
   - [Univerzalne karakteristike](#univerzalne-karakteristike)
      - [Tema i jezik](#tema-i-jezik)
      - [Promjena lozinke](#promjena-lozinke)
      - [Povratne informacije](#povratne-informacije)  
   - [Administrator](#administrator)  
     - [Rad sa zaposlenima](#rad-sa-zaposlenima)  
     - [Rad sa destinacijama](#rad-sa-destinacijama)  
     - [Rad sa hotelima](#rad-sa-hotelima)  
     - [Rad sa aranžmanima](#rad-sa-aranžmanima)  
     - [Rad sa rezervacijama](#rad-sa-rezervacijama)  
   - [Agent prodaje](#agent-prodaje)  
     - [Rad sa putnicima](#rad-sa-putnicima)  
     - [Rad sa hotelima](#rad-sa-hotelima)  
     - [Rad sa aranžmanima](#rad-sa-aranžmanima)  
     - [Rad sa rezervacijama](#rad-sa-rezervacijama)
     - [Rad sa pojedinačnim uplatama](#rad-sa-pojedinačnim-uplatama)
     - [Rad sa ukupnim uplatama](#rad-sa-ukupnim-uplatama)
3. [Korištene tehnologije](#korištene-tehnologije)  
4. [Kontakt](#kontakt)

---

## Prijava na aplikaciju

Za pristup funkcionalnostima aplikacije neophodno je da korisnik ima korisnički nalog i da se na njega prijavi. Ukoliko kredencijali nisu tačni prikazaće se odgovarajuća poruka i korisnik neće biti u mogućnosti da nastavi sa radom dok ih ne ispravi.
<p >
<img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/Login.png?raw=true" alt="LoginWindow" width="450"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/LoginUnsuccessful.png?raw=true" alt="LoginWindow" width="450">
</p>

## Korisnici

Definisane su dvije vrste korisničkh naloga: administrator i agent prodaje. Nalozi se razlikuju prema ovlaštenjima koje određena grupa zaposlenih ima u agenciji. 

## Univerzalne karakteristike
### Tema i jezik
U gorenjem desnom uglu prozora pozicioniran je padajući meni sa tri dostupne teme: *plava*, *bordo* i *tamna*.
Pored izbora teme nalaze se dva dugmeta: **EN** za engleski jezik i **SR** za srpski. 

![image](https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/ThemeLanguageChoice.png)

**Teme**:

<p >
<img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/Employees.png?raw=true" alt="BlueTheme" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/BurgundyTheme.png?raw=true" alt="BurgundyTheme" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/DarkTheme.png?raw=true" alt="BurgundyTheme" width=" 300">
</p>

### Promjena lozinke
Na dnu navigacionog menija postavljena je mogućnost promjene lozinke. Klikom na dugme "Promijeni lozinku" otvara se prozor sa poljima za unos trenutne lozinke, nove lozinke, kao i potvrda nove lozinke. U slučaju da unesena trenutna lozinka nije tačna ili da se ne poklapaju unosi nove lozinke korisniku će se prikazati poruka i biće vraćen na prethodni korak. Za prihvatanje izmjene lozinke traži se dodatna potvrda od korisnika, uz mogućnost odustajanja. 

<p >
    <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/PasswordChange.png?raw=true" alt="PasswordChange" width="  300">
</p>

<p >
<img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/OldPasswordError.png?raw=true" alt="OldPasswordError" width="  300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/NewPasswordMismatch.png?raw=true" alt="NewPasswordMissmatch" width="  300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/PasswordChanged.png?raw=true" alt="PasswordChanged" width="  300">
</p>

### Povratne informacije
Sve aktivnosti podržane u aplikaciji imaju povratnu informaciju, kao što su potvrda o dodavanju, ažuriranju i brisanju stavki, kao i otkazivanju neke ranije započete radnje. U formama gdje je neophodno da određeno polje bude popunjeno postoji provjera da li je uslov ispunjen i u slučaju da nije aktivnost ne može biti nastavljena. Polja za unos imaju dodatnu provjeru da li je uneseni tekst u skladu sa očekivanim formatom, u suprotnom korisnik dobija obavještenje da nije. Aktivnosti koje se odnose na tačno određenu stavku u list/tabeli, npr. ažuriranje ili brisanje, očekuju selekciju željene stavke. Navedena obavještenja se kreiraju u obliku zasebnog prozora, prisutna su za svaku korisničku aktivnost i prikazana su na slikama ispod:



*Odustajanje od započete radnje*
 <p>
    <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/ActionCanceled.png?raw=true" alt="ActionCanceled" width="  300">
</p>

*Pogrešan format unosa*
<p>
    <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/BadForm.png?raw=true" alt="BadForm" width="  300">
</p>

*Prazno polje za unos*
<p>
    <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/EmptyField.png?raw=true" alt="BadForm" width="  300">
</p>

*Stavka nije selektovana*
<p>
    <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/RowNotSelected.png?raw=true" alt="RowNotSelected" width="  300">
</p>

*Usješno dodavanje, ažuriranje, brisanje*
<p >
<img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/AddSuccessful.png?raw=true" alt="AddSuccessful" width="  300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/UpdateSuccessful.png?raw=true" alt="UpdateSuccessful" width="  300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/General/DeleteSuccessful.png?raw=true" alt="DeleteSuccessful" width="  300">
</p>

*Shodno navedenom prikazuju se i informacije o neuspjelim radnjama.*

---
## Administrator
Administrator je tip korisničkog naloga koji ima ovlaštenje da upravlja nalozima zaposlenika, destinacijama, hotelima, aranžmanima. Pored toga on ima i uvid u kreirane rezervacije, uz navođenje agenta prodaje odgovornog za rezervaciju. Početna stavka navigacionog menija koja se aktivira pri otvaranju prozora je "Zaposleni". 

   - ### Rad sa zaposlenima
     Informacije o radnicima turističke agencije prikazane su u vidu kartica. Omogućeno je **pretraživanje** osoba unosom (dijela/kompletnog) imena i/ili prezimena u polje za pretragu.
     <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/Employees.png?raw=true" alt="Employees"> <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/EmployeesSearch.png?raw=true" alt="EmployeesSearch" width="700" > 
      </p>

      Klikom na dugme **Dodaj** otvara se forma za unos podataka o novom radniku. Svaki radnik ima jedinstven JMB. Za nastavak neophodno je kliknuti na dugme "OK", nakokn čega se otvaa prozor za potvrdu dodavanja ili odustajanje. Shodno izboru korisnika prikazaće se poruka o tome da li je dodavanje bilo uspješno ili nije, ili u slučaju odustajanja da je aktivnost otkazana.
     <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/AddEmployee1.png?raw=true" alt="AddEmployee1" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/AddEmployee2.png?raw=true" alt="AddEmployee2" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/ConfirmAddEmployee.png?raw=true" alt="ConfirmAddEmployee" width="  300">
      </p>

      Za ažuriranje podataka o zaposlenom potrebno je prvo selektovati željenu karticu u prikazu, te onda kliknuti na dugme **Uredi**. Dalji proces se odvija isto kao prilikom dodavanja.
       <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/UpdateEmployee1.png?raw=true" alt="UpdateEmployee1" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/UpdateEmployee2.png?raw=true" alt="UpdateEmployee2" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/ConfirmUpdateEmployee.png?raw=true" alt="ConfirmUpdateEmployee" width="  300">
      </p>

      Brisanje je moguće samo ukoliko tip naloga nije "administrator" i ukoliko zaposleni nije obavio neku značajnu aktivnost kao što je kreiranje rezervacije ili potvrda uplate. Obavlja se selekcijom kartice i klikom na dugme **Delete**, nakon čega je potrebna dodatna konfirmacija brisanja.
     
     <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/Employees/ConfirmDeleteEmployee.png?raw=true" alt="DeleteEmployee" width="  300">

   - ### Rad sa destinacijama
     Podaci o destinacijama prikazani su u vidu tabele, a **filtriranje** se vrši unošenjem imena destinacije u polje.
     <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminDestinations/AdminDestinations.png?raw=true" alt="Destinations"> <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminDestinations/AdminDestinationSearch.png?raw=true" alt="DestinationsSearch" width=" 700" > 
      </p>

     Koncept dodavanja i ažuriranja je sličan kao kod zaposlenih. Dodaje se klikom na dugme **Dodaj**. Kombinacija naziva destinacije i poštanskog broja mora biti jedinstvena, u suprotnom dodavanje neće biti moguće.

     <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminDestinations/AddDestination1.png?raw=true" alt="AddDestination1" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminDestinations/AddDestination2.png?raw=true" alt="AddDestination2" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminDestinations/ConfirmAddDestination.png?raw=true" alt="ConfirmAddDestination" width=" 300">
      </p>

     Za brisanje je takođe potrebno odabrati stavku u tabeli i kliknuti na **Obriši**.

      <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminDestinations/ConfirmDestinationDelete.png?raw=true" alt="DeleteEmployee" width="  300">
      
   - ### Rad sa hotelima

     Informacije o hotelima su prikazane u tabeli. Moguće ih je pretražiti po imenu destinacije u kojoj se nalaze.
        <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/AdminHotels.png?raw=true" alt="Hotels"><img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/AdminHotelsSearch.png?raw=true" alt="HotelsSearch" width="700"> 
      </p>

     Hotel se može dodati u neku od postojećih destinacija. Ako uneseni naziv destinacije nije pornđen u bazi korisniku će biti pružena mogućnost da kreira novu destinaciju ukoliko želi ili da promijeni unos.

     <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/AddHotel1.png?raw=true" alt="Hotels" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/AddHotel2.png?raw=true" alt="HotelsSearch" width=" 300">
      </p>
       <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/DestinationNotFound.png?raw=true" alt="DestinationNotFound" width="  300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/ConfirmAddHotel.png?raw=true" alt="ConfirmHotelAdd" width="  300"> 
      </p>

     Ažuriranje podataka o hotelu moguće je klikom na red u tabeli koji se odnosi na taj hotel, a zatim klikom na **Uredi** dugme.
       <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/UpdateHotel1.png?raw=true" alt="UpdateHotel1" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/UpdateHotel2.png?raw=true" alt="UpdateHotel2" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/ConfirmUpdateHotel.png?raw=true" alt="ConfirmUpdateHotel" width="  300">
      </p>

      Selekcija reda u tabeli i klik na **Obriši** dugme dovešće do prozora za konfirmaciju brisanja hotela.
     
      <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminHotels/ConfirmDeleteHotel.png?raw=true" alt="ConfirmDeleteHotel" width="  300">

   - ### Rad sa aranžmanima
   Svaki aranžman se odnosi na jednu destinaciju i ima određeno vrijeme trajanja definisano datumom početka i kraja. Za istu destinaciju moguće je kreirati više aranžmana. Filtriranje aranžmana vrši se navođenjem naziva destinacije u polje za pretragu.
   <p>
      <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/AdminPackages.png?raw=true" alt="AdminPackages" ><img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/AminPackagesSearch.png?raw=true" alt="AdminPackagesSearch" width="700">
      </p>

   Kao i dosada, proces kreiranja započinje se klikom na dugme **Dodaj**, a zatim se popune polja. U sklopu prozora za kreiranje ponuđena je mogućnost dodavanja nove destinacije, ukoliko ona nije ranije unesena u bazu. 

 <p>
      <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/AddPackage.png?raw=true" alt="AddPackage" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/ConfirmAddPackage.png?raw=true" alt="ConfirmAddPackage" width=" 300"> 
   </p>
      
   Svaki aranžman u ponudi može imati više hotela iz destinacije u kojoj se nalazi, međutim njih je potrebno naknadno dodati. Za upravljanje hotelima u sklopu aranžmana potrebno je izabrati aranžman u tabeli i kliknuti na dugme **Hoteli**. Nakon toga ponuđena je lista svih hotela koji se već nalaze u sklopu aranžmana (ukoliko nema hotela biće ispisana poruka), te mogućnost da dodamo novi hotel ili da obrišemo postojeći. Za brisanje potrebno je selektovati hotel u listi i kliknuti na dugme **Obriši**, a za dodavanje novog na dugme **Dodaj**. U sklopu prozora za dodavanje nalazi se padajući meni sa svim hotelima u destinaciji za koju je aranžman, bez onih koji se već nalaze u ponudi tog aranžmana. Pored ponuđenih hotela administrator ima mogućnost da kreira u bazi unos za novi hotel koji već nije definisan.
   <p>
      <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/HotelsInPackage.png?raw=true" alt="HotelsInPackage" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/HotelInPackageExpanded.png?raw=true" alt="NoHotelsInPackage" width=" 300">  &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/NoHotelsInPackage.png?raw=true" alt="NoHotelsInPackage" width=" 300"> 
      </p>
      <p>
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/AddHotelToPacage.png?raw=true" alt="AddHotelToPackage" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/ConfirmAddHotelToPackage.png?raw=true" alt="ConfirmAddhotelToPackage" width=" 300">  &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/HotelAddedToPackage.png?raw=true" alt="HotelAddedToPackage" width=" 300"> 
      </p>

   Za ažuriranje i brisanje aranžmana prvo ga je potrebno selektovati u tabeli, a zatim kliknuti na **Uredi** i **Obriši** respektivno.
   <p>
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/UpdatePackage1.png?raw=true" alt="UpdatePackage" width=" 300"> &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/UpdatePackage2.png?raw=true" alt="UpdatePackage2" width=" 300">  &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/ConfirmUpdatePackage.png?raw=true" alt="ConfirmUpdatePackage" width=" 300">  &nbsp; <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminPackages/ConfirmDeletePackage.png?raw=true" alt="ConfirmUpdatePackage" width=" 300"> 
      </p>
   
     
   - ### Rad sa rezervacijama
     Administratorska ovlaštenja za rezervacije završavaju na nivou jednostavnog pregleda informacija o rezervacijama i pretraživanja istih na osnovu imena i/ili prezime putnika.

      <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminReservations/AdminReservations.png?raw=true" alt="AdminReservations" > <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AdminReservations/AdminReservationsSearch.png?raw=true" alt="AdminReservationsSearch" width="700">
      </p>

## Agent prodaje
Agent prodaje je tip korisničkog naloga koji ima ovlaštenje da upravlja informacijama o putnicima, rezervacijama i uplatama. Pored toga on ima i uvid u kreirane aranžmane, hotele, pregled svih uplata i pregled uplata za svaku rezervaciju. Početna stavka navigacionog menija koja se aktivira pri otvaranju prozora je "Putnici". 

   - ### Rad sa putnicima
   - ### Rad sa aranžmanima
   - ### Rad sa hotelima
     Za razliku od administratora, agent prodaje nema mogućnost da uređuje podatke o hotelima, ali ima pristup informacijama o svim hotelima koji postoje u bazi. Može i da ih pretražuje prema imenu destinacije.
        <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/SAHotels/SAHotels.png?raw=true" alt="SAHotels" > <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/SAHotels/SAHotelsSearch.png?raw=true" alt="SAHotelsSearch" width="700">
      </p>
   
   - ### Rad sa rezervacijama
     
   - ### Rad sa pojedinačnim uplatama
     Prikaz svih uplata sa važnim podacima o rezervaciji, iznosu, korisniku, kao i agentu prodaje koji je zaključio uplatu. Pretraga se vrši po imenu putnika.
     <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AllPayments/AllPayments.png?raw=true" alt="AllPayments" > <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/AllPayments/AllPaymentsSearch.png?raw=true" alt="AllPaymentsSearch" width="700">
      </p>
      
   - ### Rad sa ukupnim uplatama
     Informacije o ukupnim dosadašnjim uplatama za svaku rezervaciju prikazane u formi liste. Naveden je ID rezervacije, ime putnika, cijena, koliko je uplaćeno, koliko je preostalo da se uplati i da li je uplaćena ukupna suma. Pretragu je moguće izvršiti po imenu putnika.
      <p >
        <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/TotalPayments/TotalPayments.png?raw=true" alt="TotalPayments" > <img src="https://github.com/sofijagligoric/TravelAgency/blob/master/Screenshots/TotalPayments/TotalPaymentsSearch.png?raw=true" alt="TotalPaymentsSearch" width="700">
      </p>
     

## Korištene tehnologije

Za izradu ove aplikacije, korištene su sljedeće tehnologije
- **WPF (Windows Presentation Foundation)** - Za kreiranje korisničkog interfejsa aplikacije.
- **Material Design in XAML Toolkit** - Za primjenu Material Design stilova i ikonica.
- **C#** - Kao programski jezik za implementaciju logike aplikacije.
- **XAML (Extensible Application Markup Language)** - Za definisanje korisničkog interfejsa.
- **MySQL** - Baza podataka.
