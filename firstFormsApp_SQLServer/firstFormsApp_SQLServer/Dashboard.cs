namespace firstFormsApp_SQLServer
{
    public partial class Dashboard : Form
    {
        List<Person> people = new List<Person>();

        public Dashboard()
        {
            InitializeComponent();

            foundPeopleListbox.DataSource = people;
            foundPeopleListbox.DisplayMember = "";
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            DataAccess db = new DataAccess();
            db.GetPeople(lastNameTextBox.Text);
        }
    }
}
