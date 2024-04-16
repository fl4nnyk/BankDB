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
    public partial class Account : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;

        private bool NewRowAdding = false;

        public Account()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Account", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Account");

                dataGridView1.DataSource = dataSet.Tables["Account"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[7, i] = linkCell;
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
                dataSet.Tables["Account"].Clear();

                sqlDataAdapter.Fill(dataSet, "Account");

                dataGridView1.DataSource = dataSet.Tables["Account"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[7, i] = linkCell;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Account_Load(object sender, EventArgs e)
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
                if (e.ColumnIndex == 7)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Ви справді хочете видалити цю строку?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int RowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(e.RowIndex);

                            dataSet.Tables["Account"].Rows[RowIndex].Delete();

                            sqlDataAdapter.Update(dataSet, "Account");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int RowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Account"].NewRow();

                        row["account_id"] = dataGridView1.Rows[RowIndex].Cells["account_id"].Value;
                        row["client_id"] = dataGridView1.Rows[RowIndex].Cells["client_id"].Value;
                        row["currency_id"] = dataGridView1.Rows[RowIndex].Cells["currency_id"].Value;
                        row["status_id"] = dataGridView1.Rows[RowIndex].Cells["status_id"].Value;
                        row["balance"] = dataGridView1.Rows[RowIndex].Cells["balance"].Value;
                        row["opening_date"] = dataGridView1.Rows[RowIndex].Cells["opening_date"].Value;
                        row["closing_date"] = dataGridView1.Rows[RowIndex].Cells["closing_date"].Value;

                        dataSet.Tables["Account"].Rows.Add(row);

                        dataSet.Tables["Account"].Rows.RemoveAt(dataSet.Tables["Account"].Rows.Count - 1);

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Account");

                        NewRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int row = e.RowIndex;

                        dataSet.Tables["Account"].Rows[row]["account_id"] = dataGridView1.Rows[row].Cells["account_id"].Value;
                        dataSet.Tables["Account"].Rows[row]["client_id"] = dataGridView1.Rows[row].Cells["client_id"].Value;
                        dataSet.Tables["Account"].Rows[row]["currency_id"] = dataGridView1.Rows[row].Cells["currency_id"].Value;
                        dataSet.Tables["Account"].Rows[row]["status_id"] = dataGridView1.Rows[row].Cells["status_id"].Value;
                        dataSet.Tables["Account"].Rows[row]["balance"] = dataGridView1.Rows[row].Cells["balance"].Value;
                        dataSet.Tables["Account"].Rows[row]["opening_date"] = dataGridView1.Rows[row].Cells["opening_date"].Value;
                        dataSet.Tables["Account"].Rows[row]["closing_date"] = dataGridView1.Rows[row].Cells["closing_date"].Value;

                        sqlDataAdapter.Update(dataSet, "Account");

                        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Delete";
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

                    dataGridView1[7, lastRow] = linkCell;

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

                    dataGridView1[7, RowIndex] = linkCell;

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

            if (dataGridView1.CurrentCell.ColumnIndex != 6 && dataGridView1.CurrentCell.ColumnIndex != 7 && dataGridView1.CurrentCell.ColumnIndex != 5)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(ColumnKeyPress);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 6  && dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.Validating += new CancelEventHandler(ColumnValidating);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 5)
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
                Regex dateRegex = new Regex(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$");
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