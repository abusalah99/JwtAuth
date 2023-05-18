namespace jwtauth;

public class RecordResultUnitOfWork : BaseSettingsUnitOfWork<RecordResult>, IRecordResultUnitOfWork
{
    private readonly IPythonScriptExcutor _scriptExcuter;
    private readonly IFileSaver _FileSaver;
    private readonly IRecordResultRepository _recordResultRepository;
    public RecordResultUnitOfWork(IRecordResultRepository repository,
         IPythonScriptExcutor modelExcutor, IFileSaver fileSaver): base(repository)
    {
        _scriptExcuter = modelExcutor;
        _FileSaver = fileSaver;
        _recordResultRepository = repository;
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

        byte[] Pdf = await File.ReadAllBytesAsync(rootPath 
            + @"\Resources\Results\" + scriptResult + ".pdf");

        IRecordResult result = ResultFactory.GetResult(scriptResult);

        RecordResult recordResult = result.GetResult(userId);
        recordResult.Pdf = Pdf; 

        await Create(recordResult); 
    }

    public override async Task Update(RecordResult result)
    {
        RecordResult resultFromDb = await Read(result.Id);

        resultFromDb.Rate = result.Rate;
        resultFromDb.Feedback = result.Feedback;
        
        await base.Update(resultFromDb);
    }
    public async Task<IEnumerable<RecordResult>> GetRecordsByUserId(Guid id)
                => await _recordResultRepository.GetByUserId(id);
} 