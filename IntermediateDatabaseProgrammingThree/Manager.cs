using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace IntermediateDatabaseProgrammingThree
{
    class Manager : Wolf.DataObjects.EntityBase
    {
        public Manager()
            : base()
        { }

        public Manager(IDataReader dr)
            : base(dr)
        { }

        public Manager(IDataReader dr, int position)
            : base(dr, position)
        { }

        public string FullName { get; set; }
       
    }
}
