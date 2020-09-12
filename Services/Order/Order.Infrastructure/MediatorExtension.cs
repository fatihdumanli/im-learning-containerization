using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Ordering.Domain.SharedKernel;

namespace Ordering.Infrastructure
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventAsync(this IMediator mediator, OrderingContext ctx)
        {
       
            //butun domain eventları mediator aracılıgıyla publish edecek.

        }

    }
}