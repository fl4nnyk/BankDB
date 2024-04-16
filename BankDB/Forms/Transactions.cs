using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankDB.Forms
{
    public partial class Transactions : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;

        private bool NewRowAdding = false;
        public Transactions()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Transactions", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Transactions");

                dataGridView1.DataSource = dataSet.Tables["Transactions"];

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
                dataSet.Tables["Transactions"].Clear();

                sqlDataAdapter.Fill(dataSet, "Transactions");

                dataGridView1.DataSource = dataSet.Tables["Transactions"];

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
        private void Transactions_Load(object sender, EventArgs e)
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

                            dataSet.Tables["Transactions"].Rows[RowIndex].Delete();

                            sqlDataAdapter.Update(dataSet, "Transactions");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int RowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Transactions"].NewRow();

                        row["transaction_id"] = dataGridView1.Rows[RowIndex].Cells["transaction_id"].Value;
                        row["account_id"] = dataGridView1.Rows[RowIndex].Cells["account_id"].Value;
                        row["account_id_to"] = dataGridView1.Rows[RowIndex].Cells["account_id_to"].Value;
                        row["exchange_rate_id"] = dataGridView1.Rows[RowIndex].Cells["exchange_rate_id"].Value;
                        row["department_id"] = dataGridView1.Rows[RowIndex].Cells["department_id"].Value;
                        row["type_transaction_id"] = dataGridView1.Rows[RowIndex].Cells["type_transaction_id"].Value;
                        row["amount"] = dataGridView1.Rows[RowIndex].Cells["amount"].Value;
                        row["datetime"] = dataGridView1.Rows[RowIndex].Cells["datetime"].Value;
                        row["description"] = dataGridView1.Rows[RowIndex].Cells["description"].Value;

                        dataSet.Tables["Transactions"].Rows.Add(row);

                        dataSet.Tables["Transactions"].Rows.RemoveAt(dataSet.Tables["Transactions"].Rows.Count - 1);

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[9].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Transactions");

                        NewRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int row = e.RowIndex;

                        dataSet.Tables["Transactions"].Rows[row]["transaction_id"] = dataGridView1.Rows[row].Cells["transaction_id"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["account_id"] = dataGridView1.Rows[row].Cells["account_id"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["account_id_to"] = dataGridView1.Rows[row].Cells["account_id_to"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["exchange_rate_id"] = dataGridView1.Rows[row].Cells["exchange_rate_id"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["department_id"] = dataGridView1.Rows[row].Cells["department_id"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["type_transaction_id"] = dataGridView1.Rows[row].Cells["type_transaction_id"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["amount"] = dataGridView1.Rows[row].Cells["amount"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["datetime"] = dataGridView1.Rows[row].Cells["datetime"].Value;
                        dataSet.Tables["Transactions"].Rows[row]["description"] = dataGridView1.Rows[row].Cells["description"].Value;

                        sqlDataAdapter.Update(dataSet, "Transactions");

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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnKeyPress);

            if (dataGridView1.CurrentCell.ColumnIndex != 8 && dataGridView1.CurrentCell.ColumnIndex != 9 && dataGridView1.CurrentCell.ColumnIndex != 7)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(ColumnKeyPress);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 8)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.Validating += new CancelEventHandler(ColumnValidating);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(ColumnKeyPresD);
                }

            }
        }

        private void ColumnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ColumnValidating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                Regex dateRegex = new Regex(@"^\d{5}-\d{2}-\d{2}$");
                if (!dateRegex.IsMatch(textBox.Text))
                {
                    MessageBox.Show("Неправильний формат дати! Формат має бути РРРР-ММ-ДД.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void ColumnKeyPresD(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            TextBox textBox = sender as TextBox;
            if (textBox != null && e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
    }
}
