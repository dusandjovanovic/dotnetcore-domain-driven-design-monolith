# Domain-driven design

DDD je pristup koji garantuje smanjivanje kompleksnosti, njačešće u slučaju razvoja poslovnog softvera. U ovom kontekstu, kompleksnost se odnosi na medjusobne veze, veliki broj izvora podataka, poslovne zahteve i slično. DDD je baziran na poslovnom domenu, što znači da treba da oslikava poslovnu logiku i veze izmdju njenih elemenata.

U nastavku je dato nekoliko osnovnih termina:

### Logika domena
Logika je ujedno i svrha modelovanja - najčešće se označava kao "poslovna logika". Ovde pravila poslovanja utiču na upravljanje podacima.

### Model domena
Model podrazumeva ideje, znanje, podatke, metrike i ciljeve koji proizilaze iz problema koji se rešava. Sadrži sva pravila i šablone koji pomažu u savladavanju poslovne logike.

### Pod-domeni
Pod-domeni se odnose na različite delove poslovne logike.

### Ograničeni kontekst
Ovo je ujedno centralni šablon DDD-a koji sadrži svu kompleksnost sistema. Nakon definicije domena i pod-domena, ograničeni konteksti predstavljaju granice u kojima se konkretan domen primenjuje.

### Sveprisutan jezik
Odnosi se na isti "jezik", odnosno termine, koji inženjeri i eksperti domena koriste. Drugim rečima, nije proporučivo koristiti sopstveni žargon već se dogovoriti o jedinstvenom "jeziku".

### Entiteti
Entiteti su kombijacije podataka i ponašanja. Imaju identitete ali predstavljaju tačke podataka sa ponašanjem.

### Objekti vrednosti i agregati
Objekti vrednosti poseduju atribute, ali ne mogu da postoje sami od sebe. Sastavni su deo logike domena u kome se koriste i potrebno je da **budu razdvojeni po logičkim grupama**. Ove grupe predstavljaju kolekciju i nazivaju se agregatima. Osnovna svrha im je upravljanje grupom kao celinom, odnosno **jednom jedinicom**. Dalje, poseduju koren agregata preko koga se pristupa svim elementima grupe.

### Servisi domena
Servisi su dodatni sloj koji sadrži logiku domena. Delovi su modela domena, baš kao i entiteti i objekti vrednosti. Takodje, **servis aplikacije** je još jedan sloj koji nasuprot prethodnog ne sadrži poslovnu logiku. Servis aplikacije koordiniše aktivnostima i postavljen je iznad sloja domena.

### Repozitorijumi
Ovaj šablon predstavlje kolekciju poslovnih entiteta koji uprošćava infrastrukturu podataka. Implementacijom repozitorijuma se model domena oslobadja infrastrukturnih briga. Konceptom raslojavanja postiže se razdvajanje briga.

## Pregled osnovne arhitekture sistema

Arhitektura ovog sistema prati principe DDD-a po kojima je dizajniran odvojeni **sloj domena**.

```
/
  Solution/
    DDDMedical.API/
    DDDMedical.Application/
    DDDMedical.Domain/
    DDDMedical.Domain.Core/
    DDDMedical.Infrastructure.Bus/
    DDDMedical.Infrastructure.Data/
    DDDMedical.Infrastructure.Identity/
    DDDMedical.Infrastructure.Injector/
```

Sloj domena podeljen je u dva projekta - `DDDMedical.Domain.Core` i `DDDMedical.Domain`.

`DDDMedical.Domain.Core` daje osnovne interfejse poput okvira za entitete, dogadjaje, komande i modele.

`DDDMedical.Domain` definiše **sloj domena** i sadrži sve **komande i handler-e komandi**, **dogadjaje i handler-e**, **modele** , a na kraju i **servise**. Kontroleri sistema komunicraju preko servisa. Servisi objavljuju komande. Na kraju, komande obradjuju handler-i koji konsultuju repozitorijume i objavljuju nove dogadjaje. **Jedini nacin promene agregata je dogadjajima domena** koji proizvode takozvane bočne efekte.

Iz ovog razloga, sva validacija i provera ispravnosti dogadjaja/komandi obavlja se u njihovim handler-ima. Pomoćna biblioteka odabrana kao rešenje za validaciju je `FluentValidation`.

![alt text][architecture]

[architecture]: Docs/architecture.png

