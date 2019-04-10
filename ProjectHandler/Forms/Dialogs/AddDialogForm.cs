using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Templates;
using Projecthandler.Templates_and_interfaces;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.Forms.Dialogs
{
    public partial class AddDialogForm : Form, IDialogInterface<EventArgs>
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public AddDialogForm(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();
        }

        public void initializeListControls()
        {
            var currentUserName = uManager.loggedIn().UserName();
            var activityModels = pManager.Activities(currentUserName).Select(item => item.ActivityId).ToArray();

            ActivityComboSelector.Items.AddRange(activityModels);
        }

        public void InitializeDialogValues()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<EventArgs> OnSaveClicked;
        public event EventHandler<EventArgs> OnEditClicked;
        public event EventHandler<EventArgs> OnCancelClicked;
    }
}
