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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace IntermediateDatabaseProgrammingThree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ClubManager> _cMList;
        private List<Equipment> _equipmentList;
        private List<Member> _memberList;

        internal void RefreshTabControl()
        {
            EquipmentService service1 = new EquipmentService();
            _equipmentList = service1.GetByClubID(((ClubManager)grdClub.SelectedItem).ID.Value);
            grdEquipment.ItemsSource = _equipmentList;

            MemberService service2 = new MemberService();
            _memberList = service2.GetByClubID(((ClubManager)grdClub.SelectedItem).ID.Value);
            grdMember.ItemsSource = _memberList;
        }

        public MainWindow()
        {
            InitializeComponent();
            // Windows Authentication is used here in Trusted_Connection
            Wolf.DataObjects.Connection.ConnectionString = @"Data Source=(localdb)\v11.0; Initial Catalog=FitnessClubChain; Trusted_Connection=true";
        }

        internal void Refresh()
        {
            ClubManagerService cMService = new ClubManagerService();

            // _cMList = cMService.GetAll(); This is a generic method and accesses the hybrid ClubManagerService Class via object cMService
            _cMList = cMService.GetAllActive();
            //ItemSource propertie of datagrid used below
            grdClub.ItemsSource = _cMList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ClubManagerService cMService = new ClubManagerService();

            _cMList = cMService.GetBySearch(txtSearch.Text);

            grdClub.ItemsSource = _cMList;
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAddClub_Click(object sender, RoutedEventArgs e)
        {
            // create the edit window in add mode passing the parent window

            EditClub editClub = new EditClub(this);
            editClub.Show();
        }

        private void btnUpdateClub_Click(object sender, RoutedEventArgs e)
        {
            if (grdClub.SelectedItem != null)
            {
                // Retrieve the row to edit

                int clubId = ((ClubManager)grdClub.SelectedItem).ID.Value;

                /* Create the edit window in edit mode passing the parent 
                   window & club id */

                EditClub editClub = new EditClub(this, clubId);
                editClub.Show();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = null;

            Refresh();
        }

        private void grdClub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdClub.SelectedItem != null)
            {
                // Record the selected item and populate the tabs

                RefreshTabControl();
            }
        }

        private void btnAddTab_Click(object sender, RoutedEventArgs e)
        {
            // Ensure that a club has been selected

            if (grdClub.SelectedItem != null)
            {
                TabItem ti = tabDetails.SelectedItem as TabItem;
                switch (ti.Name)
                {
                    case "EQUIPMENT":

                        /* Create the Equipment edit window in add mode passing the parent window & club selected */

                        EditEquipment editEquipment = new EditEquipment(this,
                        ((ClubManager)grdClub.SelectedItem).ID.Value);

                        editEquipment.Show();

                        break;

                    case "MEMBER":
                        /* Create the Member edit window in add mode passing the parent window & club selected */

                        EditMember editMember = new EditMember(this, ((ClubManager)grdClub.SelectedItem).ID.Value);

                        editMember.Show();

                        break;
                }
            }
        }

        private void btnUpdateTab_Click(object sender, RoutedEventArgs e)
        {
            if (grdClub.SelectedItem != null)
            {
                TabItem ti = tabDetails.SelectedItem as TabItem;
                switch (ti.Name)
                {
                    case "EQUIPMENT":

                        if (grdEquipment.SelectedItem != null)
                        {

                            /* Create the Equipment edit window in update mode passing the equipment selected & parent window */

                            EditEquipment editEquipment = new EditEquipment(((Equipment)grdEquipment.SelectedItem).ID.Value, this);

                            editEquipment.Show();
                        }

                        break;

                    case "MEMBER":

                        if (grdMember.SelectedItem != null)
                        {
                            /* Create the Member edit window in update mode passing the member selected & parent window */

                            EditMember editMember = new EditMember(((Member)grdMember.SelectedItem).ID.Value, this);

                            editMember.Show();
                        }

                        break;
                }


            }
        }
    }
}
    

  

