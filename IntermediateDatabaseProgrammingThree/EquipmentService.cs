using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.DataObjects;

namespace IntermediateDatabaseProgrammingThree
{
    class EquipmentService : Wolf.DataObjects.ServiceBase<Equipment>
    {
        public List<Equipment> GetByClubID(int clubID)
        {
            string filterSQL = String.Format(" WHERE ClubID = {0} AND Deleted = 0", clubID); 

            return Data.GetData<Equipment>(selectAll + filterSQL); 
        }
    }
}
