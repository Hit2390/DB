using Task.MAL.POCO;
using Task.DAL.Interfaces;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Task.MAL.Others.Messages;
using Task.MAL.Others.Response;

namespace Task.DAL.Repositories
{
    /// <summary>
    /// Database context for managing patient details.
    /// </summary>
    public class DBPatientContext : IPatientRepositories
    {

        #region Private Properties

        private readonly string _connection;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DBPatientContext"/> class.
        /// </summary>
        /// <param name="_configuration">The IConfiguration instance used to retrieve the connection string.</param>
        public DBPatientContext(IConfiguration configuration)
        {
            // Retrieve the connection string from IConfiguration.
            _connection = configuration.GetConnectionString("MysqlConnection") ?? throw new ArgumentNullException("Database connection string is null or empty.");
        }

        #endregion


        #region Public Methods


        #region Get Patient Details

        /// <summary>
        /// Retrieves patient details from the database.
        /// </summary>
        /// <returns>
        /// A response object containing the list of patient details as a collection of dictionaries, where each dictionary represents patient details.
        /// </returns>
        public async Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsRepository()
        {
            List<Dictionary<string, object>> patientDetailsData = new();

            // Establish a connection to the database
            using MySqlConnection con = new(_connection);
            try
            {
                string query = @"SELECT 
                                        p.Patient_Id,
                                        p.Prefix,
                                        p.FirstName,
                                        p.Surname,
                                        p.DateOfBirth,
                                        p.Gender,
                                        p.Age,
                                        p.Blood_Type,
                                        p.Height,
                                        p.Weight,
                                        p.Address,
                                        e.Patient_email_address,
                                        pn.Patient_Phone_Number
                                    FROM
                                        patient AS p
                                            INNER JOIN
                                        patient_email_address AS e ON p.Patient_Id = e.Patient_Ref_Id
                                            INNER JOIN
                                        patient_phone_number AS pn ON p.Patient_Id = pn.Patient_Ref_Id;";

                await con.OpenAsync();

                // Execute the query to fetch patient details data
                MySqlCommand cmd = new(query, con);

                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Create a dictionary to hold the data for each patient detail
                        Dictionary<string, object> patientDetail = new();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string fieldName = reader.GetName(i);
                            object value = reader.GetValue(i);
                            patientDetail.Add(fieldName, value);
                        }
                        patientDetailsData.Add(patientDetail);
                    }
                }

                // Return response with patient details data
                return new Response<List<Dictionary<string, object>>>
                {
                    IsSuccess = true,
                    Values = patientDetailsData,
                    Message = EnumMessage.G.GetMessage()
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions if occurs
                return new Response<List<Dictionary<string, object>>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        #endregion


        #region Get Patient Deatils by Id

        /// <summary>
        /// Retrieves patient details by Id from the database.
        /// </summary>
        /// <param name="id">The ID of the patient details record to search.</param>
        /// <returns>
        /// A response object containing the list of patient details as a collection of dictionaries, where each dictionary represents patient details.
        /// </returns>
        public async Task<Response<List<Dictionary<string, object>>>> GetPatientDetailsByIDRepository(int id)
        {
            List<Dictionary<string, object>> patientDetailsData = new();

            // Establish a connection to the database
            using MySqlConnection con = new(_connection);
            try
            {
                string query = @$"SELECT 
                                        p.Patient_Id,
                                        p.Prefix,
                                        p.FirstName,
                                        p.Surname,
                                        p.DateOfBirth,
                                        p.Gender,
                                        p.Age,
                                        p.Blood_Type,
                                        p.Height,
                                        p.Weight,
                                        p.Address,
                                        e.patient_email_address,
                                        pn.Patient_Phone_Number
                                    FROM
                                        patient AS p
                                            INNER JOIN
                                        patient_email_address AS e ON p.Patient_Id = e.Patient_Ref_Id
                                            INNER JOIN
                                        patient_phone_number AS pn ON p.Patient_Id = pn.Patient_Ref_Id where p.patient_Id={id};";

                await con.OpenAsync();

                // Execute the query to fetch patient details data
                MySqlCommand cmd = new(query, con);

                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Create a dictionary to hold the data for each patient detail
                        Dictionary<string, object> patientDetail = new();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string fieldName = reader.GetName(i);
                            object value = reader.GetValue(i);
                            patientDetail.Add(fieldName, value != DBNull.Value ? value : "");
                        }
                        patientDetailsData.Add(patientDetail);
                    }
                }

