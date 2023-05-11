namespace BaseCode.Data
{
    public static class Constants
    {
        public class Token
        {
            public const string Issuer = "BaseCode:Issuer";
            public const string Audience = "BaseCode:Audience";            
            public const string SecretKey = "BaseCode:AuthSecretKey";
            public const string POST = "POST";
            public const string TokenPath = "/api/token";
            public const string Username = "username";
            public const string Password = "password";
            public const string RefreshToken = "refresh_token";
            public const string UserID = "user_id";
        }

        public class SortDirection
        {
            public const string Ascending = "Ascending";
            public const string Descending = "Descending";
        }

        public class Claims
        {
            public const string Id = "userId";
            public const string UserName = "userName";
            public const string Role = "userRole";
        }

        public class ClaimTypes
        {
            public const string UserName = "user_name";
            public const string ID = "id";
            public const string UserId = "user_id";
            public const string FullName = "full_name";
        }

        public class Common
        {
            public const string BaseCode = "BaseCode";
            public const string OAuth = "/oauth";
            public const string Client = "Client";
            public const string ClientID = "ClientID";
            public const string ClientSecret = "ClientSecret";
            public const string JSONContentType = "application/json";
            public const string Bearer = "Bearer";

            // Messages
            public const string BadRequest = "Bad Request";
            public const string InvalidRole = "Invalid Role";
        }
        public class Roles
        {
            public const string Admin = "Administrator";
            public const string User = "User";

        }

        public class User
        {
            public const string InvalidUserNamePassword = "Invalid username or password.";
        }
        public class Applicant
        {
            // Sort Keys
            public const string ApplicantId = "applicant_id";
            public const string ApplicantFirstName = "applicant_firstname";
            public const string ApplicantLastName = "applicant_lastname";
            public const string ApplicantEmailAddress = "applicant_email";
            public const string ApplicantPhoneNumber = "applicant_phonenumber";

            // Messages
            public const string ApplicantNameExists = "Applicant name already exists";
            public const string ApplicantEmailExists = "Applicant email already exists";
            public const string ApplicantEntryInvalid = "Applicant entry is not valid!";
            public const string ApplicantPhoneNumberInvalid = "Applicant number is not valid!";
            public const string ApplicantNotExist = "Applicant does not exist.";
            public const string ApplicantDoesNotExists = "Applicant does not exist.";
            public const string ApplicantSuccessAdd = "Applicant added successfully.";
            public const string ApplicantSuccessEdit = "Applicant is updated successfully.";
            public const string ApplicantSuccessDelete = "Applicant is deleted successfully.";
        }


        public class Position
        {
            // Sort Keys
            public const string PositionId = "position_id";
            public const string PositionName = "position_name";

            // Messages
            public const string PositionNameExists = "Position name already exists";
            public const string PositionEntryInvalid = "Position entry is not valid!";
            public const string PositionNotExist = "Position does not exist.";
            public const string PositionDoesNotExists = "Position does not exist.";
            public const string PositionSuccessAdd = "Position added successfully.";
            public const string PositionSuccessEdit = "Position is updated successfully.";
            public const string PositionSuccessDelete = "Position is deleted successfully.";
        }

        public class Status
        {
            // Sort Keys
            public const string StatusId = "status_id";
            public const string StatusName = "status_name";

            // Messages
            public const string StatusNameExists = "Status name already exists";
            public const string StatusEntryInvalid = "Status entry is not valid!";
            public const string StatusNotExist = "Status does not exist.";
            public const string StatusDoesNotExists = "Status does not exist.";
            public const string StatusSuccessAdd = "Status added successfully.";
            public const string StatusSuccessEdit = "Status is updated successfully.";
            public const string StatusSuccessDelete = "Status is deleted successfully.";
        }
    }
}
