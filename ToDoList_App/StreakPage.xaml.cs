using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList_App
{
    public partial class StreakPage : Page
    {
        private int StreakCount = 0;
        private List<string> Achievements = new List<string>();

        public StreakPage()
        {
            InitializeComponent();
            CalculateStreak();
            LoadAchievements();
        }

        private void CalculateStreak()
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                var completedTasks = main.TaskList.Where(t => t.IsChecked).ToList();
                if (completedTasks.Count > 0)
                {
                    DateTime lastDate = DateTime.Parse(completedTasks.Last().Deadline);
                    if (lastDate == DateTime.Today)
                    {
                        StreakCount++;
                    }
                }
            }
            StreakCountText.Text = $"🔥 {StreakCount} Day Streak";
        }

        private void LoadAchievements()
        {
            Achievements.Clear();
            if (StreakCount >= 5) Achievements.Add("🌟 5-Day Streak!");
            if (StreakCount >= 10) Achievements.Add("🏅 10-Day Streak!");
            if (StreakCount >= 20) Achievements.Add("🎖️ 20-Day Streak!");
            if (StreakCount >= 30) Achievements.Add("🌠 30-Day Streak!");

            if (Achievements.Count == 0)
            {
                AchievementsList.Items.Add("No achievements yet. Keep going!");
            }
            else
            {
                foreach (var achievement in Achievements)
                {
                    AchievementsList.Items.Add(achievement);
                }
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            log_in loginWindow = new log_in();
            loginWindow.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
