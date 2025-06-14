namespace LogAnalyzer.Services;

public class LogAnalyzerClient
{
    private readonly HttpClient _httpClient;

    public LogAnalyzerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
}