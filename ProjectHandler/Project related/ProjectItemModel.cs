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
        public ProjectItemModel(Project p)
        {
            this.p = p;
        }

        private Project p = null;
    }
}
