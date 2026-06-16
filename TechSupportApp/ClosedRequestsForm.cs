using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class ClosedRequestsForm : Form
    {
        private int employeeId;
        private DataGridView dgvClosed;
        private Button btnClose;
        private Panel panelButtons;

        public ClosedRequestsForm(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            LoadClosed();
        }

        private void InitializeComponent()
        {
            this.dgvClosed = new DataGridView();
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

            this.dgvClosed.Dock = DockStyle.Fill;
            this.dgvClosed.ReadOnly = true;
            this.dgvClosed.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.ClientSize = new System.Drawing.Size(620, 400);
            this.Controls.Add(this.dgvClosed);
            this.Controls.Add(this.panelButtons);
            this.Name = "ClosedRequestsForm";
            this.Text = "Закрытые заявки";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void LoadClosed()
        {
            string query = @"
                SELECT r.request_id AS [ID], r.tip_request AS [Тип], r.date AS [Дата],
                       r.status AS [Статус], c.first_name + ' ' + c.last_name AS [Клиент],
                       r.description AS [Описание], r.solution AS [Решение]
                FROM Requests r
                INNER JOIN Clients c ON r.client_id = c.client_id
                WHERE r.status IN ('Закрыта', 'Решена')
                ORDER BY r.date DESC";

            DataTable dt = DatabaseHelper.ExecuteQuery(query);
            dgvClosed.DataSource = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}