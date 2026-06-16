using System;
using System.Windows.Forms;
using TechSupportApp.Models;

namespace TechSupportApp
{
    public partial class EmployeeMainForm : Form
    {
        private User currentUser;
        private Label lblWelcome;
        private Button btnActiveRequests, btnClosedRequests, btnChangeColor, btnLogout;

        public EmployeeMainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            lblWelcome.Text = $"Добро пожаловать, {user.FullName}!";
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.btnActiveRequests = new Button();
            this.btnClosedRequests = new Button();
            this.btnChangeColor = new Button();
            this.btnLogout = new Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(30, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(150, 17);
            this.lblWelcome.Text = "Добро пожаловать!";
            // 
            // btnActiveRequests
            // 
            this.btnActiveRequests.Location = new System.Drawing.Point(30, 80);
            this.btnActiveRequests.Name = "btnActiveRequests";
            this.btnActiveRequests.Size = new System.Drawing.Size(150, 30);
            this.btnActiveRequests.Text = "Активные заявки";
            this.btnActiveRequests.Click += new EventHandler(btnActiveRequests_Click);
            // 
            // btnClosedRequests
            // 
            this.btnClosedRequests.Location = new System.Drawing.Point(30, 120);
            this.btnClosedRequests.Name = "btnClosedRequests";
            this.btnClosedRequests.Size = new System.Drawing.Size(150, 30);
            this.btnClosedRequests.Text = "Закрытые заявки";
            this.btnClosedRequests.Click += new EventHandler(btnClosedRequests_Click);
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(30, 160);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(150, 30);
            this.btnChangeColor.Text = "Сменить цвет фона";
            this.btnChangeColor.Click += new EventHandler(btnChangeColor_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(30, 200);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 30);
            this.btnLogout.Text = "Выйти";
            this.btnLogout.Click += new EventHandler(btnLogout_Click);
            // 
            // EmployeeMainForm
            // 
            this.ClientSize = new System.Drawing.Size(250, 280);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnChangeColor);
            this.Controls.Add(this.btnClosedRequests);
            this.Controls.Add(this.btnActiveRequests);
            this.Controls.Add(this.lblWelcome);
            this.Name = "EmployeeMainForm";
            this.Text = "Сотрудник";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnActiveRequests_Click(object sender, EventArgs e)
        {
            ActiveRequestsForm form = new ActiveRequestsForm(currentUser.EmployeeId.Value);
            form.ShowDialog();
        }

        private void btnClosedRequests_Click(object sender, EventArgs e)
        {
            ClosedRequestsForm form = new ClosedRequestsForm(currentUser.EmployeeId.Value);
            form.ShowDialog();
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog.Color;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Restart();
        }
    }
}