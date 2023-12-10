using blanc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
namespace blanc.ViewModels
{
    public class StaffViewModel : ViewModelBase
    {
        const string staffJson = "Staff.json";
        private ObservableCollection<Staff>? _staff;
        private Staff? _selectedStaffMember;

        public ObservableCollection<Staff>? Staff
        {
            get { return _staff; }
            set { _staff = value; OnPropertyChanged(nameof(Staff)); }
        }

        public string? fName { get; set; }
        public string? lName { get; set; }
        public float hWorked { get; set; }
        public float sWage { get; set; }
        public string? sPosition { get; set; }
        public StaffViewModel()
        {
            Staff = new ObservableCollection<Staff>();
            string rawJson = File.ReadAllText(staffJson);
            List<Staff>? staff = JsonConvert.DeserializeObject<List<Staff>>(rawJson);
            if (staff != null && staff.Any())
            {
                foreach (var item in staff)
                {
                    Staff.Add(item);
                }
                SelectedStaffMember = Staff.First();
            }
            if (Staff != null && Staff.Any())
            {
                Staff firstStaffMember = Staff.First();

                fName = firstStaffMember.FirstName;
                lName = firstStaffMember.LastName;
                hWorked = firstStaffMember.HoursWorked;
                sWage = firstStaffMember.Wage;
                sPosition = firstStaffMember.Position;
            }
            LoadDataFromFile("Staff.json");
        }
        public Staff? SelectedStaffMember
        {
            get { return _selectedStaffMember; }
            set { _selectedStaffMember = value; OnPropertyChanged(nameof(SelectedStaffMember)); }
        }
        private void LoadDataFromFile(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                List<Staff>? staffList = JsonConvert.DeserializeObject<List<Staff>>(jsonContent);

                Staff = new ObservableCollection<Staff>(staffList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NE STANAAA: {ex.Message}");
            }
        }

    }
}