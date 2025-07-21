using System;

public static class Score
{
    private static string _winner;

    public static string Winner
    {
        get => _winner;
        set
        {
            _winner = value;
            OnWinnerChanged?.Invoke(_winner);
        }
    }

    public static Action<string> OnWinnerChanged;
}
