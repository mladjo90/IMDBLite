# IMDBLite
Lite movie rating engine

Aplikacija koja je napravljena je Lite Movie engine. U sustini korisnik ima mogucnost da vidi 
filmove ili serije poredane po prosjecnoj ocjeni (od najvise ka najnizoj).

Takodje ima dodatnu mogucnost da vrsi ocjenjivanje filmova ili serija.

RESENJE:

Solution se sastoji od 7 projekata:

*IMDBLite.API.DataModels*
	Sadrzi modele podataka iz baze, helper klase za izvrsavanje stored procedura, kao i dodatnu
	klasu za izvlacenje podataka iz User Secrets okruzenja.

*IMDBLite.API.BLL*
	Sadrzi interface-e i implementacije Base repository-ja kao i dodatne interfce-e i implementacije
	svih modela iz baze podataka, ukoliko bi se ukazala potreba za nekim specificnim radnjama nad odredjenim
	repositorijumima.
	Base repo sadrzi osnovne radnje tipa FindAll, Insert, Delete, Update...
	
*IMDBLite.Server* 
	Predstavlja API dio solution-a koji je zaduzen za logiku i pribavljanje podataka.
	Dodatno se tu nalazi AuthAPIControler.(O njemu bih volio popricati na tehnickom razgovoru
	ukoliko do njega dodje).
	U sustini pravio sam authorizaciju client aplikacije na API (client credentials flow), medjutim,
	u Blazoru sam naisao na sledeci problem gdje se WebAssembly aplikacija skida na korisnikov racunar
	i kao takva otvara mogucnost dohvatanja podataka za logovanje. Na ovaj nacin sam sakrio te podatke, ali
	otvorio nove probleme. (Rijec je o Auth0)
	
*IMDBLite.DTO*
	Sadrzi Data Transfer Request i Response objekte.

*IMDBLite.Client*
	Klijenta aplikacija koja sadrzi pages i lokigu ponasanja istih
	
*IMDBLite.Shared*
	Sadrzao bi neke enume i stvari koje se koriste izmedju ostalih projekata.
	Recimo sadrzi sada class-u koja sluzi kao model za paginaciju.
	
*IMDBLite.ServiceClientContracts*
	Predstavlja vezu izmedju klijenta i API-ja. Klijent poziva ServiceClientContractor koji dalje
	salje pozive ka Apiju aplikacije.
	
Autentfikaciju korisnika nisam stigao zavrsiti, ali ona bi se mogla ubaciti isto preko Auth0.

Dodatno export baze se nalazi takodje unutar git-a, file pod nazivom imdblite.sql
Nakon restora baze, potrebno je u appsettings.json unutar IMDBLite.Server projekta izmjeniti *** 
sa pravim podacima kako bi se aliplikacija nakacila na bazu.