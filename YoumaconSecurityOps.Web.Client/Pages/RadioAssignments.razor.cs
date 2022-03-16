using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Web.Client.Services;
using Modal = Blazorise.Bootstrap5.Modal;

namespace YoumaconSecurityOps.Web.Client.Pages;

public partial class RadioAssignments : ComponentBase
{
    #region Injected Services
    [Inject]
    public INotificationService NotificationService { get; set; }

    [Inject]
    public IPageProgressService PageProgressService { get; set; }

    [Inject]
    public IRadioScheduleService RadioScheduleService { get; set; }

    [Inject]
    public IStaffService StaffService { get; set; }

    [Inject]
    public ILocationService LocationService { get; set; }
    #endregion

    public int CurrentPage { get; set; } = 1;

    #region Private Fields

    private Int32 _totalRadios;

    private RadioScheduleReader _selectedRadio;

    private IEnumerable<RadioScheduleReader> _radios = new List<RadioScheduleReader>(20);

    private IEnumerable<RadioScheduleReader> _gridDisplay = new List<RadioScheduleReader>(20);

    private IEnumerable<StaffReader> _staffMembers = new List<StaffReader>(100);

    private IEnumerable<LocationReader> _locations = new List<LocationReader>(20);

    private DataGrid<RadioScheduleReader> _dataGrid = new();

    private Modal _modalRef;

    private Guid _selectedStaffMember = Guid.Empty;

    private Guid _selectedStartingLocation = Guid.Empty;

    private DateTime? _selectedStartDate;
    private DateTime? _selectedEndDate;
    #endregion
        

    #region DataGrid Loading Methods
    private async Task LoadRadioData()
    {
        _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());

        _staffMembers = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

        _radios = await RadioScheduleService.GetRadiosAsync(new GetRadioSchedule());
    }

    private async Task OnReadData(DataGridReadDataEventArgs<RadioScheduleReader> e)
    {
        await LoadRadioData();

        if (!e.CancellationToken.IsCancellationRequested)
        {
            _totalRadios = _radios.Count();
                
            _gridDisplay = _radios.Skip((e.Page - 1) * e.PageSize).Take(e.PageSize).ToList();
        }

        StateHasChanged();
    }

    private static void OnRowStyling(RadioScheduleReader radio, DataGridRowStyling styling)
    {

        if (radio is not null && radio.IsCharging)
        {
            styling.Background = Background.Warning;
        }
    }

    private static String DetermineDisplayIcon(Boolean statusCheck)
    {
        return statusCheck ? " fa-check-circle text-success" : " fa-times-circle text-danger";
    }

    private Task Reset()
    {
        return _dataGrid.Reload();
    }

    private static string SetPopupTitle(PopupTitleContext<RadioScheduleReader> context)
    {
        var popupTitle = context.LocalizationString;

        if (context.EditState is not DataGridEditState.Edit)
        {
            popupTitle += " Radio";
        }

        if (context.Item is not null)
        {
            popupTitle += $" {context.Item.RadioNumber}";
        }

        return popupTitle;
    }
    #endregion

    #region DataGrid Mutation Methods
    #endregion

    #region SelectList Methods
    private void OnSelectedStaffMemberChanged(Guid value)
    {
        _selectedStaffMember = value;

        StateHasChanged();
    }

    private void OnSelectedStartingLocationChanged(Guid value)
    {
        _selectedStartingLocation = value;

        StateHasChanged();
    }

    private void OnStartDateChanged(DateTime? date)
    {
        _selectedStartDate = date;
    }

    private void OnEndDateChanged(DateTime? date)
    {
        _selectedEndDate = date;
    }
    #endregion
}