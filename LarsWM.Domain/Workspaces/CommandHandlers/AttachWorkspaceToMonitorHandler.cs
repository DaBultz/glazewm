﻿using LarsWM.Domain.Workspaces.Commands;
using LarsWM.Domain.Workspaces.Events;
using LarsWM.Infrastructure.Bussing;

namespace LarsWM.Domain.Workspaces.CommandHandlers
{
  class AttachWorkspaceToMonitorHandler : ICommandHandler<AttachWorkspaceToMonitorCommand>
  {
    private WorkspaceService _workspaceService;
    public Bus _bus { get; }

    public AttachWorkspaceToMonitorHandler(WorkspaceService workspaceService, Bus bus)
    {
      _workspaceService = workspaceService;
      _bus = bus;
    }

    public CommandResponse Handle(AttachWorkspaceToMonitorCommand command)
    {
      command.Monitor.AddChild(command.Workspace);
      _workspaceService.InactiveWorkspaces.Remove(command.Workspace);

      _bus.RaiseEvent(new WorkspaceAttachedEvent(command.Workspace));

      return CommandResponse.Ok;
    }
  }
}
