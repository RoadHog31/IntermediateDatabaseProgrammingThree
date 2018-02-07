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

namespace IntermediateDatabaseProgrammingThree
{
    /// <summary>
    /// Interaction logic for EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {

        // Reference to parent window

        MainWindow _parent;
 
        // Selected Club ID - will always have a value

        int _editClubID; 

        // Nullable equipment ID - Null on add, populated on update

        int? _editEquipmentID; 

        // Equipment details for editing. _editEquipment is a variable. It can take different content depending on the input parameter.

        Equipment _editEquipment; 


        public EditEquipment()
        {
            InitializeComponent();
        }

        public EditEquipment(MainWindow parent, int clubID)
        {
            // Constructor for add

            InitializeComponent();

            _editClubID = clubID;
            _editEquipmentID = null;
            _parent = parent; 
        }

        public EditEquipment(int equipmentID, MainWindow parent)
        {
            // Constructor for update

            InitializeComponent();

            _editEquipmentID = equipmentID;
            _parent = parent; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Position the EditWindow to the right of the MainWindow

            this.Left = _parent.Left + _parent.Width;
            this.Top = _parent.Top; 

            // Decide if edit is add or update

            if (_editEquipmentID == null)
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
            // Create an empty equipment object for addition

            Equipment equipment = new Equipment();
            _editEquipment = equipment;

            txtDescription.Text = _editEquipment.Description;
            dpDateMaintained.SelectedDate = DateTime.Today;
            txtMaxWeight.Text = Convert.ToString(_editEquipment.MaxWeight);
            ckDeleted.IsEnabled = false;
            ckDeleted.IsChecked = false; 
        }

        private void PopulateUpdate()
        {
            // Retrieve equipment object for update

            EquipmentService equipmentService = new EquipmentService();
            _editEquipment = equipmentService.GetByID(_editEquipmentID.Value);

            txtDescription.Text = _editEquipment.Description;
            dpDateMaintained.SelectedDate = _editEquipment.DateMaintained;
            txtMaxWeight.Text = Convert.ToString(_editEquipment.MaxWeight);
            ckDeleted.IsChecked = _editEquipment.Deleted; 

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate 

            if (Validate())
            {

                // Update the equipment

                EquipmentService equipmentService = new EquipmentService();
                _editEquipment.Description = txtDescription.Text;
                _editEquipment.DateMaintained = Convert.ToDateTime(dpDateMaintained.SelectedDate);
                _editEquipment.MaxWeight = Convert.ToDecimal(txtMaxWeight.Text);

            // If performing an add - populate the ClubID foreign key too

            if (_editEquipmentID == null)
            {
                _editEquipment.ClubID = _editClubID; 
            }

            _editEquipment.Deleted = Convert.ToBoolean(ckDeleted.IsChecked);

            equipmentService.Save(_editEquipment); 

            // Update the parent DataGrid

            _parent.RefreshTabControl();

            if (_editEquipmentID == null)
            {

                // Reset the equipment ID after add/update to 'go again'
                // Save method populates the equipment ID with last ID that was added
                // It needs to be null to add another

                _editEquipment.ID = _editEquipmentID;

            }
            else
            {
                this.Close(); 
            }
            }
        }

        private bool Validate()
        {

            if ((string.IsNullOrEmpty(txtDescription.Text)) ||
                (string.IsNullOrWhiteSpace(txtDescription.Text)))
            {
                MessageBox.Show("Description is invalid.");
                return false; 
            }

            if (dpDateMaintained.SelectedDate == null)
            {
                MessageBox.Show("Date Maintained is invalid.");
                return false; 
            }
            
            if ((string.IsNullOrEmpty(txtMaxWeight.Text)) ||
            (string.IsNullOrWhiteSpace(txtMaxWeight.Text)))
            {
                MessageBox.Show("Maximum Weight is invalid.");
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
