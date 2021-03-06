using System;
using System.Collections.Generic;
using DDDMedical.Application.EventSourcedNormalizers.Consultation;
using DDDMedical.Application.ViewModels;

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