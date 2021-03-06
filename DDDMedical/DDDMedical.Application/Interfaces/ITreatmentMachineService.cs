using System;
using System.Collections.Generic;
using DDDMedical.Application.EventSourcedNormalizers.TreatmentMachine;
using DDDMedical.Application.ViewModels;

namespace DDDMedical.Application.Interfaces
{
    public interface ITreatmentMachineService : IDisposable
    {
        void RegisterSimple(TreatmentMachineViewModel customerViewModel);
        
        void RegisterAdvanced(TreatmentMachineViewModel customerViewModel);

        IEnumerable<TreatmentMachineViewModel> GetAll();

        IEnumerable<TreatmentMachineViewModel> GetAll(int skip, int take);

        TreatmentMachineViewModel GetById(Guid id);

        IList<TreatmentMachineHistoryData> GetAllHistory(Guid id);
    }
}