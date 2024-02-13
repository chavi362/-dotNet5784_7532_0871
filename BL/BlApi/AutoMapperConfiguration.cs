using AutoMapper;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlApi
{
    internal class AutoMapperConfiguration
    {
        private static IDal? _dal; // Ensure that _dal is declared at the class level

        // Configure AutoMapper mappings
        public static IMapper Configure(IDal dal)
        {
            _dal = dal; // Initialize _dal with the provided dal instance

            var config = new MapperConfiguration(cfg =>
            {
                // Map DO.Engineer to BO.Engineer
                cfg.CreateMap<DO.Engineer, BO.Engineer>()
                  .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (BO.EngineerExperience?)src.Level))
                  .ForMember(dest => dest.CurrentTask, opt => opt.MapFrom(src => FindCurrentTask(src.Id)));

                // Map DO.Task to BO.Task
                _ = cfg.CreateMap<DO.Task, BO.Task>()
                    .ForMember(dest => dest.ComplexityLevel, opt => opt.MapFrom(src => (BO.EngineerExperience?)src.ComplexityLevel))
                    .ForMember(dest => dest.CreatedAtDate, opt => opt.MapFrom(src => src.CreatedAtDate ?? DateTime.MinValue))
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Start ?? DateTime.MinValue))
                    .ForMember(dest => dest.ForecastDate, opt => opt.MapFrom(src => src.Forecast ?? DateTime.MinValue))
                    .ForMember(dest => dest.DeadlineDate, opt => opt.MapFrom(src => src.DeadLineDate ?? DateTime.MinValue))
                    .ForMember(dest => dest.CompleteDate, opt => opt.MapFrom(src => src.Complete ?? DateTime.MinValue))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => GetTaskStatus(src)))
                    .ForMember(dest => dest.Milestone, opt => opt.MapFrom(src => GetMilestone(src)))
                    .ForMember(dest => dest.DependenceList, opt => opt.MapFrom(src => ReadTaskInList(src.Id)));

                // Add other mappings as needed...
            });

            return config.CreateMapper();
        }

        // Find and map the current task for the engineer
        private static BO.EngineerInTask? FindCurrentTask(int engineerId)
        {
            // Implement the logic to find and map the current task for the engineer
            // Return the mapped BO.EngineerInTask or null if no current task
            return null;
        }

        // Get the status of a task based on its completion and start dates
        private static BO.Status GetTaskStatus(DO.Task doTask)
        {
            if (doTask.Complete != null && doTask.Complete <= DateTime.Now)
            {
                return BO.Status.Complete;
            }

            if (doTask.Start == null || doTask.Forecast == null || doTask.DeadLineDate == null)
            {
                return BO.Status.Unscheduled;
            }

            if (doTask.Start > DateTime.Now)
            {
                return BO.Status.Scheduled;
            }

            if (DateTime.Now <= doTask.DeadLineDate)
            {
                return BO.Status.OnTrack;
            }

            return BO.Status.InJeopardy;
        }

        // Placeholder for GetMilestone logic, needs implementation
        private static object GetMilestone(DO.Task src)
        {
            throw new NotImplementedException();
        }

        // Read and map Task dependencies for a given task ID
        private static IEnumerable<BO.TaskInList?> ReadTaskInList(int taskId)
        {
            IEnumerable<DO.Dependency> dependencies;
            List<BO.TaskInList>? dependenciesOfTask = null;

            try
            {
                dependencies = _dal!.Dependency.ReadAll((dependency) => dependency.DependsOnTask == taskId)!;

                if (dependencies.Any())
                {
                    dependenciesOfTask = dependencies
                        .Select(dependency =>
                        {
                            DO.Task? dependTask = _dal.Task.Read(dependency!.DependentTask);

                            if (dependTask != null)
                            {
                                return new BO.TaskInList
                                {
                                    Id = dependTask.Id,
                                    Description = dependTask.Description,
                                    Alias = dependTask.Alias!,
                                    Status = GetTaskStatus(dependTask)
                                };
                            }

                            return null;
                        })
                        .OfType<BO.TaskInList>()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dependenciesOfTask!;
        }
    }
}
