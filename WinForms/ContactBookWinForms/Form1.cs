namespace ContactBookWinForms
{
    public partial class Form1 : Form
    {
        List<Contact> contactBook = new List<Contact>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Name and Phone are required.");
                return;
            }

            contactBook.Add(new Contact(name, phone, email));
            RefreshContactList();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstContacts.SelectedItem is Contact selected)
            {
                selected.Name = txtName.Text.Trim();
                selected.Phone = txtPhone.Text.Trim();
                selected.Email = txtEmail.Text.Trim();
                RefreshContactList();
                ClearInputs();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstContacts.SelectedItem is Contact selected)
            {
                contactBook.Remove(selected);
                RefreshContactList();
                ClearInputs();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text.Trim().ToLower();
            var results = contactBook
                .Where(c => c.Name.ToLower().Contains(query) || c.Phone.Contains(query) || c.Email.ToLower().Contains(query))
                .ToList();

            lstContacts.Items.Clear();
            foreach (var contact in results)
            {
                lstContacts.Items.Add(contact);
            }
        }

        private void RefreshContactList()
        {
            lstContacts.Items.Clear();
            foreach (var contact in contactBook)
            {
                lstContacts.Items.Add(contact);
            }
        }

        private void ClearInputs()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
        }

        private void lstContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstContacts.SelectedItem is Contact selected)
            {
                txtName.Text = selected.Name;
                txtPhone.Text = selected.Phone;
                txtEmail.Text = selected.Email;
            }
        }
    }
}
