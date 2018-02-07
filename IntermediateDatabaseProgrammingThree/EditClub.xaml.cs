using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;


namespace IntermediateDatabaseProgrammingThree
{
    /// <summary>
    /// Interaction logic for EditClub.xaml
    /// </summary>
    public partial class EditClub : Window
    {
        // reference to parent window

        MainWindow _parent;

        //Nullable club ID - Null on add, populated on update

        int? _editClubID;

        //Club details for editing

        Club _editClub;

        public EditClub(MainWindow parent)
        {
            // Constructor for add

            InitializeComponent();

            _editClubID = null;
            _parent = parent;
        }

        public EditClub(MainWindow parent, int clubID)
        {
            // Constructor for update

            InitializeComponent();

            _editClubID = clubID;
            _parent = parent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Position the EditClub window underneath the MainWindow

            this.Left = _parent.Left;
            this.Top = _parent.Top + _parent.Height;

            // Populate the manager comboBox with selectable items

            ManagerService managerService = new ManagerService();
            cbManager.ItemsSource = managerService.GetAll();

            // Decide if edit is add or update

            if (_editClubID == null)
            {
                PopulateAdd();
            }
            else
            {
                PopulateUpdate();
            }
        }

        private void PopulateAdd()
        {
            // Create an empty club object for addition

            Club club = new Club();
            _editClub = club;

            txtName.Text = _editClub.Name;
            txtAddressLine1.Text = _editClub.AddressLine1;
            txtAddressLine2.Text = _editClub.AddressLine2;
            txtAddressLine3.Text = _editClub.AddressLine3;
            txtAddressLine4.Text = _editClub.AddressLine4;
            txtAddressLine5.Text = _editClub.AddressLine5;
            txtPostcode.Text = _editClub.Postcode;

            /* On addition, default Opening & Closing times to
             * 09:00 & 17:00 respectively */

            txtOpeningTime.Text = "09:00";
            txtClosingTime.Text = "17:00";

            cbManager.SelectedValuePath = "ID";
            cbManager.DisplayMemberPath = "FullName";

            ckDeleted.IsEnabled = false;
            ckDeleted.IsChecked = false;
        }

        private void PopulateUpdate()
        {
            // Retrieve club object for update

            ClubService clubService = new ClubService();
            _editClub = clubService.GetByID(_editClubID.Value);

            txtName.Text = _editClub.Name;
            txtAddressLine1.Text = _editClub.AddressLine1;
            txtAddressLine2.Text = _editClub.AddressLine2;
            txtAddressLine3.Text = _editClub.AddressLine3;
            txtAddressLine4.Text = _editClub.AddressLine4;
            txtAddressLine5.Text = _editClub.AddressLine5;
            txtPostcode.Text = _editClub.Postcode;

            // Just display the HH:mm of the Opening & Closing datetime

            txtOpeningTime.Text = Convert.ToString(_editClub.OpeningTime.ToString("HH:mm"));
            txtClosingTime.Text = Convert.ToString(_editClub.ClosingTime.ToString("HH:mm"));

            cbManager.SelectedValuePath = "ID";
            cbManager.DisplayMemberPath = "FullName";
            cbManager.SelectedValue = _editClub.ManagerID;

            ckDeleted.IsChecked = _editClub.Deleted;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate

            if (Validate())
            {
                // Update the club

                ClubService clubService = new ClubService();

                _editClub.Name = txtName.Text;
                _editClub.AddressLine1 = txtAddressLine1.Text;
                _editClub.AddressLine2 = txtAddressLine2.Text;
                _editClub.AddressLine3 = txtAddressLine3.Text;
                _editClub.AddressLine4 = txtAddressLine4.Text;
                _editClub.AddressLine5 = txtAddressLine5.Text;
                _editClub.Postcode = txtPostcode.Text;

                // We're not interested in the data part of the datetime
                // So write the Opening & Closing datetime with a valid but arbituary
                // date of 01-01-1900
                // Could actually use the Time property here also.
                _editClub.OpeningTime = Convert.ToDateTime("01-01-1900 " + txtOpeningTime.Text);
                _editClub.ClosingTime = Convert.ToDateTime("01-01-1900 " + txtClosingTime.Text);

                if (cbManager.SelectedItem != null)
                {
                    _editClub.ManagerID = ((Manager)cbManager.SelectedItem).ID.Value;
                }

                _editClub.Deleted = Convert.ToBoolean(ckDeleted.IsChecked);

                clubService.Save(_editClub);

                // Update the parent DataGrid

                _parent.Refresh();

                if (_editClubID == null)
                {
                    // Reset the club ID after add/update to 'go again' 
                    // Save method populates the club ID with last ID that was added
                    // It needs to be null to add another

                    _editClub.ID = _editClubID;
                }
                else
                {
                    this.Close();
                }
            }
        }

        private bool Validate()
        {
            if ((string.IsNullOrEmpty(txtName.Text)) ||
                (string.IsNullOrWhiteSpace(txtName.Text)))
            {
                MessageBox.Show("Club name is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtAddressLine1.Text)) ||
                (string.IsNullOrWhiteSpace(txtAddressLine1.Text)))
            {
                MessageBox.Show("Club address line-1 is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtAddressLine2.Text)) ||
                (string.IsNullOrWhiteSpace(txtAddressLine2.Text)))
            {
                MessageBox.Show("Club address line-2 is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtPostcode.Text)) ||
                (string.IsNullOrWhiteSpace(txtPostcode.Text)))
            {
                MessageBox.Show("Club postcode is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtOpeningTime.Text)) ||
                (string.IsNullOrWhiteSpace(txtOpeningTime.Text)))
            {
                MessageBox.Show("Club opening time is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtClosingTime.Text)) ||
                (string.IsNullOrWhiteSpace(txtClosingTime.Text)))
            {
                MessageBox.Show("Club closing time is invalid.");
                return false;
            }

            if (cbManager.SelectedItem == null)
            {
                MessageBox.Show("Club manager not selected.");
                return false;
            }

            return true;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the edit window

            this.Close();
        }   

    }
}
