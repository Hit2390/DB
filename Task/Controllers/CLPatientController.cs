using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.BAL.Interfaces;
using Task.MAL.Others.Response;
using Task.MAL.POCO;

namespace Task.Controllers
{
    [Route("API/Patient")]
    [ApiController]
    public class CLPatientController : ControllerBase
    {
        #region Private Properties

        private readonly IPatientService _iPatientService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CLPatientController"> class with the specified service.
        /// </summary>
        /// <param name="iPatientService">The service responsible for managing patient details.</param>
        public CLPatientController(IPatientService iPatientService)
        {
            _iPatientService = iPatientService;
        }

        #endregion

        #region Public Methods

        #region Get User Details
        
        //URL: https://localhost:7125/API/Patient/allDetails

        /// <summary>
        /// Retrieves all patient details.
        /// </summary>
        [HttpGet]
        [Route("allDetails")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patientData = await _iPatientService.GetPatientDetailsHandler();
            if (patientData.IsSuccess)
            {
                return Ok(patientData);
            }
            else
            {
                return NotFound(new Response<string>
                {
                    IsSuccess = false,
                    Message = "No Data Found"
                });
            }
        }

        #endregion

        #region Get User Details By Id

        //URL: https://localhost:7125/API/Patient?id=1

        /// <summary>
        /// Retrieves patient details by Id.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patientData = await _iPatientService.GetPatientDetailsByIdHandler(id);
            if (patientData.IsSuccess)
            {
                return Ok(patientData);
            }
            else
            {
                return NotFound(new Response<string>
                {
                    IsSuccess = false,
                    Message = "No Data Found"
                });
            }
        }

        #endregion

        #region Add User Details

        //URL:https://localhost:7125/API/Patient

        /// <summary>
        /// Adds patient details.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddPatientDetails([FromBody] PatientDetails addUpdateRecordsModel)
        {
            var patientData = await _iPatientService.AddPatientDetailsHandler(addUpdateRecordsModel);
            if (patientData.IsSuccess)
            {
                return Ok(patientData);
            }
            else
            {
                return BadRequest(new Response<string>
                {
                    IsSuccess = false,
                    Message = "Failed to add patient details"
                });
            }
        }

        #endregion

        #region Update User Details

        //UrlHelperExtensions: https://localhost:7125/API/Patient
        /// <summary>
        /// Updates patient details.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdatePatientDetails([FromBody] PatientDetails addUpdateRecordsModel)
        {
            var patientData = await _iPatientService.UpdatePatientDetailsHandler(addUpdateRecordsModel);
            if (patientData.IsSuccess)
            {
                return Ok(patientData);
            }
            else
            {
                return BadRequest(new Response<string>
                {
                    IsSuccess = false,
                    Message = "Failed to update patient details"
                });
            }
        }

        #endregion

        #region Delete User Details

        //URL: https://localhost:7125/API/Patient?id=1
        /// <summary>
        /// Deletes patient details.
        /// </summary>
        [HttpDelete]

        public async Task<IActionResult> DeletePatientDetails(int id)
        {
            var patientData = await _iPatientService.DeletePatientDetailsHandler(id);
            if (patientData.IsSuccess)
            {
                return Ok(patientData);
            }
            else
            {
                return BadRequest(new Response<string>
                {
                    IsSuccess = false,
                    Message = "Failed to delete patient details"
                });
            }
        }

        #endregion

        #endregion
    }
}
