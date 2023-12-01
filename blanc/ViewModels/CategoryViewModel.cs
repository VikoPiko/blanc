using blanc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.Json;
using System.IO;

namespace blanc.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        const string fileName = "D:\\vikoEdit\\blanc\\blanc\\jsonFiles\\Reservations.json";


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
            AddReservationCommand = new RelayCommand(AddReservationAsync);


            string rawJson = File.ReadAllText(fileName);

            List<Reservation> reservations = JsonConvert.DeserializeObject<List<Reservation>>(rawJson);
            
            if(reservations != null)
            {
                foreach (var item in reservations)
                {
                    Reservations.Add(item);
                }
            } 
        }

        private void AddReservationAsync()
        {
            var newReservation = new Reservation
            {
                Id = Reservations.Count + 1, // Примерен начин за генериране на Id
                Number = InputNumber,
                Name = InputName
            };

            Reservations.Add(newReservation);

            List<Reservation> reservations = new List<Reservation>();

            foreach (Reservation reservation in Reservations)
            {
                reservations.Add(new Reservation()
                {
                    Id = reservation.Id,
                    Name = reservation.Name,
                    Number = reservation.Number,
                });
            }

            string json = JsonConvert.SerializeObject(reservations, Formatting.Indented);


            File.WriteAllText(fileName, json);
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
            if (SelectedReservation == null)
            {
                return; // Nothing to remove
            }

            // Read the file
            string jsonContent = File.ReadAllText(fileName);
            List<Reservation>? reservations = JsonConvert.DeserializeObject<List<Reservation>>(jsonContent);

            // Finding and removing the chosen ONE
            var reservationToRemove = reservations.FirstOrDefault(r => r.Id == SelectedReservation.Id);
            if (reservationToRemove != null)
            {
                reservations.Remove(reservationToRemove);

                // Serializing the object again into JSON
                string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);

                // Saving the JSON file again
                File.WriteAllText(fileName, updatedJson);
            }

            //Update front-end upon  deleting
            if (SelectedReservation != null)
            {
                Reservations.Remove(SelectedReservation);
            }
        }
    }
}
