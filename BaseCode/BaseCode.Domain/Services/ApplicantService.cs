using System.Linq;
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IMapper _mapper;

        public ApplicantService(IApplicantRepository applicantRepository, IMapper mapper)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
        }

        public ApplicantViewModel Find(int id)
        {
            ApplicantViewModel aplViewModel = null;
            var apl = _applicantRepository.Find(id);

            if (apl != null)
            {
                aplViewModel = _mapper.Map<ApplicantViewModel>(apl);
            }

            return aplViewModel;
        }

        public IQueryable<Applicant> RetrieveAll()
        {
            return _applicantRepository.RetrieveAll();
        }

        public ListViewModel FindApplicants(ApplicantSearchViewModel searchModel)
        {
            return _applicantRepository.FindApplicants(searchModel);
        }

        public void Create(Applicant apl)
        {
            _applicantRepository.Create(apl);
        }

        public void Update(Applicant apl)
        {
            _applicantRepository.Update(apl);
        }

        public void SoftDelete(Applicant apl)
        {
            _applicantRepository.SoftDelete(apl);
        }

        public void Delete(Applicant apl)
        {
            _applicantRepository.Delete(apl);
        }

        public void DeleteById(int id)
        {
            _applicantRepository.DeleteById(id);
        }

        public bool IsApplicantExists(string name)
        {
            return _applicantRepository.IsApplicantExists(name);
        }
    }
}
