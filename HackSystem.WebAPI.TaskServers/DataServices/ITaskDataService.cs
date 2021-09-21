﻿using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.TaskServers.Domain.Entity;

namespace HackSystem.WebAPI.TaskServers.DataServices;

public interface ITaskDataService : IDataServiceBase<TaskDetail>
{
    Task<IEnumerable<TaskDetail>> QueryTasks();

    Task<IEnumerable<TaskDetail>> QueryEnabledTasks();
}
