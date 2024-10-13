using System;
using System.Diagnostics;
using System.Timers;
using Microsoft.Maui.Controls;

namespace RMP6;

public partial class NewPage2 : ContentPage
{
    private System.Timers.Timer _timer;
    private DateTime _startTime;
    private bool _isRunning;
    public NewPage2()
	{
		InitializeComponent();
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimerElapsed;
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var elapsedTime = DateTime.Now - _startTime;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TimerLabel.Text = elapsedTime.ToString(@"hh\:mm\:ss");
        });
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        if (!_isRunning)
        {
            _startTime = DateTime.Now;
            _timer.Start();
            _isRunning = true;
        }
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            _timer.Stop();
            _isRunning = false;
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        _timer.Stop();
        _isRunning = false;
        TimerLabel.Text = "00:00:00";
    }
}