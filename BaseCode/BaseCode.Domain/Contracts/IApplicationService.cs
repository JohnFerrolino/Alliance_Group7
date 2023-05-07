using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Domain.Contracts
{
    public interface IApplicationService
    {
        ApplicationViewModel Find(int id);
        IQueryable<Application> RetrieveAll();
        ListViewModel FindApplication(ApplicationSearchViewModel searchModel);
        void Create(Application apl);
        void Update(Application apl);
        void SoftDelete(Application apl);
        void Delete(Application apl);
        void DeleteById(int id);
        bool IsApplicationExists(int id);
    }
}
