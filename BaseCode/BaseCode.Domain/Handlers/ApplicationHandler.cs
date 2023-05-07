using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class ApplicationHandler
    {
        private readonly IApplicationService _applicationService;

        public ApplicationHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        //EMAIL MUST BE UNIQUE
        public IEnumerable<ValidationResult> CanAdd(Application apl)
        {
            var validationErrors = new List<ValidationResult>();

            if (apl != null)
            {
                if (_applicationService.IsApplicationExists(apl.ApplicationID))
                {
                    validationErrors.Add(new ValidationResult(Constants.Application.ApplicationID));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Application.ApplicationEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Application apl)
        {
            var validationErrors = new List<ValidationResult>();

            if (apl != null)
            {
                var dbApplicant = _applicationService.Find(apl.ApplicantID);

                if (dbApplicant != null)
                {
                    if (!dbApplicant.ApplicationCode.Equals(apl.ApplicationCode) && _applicationService.IsApplicationExists(apl.ApplicationID))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Application.ApplicationCode));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Applicant.ApplicantNotExist));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Application.ApplicationEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var applicant = _applicationService.Find(id);
            if (applicant == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Application.ApplicationNotExist));
            }

            return validationErrors;
        }
    }
}
