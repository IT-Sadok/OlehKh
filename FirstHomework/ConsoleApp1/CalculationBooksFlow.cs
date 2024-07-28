class CalculationBooksFlow: Library
{
    public int Remove(int a) // calculating if reader's reply has "take"
    {
        int result = Deposit - a;
        return result;
    }

    public int Add(int a) // calculating if reader's reply has "back"
    {
        int result = Deposit + a;
        return result;
    }
}

