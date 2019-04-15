using System;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Events;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddRegistrationDialogForm : Form, IDialogInterface<EventArgs>
    {
        private readonly IApplicationProgrammableInterface service;

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        public AddRegistrationDialogForm(IApplicationProgrammableInterface service)
        {
            this.service = service;
            InitializeComponent();
            initializeListControls();
        }

        public AddRegistrationDialogForm(IApplicationProgrammableInterface service, string activityId)
        {
            this.service = service;
            InitializeComponent();
            initializeListControls();

            ActivityComboBoxSelector.SelectedItem = activityId;
        }

        public void initializeListControls()
        {
            var currentUserName = service.CurrentUserLoggedIn().ModelIdentity;
            var activityModels = service.activityItemModels(currentUserName);

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
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentUserId = service.CurrentUserLoggedIn().ModelIdentity;
            string regTitle = TitleBoxSelector.Text, 
                activityId = ActivityComboBoxSelector.Text,
                description = DescriptionBoxSelector.Text,
                Hours = HourBoxSelector.Text;

            if (!int.TryParse(Hours, out int hours))
                throw new ArgumentException("Something went wrong with conversion from string to int.");

            var ParentActivity = service.Activity(activityId);

            var rObject = new HourRegistrationModel(regTitle,hours,currentUserId,description,ParentActivity);

            var sEvent = new SubmitEvent(rObject);
            
            Close();
        }

        private void ActivityComboBoxSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            HourBoxSelector.Enabled = ActivityComboBoxSelector.Text != "";
        }
    }
}
