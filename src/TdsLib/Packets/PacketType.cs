using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TdsLib.Packets
{
    public enum PacketType
    {
        SqlBatch = 1,
        PreTds7Login = 2,

        Rpc = 3,
        TabularResult = 4,
        AttentionSignal = 6,
        BulkLoadData = 7,
        FederatedAuthenticationToken = 8,
        TransactionManagerRequest = 14,
        Tds7Login = 16,
        Sspi = 17,
        PreLogin = 18,
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