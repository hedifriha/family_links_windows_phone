using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8.Model
{
    class Belongs2 : ObservableCollection<RelationGroup2>
    {
        public Belongs2(IEnumerable<RelationGroup2> items)
            : base(items)
        {
        }

        public string Belong { get; set; }
    }
}