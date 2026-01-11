namespace ContactBookWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstContacts;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstContacts = new System.Windows.Forms.ListBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            //Labels
            // 
            this.lblName.Text = "Name:";
            this.lblName.Location = new System.Drawing.Point(20, 20);
            this.lblPhone.Text = "Phone:";
            this.lblPhone.Location = new System.Drawing.Point(20, 60);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(20, 100);
            // 
            // TextBoxes
            // 
            this.txtName.Location = new System.Drawing.Point(80, 20);
            this.txtPhone.Location = new System.Drawing.Point(80, 60);
            this.txtEmail.Location = new System.Drawing.Point(80, 100);
            this.txtName.Width = this.txtPhone.Width = this.txtEmail.Width = 200;
            // 
            // Buttons
            // 
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new System.Drawing.Point(300, 20);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Text = "Edit";
            this.btnEdit.Location = new System.Drawing.Point(300, 60);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new System.Drawing.Point(300, 100);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new System.Drawing.Point(300, 140);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Search Box
            // 
            this.txtSearch.Location = new System.Drawing.Point(80, 140);
            this.txtSearch.Width = 200;
            // 
            // ListBox
            // 
            this.lstContacts.Location = new System.Drawing.Point(20, 180);
            this.lstContacts.Size = new System.Drawing.Size(360, 200);
            this.lstContacts.SelectedIndexChanged += new System.EventHandler(this.lstContacts_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lstContacts);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblEmail);
            this.Text = "Contact Book";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
