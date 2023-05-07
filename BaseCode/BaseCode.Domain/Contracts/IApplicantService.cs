using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Domain.Contracts
{
    public interface IApplicantService
    {
        ApplicantViewModel Find(int id);
        IQueryable<Applicant> RetrieveAll();
        ListViewModel FindApplicants(ApplicantSearchViewModel searchModel);
        void Create(Applicant apl);
        void Update(Applicant apl);
        void SoftDelete(Applicant apl);
        void Delete(Applicant apl);
        void DeleteById(int id);
        bool IsApplicantExists(string name);
    }
}
