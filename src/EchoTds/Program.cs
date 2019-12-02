using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TdsLib.Packets;
using TdsLib.StateMachine;

namespace Echotds
{


    public class Server
    {
        // Thread signal.  
        static SemaphoreSlim _stop = new SemaphoreSlim(1, 1);
        static ManualResetEvent _allDone = new ManualResetEvent(false);
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
                    var acceptTask = listener.AcceptAsync();
                    await Task.WhenAny(acceptTask, _stop.WaitAsync());
                    if (acceptTask.Status != TaskStatus.RanToCompletion)
                    {
                        break;
                    }

                    var socket = acceptTask.Result;
                    await HandleSession(socket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static async Task HandleSession(Socket socket)
        {
            byte[] bytes = new byte[1024];
            var session = new Session();

            Func<Task<(int, byte[])>> getBytes = async () =>
            {
                var bytes = new byte[4096];
                var receiveTask = socket.ReceiveAsync(bytes, SocketFlags.None);
                await Task.WhenAny(receiveTask, session.WaitForCancelSignal());
                if (receiveTask.Status != TaskStatus.RanToCompletion)
                {
                    return (-1, null);
                }
                var receivedByteCount = receiveTask.Result;
                return (receivedByteCount, bytes);
            };

            await session.Serve(getBytes);
            socket.Close();
        }



        public static int Main(String[] args)
        {
            _stop.WaitAsync();
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
                connection.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error openning sql connection");
            }
            Console.WriteLine("\nPress ENTER to end...");
            Console.Read();
            _stop.Release();
            _allDone.WaitOne();
            Console.WriteLine("\nend");
            return 0;
        }


    }
}
