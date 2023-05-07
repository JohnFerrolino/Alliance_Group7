using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class ApplicantHandler
    {
        private readonly IApplicantService _applicantService;

        public ApplicantHandler(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        public IEnumerable<ValidationResult> CanAdd(Applicant applicant)
        {
            var validationErrors = new List<ValidationResult>();

            if (applicant != null)
            {
                if (_applicantService.IsApplicantExists(applicant.EmailAddress))
                {
                    validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantEmailExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Applicant applicant)
        {
            var validationErrors = new List<ValidationResult>();

            if (applicant != null)
            {
                var dbStudent = _applicantService.Find(applicant.ApplicantID);

                if (dbStudent != null)
                {
                    if (!dbStudent.EmailAddress.Equals(applicant.EmailAddress) && _applicantService.IsApplicantExists(applicant.EmailAddress))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantEmailExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Student.StudentNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Student.StudentEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var applicant = _applicantService.Find(id);
            if (applicant == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantDoesNotExists));
            }

            return validationErrors;
        }
    }
}
