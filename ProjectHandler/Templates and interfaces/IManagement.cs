using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projecthandler.Templates
{
    interface IManagement
    {
        void addTabPage(string title,Control control);
        void removeTabPage(int index);
        bool tabsActive();
        void updateCurrentTabTitle(string title);
        void updateView();
    }
}
