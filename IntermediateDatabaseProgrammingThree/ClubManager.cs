using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IntermediateDatabaseProgrammingThree
{
    class ClubManager : Wolf.DataObjects.EntityBase
    {
        public ClubManager()
            :base()
        { }

        public ClubManager(IDataReader dr)
            :base(dr)
        { }

        public ClubManager(IDataReader dr, int position)
            :base(dr, position)
        { }

        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string Postcode { get; set; }
        public string FullName { get; set; }
    }
}
