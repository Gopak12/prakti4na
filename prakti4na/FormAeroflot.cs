﻿using System;
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
    public partial class FormAeroflot : Form
    {
        public FormAeroflot(string login)
        {
            InitializeComponent();
            DataTable dt = db.selectQuery($"SELECT id, dest, days, flight_num, capacity, plane_name FROM public.aeroflot where user_login='{login}';");
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            dataGridView1.Columns[0].Visible = false;
        }

        private void FormAeroflot_Load(object sender, EventArgs e)
        {

        }
    }
}
