class LibraryCount
{ 
    private int _startDeposit = 10000; // the amount of books when Library opened

    public int Deposit
    {
        get => _startDeposit;
        set => _startDeposit = value;
    } 

    public int Remove(int a, int startDeposit) // call method if reader's reply has "take"
    {
        int result = startDeposit - a;
        return result;
    }

    public int Add(int a, int startedDeposit) // call method if reader's reply has "back"
    {
        int result = _startDeposit + a;
        return result;
    }
}

