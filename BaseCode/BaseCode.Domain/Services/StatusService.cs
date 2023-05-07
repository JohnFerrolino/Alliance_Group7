using System.Linq;
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository applicantRepository, IMapper mapper)
        {
            _statusRepository = applicantRepository;
            _mapper = mapper;
        }

        public StatusViewModel Find(int id)
        {
            StatusViewModel statusViewModel = null;
            var status = _statusRepository.Find(id);

            if (status != null)
            {
                statusViewModel = _mapper.Map<StatusViewModel>(status);
            }

            return statusViewModel;
        }

        public IQueryable<Status> RetrieveAll()
        {
            return _statusRepository.RetrieveAll();
        }

        public ListViewModel FindStatus(StatusSearchViewModel searchModel)
        {
            return _statusRepository.FindStatus(searchModel);
        }

        public void Create(Status status)
        {
            _statusRepository.Create(status);
        }

        public void Update(Status status)
        {
            _statusRepository.Update(status);
        }

        public void SoftDelete(Status status)
        {
            _statusRepository.SoftDelete(status);
        }

        public void Delete(Status status)
        {
            _statusRepository.Delete(status);
        }

        public void DeleteById(int id)
        {
            _statusRepository.DeleteById(id);
        }

        public bool IsStatusExists(string name)
        {
            return _statusRepository.IsStatusExists(name);
        }
    }
}
