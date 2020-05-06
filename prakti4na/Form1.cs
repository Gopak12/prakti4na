using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prakti4na
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var regForm = new FormRegister();
            regForm.Show();
        }

        public static string login = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (db.countQuery($"SELECT COUNT(login)	FROM public.deactivated_users where login = '{textBox1.Text}';") == 0 &&
                    db.countQuery($"SELECT COUNT(login)	FROM public.users where login = '{textBox1.Text}' and pass = '{textBox2.Text}';") != 0)
            {
                login = textBox1.Text;
                
                if (login == "admin")
                {
                    var form = new FormAdmin();
                    form.Show();
                    (this).Hide();
                    form.FormClosed += new FormClosedEventHandler(mnfrmclsd);
                }
                else
                {
                    var form = new MainForm();
                    form.Show();
                    (this).Hide();
                    form.FormClosed += new FormClosedEventHandler(mnfrmclsd);
                }
                
            }
            else
            {
                MessageBox.Show("Wrong login or password");
            }
        }

        private void mnfrmclsd(object sender, FormClosedEventArgs e)
        {
            (this).Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var lod = new FormLoading();
            lod.Show();
            lod.FormClosed += new FormClosedEventHandler(frmClsd);
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void frmClsd(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.CenterToScreen();
        }
    }
}
