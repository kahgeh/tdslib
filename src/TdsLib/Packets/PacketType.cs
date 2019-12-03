using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TdsLib.Packets
{
    public enum PacketType
    {
        SqlBatch = 0x01,
        PreTds7Login = 0x02,

        Rpc = 0x03,
        TabularResult = 0x04,
        AttentionSignal = 0x06,
        BulkLoadData = 0x07,
        FederatedAuthenticationToken = 0x08,
        TransactionManagerRequest = 0x0e,
        Tds7Login = 0x10,
        Sspi = 0x11,
        PreLogin = 0x12,
    }

    public static class PacketTypeExtensions
    {
        public static string GetName(this PacketType? packetType)
        {
            if (!packetType.HasValue)
            {
                return "Invalid packet type";
            }
            return Enum.GetName(typeof(PacketType), packetType.Value);
        }
    }

    public static class PacketTypesExtensions
    {
        public static string GetCommaDelimitedText(this IEnumerable<PacketType> packetTypes)
        {
            return
                string.Join(",", packetTypes.Select(packetType => Enum.GetName(typeof(PacketType), packetType)));
        }
    }
}