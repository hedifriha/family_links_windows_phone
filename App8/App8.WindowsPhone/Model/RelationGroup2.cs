using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App8.Model
{
    public class RelationGroup2 : ObservableCollection<SourceData2>
    {
        public RelationGroup2(IEnumerable<SourceData2> items)
            : base(items)
        {
        }

        public string RelationG { get; set; }
    }
}
