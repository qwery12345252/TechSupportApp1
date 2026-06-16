using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class CreateRequestForm : Form
    {
        private int clientId;
        private ComboBox cmbRequestType;
        private TextBox txtDescription;
        private Button btnCreate, btnCancel;

        public CreateRequestForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            cmbRequestType.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.cmbRequestType = new ComboBox();
            this.txtDescription = new TextBox();
            this.btnCreate = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();
            // 
            // cmbRequestType
            // 
            this.cmbRequestType.Location = new System.Drawing.Point(30, 30);
            this.cmbRequestType.Name = "cmbRequestType";
            this.cmbRequestType.Size = new System.Drawing.Size(200, 24);
            this.cmbRequestType.Items.AddRange(new object[] { "Инцидент", "Запрос обслуживания", "Запрос на изменение" });
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(30, 70);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 150);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(30, 240);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(90, 30);
            this.btnCreate.Text = "Создать";
            this.btnCreate.Click += new EventHandler(btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(140, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            // 
            // CreateRequestForm
            // 
            this.ClientSize = new System.Drawing.Size(280, 310);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cmbRequestType);
            this.Name = "CreateRequestForm";
            this.Text = "Создание заявки";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string requestType = cmbRequestType.SelectedItem.ToString();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Введите описание заявки.");
                return;
            }

            // Исправленный запрос с параметром @desc
            string insertRequest = @"
                INSERT INTO Requests (client_id, tip_request, time, date, status, description)
                VALUES (@clientId, @type, @time, @date, 'Новая', @desc);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parsRequest = {
                new SqlParameter("@clientId", clientId),
                new SqlParameter("@type", requestType),
                new SqlParameter("@time", DateTime.Now.TimeOfDay),
                new SqlParameter("@date", DateTime.Now.Date),
                new SqlParameter("@desc", description)  // <-- Параметр добавлен!
            };

            int requestId = Convert.ToInt32(DatabaseHelper.ExecuteScalar(insertRequest, parsRequest));

            string getFirstEmp = "SELECT TOP 1 employee_id FROM Employees";
            object empObj = DatabaseHelper.ExecuteScalar(getFirstEmp);
            int firstEmp = empObj != null ? Convert.ToInt32(empObj) : 1;

            string insertAccounting = @"
                INSERT INTO RequestAccounting (request_id, accepted_by_employee_id, reassigned_to_employee_id, accepted_date)
                VALUES (@reqId, @accEmp, @reassignEmp, @date)";

            SqlParameter[] parsAcc = {
                new SqlParameter("@reqId", requestId),
                new SqlParameter("@accEmp", firstEmp),
                new SqlParameter("@reassignEmp", firstEmp),
                new SqlParameter("@date", DateTime.Now)
            };

            DatabaseHelper.ExecuteNonQuery(insertAccounting, parsAcc);

            MessageBox.Show("Заявка создана! Номер: " + requestId);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}