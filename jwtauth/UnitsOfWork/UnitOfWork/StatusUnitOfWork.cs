namespace jwtauth;

public class StatusUnitOfWork : IStatusUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly IRecordResultRepository _recordResultRepository;

    public StatusUnitOfWork(IUserRepository userRepository, IRecordResultRepository recordResultRepository)
    {
        _userRepository = userRepository;
        _recordResultRepository = recordResultRepository;
    }

    public async Task<Status> GetStatus()
    {
        int numberOfRecordsCreatedToday = GetListCount(await _recordResultRepository.GetRecordsCreatedToday());

        int numberOfUsersCreatedToday = GetListCount(await _userRepository.GetUsersCreatedToday());

        YearGraph userYearGraph = new();
        userYearGraph = await CalculateGraph(userYearGraph, _userRepository.GetUsersCreatedAtMonth);

        YearGraph recordsYearGraph = new();
        recordsYearGraph = await CalculateGraph(recordsYearGraph, _recordResultRepository.GetRecordsCreatedAtMonth);

        Status status = new()
        {
            NumberOfRcordsCreatedToday = numberOfRecordsCreatedToday,
            NumberOfUsersCreatedToday = numberOfUsersCreatedToday,
            UsersYearGraph = userYearGraph,
            RecordsYearGraph = recordsYearGraph
        };

        return status;
    }

    private static async Task<YearGraph> CalculateGraph<T>
        (YearGraph graph, Func<int, int, Task<IEnumerable<T>>> GetListCreatedAtMonth)
    {
        graph.Year = DateTime.Now.Year;

        for(int i = 1; i <= 12;  i++)
            graph[i] = GetListCount(await GetListCreatedAtMonth(i , DateTime.Now.Year));
        
        return graph;   
    }

    private static int GetListCount<T>(IEnumerable<T> list) => list.Count();
}
