//using AutoMapper;
using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlImplementation
{
    internal class EngineerImplementation : IEngineer
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        //private readonly IMapper _mapper;

        //public EngineerImplementation()
        //{
        //    _mapper = AutoMapperConfiguration.Configure(_dal);
        //}

        /// <summary>
        /// Validates the format of an email address.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True if the email address is valid; otherwise, false.</returns>
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

        /// <summary>
        /// Creates a new engineer in the system.
        /// </summary>
        /// <param name="boEngineer">The engineer object to create.</param>
        /// <returns>The ID of the created engineer.</returns>
        public int Create(Engineer boEngineer)
        {
            if (boEngineer.Id <= 99999999 || boEngineer.Name == "" || boEngineer.Cost < 0 || !ValidateEmail(boEngineer.Email))
                throw new BO.BlInvalidPropertyException("One or more details are not valid");

            DO.EngineerExperience? level = null;
            if (boEngineer.Level != null && boEngineer.Level != EngineerExperience.None)
                level = (DO.EngineerExperience)boEngineer.Level!;

            DO.Engineer doEngineer = new DO.Engineer(boEngineer.Id, boEngineer.Name, boEngineer.Email, level, boEngineer.Cost);

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

        /// <summary>
        /// Deletes an engineer from the system.
        /// </summary>
        /// <param name="id">The ID of the engineer to delete.</param>
        /// <returns>The ID of the deleted engineer.</returns>
        public int Delete(int id)
        {
            if (FindCurrentTask(id) != null)
            {
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
                    throw new BO.BlDeletionImpossible($"Engineer with ID={id} has some tasks", ex);
                }
            }
            return id;
        }

        /// <summary>
        /// Reads details of an engineer.
        /// </summary>
        /// <param name="id">The ID of the engineer to read.</param>
        /// <returns>The details of the engineer.</returns>
        public Engineer? Read(int id)
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);

            if (doEngineer == null)
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist", null!);

            BO.EngineerExperience? level = null; // Moved inside the Select lambda
            if (doEngineer!.Level != null)
                level = (BO.EngineerExperience)doEngineer.Level!;

            return new BO.Engineer()
            {
                Id = id,
                Name = doEngineer.Name,
                Email = doEngineer.Email,
                Level = level,
                Cost = (double)doEngineer.Cost!,
                CurrentTask = FindCurrentTask(doEngineer.Id)
            };
        }

        /// <summary>
        /// Finds the current task for an engineer.
        /// </summary>
        /// <param name="id">The ID of the engineer.</param>
        /// <returns>A tuple containing the ID and alias of the current task; or (-1, "NoTaskFound") if no task is found.</returns>
        private Tuple<int, string> FindCurrentTask(int id)
        {
            DateTime today = DateTime.Now;

            List<DO.Task> tasks = _dal.Task.ReadAll(task => task.EngineerId == id && task.Complete > today && task.Start < today).ToList()!;

            if (tasks.Any())
            {
                return new Tuple<int, string>(tasks.First().Id, tasks.First().Alias!);
            }
            else
            {
                // Handle the case where no tasks are found
                return new Tuple<int, string>(-1, "NoTaskFound");
            }
        }

        /// <summary>
        /// Reads all engineers in the system.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The list of engineers.</returns>
        public IEnumerable<Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
        {
            IEnumerable<Engineer> engineers = _dal.Engineer.ReadAll().Select(doEngineer =>
            {
                BO.EngineerExperience? level = null; // Moved inside the Select lambda
                if (doEngineer!.Level != null)
                    level = (BO.EngineerExperience)doEngineer.Level!;

                return new Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = level,
                    Cost = (double)doEngineer.Cost!,
                    CurrentTask = FindCurrentTask(doEngineer.Id)
                };
            });

            if (filter == null)
                return engineers;

            return engineers.Where(filter);
        }

        /// <summary>
        /// Updates the details of an engineer.
        /// </summary>
        /// <param name="boEngineer">The engineer object with updated details.</param>
        /// <returns>The ID of the updated engineer.</returns>
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
                throw new BlDoesNotExistException($"Engineer with ID={boEngineer.Id} doesn't exist", ex);
            }

            return boEngineer.Id;
        }
    }
}
