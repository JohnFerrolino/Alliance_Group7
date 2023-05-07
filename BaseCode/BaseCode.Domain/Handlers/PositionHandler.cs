using BaseCode.Data.Models;
using BaseCode.Domain.Contracts;
using System.Collections.Generic;
using Constants = BaseCode.Data.Constants;


namespace BaseCode.Domain.Handlers
{
    public class PositionHandler
    {
        private readonly IPositionService _positionService;

        public PositionHandler(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public IEnumerable<ValidationResult> CanAdd(Position position)
        {
            var validationErrors = new List<ValidationResult>();

            if (position != null)
            {
                if (_positionService.IsPositionExists(position.Name))
                {
                    validationErrors.Add(new ValidationResult(Constants.Position.PositionNameExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Position.PositionEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanUpdate(Position position)
        {
            var validationErrors = new List<ValidationResult>();

            if (position != null)
            {
                var dbStudent = _positionService.Find(position.PositionID);

                if (dbStudent != null)
                {
                    if (!dbStudent.Name.Equals(position.Name) && _positionService.IsPositionExists(position.Name))
                    {
                        validationErrors.Add(new ValidationResult(Constants.Position.PositionNameExists));
                    }
                }
                else
                {
                    validationErrors.Add(new ValidationResult(Constants.Position.PositionDoesNotExists));
                }
            }
            else
            {
                validationErrors.Add(new ValidationResult(Constants.Position.PositionEntryInvalid));
            }

            return validationErrors;
        }

        public IEnumerable<ValidationResult> CanDelete(int id)
        {
            var validationErrors = new List<ValidationResult>();

            var student = _positionService.Find(id);
            if (student == null)
            {
                validationErrors.Add(new ValidationResult(Constants.Position.PositionDoesNotExists));
            }

            return validationErrors;
        }
    }
}
