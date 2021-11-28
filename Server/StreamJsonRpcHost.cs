//using Microsoft.AspNetCore.Connections;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using StreamJsonRpc;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Server
//{
//    public class StreamJsonRpcHost : BackgroundService 
//    {
//        private readonly IConnectionListenerFactory _connectionListenerFactory;
//        private readonly ConcurrentDictionary<string, (ConnectionContext Context, Task ExecutionTask)> _connections =
//            new ConcurrentDictionary<string, (ConnectionContext, Task)>();
//        private readonly ILogger<StreamJsonRpcHost> _logger;

//        private IConnectionListener _connectionListener;

//        public StreamJsonRpcHost(IConnectionListenerFactory connectionListenerFactory, ILogger<StreamJsonRpcHost> logger)
//        {
//            _connectionListenerFactory = connectionListenerFactory;
//            _logger = logger;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _connectionListener = await _connectionListenerFactory.BindAsync(new IPEndPoint(IPAddress.Loopback, 6000), stoppingToken);

//            while (true)
//            {
//                ConnectionContext connectionContext = await _connectionListener.AcceptAsync(stoppingToken);

//                if (connectionContext == null)
//                {
//                    break;
//                }

//                _connections[connectionContext.ConnectionId] = (connectionContext, AcceptAsync(connectionContext));
//            }

//            List<Task> connectionsExecutionTasks = new List<Task>(_connections.Count);

//            foreach (var connection in _connections)
//            {
//                connectionsExecutionTasks.Add(connection.Value.ExecutionTask);
//                connection.Value.Context.Abort();
//            }

//            await Task.WhenAll(connectionsExecutionTasks);
//        }

//        public override async Task StopAsync(CancellationToken cancellationToken)
//        {
//            await _connectionListener.DisposeAsync();
//        }

//        private async Task AcceptAsync(ConnectionContext connectionContext)
//        {
//            try
//            {
//                await Task.Yield();

//                IJsonRpcMessageFormatter jsonRpcMessageFormatter = new JsonMessageFormatter(Encoding.UTF8);
//                IJsonRpcMessageHandler jsonRpcMessageHandler = new LengthHeaderMessageHandler(
//                    connectionContext.Transport,
//                    jsonRpcMessageFormatter);

//                using (var jsonRpc = new JsonRpc(jsonRpcMessageHandler, new GreeterServer()))
//                {
//                    jsonRpc.StartListening();

//                    await jsonRpc.Completion;
//                }

//                await connectionContext.ConnectionClosed.WaitAsync();
//            }
//            catch
//            {

//            }
//            finally
//            {
//                await connectionContext.DisposeAsync();

//                _connections.TryRemove(connectionContext.ConnectionId, out _);
//            }
//        }
//    }
//}
