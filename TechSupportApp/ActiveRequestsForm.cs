using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class ActiveRequestsForm : Form
    {
        private int employeeId;
        private DataGridView dgvRequests;
        private Button btnCloseRequest, btnRefresh, btnClose;
        private Panel panelButtons;

        public ActiveRequestsForm(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            LoadRequests();
        }

        private void InitializeComponent()
        {
            this.dgvRequests = new DataGridView();
            this.btnCloseRequest = new Button();
            this.btnRefresh = new Button();
            this.btnClose = new Button();
            this.panelButtons = new Panel();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();

            // Panel для кнопок
            this.panelButtons.Dock = DockStyle.Top;
            this.panelButtons.Height = 50;
            this.panelButtons.Controls.Add(this.btnCloseRequest);
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Controls.Add(this.btnClose);

            // btnCloseRequest
            this.btnCloseRequest.Location = new System.Drawing.Point(12, 10);
            this.btnCloseRequest.Size = new System.Drawing.Size(120, 30);
            this.btnCloseRequest.Text = "Закрыть заявку";
            this.btnCloseRequest.Click += new EventHandler(btnCloseRequest_Click);

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(140, 10);
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(500, 10);
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new EventHandler(btnClose_Click);

            // dgvRequests
            this.dgvRequests.Dock = DockStyle.Fill;
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ActiveRequestsForm
            this.ClientSize = new System.Drawing.Size(620, 400);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.panelButtons);
            this.Name = "ActiveRequestsForm";
            this.Text = "Активные заявки";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void LoadRequests()
        {
            string query = @"
                SELECT r.request_id AS [ID], r.tip_request AS [Тип], r.date AS [Дата], r.time AS [Время],
                       r.status AS [Статус], c.first_name + ' ' + c.last_name AS [Клиент],
                       r.description AS [Описание]
                FROM Requests r
                INNER JOIN Clients c ON r.client_id = c.client_id
                WHERE r.status NOT IN ('Закрыта', 'Решена')
                ORDER BY r.date DESC, r.time DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            dgvRequests.DataSource = dt;
        }

        private void btnCloseRequest_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку для закрытия.");
                return;
            }

            int requestId = Convert.ToInt32(dgvRequests.SelectedRows[0].Cells["ID"].Value);
            string description = dgvRequests.SelectedRows[0].Cells["Описание"].Value?.ToString() ?? "";
            CloseRequestDialog dialog = new CloseRequestDialog(requestId, employeeId, description);
            dialog.ShowDialog();
            LoadRequests();
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