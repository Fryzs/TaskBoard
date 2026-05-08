// Form1.Designer.cs — Повний Designer-код форми (TaskBoard Dashboard)
// Містить InitializeComponent: всі панелі, кнопки, мітки та їх налаштування.
// Цей файл зазвичай генерується Visual Studio Designer — тут написаний вручну.

namespace WinFormsApp10
{
    partial class Form1
    {
        // ─── Обов'язкова змінна Designer ────────────────────────────────────────
        private System.ComponentModel.IContainer components = null;

        // ─── Декларації полів (усі контроли форми) ──────────────────────────────

        // Структурні панелі
        private Panel panelHeader;       // Верхня шапка
        private Panel panelHdrLeft;      // Ліва частина шапки (логотип)
        private Panel panelHdrRight;     // Права частина шапки (годинник)
        private Panel panelSidebar;      // Ліва панель навігації
        private Panel panelSidebarNav;   // Область кнопок навігації
        private Panel panelSidebarFoot;  // Нижня частина сайдбару (версія)
        private Panel panelContent;      // Головна контентна область
        private Panel panelStatusBar;    // Нижній рядок стану

        // Шапка
        private Label lblAppIcon;        // Іконка-логотип "📊"
        private Label lblAppTitle;       // Назва "TaskBoard"
        private Label lblClock;          // Поточний час (оновлюється щосекунди)
        private Label lblDate;           // Поточна дата

        // Навігація (сайдбар)
        private Label  lblNavTitle;      // Підпис "НАВІГАЦІЯ"
        private Button btnNavHome;       // 🏠 Головна
        private Button btnNavTasks;      // 📋 Завдання
        private Button btnNavStats;      // 📊 Статистика
        private Button btnNavSettings;   // ⚙ Налаштування

        // Нижній рядок
        private Label lblStatus;         // Індикатор стану "● Готово"
        private Label lblUserInfo;       // Ім'я поточного користувача

