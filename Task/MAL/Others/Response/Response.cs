namespace Task.MAL.Others.Response
{
    #region Response Structure

    /// <summary>
    /// Represents a generic response structure for API responses.
    /// </summary>
    /// <typeparam name="T">Type of the data to be included in the response.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Contains the data returned by the API operation.
        /// </summary>
        public T? Values { get; set; }

        /// <summary>
        /// Provides additional information or error messages related to the API operation.
        /// </summary>
        public string? Message { get; set; }
    }

    #endregion
}
