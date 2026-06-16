using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class MyActiveRequestsForm : Form
    {
        private int clientId;
        private DataGridView dgvRequests;
        private Button btnRefresh, btnClose;
        private Panel panelButtons;

        public MyActiveRequestsForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            LoadRequests();
        }

        private void InitializeComponent()
        {
            this.dgvRequests = new DataGridView();
            this.btnRefresh = new Button();
            this.btnClose = new Button();
            this.panelButtons = new Panel();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();

            this.panelButtons.Dock = DockStyle.Top;
            this.panelButtons.Height = 50;
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Controls.Add(this.btnClose);

            this.btnRefresh.Location = new System.Drawing.Point(12, 10);
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);

            this.btnClose.Location = new System.Drawing.Point(500, 10);
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new EventHandler(btnClose_Click);

            this.dgvRequests.Dock = DockStyle.Fill;
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.ClientSize = new System.Drawing.Size(620, 400);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.panelButtons);
            this.Name = "MyActiveRequestsForm";
            this.Text = "Мои активные заявки";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void LoadRequests()
        {
            string query = @"
                SELECT request_id AS [ID], tip_request AS [Тип], date AS [Дата], time AS [Время],
                       status AS [Статус], description AS [Описание]
                FROM Requests
                WHERE client_id = @cid AND status NOT IN ('Закрыта', 'Решена')
                ORDER BY date DESC, time DESC";

            SqlParameter[] pars = { new SqlParameter("@cid", clientId) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, pars);
            dgvRequests.DataSource = dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRequests();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}