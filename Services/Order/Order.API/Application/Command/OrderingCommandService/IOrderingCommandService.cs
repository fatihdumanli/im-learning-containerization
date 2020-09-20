using System.Threading.Tasks;

namespace Ordering.API.Application.Command.OrderingCommandService
{
    public interface IOrderingCommandService
    {
        Task SendCommand(DomainDispatching.Commanding.Command command);
    }
}