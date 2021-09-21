﻿using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.TaskServer.Domain.Entity;

namespace HackSystem.WebAPI.TaskServer.Repository;

public interface ITaskLogRepository : IRepositoryBase<TaskLogDetail>
{
}
