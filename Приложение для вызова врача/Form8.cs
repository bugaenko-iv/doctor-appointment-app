using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace Приложение_для_вызова_врача
{
    public partial class Form8 : Form
    {
        string connectionString = "server=127.0.0.1;user=root;password=12345;database=call_doctor";

        string login;
        int menuIndex = 0;
        int menuIndexChoice = 0;

        string fio = "";
        string direction = "";
        string category = "";

        string servicename = " ";
        string servicedescr = " ";
        string serviceprice = " ";

        string DefaultStatusRequest = "Ожидается";

        string date = DateTime.Now.ToShortDateString();

        public Form8(string login)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.login = login;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void поискУслогуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuIndex = 2;

            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            guna2DataGridView1.AllowUserToResizeRows = false;
            guna2DataGridView1.AllowUserToResizeColumns = false;

            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги;";
            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            dataTable2 = new DataTable();

            adapter.Fill(dataTable2);

            guna2DataGridView1.DataSource = dataTable2;

            guna2DataGridView1.Columns[0].Width = 130;
            guna2DataGridView1.Columns[1].Width = 190;
            guna2DataGridView1.Columns[2].Width = 68;
        }

        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void выToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuIndex = 4;
        }

        DataTable dataTable, dataTable1_1, dataTable2, dataTable2_2;

        private void поискВрачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuIndex = 1;

            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            guna2DataGridView1.AllowUserToResizeRows = false;
            guna2DataGridView1.AllowUserToResizeColumns = false;


            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "SELECT ФИО, направление, категория FROM Врачи;";
            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            dataTable = new DataTable();

            adapter.Fill(dataTable);

            guna2DataGridView1.DataSource = dataTable;

            guna2DataGridView1.Columns[0].Width = 160;
            guna2DataGridView1.Columns[2].Width = 128;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

            if (menuIndex == 1 && selectedRow != null)
            {
                menuIndexChoice = 1;
                guna2Button7.Visible = true;
                fio = selectedRow.Cells[0].Value.ToString();
                direction = selectedRow.Cells[1].Value.ToString();
                category = selectedRow.Cells[2].Value.ToString();

                guna2Button8.Visible = true;
            }

            if (menuIndex == 2 && menuIndexChoice == 2 && selectedRow != null)
            {
                menuIndexChoice = 2;
                guna2Button10.Visible = true;
                servicename = selectedRow.Cells[0].Value.ToString();
                servicedescr = selectedRow.Cells[1].Value.ToString();
                serviceprice = selectedRow.Cells[2].Value.ToString();

                guna2Button8.Visible = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (menuIndex == 1)
            {
                guna2Button1.Visible = false;
                guna2Button2.Visible = true;
                guna2ComboBox1.Visible = true;
            }

            if (menuIndex == 2)
            {
                guna2Button1.Visible = false;
                guna2Button2.Visible = true;
                guna2ComboBox2.Visible = true;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2Button2.Visible = false;

            guna2ComboBox1.Text = null;
            guna2ComboBox2.Text = null;

            if (guna2Button2.Visible == false)
            {
                guna2Button1.Visible = true;

                guna2ComboBox1.Visible = false;
                guna2ComboBox2.Visible = false;

                guna2TextBox1.Visible = false;
                guna2TextBox2.Visible = false;
                guna2TextBox3.Visible = false;
                guna2TextBox4.Visible = false;
                guna2TextBox5.Visible = false;
                guna2TextBox6.Visible = false;
                guna2TextBox7.Visible = false;
                guna2TextBox8.Visible = false;

                guna2Button3.Visible = false;
                guna2Button4.Visible = false;
                guna2Button6.Visible = false;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                guna2TextBox1.Text = null;
                guna2TextBox1.Visible = true;
                guna2TextBox2.Visible = false;
                guna2TextBox3.Visible = false;
                guna2Button6.Visible = false;
                guna2Button3.Visible = true;
                guna2Button4.Visible = true;
            }

            if (guna2ComboBox1.SelectedIndex == 1)
            {
                guna2TextBox1.Visible = false;
                guna2TextBox2.Text = null;
                guna2TextBox2.Visible = true;
                guna2TextBox3.Visible = false;
                guna2Button6.Visible = false;
                guna2Button3.Visible = true;
                guna2Button4.Visible = true;
            }

            if (guna2ComboBox1.SelectedIndex == 2)
            {
                guna2TextBox1.Visible = false;
                guna2TextBox2.Visible = false;
                guna2TextBox3.Text = null;
                guna2TextBox3.Visible = true;
                guna2Button6.Visible = false;
                guna2Button3.Visible = true;
                guna2Button4.Visible = true;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == 0 && string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                MessageBox.Show("Введите фио");
            }
            else if (guna2ComboBox1.SelectedIndex == 0 && !string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query1 = "SELECT ФИО, направление, категория FROM Врачи WHERE ФИО = @fio;";
                    MySqlCommand command1 = new MySqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@fio", guna2TextBox1.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command1))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("К сожалению, по вашему запросу не удалось найти соответствующих данных");
                        }
                        else
                        {
                            guna2DataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }

            if (guna2ComboBox1.SelectedIndex == 1 && string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                MessageBox.Show("Введите направление");
            }
            else if (guna2ComboBox1.SelectedIndex == 1 && !string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query2 = "SELECT ФИО, направление, категория FROM Врачи WHERE направление = @direction;";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@direction", guna2TextBox2.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command2))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("К сожалению, по вашему запросу не удалось найти соответствующих данных");
                        }
                        else
                        {
                            guna2DataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }

            if (guna2ComboBox1.SelectedIndex == 2 && string.IsNullOrWhiteSpace(guna2TextBox3.Text))
            {
                MessageBox.Show("Введите категорию");
            }
            else if (guna2ComboBox1.SelectedIndex == 2 && !string.IsNullOrWhiteSpace(guna2TextBox3.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query3 = "SELECT ФИО, направление, категория FROM Врачи WHERE категория = @categories;";
                    MySqlCommand command3 = new MySqlCommand(query3, connection);
                    command3.Parameters.AddWithValue("@categories", guna2TextBox3.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command3))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("К сожалению, по вашему запросу не удалось найти соответствующих данных");
                        }
                        else
                        {
                            guna2DataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string fio = guna2TextBox1.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < fio.Length; i++)
            {
                if (fio[i] >= 'A' && fio[i] <= 'Z' || fio[i] >= 'a' && fio[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox1.Text = null;
                }

                if (fio[i] >= '0' && fio[i] <= '9')
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
            string direction = guna2TextBox2.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < direction.Length; i++)
            {
                if (direction[i] >= 'A' && direction[i] <= 'Z' || direction[i] >= 'a' && direction[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox2.Text = null;
                }

                if (direction[i] >= '0' && direction[i] <= '9')
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
            string categories = guna2TextBox3.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < categories.Length; i++)
            {
                if (categories[i] >= 'A' && categories[i] <= 'Z' || categories[i] >= 'a' && categories[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox3.Text = null;
                }

                if (categories[i] >= '0' && categories[i] <= '9')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox3.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox3.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox3.Text = null;
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (menuIndex == 1)
            {
                guna2DataGridView1.DataSource = dataTable;
            }

            if (menuIndex == 2)
            {
                guna2DataGridView1.DataSource = dataTable2;
            }

            if(menuIndexChoice == 2)
            {
                guna2DataGridView1.DataSource = dataTable2_2;
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            string password = guna2TextBox4.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 'А' && password[i] <= 'Я' || password[i] >= 'а' && password[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста, цифр и специальные символы");
                    guna2TextBox4.Text = null;
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text == "")
            {
                MessageBox.Show("Введите пароль");
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string queryCheck = "SELECT логин, пароль FROM Клиенты WHERE логин = @Login AND пароль = @Password";
                    MySqlCommand command = new MySqlCommand(queryCheck, connection);
                    command.Parameters.AddWithValue("@Login", guna2TextBox5.Text);
                    command.Parameters.AddWithValue("@Password", guna2TextBox4.Text);

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

                                string queryDelete = "DELETE FROM Клиенты WHERE логин = @Login AND пароль = @Password";
                                MySqlCommand command2 = new MySqlCommand(queryDelete, connection2);
                                command2.Parameters.AddWithValue("@Login", guna2TextBox5.Text);
                                command2.Parameters.AddWithValue("@Password", guna2TextBox4.Text);
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
                        guna2TextBox4.Text = null;
                        MessageBox.Show("Ошибка, неправильный пароль");
                    }

                    connection.Close();
                }
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            string servicename = guna2TextBox6.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < servicename.Length; i++)
            {
                if (servicename[i] >= 'A' && servicename[i] <= 'Z' || servicename[i] >= 'a' && servicename[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox6.Text = null;
                }

                if (servicename[i] >= '0' && servicename[i] <= '9')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox6.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox6.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox6.Text = null;
                }
            }
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            string servicedescr = guna2TextBox7.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < servicedescr.Length; i++)
            {
                if (servicedescr[i] >= 'A' && servicedescr[i] <= 'Z' || servicedescr[i] >= 'a' && servicedescr[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox7.Text = null;
                }

                if (servicedescr[i] >= '0' && servicedescr[i] <= '9')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox7.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox7.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox7.Text = null;
                }
            }
        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
            string serviceprice = guna2TextBox8.Text;
            string specialCharacters = @"[!@#$%^&*()_\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < serviceprice.Length; i++)
            {
                if (serviceprice[i] >= 'A' && serviceprice[i] <= 'Z' || serviceprice[i] >= 'a' && serviceprice[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox8.Text = null;
                }

                if (serviceprice[i] >= 'А' && serviceprice[i] <= 'Я' || serviceprice[i] >= 'а' && serviceprice[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox8.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox8.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox8.Text = null;
                }
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedIndex == 0)
            {
                guna2TextBox6.Text = null;
                guna2TextBox6.Visible = true;
                guna2TextBox7.Visible = false;
                guna2TextBox8.Visible = false;
                guna2Button3.Visible = false;
                guna2Button4.Visible = true;
                guna2Button6.Visible = true;
            }

            if (guna2ComboBox2.SelectedIndex == 1)
            {
                guna2TextBox6.Visible = false;
                guna2TextBox7.Text = null;
                guna2TextBox7.Visible = true;
                guna2TextBox8.Visible = false;
                guna2Button3.Visible = false;
                guna2Button4.Visible = true;
                guna2Button6.Visible = true;
            }

            if (guna2ComboBox2.SelectedIndex == 2)
            {
                guna2TextBox6.Visible = false;
                guna2TextBox7.Visible = false;
                guna2TextBox8.Text = null;
                guna2TextBox8.Visible = true;
                guna2Button3.Visible = false;
                guna2Button4.Visible = true;
                guna2Button6.Visible = true;
            }
        }

        bool checkButton1 = false;

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (menuIndex == 1)
            {
                menuIndex = 2;
                menuIndexChoice = 2;

                if (guna2Button1.Visible == false)
                {
                    checkButton1 = true;
                    guna2Button1.Visible = true;
                    guna2Button2.Visible = false;
                    guna2ComboBox1.Visible = false;
                    guna2TextBox1.Visible = false;
                    guna2TextBox2.Visible = false;
                    guna2TextBox3.Visible = false;
                    guna2Button3.Visible = false;
                    guna2Button4.Visible = false;
                }

                guna2HtmlLabel1.Visible = false;
                guna2HtmlLabel2.Visible = true;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string queryId = "SELECT id_врача FROM Врачи WHERE фио = @fio and направление = @direction and категория = @category;";
                    MySqlCommand command2 = new MySqlCommand(queryId, connection);
                    command2.Parameters.AddWithValue("@fio", fio);
                    command2.Parameters.AddWithValue("@direction", direction);
                    command2.Parameters.AddWithValue("@category", category);
                    connection.Open();
                    object resultId = command2.ExecuteScalar();

                    if (resultId != null)
                    {
                        int idDoctor = Convert.ToInt32(resultId);

                        string query = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги WHERE id_врача = @idDoctor;";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@idDoctor", idDoctor);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            guna2Button7.Visible = false;
                            guna2Button8.Visible = false;
                            guna2Button9.Visible = true;

                            menuIndexChoice = 2;

                            dataTable2_2 = new DataTable();

                            adapter.Fill(dataTable2_2);

                            guna2DataGridView1.DataSource = dataTable2_2;

                            guna2DataGridView1.Columns[0].Width = 160;
                            guna2DataGridView1.Columns[2].Width = 128;
                        }
                    }
                    else
                    {
                        MessageBox.Show("У этого врача нету услуг");
                    }
                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedIndex == 0 && string.IsNullOrWhiteSpace(guna2TextBox6.Text))
            {
                MessageBox.Show("Введите название услуги");
            }
            else if (guna2ComboBox2.SelectedIndex == 0 && !string.IsNullOrWhiteSpace(guna2TextBox6.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query1 = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги WHERE название_уcлуги = @servicename;";
                    MySqlCommand command1 = new MySqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@servicename", guna2TextBox6.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command1))
                    {
                        DataTable dataTable2 = new DataTable();
                        adapter.Fill(dataTable2);

                        if (dataTable2.Rows.Count == 0)
                        {
                            MessageBox.Show("К сожалению, по вашему запросу не удалось найти соответствующих данных");
                        }
                        else
                        {
                            guna2DataGridView1.DataSource = dataTable2;
                        }
                    }
                }
            }

            if (guna2ComboBox2.SelectedIndex == 1 && string.IsNullOrWhiteSpace(guna2TextBox7.Text))
            {
                MessageBox.Show("Введите описание услуги");
            }
            else if (guna2ComboBox2.SelectedIndex == 1 && !string.IsNullOrWhiteSpace(guna2TextBox7.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query2 = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги WHERE описание_услуги = @servicedescr;";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@servicedescr", guna2TextBox7.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command2))
                    {
                        DataTable dataTable2 = new DataTable();
                        adapter.Fill(dataTable2);

                        if (dataTable2.Rows.Count == 0)
                        {
                            MessageBox.Show("К сожалению, по вашему запросу не удалось найти соответствующих данных");
                        }
                        else
                        {
                            guna2DataGridView1.DataSource = dataTable2;
                        }
                    }
                }
            }

            if (guna2ComboBox2.SelectedIndex == 2 && string.IsNullOrWhiteSpace(guna2TextBox8.Text))
            {
                MessageBox.Show("Введите стоимость услуги");
            }
            else if (guna2ComboBox2.SelectedIndex == 2 && !string.IsNullOrWhiteSpace(guna2TextBox8.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query3 = "SELECT название_уcлуги, описание_услуги, стоимость FROM Мед_услуги WHERE стоимость = @serviceprice;";
                    MySqlCommand command3 = new MySqlCommand(query3, connection);
                    command3.Parameters.AddWithValue("@serviceprice", guna2TextBox8.Text);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command3))
                    {
                        DataTable dataTable2 = new DataTable();
                        adapter.Fill(dataTable2);

                        if (dataTable2.Rows.Count == 0)
                        {
                            MessageBox.Show("К сожалению, по вашему запросу не удалось найти соответствующих данных");
                        }
                        else
                        {
                            guna2DataGridView1.DataSource = dataTable2;
                        }
                    }
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (menuIndex == 1 && menuIndexChoice == 1)
            {
                guna2Button7.Visible = false;
                guna2Button8.Visible = false;
            }

            if(menuIndex == 1 && menuIndexChoice == 1)
            {
                guna2DataGridView1.AllowUserToResizeRows = false;
                guna2DataGridView1.AllowUserToResizeColumns = false;


                MySqlConnection connection = new MySqlConnection(connectionString);

                string query = "SELECT ФИО, направление, категория FROM Врачи;";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                dataTable = new DataTable();

                adapter.Fill(dataTable);

                guna2DataGridView1.DataSource = dataTable;

                guna2DataGridView1.Columns[0].Width = 160;
                guna2DataGridView1.Columns[2].Width = 128;
            }

            if (menuIndex == 2 && menuIndexChoice == 2)
            {
                guna2Button10.Visible = false;
                guna2Button8.Visible = false;
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (menuIndex == 2 && menuIndexChoice == 2)
            {
                string time = Microsoft.VisualBasic.Interaction.InputBox("Введите время:");

                if (!string.IsNullOrWhiteSpace(time))
                {
                    bool containsLetters = false;

                    foreach (char c in time)
                    {
                        if (char.IsLetter(c))
                        {
                            containsLetters = true;
                            break;
                        }
                    }

                    if (!containsLetters)
                    {
                        // Формируем информацию о заявке
                        string applicationInfo = $"Информация о заявке:\n" +
                            $"ФИО врача: {fio}\n" +
                            $"Направление: {direction}\n" +
                            $"Категория: {category}\n" +
                            $"Название услуги: {servicename}\n" +
                            $"Описание услуги: {servicedescr}\n" +
                            $"Цена услуги: {serviceprice}\n" +
                            $"Время: {time}\n" +
                            $"Дата оформления заявки: {date}";

                        // Создаем сообщение с информацией о заявке и вариантами ответов
                        string message = $"{applicationInfo}\n\nХотите оформить заявку?";
                        string caption = "Оформление заявки";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                        // Отображаем диалоговое окно с информацией и вариантами ответов в одном окне
                        DialogResult result = MessageBox.Show(message, caption, buttons);

                        // Обрабатываем выбранный результат
                        if (result == DialogResult.Yes)
                        {
                            // Пользователь выбрал "Оформить"

                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                connection.Open();

                                string query = "SELECT id_заявки FROM Заявки ORDER BY id_заявки DESC LIMIT 1";
                                MySqlCommand command1 = new MySqlCommand(query, connection);
                                object result2 = command1.ExecuteScalar();

                                string query2 = "SELECT id_клиента FROM Клиенты WHERE логин = @login";
                                MySqlCommand command3 = new MySqlCommand(query2, connection);
                                command3.Parameters.AddWithValue("@login", login);
                                object result3 = command3.ExecuteScalar();

                                string query3 = "SELECT id_врача FROM Врачи WHERE фио = @fio";
                                MySqlCommand command4 = new MySqlCommand(query3, connection);
                                command4.Parameters.AddWithValue("@fio", fio);
                                object result4 = command4.ExecuteScalar();

                                string query4 = "SELECT id_услуги FROM Мед_услуги WHERE название_уcлуги = @servicename";
                                MySqlCommand command5 = new MySqlCommand(query4, connection);
                                command5.Parameters.AddWithValue("@servicename", servicename);
                                object result5 = command5.ExecuteScalar();


                                try
                                {
                                    if (result2 != DBNull.Value)
                                    {
                                        int id = Convert.ToInt32(result2);
                                        id += 1;

                                        int idKlient = Convert.ToInt32(result3);
                                        int idDoctor = Convert.ToInt32(result4);
                                        int idService = Convert.ToInt32(result5);

                                        string commandText = "INSERT INTO Заявки (id_заявки, id_клиента, id_врача, id_услуги, дата_оформления_заявки, выбранное_время, статус_заявки) VALUES (@id_заявки, @id_клиента, @id_врача, @id_услуги, @дата_оформления_заявки, @выбранное_время, @статус_заявки)";
                                        MySqlCommand command2 = new MySqlCommand(commandText, connection);
                                        command2.Parameters.AddWithValue("@id_заявки", id);
                                        command2.Parameters.AddWithValue("@id_клиента", idKlient);
                                        command2.Parameters.AddWithValue("@id_врача", idDoctor);
                                        command2.Parameters.AddWithValue("@id_услуги", idService);
                                        command2.Parameters.AddWithValue("@дата_оформления_заявки", date);
                                        command2.Parameters.AddWithValue("@выбранное_время", time);
                                        command2.Parameters.AddWithValue("@статус_заявки", DefaultStatusRequest);
                                        command2.ExecuteNonQuery();
                                        MessageBox.Show("Заявка успешно оформлена");
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
                        else if (result == DialogResult.No)
                        {
                            // Пользователь выбрал "Отменить"
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, допускается ввод только цифр и специальные символы");
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное время");
                }
            }
            else
            {
                MessageBox.Show("Ошибка в параметрах меню");
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void заявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuIndex = 3;

            guna2DataGridView1.AllowUserToResizeRows = false;
            guna2DataGridView1.AllowUserToResizeColumns = false;

            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "SELECT Врачи.фио, Мед_услуги.название_уcлуги, Заявки.выбранное_время, Заявки.дата_оформления_заявки, Заявки.статус_заявки FROM Клиенты JOIN Заявки ON Клиенты.id_клиента = Заявки.id_клиента JOIN Мед_услуги ON Заявки.id_услуги = Мед_услуги.id_услуги JOIN Врачи ON Заявки.id_врача = Врачи.id_врача WHERE Клиенты.логин = @login;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@login", login);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            dataTable = new DataTable();

            adapter.Fill(dataTable);

            guna2DataGridView1.DataSource = dataTable;

            if (menuIndex == 3)
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

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if(menuIndex == 2 && menuIndexChoice == 2)
            {
                guna2Button7.Visible = true;
                guna2Button8.Visible = true;

                guna2HtmlLabel2.Visible = false;
                guna2HtmlLabel1.Visible = true;

                menuIndex = 1;
                menuIndexChoice = 1;

                if(checkButton1 == true)
                {
                    guna2Button1.Visible = false;
                    guna2Button2.Visible = true;
                    guna2ComboBox1.Visible = true;   //фикс
                    guna2TextBox1.Visible = true;
                    guna2TextBox2.Visible = true;
                    guna2TextBox3.Visible = true;
                    guna2Button3.Visible = true;
                    guna2Button4.Visible = true;
                    checkButton1 = false;
                }

                guna2DataGridView1.AllowUserToResizeRows = false;
                guna2DataGridView1.AllowUserToResizeColumns = false;


                MySqlConnection connection = new MySqlConnection(connectionString);

                string query = "SELECT ФИО, направление, категория FROM Врачи WHERE ФИО = @fio and направление = @direction and категория = @category;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@fio", fio);
                command.Parameters.AddWithValue("@direction", direction);
                command.Parameters.AddWithValue("@category", category);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                dataTable = new DataTable();

                adapter.Fill(dataTable);

                guna2DataGridView1.DataSource = dataTable;

                guna2DataGridView1.Columns[0].Width = 160;
                guna2DataGridView1.Columns[2].Width = 128;

                guna2Button9.Visible = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            guna2DataGridView1.Visible = false;

            guna2Button1.Visible = false;
            guna2Button2.Visible = false;
            guna2Button3.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button10.Visible = false;

            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2TextBox3.Visible = false;
            guna2TextBox4.Visible = false;
            guna2TextBox5.Visible = false;
            guna2TextBox6.Visible = false;
            guna2TextBox7.Visible = false;
            guna2TextBox8.Visible = false;

            guna2HtmlLabel1.Visible = false;
            guna2HtmlLabel2.Visible = false;
            guna2HtmlLabel3.Visible = false;
            guna2HtmlLabel4.Visible = false;

            guna2ComboBox1.Visible = false;
            guna2ComboBox2.Visible = false;

            if (e.ClickedItem == menuStrip1.Items[0])
            {
                guna2DataGridView1.Visible = true;
                guna2Button1.Visible = true;
                guna2HtmlLabel1.Visible = true;
            }

            else if (e.ClickedItem == menuStrip1.Items[1])
            {
                guna2DataGridView1.Visible = true;
                guna2Button1.Visible = true;
                guna2HtmlLabel2.Visible = true;
            }

            else if (e.ClickedItem == menuStrip1.Items[2])
            {
                guna2DataGridView1.Visible = true;
                guna2HtmlLabel3.Visible = true;
            }

            else if (e.ClickedItem == menuStrip1.Items[3])
            {
                guna2TextBox4.Visible = true;
                guna2TextBox5.Text = login;
                guna2TextBox5.Enabled = false;
                guna2TextBox5.Visible = true;
                guna2Button5.Visible = true;
                guna2HtmlLabel4.Visible = true;
            }
        }
    }
}
