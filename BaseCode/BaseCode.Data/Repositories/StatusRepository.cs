using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Status Find(int id)
        {
            return GetDbSet<Status>().Find(id);
        }

        public IQueryable<Status> RetrieveAll()
        {
            return GetDbSet<Status>();
        }

        public ListViewModel FindStatus(StatusSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var status = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.StatusID) || x.StatusID.ToString().Contains(searchModel.StatusID)) &&
                            (string.IsNullOrEmpty(searchModel.StatusName) || x.Name.Contains(searchModel.StatusName)))
                .OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = status.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = status.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(stat => new {
                    id = stat.StatusID,
                    name = stat.Name,
                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public void Create(Status status)
        {
            GetDbSet<Status>().Add(status);
            UnitOfWork.SaveChanges();
        }

        public void Update(Status status)
        {
            var statusUpdate = Find(status.StatusID);
            statusUpdate.Name = status.Name;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void SoftDelete(Status status)
        {
            var statusUpdate = Find(status.StatusID);
            statusUpdate.IsActive = status.IsActive;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void Delete(Status status)
        {
            GetDbSet<Status>().Remove(status);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var status = Find(id);
            GetDbSet<Status>().Remove(status);
            UnitOfWork.SaveChanges();
        }

        public bool IsStatusExists(string name)
        {
            return GetDbSet<Status>().Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSortKey(string sortBy)
        {
            string sortKey;

            switch (sortBy)
            {
                case (Constants.Status.StatusId):
                    sortKey = "StatusID";
                    break;

                case (Constants.Status.StatusName):
                    sortKey = "Name";
                    break;

                default:
                    sortKey = "StatusID";
                    break;
            }

            return sortKey;
        }
    }
}
