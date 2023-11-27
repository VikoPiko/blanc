using blanc.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace blanc.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private ObservableCollection<Reservation> _reservations;
        private Reservation _selectedReservation;
        public ObservableCollection<Reservation> Reservations
        {
            get { return _reservations; }
            set { _reservations = value; OnPropertyChanged(nameof(Reservations)); }
        }

      
        public string InputNumber { get; set; }
        public string InputName { get; set; }

        

        public ICommand AddReservationCommand { get; private set; }

        public ICommand RemoveReservationCommand { get; private set; }

        public CategoryViewModel()
        {
            RemoveReservationCommand = new RelayCommand(RemoveReservation, CanRemoveReservation);
            Reservations = new ObservableCollection<Reservation>();
            AddReservationCommand = new RelayCommand(AddReservation);
        }

        private void AddReservation()
        {
            var newReservation = new Reservation
            {
                Id = Reservations.Count + 1, // Примерен начин за генериране на Id
                Number = InputNumber,
                Name = InputName
            };

            Reservations.Add(newReservation);
        }
      
        public Reservation SelectedReservation
        {
            get { return _selectedReservation; }
            set { _selectedReservation = value; OnPropertyChanged(nameof(SelectedReservation)); }
        }
    

        private bool CanRemoveReservation()
        {
            return SelectedReservation != null;
        }

        private void RemoveReservation()
        {
            if (SelectedReservation != null)
            {
                Reservations.Remove(SelectedReservation);
            }
        }
    }
}