Na slici se može videti osnovna arhitektura sistema. Po slici postoje jasno deifinisani slojevi. Sloj kontrolera (Application layer) preko servisa komunicira sa nažim slojem domena. Sloj domena, sa druge strane, nalazi se iznad sloja infrastrukture.

Infrastrukturu čine pod-projekti `DDDMedical.Infrastructure.Bus`, `DDDMedical.Infrastructure.Data`, `DDDMedical.Infrastructure.Identity`, `DDDMedical.Infrastructure.Injector`. Ova infrstrukura obezbedjuje perzistenciju podataka, postavljanje magistrale dogadjaja kao i autorizaciju.

Tok obrade zahteva prikazan je na sledećoj slici. Ukoliko se radi o `GET` zahtevima, odmah se prosledjuju do repozitorijuma koja preko infrastrukturnog sloja održavaju stanje agregata. U suprotnom, preko servisa se prave komande koje se validiraju. Ukoliko je validacija uspešna prelazi se na repozitorijume koji menjaju perzistenciju podataka i "podižu" dogadjaj kako bi obavestili sve agregate.

![alt text][flow]

[flow]: Docs/flow.png


## Sistem za upravljanje medicinskim entitetima

### Entiteti i pravila domena

#### Entiteti domena

Sistem je izradjen kao pokazno rešenje za upravljanje medicinskim entitetima, skupovi ovih entiteta su:
1. Lekari - `Doctor` sa atributima `Id`, `Name`, `Email` i `Reservations`. Lekar poseduje ime i osnovne opisne atribute, zajedno sa listom rezervacija u obliku niza datuma kojih je zauzet. Lekari dodatno mogu da budu dva različita tipa (`Pulmonologist` i `GeneralPractitioner`).
2. Pacijenti - `Patient` sa atributima `Id`, `Name`, `Email`, `RegistrationDate` i `PatientType`. Pacijenat može da bude različitog tipa - `Covid19Patient` i `FluPatient`.
3. Konsultacije - `Consultation` sa atributima `Id`, `DoctorId`, `PatientId`, `TreatmentRoomId`, `RegistrationDate` i `ConsultationDate`. Konsultacije su uparivanja izmedju pacijenata, lekara i soba za lečenje. Neophodno je ispoštovati pravila domena poput zakazivanja samo u terminu kada je lekar slobodan.
4. Sobe za lečenje - `TreatmentRoom` sa atributima `Id`, `TreatmentMachineId` i `Name`. Sobe za lečenje mogu biti opremljene različitim mašinama.
5. Mašine za lečenje - `TreatmentMachine` sa atributima `Id`, `TreatmentMachineType` i `Name`. Mašine za lečenje mogu da budu dva tipa (`Advanced` i `Simple`) i dodeljuju se sobama za lečenje.

Dodatno, svi atributi poseduju interne atribute `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy` i nasledjuju klasu osnove svih entiteta pod nazivom `EntityAudit`. Ova klasa se koristi za gradjenje genričkih repozitorijuma koji se proširuju po potrebi konkretnog agregata u specifiziraju pravilima domena.

```csharp
namespace DDDMedical.Domain.Models
{
    public class TreatmentMachine : EntityAudit
    {
    ...atributi entiteta
    ...
}
```

#### Pravila domena

1. Konsultacija traje jedan dan i prema tome, lekar može imati jednu konsultaciju dnevno.
2. U zavisnosti od tipa pacijenta, konsultacija može biti dodeljena samo odredjenom tipu lekara. `Covid19Patient` pacijenti moraju biti dodeljeni lekarima tipa `Pulmonologist`. Suprotno važi za preostalo uparivanje tipova.
3. Jedna soba za lečenje može posedovati veći broj mašina.
4. Kako bi konsultacija za `Covid19Patient` pacijente bila dozvoljena, neophodno je da soba za lečenje poseduje bar jednu mašinu.
5. Ukoliko više pacijenata zakazuje konsultacije istog dana (kod različitih lekara), neophodno je da soba za lečenje poseduje dovoljan broj slobodnih mašina `num(Covid19Patient)x1`.
6. Svi atributi lekara/pacijenata/soba/mašina su neophodni prilikom njihovog dodavanja u sistem.
7. Prilikom brisanja lekara iz sistema, neophodno je da lekar nema zakazane konsultacije u budućnosti.
8. Prilikom brisanja soba/mašina za lečenje iz sistema, takodje je neophodno da nemaju zakazane rezervacije u budućnosti.
9. Prilikom dodavanja lekara u sistem, neophodno je da entitet poseduje jedinstvenu e-mail adresu.
10. Prilikom dodavanja pacdijenata u sistem, takodje je neophodno da entitet poseduje jedinstvenu e-mail adresu.
11. Prilikom dodavanja soba/mašina za lečenje, neophodno je da entiteti poseduju jedinstvena imena.

