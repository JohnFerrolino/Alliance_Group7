using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        public ApplicationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Application Find(int id)
        {
            return GetDbSet<Application>().Find(id);
        }

        public IQueryable<Application> RetrieveAll()
        {
            return GetDbSet<Application>();
        }

        public ListViewModel FindApplication(ApplicationSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var application = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.ApplicationId) || x.ApplicationID.ToString().Contains(searchModel.ApplicationId)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicationCode) || x.ApplicationCode.ToString().Contains(searchModel.ApplicationCode)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicationApplicant) || x.ApplicantID == Convert.ToInt32(searchModel.ApplicationApplicant)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicationPosition) || x.PositionID == Convert.ToInt32(searchModel.ApplicationPosition)) &&
                            (string.IsNullOrEmpty(searchModel.ApplicationStatus) || x.PositionID == Convert.ToInt32(searchModel.ApplicationStatus)))
                .OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = application.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = application.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(apl => new {
                    id = apl.ApplicationID,
                    code = apl.ApplicationCode,
                    applicant = apl.ApplicationID,
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

        public void Create(Application apl)
        {
            GetDbSet<Application>().Add(apl);
            UnitOfWork.SaveChanges();
        }

        public void Update(Application apl)
        {
            var aplUpdate = Find(apl.ApplicationID);
            aplUpdate.ApplicationCode = apl.ApplicationCode;
            aplUpdate.ApplicantID = apl.ApplicantID;
            aplUpdate.PositionID = apl.PositionID;
            aplUpdate.StatusID = apl.StatusID;
            aplUpdate.IsActive = apl.IsActive;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void SoftDelete(Application apl)
        {
            var aplUpdate = Find(apl.ApplicationID);
            aplUpdate.IsActive = apl.IsActive;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void Delete(Application apl)
        {
            GetDbSet<Application>().Remove(apl);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var apl = Find(id);
            GetDbSet<Application>().Remove(apl);
            UnitOfWork.SaveChanges();
        }


        public bool IsApplicationExists(int id)
        {
            return GetDbSet<Application>().Any(x => x.ApplicationID == id);
        }



        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.Application.ApplicationID):
                    sortKey = "ApplicationID";
                    break;
                case (Constants.Application.ApplicationCode):
                    sortKey = "ApplicationCode";
                    break;

                default:
                    sortKey = "ApplicationID";
                    break;
            }

            return sortKey;
        }
    }
}
