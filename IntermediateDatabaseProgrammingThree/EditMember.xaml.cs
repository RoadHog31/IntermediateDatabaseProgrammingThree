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
    /// Interaction logic for EditMember.xaml
    /// </summary>
    public partial class EditMember : Window
    {
        // Reference to parent window

        MainWindow _parent; 

        // Selected Club ID - Will always have a value

        int _editClubID; 

        // Nullable member ID - Null on add, populated on update

        int? _editMemberID; 

        // Member details for editing

        Member _editMember; 


        public EditMember()
        {
            InitializeComponent();
        }

        public EditMember(MainWindow parent, int clubID)
        {
            // Constructor for add

            InitializeComponent();

            _editClubID = clubID;
            _editMember = null;
            _parent = parent; 
        }

        public EditMember(int memberID, MainWindow parent)
        {
            // Constructor for update

            InitializeComponent();

            _editMemberID = memberID;
            _parent = parent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Position the EditWindow to the right of the MainWindow

            this.Left = _parent.Width;
            this.Top = _parent.Top; 

            // Decide if edit is add or update

            if (_editMemberID == null)
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
            // Create an empty member object for addition

            Member member = new Member(); 
            _editMember = member;

            txtFirstName.Text = _editMember.FirstName; 
            txtSurname.Text = _editMember.Surname; 
            txtAddressLine1.Text = _editMember.AddressLine1;
            txtAddressLine2.Text = _editMember.AddressLine2;
            txtAddressLine3.Text = _editMember.AddressLine3;
            txtAddressLine4.Text = _editMember.AddressLine4;
            txtAddressLine5.Text = _editMember.AddressLine5;
            txtPostcode.Text = _editMember.Postcode;

            dpDateOfBirth.SelectedDate = DateTime.Today;
            dpRegistrationDate.SelectedDate = DateTime.Today;

            ckDeleted.IsEnabled = false;
            ckDeleted.IsChecked = false;
        }

        private void PopulateUpdate()
        {
            // Retrieve member object for update

            MemberService memberService = new MemberService();
            _editMember = memberService.GetByID(_editMemberID.Value);

            txtFirstName.Text = _editMember.FirstName;
            txtSurname.Text = _editMember.Surname;
            txtAddressLine1.Text = _editMember.AddressLine1;
            txtAddressLine2.Text = _editMember.AddressLine2;
            txtAddressLine3.Text = _editMember.AddressLine3;
            txtAddressLine4.Text = _editMember.AddressLine4;
            txtAddressLine5.Text = _editMember.AddressLine5;
            txtPostcode.Text = _editMember.Postcode;

            dpDateOfBirth.SelectedDate = _editMember.DateOfBirth;
            dpRegistrationDate.SelectedDate = _editMember.RegistrationDate;

            ckDeleted.IsChecked = _editMember.Deleted; 
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate

            if (Validate())
            {

                // Update the member

                MemberService memberService = new MemberService();
                _editMember.FirstName = txtFirstName.Text;
                _editMember.Surname = txtSurname.Text;
                _editMember.AddressLine1 = txtAddressLine1.Text;
                _editMember.AddressLine2 = txtAddressLine2.Text;
                _editMember.AddressLine3 = txtAddressLine3.Text;
                _editMember.AddressLine4 = txtAddressLine4.Text;
                _editMember.AddressLine5 = txtAddressLine5.Text;
                _editMember.Postcode = txtPostcode.Text;
                _editMember.DateOfBirth = Convert.ToDateTime(dpDateOfBirth.SelectedDate);
                _editMember.RegistrationDate = Convert.ToDateTime(dpRegistrationDate.SelectedDate); 

                // If performing an add - populate the ClubID foreign key too

                if (_editMemberID == null)
                {
                    _editMember.ClubID = _editClubID; 
                }

                _editMember.Deleted = Convert.ToBoolean(ckDeleted.IsChecked);

                memberService.Save(_editMember);

                // Update the parent DataGrid

                _parent.RefreshTabControl(); 

                if (_editMemberID == null)
                {
                    // Reset the member ID after add/update to 'go again'
                    // Save method populates the member ID with last ID that was added 
                    // It needs to be null to add another

                    _editMember.ID = _editMemberID; 
                }
                else
                {
                    this.Close();
                }
            }
        }    

        private bool Validate()
        {
            if ((string.IsNullOrEmpty(txtFirstName.Text)) ||
                (string.IsNullOrWhiteSpace(txtFirstName.Text)))
            {
                MessageBox.Show("First Name is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtFirstName.Text)) ||
                (string.IsNullOrWhiteSpace(txtFirstName.Text)))
            {
                MessageBox.Show("SurName is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtFirstName.Text)) ||
                (string.IsNullOrWhiteSpace(txtFirstName.Text)))
            {
                MessageBox.Show("Postcode is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtFirstName.Text)) ||
                (string.IsNullOrWhiteSpace(txtFirstName.Text)))
            {
                MessageBox.Show("Date Of Birth is invalid.");
                return false;
            }

            if ((string.IsNullOrEmpty(txtFirstName.Text)) ||
                (string.IsNullOrWhiteSpace(txtFirstName.Text)))
            {
                MessageBox.Show("Registration Date is invalid.");
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

