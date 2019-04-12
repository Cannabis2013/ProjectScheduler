using System;
using System.Windows.Forms;

namespace Templates
{
    interface IManagement
    {
        void addTabPage(string title,Control control);
        void removeTabPage(int index);
        bool tabsActive();
        void updateCurrentTabTitle(string title);
        void updateView();

        void _OnSaveClicked(object sender, EventArgs e);
        void _OnEditClicked(object sender, EventArgs e);
        void _OnCancelClicked(object sender, EventArgs e);
    }
}