                // Return response with patient details data
                return new Response<List<Dictionary<string, object>>>
                {
                    IsSuccess = true,
                    Values = patientDetailsData,
                    Message = EnumMessage.G.GetMessage()
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions if occurs
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
        /// Adds new patient details to the database.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be added.</param>
        /// <returns>A response object indicating the success or failure of the operation.</returns>
        public async Task<Response<string>> AddPatientDetailsRepository(PatientDetails addUpdateRecordsModel)
        {
            using MySqlConnection con = new(_connection);
            MySqlTransaction transaction = null;
            try
            {
                string patientTableQuery = @"INSERT INTO Patient(
                                                Prefix,
                                                FirstName,
                                                Surname,
                                                DateOfBirth,
                                                Gender,
                                                Age,
                                                Blood_Type,
                                                Height,
                                                Weight,
                                                Address,
                                                Record_Inserted_DateTime)
                                             VALUES(
                                                @Prefix,
                                                @FirstName,
                                                @Surname,
                                                @DateOfBirth,
                                                @Gender,
                                                @Age,
                                                @Blood_Type,
                                                @Height,
                                                @Weight,
                                                @Address,
                                                @Record_Inserted_DateTime_of_Patient);
                                                SELECT LAST_INSERT_ID();";

                string patientEmailAddressTableQuery = @"INSERT INTO patient_email_address(
                                                Patient_Email_Address,
                                                Patient_Ref_Id,
                                                Record_Inserted_DateTime)
                                             VALUES(
                                                @patient_email_address,
                                                @patient_ref_Id,
                                                @record_inserted_DateTime_of_Email_Address)";


                string patientPhoneNumberTableQuery = @"INSERT INTO patient_phone_number(
                                                Patient_Phone_Number,
                                                patient_ref_Id,
                                                Record_Inserted_DateTime)
                                             VALUES(
                                                @patinet_Phone_Number,
                                                @patient_ref_Id,
                                                @record_inserted_DateTime_of_Phone_Number)";

