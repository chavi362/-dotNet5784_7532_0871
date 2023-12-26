

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
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist",null!);

            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = (EngineerExperience)doEngineer.Level!,
                Cost = (double)doEngineer.Cost!,
                CurrentTask=FindCurrentTask(doEngineer.Id)

            };
        }
        private Tuple<int,string> FindCurrentTask(int id)
        {
            DateTime today= DateTime.Now;
            List<DO.Task> tasks = (List<DO.Task>)_dal.Task.ReadAll((task) => task.EngineerId == id&&task.Complete>today&&task.Start<today);
            return new Tuple<int, string>(tasks.First().Id, tasks.First().Alias!);
        }
        public IEnumerable<Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
        {
            return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
                    select new Engineer
                    {
                        Id = doEngineer.Id,
                        Name = doEngineer.Name,
                        Email = doEngineer.Email,
                        Cost = (double)doEngineer.Cost!,
                        Level = (EngineerExperience)doEngineer.Level!,
                        CurrentTask = FindCurrentTask(doEngineer.Id)
                    });
        }


        public int Update(BO.Engineer boEngineer)
        {
            DO.Engineer doEngineer = new DO.Engineer
                (boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level!, boEngineer.Cost);
            try
            {
                _dal.Engineer.Update(doEngineer);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BlDoesNotExistException($"Engineer with ID={boEngineer.Id} doern't exists",ex);
            }
            return boEngineer.Id;
        }
    }
}
