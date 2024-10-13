using System;
using System.Timers;
using Microsoft.Maui.Controls;
using Timer = System.Timers.Timer;
namespace RMP6
{
    public partial class MainPage : ContentPage
    {
        private Timer timer;
        public MainPage()
        {
            InitializeComponent();
            UpdateTime();
            StartTimer();
        }

        private void StartTimer()
        {
            timer = new Timer(1000); // 1 секунда
            timer.Elapsed += (s, e) => UpdateTime();
            timer.Start();
        }

        private void UpdateTime()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DateLabel.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy"); // Отображение даты
                ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss"); // Отображение времени
            });
        }

        private void OnUpdateTimeClicked(object sender, EventArgs e)
        {
            UpdateTime();
        }
    }

}
