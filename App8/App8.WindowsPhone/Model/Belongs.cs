using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8
{
    class Belongs : ObservableCollection<RelationGroup>
    {
        public Belongs(IEnumerable<RelationGroup> items)
            : base(items)
        {
        }

        public string Belong { get; set; }
    }
}