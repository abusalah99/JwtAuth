namespace jwtauth;

public class ResultFactory
{
    public static IRecordResult GetResult(string result)
    {
        if (result == "HorizontalMisalignment")
            return new HorizontalMisalignment();

        if (result == "Imbalance")
            return new Imbalance();

        if (result == "Normal")
            return new Normal();

        if (result == "Overhang")
            return new Overhang();

        if (result == "Underhang")
            return new Underhang();

        if (result == "VerticalMisalignment")
            return new VerticalMisalignment();

        throw new ArgumentException("Invalid result");
    }
}
