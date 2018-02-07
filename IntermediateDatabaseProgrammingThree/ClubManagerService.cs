using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.DataObjects;

namespace IntermediateDatabaseProgrammingThree
{
    class ClubManagerService : Wolf.DataObjects.ServiceBase<ClubManager>
    {
        // Create SQL join in Constructor

        public ClubManagerService()
        {
            selectAll =
                "SELECT c.*, m.FullName " +
                "FROM Club c " +
                "JOIN Manager m ON c.ManagerID = m.ID "; 
        }

        public List<ClubManager> GetAllActive()
        {
            // Method to access the ClubManager class.
            // This Method creates SQL which eliminates deleted rows from the selection.

            string filterSQL = " WHERE Deleted = 0";

            return Data.GetData<ClubManager>(selectAll + filterSQL);
        }

        public List<ClubManager> GetBySearch(string searchName)
        {
            string searchSQL = " WHERE Deleted = 0 AND" + 
            String.Format(" Name LIKE '%{0}%'" , searchName);
        
            return Data.GetData<ClubManager>(selectAll + searchSQL);
        }

    }
}
