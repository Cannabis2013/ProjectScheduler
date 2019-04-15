using System;
using System.Windows.Forms;

namespace Templates
{
    interface IManagement
    {
        void AddTabPage(string title,Control control);
        void RemoveTabPage(int index);
        bool TabsActive();
        void UpdateCurrentTabTitle(string title);

        void _OnSaveClicked(object sender, EventArgs e);
        void _OnEditClicked(object sender, EventArgs e);
        void _OnCancelClicked(object sender, EventArgs e);
    }
}
