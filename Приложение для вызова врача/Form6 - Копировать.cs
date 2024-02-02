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
using System.Windows.Forms;

namespace Приложение_для_вызова_врача
{
    public partial class Form6 : Form
    {
        string connectionString = "server=127.0.0.1;user=root;password=12345;database=call_doctor";

        public Form6()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string login = guna2TextBox1.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < login.Length; i++)
            {
                if (login[i] >= 'А' && login[i] <= 'Я' || login[i] >= 'а' && login[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста и цифр");
                    guna2TextBox1.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox1.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста и цифр");
                    guna2TextBox1.Text = null;
                }
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            string password = guna2TextBox2.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 'А' && password[i] <= 'Я' || password[i] >= 'а' && password[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста, цифр и специальные символы");
                    guna2TextBox2.Text = null;
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Введите логин");
            }

            else if (guna2TextBox2.Text == "")
            {
                MessageBox.Show("Введите пароль");
            }


            if (!string.IsNullOrEmpty(guna2TextBox1.Text) && !string.IsNullOrEmpty(guna2TextBox2.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string login = guna2TextBox1.Text;

                    string queryVxod = "SELECT логин, пароль FROM Клиенты WHERE логин = @Login AND пароль = @Password";
                    MySqlCommand command = new MySqlCommand(queryVxod, connection);
                    command.Parameters.AddWithValue("@Login", guna2TextBox1.Text);
                    command.Parameters.AddWithValue("@Password", guna2TextBox2.Text);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        Form8 form8 = new Form8(login);
                        form8.Show();
                        this.Hide();
                        MessageBox.Show("Вы вошли успешно");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, неправильный логин или пароль");
                        guna2TextBox1.Text = null;
                        guna2TextBox2.Text = null;
                    }

                    connection.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            guna2TextBox3.Visible = true;
            guna2TextBox4.Visible = true;
            guna2Button3.Visible = true;
            guna2Button4.Visible = true;

            label1.Visible = false;
            guna2TextBox1.Visible = false;
            guna2TextBox2.Visible = false;
            guna2Button2.Visible = false;

            guna2TextBox1.Text = null;
            guna2TextBox2.Text = null;
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            string phone_number = guna2TextBox3.Text;
            string specialCharacters = @"[!@#$%^&*()_\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < phone_number.Length; i++)
            {
                if (phone_number[i] >= 'A' && phone_number[i] <= 'Z' || phone_number[i] >= 'a' && phone_number[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox3.Text = "+375";
                    guna2TextBox3.Select(guna2TextBox3.Text.Length, 0);
                }

                if (phone_number[i] >= 'А' && phone_number[i] <= 'Я' || phone_number[i] >= 'а' && phone_number[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox3.Text = "+375";
                    guna2TextBox3.Select(guna2TextBox3.Text.Length, 0);
                }

                if (Regex.IsMatch(guna2TextBox3.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только цифр");
                    guna2TextBox3.Text = "+375";
                    guna2TextBox3.Select(guna2TextBox3.Text.Length, 0);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "")
            {
                MessageBox.Show("Введите номер телефона");
            }
            else if (guna2TextBox3.Text.Length != 13)
            {
                MessageBox.Show("Ошибка, некорректный номер телефона");
                guna2TextBox3.Text = "+375";
                guna2TextBox3.Select(guna2TextBox3.Text.Length, 0);
            }

            else if (guna2TextBox4.Text == "")
            {
                MessageBox.Show("Введите ключевое слово");
            }
            else if (guna2TextBox4.Text.Length <= 3)
            {
                MessageBox.Show("Ошибка, ключевое слово должно содержать не менее 4 символов");
                guna2TextBox4.Text = null;
            }


            if ((guna2TextBox3.Text.Length == 13) && !string.IsNullOrEmpty(guna2TextBox4.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string queryLogin = "SELECT логин FROM Клиенты WHERE номер_телефона = @NumberPhone and ключевое_слово = @KeyWord";
                    MySqlCommand command1 = new MySqlCommand(queryLogin, connection);
                    command1.Parameters.AddWithValue("@NumberPhone", guna2TextBox3.Text);
                    command1.Parameters.AddWithValue("@KeyWord", guna2TextBox4.Text);

                    object resultLogin = command1.ExecuteScalar();

                    string queryPassword = "SELECT пароль FROM Клиенты WHERE номер_телефона = @NumberPhone and ключевое_слово = @KeyWord";
                    MySqlCommand command2 = new MySqlCommand(queryPassword, connection);
                    command2.Parameters.AddWithValue("@NumberPhone", guna2TextBox3.Text);
                    command2.Parameters.AddWithValue("@KeyWord", guna2TextBox4.Text);

                    object resulPassword = command2.ExecuteScalar();

                    if (resultLogin != null && resulPassword != null)
                    {
                        MessageBox.Show($"Логин {resultLogin}\nПароль {resulPassword}");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка, неправильный номер телефона или ключевое слово");
                        guna2TextBox3.Text = null; 
                        guna2TextBox4.Text = null;
                    }

                    connection.Close();
                }
            }
        }

        private void guna2TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (guna2TextBox3.Text == "+375" && e.KeyChar == (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void guna2TextBox3_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox3.Text))
            {
                guna2TextBox3.Text = "+375";
            }
            guna2TextBox3.SelectionStart = guna2TextBox3.Text.Length;
        }

        private void guna2TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (guna2TextBox3.Text.StartsWith("+375"))
            {
                if (e.KeyCode == Keys.Back && guna2TextBox3.SelectionStart <= 3)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            string keyword = guna2TextBox4.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < keyword.Length; i++)
            {
                if (keyword[i] >= 'A' && keyword[i] <= 'Z' || keyword[i] >= 'a' && keyword[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox4.Text = null;
                }

                if (keyword[i] >= '0' && keyword[i] <= '9')
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2TextBox3.Visible = false;
            guna2TextBox4.Visible = false;
            guna2Button3.Visible = false;
            guna2Button4.Visible = false;

            guna2TextBox3.Text = null;
            guna2TextBox4.Text = null;

            label1.Visible = true;
            guna2TextBox1.Visible = true;
            guna2TextBox2.Visible = true;
            guna2Button2.Visible = true;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
