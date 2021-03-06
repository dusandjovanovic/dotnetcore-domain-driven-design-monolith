using System;
using System.Collections.Generic;
using DDDMedical.Application.EventSourcedNormalizers.Doctor;
using DDDMedical.Application.ViewModels;

namespace DDDMedical.Application.Interfaces
{
    public interface IDoctorService : IDisposable
    {
        void Register(DoctorViewModel customerViewModel);

        IEnumerable<DoctorViewModel> GetAll();

        IEnumerable<DoctorViewModel> GetAll(int skip, int take);

        DoctorViewModel GetById(Guid id);

        void Reserve(DoctorViewModel customerViewModel);

        void Remove(Guid id);

        IList<DoctorHistoryData> GetAllHistory(Guid id);
    }
}