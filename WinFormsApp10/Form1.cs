// Form1.cs — Головна логіка форми (TaskBoard Dashboard)
// Відповідає за обробку подій, навігацію та динамічне наповнення контентної зони.

using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormsApp10
{
    public partial class Form1 : Form
    {
        // ─── Поточна активна кнопка навігації ───────────────────────────────────
        private Button? _activeNavButton;

        // ─── Список завдань і поточно виділене завдання ─────────────────────────
        private readonly List<TaskItem> _tasks = new()
        {
            new() { Name = "Підготувати звіт Q2",     Priority = "🔴 Високий",  Status = "В процесі",  Deadline = new DateTime(2026, 5, 15) },
            new() { Name = "Оновити документацію",    Priority = "🟡 Середній", Status = "Виконано",   Deadline = new DateTime(2026, 5, 10) },
            new() { Name = "Виправити помилку #42",   Priority = "🔴 Високий",  Status = "Просрочено", Deadline = new DateTime(2026, 5, 1)  },
            new() { Name = "Провести код-рев'ю",      Priority = "🟢 Низький",  Status = "В процесі",  Deadline = new DateTime(2026, 5, 20) },
            new() { Name = "Написати unit-тести",     Priority = "🟡 Середній", Status = "Виконано",   Deadline = new DateTime(2026, 5, 8)  },
            new() { Name = "Підготувати презентацію", Priority = "🔴 Високий",  Status = "Очікує",     Deadline = new DateTime(2026, 5, 25) },
            new() { Name = "Аналіз конкурентів",      Priority = "🟢 Низький",  Status = "Очікує",     Deadline = new DateTime(2026, 5, 30) },
        };
        private TaskItem? _selectedTask;
        private Panel? _selectedTaskRow;

        // ─── Кольорова палітра (темна тема) ─────────────────────────────────────
        internal static readonly Color ClrBackground = Color.FromArgb(18, 18, 30);   // Найтемніший фон
        internal static readonly Color ClrSurface = Color.FromArgb(30, 30, 46);   // Фон панелей / сайдбару
        internal static readonly Color ClrCard = Color.FromArgb(49, 50, 68);   // Фон карток
        internal static readonly Color ClrCardAlt = Color.FromArgb(55, 56, 74);   // Альтернативний рядок
        internal static readonly Color ClrText = Color.White;
        internal static readonly Color ClrSubText = Color.FromArgb(148, 163, 184);  // Другорядний текст
        internal static readonly Color ClrBlue = Color.FromArgb(59, 130, 246);  // Синій акцент
        internal static readonly Color ClrGreen = Color.FromArgb(34, 197, 94);   // Зелений акцент
        internal static readonly Color ClrRed = Color.FromArgb(239, 68, 68);   // Червоний акцент
        internal static readonly Color ClrOrange = Color.FromArgb(249, 115, 22);   // Помаранчевий акцент
        internal static readonly Color ClrPurple = Color.FromArgb(168, 85, 247);  // Фіолетовий акцент
        internal static readonly Color ClrNavActive = Color.FromArgb(59, 130, 246);  // Активна навіг. кнопка
        internal static readonly Color ClrNavHover = Color.FromArgb(40, 50, 68);   // Hover навіг. кнопки

        // ────────────────────────────────────────────────────────────────────────
        public Form1()
        {
            InitializeComponent();

            // Таймер для годинника в шапці
            var clock = new System.Windows.Forms.Timer { Interval = 1000 };
            clock.Tick += (_, _) => lblClock.Text = DateTime.Now.ToString("HH:mm:ss");
            clock.Start();

            // Початкова сторінка
            SelectNav(btnNavHome);
            BuildHome();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  НАВІГАЦІЯ
        // ═══════════════════════════════════════════════════════════════════════

        /// <summary>Виділяє кнопку навігації та знімає виділення з попередньої.</summary>
        private void SelectNav(Button btn)
        {
            if (_activeNavButton != null)
            {
                _activeNavButton.BackColor = ClrSurface;
                _activeNavButton.ForeColor = ClrSubText;
            }
            _activeNavButton = btn;
            btn.BackColor = ClrNavActive;
            btn.ForeColor = ClrText;
        }

        // Обробники кліків на навігаційні кнопки
        private void BtnNavHome_Click(object sender, EventArgs e) { SelectNav(btnNavHome); BuildHome(); }
        private void BtnNavTasks_Click(object sender, EventArgs e) { SelectNav(btnNavTasks); BuildTasks(); }
        private void BtnNavStats_Click(object sender, EventArgs e) { SelectNav(btnNavStats); BuildStats(); }
        private void BtnNavSettings_Click(object sender, EventArgs e) { SelectNav(btnNavSettings); BuildSettings(); }

        // Обробники зміни розміру панелей шапки та статус-бару
        // (Винесені з InitializeComponent, щоб Designer міг розпарсити файл.)
        private void PanelHdrRight_Resize(object sender, EventArgs e)
        {
            const int margin = 16;
            lblClock.Left = panelHdrRight.Width - lblClock.Width - margin;
            lblClock.Top = 8;
            lblDate.Left = panelHdrRight.Width - lblDate.Width - margin;
            lblDate.Top = 38;
        }

        private void PanelStatusBar_Resize(object sender, EventArgs e)
        {
            lblUserInfo.Location = new Point(panelStatusBar.Width - lblUserInfo.Width - 14, 7);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  СТОРІНКА: ГОЛОВНА
        // ═══════════════════════════════════════════════════════════════════════
        private void BuildHome()
        {
            ClearContent();

            AddLabel("Головна панель", ClrText, new Font("Segoe UI", 18, FontStyle.Bold), new Point(20, 20));
            AddLabel("Ласкаво просимо! Ось ваш поточний огляд.", ClrSubText, new Font("Segoe UI", 10), new Point(20, 55));

            // ── Статистичні картки ──
            var cards = new (string Title, string Value, Color Accent)[]
            {
                ("📋  Всього завдань",  "12", ClrBlue),
                ("✅  Виконано",        "8",  ClrGreen),
                ("⏳  В процесі",       "3",  ClrOrange),
                ("❌  Просрочено",      "1",  ClrRed),
            };

            int cx = 20, cy = 95, cw = 160, ch = 95, gap = 14;
            foreach (var c in cards)
            {
                panelContent.Controls.Add(MakeStatCard(c.Title, c.Value, c.Accent,
                    new Point(cx, cy), new Size(cw, ch)));
                cx += cw + gap;
            }

            // ── Остання активність ──
            AddLabel("Остання активність", ClrText, new Font("Segoe UI", 12, FontStyle.Bold),
                new Point(20, cy + ch + 24));

            string[] news =
            {
                "✔  Завдання «Звіт Q1» — виконано",
                "➕  Додано нове завдання «Презентація»",
                "✏  Оновлено дедлайн «Аналіз даних»",
                "🔔  Нагадування: «Зустріч з командою» о 15:00",
                "🔵  Новий учасник доданий до проєкту",
            };
            int ny = cy + ch + 56;
            foreach (var n in news)
            {
                AddLabel(n, ClrSubText, new Font("Segoe UI", 10), new Point(28, ny));
                ny += 27;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  СТОРІНКА: ЗАВДАННЯ
        // ═══════════════════════════════════════════════════════════════════════
        private void BuildTasks()
        {
            ClearContent();

            AddLabel("Мої завдання", ClrText, new Font("Segoe UI", 18, FontStyle.Bold), new Point(20, 20));

            // Панель інструментів
            var btnAdd = MakeButton("➕  Додати", ClrGreen, new Point(20, 65), new Size(130, 36));
            var btnEdit = MakeButton("✏  Редагувати", ClrOrange, new Point(160, 65), new Size(140, 36));
            var btnDel = MakeButton("🗑  Видалити", ClrRed, new Point(310, 65), new Size(130, 36));

            btnAdd.Click += OnAddTask;
            btnEdit.Click += OnEditTask;
            btnDel.Click += OnDeleteTask;

            panelContent.Controls.Add(btnAdd);
            panelContent.Controls.Add(btnEdit);
            panelContent.Controls.Add(btnDel);

            // Підказка щодо виділення
            AddLabel("Натисніть на рядок щоб виділити завдання для редагування або видалення.",
                ClrSubText, new Font("Segoe UI", 8.5F, FontStyle.Italic), new Point(460, 75));

            // Список завдань
            var list = new Panel
            {
                BackColor = ClrCard,
                Location = new Point(20, 115),
                Size = new Size(panelContent.ClientSize.Width - 60, panelContent.ClientSize.Height - 130),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
            };
            panelContent.Controls.Add(list);

            // Заголовок таблиці
            var hdr = new Panel { BackColor = ClrBackground, Location = new Point(0, 0), Size = new Size(list.Width, 34) };
            hdr.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AddListHeader(hdr, "Назва завдання", 40, 240);
            AddListHeader(hdr, "Пріоритет", 290, 90);
            AddListHeader(hdr, "Статус", 390, 110);
            AddListHeader(hdr, "Дедлайн", 510, 110);
            list.Controls.Add(hdr);

            var statusColors = new Dictionary<string, Color>
            {
                ["В процесі"] = ClrBlue,
                ["Виконано"] = ClrGreen,
                ["Просрочено"] = ClrRed,
                ["Очікує"] = ClrOrange,
            };

            // Скидаємо посилання на рядок — буде встановлено заново, якщо знайдемо виділення.
            _selectedTaskRow = null;

            // Якщо завдань немає
            if (_tasks.Count == 0)
            {
                list.Controls.Add(new Label
                {
                    Text = "Немає завдань. Натисніть «Додати», щоб створити нове.",
                    ForeColor = ClrSubText,
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    Location = new Point(20, 50),
                    AutoSize = true,
                });
                return;
            }

            int ry = 36;
            bool alt = false;
            foreach (var task in _tasks)
            {
                var origColor = alt ? ClrCardAlt : ClrCard;
                var row = new Panel
                {
                    BackColor = origColor,
                    Location = new Point(0, ry),
                    Size = new Size(list.Width, 36),
                    Tag = origColor, // зберігаємо для відновлення після зняття виділення
                    Cursor = Cursors.Hand,
                };
                row.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                var icoL = new Label { Text = task.Icon, Location = new Point(10, 8), Size = new Size(24, 20), ForeColor = ClrText, Font = new Font("Segoe UI Emoji", 10F) };
                var nameL = new Label { Text = task.Name, Location = new Point(40, 9), Size = new Size(240, 18), ForeColor = ClrText, Font = new Font("Segoe UI", 10F) };
                var priL = new Label { Text = task.Priority, Location = new Point(290, 9), Size = new Size(90, 18), ForeColor = ClrSubText, Font = new Font("Segoe UI", 9F) };
                var stL = new Label
                {
                    Text = task.Status,
                    Location = new Point(390, 9),
                    Size = new Size(110, 18),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = statusColors.TryGetValue(task.Status, out var sc) ? sc : ClrSubText
                };
                var dlL = new Label { Text = task.Deadline.ToString("yyyy-MM-dd"), Location = new Point(510, 9), Size = new Size(110, 18), ForeColor = ClrSubText, Font = new Font("Segoe UI", 9F) };

                foreach (var lbl in new Control[] { icoL, nameL, priL, stL, dlL })
                    lbl.Cursor = Cursors.Hand;

                row.Controls.AddRange(new Control[] { icoL, nameL, priL, stL, dlL });
                AttachRowClick(row, task);

                // Підсвітити поточно виділене завдання
                if (ReferenceEquals(_selectedTask, task))
                {
                    row.BackColor = ClrNavHover;
                    _selectedTaskRow = row;
                }

                list.Controls.Add(row);

                ry += 38;
                alt = !alt;
            }

            // Якщо виділеного завдання вже немає в списку — скидаємо
            if (_selectedTaskRow == null) _selectedTask = null;
        }

        // ───── Виділення / редагування / додавання / видалення завдань ──────────

        private void AttachRowClick(Panel row, TaskItem task)
        {
            void OnClick(object? s, EventArgs e) => SelectTask(task, row);
            row.Click += OnClick;
            foreach (Control c in row.Controls)
                c.Click += OnClick;
        }

        private void SelectTask(TaskItem task, Panel row)
        {
            // Відновити фон попередньо виділеного рядка
            if (_selectedTaskRow != null && _selectedTaskRow.Tag is Color origColor)
                _selectedTaskRow.BackColor = origColor;

            _selectedTask = task;
            _selectedTaskRow = row;
            row.BackColor = ClrNavHover;
        }

        private void OnAddTask(object? sender, EventArgs e)
        {
            using var dlg = new TaskEditDialog(null);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            _tasks.Add(dlg.Task);
            _selectedTask = dlg.Task; // одразу виділяємо новостворене
            BuildTasks();
        }

        private void OnEditTask(object? sender, EventArgs e)
        {
            if (_selectedTask == null)
            {
                MessageBox.Show(this, "Спочатку виберіть завдання у списку.", "Завдання не вибрано",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new TaskEditDialog(_selectedTask);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            // Переносимо зміни з робочої копії у вихідне завдання
            _selectedTask.Name = dlg.Task.Name;
            _selectedTask.Priority = dlg.Task.Priority;
            _selectedTask.Status = dlg.Task.Status;
            _selectedTask.Deadline = dlg.Task.Deadline;

            BuildTasks();
        }

        private void OnDeleteTask(object? sender, EventArgs e)
        {
            if (_selectedTask == null)
            {
                MessageBox.Show(this, "Спочатку виберіть завдання у списку.", "Завдання не вибрано",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this,
                $"Видалити завдання «{_selectedTask.Name}»?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            _tasks.Remove(_selectedTask);
            _selectedTask = null;
            _selectedTaskRow = null;
            BuildTasks();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  СТОРІНКА: СТАТИСТИКА
        // ═══════════════════════════════════════════════════════════════════════
        private void BuildStats()
        {
            ClearContent();

            AddLabel("Статистика", ClrText, new Font("Segoe UI", 18, FontStyle.Bold), new Point(20, 20));
            AddLabel("Виконання завдань по тижнях", ClrSubText, new Font("Segoe UI", 10), new Point(20, 58));

            // ── Стовпчаста діаграма ──
            var chartData = new (string Week, float Ratio, Color Color)[]
            {
                ("Тиж 1", 0.60f, ClrBlue),
                ("Тиж 2", 0.85f, ClrGreen),
                ("Тиж 3", 0.45f, ClrOrange),
                ("Тиж 4", 0.92f, ClrPurple),
                ("Тиж 5", 0.70f, ClrBlue),
            };

            const int maxH = 140, barW = 56, barGap = 32, baseY = 255, startX = 40;
            int bx = startX;

            foreach (var (week, ratio, color) in chartData)
            {
                int h = (int)(ratio * maxH);
                int y = baseY - h;

                // Стовпець
                var bar = new Panel { BackColor = color, Location = new Point(bx, y), Size = new Size(barW, h) };
                panelContent.Controls.Add(bar);

                // Відсоток над стовпцем
                AddLabel($"{(int)(ratio * 100)}%", color, new Font("Segoe UI", 8F, FontStyle.Bold),
                    new Point(bx + 8, y - 20));

                // Підпис під стовпцем
                AddLabel(week, ClrSubText, new Font("Segoe UI", 9F), new Point(bx + 4, baseY + 8));

                bx += barW + barGap;
            }

            // Базова лінія діаграми
            panelContent.Controls.Add(new Panel
            {
                BackColor = ClrCard,
                Location = new Point(startX, baseY),
                Size = new Size(bx - barGap - startX + barW, 2),
            });

            // ── Зведена таблиця ──
            AddLabel("Зведена таблиця", ClrText, new Font("Segoe UI", 12, FontStyle.Bold),
                new Point(20, baseY + 50));

            var rows = new (string Key, string Val, Color Color)[]
            {
                ("Всього завдань",            "12",         ClrText),
                ("Виконано",                  "8  (67 %)",  ClrGreen),
                ("В процесі",                 "3  (25 %)",  ClrBlue),
                ("Просрочено",                "1  (8 %)",   ClrRed),
                ("Середній час виконання",    "3.2 дні",    ClrOrange),
                ("Найпродуктивніший день",    "Середа",     ClrPurple),
            };
            int sy = baseY + 86;
            foreach (var (k, v, c) in rows)
            {
                AddLabel(k + ":", ClrSubText, new Font("Segoe UI", 10F), new Point(30, sy));
                AddLabel(v, c, new Font("Segoe UI", 10F, FontStyle.Bold), new Point(260, sy));
                sy += 26;
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  СТОРІНКА: НАЛАШТУВАННЯ
        // ═══════════════════════════════════════════════════════════════════════
        private void BuildSettings()
        {
            ClearContent();

            AddLabel("Налаштування", ClrText, new Font("Segoe UI", 18, FontStyle.Bold), new Point(20, 20));

            // ── Тема ──
            AddLabel("Тема оформлення", ClrSubText, new Font("Segoe UI", 10F, FontStyle.Bold), new Point(20, 66));
            panelContent.Controls.Add(MakeRadioButton("🌙  Темна тема", new Point(30, 92), true));
            panelContent.Controls.Add(MakeRadioButton("☀  Світла тема", new Point(30, 118), false));

            // ── Сповіщення ──
            AddLabel("Сповіщення", ClrSubText, new Font("Segoe UI", 10F, FontStyle.Bold), new Point(20, 158));
            panelContent.Controls.Add(MakeCheckBox("📧  Email сповіщення", new Point(30, 183), true));
            panelContent.Controls.Add(MakeCheckBox("🔔  Push сповіщення", new Point(30, 209), true));
            panelContent.Controls.Add(MakeCheckBox("🔊  Звукові сповіщення", new Point(30, 235), false));

            // ── Мова ──
            AddLabel("Мова інтерфейсу", ClrSubText, new Font("Segoe UI", 10F, FontStyle.Bold), new Point(20, 275));
            var cmb = new ComboBox
            {
                Location = new Point(30, 300),
                Size = new Size(200, 28),
                BackColor = ClrCard,
                ForeColor = ClrText,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
            };
            cmb.Items.AddRange(new object[] { "Українська", "English", "Deutsch", "Français" });
            cmb.SelectedIndex = 0;
            panelContent.Controls.Add(cmb);

            // ── Профіль ──
            AddLabel("Ім'я користувача", ClrSubText, new Font("Segoe UI", 10F, FontStyle.Bold), new Point(20, 346));
            var txt = new TextBox
            {
                Location = new Point(30, 370),
                Size = new Size(220, 28),
                BackColor = ClrCard,
                ForeColor = ClrText,
                Font = new Font("Segoe UI", 10F),
                Text = "Admin",
                BorderStyle = BorderStyle.FixedSingle,
            };
            panelContent.Controls.Add(txt);

            // ── Кнопки ──
            var btnSave = MakeButton("💾  Зберегти", ClrGreen, new Point(30, 420), new Size(160, 40));
            var btnReset = MakeButton("↩  Скинути", ClrRed, new Point(200, 420), new Size(130, 40));
            var btnAbout = MakeButton("ℹ  Про програму", ClrPurple, new Point(340, 420), new Size(160, 40));

            btnSave.Click += (_, _) => MessageBox.Show("Налаштування збережено!", "Збережено",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReset.Click += (_, _) =>
            {
                if (MessageBox.Show("Скинути всі налаштування до стандартних?", "Підтвердження",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    MessageBox.Show("Налаштування скинуто.", "Скинуто", MessageBoxButtons.OK);
            };

            btnAbout.Click += (_, _) =>
                MessageBox.Show("TaskBoard v1.0.0\n\nРозроблено: 2026\nПлатформа: .NET 8 Windows Forms\nТема: Темна",
                    "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);

            panelContent.Controls.Add(btnSave);
            panelContent.Controls.Add(btnReset);
            panelContent.Controls.Add(btnAbout);
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  ФАБРИЧНІ МЕТОДИ — допоміжні утиліти для створення елементів UI
        // ═══════════════════════════════════════════════════════════════════════

        /// <summary>Очищає контентну панель перед відображенням нової сторінки.</summary>
        private void ClearContent()
        {
            // Звільняємо ресурси дочірніх контролів перед очищенням
            foreach (Control c in panelContent.Controls)
                c.Dispose();
            panelContent.Controls.Clear();
        }

        /// <summary>Додає Label до контентної панелі.</summary>
        private void AddLabel(string text, Color color, Font font, Point location)
        {
            panelContent.Controls.Add(new Label
            {
                Text = text,
                ForeColor = color,
                Font = font,
                AutoSize = true,
                Location = location,
                BackColor = Color.Transparent,
            });
        }

        /// <summary>Додає заголовковий Label до рядка списку.</summary>
        private static void AddListHeader(Panel parent, string text, int x, int width)
        {
            parent.Controls.Add(new Label
            {
                Text = text,
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Location = new Point(x, 8),
                Size = new Size(width, 18),
            });
        }

        /// <summary>Створює стилізовану кнопку з кольоровим фоном та ефектом наведення.</summary>
        private static Button MakeButton(string text, Color back, Point loc, Size size)
        {
            // Ефект наведення: трохи світліший відтінок базового кольору
            var hover = Color.FromArgb(
                Math.Min(255, back.R + 28),
                Math.Min(255, back.G + 28),
                Math.Min(255, back.B + 28));

            var btn = new Button
            {
                Text = text,
                BackColor = back,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Location = loc,
                Size = size,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(
                Math.Max(0, back.R - 20),
                Math.Max(0, back.G - 20),
                Math.Max(0, back.B - 20));
            return btn;
        }

        /// <summary>Створює картку статистики з кольоровою бічною смугою.</summary>
        private static Panel MakeStatCard(string title, string value, Color accent,
                                          Point loc, Size size)
        {
            var card = new Panel { BackColor = ClrCard, Location = loc, Size = size };

            // Кольорова вертикальна смуга зліва
            card.Controls.Add(new Panel
            {
                BackColor = accent,
                Location = new Point(0, 0),
                Size = new Size(4, size.Height),
            });

            // Числове значення
            card.Controls.Add(new Label
            {
                Text = value,
                ForeColor = accent,
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(14, 8),
                Size = new Size(size.Width - 18, 40),
                BackColor = Color.Transparent,
            });

            // Підпис
            card.Controls.Add(new Label
            {
                Text = title,
                ForeColor = ClrSubText,
                Font = new Font("Segoe UI", 8.5F),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(14, 52),
                Size = new Size(size.Width - 18, 30),
                BackColor = Color.Transparent,
            });

            return card;
        }

        /// <summary>Створює стилізований RadioButton під темну тему.</summary>
        private static RadioButton MakeRadioButton(string text, Point loc, bool isChecked) =>
            new RadioButton
            {
                Text = text,
                ForeColor = ClrText,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.Transparent,
                Location = loc,
                AutoSize = true,
                Checked = isChecked,
            };

        /// <summary>Створює стилізований CheckBox під темну тему.</summary>
        private static CheckBox MakeCheckBox(string text, Point loc, bool isChecked) =>
            new CheckBox
            {
                Text = text,
                ForeColor = ClrText,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.Transparent,
                Location = loc,
                AutoSize = true,
                Checked = isChecked,
            };

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
