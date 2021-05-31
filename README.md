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

## Sistem za upravljanje medicinskim entitetima

### Entiteti i pravila domena

Sistem je izradjen kao pokazno rešenje za upravljanje medicinskim entitetima, skupovi ovih entiteta su:
1. Lekari - `Doctor` sa atributima `Id`, `Name`, `Email` i `Reservations`. Lekar poseduje ime i osnovne opisne atribute, zajedno sa listom rezervacija u obliku niza datuma kojih je zauzet. Lekari dodatno mogu da imaju dva tipa (`Pulmonologist` i `GeneralPractitioner`).
2. Pacijenti - `Patient` sa atributima `Id`, `Name`, `Email`, `RegistrationDate` i `PatientType`. Pacijenat može da bude različitog tipa (poput `Covid19Patient` i `FluPatient`). U zavisnosti od tipa pacijenat može biti dodeljen samo jednom tipu lekara. `Covid19Patient` pacijenti moraju biti dodeljeni lekarima tipa `Pulmonologist`.
3. Konsultacije - `Consultation` sa atributima `Id`, `DoctorId`, `PatientId`, `TreatmentRoomId`, `RegistrationDate` i `ConsultationDate`. Konsultacije su uparivanja izmedju pacijenata, lekara i soba za lečenje. Neophodno je ispoštovati pravila domena poput zakazivanja samo u terminu kada je lekar slobodan. Konsultacija traje jedan ceo dan i prema tome, lekar može imati jednu konsultaciju dnevno.
4. Sobe za lečenje - `TreatmentRoom` sa atributima `Id`, `TreatmentMachineId` i `Name`. Sobe za lečenje mogu biti opremljene različitim mašinama.
5. Mašine za lečenje - `TreatmentMachine` sa atributima `Id`, `TreatmentMachineType` i `Name`. Mašine za lečenje mogu da budu dva tipa (`Advanced` i `Simple`) i dodeljuju se sobama za lečenje.

Dodatno, svi atributi poseduju interne atribute `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy` i nasledjuju klasu osnove svih entiteta.

```csharp
namespace DDDMedical.Domain.Models
{
    public class TreatmentMachine : EntityAudit
    {
    ...atributi entiteta
    ...
}
```

### Repozitorijumi

Repozitorijumi enkapsuliraju upravljanje jednom grupom entitea. Svi repozitorijumi implementiraju osnovne upravljačke metode:

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

A zatim, konkretni repozitorijumi implementiraju posebnu poslovnu logiku. Na primeru repozitorijuma za upravljanje lekarima postoje osnovna pravila domena koja se zadaju dodatnim metodama u vidu provere dostupnosti lekara.

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
