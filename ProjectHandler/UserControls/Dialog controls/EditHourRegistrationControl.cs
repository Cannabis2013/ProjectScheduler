using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Events;
using Projecthandler.Templates_and_interfaces;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.UserControls.Dialog_controls
{
    public partial class EditHourRegistrationControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;
        private readonly RegistrationObject rObject;

        public EditHourRegistrationControl(ProjectManager pManager, UserManager uManager, RegistrationObject rObject)
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
            var currentUserName = uManager.loggedIn().UserName();
            var activityModels = pManager.Activities(currentUserName).Select(item => item.ActivityId).ToArray();

            // ReSharper disable once CoVariantArrayConversion
            ActivityComboBoxSelector.Items.AddRange(activityModels);
        }

        public void InitializeDialogValues()
        {
            TitleBoxSelector.Text = rObject.RegistrationId;
            ActivityComboBoxSelector.SelectedItem = rObject.ParentActivityId;
            HourBoxSelector.Text = rObject.Hours.ToString();
            DescriptionBoxSelector.Text = rObject.Description;
        }

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;

        private void button1_Click(object sender, EventArgs e)
        {
            var currentUserId = uManager.loggedIn().UserName();

            var regTitle = TitleBoxSelector.Text;
            rObject.ParentActivityId = ActivityComboBoxSelector.Text;
            rObject.Description = DescriptionBoxSelector.Text;

            if (!int.TryParse(HourBoxSelector.Text, out int hours))
                throw new ArgumentException("Something went wrong with conversion from string to int.");

            rObject.Hours = hours;

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
