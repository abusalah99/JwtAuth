namespace jwtauth;

public class RecordResultResponse : BaseEntitySettings

{
    public DateTime CreatedAt { get; set; }
    public int Rate { get; set; }
    public string? Feedback { get; set; }
    public string PdfUrl { get; set; }  
}
