using BaseCode.Data.Models;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IApplicantRepository
    {
        Applicant Find(int id);
        IQueryable<Applicant> RetrieveAll();
        ListViewModel FindApplicants(ApplicantSearchViewModel searchModel);
        void Create(Applicant applicant);
        void Update(Applicant applicant);
        void Delete(Applicant applicant);
        void SoftDelete(Applicant applicant);
        void DeleteById(int id);
        bool IsApplicantExists(string name);
        string GetSortKey(string sortBy);
    }
}
