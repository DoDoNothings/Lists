using Lists.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lists.View
{
    public partial class Worksheet : Form
    {
        Data.Entities.DBContext db;
        public Worksheet()
        {
            InitializeComponent();
            this.Text = "Пользователи";
            db = Data.Entities.DBContext.getInstance();
            db.onError += (msg) =>
            {
                MessageBox.Show(msg);
            };
        }
        private void LoadPeopleToListBox()
        {
            listPeople.Items.Clear();
            listPeople.Items.AddRange(db.People.ToArray());
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            OnePerson p = new OnePerson { Id = Guid.NewGuid() };
            var f = new Person(p);
            f.Text = "Анкета";
            if (f.ShowDialog() == DialogResult.OK)
            {
                db.People.Add(f.person);
                listPeople.Items.Add(f.person);
                this.Visible = true;
            }
            this.Visible = true;
        }

        string fileName;

        //сохранение в файл
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            var f = new SaveFileDialog();
            f.Filter = "db Files (*.xml)|*.xml|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            f.FileName = fileName;
            if (f.ShowDialog() == DialogResult.OK)
            {
                fileName = f.FileName;
                db.Save(fileName);
            }
        }

        //выгрузка с файла
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            f.Filter = "db Files (*.xml)|*.xml|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            f.FileName = fileName;
            if (f.ShowDialog() == DialogResult.OK)
            {
                fileName = f.FileName;
                db.Load(fileName);
                LoadPeopleToListBox();
            }
        }

        //удалить пользователя
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listPeople.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Вы точно хотите удалить пользователя навсегда?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    listPeople.Items.Remove(listPeople.SelectedItem);
                }
            }
        }

        //редактирование пользователя
        private void btnChange_Click(object sender, EventArgs e)
        {
            if (listPeople.SelectedItem != null)
            {
                string temp = listPeople.SelectedItem.ToString();
                string[] personaly = temp.Split(" ");
                DialogResult result = MessageBox.Show("Вы точно хотите изменить данные о пользователе?", "Редактирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Visible = false;
                    OnePerson p = new OnePerson { Id = Guid.NewGuid() };
                    p.FirstName = personaly[1];
                    p.Name = personaly[2];
                    p.Patronymic = personaly[3];
                    p.Phone = personaly[5];
                    p.Email = personaly[4];
                    var f = new Person(p);
                    f.Text = "Анкета";
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        listPeople.Items.Remove(listPeople.SelectedItem);
                        listPeople.Items.Add(f.person);
                        this.Visible = true;
                    }
                    this.Visible = true;
                }
            }
        }

    
    }
}
