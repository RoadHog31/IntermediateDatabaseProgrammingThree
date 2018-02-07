using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IntermediateDatabaseProgrammingThree
{
    class Equipment : Wolf.DataObjects.EntityBase
    {
        public Equipment()
            : base()
        {}

        public Equipment(IDataReader dr)
            : base(dr)
        { }

        public Equipment(IDataReader dr, int position)
            : base(dr, position)
        { }

        public string Description { get; set; }      
        public DateTime DateMaintained { get; set; }
        public Decimal MaxWeight { get; set; }
        public int ClubID { get; set; }
        public bool Deleted { get; set; }
    
    }
}
