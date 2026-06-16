using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TechSupportApp.Helpers;

namespace TechSupportApp
{
    public partial class CloseRequestDialog : Form
    {
        private int requestId;
        private int employeeId;
        private TextBox txtSolution;
        private Label lblDescription;
        private TextBox txtDescriptionReadOnly;
        private Button btnConfirm, btnCancel;

        public CloseRequestDialog(int requestId, int employeeId, string description)
        {
            InitializeComponent();
            this.requestId = requestId;
            this.employeeId = employeeId;
            txtDescriptionReadOnly.Text = description;
        }

        private void InitializeComponent()
        {
            this.lblDescription = new Label();
            this.txtDescriptionReadOnly = new TextBox();
            this.txtSolution = new TextBox();
            this.btnConfirm = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(30, 20);
            this.lblDescription.Text = "Описание заявки:";

            // txtDescriptionReadOnly
            this.txtDescriptionReadOnly.Location = new System.Drawing.Point(30, 50);
            this.txtDescriptionReadOnly.Size = new System.Drawing.Size(300, 80);
            this.txtDescriptionReadOnly.Multiline = true;
            this.txtDescriptionReadOnly.ReadOnly = true;
            this.txtDescriptionReadOnly.ScrollBars = ScrollBars.Vertical;

            // lblSolution (метка для решения)
            Label lblSolution = new Label();
            lblSolution.AutoSize = true;
            lblSolution.Location = new System.Drawing.Point(30, 140);
            lblSolution.Text = "Решение:";

            // txtSolution
            this.txtSolution.Location = new System.Drawing.Point(30, 170);
            this.txtSolution.Size = new System.Drawing.Size(300, 80);
            this.txtSolution.Multiline = true;
            this.txtSolution.ScrollBars = ScrollBars.Vertical;

            // btnConfirm
            this.btnConfirm.Location = new System.Drawing.Point(30, 270);
            this.btnConfirm.Size = new System.Drawing.Size(120, 30);
            this.btnConfirm.Text = "Закрыть заявку";
            this.btnConfirm.Click += new EventHandler(btnConfirm_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(170, 270);
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(370, 330);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtSolution);
            this.Controls.Add(lblSolution);
            this.Controls.Add(this.txtDescriptionReadOnly);
            this.Controls.Add(this.lblDescription);
            this.Name = "CloseRequestDialog";
            this.Text = "Закрытие заявки";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string solution = txtSolution.Text.Trim();
            if (string.IsNullOrEmpty(solution))
            {
                MessageBox.Show("Введите решение.");
                return;
            }

            string updateRequest = @"
                UPDATE Requests 
                SET status = 'Закрыта', solution = @solution 
                WHERE request_id = @rid";

            SqlParameter[] pars = {
                new SqlParameter("@rid", requestId),
                new SqlParameter("@solution", solution)
            };
            DatabaseHelper.ExecuteNonQuery(updateRequest, pars);

            MessageBox.Show("Заявка закрыта.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}