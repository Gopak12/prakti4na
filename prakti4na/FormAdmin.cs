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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void refresh()
        {
            DataTable dt = db.selectQuery($"SELECT login, name, email FROM public.users;");
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (db.countQuery($"SELECT COUNT(login)	FROM public.deactivated_users where login = '{row.Cells[0].Value.ToString()}';") != 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
            dataGridView1.ClearSelection();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void buttonAct_Click(object sender, EventArgs e)
        {
            db.insertQuery($"DELETE FROM public.deactivated_users where login='{dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}'");
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.insertQuery($"INSERT INTO public.deactivated_users VALUES('{dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}')");
            refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new FormRegister();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string login = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var frm = new FormAeroflot(login);
                frm.Show();
            }
        }
    }
}
