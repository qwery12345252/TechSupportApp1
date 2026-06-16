using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class RegisterForm : Form
    {
        private Label lblLogin, lblPassword, lblPassword2;
        private Label lblFirstName, lblLastName, lblMiddleName;
        private Label lblPhone, lblEmail, lblAddress, lblRole;
        private TextBox txtLogin, txtPassword, txtPassword2;
        private TextBox txtFirstName, txtLastName, txtMiddleName;
        private TextBox txtPhone, txtEmail, txtAddress;
        private ComboBox cmbRole;
        private Button btnRegister, btnBack;

        public RegisterForm()
        {
            InitializeComponent();
            cmbRole.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            // Создаём все элементы
            this.lblLogin = new Label();
            this.lblPassword = new Label();
            this.lblPassword2 = new Label();
            this.lblFirstName = new Label();
            this.lblLastName = new Label();
            this.lblMiddleName = new Label();
            this.lblPhone = new Label();
            this.lblEmail = new Label();
            this.lblAddress = new Label();
            this.lblRole = new Label();

            this.txtLogin = new TextBox();
            this.txtPassword = new TextBox();
            this.txtPassword2 = new TextBox();
            this.txtFirstName = new TextBox();
            this.txtLastName = new TextBox();
            this.txtMiddleName = new TextBox();
            this.txtPhone = new TextBox();
            this.txtEmail = new TextBox();
            this.txtAddress = new TextBox();
            this.cmbRole = new ComboBox();
            this.btnRegister = new Button();
            this.btnBack = new Button();

            this.SuspendLayout();

            int y = 20;
            int leftLabel = 20;
            int leftTextBox = 110;
            int step = 35;
            int textBoxWidth = 180;

            // Логин
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(leftLabel, y);
            this.lblLogin.Text = "Логин:";
            this.txtLogin.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtLogin.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Пароль
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(leftLabel, y);
            this.lblPassword.Text = "Пароль:";
            this.txtPassword.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtPassword.Size = new System.Drawing.Size(textBoxWidth, 22);
            this.txtPassword.PasswordChar = '*';
            y += step;

            // Повтор
            this.lblPassword2.AutoSize = true;
            this.lblPassword2.Location = new System.Drawing.Point(leftLabel, y);
            this.lblPassword2.Text = "Повтор:";
            this.txtPassword2.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtPassword2.Size = new System.Drawing.Size(textBoxWidth, 22);
            this.txtPassword2.PasswordChar = '*';
            y += step;

            // Имя
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(leftLabel, y);
            this.lblFirstName.Text = "Имя:";
            this.txtFirstName.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtFirstName.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Фамилия
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(leftLabel, y);
            this.lblLastName.Text = "Фамилия:";
            this.txtLastName.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtLastName.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Отчество
            this.lblMiddleName.AutoSize = true;
            this.lblMiddleName.Location = new System.Drawing.Point(leftLabel, y);
            this.lblMiddleName.Text = "Отчество:";
            this.txtMiddleName.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtMiddleName.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Телефон
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(leftLabel, y);
            this.lblPhone.Text = "Телефон:";
            this.txtPhone.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtPhone.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(leftLabel, y);
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtEmail.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Адрес
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(leftLabel, y);
            this.lblAddress.Text = "Адрес:";
            this.txtAddress.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.txtAddress.Size = new System.Drawing.Size(textBoxWidth, 22);
            y += step;

            // Роль
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(leftLabel, y);
            this.lblRole.Text = "Роль:";
            this.cmbRole.Location = new System.Drawing.Point(leftTextBox, y - 3);
            this.cmbRole.Size = new System.Drawing.Size(textBoxWidth, 24);
            this.cmbRole.Items.AddRange(new object[] { "Клиент", "Сотрудник" });
            y += step + 10;

            // Кнопки
            this.btnRegister.Location = new System.Drawing.Point(leftTextBox, y);
            this.btnRegister.Size = new System.Drawing.Size(130, 30);
            this.btnRegister.Text = "Зарегистрироваться";
            this.btnRegister.Click += new EventHandler(btnRegister_Click);

            this.btnBack.Location = new System.Drawing.Point(leftTextBox + 140, y);
            this.btnBack.Size = new System.Drawing.Size(80, 30);
            this.btnBack.Text = "Назад";
            this.btnBack.Click += new EventHandler(btnBack_Click);

            // Форма
            this.ClientSize = new System.Drawing.Size(360, y + 60);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.lblMiddleName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtPassword2);
            this.Controls.Add(this.lblPassword2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Name = "RegisterForm";
            this.Text = "Регистрация";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            string password2 = txtPassword2.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || password != password2)
            {
                MessageBox.Show("Проверьте правильность заполнения полей и совпадение паролей.");
                return;
            }

            string role = cmbRole.SelectedItem.ToString() == "Клиент" ? "client" : "employee";

            string checkLogin = "SELECT COUNT(*) FROM Users WHERE login = @login";
            int exists = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkLogin, new SqlParameter("@login", login)));
            if (exists > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }

            string hashed = HashPassword(password);

            using (SqlConnection conn = new SqlConnection(DatabaseHelper.GetConnectionString()))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    int? clientId = null;
                    int? employeeId = null;

                    if (role == "client")
                    {
                        string insertClient = @"
                            INSERT INTO Clients (first_name, last_name, middle_name, phone, email, date, address)
                            VALUES (@fn, @ln, @mn, @ph, @em, @dt, @ad);
                            SELECT SCOPE_IDENTITY();";

                        SqlParameter[] parsClient = {
                            new SqlParameter("@fn", txtFirstName.Text),
                            new SqlParameter("@ln", txtLastName.Text),
                            new SqlParameter("@mn", txtMiddleName.Text),
                            new SqlParameter("@ph", txtPhone.Text),
                            new SqlParameter("@em", txtEmail.Text),
                            new SqlParameter("@dt", DateTime.Now.Date),
                            new SqlParameter("@ad", txtAddress.Text)
                        };
                        using (SqlCommand cmd = new SqlCommand(insertClient, conn, tran))
                        {
                            cmd.Parameters.AddRange(parsClient);
                            clientId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    else
                    {
                        string insertEmp = @"
                            INSERT INTO Employees (first_name, last_name, middle_name, phone, email, date, quantity_request)
                            VALUES (@fn, @ln, @mn, @ph, @em, @dt, 0);
                            SELECT SCOPE_IDENTITY();";

                        SqlParameter[] parsEmp = {
                            new SqlParameter("@fn", txtFirstName.Text),
                            new SqlParameter("@ln", txtLastName.Text),
                            new SqlParameter("@mn", txtMiddleName.Text),
                            new SqlParameter("@ph", txtPhone.Text),
                            new SqlParameter("@em", txtEmail.Text),
                            new SqlParameter("@dt", DateTime.Now.Date)
                        };
                        using (SqlCommand cmd = new SqlCommand(insertEmp, conn, tran))
                        {
                            cmd.Parameters.AddRange(parsEmp);
                            employeeId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }

                    string insertUser = @"
                        INSERT INTO Users (login, password, role, client_id, employee_id)
                        VALUES (@login, @pwd, @role, @cid, @eid);";

                    SqlParameter[] parsUser = {
                        new SqlParameter("@login", login),
                        new SqlParameter("@pwd", hashed),
                        new SqlParameter("@role", role),
                        new SqlParameter("@cid", (object)clientId ?? DBNull.Value),
                        new SqlParameter("@eid", (object)employeeId ?? DBNull.Value)
                    };
                    using (SqlCommand cmd = new SqlCommand(insertUser, conn, tran))
                    {
                        cmd.Parameters.AddRange(parsUser);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    MessageBox.Show("Регистрация успешна! Теперь войдите.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Ошибка регистрации: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}