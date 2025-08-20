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
        // Проверка валидности входных данных
        if (shiftStartHour < 0 || shiftStartHour > 23)
            throw new ArgumentException("Час начала смены должен быть от 0 до 23");

        if (shiftEndHour < 0 || shiftEndHour > 23)
            throw new ArgumentException("Час конца смены должен быть от 0 до 23");

        if (completionPercentage < 0 || completionPercentage > 1)
            throw new ArgumentException("Процент завершения должен быть в диапазоне от 0 до 1");

        // Создание TimeSpan для начала и конца смены (минуты = 0)
        TimeSpan startTime = new TimeSpan(shiftStartHour, 0, 0);
        TimeSpan endTime = new TimeSpan(shiftEndHour, 0, 0);

        // Если конец смены раньше или равен началу (ночная смена), добавляем 24 часа
        if (endTime <= startTime)
        {
            endTime = endTime.Add(TimeSpan.FromHours(24));
        }

        // Вычисление общей продолжительности смены
        TimeSpan totalDuration = endTime - startTime;

        // Вычисление прошедшего времени на основе процента завершения
        TimeSpan elapsedTime = TimeSpan.FromTicks((long)(totalDuration.Ticks * completionPercentage));

        // Вычисление текущего времени
        TimeSpan currentTime = startTime + elapsedTime;

        // Если текущее время превысило 24 часа, нормализуем его
        if (currentTime.Days > 0)
        {
            currentTime = currentTime.Subtract(TimeSpan.FromDays(currentTime.Days));
        }

        // Форматирование результата в строку hh:mm
        return FormatTime(currentTime);
    }

    private static string FormatTime(TimeSpan time)
    {
        return $"{(int)time.TotalHours:00}:{time.Minutes:00}";
    }
}
