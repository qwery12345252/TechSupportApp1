using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class RequestDetailsForm : Form
    {
        private int requestId;

        private Label lblId, lblClient, lblEmployee, lblType, lblStatus, lblDate, lblTime, lblDescription, lblSolution;
        private TextBox txtId, txtClient, txtEmployee, txtType, txtStatus, txtDate, txtTime;
        private TextBox txtDescription;
        private RichTextBox rtbSolution;
        private Button btnClose;

        public RequestDetailsForm(int requestId)
        {
            InitializeComponent();
            this.requestId = requestId;
            LoadDetails();
        }

        private void InitializeComponent()
        {
            this.lblId = new Label();
            this.lblClient = new Label();
            this.lblEmployee = new Label();
            this.lblType = new Label();
            this.lblStatus = new Label();
            this.lblDate = new Label();
            this.lblTime = new Label();
            this.lblDescription = new Label();
            this.lblSolution = new Label();

            this.txtId = new TextBox();
            this.txtClient = new TextBox();
            this.txtEmployee = new TextBox();
            this.txtType = new TextBox();
            this.txtStatus = new TextBox();
            this.txtDate = new TextBox();
            this.txtTime = new TextBox();
            this.txtDescription = new TextBox();
            this.rtbSolution = new RichTextBox();

            this.btnClose = new Button();

            this.SuspendLayout();

            // Размещение
            int y = 20;
            int left = 20;
            int labelWidth = 100;
            int textWidth = 250;
            int step = 30;

            // ID
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(left, y);
            this.lblId.Text = "ID заявки:";
            this.txtId.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtId.Size = new System.Drawing.Size(textWidth, 22);
            this.txtId.ReadOnly = true;
            y += step;

            // Клиент
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(left, y);
            this.lblClient.Text = "Клиент:";
            this.txtClient.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtClient.Size = new System.Drawing.Size(textWidth, 22);
            this.txtClient.ReadOnly = true;
            y += step;

            // Сотрудник
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Location = new System.Drawing.Point(left, y);
            this.lblEmployee.Text = "Сотрудник:";
            this.txtEmployee.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtEmployee.Size = new System.Drawing.Size(textWidth, 22);
            this.txtEmployee.ReadOnly = true;
            y += step;

            // Тип
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(left, y);
            this.lblType.Text = "Тип:";
            this.txtType.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtType.Size = new System.Drawing.Size(textWidth, 22);
            this.txtType.ReadOnly = true;
            y += step;

            // Статус
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(left, y);
            this.lblStatus.Text = "Статус:";
            this.txtStatus.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtStatus.Size = new System.Drawing.Size(textWidth, 22);
            this.txtStatus.ReadOnly = true;
            y += step;

            // Дата
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(left, y);
            this.lblDate.Text = "Дата:";
            this.txtDate.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtDate.Size = new System.Drawing.Size(textWidth, 22);
            this.txtDate.ReadOnly = true;
            y += step;

            // Время
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(left, y);
            this.lblTime.Text = "Время:";
            this.txtTime.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtTime.Size = new System.Drawing.Size(textWidth, 22);
            this.txtTime.ReadOnly = true;
            y += step;

            // Описание
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(left, y);
            this.lblDescription.Text = "Описание:";
            this.txtDescription.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.txtDescription.Size = new System.Drawing.Size(textWidth, 60);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.ReadOnly = true;
            y += 70;

            // Решение
            this.lblSolution.AutoSize = true;
            this.lblSolution.Location = new System.Drawing.Point(left, y);
            this.lblSolution.Text = "Решение:";
            this.rtbSolution.Location = new System.Drawing.Point(left + labelWidth, y - 3);
            this.rtbSolution.Size = new System.Drawing.Size(textWidth, 60);
            this.rtbSolution.ReadOnly = true;
            y += 70;

            // Кнопка закрыть
            this.btnClose.Location = new System.Drawing.Point(left + labelWidth, y + 10);
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Закрыть";
            this.btnClose.Click += new EventHandler(btnClose_Click);

            // Форма
            this.ClientSize = new System.Drawing.Size(420, y + 70);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rtbSolution);
            this.Controls.Add(this.lblSolution);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtEmployee);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.Name = "RequestDetailsForm";
            this.Text = "Детали заявки";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadDetails()
        {
            string query = @"
                SELECT r.request_id, r.tip_request, r.date, r.time, r.status,
                       c.first_name + ' ' + c.last_name AS ClientName,
                       e.first_name + ' ' + e.last_name AS EmployeeName,
                       r.description, r.solution
                FROM Requests r
                LEFT JOIN Clients c ON r.client_id = c.client_id
                LEFT JOIN Employees e ON r.employee_id = e.employee_id
                WHERE r.request_id = @rid";

            SqlParameter[] pars = { new SqlParameter("@rid", requestId) };
            DataTable dt = DatabaseHelper.ExecuteQuery(query, pars);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtId.Text = row["request_id"].ToString();
                txtClient.Text = row["ClientName"]?.ToString() ?? "Не указан";
                txtEmployee.Text = row["EmployeeName"]?.ToString() ?? "Не назначен";
                txtType.Text = row["tip_request"].ToString();
                txtStatus.Text = row["status"].ToString();
                txtDate.Text = Convert.ToDateTime(row["date"]).ToShortDateString();
                txtTime.Text = row["time"].ToString();
                txtDescription.Text = row["description"]?.ToString() ?? "Нет описания";
                rtbSolution.Text = row["solution"]?.ToString() ?? "Решение ещё не добавлено";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}