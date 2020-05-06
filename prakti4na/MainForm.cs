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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void refresh()
        {
            DataTable dt = db.selectQuery($"SELECT id, dest, days, flight_num, capacity, plane_name FROM public.aeroflot where user_login='{Form1.login}';");
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            dataGridView1.Columns[0].Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private List<int> strToDays(string s)
        {
            string[] arr = s.Split(' ');
            List<int> days = new List<int>();
            foreach (var elem in arr)
            {
                try
                {
                    int val = Int32.Parse(elem);
                    days.Add(val);
                }
                catch (Exception ex)
                {
                    throw new Exception("bad values");
                }
            }
            if (days.Count != 7)
            {
                throw new Exception("bad values");
            }
            return days;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string dest = textBox1.Text;
            List<int> days = new List<int>();
            try
            {
                days = strToDays(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad days");
                return;
            }
            string flight_num = textBox3.Text;
            int cap = 0;
            try
            {
                cap = Int32.Parse(textBox4.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad capacity");
                return;
            }
            string plane_name = textBox5.Text;
            db.insertQuery($"INSERT INTO public.aeroflot( dest, days, flight_num, capacity, plane_name, user_login) VALUES('{dest}', '{textBox2.Text}', '{flight_num}', '{cap}', '{plane_name}', '{Form1.login}'); ");
            refresh();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            db.insertQuery($"DELETE FROM public.aeroflot where id='{dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}'");
            refresh();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (e.ColumnIndex == 2)
            {
                List<int> days = new List<int>();
                try
                {
                    days = strToDays(value);
                }
                catch (Exception ex)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                    return;
                }
            }
            if (e.ColumnIndex == 4)
            {
                try
                {
                    int cap = Int32.Parse(value);
                }
                catch (Exception ex)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString();
                    return;
                }
            }
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            db.insertQuery($"UPDATE public.aeroflot SET dest ='{row.Cells[1].Value.ToString()}', days ='{row.Cells[2].Value.ToString()}', flight_num ='{row.Cells[3].Value.ToString()}', capacity ='{row.Cells[4].Value.ToString()}', plane_name ='{row.Cells[5].Value.ToString()}' WHERE id='{row.Cells[0].Value.ToString()}'; ");

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (this).Close();
        }
    }
}
