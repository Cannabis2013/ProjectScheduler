using System;
using System.Linq;
using System.Windows.Forms;
using ProjectRelated;
using Templates;
using UserDomain;

namespace Projecthandler.UserControls.Dialog_controls
{
    public partial class EditHourRegistrationControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;
        private readonly HourRegistrationModel rObject;

        public EditHourRegistrationControl(ProjectManager pManager, UserManager uManager, HourRegistrationModel rObject)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            this.rObject = rObject;
            InitializeComponent();
            initializeListControls();
            InitializeDialogValues();
        }

        public void initializeListControls()
        {
            var currentUserName = uManager.loggedIn().ModelIdentity;
            var activityModels = pManager.ActivityModels(currentUserName).Select(item => item.ModelIdentity).ToArray();

            // ReSharper disable once CoVariantArrayConversion
            ActivityComboBoxSelector.Items.AddRange(activityModels);
        }

        public void InitializeDialogValues()
        {
            TitleBoxSelector.Text = rObject.ModelIdentity;
            ActivityComboBoxSelector.SelectedItem = rObject.ParentModelIdentity();
            HourBoxSelector.Text = rObject.Hours.ToString();
            DescriptionBoxSelector.Text = rObject.Description;
        }

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        private void button1_Click(object sender, EventArgs e)
        {
            var currentUserId = uManager.loggedIn().ModelIdentity;

            var oldActivityIdentity = rObject.ParentModelIdentity();
            var oldActivity = pManager.ActivityModels()
                .FirstOrDefault(item => item.ModelIdentity == oldActivityIdentity);
            oldActivity.RemoveSubModel(rObject.ModelIdentity);

            var newRegIdentity = TitleBoxSelector.SelectedText;
            rObject.ModelIdentity = newRegIdentity;

            rObject.Description = DescriptionBoxSelector.Text;

            if (!int.TryParse(HourBoxSelector.Text, out int hours))
                throw new ArgumentException("Something went wrong with conversion from string to int.");

            rObject.Hours = hours;

            var newActivityIdentity = TitleBoxSelector.Text;
            var newActivity = pManager.ActivityModels()
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
