namespace TdsLib.Utility
{
    public static class LoadingProgressExtensions
    {
        public static string GetDisplayText(this (string packetName, int totalSteps, string stepName, byte loadingProgress) requestPacketLoadingProgress)
        {
            var (packetName, totalSteps, stepName, loadingProgress) = requestPacketLoadingProgress;
            return $"Loading {packetName} is currently at {stepName} ({loadingProgress} of {totalSteps})";
        }
    }
}