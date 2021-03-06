using System;
using System.Collections.Generic;
using DDDMedical.Application.EventSourcedNormalizers.TreatmentRoom;
using DDDMedical.Application.ViewModels;

namespace DDDMedical.Application.Interfaces
{
    public interface ITreatmentRoomService : IDisposable
    {
        void Register(TreatmentRoomViewModel customerViewModel);
        
        void Reserve(TreatmentRoomViewModel customerViewModel);
        
        void Equip(TreatmentRoomViewModel customerViewModel);

        IEnumerable<TreatmentRoomViewModel> GetAll();

        IEnumerable<TreatmentRoomViewModel> GetAll(int skip, int take);

        TreatmentRoomViewModel GetById(Guid id);

        void Remove(Guid id);

        IList<TreatmentRoomHistoryData> GetAllHistory(Guid id);
    }
}