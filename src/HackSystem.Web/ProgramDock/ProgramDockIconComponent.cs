﻿using HackSystem.Web.Component.Popover;
using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using Microsoft.AspNetCore.Components.Web;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockIconComponent
{
    public async Task OnClick(MouseEventArgs args)
        => await this.RaiseIconSelect();

    public async Task OnTouchEnd(TouchEventArgs args)
        => await this.RaiseIconSelect();

    protected async Task RaiseIconSelect()
    {
        if (!this.OnIconSelect.HasDelegate) return;

        var eventArgs = new ProgramIconEventArgs(this.UserProgramMap);
        await this.OnIconSelect.InvokeAsync(eventArgs);
    }

    public async Task UpdateWindowDetail(ProgramWindowDetail windowDetail, WindowChangeStates changeState)
    {
        this.pendingRefreshWindows = true;
        this.StateHasChanged();
        this.pendingRefreshWindows = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            this.PopoverReplacementId = await this.popoverHandler.SetupPopover(new PopoverDetail()
            {
                ShowDelay = 150,
                IsHtmlContent = true,
                Content = this.DockIconPopoverId,
                Trigger = PopoverTriggers.Hover,
                Placement = PopoverPlacements.Top,
                TargetElemantFilter = this.DockIconId,
                Title = this.UserProgramMap.Rename ?? this.UserProgramMap.Program.Name,
                ReplaceContent = true,
            });
        }

        if (pendingRefreshWindows)
        {
            await this.popoverHandler.RefreshContent(this.DockIconId, this.PopoverReplacementId, this.DockIconPopoverId);
            pendingRefreshWindows = false;
        }
    }
}