        // ────────────────────────────────────────────────────────────────────────
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        // ════════════════════════════════════════════════════════════════════════
        //  InitializeComponent — єдина точка ініціалізації всього UI
        // ════════════════════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            panelHeader = new Panel();
            panelHdrRight = new Panel();
            lblClock = new Label();
            lblDate = new Label();
            panelHdrLeft = new Panel();
            lblAppIcon = new Label();
            lblAppTitle = new Label();
            panelSidebar = new Panel();
            panelSidebarNav = new Panel();
            lblNavTitle = new Label();
            btnNavHome = new Button();
            btnNavTasks = new Button();
            btnNavStats = new Button();
            btnNavSettings = new Button();
            panelSidebarFoot = new Panel();
            lblVer = new Label();
            panelContent = new Panel();
            panelStatusBar = new Panel();
            lblStatus = new Label();
            lblUserInfo = new Label();
            panelHeader.SuspendLayout();
            panelHdrRight.SuspendLayout();
            panelHdrLeft.SuspendLayout();
            panelSidebar.SuspendLayout();
            panelSidebarNav.SuspendLayout();
            panelSidebarFoot.SuspendLayout();
            panelStatusBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(panelHdrRight);
            panelHeader.Controls.Add(panelHdrLeft);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1050, 60);
            panelHeader.TabIndex = 3;
            // 
            // panelHdrRight
            // 
            panelHdrRight.Controls.Add(lblClock);
            panelHdrRight.Controls.Add(lblDate);
            panelHdrRight.Dock = DockStyle.Right;
            panelHdrRight.Location = new Point(840, 0);
            panelHdrRight.Name = "panelHdrRight";
            panelHdrRight.Padding = new Padding(0, 0, 16, 0);
            panelHdrRight.Size = new Size(210, 60);
            panelHdrRight.TabIndex = 0;
            panelHdrRight.Resize += PanelHdrRight_Resize;
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblClock.Location = new Point(56, 8);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(94, 28);
            lblClock.TabIndex = 0;
            lblClock.Text = "05:15:47";
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9F);
            lblDate.Location = new Point(56, 37);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(86, 15);
            lblDate.TabIndex = 1;
            lblDate.Text = "08 травня 2026";
            // 
            // panelHdrLeft
            // 
            panelHdrLeft.Controls.Add(lblAppIcon);
            panelHdrLeft.Controls.Add(lblAppTitle);
            panelHdrLeft.Dock = DockStyle.Left;
            panelHdrLeft.Location = new Point(0, 0);
            panelHdrLeft.Name = "panelHdrLeft";
            panelHdrLeft.Size = new Size(300, 60);
            panelHdrLeft.TabIndex = 1;
            // 
            // lblAppIcon
            // 
            lblAppIcon.Font = new Font("Segoe UI Emoji", 20F);
            lblAppIcon.Location = new Point(12, 7);
            lblAppIcon.Name = "lblAppIcon";
            lblAppIcon.Size = new Size(46, 46);
            lblAppIcon.TabIndex = 0;
            lblAppIcon.Text = "📊";
            lblAppIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAppTitle
            // 
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblAppTitle.Location = new Point(62, 16);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(124, 31);
            lblAppTitle.TabIndex = 1;
            lblAppTitle.Text = "TaskBoard";
            // 
            // panelSidebar
            // 
            panelSidebar.Controls.Add(panelSidebarNav);
            panelSidebar.Controls.Add(panelSidebarFoot);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 60);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(212, 570);
            panelSidebar.TabIndex = 1;
            // 
            // panelSidebarNav
            // 
            panelSidebarNav.Controls.Add(lblNavTitle);
            panelSidebarNav.Controls.Add(btnNavHome);
            panelSidebarNav.Controls.Add(btnNavTasks);
            panelSidebarNav.Controls.Add(btnNavStats);
            panelSidebarNav.Controls.Add(btnNavSettings);
            panelSidebarNav.Dock = DockStyle.Top;
            panelSidebarNav.Location = new Point(0, 0);
            panelSidebarNav.Name = "panelSidebarNav";
            panelSidebarNav.Size = new Size(212, 245);
            panelSidebarNav.TabIndex = 0;
            // 
            // lblNavTitle
            // 
            lblNavTitle.AutoSize = true;
            lblNavTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblNavTitle.Location = new Point(16, 14);
            lblNavTitle.Name = "lblNavTitle";
            lblNavTitle.Size = new Size(65, 13);
            lblNavTitle.TabIndex = 0;
            lblNavTitle.Text = "НАВІГАЦІЯ";
            // 
            // btnNavHome
            // 
            btnNavHome.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnNavHome.Cursor = Cursors.Hand;
            btnNavHome.FlatAppearance.BorderSize = 0;
            btnNavHome.FlatStyle = FlatStyle.Flat;
            btnNavHome.Font = new Font("Segoe UI", 10F);
            btnNavHome.Location = new Point(0, 38);
            btnNavHome.Name = "btnNavHome";
            btnNavHome.Padding = new Padding(14, 0, 0, 0);
            btnNavHome.Size = new Size(224, 44);
            btnNavHome.TabIndex = 1;
            btnNavHome.Text = "  🏠  Головна";
            btnNavHome.TextAlign = ContentAlignment.MiddleLeft;
            btnNavHome.Click += BtnNavHome_Click;
            // 
            // btnNavTasks
            // 
            btnNavTasks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnNavTasks.Cursor = Cursors.Hand;
            btnNavTasks.FlatAppearance.BorderSize = 0;
            btnNavTasks.FlatStyle = FlatStyle.Flat;
            btnNavTasks.Font = new Font("Segoe UI", 10F);
            btnNavTasks.Location = new Point(0, 88);
            btnNavTasks.Name = "btnNavTasks";
            btnNavTasks.Padding = new Padding(14, 0, 0, 0);
            btnNavTasks.Size = new Size(224, 44);
            btnNavTasks.TabIndex = 2;
            btnNavTasks.Text = "  📋  Завдання";
            btnNavTasks.TextAlign = ContentAlignment.MiddleLeft;
            btnNavTasks.Click += BtnNavTasks_Click;
            // 
            // btnNavStats
            // 
            btnNavStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnNavStats.Cursor = Cursors.Hand;
            btnNavStats.FlatAppearance.BorderSize = 0;
            btnNavStats.FlatStyle = FlatStyle.Flat;
            btnNavStats.Font = new Font("Segoe UI", 10F);
            btnNavStats.Location = new Point(0, 138);
            btnNavStats.Name = "btnNavStats";
            btnNavStats.Padding = new Padding(14, 0, 0, 0);
            btnNavStats.Size = new Size(224, 44);
            btnNavStats.TabIndex = 3;
            btnNavStats.Text = "  📊  Статистика";
            btnNavStats.TextAlign = ContentAlignment.MiddleLeft;
            btnNavStats.Click += BtnNavStats_Click;
            // 
            // btnNavSettings
            // 
            btnNavSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnNavSettings.Cursor = Cursors.Hand;
            btnNavSettings.FlatAppearance.BorderSize = 0;
            btnNavSettings.FlatStyle = FlatStyle.Flat;
            btnNavSettings.Font = new Font("Segoe UI", 10F);
            btnNavSettings.Location = new Point(0, 188);
            btnNavSettings.Name = "btnNavSettings";
            btnNavSettings.Padding = new Padding(14, 0, 0, 0);
            btnNavSettings.Size = new Size(224, 44);
            btnNavSettings.TabIndex = 4;
            btnNavSettings.Text = "  ⚙  Налаштування";
            btnNavSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnNavSettings.Click += BtnNavSettings_Click;
            // 
            // panelSidebarFoot
            // 
            panelSidebarFoot.Controls.Add(lblVer);
            panelSidebarFoot.Dock = DockStyle.Bottom;
            panelSidebarFoot.Location = new Point(0, 534);
            panelSidebarFoot.Name = "panelSidebarFoot";
            panelSidebarFoot.Size = new Size(212, 36);
            panelSidebarFoot.TabIndex = 1;
            // 
            // lblVer
            // 
            lblVer.Location = new Point(0, 0);
            lblVer.Name = "lblVer";
            lblVer.Size = new Size(100, 23);
            lblVer.TabIndex = 0;
            // 
            // panelContent
            // 
            panelContent.AutoScroll = true;
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(212, 60);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(8);
            panelContent.Size = new Size(838, 570);
            panelContent.TabIndex = 0;
            // 
            // panelStatusBar
            // 
            panelStatusBar.Controls.Add(lblStatus);
            panelStatusBar.Controls.Add(lblUserInfo);
            panelStatusBar.Dock = DockStyle.Bottom;
            panelStatusBar.Location = new Point(0, 630);
            panelStatusBar.Name = "panelStatusBar";
            panelStatusBar.Size = new Size(1050, 30);
            panelStatusBar.TabIndex = 2;
            panelStatusBar.Resize += PanelStatusBar_Resize;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.Location = new Point(14, 7);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(58, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "●  Готово";
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 9F);
            lblUserInfo.Location = new Point(700, 7);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(59, 15);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "👤  Admin";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 660);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Controls.Add(panelStatusBar);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(820, 560);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TaskBoard — Менеджер завдань";
            Load += Form1_Load;
            panelHeader.ResumeLayout(false);
            panelHdrRight.ResumeLayout(false);
            panelHdrRight.PerformLayout();
            panelHdrLeft.ResumeLayout(false);
            panelHdrLeft.PerformLayout();
            panelSidebar.ResumeLayout(false);
            panelSidebarNav.ResumeLayout(false);
            panelSidebarNav.PerformLayout();
            panelSidebarFoot.ResumeLayout(false);
            panelStatusBar.ResumeLayout(false);
            panelStatusBar.PerformLayout();
            ResumeLayout(false);
        }
        private Label lblVer;
    }
}
