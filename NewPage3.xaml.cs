using System.Timers;
using Timer = System.Timers.Timer;


namespace RMP6;

public partial class NewPage3 : ContentPage
{
    private Timer timer;
    private DateTime targetTime;
    private TimeSpan remainingTime;

    public NewPage3()
    {
        InitializeComponent();
        timer = new Timer(1000);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
    }

    async private void Button_Clicked(object sender, EventArgs e)
    {
        // Устанавливаем целевое время
        targetTime = DateTime.Now.Add(timePicker.Time);
        remainingTime = timePicker.Time; // Сохраняем первоначальное время для отсчета
        timer.Start();

        // Уведомляем пользователя о запуске таймера
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await DisplayAlert("Внимание", $"Таймер запущен на {timePicker.Time.Hours:D2}:{timePicker.Time.Minutes:D2}:{remainingTime.Seconds:D2}", "Ок");
        });
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        remainingTime = targetTime - DateTime.Now;

        if (remainingTime.TotalSeconds <= 0)
        {
            timer.Stop();

            // Уведомляем пользователя о завершении таймера
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Внимание", "Время вышло!", "Ок");
            });
        }
        else
        {
            // Обновляем оставшееся время в интерфейсе (например, с помощью Label)
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Здесь должно быть обновление UI, например:
                timeLabel.Text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
            });
        }
    }

    async private void Button_Clicked_1(object sender, EventArgs e)
    {
        // Логика для остановки таймера
        timer.Stop();
        await DisplayAlert("Внимание", "Таймер остановлен.", "Ок");

    }


}