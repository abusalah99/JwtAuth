namespace jwtauth;

public class Status
{
    public int NumberOfRcordsCreatedToday { get; set; }
    public int NumberOfUsersCreatedToday { get; set; }
    public YearGraph UsersYearGraph { get; set; }   
    public YearGraph RecordsYearGraph { get; set; }
}
