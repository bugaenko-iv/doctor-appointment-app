using Google.Protobuf.Reflection;
using Org.BouncyCastle.Tls.Crypto;
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
using MySql.Data.MySqlClient;

namespace Приложение_для_вызова_врача
{
    public partial class Form5 : Form
    {
        string connectionString = "server=127.0.0.1;user=root;password=12345;database=call_doctor";

        public Form5()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string fio = guna2TextBox1.Text;
            string[] checkFio = fio.Split(' ');

            int wordCount = checkFio.Length;

            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Введите фио");
            }
            else if (wordCount != 3)
            {
                MessageBox.Show("Ошибка, некорректное фио");
                guna2TextBox1.Text = null;
            }

            else if(guna2TextBox2.Text == "")
            {
                MessageBox.Show("Введите направление");
            }
            else if (guna2TextBox2.Text.Length <= 3)
            {
                MessageBox.Show("Ошибка, направление должно содержать не менее 4 символов");
                guna2TextBox2.Text = null;
            }

            else if(guna2TextBox3.Text == "")
            {
                MessageBox.Show("Введите категорию");
            }
            else if (guna2TextBox3.Text.Length <= 3)
            {
                MessageBox.Show("Ошибка, категория должна содержать не менее 4 символов");
                guna2TextBox3.Text = null;
            }

            else if (guna2TextBox4.Text == "")
            {
                MessageBox.Show("Введите логин");
            }
            else if (guna2TextBox4.Text.Length <= 3)
            {
                MessageBox.Show("Ошибка, логин должен содержать не менее 4 символов");
                guna2TextBox4.Text = null;
            }
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string queryLogin = "SELECT логин FROM Врачи WHERE логин = @Login";
                MySqlCommand command1 = new MySqlCommand(queryLogin, connection);
                command1.Parameters.AddWithValue("@Login", guna2TextBox4.Text);

                object resultLogin = command1.ExecuteScalar();

                if (resultLogin != null)
                {
                    MessageBox.Show("Ошибка, такой логин уже существует");
                    guna2TextBox4.Text = null;
                }

                connection.Close();
            }

            if (guna2TextBox5.Text == "")
            {
                MessageBox.Show("Введите пароль");
            }
            else if (guna2TextBox5.Text.Length <= 5)
            {
                MessageBox.Show("Ошибка, пароль должен содержать не менее 6 символов");
                guna2TextBox5.Text = null;
            }

            else if (guna2TextBox6.Text == "")
            {
                MessageBox.Show("Введите ключевое слово");
            }
            else if (guna2TextBox6.Text.Length <= 3)
            {
                MessageBox.Show("Ошибка, ключевое слово должно содержать не менее 4 символов");
                guna2TextBox6.Text = null;
            }


            if (!string.IsNullOrEmpty(guna2TextBox1.Text) && !string.IsNullOrEmpty(guna2TextBox2.Text) && !string.IsNullOrEmpty(guna2TextBox3.Text) && !string.IsNullOrEmpty(guna2TextBox4.Text) && !string.IsNullOrEmpty(guna2TextBox5.Text) && !string.IsNullOrEmpty(guna2TextBox6.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_врача FROM Врачи ORDER BY id_врача DESC LIMIT 1";
                    MySqlCommand command1 = new MySqlCommand(query, connection);
                    object result = command1.ExecuteScalar();

                    try
                    {
                        if (result != DBNull.Value)
                        {
                            int id = Convert.ToInt32(result);
                            id += 1;

                            string commandText = "INSERT INTO Врачи (id_врача, фио, направление, категория, логин, пароль, ключевое_слово) VALUES (@id_врача, @фио, @направление, @категория, @логин, @пароль, @ключевое_слово)";
                            MySqlCommand command2 = new MySqlCommand(commandText, connection);
                            command2.Parameters.AddWithValue("@id_врача", id);
                            command2.Parameters.AddWithValue("@фио", guna2TextBox1.Text);
                            command2.Parameters.AddWithValue("@направление", guna2TextBox2.Text);
                            command2.Parameters.AddWithValue("@категория", guna2TextBox3.Text);
                            command2.Parameters.AddWithValue("@логин", guna2TextBox4.Text);
                            command2.Parameters.AddWithValue("@пароль", guna2TextBox5.Text);
                            command2.Parameters.AddWithValue("@ключевое_слово", guna2TextBox6.Text);
                            command2.ExecuteNonQuery();
                            MessageBox.Show("Данные успешно добавлены");

                            guna2TextBox1.Text = null;
                            guna2TextBox2.Text = null;
                            guna2TextBox3.Text = null;
                            guna2TextBox4.Text = null;
                            guna2TextBox5.Text = null;
                            guna2TextBox6.Text = null;
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

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            string login = guna2TextBox4.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < login.Length; i++)
            {
                if (login[i] >= 'А' && login[i] <= 'Я' || login[i] >= 'а' && login[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста и цифр");
                    guna2TextBox4.Text = null;
                }

                if (Regex.IsMatch(guna2TextBox4.Text, specialCharacters))
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста и цифр");
                    guna2TextBox4.Text = null;
                }
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            string password = guna2TextBox5.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 'А' && password[i] <= 'Я' || password[i] >= 'а' && password[i] <= 'я')
                {
                    MessageBox.Show("Ошибка, допускается ввод только английского текста, цифр и специальные символы");
                    guna2TextBox5.Text = null;
                }
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            string keyword = guna2TextBox6.Text;
            string specialCharacters = @"[!@#$%^&*()_+\-=\[\]{}|\\:;""'<>,.?\/~]";

            for (int i = 0; i < keyword.Length; i++)
            {
                if (keyword[i] >= 'A' && keyword[i] <= 'Z' || keyword[i] >= 'a' && keyword[i] <= 'z')
                {
                    MessageBox.Show("Ошибка, допускается ввод только русского текста");
                    guna2TextBox6.Text = null;
                }

                if (keyword[i] >= '0' && keyword[i] <= '9')
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

        private void Form5_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
