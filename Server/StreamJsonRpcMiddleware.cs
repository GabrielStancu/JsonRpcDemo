using Microsoft.AspNetCore.Http;
using StreamJsonRpc;
using System.Threading.Tasks;

namespace Server
{
    public class StreamJsonRpcMiddleware
    {
        public StreamJsonRpcMiddleware(RequestDelegate next)
        { }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();

                IJsonRpcMessageHandler jsonRpcMessageHandler = new WebSocketMessageHandler(webSocket);

                using (var jsonRpc = new JsonRpc(jsonRpcMessageHandler, new GreeterServer()))
                {
                    jsonRpc.StartListening();

                    await jsonRpc.Completion;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
