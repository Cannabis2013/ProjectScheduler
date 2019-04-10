using System;
using System.Windows.Forms;
using Projecthandler.Custom_events;

namespace Projecthandler.Class_forms
{
    public partial class LoginView : Form
    {
        public LoginView()
        {
            InitializeComponent();
        }


        // For testing purposes
        public void enterCredentialsManual(string uName, string pass)
        {
            OnSubmitClicked?.Invoke(this, new CredentialArguments(uName, pass));
        }

        public void setWarningText(string msg)
        {
            warningLabel.Text = msg;
        }

        public void resetForms()
        {
            textBox1.Clear();
            textBox2.Clear();
            warningLabel.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if user credentials is valid..

            var uName = textBox1.Text;
            var pass = textBox2.Text;

            OnSubmitClicked?.Invoke(this, new CredentialArguments(uName, pass));
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public event EventHandler<CredentialArguments> OnSubmitClicked;
        public event EventHandler<EventArgs> onFormClose;

        private void LoginView_FormClosed(object sender, FormClosedEventArgs e)
        {
            onFormClose?.Invoke(this, new EventArgs());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter || e.KeyChar == (char) Keys.Return)
                button1_Click(this, e);
        }
    }
}