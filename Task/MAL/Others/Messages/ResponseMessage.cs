namespace Task.MAL.Others.Messages
{
    /// <summary>
    /// Enum defining messages for different operations.
    /// </summary>
    public enum EnumMessage
    {
        /// <summary>
        /// Record inserted successfully.
        /// </summary>
        I,

        /// <summary>
        /// Record fetched successfully.
        /// </summary>
        G,

        /// <summary>
        /// Record deleted successfully.
        /// </summary>
        D,

        /// <summary>
        /// Record updated successfully.
        /// </summary>
        U
    }

    /// <summary>
    /// Extension methods for EnumMessage enum.
    /// </summary>
    public static class EnumMessageExtensions
    {
        /// <summary>
        /// Gets the corresponding message for the enum value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The message corresponding to the enum value.</returns>
        public static string GetMessage(this EnumMessage enumValue)
        {
            return enumValue switch
            {
                EnumMessage.I => "Record inserted successfully.",
                EnumMessage.G => "Record fetched successfully.",
                EnumMessage.D => "Record deleted successfully.",
                EnumMessage.U => "Record updated successfully.",
                _ => "Unknown operation.",
            };
        }
    }
}
