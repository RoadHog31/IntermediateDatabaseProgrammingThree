using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IntermediateDatabaseProgrammingThree
{
    class Club : Wolf.DataObjects.EntityBase
    {
        public Club()
            : base()
        { }

        public Club(IDataReader dr)
            : base(dr)
        { }

        public Club(IDataReader dr, int position)
            : base(dr, position)
        { }

        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string Postcode { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public int ManagerID { get; set; }
        public bool Deleted { get; set; }
    }
}
