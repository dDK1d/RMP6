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
        _timer = new System.Timers.Timer(1000); // �������� ������ �������
        _timer.Elapsed += OnTimerElapsed;
        _timer.AutoReset = true;
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (_isAlarmEnabled && DateTime.Now.TimeOfDay >= _alarmTime)
        {
            _isAlarmEnabled = false; // ��������� ��������� ����� ������������
            _timer.Stop(); // ������������� ������
            MainThread.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("���������", "����� �����������!", "OK");
            });
        }
    }

    private void OnEnableAlarmClicked(object sender, EventArgs e)
    {
        _alarmTime = AlarmTimePicker.Time;
        var selectedDays = GetSelectedDays();
        _isAlarmEnabled = true; // �������� ���������

        // ����� ����� �������� ��� ��� ��������� ���������� �� ��������� ���
        DisplayAlert("��������� �������", $"�����: {_alarmTime}\n���: {string.Join(", ", selectedDays)}", "OK");

        _timer.Start(); // ��������� ������
    }

    private void OnDisableAlarmClicked(object sender, EventArgs e)
    {
        _isAlarmEnabled = false; // ��������� ���������
        _timer.Stop(); // ������������� ������
        DisplayAlert("��������� ��������", "��� ��������� �������������.", "OK");
    }

    private List<string> GetSelectedDays()
    {
        var selectedDays = new List<string>();

        if (CheckBoxMon.IsChecked) selectedDays.Add("��");
        if (CheckBoxTue.IsChecked) selectedDays.Add("��");
        if (CheckBoxWed.IsChecked) selectedDays.Add("��");
        if (CheckBoxThu.IsChecked) selectedDays.Add("��");
        if (CheckBoxFri.IsChecked) selectedDays.Add("��");
        if (CheckBoxSat.IsChecked) selectedDays.Add("��");
        if (CheckBoxSun.IsChecked) selectedDays.Add("��");

        return selectedDays;
    }
}
