using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mng;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Events;
using Projecthandler.Forms.Dialogs;
using ProjectRelated;
using UserDomain;

// ReSharper disable InconsistentNaming

namespace MainDomain
{
    public partial class ProjectView : Form, ICustomObserver
    {
        private readonly ListView ActivitiesView;
        private readonly IApplicationProgrammableInterface service;
        
        public event EventHandler<EventArgs> LogoutEvent;
        public event EventHandler<EventArgs> CloseRequest;
        public event EventHandler<EventArgs> HardCloseEvent;

        public ProjectView(IApplicationProgrammableInterface service)
        {
            InitializeComponent();
            this.service = service;
            var item = new ListViewItem();
            
            ActivitiesView = ActivityListView;

            service.SubScribe(this);

            var welcomingText = new StringBuilder("Welcome ");
            var userName = service.CurrentUserLoggedIn().ModelIdentity;
            welcomingText.Append(userName);

            WelcomeLabel.Text = welcomingText.ToString();

            UpdateView();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.Logut();
            LogoutEvent?.Invoke(this, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.UnSubScribeAll();
            HardCloseEvent?.Invoke(this,e);
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            service.Logut();
            service.UnSubScribeAll();
            LogoutEvent?.Invoke(this, e);
        }

        public void ManagementLink_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var mng = new Management(service);
            mng.ShowDialog(this);
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagementLink_Clicked(this, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (service.IsAdmin())
            {
                MessageBox.Show(@"Admin not allowed to register hour objects. Its beneath your paygrade. Sorry.");
                return;
            }

            if (ActivityListView.SelectedItems.Count < 1)
                return;
            var activityId = ActivityListView.SelectedItems[0].Text;
            var rDialog = new AddRegistrationDialogForm(service,activityId);

            rDialog.ShowDialog(this);
        }

        public void UpdateView()
        {
            
            var activityModels = (service.IsAdmin()) ? service.activityItemModels() : 
                service.activityItemModels(service.CurrentUserLoggedIn().ModelIdentity);
            ActivitiesView.Clear();
            ActivitiesView.View = View.Details;

            int columnWidth = 160;

            ActivitiesView.Columns.Add("Activity title", columnWidth, HorizontalAlignment.Left);
            ActivitiesView.Columns.Add("Start week", columnWidth, HorizontalAlignment.Left);
            ActivitiesView.Columns.Add("Estimated end week", columnWidth, HorizontalAlignment.Left);
            ActivitiesView.Columns.Add("Total registered hours", columnWidth, HorizontalAlignment.Left);
            ActivitiesView.Columns.Add("Assigned users", columnWidth, HorizontalAlignment.Left);
            ActivitiesView.Columns.Add("Project", columnWidth, HorizontalAlignment.Left);

            ActivitiesView.Items.AddRange(activityModels);

            RegistrationHourListView.Clear();
            RegistrationHourListView.View = View.Details;

            columnWidth = 120;

            RegistrationHourListView.Columns.Add("Registration id", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("User", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Original registration date", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Work hours registrated", columnWidth, HorizontalAlignment.Left);
            RegistrationHourListView.Columns.Add("Parent activity", columnWidth, HorizontalAlignment.Left);


            ListViewItem[] regObjects = service.IsAdmin() ?
                regObjects = service.HourRegistrationItemModels() :
                regObjects = service.HourRegistrationItemModels(service.CurrentUserLoggedIn().ModelIdentity);

            RegistrationHourListView.Items.AddRange(regObjects);
        }
    }
}