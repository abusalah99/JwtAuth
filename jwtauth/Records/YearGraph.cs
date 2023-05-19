namespace jwtauth;

public record YearGraph
{
    public int Year { get; set; }
    public int January { get; set; }
    public int February { get; set; }
    public int March { get; set; }
    public int April { get; set; }
    public int May { get; set; }
    public int June { get; set; }
    public int July { get; set; }
    public int August { get; set; }
    public int September { get; set; }
    public int October { get; set; }
    public int November { get; set; }
    public int December { get; set; }

    public int this[int month]
    {
        get
        {
            return month switch
            {
                1 => January,
                2 => February,
                3 => March,
                4 => April,
                5 => May,
                6 => June,
                7 => July,
                8 => August,
                9 => September,
                10 => October,
                11 => November,
                12 => December,
                _ => throw new ArgumentOutOfRangeException(
                    nameof(month), "Month must be between 1 and 12."),
            };
        }
        set
        {
            switch (month)
            {
                case 1:
                    January = value;
                    break;
                case 2:
                    February = value;
                    break;
                case 3:
                    March = value;
                    break;
                case 4:
                    April = value;
                    break;
                case 5:
                    May = value;
                    break;
                case 6:
                    June = value;
                    break;
                case 7:
                    July = value;
                    break;
                case 8:
                    August = value;
                    break;
                case 9:
                    September = value;
                    break;
                case 10:
                    October = value;
                    break;
                case 11:
                    November = value;
                    break;
                case 12:
                    December = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(month), "Month must be between 1 and 12.");
            }
        }
    }
}