### Repozitorijumi

Repozitorijumi enkapsuliraju upravljanje grupama entiteta. Svi repozitorijumi implementiraju osnovne upravljačke metode:

```csharp
namespace DDDMedical.Domain.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where TEntity: class
    {
        void Add(TEntity obj);
        
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(ISpecification<TEntity> specification);
        IQueryable<TEntity> GetAllSoftDeleted();

        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }
}
```

A zatim, konkretni repozitorijumi implementiraju posebnu poslovnu logiku, tačnije **logiku domena**. Na primeru repozitorijuma za upravljanje lekarima postoje osnovna pravila domena koja se zadaju dodatnim metodama u vidu provere dostupnosti lekara.

### Dogadjaji domena
Dogadjaji se koriste kako bi se eksplicitno implementirali efekti nad agregatima. Generalno, za svak iskup agregata postoji po nekoliko dogadjaja koji mogu nastupiti.

1. Lekari - `DoctorRegisteredEvent`, `DoctorRemovedEvent`, `DoctorReservedEvent`, `DoctorRegisteredEvent`.
2. Pacijenti - `PatientCovidRegisteredEvent`, `PatientFluRegisteredEvent`, `PatientRemovedEvent`.
3. Konsultacije - `ConsultationRegisteredEvent`.
4. Sobe za lečenje - `TreatmentRoomRegisteredEvent`, `TreatmentRoomRemovedEvent`, `TreatmentRoomReservedEvent`, `TreatmentRoomEquippedWithMachineEvent`.
5. Mašine za lečenje - `DoctorRegisteredEvent`, `DoctorRemovedEvent`, `DoctorReservedEvent`, `DoctorRegisteredEvent`.

## Kontroleri i servisi
Kontroleri su pisani tako da komuniciraju direktno sa servisima domena. Za svaki agregat domena postoji odvojen servis - tako, u slučaju konsultacija postoji servis koji implementira interfejs `IConsultationService`.

```csharp
namespace DDDMedical.Application.Interfaces
{
    public interface IConsultationService : IDisposable
    {
        void Register(ConsultationViewModel customerViewModel);

        IEnumerable<ConsultationViewModel> GetAll();

        IEnumerable<ConsultationViewModel> GetAll(int skip, int take);

        ConsultationViewModel GetById(Guid id);

        IList<ConsultationHistoryData> GetAllHistory(Guid id);
    }
}
```

Ovaj servis se "ubrizgava" u odgovarajući kontroler i koristi za **kreiranje komandi**. Ovim pristup se kontroler oslobadja domenske logike i samo poziva uslužne metode servisa. Kontroleri takodje ostaju veoma kratki i jasni, sva logika odvojena je u sloju domena. Evo primera obrade zahteva zakazivanja konsultacije.

```csharp
[HttpPost]
[AllowAnonymous]
[Route("consultation-management")]
public IActionResult Post([FromBody]ConsultationViewModel consultationViewModel)
{
    if (!ModelState.IsValid)
    {
        NotifyModelStateErrors();
        return Response(consultationViewModel);
    }

    _consultationService.Register(consultationViewModel);

    return Response(consultationViewModel);
}
```

Ovaj zahtev poziva metodu `.Register` servisa. Korišćenjem medijatora se "podiže" dogadjaj `RegisterConsultationCommand`.

```csharp
public void Register(ConsultationViewModel consultationViewModel)
{
    var registerConsultationCommand = _mapper.Map<RegisterConsultationCommand>(consultationViewModel);
    _mediator.SendCommand(registerConsultationCommand);
}
```

### Komande domena
Komande domena pozivaju se od strane servisa, neposredno proizilaze od ruta kontrolera kao što je prethodno objašnjeno.

Za svaku komandu domena postoji zaseban handler koji garantuje njenu obradu. U kodu koji sledi može se videti primer zakazivanja konsultacije. Polazi se od formiranja novog entiteta. Zatim, konsultuju se repozitorijumi entiteta lekara i soba za lečenje.

