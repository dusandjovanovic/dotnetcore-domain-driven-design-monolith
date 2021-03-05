using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Application.EventSourcedNormalizers.Consultation
{
    public class ConsultationHistory
    {
        public static IList<ConsultationHistoryData> HistoryData { get; set; }

        public static IList<ConsultationHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<ConsultationHistoryData>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<ConsultationHistoryData>();
            var last = new ConsultationHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new ConsultationHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    DoctorId = change.DoctorId == Guid.Empty.ToString() || change.DoctorId == last.DoctorId
                        ? ""
                        : change.DoctorId,
                    PatientId = change.PatientId == Guid.Empty.ToString() || change.PatientId == last.PatientId
                        ? ""
                        : change.PatientId,
                    TreatmentRoomId = change.TreatmentRoomId == Guid.Empty.ToString() || change.TreatmentRoomId == last.TreatmentRoomId
                        ? ""
                        : change.TreatmentRoomId,
                    RegistrationDate = string.IsNullOrWhiteSpace(change.RegistrationDate) || change.RegistrationDate == last.RegistrationDate
                        ? ""
                        : change.RegistrationDate.Substring(0, 10),
                    ConsultationDate = string.IsNullOrWhiteSpace(change.ConsultationDate) || change.ConsultationDate == last.ConsultationDate
                        ? ""
                        : change.ConsultationDate.Substring(0, 10),
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };
                
                list.Add(jsSlot);
                last = change;
            }

            return list;
        }
        
        private static void HistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new ConsultationHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "ConsultationRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.DoctorId = values["DoctorId"];
                        slot.PatientId = values["PatientId"];
                        slot.TreatmentRoomId = values["TreatmentRoomId"];
                        slot.RegistrationDate = values["RegistrationDate"];
                        slot.ConsultationDate = values["ConsultationDate"];
                        slot.Action = "ConsultationRegistered";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}