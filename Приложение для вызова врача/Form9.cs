using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Приложение_для_вызова_врача
{
    public partial class Form9 : Form
    {
        string connectionString = "server=127.0.0.1;user=root;password=12345;database=call_doctor";

        string fio = " ";
        string login;
        int idDoctor_2;

        int menuIndex = 0;

        DataTable dataTable;

        public Form9(string login)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.login = login;
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            поискУслогуToolStripMenuItem.Click += поискУслогуToolStripMenuItem_Click;
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void поискВрачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string servicename = guna2TextBox1.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < servicename.Length; i++)
            {
                if (servicename[i] >= 'A' && servicename[i] <= 'Z' || servicename[i] >= 'a' && servicename[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox1.Text = null;
                }

                if (servicename[i] >= '0' && servicename[i] <= '9')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox1.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox1.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox1.Text = null;
                }
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            string servicedescr = guna2TextBox2.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < servicedescr.Length; i++)
            {
                if (servicedescr[i] >= 'A' && servicedescr[i] <= 'Z' || servicedescr[i] >= 'a' && servicedescr[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox2.Text = null;
                }

                if (servicedescr[i] >= '0' && servicedescr[i] <= '9')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox2.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox2.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox2.Text = null;
                }
            }
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            string serviceprice = guna2TextBox3.Text;
            string specialCharacters = @"[!@#$%^&*()_\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < serviceprice.Length; i++)
            {
                if (serviceprice[i] >= 'A' && serviceprice[i] <= 'Z' || serviceprice[i] >= 'a' && serviceprice[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox3.Text = null;
                }

                if (serviceprice[i] >= 'А' && serviceprice[i] <= 'Я' || serviceprice[i] >= 'а' && serviceprice[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox3.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox3.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox3.Text = null;
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(guna2TextBox1.Text == "")
            {
                MessageBox.Show("Введите название услуги");
            }
            else if(guna2TextBox2.Text == "")
            {
                MessageBox.Show("Введите описание услуги");
            }
            else if (guna2TextBox3.Text == "")
            {
                MessageBox.Show("Введите стоимость услуги");
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string queryId_3 = "SELECT id_врача FROM Врачи WHERE логин = @login";
                MySqlCommand command5 = new MySqlCommand(queryId_3, connection);
                command5.Parameters.AddWithValue("@login", login);
                object resultId_3 = command5.ExecuteScalar();

                int idDoctor_3 = Convert.ToInt32(resultId_3);

                string queryServiceName = "SELECT название_уcлуги FROM Мед_услуги WHERE название_уcлуги = @название_уcлуги and id_врача = @idDoctor_3";
                MySqlCommand command4 = new MySqlCommand(queryServiceName, connection);
                command4.Parameters.AddWithValue("@название_уcлуги", guna2TextBox1.Text);
                command4.Parameters.AddWithValue("@idDoctor_3", idDoctor_3);

                object resultServiceName = command4.ExecuteScalar();

                if (resultServiceName != null)
                {
                    MessageBox.Show("Ошибка, такая услуга уже существует");
                    guna2TextBox1.Text = null;
                }

                connection.Close();
            }

            if (!string.IsNullOrEmpty(guna2TextBox1.Text) && !string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string queryId = "SELECT id_врача FROM Врачи WHERE логин = @login";
                    MySqlCommand command3 = new MySqlCommand(queryId, connection);
                    command3.Parameters.AddWithValue("@login", login);
                    object resultId = command3.ExecuteScalar();

                    string query = "SELECT id_услуги FROM Мед_услуги ORDER BY id_услуги DESC LIMIT 1";
                    MySqlCommand command1 = new MySqlCommand(query, connection);
                    object result = command1.ExecuteScalar();


                    try
                    {
                        if (result != DBNull.Value)
                        {
                            int id = Convert.ToInt32(result);
                            id += 1;

                            int idDoctor = Convert.ToInt32(resultId);

                            string commandText = "INSERT INTO Мед_услуги (id_услуги, id_врача, название_уcлуги, описание_услуги, стоимость) VALUES (@id_услуги, @id_врача, @название_уcлуги, @описание_услуги, @стоимость)";
                            MySqlCommand command2 = new MySqlCommand(commandText, connection);
                            command2.Parameters.AddWithValue("@id_услуги", id);
                            command2.Parameters.AddWithValue("@id_врача", idDoctor);
                            command2.Parameters.AddWithValue("@название_уcлуги", guna2TextBox1.Text);
                            command2.Parameters.AddWithValue("@описание_услуги", guna2TextBox2.Text);
                            command2.Parameters.AddWithValue("@стоимость", guna2TextBox3.Text);
                            command2.ExecuteNonQuery();
                            MessageBox.Show("Данные успешно добавлены");

                            guna2TextBox1.Text = null;
                            guna2TextBox2.Text = null;
                            guna2TextBox3.Text = null;
                        }
                        else
                        {
                            throw new Exception("Ошибка");
                        }
                    }
                    catch (Exception checkDate)
                    {
                        MessageBox.Show(checkDate.Message);
                    }
                }
            }
        }

        private void поискУслогуToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dwqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2HtmlLabel2.Visible = true;
            guna2TextBox1.Visible = true;
            guna2TextBox2.Visible = true;
            guna2TextBox3.Visible = true;
            guna2Button1.Visible = true;
            guna2DataGridView1.Visible = false;
        }

        private void qwdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2HtmlLabel3.Visible = true;
            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2TextBox3.Visible = false;
            guna2Button1.Visible = false;
            guna2DataGridView1.Visible = false;

            guna2TextBox4.Visible = true;
            guna2Button2.Visible = true;
        }

        private void просмотрУслугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guna2HtmlLabel1.Visible = true;
            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2TextBox3.Visible = false;
            guna2Button1.Visible = false;

            guna2DataGridView1.Visible = true;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string queryId_2 = "SELECT id_врача FROM Врачи WHERE логин = @login";
                using (MySqlCommand command3 = new MySqlCommand(queryId_2, connection))
                {
                    command3.Parameters.AddWithValue("@login", login);
                    object resultId_2 = command3.ExecuteScalar();

                    if (resultId_2 != null)
                    {
                        idDoctor_2 = Convert.ToInt32(resultId_2);

                        string query = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги WHERE id_врача = @idDoctor_2";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@idDoctor_2", idDoctor_2);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                guna2DataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нету");
                    }
                }
            }
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

            if (menuIndex == 2 && selectedRow != null)
            {
                fio = selectedRow.Cells[0].Value.ToString();
                guna2ComboBox1.Visible = true;
                guna2Button8.Visible = true;
            }
        }

        private void заявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuIndex = 2;

            guna2DataGridView1.AllowUserToResizeRows = false;
            guna2DataGridView1.AllowUserToResizeColumns = false;

            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "SELECT Клиенты.фио, Мед_услуги.название_уcлуги, Заявки.выбранное_время, Заявки.дата_оформления_заявки, Заявки.статус_заявки FROM Клиенты JOIN Заявки ON Клиенты.id_клиента = Заявки.id_клиента JOIN Мед_услуги ON Заявки.id_услуги = Мед_услуги.id_услуги JOIN Врачи ON Заявки.id_врача = Врачи.id_врача WHERE Врачи.логин = @login;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@login", login);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            guna2DataGridView1.DataSource = dataTable;

            if (menuIndex == 2)
            {
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            else
            {
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                guna2DataGridView1.Columns[0].Width = 80;
                guna2DataGridView1.Columns[1].Width = 80;
                guna2DataGridView1.Columns[2].Width = 105;
                guna2DataGridView1.Columns[3].Width = 95;
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            string servicename = guna2TextBox4.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < servicename.Length; i++)
            {
                if (servicename[i] >= 'A' && servicename[i] <= 'Z' || servicename[i] >= 'a' && servicename[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox4.Text = null;
                }

                if (servicename[i] >= '0' && servicename[i] <= '9')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox4.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox4.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox4.Text = null;
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string nameservice = guna2TextBox4.Text;

            if (guna2TextBox4.Text == "")
            {
                MessageBox.Show("Введите название услуги");
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string queryCheck = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги WHERE название_уcлуги = @nameservice";
                    MySqlCommand command = new MySqlCommand(queryCheck, connection);
                    command.Parameters.AddWithValue("@nameservice", nameservice);

                    object resultCheck = command.ExecuteScalar();

                    if (resultCheck != null)
                    {
                        DialogResult result = MessageBox.Show
                        ("Вы точно хотите удалить эту услугу?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            using (MySqlConnection connection2 = new MySqlConnection(connectionString))
                            {
                                connection2.Open();

                                string queryDelete = "DELETE FROM Мед_услуги WHERE название_уcлуги = @nameservice";
                                MySqlCommand command2 = new MySqlCommand(queryDelete, connection2);
                                command2.Parameters.AddWithValue("@nameservice", nameservice);
                                int rowsAffected = command2.ExecuteNonQuery();
                                guna2TextBox4.Text = null;
                                MessageBox.Show("Услуга удалена успешно");
                                connection2.Close();
                            }
                        }
                    }
                    else
                    {
                        guna2TextBox4.Text = null;
                        MessageBox.Show("Ошибка, такой услуги нету");
                    }

                    connection.Close();
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            guna2HtmlLabel1.Visible = false;
            guna2HtmlLabel2.Visible = false;
            guna2HtmlLabel3.Visible = false;
            guna2HtmlLabel4.Visible = false;
            guna2HtmlLabel5.Visible = false;

            guna2Button1.Visible = false;
            guna2Button2.Visible = false;
            guna2Button3.Visible = false;
            guna2Button5.Visible = false;
            guna2Button8.Visible = false;


            guna2DataGridView1.Visible = false;

            guna2ComboBox1.Visible = false;

            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2TextBox3.Visible = false;
            guna2TextBox4.Visible = false;
            guna2TextBox5.Visible = false;
            guna2TextBox6.Visible = false;


            if (e.ClickedItem == menuStrip1.Items[1])
            {
                guna2HtmlLabel4.Visible = true;
                guna2DataGridView1.Visible = true;
            }

            else if (e.ClickedItem == menuStrip1.Items[2])
            {
                guna2HtmlLabel5.Visible = true;
                guna2TextBox5.Visible = true;
                guna2TextBox6.Visible = true;
                guna2Button5.Visible = true;
                guna2TextBox5.Text = login;
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void выToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text == "")
            {
                MessageBox.Show("Введите пароль");
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string queryCheck = "SELECT логин, пароль FROM Врачи WHERE логин = @Login AND пароль = @Password";
                    MySqlCommand command = new MySqlCommand(queryCheck, connection);
                    command.Parameters.AddWithValue("@Login", guna2TextBox5.Text);
                    command.Parameters.AddWithValue("@Password", guna2TextBox6.Text);

                    object resultCheck = command.ExecuteScalar();

                    if (resultCheck != null)
                    {
                        DialogResult result = MessageBox.Show
                        ("Вы точно хотите удалить профиль?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            using (MySqlConnection connection2 = new MySqlConnection(connectionString))
                            {
                                connection2.Open();

                                string queryDelete = "DELETE FROM Врачи WHERE логин = @Login AND пароль = @Password";
                                MySqlCommand command2 = new MySqlCommand(queryDelete, connection2);
                                command2.Parameters.AddWithValue("@Login", guna2TextBox5.Text);
                                command2.Parameters.AddWithValue("@Password", guna2TextBox6.Text);
                                int rowsAffected = command2.ExecuteNonQuery();
                                Form1 form1 = new Form1();
                                form1.Show();
                                this.Hide();

                                connection2.Close();
                            }
                        }
                    }
                    else
                    {
                        guna2TextBox6.Text = null;
                        MessageBox.Show("Ошибка, неправильный пароль");
                    }

                    connection.Close();
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(guna2ComboBox1.Text != " ")
            {
                guna2Button3.Visible = true;
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2ComboBox1.Visible = false;
            guna2Button3.Visible = false;
            guna2Button8.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Заявки " +
                                     "JOIN Клиенты ON Клиенты.id_клиента = Заявки.id_клиента " +
                                     "SET Заявки.статус_заявки = @newStatus " +
                                     "WHERE Клиенты.фио = @clientName";

                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@newStatus", guna2ComboBox1.Text);
                updateCommand.Parameters.AddWithValue("@clientName", fio);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Статус заявок был успешно обновлен.");
                }
                else
                {
                    MessageBox.Show("Не удалось обновить статус заявок.");
                }
            }
        }
    }
}
