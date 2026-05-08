// TaskEditDialog.cs — Модальний діалог створення/редагування завдання.

using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp10
{
    public class TaskEditDialog : Form
    {
        private TextBox        _txtName     = null!;
        private ComboBox       _cmbPriority = null!;
        private ComboBox       _cmbStatus   = null!;
        private DateTimePicker _dtDeadline  = null!;

        // Робоча копія завдання — застосовується тільки при OK.
        public TaskItem Task { get; }

        public TaskEditDialog(TaskItem? existing)
        {
            Task = existing != null
                ? new TaskItem
                {
                    Name     = existing.Name,
                    Priority = existing.Priority,
                    Status   = existing.Status,
                    Deadline = existing.Deadline,
                }
                : new TaskItem();

            BuildUI(isEdit: existing != null);
        }

        private void BuildUI(bool isEdit)
        {
            Text            = isEdit ? "Редагувати завдання" : "Нове завдання";
            ClientSize      = new Size(440, 260);
            StartPosition   = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox     = false;
            MinimizeBox     = false;
            ShowInTaskbar   = false;
            BackColor       = Form1.ClrBackground;
            ForeColor       = Form1.ClrText;
            Font            = new Font("Segoe UI", 9F);

            // ── Назва ────────────────────────────────────────────────────────────
            var lblName = MakeFieldLabel("Назва:", new Point(20, 18));
            _txtName = new TextBox
            {
                Location    = new Point(20, 42),
                Size        = new Size(400, 24),
                BackColor   = Form1.ClrCard,
                ForeColor   = Form1.ClrText,
                BorderStyle = BorderStyle.FixedSingle,
                Font        = new Font("Segoe UI", 10F),
                Text        = Task.Name,
            };

            // ── Пріоритет ────────────────────────────────────────────────────────
            var lblPriority = MakeFieldLabel("Пріоритет:", new Point(20, 80));
            _cmbPriority = MakeCombo(new Point(20, 104), 190);
            _cmbPriority.Items.AddRange(new object[] { "🔴 Високий", "🟡 Середній", "🟢 Низький" });
            _cmbPriority.SelectedItem = Task.Priority;
            if (_cmbPriority.SelectedIndex < 0) _cmbPriority.SelectedIndex = 1;

            // ── Статус ───────────────────────────────────────────────────────────
            var lblStatus = MakeFieldLabel("Статус:", new Point(230, 80));
            _cmbStatus = MakeCombo(new Point(230, 104), 190);
            _cmbStatus.Items.AddRange(new object[] { "Очікує", "В процесі", "Виконано", "Просрочено" });
            _cmbStatus.SelectedItem = Task.Status;
            if (_cmbStatus.SelectedIndex < 0) _cmbStatus.SelectedIndex = 0;

            // ── Дедлайн ──────────────────────────────────────────────────────────
            var lblDeadline = MakeFieldLabel("Дедлайн:", new Point(20, 142));
            _dtDeadline = new DateTimePicker
            {
                Location = new Point(20, 166),
                Size     = new Size(190, 24),
                Format   = DateTimePickerFormat.Short,
                Value    = Task.Deadline,
                Font     = new Font("Segoe UI", 10F),
            };

            // ── Кнопки OK / Скасувати ────────────────────────────────────────────
            var btnOk = new Button
            {
                Text      = "OK",
                Location  = new Point(240, 215),
                Size      = new Size(85, 32),
                BackColor = Form1.ClrGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor    = Cursors.Hand,
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += OnOkClick;

            var btnCancel = new Button
            {
                Text         = "Скасувати",
                Location     = new Point(335, 215),
                Size         = new Size(85, 32),
                BackColor    = Form1.ClrCard,
                ForeColor    = Form1.ClrText,
                FlatStyle    = FlatStyle.Flat,
                Font         = new Font("Segoe UI", 10F),
                Cursor       = Cursors.Hand,
                DialogResult = DialogResult.Cancel,
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[]
            {
                lblName,     _txtName,
                lblPriority, _cmbPriority,
                lblStatus,   _cmbStatus,
                lblDeadline, _dtDeadline,
                btnOk,       btnCancel,
            });
        }

        private static Label MakeFieldLabel(string text, Point loc) => new()
        {
            Text      = text,
            ForeColor = Form1.ClrSubText,
            Font      = new Font("Segoe UI", 9F, FontStyle.Bold),
            Location  = loc,
            AutoSize  = true,
            BackColor = Color.Transparent,
        };

        private static ComboBox MakeCombo(Point loc, int width) => new()
        {
            Location      = loc,
            Size          = new Size(width, 24),
            BackColor     = Form1.ClrCard,
            ForeColor     = Form1.ClrText,
            FlatStyle     = FlatStyle.Flat,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font          = new Font("Segoe UI", 10F),
        };

        private void OnOkClick(object? sender, EventArgs e)
        {
            var name = _txtName.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(this, "Введіть назву завдання.", "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtName.Focus();
                return;
            }

            Task.Name     = name;
            Task.Priority = _cmbPriority.SelectedItem?.ToString() ?? "🟡 Середній";
            Task.Status   = _cmbStatus.SelectedItem?.ToString()   ?? "Очікує";
            Task.Deadline = _dtDeadline.Value.Date;

            DialogResult = DialogResult.OK;
        }
    }
}
