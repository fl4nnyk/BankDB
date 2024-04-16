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
    public partial class ExchangeRate : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlCommandBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;

        private bool NewRowAdding = false;
        public ExchangeRate()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Command] FROM Exchange_Rate", sqlConnection);
                sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlCommandBuilder.GetInsertCommand();
                sqlCommandBuilder.GetUpdateCommand();
                sqlCommandBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Exchange_Rate");

                dataGridView1.DataSource = dataSet.Tables["Exchange_Rate"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, i] = linkCell;
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
                dataSet.Tables["Exchange_Rate"].Clear();

                sqlDataAdapter.Fill(dataSet, "Exchange_Rate");

                dataGridView1.DataSource = dataSet.Tables["Exchange_Rate"];

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[5, i] = linkCell;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExchangeRate_Load(object sender, EventArgs e)
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
                if (e.ColumnIndex == 5)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Ви справді хочете видалити цю строку?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int RowIndex = e.RowIndex;

                            dataGridView1.Rows.RemoveAt(e.RowIndex);

                            dataSet.Tables["Exchange_Rate"].Rows[RowIndex].Delete();

                            sqlDataAdapter.Update(dataSet, "Exchange_Rate");
                        }
                    }
                    else if (task == "Insert")
                    {
                        int RowIndex = dataGridView1.Rows.Count - 2;

                        DataRow row = dataSet.Tables["Exchange_Rate"].NewRow();

                        row["exchange_rate_id"] = dataGridView1.Rows[RowIndex].Cells["exchange_rate_id"].Value;
                        row["currency_id_from"] = dataGridView1.Rows[RowIndex].Cells["currency_id_from"].Value;
                        row["currency_id_to"] = dataGridView1.Rows[RowIndex].Cells["currency_id_to"].Value;
                        row["rate"] = dataGridView1.Rows[RowIndex].Cells["rate"].Value;
                        row["date_effective"] = dataGridView1.Rows[RowIndex].Cells["date_effective"].Value;

                        dataSet.Tables["Exchange_Rate"].Rows.Add(row);

                        dataSet.Tables["Exchange_Rate"].Rows.RemoveAt(dataSet.Tables["Exchange_Rate"].Rows.Count - 1);

                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Exchange_Rate");

                        NewRowAdding = false;
                    }
                    else if (task == "Update")
                    {
                        int row = e.RowIndex;

                        dataSet.Tables["Exchange_Rate"].Rows[row]["exchange_rate_id"] = dataGridView1.Rows[row].Cells["exchange_rate_id"].Value;
                        dataSet.Tables["Exchange_Rate"].Rows[row]["currency_id_from"] = dataGridView1.Rows[row].Cells["currency_id_from"].Value;
                        dataSet.Tables["Exchange_Rate"].Rows[row]["currency_id_to"] = dataGridView1.Rows[row].Cells["currency_id_to"].Value;
                        dataSet.Tables["Exchange_Rate"].Rows[row]["rate"] = dataGridView1.Rows[row].Cells["rate"].Value;
                        dataSet.Tables["Exchange_Rate"].Rows[row]["date_effective"] = dataGridView1.Rows[row].Cells["date_effective"].Value;

                        sqlDataAdapter.Update(dataSet, "Exchange_Rate");

                        dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Delete";
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

                    dataGridView1[5, lastRow] = linkCell;

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

                    dataGridView1[5, RowIndex] = linkCell;

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

            if (dataGridView1.CurrentCell.ColumnIndex != 3 && dataGridView1.CurrentCell.ColumnIndex != 5)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(ColumnKeyPress);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.Validating += new CancelEventHandler(ColumnValidating);
                }
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 3)
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
