// TaskItem.cs — Модель одного завдання TaskBoard.

namespace WinFormsApp10
{
    public class TaskItem
    {
        public string   Name     { get; set; } = "";
        public string   Priority { get; set; } = "🟡 Середній";
        public string   Status   { get; set; } = "Очікує";
        public DateTime Deadline { get; set; } = DateTime.Today.AddDays(7);

        // Іконка автоматично визначається за статусом — щоб список був консистентним.
        public string Icon => Status switch
        {
            "Виконано"   => "🟢",
            "Просрочено" => "🔴",
            "В процесі"  => "🔵",
            _            => "🟡",
        };
    }
}
