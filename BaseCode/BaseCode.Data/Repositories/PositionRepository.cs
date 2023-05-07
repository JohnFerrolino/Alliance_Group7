using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System;
using System.Linq;

namespace BaseCode.Data.Repositories
{
    public class PositionRepository : BaseRepository, IPositionRepository
    {
        public PositionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Position Find(int id)
        {
            return GetDbSet<Position>().Find(id);
        }

        public IQueryable<Position> RetrieveAll()
        {
            return GetDbSet<Position>();
        }

        public ListViewModel FindPosition(PositionSearchViewModel searchModel)
        {
            var sortKey = GetSortKey(searchModel.SortBy);
            var sortDir = ((!string.IsNullOrEmpty(searchModel.SortOrder) && searchModel.SortOrder.Equals("dsc"))) ?
                Constants.SortDirection.Descending : Constants.SortDirection.Ascending;

            var pos = RetrieveAll()
                .Where(x => (string.IsNullOrEmpty(searchModel.PositionID) || x.PositionID.ToString().Contains(searchModel.PositionID)) &&
                            (string.IsNullOrEmpty(searchModel.PositionName) || x.Name.Contains(searchModel.PositionName)) &&
                            (string.IsNullOrEmpty(searchModel.PositionDescription) || x.Description.Contains(searchModel.PositionDescription)))
                .OrderByPropertyName(sortKey, sortDir);

            if (searchModel.Page == 0)
                searchModel.Page = 1;
            var totalCount = pos.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / searchModel.PageSize);

            var results = pos.Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .AsEnumerable()
                .Select(position => new {
                    id = position.PositionID,
                    name = position.Name,
                    //res = position.PositionRequirements.Select(r => r.PositionRequirement).ToList(),
                    description = position.Description,
                })
                .ToList();

            var pagination = new
            {
                pages = totalPages,
                size = totalCount
            };

            return new ListViewModel { Pagination = pagination, Data = results };
        }

        public void Create(Position pos)
        {
            GetDbSet<Position>().Add(pos);
            UnitOfWork.SaveChanges();
        }

        public void Update(Position pos)
        {
            var posUpdate = Find(pos.PositionID);
            posUpdate.Name = pos.Name;
            posUpdate.PositionRequirements = pos.PositionRequirements;
            posUpdate.Description = pos.Description;
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void SoftDelete(Position pos)
        {
            var statusUpdate = Find(pos.PositionID);
            //this.SetEntityState(student, System.Data.Entity.EntityState.Modified);
            UnitOfWork.SaveChanges();
        }

        public void Delete(Position pos)
        {
            GetDbSet<Position>().Remove(pos);
            UnitOfWork.SaveChanges();
        }

        // Hard Deletion
        public void DeleteById(int id)
        {
            var pos = Find(id);
            GetDbSet<Position>().Remove(pos);
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
                case (Constants.Position.PositionId):
                    sortKey = "PositionID";
                    break;

                case (Constants.Position.PositionName):
                    sortKey = "Name";
                    break;

                default:
                    sortKey = "PositionID";
                    break;
            }

            return sortKey;
        }
    }
}
