using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class StatusHandler
    {
        private readonly IStatusService _statusService;

        public StatusHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public IEnumerable<ValidationResult> CanAdd(Status status)
        {
            var validationErrors = new List<ValidationResult>();

            if (status != null)
            {
                if (_statusService.IsStatusExists(status.Name))
                {
                    validationErrors.Add(new ValidationResult(Constants.Status.StatusNameExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Status.StatusDoesNotExists));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Status status)
        {
            var validationErrors = new List<ValidationResult>();

            if (status != null)
            {
                var dbStudent = _statusService.Find(status.StatusID);

                if (dbStudent != null)
                {
                    if (!dbStudent.Name.Equals(status.Name) && _statusService.IsStatusExists(status.Name))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Status.StatusNameExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Status.StatusDoesNotExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Status.StatusEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var status = _statusService.Find(id);
            if (status == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Status.StatusDoesNotExists));
            }

            return validationErrors;
        }
    }
}
