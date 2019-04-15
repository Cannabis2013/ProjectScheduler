using System;
using System.Linq;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.UserControls.Dialog_controls
{
    public partial class EditHourRegistrationControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly IApplicationProgrammableInterface service;
        private readonly HourRegistrationModel rObject;

        public EditHourRegistrationControl(IApplicationProgrammableInterface service, HourRegistrationModel rObject)
        {
            this.service = service;
            this.rObject = rObject;
            InitializeComponent();
            initializeListControls();
            InitializeDialogValues();
        }

        public void initializeListControls()
        {
            var currentUserName = service.CurrentUserLoggedIn().ModelIdentity;
            var activityModelsIdentities = 
                service.Activities(currentUserName).Select(item => item.ModelIdentity).ToArray();

            // ReSharper disable once CoVariantArrayConversion
            ActivityComboBoxSelector.Items.AddRange(activityModelsIdentities);
        }

        public void InitializeDialogValues()
        {
            TitleBoxSelector.Text = rObject.ModelIdentity;
            ActivityComboBoxSelector.SelectedItem = rObject.ParentModelIdentity();
            HourBoxSelector.Text = rObject.Hours.ToString();
            DescriptionBoxSelector.Text = rObject.ShortDescription;
        }

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        private void button1_Click(object sender, EventArgs e)
        {
            var currentUserId = service.CurrentUserLoggedIn().ModelIdentity;

            var oldActivityIdentity = rObject.ParentModelIdentity();
            var oldActivity = service.Activities()
                .FirstOrDefault(item => item.ModelIdentity == oldActivityIdentity);
            oldActivity.RemoveSubModel(rObject.ModelIdentity);

            var newRegIdentity = TitleBoxSelector.SelectedText;
            rObject.ModelIdentity = newRegIdentity;

            rObject.ShortDescription = DescriptionBoxSelector.Text;

            if (!int.TryParse(HourBoxSelector.Text, out int hours))
                throw new ArgumentException("Something went wrong with conversion from string to int.");

            rObject.Hours = hours;

            var newActivityIdentity = TitleBoxSelector.Text;
            var newActivity = service.Activities()
                .FirstOrDefault(item => item.ModelIdentity == newActivityIdentity);

            rObject.Parent = newActivity;
            newActivity.AddSubModel(rObject);

            OnEditClicked?.Invoke(this, new EventArgs());
        }

        private void DescriptionBoxSelector_TextChanged(object sender, EventArgs e)
        {
            HourBoxSelector.Enabled = ActivityComboBoxSelector.Text != "" || true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnCancelClicked?.Invoke(this,e);
        }
    }
}
