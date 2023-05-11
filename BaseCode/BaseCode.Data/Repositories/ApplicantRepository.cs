using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class ApplicantRepository : BaseRepository, IApplicantRepository
    {
        public ApplicantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Applicant Find(int id)
        {
            return GetDbSet<Applicant>().Find(id);
        }

        public IQueryable<Applicant> RetrieveAll()
        {
            return GetDbSet<Applicant>();
        }

        public ListViewModel FindApplicants(ApplicantSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var applicants = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.ApplicantID) || x.ApplicantID.ToString().Contains(searchModel.ApplicantID)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantFirstname) || x.FirstName.Contains(searchModel.ApplicantFirstname)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantLastname) || x.LastName.Contains(searchModel.ApplicantLastname)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantEmail) || x.EmailAddress.Contains(searchModel.ApplicantEmail)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantPhoneNumber) || x.PhoneNumber.Contains(searchModel.ApplicantPhoneNumber))&&
                            (string.IsNullOrEmpty(searchModel.ApplicantResume) || x.Resume.Contains(searchModel.ApplicantResume)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicantPosition) || x.PositionID == Convert.ToInt32(searchModel.ApplicantPosition))&&
                            (string.IsNullOrEmpty(searchModel.ApplicantStatus) || x.PhoneNumber.Contains(searchModel.ApplicantStatus)))
                            .OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = applicants.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = applicants.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(apl => new {
                    id = apl.ApplicantID,
                    firstname = apl.FirstName,
                    lastname = apl.LastName,
                    emailaddress = apl.EmailAddress,
                    phonenumber = apl.PhoneNumber,
                    position = apl.PositionID,  
                    status = apl.StatusID
                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public void Create(Applicant apl)
        {
            GetDbSet<Applicant>().Add(apl);
            UnitOfWork.SaveChanges();
        }

        public void Update(Applicant apl)
        {
            var aplUpdate = Find(apl.ApplicantID);
            aplUpdate.FirstName = apl.FirstName;
            aplUpdate.LastName = apl.LastName;
            aplUpdate.EmailAddress = apl.EmailAddress;
            aplUpdate.PhoneNumber = apl.PhoneNumber;
            aplUpdate.PositionID = apl.PositionID;
            aplUpdate.StatusID = apl.StatusID;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void SoftDelete(Applicant apl)
        {
            var aplUpdate = Find(apl.ApplicantID);
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void Delete(Applicant apl)
        {
            GetDbSet<Applicant>().Remove(apl);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var apl = Find(id);
            GetDbSet<Applicant>().Remove(apl);
            UnitOfWork.SaveChanges();
        }

        public bool IsApplicantExists(string name)
        {
            return GetDbSet<Applicant>().Any(x => x.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.Applicant.ApplicantId):
                    sortKey = "ApplicantID";
                    break;

                case (Constants.Applicant.ApplicantFirstName):
                    sortKey = "FirstName";
                    break;

                case (Constants.Applicant.ApplicantLastName):
                    sortKey = "LastName";
                    break;

                case (Constants.Applicant.ApplicantEmailAddress):
                    sortKey = "EmailAddress";
                    break;

                case (Constants.Applicant.ApplicantPhoneNumber):
                    sortKey = "PhoneNumber";
                    break;
                default:
                    sortKey = "ApplicantID";
                    break;
            }

            return sortKey;
        }
    }
}
