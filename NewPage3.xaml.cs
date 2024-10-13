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
        // ������������� ������� �����
        targetTime = DateTime.Now.Add(timePicker.Time);
        remainingTime = timePicker.Time; // ��������� �������������� ����� ��� �������
        timer.Start();

        // ���������� ������������ � ������� �������
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await DisplayAlert("��������", $"������ ������� �� {timePicker.Time.Hours:D2}:{timePicker.Time.Minutes:D2}:{remainingTime.Seconds:D2}", "��");
        });
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        remainingTime = targetTime - DateTime.Now;

        if (remainingTime.TotalSeconds <= 0)
        {
            timer.Stop();

            // ���������� ������������ � ���������� �������
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("��������", "����� �����!", "��");
            });
        }
        else
        {
            // ��������� ���������� ����� � ���������� (��������, � ������� Label)
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // ����� ������ ���� ���������� UI, ��������:
                timeLabel.Text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
            });
        }
    }

    async private void Button_Clicked_1(object sender, EventArgs e)
    {
        // ������ ��� ��������� �������
        timer.Stop();
        await DisplayAlert("��������", "������ ����������.", "��");

    }


}