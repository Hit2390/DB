using Task.MAL.POCO;
using Task.MAL.Others.Response;

namespace Task.BAL.Interfaces
{
    /// <summary>
    /// Interface for patient services.
    /// </summary>
    public interface IPatientService
    {
        #region Public Methods

        /// <summary>
        /// Asynchronously retrieves patient details.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a response containing a list of dictionaries and representing patient details.</returns>
        Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsHandler();


        /// <summary>
        /// Asynchronously retrieves patient details by Id.
        /// </summary>
        /// <param name="id">The ID of the patient details record to search.</param>
        /// <returns>A task representing the asynchronous operation, returning a response containing a list of dictionaries and representing patient details by id.</returns>
        Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsByIdHandler(int id);


        /// <summary>
        /// Asynchronously adds new patient details.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be added.</param>
        /// <returns>A task representing the asynchronous operation, returning a response indicating the success or failure.</returns>
        Task<Response<string>> AddPatientDetailsHandler(PatientDetails addUpdateRecordsModel);


        /// <summary>
        /// Asynchronously updates existing patient details.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be updated.</param>
        /// <returns>A task representing the asynchronous operation, returning a response indicating the success or failure.</returns>
        Task<Response<string>> UpdatePatientDetailsHandler(PatientDetails addUpdateRecordsModel);


        /// <summary>
        /// Asynchronously deletes patient details based on the provided patient ID.
        /// </summary>
        /// <param name="id">The ID of the patient details to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, returning a response indicating the success or failure.</returns>
        Task<Response<string>> DeletePatientDetailsHandler(int id);

        #endregion
    }
}
