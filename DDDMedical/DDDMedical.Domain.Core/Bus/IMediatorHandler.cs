using System.Threading.Tasks;
using DDDMedical.Domain.Core.Commands;
using DDDMedical.Domain.Core.Events;

namespace DDDMedical.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}