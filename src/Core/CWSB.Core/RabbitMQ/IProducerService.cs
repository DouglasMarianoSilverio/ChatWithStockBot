using CWSB.Core.Models;
using System.Threading.Tasks;

namespace CWSB.Core.RabbitMQ
{
    public interface IProducerService
    {
        Task Produce(Post postMessage);
    }
}