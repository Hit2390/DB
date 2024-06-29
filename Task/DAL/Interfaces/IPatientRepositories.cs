using Task.MAL.POCO;
using Task.MAL.Others.Response;

namespace Task.DAL.Interfaces
{
    /// <summary>
    /// Interface for patient repositories.
    /// </summary>
    public interface IPatientRepositories
    {
        #region Public Methods

        /// <summary>
        /// Asynchronously retrieves patient details.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a response containing a list of dictionaries and representing patient details.</returns>
        Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsRepository();


        /// <summary>
        /// Asynchronously retrieves patient details by Id.
        /// </summary>
        /// <param name="id">The ID of the patient details record to search.</param>
        /// <returns>A task representing the asynchronous operation, returning a response containing a list of dictionaries and representing patient details by Id.</returns>
        Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsByIDRepository(int id);


        /// <summary>
        /// Asynchronously adds new patient details.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be added.</param>
        /// <returns>A task representing the asynchronous operation, returning a response indicating the success or failure.</returns>
        Task<Response<string>> AddPatientDetailsRepository(PatientDetails addUpdateRecordsModel);


        /// <summary>
        /// Asynchronously updates existing patient details.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be updated.</param>
        /// <returns>A task representing the asynchronous operation, returning a response indicating the success or failure.</returns>
        Task<Response<string>> UpdatePatientDetailsRepository(PatientDetails addUpdateRecordsModel);


        /// <summary>
        /// Asynchronously deletes patient details based on the provided patient ID.
        /// </summary>
        /// <param name="id">The ID of the patient details to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, returning a response indicating the success or failure.</returns>
        Task<Response<string>> DeletePatientDetailsRepository(int id);

        #endregion
    }
}