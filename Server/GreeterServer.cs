using Contract;
using System.Threading.Tasks;

namespace Server
{
    public class GreeterServer : IGreeter
    {
        public Task<HelloReply> SayHelloAsync(HelloRequest request)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
