using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Domain.Contracts
{
    public interface IPositionService
    {
        PositionViewModel Find(int id);
        IQueryable<Position> RetrieveAll();
        ListViewModel FindPosition(PositionSearchViewModel searchModel);
        void Create(Position status);
        void Update(Position status);
        void SoftDelete(Position status);
        void Delete(Position status);
        void DeleteById(int id);
        bool IsPositionExists(string name);
    }
}
