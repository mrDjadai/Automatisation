public static class UpgradePointsTextSetter
{
    public static string GetText(int value)
    {
        if (value % 10 == 0)
        {
            return (value / 10).ToString();
        }
        return (value / 10).ToString() + '.' + (value % 10).ToString();
    }
}
