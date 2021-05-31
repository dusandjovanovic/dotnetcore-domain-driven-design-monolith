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

## Sistem za upravljanje medicinskim entitetima

### Entiteti

Sistem je izradjen kao pokazno rešenje za upravljanje medicinskim entitetima, skupovi ovih entiteta su:
1. Lekari - `Doctor` sa atributima `Id`, `Name`, `Email` i `Reservations`.
2. Pacijenti - `Patient` sa atributima `Id`, `Name`, `Email`, `RegistrationDate` i `PatientType`.
3. Konsultacije - `Consultation` sa atributima `Id`, `DoctorId`, `PatientId`, `TreatmentRoomId`, `RegistrationDate` i `ConsultationDate`.
4. Sobe za lečenje - `TreatmentRoom` sa atributima `Id`, `TreatmentMachineId` i `Name`.
5. Mašine za lečenje - `TreatmentMachine` sa atributima `Id`, `TreatmentMachineType` i `Name`.

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
Dogadjaji se koriste kako bi se eksplicitno implementirali 
