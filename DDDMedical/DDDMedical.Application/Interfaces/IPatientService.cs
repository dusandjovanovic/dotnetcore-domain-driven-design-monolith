using System;
using System.Collections.Generic;
using DDDMedical.Application.EventSourcedNormalizers.Patient;
using DDDMedical.Application.ViewModels;

namespace DDDMedical.Application.Interfaces
{
    public interface IPatientService : IDisposable
    {
        void RegisterCovid(PatientViewModel customerViewModel);
        
        void RegisterFlu(PatientViewModel customerViewModel);

        IEnumerable<PatientViewModel> GetAll();

        IEnumerable<PatientViewModel> GetAll(int skip, int take);

        PatientViewModel GetById(Guid id);

        void Remove(Guid id);

        IList<PatientHistoryData> GetAllHistory(Guid id);
    }
}