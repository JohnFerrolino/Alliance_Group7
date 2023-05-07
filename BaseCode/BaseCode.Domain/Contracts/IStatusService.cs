using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Domain.Contracts
{
    public interface IStatusService
    {
        StatusViewModel Find(int id);
        IQueryable<Status> RetrieveAll();
        ListViewModel FindStatus(StatusSearchViewModel searchModel);
        void Create(Status status);
        void Update(Status status);
        void SoftDelete(Status status);
        void Delete(Status status);
        void DeleteById(int id);
        bool IsStatusExists(string name);
    }
}
