﻿namespace jwtauth;

public class Status
{
    public int NumberOfRcordsCreatedToday { get; set; }
    public int NumberOfUsersCreatedToday { get; set; }
    public required YearGraph UsersYearGraph { get; set; }   
    public required YearGraph RecordsYearGraph { get; set; }
}