                await con.OpenAsync();
                using (transaction = await con.BeginTransactionAsync())
                {
                    MySqlCommand command = new(patientTableQuery, con);
                    command.Parameters.AddWithValue("@Prefix", addUpdateRecordsModel.Prefix);
                    command.Parameters.AddWithValue("@FirstName", addUpdateRecordsModel.FirstName);
                    command.Parameters.AddWithValue("@Surname", addUpdateRecordsModel.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", addUpdateRecordsModel.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", addUpdateRecordsModel.Gender);
                    command.Parameters.AddWithValue("@Age", addUpdateRecordsModel.Age);
                    command.Parameters.AddWithValue("@Blood_Type", addUpdateRecordsModel.Blood_Type);
                    command.Parameters.AddWithValue("@Height", addUpdateRecordsModel.Height);
                    command.Parameters.AddWithValue("@Weight", addUpdateRecordsModel.Weight);
                    command.Parameters.AddWithValue("@Address", addUpdateRecordsModel.Address);
                    command.Parameters.AddWithValue("@Record_Inserted_DateTime_of_Patient", DateTime.Now);

                    // Execute the query to insert patient details and get the last inserted ID
                    int lastInsertedPatientId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    // Now, you can use the lastInsertedPatientId as the Patient_Ref_Id for other tables
                    command = new MySqlCommand(patientEmailAddressTableQuery, con);
                    command.Parameters.AddWithValue("@patient_email_address", addUpdateRecordsModel.Email_Address);
                    command.Parameters.AddWithValue("@patient_ref_Id", lastInsertedPatientId);
                    command.Parameters.AddWithValue("@record_inserted_DateTime_of_Email_Address", DateTime.Now);
                    await command.ExecuteNonQueryAsync();

                    command = new MySqlCommand(patientPhoneNumberTableQuery, con);
                    command.Parameters.AddWithValue("@patinet_Phone_Number", addUpdateRecordsModel.PhoneNumber);
                    command.Parameters.AddWithValue("@patient_ref_Id", lastInsertedPatientId);
                    command.Parameters.AddWithValue("@record_inserted_DateTime_of_Phone_Number", DateTime.Now);
                    await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                }

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = EnumMessage.I.GetMessage()
                };
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                // Handle any exceptions
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
        /// Updates an existing patient record in the database.
        /// </summary>
        /// <param name="addUpdateRecordsModel">The patient details to be updated..</param>
        /// <returns>
        /// A response object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Response<string>> UpdatePatientDetailsRepository([FromBody] PatientDetails addUpdateRecordsModel)
        {
            using MySqlConnection con = new(_connection);
            MySqlTransaction transaction = null;
            try
            {
                string patientTableQuery = @"UPDATE patient
                             SET 
                                Prefix = @Prefix,
                                FirstName = @FirstName,
                                Surname = @Surname,
                                DateOfBirth = @DateOfBirth,
                                Gender = @Gender,
                                Age = @Age,
                                Blood_Type = @Blood_Type,
                                Height = @Height,
                                Weight = @Weight,
                                Address = @Address,
                                Record_Updated_DateTime = @Record_Updated_DateTime_of_Patient
                             WHERE
                                Patient_Id = @Id";


                string patientEmailAddressTableQuery = @"UPDATE patient_email_address
                             SET 
                                Patient_Email_Address = @Patient_Email_Address,
                                Record_Updated_DateTime = @Record_Updated_DateTime_of_Email_Address
                             WHERE
                                Patient_Ref_Id = @Patient_Ref_Id";


                string patientPhoneNumberTableQuery = @"UPDATE patient_phone_number
                             SET 
                                Patient_Phone_Number = @Patient_Phone_Number,
                                Record_Updated_DateTime = @Record_Updated_DateTime_of_Phone_Number
                             WHERE
                                Patient_Ref_Id = @Patient_Ref_Id";


                await con.OpenAsync();
                using (transaction = await con.BeginTransactionAsync())
                {
                    MySqlCommand command = new(patientTableQuery, con);
                    command.Parameters.AddWithValue("@Id", addUpdateRecordsModel.Patient_Id);
                    command.Parameters.AddWithValue("@Prefix", addUpdateRecordsModel.Prefix);
                    command.Parameters.AddWithValue("@FirstName", addUpdateRecordsModel.FirstName);
                    command.Parameters.AddWithValue("@Surname", addUpdateRecordsModel.Surname);
                    command.Parameters.AddWithValue("@DateOfBirth", addUpdateRecordsModel.DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", addUpdateRecordsModel.Gender);
                    command.Parameters.AddWithValue("@Age", addUpdateRecordsModel.Age);
                    command.Parameters.AddWithValue("@Blood_Type", addUpdateRecordsModel.Blood_Type);
                    command.Parameters.AddWithValue("@Height", addUpdateRecordsModel.Height);
                    command.Parameters.AddWithValue("@Weight", addUpdateRecordsModel.Weight);
                    command.Parameters.AddWithValue("@Address", addUpdateRecordsModel.Address);
                    command.Parameters.AddWithValue("@Record_Updated_DateTime_of_Patient", DateTime.Now);
                    await command.ExecuteNonQueryAsync();

                    command = new MySqlCommand(patientEmailAddressTableQuery, con);
                    command.Parameters.AddWithValue("@Patient_Email_Address", addUpdateRecordsModel.Email_Address);
                    command.Parameters.AddWithValue("@Patient_Ref_Id", addUpdateRecordsModel.Patient_Id);
                    command.Parameters.AddWithValue("@Record_Updated_DateTime_of_Email_Address", DateTime.Now);
                    await command.ExecuteNonQueryAsync();

                    command = new MySqlCommand(patientPhoneNumberTableQuery, con);
                    command.Parameters.AddWithValue("@Patient_Phone_Number", addUpdateRecordsModel.PhoneNumber);
                    command.Parameters.AddWithValue("@Patient_Ref_Id", addUpdateRecordsModel.Patient_Id);
                    command.Parameters.AddWithValue("@Record_Updated_DateTime_of_Phone_Number", DateTime.Now);
                    await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                }

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = EnumMessage.U.GetMessage()
                };
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                // Handle exceptions if occurs
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
        /// Delete a patient record from the database.
        /// </summary>
        /// <param name="id">The ID of the patient record to delete.</param>
        /// <returns>
        /// A response object indicating the success or failure of the operation.
        /// </returns>
        public async Task<Response<string>> DeletePatientDetailsRepository(int id)
        {
            using MySqlConnection con = new(_connection);
            MySqlTransaction transaction = null;
            try
            {
                string patientPhoneNumberTableQuery = @"DELETE 
                                    FROM
                                        patient_phone_number 
                                    WHERE
                                        Patient_Ref_Id = @id;";

                string patientEmailAddressTableQuery = @"DELETE 
                                    FROM
                                        patient_email_address 
                                    WHERE
                                        Patient_Ref_Id = @id;";

                string patientTableQuery = @"DELETE 
                                    FROM
                                        patient 
                                    WHERE
                                        Patient_Id = @id;";

                await con.OpenAsync();

                using (transaction = await con.BeginTransactionAsync())
                {
                    MySqlCommand command = new(patientPhoneNumberTableQuery, con);
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();

                    command = new MySqlCommand(patientEmailAddressTableQuery, con);
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();

                    command = new MySqlCommand(patientTableQuery, con);
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                }

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = EnumMessage.D.GetMessage()
                };
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                // Handle exceptions if occurs
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