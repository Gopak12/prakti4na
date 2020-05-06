using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace prakti4na
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

      
        private bool checkPass(string pass)
        {
            //min 6 chars, max 12 chars
            if (pass.Length < 6 || pass.Length > 20)
                return false;

            //No white space
            if (pass.Contains(" "))
                return false;

            //At least 1 upper case letter
            if (!pass.Any(char.IsUpper))
                return false;

            //At least 1 lower case letter
            if (!pass.Any(char.IsLower))
                return false;

            //At least 1 special char
            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            foreach (char c in specialCharactersArray)
            {
                if (pass.Contains(c))
                    return true;
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsValidEmail(textBox4.Text) == false)
            {
                MessageBox.Show("Bad email");
                return;
            }
            if (checkPass(textBox2.Text) == false)
            {
                MessageBox.Show("Weak pass");
                return;
            }
            if (db.countQuery($"SELECT COUNT(login)	FROM public.users where login = '{textBox1.Text}';") != 0)
            {
                MessageBox.Show("User with this login already exists");
                return;
            }
            db.insertQuery($"INSERT INTO public.users VALUES('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}');");
            MessageBox.Show("Success!");
            (this).Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
