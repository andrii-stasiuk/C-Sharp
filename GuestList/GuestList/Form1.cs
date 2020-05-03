using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuestList
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable("tbl");
        string workingFile = "";
        public Form1()
        {
            InitializeComponent();
        }

        //Method for Import XML data from file
        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Guest list file|*.xml";
            openFileDialog1.Title = "Import guest list file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    //table.ReadXml(Application.StartupPath + "\\guests.xml"); old version, testing purposes
                    table.Clear();
                    workingFile = openFileDialog1.FileName;
                    table.ReadXml(workingFile);
                    dataGridView1.DataSource = table;
                    MessageBox.Show("Guest list successfully imported");
                 }
                 catch
                 {
                    MessageBox.Show("An error occurred while importing");
                 }
        }

        //Method for export XML data to file
        private void button2_Click(object sender, EventArgs e)
        {
            //table.WriteXml(@"d:\guests.xml", XmlWriteMode.WriteSchema); old version, testing purposes
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Guest list file|*.xml";
            saveFileDialog1.Title = "Export guest list file";
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.FileName = "*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    //table.WriteXml(Application.StartupPath + "\\guests.xml", XmlWriteMode.WriteSchema); old version, testing purposes
                    workingFile = saveFileDialog1.FileName;
                    table.WriteXml(workingFile, XmlWriteMode.WriteSchema);
                    MessageBox.Show("Guest list successfully exported");
                }
                catch
                {
                    MessageBox.Show("An error occurred while exporting");
                }
        }

        //Method that creates table structure for user data (guest list)
        private void Form1_Load(object sender, EventArgs e)
        {
            var colName = new DataGridViewTextBoxColumn();
            var colSex = new DataGridViewComboBoxColumn();
            var colInvited = new DataGridViewCheckBoxColumn();
            var colResponded = new DataGridViewCheckBoxColumn();

            dataGridView1.Columns.Add(colName);
            dataGridView1.Columns.Add(colSex);
            dataGridView1.Columns.Add(colInvited);
            dataGridView1.Columns.Add(colResponded);

            colName.HeaderText = "Person Name";
            colName.DataPropertyName = "NameField";

            colSex.HeaderText = "Sex";
            colSex.DataPropertyName = "SexField";
            colSex.Items.Add("male");
            colSex.Items.Add("female");

            colInvited.HeaderText = "Invited";
            colInvited.DataPropertyName = "InvitedField";

            colResponded.HeaderText = "Responded";
            colResponded.DataPropertyName = "RespondedField";

            table.Columns.Add(new DataColumn("NameField", typeof(string)));
            table.Columns.Add(new DataColumn("SexField", typeof(string)));
            table.Columns.Add(new DataColumn("InvitedField", typeof(bool)));
            table.Columns.Add(new DataColumn("RespondedField", typeof(bool)));
            //table.Columns.Add(new DataColumn("ID", typeof(int))); reserved for future purposes

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = table.DefaultView;
        }
        
        //Method for searching persons by their names in list
        private void button3_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            bool notFoundMarker = true;

            dataGridView1.CurrentRow.Selected = false;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //if (dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(search)) old version of search - case sensitive
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >=0)
                {
                    dataGridView1.Rows[i].Selected = true;
                    notFoundMarker = false;
                    break;
                }
            }

            if (notFoundMarker) MessageBox.Show("A person with this name was not found");
        }

        //Method that checks the number of men and women who will come to the party
        //and advises what should the director do to achieve better results
        private void button4_Click(object sender, EventArgs e)
        {
            int man = 0;
            int woman = 0;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if( (dataGridView1.Rows[i].Cells[1].Value.ToString() == "male") && (dataGridView1.Rows[i].Cells[3].Value.Equals(true)))
                    man++;
                else if( (dataGridView1.Rows[i].Cells[1].Value.ToString() == "female") && (dataGridView1.Rows[i].Cells[3].Value.Equals(true)))
                    woman++;
            }

            if(man>woman)
                MessageBox.Show("You need to invite more women: " + man.ToString() + " men and " + woman.ToString() + " women will come");
            else if(woman>man)
                MessageBox.Show("You need to invite more men: " + man.ToString() + " men and " + woman.ToString() + " women will come");
            else
                MessageBox.Show("You are doing everything right! The same number of men and women will come: " + man.ToString() + " men and " + woman.ToString()+ " women");
        }

        //Method for saving XML data to file
        private void button5_Click(object sender, EventArgs e)
        {
            if (workingFile != "")
            {
                try
                {
                    table.WriteXml(workingFile, XmlWriteMode.WriteSchema);
                    MessageBox.Show("Guest list successfully saved to file " + workingFile);
                }
                catch
                {
                    MessageBox.Show("An error occurred while saving");
                }
            }
            else button2.PerformClick();

        }

        //Method that deletes one person or group of persons
        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(row.Index);
            }
        }
    }
}