Ovde se oslanjajući na pravila entiteta nastavlja sa obradom zahteva ili se isti prekida. Na primer, ukoliko je lekar zauzet u traženom terminu dolazi do prekidanja zahteva. Na kraju obrade, izdaju se novi dogadjaji i komande poput `ConsultationRegisteredEvent` i `ReserveTreatmentRoomCommand` koji utiču na promene agregata, odvojeno. Dakle, handler-i komandi konsultuju repozitorijume zarad poštovanja **pravila i ograničenja domena**, a zatim izdaju **dogadjaje domena** koji utiču na promene agregata.

```csharp
public Task<bool> Handle(RegisterConsultationCommand request, CancellationToken cancellationToken)
{
    if (!request.IsValid())
    {
        NotifyValidationErrors(request);
        return Task.FromResult(false);
    }

    var consultation = new Consultation(Guid.NewGuid(), request.DoctorId, request.PatientId, request.TreatmentRoomId, 
        request.RegistrationDate, request.ConsultationDate);

    var treatmentRoom = _treatmentRoomRepository.GetById(request.TreatmentRoomId);

    if (_doctorRepository.IsDoctorReservedByHour(request.DoctorId, request.ConsultationDate))
    {
        _mediator.RaiseEvent(new DomainNotification(request.MessageType, "Doctor's timetable is already taken."));
        return Task.FromResult(false);
    }

    _consultationRepository.Add(consultation);

    if (!Commit()) return Task.FromResult(true);

    _mediator.RaiseEvent(new ConsultationRegisteredEvent(consultation.Id, consultation.PatientId, consultation.DoctorId, 
        consultation.TreatmentRoomId, consultation.RegistrationDate, consultation.ConsultationDate));

    _mediator.SendCommand(new ReserveDoctorCommand(consultation.DoctorId, consultation.ConsultationDate, consultation.Id));

    _mediator.SendCommand(new ReserveTreatmentRoomCommand(consultation.TreatmentRoomId,
        consultation.ConsultationDate, treatmentRoom.TreatmentMachineId));

    return Task.FromResult(true);
}
```

Može se videti da su dogadjaji domena, kao i komande, pisani po jeziku samog domena. Separacija je potpuna jer dogadjaji koji se odnose na lekare utiču samo na promene agregata lekara - isti princip važi za sve ostale entitete.

### Sloj perzistencije

Za perzistenciju se koristi `mssql_server` baza podataka, dok je mapiranje ostvareno korišćenjem rešenja `EntityFrameworkCore`. Mapiranje se ostvaruje kroz dva konteksta, prvi kontekst sumira sve entitete koji predstavljaju modele i naziva se `ApplicationDbContext`, a koristi se i pomoćni kontekst za perzistenciju dogadjaja.

```csharp
namespace DDDMedical.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<TreatmentMachine> TreatmentMachines { get; set; }
        public DbSet<TreatmentRoom> TreatmentRooms { get; set; }
        
        ...
```

Za komunikaciju sa izvorom podataka neophodno je navesti parametre u stringu koji opisuje konekciju koji će ostvariti vezu sa *driver-om*. Ukoliko se koristi `docker` kontejner `mcr.microsoft.com/mssql/server:2019-latest` sa podrazumevanim podešavanjima parametar konekcije je `Server=localhost,1433;Database=medical;MultipleActiveResultSets=true;User=sa;Password=yourStrong(!)Password`.

```csharp
services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
            x => x.MigrationsAssembly("DDDMedical.API"));

        if (env.IsProduction()) return;
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    });
```

## Pokretanje sistema

Sistem se pokreće nakon build-ovanja pod-projekta `DDDMedical.API` koji predstavlja aplikativni sloj. Ovaj sloj postavlja kontrolere i inicijalizuje `ApplicarionService` servis koji komunicira sa **nižim domenskim slojem**.

Neophodno je pre svega izvršiti migracije nad bazom podataka:
`$ dotnet ef migrations add InitialCreate --context ApplicationDbContext`
`$ dotnet ef database update --context ApplicationDbContext`

Migracije se vrše iz pomenutog API pod-projekta jer je označen kao `MigrationsAssembly`, iako su konteksti napisani u domenskom pod-projektu.

Na kraju, aplikativni sloj sadrži i `swagger` interfejs preko koga se mogu koristiti kontroleri.

![alt text][user_interface]

[user_interface]: Docs/user_interface.png
