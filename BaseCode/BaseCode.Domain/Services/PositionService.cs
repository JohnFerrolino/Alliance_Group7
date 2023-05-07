using System.Linq;
using AutoMapper;
using BaseCode.Data.Contracts;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain.Contracts;

namespace BaseCode.Domain.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public PositionViewModel Find(int id)
        {
            PositionViewModel positionViewModel = null;
            var position = _positionRepository.Find(id);

            if (position != null)
            {
                positionViewModel = _mapper.Map<PositionViewModel>(position);
            }

            return positionViewModel;
        }

        public IQueryable<Position> RetrieveAll()
        {
            return _positionRepository.RetrieveAll();
        }

        public ListViewModel FindPosition(PositionSearchViewModel searchModel)
        {
            return _positionRepository.FindPosition(searchModel);
        }

        public void Create(Position position)
        {
            _positionRepository.Create(position);
        }

        public void Update(Position position)
        {
            _positionRepository.Update(position);
        }

        public void SoftDelete(Position position)
        {
            _positionRepository.SoftDelete(position);
        }

        public void Delete(Position position)
        {
            _positionRepository.Delete(position);
        }

        public void DeleteById(int id)
        {
            _positionRepository.DeleteById(id);
        }

        public bool IsPositionExists(string name)
        {
            return _positionRepository.IsStatusExists(name);
        }
    }
}
