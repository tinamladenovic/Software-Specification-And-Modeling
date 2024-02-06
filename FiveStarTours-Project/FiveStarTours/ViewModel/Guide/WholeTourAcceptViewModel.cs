using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using FiveStarTours.Model;
using FiveStarTours.Services;
using FiveStarTours.ViewModel.Command;

namespace FiveStarTours.ViewModel.Guide
{
    public class WholeTourAcceptViewModel : FiveStarTours.ViewModel.BindableBase.BindableBase
    {
        public event EventHandler RequestClose;
        public ICommand AcceptCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CheckCommand { get; }

        private readonly ToursService _tourService;
        private readonly TourRequestService _tourRequestService;

        public User LoggedInUser { get; set; }
        public Location Location { get; set; }
        public Language Language { get; set; }
        public DateInterval Interval { get; set; }
        public int GuestNumber { get; set; }
        public string Description { get; set; }
        public TourRequest TourRequest { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                SetProperty(ref _selectedDate, value);
            }
        }

        private string _selectedTime;
        public string SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                SetProperty(ref _selectedTime, value);               
            }
        }

        public WholeTourAcceptViewModel(User user, TourRequest request)
        {
            _tourService = new ToursService();
            _tourRequestService = new TourRequestService();

            LoggedInUser = user;
            Location = request.Location;
            Language = request.Language;
            Interval = request.Intervals;
            GuestNumber = request.MaxGuests;
            Description = request.Description;
            SelectedDate = DateTime.Today;
            SelectedTime = "12:00:00";
            TourRequest = request;

            AcceptCommand = new RelayCommand(AcceptRequest);
            CancelCommand = new RelayCommand(CancelRequest);
            CheckCommand = new RelayCommand(CheckScedule);
        }

        private void AcceptRequest()
        {

            DateTime dateTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day) + TimeSpan.Parse(SelectedTime);
            TourRequest.Status = Model.Enums.ReservationChangeStatusType.Approved;
            TourRequest.AcceptDateTime = dateTime;
            _tourRequestService.SaveChanges(TourRequest);
            MessageBox.Show("Request accepted..");
            CancelRequest();

        }

        private void CancelRequest()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void CheckScedule()
        { 
            List<DateTime> dates = _tourService.GetAllDates();
            if(dates.Count == 0)
            {
                return;
            }
            foreach (DateTime date in dates)
            {
                if (date.Date == SelectedDate && date.TimeOfDay.ToString() == SelectedTime)
                {
                    MessageBox.Show("This date is not free.");
                }
            }

            if (SelectedDate >= Interval.Start && SelectedDate <= Interval.End)
            {
                MessageBox.Show("This date is valid.");
            }
            else
            {
                MessageBox.Show("This date is not in interval.");
            }
        }
    }
}
