using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Maui.Controls;


namespace RMP6;

public partial class NewPage1 : ContentPage
{
    private System.Timers.Timer _timer;
    private TimeSpan _alarmTime;
    private bool _isAlarmEnabled;

    public NewPage1()
	{
		InitializeComponent();
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        _timer = new System.Timers.Timer(1000); // Проверка каждую секунду
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (_isAlarmEnabled && DateTime.Now.TimeOfDay >= _alarmTime)
        {
            _isAlarmEnabled = false; // Отключаем будильник после срабатывания
            _timer.Stop(); // Останавливаем таймер
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Будильник", "Время просыпаться!", "OK");
            });
        }
    }

    private void OnEnableAlarmClicked(object sender, EventArgs e)
    {
        _alarmTime = AlarmTimePicker.Time;
        var selectedDays = GetSelectedDays();
        _isAlarmEnabled = true; // Включаем будильник

        // Здесь можно добавить код для установки будильника на выбранные дни
        DisplayAlert("Будильник включен", $"Время: {_alarmTime}\nДни: {string.Join(", ", selectedDays)}", "OK");

        _timer.Start(); // Запускаем таймер
    }

    private void OnDisableAlarmClicked(object sender, EventArgs e)
    {
        _isAlarmEnabled = false; // Отключаем будильник
        _timer.Stop(); // Останавливаем таймер
        DisplayAlert("Будильник выключен", "Ваш будильник деактивирован.", "OK");
    }

    private List<string> GetSelectedDays()
    {
        var selectedDays = new List<string>();

        if (CheckBoxMon.IsChecked) selectedDays.Add("Пн");
        if (CheckBoxTue.IsChecked) selectedDays.Add("Вт");
        if (CheckBoxWed.IsChecked) selectedDays.Add("Ср");
        if (CheckBoxThu.IsChecked) selectedDays.Add("Чт");
        if (CheckBoxFri.IsChecked) selectedDays.Add("Пт");
        if (CheckBoxSat.IsChecked) selectedDays.Add("Сб");
        if (CheckBoxSun.IsChecked) selectedDays.Add("Вс");

        return selectedDays;
    }
}
