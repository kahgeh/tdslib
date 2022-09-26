namespace TdsLib.Packets.Login7.Response
{
    public enum EnvChangeType
    {
        Database = 0x01,
        Language,
        CharacterSet,
        PacketSize,
        UnicodeDataSortingLocalId,
        UnicodeDatSortingComparisonFlags,
        SqlCollation,
        BeginTransaction,
        CommitTransaction,
        RollbackTransaction,
        EnlistDTCTransaction,
        DefectTransaction,
        RealTimeLogShipping,
        PromoteTransaction,
        TransactionManagerAddress,
        TransactionEnded,
        ConnectionResetAck,
        UserInstanceNameOnLoginRequest,
        SendsRoutingInformation
    }
}