using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Application.EventSourcedNormalizers.Doctor
{
    public class DoctorHistory
    {
        public static IList<DoctorHistoryData> HistoryData { get; set; }

        public static IList<DoctorHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<DoctorHistoryData>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<DoctorHistoryData>();
            var last = new DoctorHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new DoctorHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    ReferenceId = change.ReferenceId == Guid.Empty.ToString() || change.ReferenceId == last.ReferenceId
                        ? ""
                        : change.ReferenceId,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    Roles = string.IsNullOrWhiteSpace(change.Roles) || change.Roles == last.Roles
                        ? ""
                        : change.Roles,
                    Reservations = string.IsNullOrWhiteSpace(change.Reservations) || change.Reservations == last.Reservations
                        ? ""
                        : change.Reservations,
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
                var slot = new DoctorHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "DoctorRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Name = values["Name"];
                        slot.Email = values["Email"];
                        slot.Roles = values["Roles"];
                        slot.Reservations = values["Reservations"];
                        slot.Action = "DoctorRegistered";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "DoctorRemovedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Action = "DoctorRemoved";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "DoctorReservedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.ReferenceId = values["ReferenceId"];
                        slot.ReservationDay = values["ReservationDay"];
                        slot.Action = "DoctorReserved";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}