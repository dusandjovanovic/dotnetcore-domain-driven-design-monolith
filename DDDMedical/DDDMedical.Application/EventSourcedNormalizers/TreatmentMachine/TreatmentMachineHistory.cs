using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Application.EventSourcedNormalizers.TreatmentMachine
{
    public class TreatmentMachineHistory
    {
        public static IList<TreatmentMachineHistoryData> HistoryData { get; set; }

        public static IList<TreatmentMachineHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<TreatmentMachineHistoryData>();
            HistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<TreatmentMachineHistoryData>();
            var last = new TreatmentMachineHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new TreatmentMachineHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
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
                var slot = new TreatmentMachineHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "TreatmentMachineAdvancedRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Name = values["Name"];
                        slot.Action = "TreatmentMachineAdvancedRegistered";
                        slot.When = values["Timestamp"];
                        slot.Who = e.User;
                        break;
                    case "TreatmentMachineSimpleRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Id = values["Id"];
                        slot.Name = values["Name"];
                        slot.Action = "TreatmentMachineSimpleRegistered";
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