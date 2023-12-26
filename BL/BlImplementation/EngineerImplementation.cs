

using BlApi;
using BO;

namespace BlImplementation
{
    internal class EngineerImplementation : IEngineer
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(Engineer boEngineer)
        {
            DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level!, boEngineer.Cost);
            try
            {
                int idEngineer = _dal.Engineer.Create(doEngineer);
                return idEngineer;
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.Id} already exists", ex);
            }

        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Engineer? Read(int id)
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);
            if (doEngineer == null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = (EngineerExperience)doEngineer.Level!,
                Cost = (double)doEngineer.Cost!,

            };
        }

        public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
        {
            return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                    select new Engineer
                    {
                        Id = doEngineer.Id,
                        Name = doEngineer.Name,
                       Email= doEngineer.Email,
                       Cost = (double)doEngineer.Cost!,
                       Level= (EngineerExperience)doEngineer.Level!,
                    });

        }

        public int Update(Engineer item)
        {
            throw new NotImplementedException();
        }
    }
}
