namespace jwtauth;

public class RecordResultUnitOfWork : BaseSettingsUnitOfWork<RecordResult>, IRecordResultUnitOfWork
{
    private readonly IPythonScriptExcutor _scriptExcuter;
    private readonly IFileSaver _FileSaver;
    private readonly IRecordResultRepository _recordResultRepository;
    private readonly ICloud _cloud;
    public RecordResultUnitOfWork(IRecordResultRepository repository,
         IPythonScriptExcutor modelExcutor,
        IFileSaver fileSaver, ICloud cloud) : base(repository)
    {
        _scriptExcuter = modelExcutor;
        _FileSaver = fileSaver;
        _recordResultRepository = repository;
        _cloud = cloud;
    }

    public async Task Create(IFormFile formFile, string rootPath , Guid userId)
    {
        string extension = Path.GetExtension(formFile.FileName).ToLower();

        if (extension != ".wav")
            throw new ArgumentException("Please send wav file");

        string audioFilePath = rootPath + @"\Services\PythonService\" + formFile.FileName;
        string modelPath = rootPath + @"\Services\PythonService\AiModels\MotorFaultsDiagnosis.h5";
        await _FileSaver.Save(formFile, audioFilePath );

        string scriptResult = await _scriptExcuter.Excute(rootPath,
            "MfccScrpit.py", audioFilePath, modelPath);

        IRecordResult result = ResultFactory.GetResult(scriptResult);

        RecordResult recordResult = result.GetResult(userId); 

        await Create(recordResult); 
    }

    public override async Task Update(RecordResult result)
    {
        RecordResult resultFromDb = await Read(result.Id);

        resultFromDb.Rate = result.Rate;
        resultFromDb.Feedback = result.Feedback;
        
        await base.Update(resultFromDb);
    }

    public async Task<IEnumerable<RecordResultResponse>> GetRecordsByUserId(Guid id)
    {
        List<RecordResultResponse> responses = new();  

        var results =  await _recordResultRepository.GetByUserId(id);

        foreach (RecordResult result in results) 
        {
            RecordResultResponse response = new();

            response.Feedback = result.Feedback;
            response.Id = result.Id;    
            response.Name = result.Name;
            response.Rate = result.Rate;
            response.PdfUrl = await _cloud.GetFileUrl($"{response.Name}.pdf");

            responses.Add(response);
        }

        return responses;
    }
                 
} 