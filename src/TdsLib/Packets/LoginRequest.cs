using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TdsLib.Errors;
using TdsLib.Packets.Login7;
using TdsLib.Utility;
using static TdsLib.Packets.Login7.VariableNames;

namespace TdsLib.Packets
{
    public class LoginRequest : RequestPacket
    {
        public Login7.Header Header { get; set; }
        public Data[] Data { get; }
        public byte[] Raw { get; set; }
        public LoginRequest()
            : base(PacketType.Tds7Login)
        {
            LoadingProgresses.Add(nameof(ReadBody), 0x02);
            Data = new Data[]{
                new TextVariable(HostName),
                new TextVariable(UserName),
                new Password(),
                new TextVariable(AppName),
                new TextVariable(ServerName),
                new TextVariable(Reserved),
                new TextVariable(LibraryName),
                new TextVariable(Locale),
                new TextVariable(DatabaseName)
            };
        }
        protected override IncompletePacket ReadBody(BinaryReader reader)
        {
            var lengthBytes = reader.ReadBytes(4);
            var length = BitConverter.ToUInt32(lengthBytes);
            var Raw = lengthBytes.Concat(reader.ReadBytes((int)length)).ToArray();//todo ensure it's safe to cast
            if (Raw.Length != length)
            {
                return new IncompletePacket(Raw, this);
            }
            LoadingProgress = LoadingProgresses[nameof(ReadBody)];
            using (var stream = new MemoryStream(Raw))
            using (var bodyReader = new BinaryReader(stream))
            {
                Header = new Header(length);
                Header.Read(bodyReader);

                foreach (var datum in Data)
                {
                    datum.OffsetLength.Read(bodyReader);
                }
            }
            Raw.HexDump();
            foreach (var datum in Data)
            {
                datum.Read(Raw);
            }
            return null;
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   $"  Data Header\n" +
                   $"{Header.ToString()}" +
                   $"  Data\n" +
                   $"{Data.GetDisplayText()}\n";
        }
    }
}