using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectNameSpace
{
    public class ProjectItemModel : ListViewItem
    {
        public ProjectItemModel(ref Project p)
        {
            Text = p.ProjectID;
            SubItems.Add(p.StartDate.ToString());
            SubItems.Add(p.estimatedEndDate.ToString());
        }
    }
}
