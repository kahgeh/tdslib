using System;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TdsLib.Errors;
using TdsLib.Exceptions;
using TdsLib.Packets;
using TdsLib.StateMachine.Scaffold;

namespace TdsLib.StateMachine
{
    public class Session
    {
        public UInt32 ClientThreadId { get; set; }
        public DateTime StartUtc { get; }
        public long PacketCount { get; set; }

        public IResult LastResult { get; set; }

        private SemaphoreSlim _stop = new SemaphoreSlim(1, 1);
        public Session()
        {
            Socket socket1;
            StartUtc = DateTime.UtcNow;
        }

        private State Setup()
        {
            var preLogin = new PreLogin();

            var login = new Login();

            var final = new Final();
            preLogin.Links = new[] {
                new Link { ResultName = nameof(PreLoginResponse), NextState = login },
                new Link { ResultName = nameof(InvalidTokenDetected), NextState = final }
             };

            login.Links = new[] {
                new Link { ResultName = nameof(LoginResponse), NextState = final },
                new Link { ResultName = nameof(NothingReturned), NextState = login }
                };
            return preLogin;
        }

        public Task WaitForCancelSignal() => _stop.WaitAsync();

        public void Cancel() => _stop.Release();

        public async Task Serve(
            Func<CancellationToken, Task<(int, byte[])>> getBytes,
            Func<byte[], CancellationToken, Task<int>> sendBytes,
            CancellationToken cancellationToken)
        {

            var first = Setup();
            var state = first;

            while (true)
            {
                Console.WriteLine($"\n{state.GetType().Name} state is waiting for input");
                var result = await state.WaitForInput(getBytes, cancellationToken);
                Console.WriteLine($"Received {JsonSerializer.Serialize(result)}");
                if (result.Error == null)
                {
                    Console.Write($"\nPacket received  \n{result.Packet.ToString()}");
                }
                var output = (LastResult = state.Execute(result, this));
                if (output is InvalidTokenDetected)
                {
                    break;
                }

                var resultName = output.GetType().Name;
                var link = state.Links.FirstOrDefault(l => l.ResultName == resultName);
                if (link == null)
                {
                    throw new NextStateNotFound(state, resultName);
                }

                if (output is Packet)
                {
                    var responsePacket = (Packet)output;
                    Console.Write($"\nResponding with \n{((Packet)output).ToString()}");
                    await sendBytes(responsePacket.ToBytes(), cancellationToken);
                }

                if (link.NextState is Final)
                {
                    LastResult = new Completed();
                    break;
                }

                state = link.NextState;
            }
            Console.WriteLine($"Session finished running, last state was {state.GetType().Name}");
        }
    }
}