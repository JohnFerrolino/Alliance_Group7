using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using System.Linq;

namespace BaseCode.Data.Contracts
{
    public interface IPositionRepository
    {
        Position Find(int id);
        IQueryable<Position> RetrieveAll();
        ListViewModel FindPosition(PositionSearchViewModel searchModel);
        void Create(Position pos);
        void Update(Position pos);
        void Delete(Position pos);
        void SoftDelete(Position pos);
        void DeleteById(int id);
        bool IsStatusExists(string name);
        string GetSortKey(string sortBy);
    }
}
