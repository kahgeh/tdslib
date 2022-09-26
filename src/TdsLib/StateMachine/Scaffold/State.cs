using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TdsLib.Errors;
using TdsLib.Packets;

namespace TdsLib.StateMachine.Scaffold
{
    public abstract class State
    {
        public IEnumerable<Link> Links { get; set; }
        public IEnumerable<PacketType> AllowPacketTypes { get; set; }
        public async Task<TdsReadResult> WaitForInput(
            Func<CancellationToken, Task<(int, byte[])>> getBytes,
            CancellationToken cancellationToken)
        {
            var (byteCount, bytes) = await getBytes(cancellationToken);
            if (byteCount < 1)
            {
                return new TdsReadResult
                {
                    Error = new NothingReturned()
                };
            }
            Array.Resize(ref bytes, byteCount);
            return TdsStream.Read(bytes, AllowPacketTypes);
        }

        public abstract IResult Execute(TdsReadResult tdsReadResult, Session session);
    }
}