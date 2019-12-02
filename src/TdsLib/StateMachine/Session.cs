using System;
using System.Linq;
using System.Net.Sockets;
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
        public DateTime StartUtc { get; }
        public long PacketCount { get; set; }

        public IResult LastResult { get; set; }

        private SemaphoreSlim _stop = new SemaphoreSlim(1, 1);
        public Session()
        {
            StartUtc = DateTime.UtcNow;
        }

        private State Setup()
        {
            var preLogin = new PreLogin();

            var login = new Login();

            var end = new End();
            preLogin.Links = new[] { new Link { ResultName = nameof(PreLoginResponse), NextState = login } };
            preLogin.Links = new[] { new Link { ResultName = nameof(InvalidTokenDetected), NextState = end } };

            login.Links = new[] { new Link { ResultName = nameof(LoginResponse), NextState = end } };
            return preLogin;
        }

        public Task WaitForCancelSignal() => _stop.WaitAsync();

        public void Cancel() => _stop.Release();

        public async Task Serve(Func<Task<(int, byte[])>> getBytes)
        {
            var session = new Session();

            var first = Setup();
            var state = first;

            while (true)
            {
                var result = await state.WaitForInput(getBytes);

                var output = (LastResult = state.Execute(result, session));
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

                if (link.NextState is End)
                {
                    break;
                }

                state = link.NextState;
            }
        }
    }
}