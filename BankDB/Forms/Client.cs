using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BankDB.Forms
{
    public partial class Client : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;

        private bool NewRowAdding = false;

        public Client()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Client", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Client");

                dataGridView1.DataSource = dataSet.Tables["Client"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[9, i] = linkCell;
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
                dataSet.Tables["Client"].Clear();

                sqlDataAdapter.Fill(dataSet, "Client");

                dataGridView1.DataSource = dataSet.Tables["Client"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[9, i] = linkCell;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Client_Load(object sender, EventArgs e)
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
                if (e.ColumnIndex == 9)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Ви справді хочете видалити цю строку?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int RowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(e.RowIndex);

                            dataSet.Tables["Client"].Rows[RowIndex].Delete();

                            sqlDataAdapter.Update(dataSet, "Client");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int RowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Client"].NewRow();

                        row["client_id"] = dataGridView1.Rows[RowIndex].Cells["client_id"].Value;
                        row["name"] = dataGridView1.Rows[RowIndex].Cells["name"].Value;
                        row["surname"] = dataGridView1.Rows[RowIndex].Cells["surname"].Value;
                        row["middle_name"] = dataGridView1.Rows[RowIndex].Cells["middle_name"].Value;
                        row["date_of_birth"] = dataGridView1.Rows[RowIndex].Cells["date_of_birth"].Value;
                        row["identification_number"] = dataGridView1.Rows[RowIndex].Cells["identification_number"].Value;
                        row["address"] = dataGridView1.Rows[RowIndex].Cells["address"].Value;
                        row["phone"] = dataGridView1.Rows[RowIndex].Cells["phone"].Value;
                        row["email"] = dataGridView1.Rows[RowIndex].Cells["email"].Value;

                        dataSet.Tables["Client"].Rows.Add(row);

                        dataSet.Tables["Client"].Rows.RemoveAt(dataSet.Tables["Client"].Rows.Count - 1);

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[9].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Client");

                        NewRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int row = e.RowIndex;

                        dataSet.Tables["Client"].Rows[row]["client_id"] = dataGridView1.Rows[row].Cells["client_id"].Value;
                        dataSet.Tables["Client"].Rows[row]["name"] = dataGridView1.Rows[row].Cells["name"].Value;
                        dataSet.Tables["Client"].Rows[row]["surname"] = dataGridView1.Rows[row].Cells["surname"].Value;
                        dataSet.Tables["Client"].Rows[row]["middle_name"] = dataGridView1.Rows[row].Cells["middle_name"].Value;
                        dataSet.Tables["Client"].Rows[row]["date_of_birth"] = dataGridView1.Rows[row].Cells["date_of_birth"].Value;
                        dataSet.Tables["Client"].Rows[row]["identification_number"] = dataGridView1.Rows[row].Cells["identification_number"].Value;
                        dataSet.Tables["Client"].Rows[row]["address"] = dataGridView1.Rows[row].Cells["address"].Value;
                        dataSet.Tables["Client"].Rows[row]["phone"] = dataGridView1.Rows[row].Cells["phone"].Value;
                        dataSet.Tables["Client"].Rows[row]["email"] = dataGridView1.Rows[row].Cells["email"].Value;

                        sqlDataAdapter.Update(dataSet, "Client");

                        dataGridView1.Rows[e.RowIndex].Cells[9].Value = "Delete";
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

                    dataGridView1[9, lastRow] = linkCell;

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

                    dataGridView1[9, RowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //e.Control.KeyPress -= new KeyPressEventHandler(ColumnKeyPress);

            //if (dataGridView1.CurrentCell.ColumnIndex != 1 && dataGridView1.CurrentCell.ColumnIndex != 2)
            //{
            //    TextBox textBox = e.Control as TextBox;

            //    if (textBox != null)
            //    {
            //        textBox.KeyPress += new KeyPressEventHandler(ColumnKeyPress);
            //    }
            //}
            if (dataGridView1.CurrentCell.ColumnIndex == 4) 
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.Validating += new CancelEventHandler(ColumnValidating);
                }
            }
        }

        //private void ColumnKeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        private void ColumnValidating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                Regex dateRegex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
                if (!dateRegex.IsMatch(textBox.Text))
                {
                    MessageBox.Show("Неправильний формат дати! Формат має бути РРРР-ММ-ДД.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true; 
                }
            }
        }
    }
}
