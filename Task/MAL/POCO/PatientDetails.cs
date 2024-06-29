using System.ComponentModel.DataAnnotations;

namespace Task.MAL.POCO
{
    #region Data Model for Add and Updated the Patient Details

    public class PatientDetails
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public int Patient_Id { get; set; }


        /// <summary>
        /// Patient Name Prefix
        /// </summary>
        [Required(ErrorMessage = "Patient name is required.")]
        public string Prefix { get; set; } = string.Empty;


        /// <summary>
        /// Patient First Name
        /// </summary>
        [Required(ErrorMessage = "Patient first name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The Name must be between 2 and 50 characters.")]
        public string FirstName { get; set; } = string.Empty;


        /// <summary>
        /// Patient Surname
        /// </summary>
        [Required(ErrorMessage = "Patient surname is required.")]
        public string Surname { get; set; } = string.Empty;


        /// <summary>
        /// Patient Date of Birth
        /// </summary>
        [Required(ErrorMessage = "Patient date of birth is required.")]
        public DateTime DateOfBirth { get; set; }


        /// <summary>
        /// Patient Gender
        /// </summary>
        [Required(ErrorMessage = "Patient gender is required.")]
        public string Gender { get; set; } = string.Empty;


        /// <summary>
        /// Patient Age
        /// </summary>
        [Required(ErrorMessage = "Patient age is required.")]
        public int Age { get; set; }


        /// <summary>
        /// Patient Blood Type
        /// </summary>
        [Required(ErrorMessage = "Patient blood group is required.")]
        public string Blood_Type { get; set; } = string.Empty;

        /// <summary>
        /// Patient Height
        /// </summary>
        [Required(ErrorMessage = "Patient height is required.")]
        public int Height { get; set; }


        /// <summary>
        /// Patient Weight
        /// </summary>
        [Required(ErrorMessage = "Patient weight is required.")]
        public int Weight { get; set; }


        /// <summary>
        /// Patient Address
        /// </summary>
        [Required(ErrorMessage = "Patient address is required.")]
        public string Address { get; set; } = string.Empty;


        /// <summary>
        /// Patient Email Address
        /// </summary>
        [Required(ErrorMessage = "Patient email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email_Address { get; set; } = string.Empty;


        /// <summary>
        /// Patient Phone Number
        /// </summary>
        [Required(ErrorMessage = "Patient phone number is required.")]
        public double PhoneNumber { get; set; }

    }

    #endregion

}