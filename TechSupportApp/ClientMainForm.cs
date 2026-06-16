using System;
using System.Windows.Forms;
using TechSupportApp.Models;

namespace TechSupportApp
{
    public partial class ClientMainForm : Form
    {
        private User currentUser;
        private Label lblWelcome;
        private Button btnCreateRequest, btnActiveRequests, btnHistory, btnChangeColor, btnLogout;

        public ClientMainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            lblWelcome.Text = $"Добро пожаловать, {user.FullName}!";
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.btnCreateRequest = new Button();
            this.btnActiveRequests = new Button();
            this.btnHistory = new Button();
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
            // btnCreateRequest
            // 
            this.btnCreateRequest.Location = new System.Drawing.Point(30, 80);
            this.btnCreateRequest.Name = "btnCreateRequest";
            this.btnCreateRequest.Size = new System.Drawing.Size(150, 30);
            this.btnCreateRequest.Text = "Создать заявку";
            this.btnCreateRequest.Click += new EventHandler(btnCreateRequest_Click);
            // 
            // btnActiveRequests
            // 
            this.btnActiveRequests.Location = new System.Drawing.Point(30, 120);
            this.btnActiveRequests.Name = "btnActiveRequests";
            this.btnActiveRequests.Size = new System.Drawing.Size(150, 30);
            this.btnActiveRequests.Text = "Мои активные заявки";
            this.btnActiveRequests.Click += new EventHandler(btnActiveRequests_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(30, 160);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(150, 30);
            this.btnHistory.Text = "История заявок";
            this.btnHistory.Click += new EventHandler(btnHistory_Click);
            // 
            // btnChangeColor
            // 
            this.btnChangeColor.Location = new System.Drawing.Point(30, 200);
            this.btnChangeColor.Name = "btnChangeColor";
            this.btnChangeColor.Size = new System.Drawing.Size(150, 30);
            this.btnChangeColor.Text = "Сменить цвет фона";
            this.btnChangeColor.Click += new EventHandler(btnChangeColor_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(30, 240);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 30);
            this.btnLogout.Text = "Выйти";
            this.btnLogout.Click += new EventHandler(btnLogout_Click);
            // 
            // ClientMainForm
            // 
            this.ClientSize = new System.Drawing.Size(250, 320);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnChangeColor);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnActiveRequests);
            this.Controls.Add(this.btnCreateRequest);
            this.Controls.Add(this.lblWelcome);
            this.Name = "ClientMainForm";
            this.Text = "Клиент";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            CreateRequestForm form = new CreateRequestForm(currentUser.ClientId.Value);
            form.ShowDialog();
        }

        private void btnActiveRequests_Click(object sender, EventArgs e)
        {
            MyActiveRequestsForm form = new MyActiveRequestsForm(currentUser.ClientId.Value);
            form.ShowDialog();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            RequestHistoryForm form = new RequestHistoryForm(currentUser.ClientId.Value);
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