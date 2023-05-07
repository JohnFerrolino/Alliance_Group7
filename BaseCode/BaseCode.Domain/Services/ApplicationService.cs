using System.Linq;
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public ApplicationViewModel Find(int id)
        {
            ApplicationViewModel aplViewModel = null;
            var apl = _applicationRepository.Find(id);

            if (apl != null)
            {
                aplViewModel = _mapper.Map<ApplicationViewModel>(apl);
            }

            return aplViewModel;
        }

        public IQueryable<Application> RetrieveAll()
        {
            return _applicationRepository.RetrieveAll();
        }

        public ListViewModel FindApplication(ApplicationSearchViewModel searchModel)
        {
            return _applicationRepository.FindApplication(searchModel);
        }

        public void Create(Application apl)
        {
            _applicationRepository.Create(apl);
        }

        public void Update(Application apl)
        {
            _applicationRepository.Update(apl);
        }

        public void SoftDelete(Application apl)
        {
            _applicationRepository.SoftDelete(apl);
        }

        public void Delete(Application apl)
        {
            _applicationRepository.Delete(apl);
        }

        public void DeleteById(int id)
        {
            _applicationRepository.DeleteById(id);
        }

        public bool IsApplicationExists(int id)
        {
            return _applicationRepository.IsApplicationExists(id);
        }


    }
}
