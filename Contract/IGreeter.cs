using System.Threading.Tasks;

namespace Contract
{
    public interface IGreeter
    {
        Task<HelloReply> SayHelloAsync(HelloRequest request);
    }
}
