using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Wolf.DataObjects; 

namespace IntermediateDatabaseProgrammingThree
{
    class MemberService : Wolf.DataObjects.ServiceBase<Member>
    {
        public List<Member> GetByClubID(int clubID)
        {
            string filterSQL = String.Format(" WHERE ClubID = {0} AND Deleted = 0", clubID);

            return Data.GetData<Member>(selectAll + filterSQL); 
        }

        
    }
}
