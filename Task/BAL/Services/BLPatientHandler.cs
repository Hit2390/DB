using Task.MAL.POCO;
using Task.BAL.Interfaces;
using Task.DAL.Interfaces;
using Task.MAL.Others.Response;

namespace Task.BAL.Services
{
    /// <summary>
    /// Business logic handler for patient services.
    /// </summary>
    public class BLPatientHandler : IPatientService
    {
        #region Private Properties

        private readonly IPatientRepositories _iPatientRepository;

        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="BLPatientHandler"> class with the specified repository.
        /// </summary>
        /// <param name="iPatientRepository">The repository responsible for data access operations related to patient details.</param>
        public BLPatientHandler(IPatientRepositories iPatientRepository)
        {
            _iPatientRepository = iPatientRepository ?? throw new ArgumentNullException(nameof(iPatientRepository));
        }

        #endregion


        #region Public Methods

        #region Get Patient Details

        /// <summary>
        /// Asynchronously retrieves patient details.
        /// </summary>
        /// <returns>A response containing a list of dictionaries representing patient details data.</returns>
        public async Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsHandler()
        {
            try
            {
                return await _iPatientRepository.GetPatientDetailsRepository();
            }
            catch (Exception ex)
            {
                return new Response<List<Dictionary<string, object>>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        #endregion


        #region Get Patient Details By Id

        /// <summary>
        /// Asynchronously retrieves patient details by Id.
        /// </summary>
        ///  <param name="id">The ID of the patient details record to search.</param>
        /// <returns>A response containing a list of dictionaries representing patient details data By Id.</returns>
        public async Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsByIdHandler(int id)
        {
            try
            {
                return await _iPatientRepository.GetPatientDetailsByIDRepository(id);
            }
            catch (Exception ex)
            {
                return new Response<List<Dictionary<string, object>>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        #endregion


        #region Add Patient Details

        /// <summary>
        /// Asynchronously adds a new patient details record.
        /// </summary>
        /// <param name="patientModel">The patient details to be added.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<Response<string>> AddPatientDetailsHandler(PatientDetails addUpdateRecordsModel)
        {
            try
            {
                return await _iPatientRepository.AddPatientDetailsRepository(addUpdateRecordsModel);
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        #endregion


        #region Update Patient Details

        /// <summary>
        /// Asynchronously updates an existing patient details record.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be updated.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<Response<string>> UpdatePatientDetailsHandler(PatientDetails addUpdateRecordsModel)
        {
            try
            {
                return await _iPatientRepository.UpdatePatientDetailsRepository(addUpdateRecordsModel);
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        #endregion


        #region Delete Patient Details

        /// <summary>
        /// Asynchronously deletes a patient details record with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the patient details record to delete.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<Response<string>> DeletePatientDetailsHandler(int id)
        {
            try
            {
                return await _iPatientRepository.DeletePatientDetailsRepository(id);
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        #endregion


        #endregion
    }
}
