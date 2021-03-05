using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Application.EventSourcedNormalizers.TreatmentRoom
{
    public class TreatmentRoomHistory
    {
        public static IList<TreatmentRoomHistoryData> HistoryData { get; set; }

        public static IList<TreatmentRoomHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<TreatmentRoomHistoryData>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<TreatmentRoomHistoryData>();
            var last = new TreatmentRoomHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new TreatmentRoomHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    TreatmentMachineId = change.TreatmentMachineId == Guid.Empty.ToString() || change.TreatmentMachineId == last.TreatmentMachineId
                        ? ""
                        : change.TreatmentMachineId,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    ReservationDay = string.IsNullOrWhiteSpace(change.ReservationDay) || change.ReservationDay == last.ReservationDay
                        ? ""
                        : change.ReservationDay.Substring(0, 10),
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
                var slot = new TreatmentRoomHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "TreatmentRoomRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.TreatmentMachineId = values["TreatmentMachineId"];
                        slot.Name = values["Name"];
                        slot.Action = "TreatmentRoomRegistered";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "TreatmentRoomEquippedWithMachineEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.TreatmentMachineId = values["DoctorId"];
                        slot.Action = "TreatmentRoomEquippedWithMachine";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "TreatmentRoomRemovedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Action = "TreatmentRoomRemoved";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "TreatmentRoomReservedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.TreatmentMachineId = values["TreatmentMachineId"];
                        slot.ReservationDay = values["ReservationDay"];
                        slot.Action = "TreatmentRoomReserved";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}