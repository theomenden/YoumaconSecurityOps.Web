using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Charts;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using YoumaconSecurityOps.Core.EventStore.Events;
using YoumaconSecurityOps.Core.Mediatr.Queries;
using YoumaconSecurityOps.Core.Shared.Models.Readers;
using YoumaconSecurityOps.Web.Client.Services;

namespace YoumaconSecurityOps.Web.Client.Pages
{
    public partial class Admin : ComponentBase
    {
        #region Injected Members
        [Inject] public IEventReaderService EventReaderService { get; set; }

        [Inject] public IShiftService ShiftService { get; set; }

        [Inject] public ILocationService LocationService { get; set; }

        [Inject] public INotificationService NotificationService { get; set; }
        #endregion
        #region Fields
        private DataGrid<EventReader> _eventDataGrid = new();

        private Chart<Int32> _shiftsByLocationBarChart;

        private readonly List<string> _backgroundColors = new() { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
        private readonly List<string> _borderColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        private Boolean _isAlreadyInitialised;

        private List<ShiftReader> _shifts = new(200);

        private List<LocationReader> _locations = new(8);

        private List<EventReader> _events = new(2000);

        private List<EventReader> _eventDataGridDisplay = new(2000);

        private Int32 _totalEvents = 0;

        private EventReader _selectedEvent;
        #endregion
        #region Draw Methods
        private async Task HandleShiftLocationGraphRedraw<TDataSet, TItem, TOptions, TModel>(BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<TDataSet> getDataSet)
            where TDataSet : ChartDataset<TItem>
            where TOptions : ChartOptions
            where TModel : ChartModel
        {
            await chart.Clear();

            await chart.AddLabelsDatasetsAndUpdate(_locations.Select(l => l.Name).ToList(), getDataSet());
        }

        private async Task SetDataAndUpdate<TDataSet, TItem, TOptions, TModel>(Blazorise.Charts.BaseChart<TDataSet, TItem, TOptions, TModel> chart, Func<List<TItem>> items)
            where TDataSet : ChartDataset<TItem>
            where TOptions : ChartOptions
            where TModel : ChartModel
        {
            await chart.SetData(0, items());
            await chart.Update();
        }

        private BarChartOptions ConfigureBarChartOptions()
        {
            return new()
            {
                Scales = new()
                {
                    XAxes = new List<Axis>(_locations.Select(l => new Axis()
                    {
                        ScaleLabel = new() { LabelString = l.Name },
                        Type = l.Name
                    }))

                }
            };
        }

        #endregion
        #region Page Configuration Methods
        protected override async Task OnInitializedAsync()
        {
            await LoadAdministrativeData().ConfigureAwait(false);

            await LoadEventStoreData();

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_isAlreadyInitialised)
            {
                _isAlreadyInitialised = true;

                await Task.WhenAll(HandleShiftLocationGraphRedraw(_shiftsByLocationBarChart, GetShiftLocationBarChartData));
            }
        }
        #endregion
        #region Data Retrieval Methods
        private async Task LoadAdministrativeData()
        {
            _locations = await LocationService.GetLocationsAsync(new GetLocationsQuery());

            _shifts = await ShiftService.GetShiftsAsync(new GetShiftListQuery());
        }

        private async Task LoadEventStoreData()
        {
            _events = await EventReaderService.GetAllEventsAsync(new GetEventListQuery());

            _totalEvents = _events.Count;
        }
        #endregion
        #region Chart Data Retrieval Methods
        private BarChartDataset<int> GetShiftLocationBarChartData()
        {
            return new()
            {
                Label = "Number of Shifts By Location",
                Data = CalculateShiftLocationInformation(),
                BackgroundColor = _backgroundColors,
                BorderColor = _borderColors,
                BorderWidth = 1
            };
        }

        private List<int> CalculateShiftLocationInformation()
        {
            var shiftsGroupedByLocation =
                from shift in _shifts
                group shift.Id by shift.CurrentLocation
                into locationGroup
                orderby locationGroup.Key.Name
                select locationGroup;

            return shiftsGroupedByLocation.Select(sg => sg.Count()).ToList();
        }
        #endregion
        #region EventDataGrid Configuration
        private Task OnReadEventData(DataGridReadDataEventArgs<EventReader> e)
        {
            if (!e.CancellationToken.IsCancellationRequested)
            {
                _eventDataGridDisplay = _events.ToList();
            }

            StateHasChanged();

            return Task.CompletedTask;
        }

        private static void OnRowStyling(EventReader eventToStyle, DataGridRowStyling styling)
        {
        }

        private Task Reset()
        {
            return _eventDataGrid.Reload();
        }
        #endregion

    }
}
