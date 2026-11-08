using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Lab1.Shared;

namespace Lab1.WinForms
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            
            Load += MainForm_Load;
            btnCreate.Click += btnCreate_Click;
            btnEditOwner.Click += btnEditOwner_Click;
            btnDelete.Click += btnDelete_Click;
            btnRestore.Click += btnRestore_Click;
            btnFreezeToggle.Click += btnFreezeToggle_Click;
            btnTransfer.Click += btnTransfer_Click;
            chkShowDeleted.CheckedChanged += chkShowDeleted_CheckedChanged;
        }

        public event EventHandler<bool>? ShowAccountsRequested;
        public event EventHandler<AddAccountEventArgs>? AddAccountRequested;
        public event EventHandler<ChangeOwnerEventArgs>? ChangeOwnerRequested;
        public event EventHandler<AccountIdEventArgs>? DeleteAccountRequested;
        public event EventHandler<AccountIdEventArgs>? RestoreAccountRequested;
        public event EventHandler<AccountIdEventArgs>? ToggleFreezeRequested;
        public event EventHandler<TransferEventArgs>? TransferRequested;

        private BankAccountDto? SelectedAccount =>
            dataGridView1.CurrentRow?.DataBoundItem as BankAccountDto;

        private void MainForm_Load(object? sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            ShowAccountsRequested?.Invoke(this, chkShowDeleted.Checked);
        }

        private void chkShowDeleted_CheckedChanged(object? sender, EventArgs e)
        {
            ShowAccountsRequested?.Invoke(this, chkShowDeleted.Checked);
        }

        public void DisplayAccounts(IEnumerable<BankAccountDto> accounts)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = accounts;
            ConfigureColumns();
        }

        public void ShowMessage(string message, bool isError = false)
        {
            MessageBox.Show(message, isError ? "Ошибка" : "Информация", MessageBoxButtons.OK, isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        public void ClearInputs()
        {
            txtAccountNumber.Clear();
            txtOwnerName.Clear();
            txtNewOwner.Clear();
            txtTargetAccount.Clear();
            txtAmount.Clear();
        }

        private void ConfigureColumns()
        {
            var headers = new Dictionary<string, string>
            {
                ["Id"] = "ID",
                ["Number"] = "Номер счёта",
                ["AccountType"] = "Тип счёта",
                ["OwnerName"] = "Владелец",
                ["OwnerId"] = "ID владельца",
                ["DateCreate"] = "Дата открытия",
                ["Balance"] = "Баланс",
                ["IsActive"] = "Активен",
                ["IsDeleted"] = "Удалён",
            };
            foreach (var (name, header) in headers)
                if (dataGridView1.Columns.Contains(name))
                    dataGridView1.Columns[name]!.HeaderText = header;

            if (dataGridView1.Columns.Contains("Number"))
                dataGridView1.Columns["Number"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void btnCreate_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtOwnerName.Text, out int ownerId))
            {
                ShowMessage("ID владельца должен быть числом.", true);
                return;
            }

            AddAccountRequested?.Invoke(this, new AddAccountEventArgs(txtAccountNumber.Text, ownerId, 1, 0m));
        }

        private void btnEditOwner_Click(object? sender, EventArgs e)
        {
            var account = SelectedAccount;
            if (account == null)
            {
                ShowMessage("Выберите счёт в таблице.", true);
                return;
            }
            if (!int.TryParse(txtNewOwner.Text, out int newOwnerId))
            {
                ShowMessage("ID нового владельца должен быть числом.", true);
                return;
            }

            ChangeOwnerRequested?.Invoke(this, new ChangeOwnerEventArgs(account.Id, newOwnerId));
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            var account = SelectedAccount;
            if (account == null)
            {
                ShowMessage("Выберите счёт в таблице.", true);
                return;
            }

            DeleteAccountRequested?.Invoke(this, new AccountIdEventArgs(account.Id));
        }

        private void btnRestore_Click(object? sender, EventArgs e)
        {
            var account = SelectedAccount;
            if (account == null)
            {
                ShowMessage("Отметьте «Показать удалённые» и выберите удалённый счёт.", true);
                return;
            }

            RestoreAccountRequested?.Invoke(this, new AccountIdEventArgs(account.Id));
        }

        private void btnFreezeToggle_Click(object? sender, EventArgs e)
        {
            var account = SelectedAccount;
            if (account == null)
            {
                ShowMessage("Выберите счёт в таблице.", true);
                return;
            }

            ToggleFreezeRequested?.Invoke(this, new AccountIdEventArgs(account.Id));
        }

        private void btnTransfer_Click(object? sender, EventArgs e)
        {
            var account = SelectedAccount;
            if (account == null)
            {
                ShowMessage("Выберите счёт-отправитель в таблице.", true);
                return;
            }
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                ShowMessage("Введите корректную сумму.", true);
                return;
            }

            TransferRequested?.Invoke(this, new TransferEventArgs(account.Id, txtTargetAccount.Text, amount));
        }

        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
