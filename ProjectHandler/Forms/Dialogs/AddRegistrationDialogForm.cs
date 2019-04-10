using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Events;
using Projecthandler.Templates;
using Projecthandler.Templates_and_interfaces;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddRegistrationDialogForm : Form, IDialogInterface<EventArgs>
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        public AddRegistrationDialogForm(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();
            initializeListControls();
        }

        public AddRegistrationDialogForm(ProjectManager pManager, UserManager uManager, string activityId)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();
            initializeListControls();

            ActivityComboBoxSelector.SelectedItem = activityId;
        }

        public void initializeListControls()
        {
            var currentUserName = uManager.loggedIn().UserName();
            var activityModels = pManager.Activities(currentUserName).Select(item => item.ActivityId).ToArray();

            // ReSharper disable once CoVariantArrayConversion
            ActivityComboBoxSelector.Items.AddRange(activityModels);
        }

        public void InitializeDialogValues()
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnCancelClicked?.Invoke(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentUserId = uManager.loggedIn().UserName();
            string regTitle = TitleBoxSelector.Text, 
                activityId = ActivityComboBoxSelector.Text,
                description = DescriptionBoxSelector.Text,
                Hours = HourBoxSelector.Text;

            if (!int.TryParse(Hours, out int hours))
                throw new ArgumentException("Something went wrong with conversion from string to int.");

            var rObject = new RegistrationObject(regTitle,hours,currentUserId,description,activityId);

            var sEvent = new SubmitEvent(rObject);

            OnSaveClicked?.Invoke(this,sEvent);

        }

        private void ActivityComboBoxSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            HourBoxSelector.Enabled = ActivityComboBoxSelector.Text != "";
        }
    }
}
