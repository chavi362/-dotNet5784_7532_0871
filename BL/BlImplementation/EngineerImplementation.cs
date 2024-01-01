

using BlApi;
using BO;



namespace BlImplementation
{

    internal class EngineerImplementation : IEngineer
    {
        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string[] parts = email.Split('@');
            if (parts.Length != 2)
            {
                return false; // email must have exactly one @ symbol
            }

            string localPart = parts[0];
            string domainPart = parts[1];
            if (string.IsNullOrWhiteSpace(localPart) || string.IsNullOrWhiteSpace(domainPart))
            {
                return false; // local and domain parts cannot be empty
            }

            // check local part for valid characters
            foreach (char c in localPart)
            {
                if (!char.IsLetterOrDigit(c) && c != '.' && c != '_' && c != '-')
                {
                    return false; // local part contains invalid character
                }
            }

            // check domain part for valid format
            if (domainPart.Length < 2 || !domainPart.Contains(".") || domainPart.Split(".").Length != 2 || domainPart.EndsWith(".") || domainPart.StartsWith("."))
            {
                return false; // domain part is not a valid format
            }

            return true;
        }
    
    private DalApi.IDal _dal = DalApi.Factory.Get;
        public int Create(Engineer boEngineer)
        {
            if (boEngineer.Id <= 0 || boEngineer.Name == "" || boEngineer.Cost < 0|| !ValidateEmail(boEngineer.Email))
                throw new BO.BlNotValid("one or more of the details is not valid");
            DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level!, boEngineer.Cost);
            try
            {
                int idEngineer = _dal.Engineer.Create(doEngineer);

                return idEngineer;
            }
            catch (DO.DalAlreadyExistsException ex)
            {
                throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
            }

        }

        public int Delete(int id)
        {
            if (FindCurrentTask(id) != null)
            {
                //if (!_dal.Task.ReadAll((task) => task.EngineerId == id && task.Complete < DateTime.Now).Any())

                try
                {
                    _dal.Engineer.Delete(id);
                }
                catch (DO.DalDoesNotExistException)
                {
                    throw new BO.BlDoesNotExistException($"Engineer with ID={id} already exists", null!);
                }
                catch (DO.DalDeletionImpossible ex)

                {
                    throw new BO.BlDeletionImpossible($"Engineer with ID={id} has some tasks",ex);
                }

            }
            return id;
        }

        public Engineer? Read(int id)
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);
            if (doEngineer == null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", null!);

            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = (EngineerExperience)doEngineer.Level!,
                Cost = (double)doEngineer.Cost!,
                CurrentTask = FindCurrentTask(doEngineer.Id)

            };
        }
        private Tuple<int, string> FindCurrentTask(int id)
        {
            DateTime today = DateTime.Now;
            List<DO.Task> tasks = (List<DO.Task>)_dal.Task.ReadAll((task) => task.EngineerId == id && task.Complete > today && task.Start < today);
            return new Tuple<int, string>(tasks.First().Id, tasks.First().Alias!);
        }
        public IEnumerable<Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
        {
            IEnumerable<Engineer> engineers=_dal.Engineer.ReadAll().Select(doEngineer =>
                     new Engineer
                    {
                        Id = doEngineer!.Id,
                        Name = doEngineer.Name,
                        Email = doEngineer.Email,
                        Level = (EngineerExperience)doEngineer.Level!,
                        Cost = (double)doEngineer.Cost!,
                        CurrentTask = FindCurrentTask(doEngineer.Id)
                    });
            if(filter == null)
                return engineers;
            return engineers.Where(filter);
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
                throw new BlDoesNotExistException($"Engineer with ID={boEngineer.Id} doern't exists", ex);
            }
            return boEngineer.Id;
        }
    }
}
