using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.DataObjects;

namespace IntermediateDatabaseProgrammingThree
{
    class ClubService : Wolf.DataObjects.ServiceBase<Club>
    {
        public List<Club> GetByClubID(int clubID)
        {
            string filterSQL = String.Format(" WHERE ClubID = {0} AND Deleted = 0", clubID);

            return Data.GetData<Club>(selectAll + filterSQL);
        }
    }
}
