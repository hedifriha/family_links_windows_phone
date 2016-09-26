using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8
{
    public class RelationGroup : ObservableCollection<SourceData>
    {
        public RelationGroup(IEnumerable<SourceData> items)
            : base(items)
        {
        }

        public string RelationG { get; set; }
    }
}