using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projecthandler.Templates_and_interfaces;
using ProjectRelated;
using VirtualUserDomain;

namespace Projecthandler.UserControls.Dialog_controls
{
    public partial class EditHourRegistrationControl : UserControl, IDialogInterface<EventArgs>
    {
        private readonly ProjectManager pManager;
        private readonly UserManager uManager;

        public EditHourRegistrationControl(ProjectManager pManager, UserManager uManager)
        {
            this.pManager = pManager;
            this.uManager = uManager;
            InitializeComponent();
        }

        public void initializeListControls()
        {
            throw new NotImplementedException();
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
