using TMPro;
using UnityEngine;
using System;

public class ClockIndicator : PercentIndicator
{
    [SerializeField, Range(0, 23)] private int shiftStart;
    [SerializeField, Range(0, 23)] private int shiftEnd;
    [SerializeField] private TMP_Text text;

    private void OnValidate()
    {
        if (shiftStart > shiftEnd)
        {
            shiftEnd = shiftStart;
        }
    }

    public override void SetValue(float v)
    {
        text.text = GetCurrentTime(shiftStart, shiftEnd, v);
    }

    private string GetCurrentTime(int shiftStartHour, int shiftEndHour, float completionPercentage)
    {
        // �������� ���������� ������� ������
        if (shiftStartHour < 0 || shiftStartHour > 23)
            throw new ArgumentException("��� ������ ����� ������ ���� �� 0 �� 23");

        if (shiftEndHour < 0 || shiftEndHour > 23)
            throw new ArgumentException("��� ����� ����� ������ ���� �� 0 �� 23");

        if (completionPercentage < 0 || completionPercentage > 1)
            throw new ArgumentException("������� ���������� ������ ���� � ��������� �� 0 �� 1");

        // �������� TimeSpan ��� ������ � ����� ����� (������ = 0)
        TimeSpan startTime = new TimeSpan(shiftStartHour, 0, 0);
        TimeSpan endTime = new TimeSpan(shiftEndHour, 0, 0);

        // ���� ����� ����� ������ ��� ����� ������ (������ �����), ��������� 24 ����
        if (endTime <= startTime)
        {
            endTime = endTime.Add(TimeSpan.FromHours(24));
        }

        // ���������� ����� ����������������� �����
        TimeSpan totalDuration = endTime - startTime;

        // ���������� ���������� ������� �� ������ �������� ����������
        TimeSpan elapsedTime = TimeSpan.FromTicks((long)(totalDuration.Ticks * completionPercentage));

        // ���������� �������� �������
        TimeSpan currentTime = startTime + elapsedTime;

        // ���� ������� ����� ��������� 24 ����, ����������� ���
        if (currentTime.Days > 0)
        {
            currentTime = currentTime.Subtract(TimeSpan.FromDays(currentTime.Days));
        }

        // �������������� ���������� � ������ hh:mm
        return FormatTime(currentTime);
    }

    private static string FormatTime(TimeSpan time)
    {
        return $"{(int)time.TotalHours:00}:{time.Minutes:00}";
    }
}
