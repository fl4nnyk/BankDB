using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankDB.Forms
{
    public partial class TypeTransaction : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;

        private bool NewRowAdding = false;
        public TypeTransaction()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Type_Transaction", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Type_Transaction");

                dataGridView1.DataSource = dataSet.Tables["Type_Transaction"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[2, i] = linkCell;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReloadData()
        {
            try
            {
                dataSet.Tables["Type_Transaction"].Clear();

                sqlDataAdapter.Fill(dataSet, "Type_Transaction");

                dataGridView1.DataSource = dataSet.Tables["Type_Transaction"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[2, i] = linkCell;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TypeTransaction_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=FLANNYK-PC;Initial Catalog=BankDB;Integrated Security=True;TrustServerCertificate=True");

            sqlConnection.Open();

            LoadData();
        }

        private void toolStripButtonUpdate_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Ви справді хочете видалити цю строку?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int RowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(e.RowIndex);

                            dataSet.Tables["Type_Transaction"].Rows[RowIndex].Delete();

                            sqlDataAdapter.Update(dataSet, "Type_Transaction");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int RowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Type_Transaction"].NewRow();

                        row["type_transaction_id"] = dataGridView1.Rows[RowIndex].Cells["type_transaction_id"].Value;
                        row["type_transaction_name"] = dataGridView1.Rows[RowIndex].Cells["type_transaction_name"].Value;

                        dataSet.Tables["Type_Transaction"].Rows.Add(row);

                        dataSet.Tables["Type_Transaction"].Rows.RemoveAt(dataSet.Tables["Type_Transaction"].Rows.Count - 1);

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Type_Transaction");

                        NewRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int row = e.RowIndex;

                        dataSet.Tables["Type_Transaction"].Rows[row]["type_transaction_id"] = dataGridView1.Rows[row].Cells["type_transaction_id"].Value;
                        dataSet.Tables["Type_Transaction"].Rows[row]["type_transaction_name"] = dataGridView1.Rows[row].Cells["type_transaction_name"].Value;

                        sqlDataAdapter.Update(dataSet, "Type_Transaction");

                        dataGridView1.Rows[e.RowIndex].Cells[2].Value = "Delete";
                    }

                    ReloadData();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (NewRowAdding == false)
                {
                    NewRowAdding = true;

                    int lastRow = dataGridView1.Rows.Count - 2;

                    DataGridViewRow row = dataGridView1.Rows[lastRow];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[2, lastRow] = linkCell;

                    row.Cells["Command"].Value = "Insert";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (NewRowAdding == false)
                {
                    int RowIndex = dataGridView1.SelectedCells[0].RowIndex;

                    DataGridViewRow editingRow = dataGridView1.Rows[RowIndex];

                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[2, RowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
