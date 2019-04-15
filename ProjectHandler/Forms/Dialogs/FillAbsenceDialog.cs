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
using ProjectRelated;
using Templates;

namespace Projecthandler.Forms.Dialogs
{
    public partial class FillAbsenceDialog : Form, IDialogInterface<SubmitEvent>
    {
        public FillAbsenceDialog()
        {
            InitializeComponent();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var absenceActivity = new ActivityModel(
                TitleBoxSelector.Text,ReasonComboBoxSelector.Text,
                StartDateSelector.Value,
                EndDateSelector.Value);
            var sEvent = new SubmitEvent(absenceActivity);

            OnSaveClicked?.Invoke(this,sEvent);
        }

        public void InitializeControls()
        {
            throw new NotImplementedException();
        }

        public void InitializeDialogValues()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<SubmitEvent> OnSaveClicked;
        public event EventHandler<SubmitEvent> OnEditClicked;
        public event EventHandler<SubmitEvent> OnCancelClicked;
    }
}
