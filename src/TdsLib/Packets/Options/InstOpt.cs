using System.IO;
using System.Text;
using TdsLib.Packets;

namespace TdsLib.Packets.Options
{
    public class InstOpt : Option
    {
        public string ServerName { get; set; }
        public InstOpt()
        {
            Token = (byte)OptionToken.INSTOPT;
            Length = 1;
            ServerName = string.Empty;
        }

        public override byte[] GetData()
        {
            if (ServerName == string.Empty)
            {
                return new byte[] { 0x00 };
            }

            return Encoding.Unicode.GetBytes(ServerName);
        }

        public override void ReadData(BinaryReader reader)
        {
            if (Length == 1)
            {
                ServerName = string.Empty;
                return;
            }
            var textBytes = reader.ReadBytes(Length);
            ServerName = Encoding.Unicode.GetString(textBytes);
        }

        public override string ToString()
        {
            var serverName = string.IsNullOrEmpty(ServerName) ? "<empty>" : ServerName;
            return $"\n{base.ToString()}" +
                $"{Indent}ServerName: {serverName}";
        }

    }
}