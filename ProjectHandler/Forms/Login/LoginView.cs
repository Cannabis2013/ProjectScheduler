using System;
using System.Windows.Forms;
using Projecthandler.Events;

namespace Projecthandler.Forms.Login
{
    public partial class LoginView : Form
    {
        public LoginView()
        {
            InitializeComponent();
        }


        // For testing purposes
        public void EnterCredentialsManual(string uName, string pass)
        {
            OnSubmitClicked?.Invoke(this, new MyEventArgs(uName, pass));
        }

        public void SetWarningText(string msg)
        {
            warningLabel.Text = msg;
        }

        public void ResetForms()
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

            OnSubmitClicked?.Invoke(this, new MyEventArgs(uName, pass));
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public event EventHandler<MyEventArgs> OnSubmitClicked;
        public event EventHandler<EventArgs> OnFormClose;

        private void LoginView_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnFormClose?.Invoke(this, new EventArgs());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter || e.KeyChar == (char) Keys.Return)
                button1_Click(this, e);
        }
    }
}