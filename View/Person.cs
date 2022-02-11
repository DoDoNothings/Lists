using Lists.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lists.View
{
 
    public partial class Person : Form
    {
        public OnePerson person { get; set; }
           
        public Person(OnePerson p)
        {
            InitializeComponent();
            this.person = p;
            tbFirstName.Text = person.FirstName;
            tbName.Text = person.Name;
            tbPatronymic.Text = person.Patronymic;
            tbPhone.Text = person.Phone;
            tbEmail.Text = person.Email;                       
        }

        //проверка на ввод e-mail
        public static bool isValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);

            if (isMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //нажатие кнопки ОК
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult result;
            this.DialogResult = DialogResult.OK;
            this.person.FirstName = tbFirstName.Text;
            this.person.Name = tbName.Text;
            this.person.Patronymic = tbPatronymic.Text;           
                if (!isValid(tbEmail.Text))
                {
                    result = MessageBox.Show("Некорректный ввод e-mail", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Visible = false;
                    Person form = new Person(person);
                    form.ShowDialog(this);
                }
                else
                {
                    this.person.Email = tbEmail.Text;
                }       
            this.person.Phone = tbPhone.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
