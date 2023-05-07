using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IStatusRepository
    {
        Status Find(int id);
        IQueryable<Status> RetrieveAll();
        ListViewModel FindStatus(StatusSearchViewModel searchModel);
        void Create(Status status);
        void Update(Status status);
        void Delete(Status status);
        void SoftDelete(Status status);
        void DeleteById(int id);
        bool IsStatusExists(string name);
        string GetSortKey(string sortBy);
    }
}
