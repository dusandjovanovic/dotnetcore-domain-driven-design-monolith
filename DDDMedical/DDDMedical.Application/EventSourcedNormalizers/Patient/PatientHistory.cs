using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Application.EventSourcedNormalizers.Patient
{
    public class PatientHistory
    {
        public static IList<PatientHistoryData> HistoryData { get; set; }

        public static IList<PatientHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<PatientHistoryData>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<PatientHistoryData>();
            var last = new PatientHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new PatientHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    RegistrationDate = string.IsNullOrWhiteSpace(change.RegistrationDate) || change.RegistrationDate == last.RegistrationDate
                        ? ""
                        : change.RegistrationDate.Substring(0, 10),
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
                var slot = new PatientHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "PatientFluRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Name = values["Name"];
                        slot.Email = values["Email"];
                        slot.RegistrationDate = values["RegistrationDate"];
                        slot.Action = "PatientFluRegistered";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "PatientCovidRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Name = values["Name"];
                        slot.Email = values["Email"];
                        slot.RegistrationDate = values["RegistrationDate"];
                        slot.Action = "PatientCovidRegistered";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "PatientRemovedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Action = "PatientRemoved";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}