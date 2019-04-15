using System;
using System.Windows.Forms;
using Projecthandler.Abstract_classes_and_interfaces;
using Projecthandler.Custom_events;

namespace Projecthandler.Class_forms
{
    public partial class LoginView : Form
    {
        private IApplicationProgrammableInterface service;

        public event EventHandler<EventArgs> OnAccessGranted;
        public event EventHandler<EventArgs> onFormClose;

        public LoginView(IApplicationProgrammableInterface service)
        {
            InitializeComponent();
            this.service = service;
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

            var uName = textBox1.Text;
            var pass = textBox2.Text;

            if (service.Login(uName, pass))
                OnAccessGranted?.Invoke(this, new EventArgs());

            warningLabel.Text = "Wrong credentials entered.";
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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