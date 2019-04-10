using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Templates;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialog_controls
{
    public partial class HourManagement : UserControl, IManagement
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public HourManagement(UserManager uManager, ProjectManager pManager)
        {
            this.uManager = uManager;
            this.pManager = pManager;
            InitializeComponent();
        }

        public void updateView()
        {
            HourListView.View = View.Details;

            HourListView.Columns.Add("User", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Original registration date", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Last edited date", 60, HorizontalAlignment.Left);
            HourListView.Columns.Add("Work hours registrated", 60, HorizontalAlignment.Left);

            var regObjects = pManager.HourRegistrationObjects().Select(item => item.ItemModel()).ToArray();

            HourListView.Items.AddRange(regObjects);
        }

        public void addTabPage(string title, Control control)
        {
            if (tabsActive())
            {
                TabView.SelectedIndex = 1;
                MessageBox.Show(@"You have to finish your current operation.");
                return;
            }

            var tPage = new TabPage(title);

            const AnchorStyles layoutAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            control.Size = tPage.Size;
            tPage.Controls.Add(control);

            control.Anchor = layoutAnchor;

            TabView.TabPages.Add(tPage);
            TabView.SelectedTab = tPage;
        }

        public void removeTabPage(int index)
        {
            TabView.TabPages.RemoveAt(index);
        }

        public bool tabsActive()
        {
            throw new NotImplementedException();
        }

        public void updateCurrentTabTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
