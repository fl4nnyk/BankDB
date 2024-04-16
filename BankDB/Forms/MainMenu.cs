namespace BankDB;

public partial class MainMenu : Form
{
    public MainMenu()
    {
        InitializeComponent();
    }

    private void buttonClient_Click(object sender, EventArgs e)
    {
        Forms.Client client = new Forms.Client();
        client.ShowDialog();
    }

    private void buttonStatus_Click(object sender, EventArgs e)
    {
        Forms.Status client = new Forms.Status();
        client.ShowDialog();
    }

    private void buttonCountry_Click(object sender, EventArgs e)
    {
        Forms.Country client = new Forms.Country();
        client.ShowDialog();
    }

    private void buttonTypeTransaction_Click(object sender, EventArgs e)
    {
        Forms.TypeTransaction client = new Forms.TypeTransaction();
        client.ShowDialog();
    }

    private void buttonDepartment_Click(object sender, EventArgs e)
    {
        Forms.Department client = new Forms.Department();
        client.ShowDialog();
    }

    private void buttonCurrency_Click(object sender, EventArgs e)
    {
        Forms.Currency client = new Forms.Currency();
        client.ShowDialog();
    }

    private void buttonExchangeRate_Click(object sender, EventArgs e)
    {
        Forms.ExchangeRate client = new Forms.ExchangeRate();
        client.ShowDialog();
    }

    private void buttonAccount_Click(object sender, EventArgs e)
    {
        Forms.Account client = new Forms.Account();
        client.ShowDialog();
    }

    private void buttonTransactions_Click(object sender, EventArgs e)
    {
        Forms.Transactions client = new Forms.Transactions();
        client.ShowDialog();
    }
}
