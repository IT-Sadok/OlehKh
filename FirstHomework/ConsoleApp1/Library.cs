using System;
using System.Collections.Generic;

public class Library
{
    public int Balance { get; private set; } = 0;

    public List<Media> Items { get; private set; } = new List<Media>();

    private IMediaFactory _mediaFactory;

    public Library() { }

    public int GetBalance()
    {
        return Balance;
    }

    public void SetBalance(int newBalance)
    {
        Balance = newBalance;
    }

    public Library(IMediaFactory mediaFactory)
    {
        _mediaFactory = mediaFactory;
    }

    public int Remove(int number, int startDeposit) // call method if reader's reply has "take"
    {
        return Balance = Balance - number;
    }

    public int Add(int number, int startedDeposit) // call method if reader's reply has "back"
    {
        return Balance = Balance + number;
    }
}
