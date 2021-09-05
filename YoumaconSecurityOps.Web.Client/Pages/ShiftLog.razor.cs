using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Core.Mediatr.Commands;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Enumerations;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Web.Client.Services;
using Modal = Blazorise.Bootstrap.Modal;

namespace YoumaconSecurityOps.Web.Client.Pages
{
    public partial class ShiftLog : ComponentBase
    {
        #region Injected Services
        [Inject] public IShiftService ShiftService { get; set; }

        [Inject] public ILocationService LocationService { get; set; }

        [Inject] public IStaffService StaffService { get; set; }

        [Inject] public INotificationService Notifications { get; set; }
        #endregion
        #region Fields
        private IEnumerable<ShiftReader> _shifts = new List<ShiftReader>(200);

        private List<ShiftReader> _gridDisplay = new(200);

        private IEnumerable<StaffReader> _staffMembers = new List<StaffReader>(200);

        private IEnumerable<LocationReader> _locations = new List<LocationReader>(15);

        private ShiftReader _selectedShift;

        private Int32 _totalShifts = 0;

        private DataGrid<ShiftReader> _dataGrid = new();

        private Modal _modalRef;

        private Guid _selectedStaffMember = Guid.Empty;

        private Guid _selectedStartingLocation = Guid.Empty;

        private DateTime? _selectedStartDate;
        private DateTime? _selectedEndDate;

        private Boolean _isLoading = false;
        #endregion
        #region DataGrid Configuration Methods
        private async Task LoadShiftData()
        {
            _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());

            _staffMembers = await StaffService.GetStaffMembersAsync(new GetStaffQuery());

            _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());

        }

        private async Task OnReadData(DataGridReadDataEventArgs<ShiftReader> e)
        {
            await LoadShiftData();

            if (!e.CancellationToken.IsCancellationRequested)
            {

                _totalShifts = _shifts.Count();

                _gridDisplay = _shifts.ToList();
            }

            StateHasChanged();
        }

        private static void OnRowStyling(ShiftReader shift, DataGridRowStyling styling)
        {
            var staffOnShift = shift.StaffMember;

            if (shift.IsLate)
            {
                styling.Background = Background.Warning;
            }

            if (staffOnShift?.IsOnBreak ?? false)
            {
                styling.Background = Background.Danger;
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
        #endregion
        #region SelectList Methods
        private void OnStartDateChanged(DateTime? date)
        {
            _selectedStartDate = date;
        }

        private void OnEndDateChanged(DateTime? date)
        {
            _selectedEndDate = date;
        }

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
        #endregion
        #region DataGrid Mutation Methods
        private async Task OnRowInserting(CancellableRowChange<ShiftReader, Dictionary<string, object>> newShift)
        {
            var staffMemberAssigned = _staffMembers.First(st => st.Id == _selectedStaffMember);

            var startingLocation = _locations.First(l => l.Id == (_selectedStartingLocation));

            var addShiftCommand = new AddShiftCommand(_selectedStartDate.GetValueOrDefault(DateTime.Now), _selectedEndDate.GetValueOrDefault(DateTime.Now),
                staffMemberAssigned.Id, staffMemberAssigned.Contact.PreferredName, startingLocation.Id);

            var addedEntityResponse = await ShiftService.AddShiftAsync(addShiftCommand);

            if (addedEntityResponse.ResponseCode is not ResponseCodes.ApiSuccess)
            {
                await Notifications.Error(new MarkupString($"<em>{addedEntityResponse.ResponseMessage}</em>"), "Failed to add shift");

                return;
            }

            newShift.Item.Id = addedEntityResponse.Data;
            newShift.Item.StaffMember = staffMemberAssigned;
            newShift.Item.StaffMember.Contact = staffMemberAssigned.Contact;
            newShift.Item.StartingLocationNavigation = startingLocation;
            newShift.Item.CurrentLocation = startingLocation;
            newShift.Item.StartAt = _selectedStartDate.GetValueOrDefault();
            newShift.Item.EndAt = _selectedEndDate.GetValueOrDefault();

            await Notifications.Success(new MarkupString($"<em>{addedEntityResponse.ResponseMessage}</em>"),
                "Successfully Added Shift");

            StateHasChanged();
        }

        private void OnRowUpdated(SavedRowItem<ShiftReader, Dictionary<string, object>> e)
        {
            var shift = e.Item;


            //var employee = e.Item;

            //employee.FirstName = (string)e.Values["FirstName"];
            //employee.LastName = (string)e.Values["LastName"];
            //employee.EMail = (string)e.Values["EMail"];
            //employee.City = (string)e.Values["City"];
            //employee.Zip = (string)e.Values["Zip"];
            //employee.DateOfBirth = (DateTime?)e.Values["DateOfBirth"];
            //employee.Childrens = (int?)e.Values["Childrens"];
            //employee.Gender = (string)e.Values["Gender"];
            //employee.Salary = (decimal)e.Values["Salary"];
        }

        private async Task OnCheckedIn(Guid shiftId)
        {
            _isLoading = true;

            var checkInCommand = new ShiftCheckInCommand(shiftId);

            var checkedInResponse = await ShiftService.CheckIn(checkInCommand);

            var markUpResponse = new MarkupString($"<em>{checkedInResponse.ResponseMessage}</em>");

            if (checkedInResponse.ResponseCode is not ResponseCodes.ApiSuccess)
            {
                await Notifications.Error(markUpResponse, "Failed to add shift");

                _isLoading = false;

                return;
            }

            await Notifications.Success(markUpResponse, $"Checked Out Successfully");

            _isLoading = false;
        }

        private async Task OnCheckedOut(Guid shiftId)
        {
            _isLoading = true;

            var checkedOutCommand = new ShiftCheckoutCommand(shiftId);

            var checkedOutResponse = await ShiftService.CheckOut(checkedOutCommand);

            var markUpResponse = new MarkupString($"<em>{checkedOutResponse.ResponseMessage}</em>");

            if (checkedOutResponse.ResponseCode is not ResponseCodes.ApiSuccess)
            {
                await Notifications.Error(markUpResponse, "Failed to add shift");
                _isLoading = false;
                return;
            }

            await Notifications.Success(markUpResponse, "Checked Out Successfully");
            _isLoading = false;
        }

        private async Task OnReportingIn(Guid shiftId)
        {
            _isLoading = true;
            var reportInCommand = new ShiftReportInCommand(shiftId, Guid.NewGuid());

            var reportedInResponse = await ShiftService.ReportIn(reportInCommand);

            var markUpResponse = new MarkupString($"<em>{reportedInResponse.ResponseMessage}</em>");

            if (reportedInResponse.ResponseCode is not ResponseCodes.ApiSuccess)
            {
                await Notifications.Error(markUpResponse, "Failed to add shift");

                _isLoading = false;

                return;
            }

            await Notifications.Success(markUpResponse, "Reported In Successfully");
            _isLoading = false;
        }
        #endregion
        #region Edit Form Methods
        private void ShowModal(Guid shiftId)
        {
            _selectedShift = _shifts.Single(sh => sh.Id == shiftId);

            _modalRef.Show();
        }

        private void HideModal()
        {
            _modalRef.Hide();
        }
        #endregion
    }
}
