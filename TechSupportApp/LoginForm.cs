using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;
using TechSupportApp.Models;

namespace TechSupportApp
{
    public partial class LoginForm : Form
    {
        private Label lblLogin;
        private Label lblPassword;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblLogin = new Label();
            this.lblPassword = new Label();
            this.txtLogin = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnRegister = new Button();
            this.SuspendLayout();

            // lblLogin
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(30, 30);
            this.lblLogin.Text = "Логин:";

            // txtLogin
            this.txtLogin.Location = new System.Drawing.Point(110, 27);
            this.txtLogin.Size = new System.Drawing.Size(150, 22);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 70);
            this.lblPassword.Text = "Пароль:";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(110, 67);
            this.txtPassword.Size = new System.Drawing.Size(150, 22);
            this.txtPassword.PasswordChar = '*';

            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(110, 110);
            this.btnLogin.Size = new System.Drawing.Size(80, 30);
            this.btnLogin.Text = "Войти";
            this.btnLogin.Click += new EventHandler(btnLogin_Click);

            // btnRegister
            this.btnRegister.Location = new System.Drawing.Point(200, 110);
            this.btnRegister.Size = new System.Drawing.Size(100, 30);
            this.btnRegister.Text = "Регистрация";
            this.btnRegister.Click += new EventHandler(btnRegister_Click);

            // LoginForm
            this.ClientSize = new System.Drawing.Size(350, 180);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLogin);
            this.Name = "LoginForm";
            this.Text = "Вход в систему";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            string hashed = HashPassword(password);
            string query = @"
                SELECT u.user_id, u.login, u.role, u.client_id, u.employee_id,
                       COALESCE(c.first_name + ' ' + c.last_name, e.first_name + ' ' + e.last_name) AS FullName
                FROM Users u
                LEFT JOIN Clients c ON u.client_id = c.client_id
                LEFT JOIN Employees e ON u.employee_id = e.employee_id
                WHERE u.login = @login AND u.password = @pwd";

            SqlParameter[] pars = {
                new SqlParameter("@login", login),
                new SqlParameter("@pwd", hashed)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, pars);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Неверный логин или пароль.");
                return;
            }

            DataRow row = dt.Rows[0];
            User currentUser = new User
            {
                UserId = Convert.ToInt32(row["user_id"]),
                Login = row["login"].ToString(),
                Role = row["role"].ToString(),
                ClientId = row["client_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["client_id"]),
                EmployeeId = row["employee_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["employee_id"]),
                FullName = row["FullName"].ToString()
            };

            if (currentUser.Role == "client")
            {
                ClientMainForm clientForm = new ClientMainForm(currentUser);
                clientForm.Show();
            }
            else if (currentUser.Role == "employee")
            {
                EmployeeMainForm empForm = new EmployeeMainForm(currentUser);
                empForm.Show();
            }
            else
            {
                MessageBox.Show("Неизвестная роль.");
                return;
            }

            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog();
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