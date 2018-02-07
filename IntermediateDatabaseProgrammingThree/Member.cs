using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; 

namespace IntermediateDatabaseProgrammingThree
{
    class Member : Wolf.DataObjects.EntityBase
    {
        public Member()
            : base()
        { }

        public Member(IDataReader dr)
            : base(dr)
        { }

        public Member(IDataReader dr, int position)
            : base(dr, position)
        { }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string Postcode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ClubID { get; set; }
        public bool Deleted { get; set; }
    
    }
}
