﻿using HackSystem.WebAPI.Domain.Entity;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.Infrastructure.DataSeed;
using HackSystem.WebAPI.MockServer.Domain.Entity;
using HackSystem.WebAPI.ProgramServer.Domain.Entity;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;
using HackSystem.WebAPI.TaskServer.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HackSystem.WebAPI.Infrastructure.DBContexts;

public class HackSystemDbContext : IdentityDbContext<HackSystemUser, HackSystemRole, string>
{
    private readonly ILogger<HackSystemDbContext> logger;

    public HackSystemDbContext() { }

    public HackSystemDbContext(DbContextOptions<HackSystemDbContext> options)
        : base(options)
    {
    }

    public HackSystemDbContext(
        ILogger<HackSystemDbContext> logger,
        DbContextOptions<HackSystemDbContext> options)
        : base(options)
    {
        this.logger = logger;
    }

    public virtual DbSet<BasicProgram> BasicPrograms { get; set; }

    public virtual DbSet<ProgramUser> ProgramUsers { get; set; }

    public virtual DbSet<UserBasicProgramMap> UserBasicProgramMaps { get; set; }

    public virtual DbSet<TaskDetail> TaskDetails { get; set; }

    public virtual DbSet<TaskLogDetail> TaskLogDetails { get; set; }

    public virtual DbSet<MockRouteDetail> MockRouteDetails { get; set; }

    public virtual DbSet<MockRouteLogDetail> MockRouteLogDetails { get; set; }

    public virtual DbSet<WebAPILog> WebAPILogs { get; set; }

    public virtual DbSet<GenericOption> GenericOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserBasicProgramMap>().HasOne(map => map.BasicProgram).WithMany(program => program.UserProgramMaps).HasForeignKey(map => map.ProgramId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<UserBasicProgramMap>().HasOne(map => map.ProgramUser).WithMany(user => user.UserProgramMaps).HasForeignKey(map => map.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<UserBasicProgramMap>().HasKey(map => new { map.UserId, map.ProgramId });
        builder.Entity<TaskDetail>().HasMany<TaskLogDetail>().WithOne().HasForeignKey(l => l.TaskID).IsRequired();
        builder.Entity<MockRouteDetail>().HasMany<MockRouteLogDetail>().WithOne().HasForeignKey(l => l.RouteID).IsRequired();

        builder.Entity<HackSystemUser>().HasOne<ProgramUser>().WithOne().HasForeignKey<HackSystemUser>(u => u.Id).IsRequired();
        builder.Entity<BasicProgram>().HasIndex(nameof(BasicProgram.Id), nameof(BasicProgram.Name));
        builder.Entity<UserBasicProgramMap>().HasIndex(nameof(UserBasicProgramMap.UserId));
        builder.Entity<UserBasicProgramMap>().HasIndex(nameof(UserBasicProgramMap.UserId), nameof(UserBasicProgramMap.ProgramId)).IsUnique();

        builder.Entity<TaskDetail>().HasIndex(nameof(TaskDetail.Enabled));
        builder.Entity<TaskDetail>().HasIndex(nameof(TaskDetail.TaskName)).IsUnique();
        builder.Entity<TaskDetail>().HasIndex(nameof(TaskDetail.TaskName), nameof(TaskDetail.ExecuteDateTime));
        builder.Entity<TaskLogDetail>().HasIndex(nameof(TaskLogDetail.TaskID));
        builder.Entity<TaskLogDetail>().HasIndex(nameof(TaskLogDetail.TaskID), nameof(TaskLogDetail.TaskLogStatus));

        builder.Entity<MockRouteDetail>().HasIndex(nameof(MockRouteDetail.Enabled));
        builder.Entity<MockRouteDetail>().HasIndex(nameof(MockRouteDetail.MockURI), nameof(MockRouteDetail.MockMethod), nameof(MockRouteDetail.MockSourceHost)).IsUnique();
        builder.Entity<MockRouteLogDetail>().HasIndex(nameof(MockRouteLogDetail.RouteID));
        builder.Entity<MockRouteLogDetail>().HasIndex(nameof(MockRouteLogDetail.URI), nameof(MockRouteLogDetail.Method), nameof(MockRouteLogDetail.SourceHost), nameof(MockRouteLogDetail.MockType));

        builder.Entity<WebAPILog>().HasIndex(nameof(WebAPILog.RequestURI), nameof(WebAPILog.SourceHost), nameof(WebAPILog.IdentityName), nameof(WebAPILog.StartDateTime));

        builder.Entity<MockRouteDetail>().Property(nameof(MockRouteDetail.MockURI)).UseCollation("NOCASE");
        builder.Entity<MockRouteDetail>().Property(nameof(MockRouteDetail.MockMethod)).UseCollation("NOCASE");
        builder.Entity<MockRouteDetail>().Property(nameof(MockRouteDetail.MockSourceHost)).UseCollation("NOCASE");

        builder.Entity<GenericOption>().HasIndex(nameof(GenericOption.OptionName), nameof(GenericOption.Category), nameof(GenericOption.OwnerLevel)).IsUnique();

        builder.Entity<GenericOption>().Property(nameof(GenericOption.OptionName)).UseCollation("NOCASE");
        builder.Entity<GenericOption>().Property(nameof(GenericOption.Category)).UseCollation("NOCASE");
        builder.Entity<GenericOption>().Property(nameof(GenericOption.OwnerLevel)).UseCollation("NOCASE");

        builder.InitializeBasicProgramData()
               .InitializeIdentityData()
               .InitializeTaskData();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.LogTo(this.Log);
    }

    private void Log(string message)
        => this.logger?.LogDebug(message);
}