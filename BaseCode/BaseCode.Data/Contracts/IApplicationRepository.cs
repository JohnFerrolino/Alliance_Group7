using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IApplicationRepository
    {
        Application Find(int id);
        IQueryable<Application> RetrieveAll();
        ListViewModel FindApplication(ApplicationSearchViewModel searchModel);
        void Create(Application applicant);
        void Update(Application applicant);
        void Delete(Application applicant);
        void SoftDelete(Application applicant);
        void DeleteById(int id);
        bool IsApplicationExists(int id);
        string GetSortKey(string sortBy);
    }
}
