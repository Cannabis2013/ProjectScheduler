using System.Linq;
using System.Windows.Forms;
using VirtualUserDomain;
using ProjectRelated;

namespace Projecthandler.Forms.Dialogs
{
    public partial class HourManager : Form
    {
        private UserManager UManager;
        private ProjectManager PManager;
        private Activity currentSelectedActivity = null;

        public HourManager(UserManager uManager, ProjectManager pManager)
        {
            InitializeComponent();
            InitializeTimeObjectView();

            this.UManager = uManager;
        }

        public UserManager uManager
        {
            get => UManager;
            set => UManager = value;
        }

        public ProjectManager pManager
        {
            get => PManager;
            set => PManager = value;
        }

        private void InitializeTimeObjectView()
        {
            var cUserUserName = uManager.currentlyLoggedIn().UserName();
            var TimeObjectItemModels = pManager.ActivityTimeObjectModels(cUserUserName);
            TimeObjectView.Items.AddRange(TimeObjectItemModels);
        }

        private void TimeObjectView_ItemActivate(object sender, System.EventArgs e)
        {

        }

        private void listView1_ItemActivate(object sender, System.EventArgs e)
        {
            var model = ActivityListView.SelectedItems[0];
            currentSelectedActivity = pManager.Activity(model.Text);
            var timeObjectItemModels = currentSelectedActivity.TimeObjectModels(uManager.currentlyLoggedIn().UserName());

            TimeObjectView.Clear();

            TimeObjectView.Items.AddRange(timeObjectItemModels);
        }
    }
}
