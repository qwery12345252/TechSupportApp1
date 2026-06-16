using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class RequestHistoryForm : Form
    {
        private int clientId;
        private DataGridView dgvHistory;
        private Button btnClose;
        private Panel panelButtons;

        public RequestHistoryForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            LoadHistory();
        }

        private void InitializeComponent()
        {
            this.dgvHistory = new DataGridView();
            this.btnClose = new Button();
            this.panelButtons = new Panel();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();

            this.panelButtons.Dock = DockStyle.Top;
            this.panelButtons.Height = 50;
            this.panelButtons.Controls.Add(this.btnClose);

            this.btnClose.Location = new System.Drawing.Point(500, 10);
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new EventHandler(btnClose_Click);

            this.dgvHistory.Dock = DockStyle.Fill;
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.ClientSize = new System.Drawing.Size(620, 400);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.panelButtons);
            this.Name = "RequestHistoryForm";
            this.Text = "История заявок";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void LoadHistory()
        {
            string query = @"
                SELECT request_id AS [ID], tip_request AS [Тип], date AS [Дата], time AS [Время],
                       status AS [Статус], description AS [Описание], solution AS [Решение]
                FROM Requests
                WHERE client_id = @cid
                ORDER BY date DESC, time DESC";

            SqlParameter[] pars = { new SqlParameter("@cid", clientId) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, pars);
            dgvHistory.DataSource = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}