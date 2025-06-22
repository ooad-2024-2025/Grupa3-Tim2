# 🎂 Cake Factory – Web aplikacija za naručivanje kolača

**🔗 Deploy link:**  
http://slasticarna-001-site1.mtempurl.com/

---

## 📖 O projektu

Ova aplikacija predstavlja web sistem za slastičarnu sa sjedištem u Sarajevu, specijaliziranu za **tradicionalne i orijentalne kolače**.  
Cilj je omogućiti korisnicima jednostavno i brzo naručivanje proizvoda, pregled prazničnih ponuda, upravljanje narudžbama i korisničkim profilima, kao i administraciju proizvoda, popusta i statistika putem administratorskog panela.

### ✨ Funkcionalnosti

- 📋 Pregled svih proizvoda sa slikama i cijenama
- 🛒 Naručivanje proizvoda (samo za registrovane korisnike)
- 👤 Login, registracija i korisnički profil
- 🎁 Praznične ponude i popusti
- 📈 Admin panel sa statistikom, upravljanjem ponudama i proizvodima
- 📬 Slanje potvrda narudžbi na e-mail

---

## ⚙️ Tehnologije

- **Backend:** ASP.NET Core MVC (.NET 7)
- **Frontend:** Razor views + Bootstrap + ApexCharts
- **Baza podataka:** MySQL (hostovana na SmarterASP.NET)
- **Hosting:** SmarterASP.NET (besplatni hosting na domeni `.mtempurl.com`)
- **Email servis:** SMTP (za slanje potvrda)
- **Deployment:** ručni upload putem File Managera

---

## 🚀 Pokretanje aplikacije lokalno

### 1. Kloniranje repozitorija

```bash
git clone https://github.com/ooad-2024-2025/Grupa3-Tim2.git
```

### 2. Otvori projekat u Visual Studiju

1. Otvori `.sln` fajl u Visual Studiju (`Slasticarna.sln`)
2. Postavi `Slasticarna` kao **Startup projekt**
3. Provjeri da je postavljen **ASP.NET Core 7.0 SDK** 

---

## ⚙️ Konfiguracija baze podataka

Baza je **MySQL**, hostovana na [SmarterASP.NET](https://www.smarterasp.net/).

U `appsettings.json` fajlu moraš unijeti odgovarajuće pristupne podatke:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=mysql5008.smarterasp.net;user=USERSMRT123;password=***;database=db_xyz;"
}
```

## ❗ Trenutni problemi sa aplikacijom

### 🚫 500 - Internal Server Error

Trenutna verzija aplikacije (na linku:  
🔗 [http://slasticarna-001-site1.mtempurl.com/](http://slasticarna-001-site1.mtempurl.com/)) ima problema sa konekcijom baze.

### 🛠️ Uzrok greške

Greška se pojavljuje zbog problema s konekcijom na bazu podataka. Tačnije:

- **SmarterASP.NET** hosting platforma je označila konekcijski string kao **sigurnosni rizik**, jer vjeruju da je lozinka baze hardkodirana unutar aplikacije.
- Zbog toga su **blokirali pristup aplikacije prema bazi**, iako baza fizički postoji i dostupna je kroz njihov Data Manager.
- Aplikacija koristi `ASP.NET Core` i pokušava da učita podatke prilikom pokretanja (npr. za prikaz proizvoda, korisnika i sl.), ali ne može pristupiti bazi, što dovodi do **fatalne greške prilikom startanja** (`500.30`, `Startup failure`).
- Trenutno **nemamo ovlasti** na SmarterASP.NET da ručno omogućimo remote pristup bazi ili promijenimo sigurnosna ograničenja za app pool.

### 📌 Zaključak

- **Deploy aplikacije je tehnički uspješan** – svi fajlovi su publishani, server ih prepoznaje i pokreće `.dll`.
- **Problem je isključivo u odbijanju konekcije ka bazi od strane hostinga**, zbog čega ni jedan backend dio ne može učitati podatke.
- Neke stranice (npr. `Home` ili statički sadržaji) bi se mogle prikazati da nije direktne baze u `_Layout` i drugim servisima.
- Ova greška nije posljedica lošeg koda, već **restrikcija hostinga** koje tim nije mogao zaobići zbog nedostatka administrativnih ovlasti.

---

