using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TdsLib.StateMachine;

namespace Echotds
{


    public class Server
    {
        // Thread signal.  
        static CancellationTokenSource _terminate;
        static ManualResetEvent _allDone;
        static Task _terminateTask;
        static Server()
        {
            _terminate = new CancellationTokenSource();
            _terminateTask = Task.Delay(TimeSpan.FromMilliseconds(-1), _terminate.Token);
            _allDone = new ManualResetEvent(false);
        }
        public Server()
        {
        }

        public static async System.Threading.Tasks.Task Listen()
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var localEndPoint = new IPEndPoint(ipAddress, 1432);
            // Create a TCP/IP socket.  
            var listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    try
                    {
                        var acceptTask = listener.AcceptAsync();
                        await Task.WhenAny(acceptTask, _terminateTask);
                        var socket = acceptTask.Result;
                        await HandleSession(socket);
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Termination signal received");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static async Task HandleSession(Socket socket)
        {
            var session = new Session();

            Func<CancellationToken, Task<(int, byte[])>> getBytes = async (CancellationToken cancellationToken) =>
            {
                var bytes = new byte[4096];
                var receivedByteCount = await socket
                                    .ReceiveAsync(bytes, SocketFlags.None, cancellationToken)
                                    .AsTask();

                return (receivedByteCount, bytes);
            };

            Func<byte[], CancellationToken, Task<int>> sendBytes = async (byte[] bytes, CancellationToken cancellationToken) =>
            {
                var sentByteCount = await socket
                                    .SendAsync(bytes, SocketFlags.None, cancellationToken)
                                    .AsTask();

                return sentByteCount;
            };

            await session.Serve(
                getBytes,
                sendBytes,
                _terminate.Token);

            socket.Close();
        }

        public static int Main(String[] args)
        {
            _allDone.Reset();
            Listen()
                .ContinueWith((task) =>
                {
                    Console.WriteLine("Server shutdown");
                    _allDone.Set();
                    return Task.CompletedTask;
                });
            var connectionString = "Encrypt=false;Application Name=EchoTds;data source=127.0.0.1,1432;initial catalog=DummyDb;User ID=DummyUser;Password=Dummy!1;persist security info=False;packet size=4096;";
            var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                var param = new DynamicParameters();
                param.Add("@param1", 0);
                param.Add("@param2", "text");
                param.Add("@Export", true);

                var sampleData = connection.Query<SampleData>("[dbo].[StoredProcedure1]", commandType: CommandType.StoredProcedure, param: param);
                connection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error openning sql connection");
            }
            Console.WriteLine("\nPress ENTER to end...");
            Console.Read();
            _terminate.Cancel();
            _allDone.WaitOne();
            Console.WriteLine("\nend");
            return 0;
        }


    }
}
