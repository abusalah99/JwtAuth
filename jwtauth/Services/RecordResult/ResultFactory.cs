namespace jwtauth;
public class ResultFactory
{
    public static IRecordResult GetResult(string result)
    {
        return result switch
        {
            "HorizontalMisalignment" => new HorizontalMisalignment(),
            "Imbalance" => new Imbalance(),
            "Normal" => new Normal(),
            "Overhang" => new Overhang(),
            "Underhang" => new Underhang(),
            "VerticalMisalignment" => new VerticalMisalignment(),
            _ => throw new ArgumentException("Invalid result")
        };
    }   
}
