using System.Net.Http.Json;
using LogParser.Models;

namespace LogAnalyzer.Services;

public class LogAnalyzerClient
{
    private readonly HttpClient _httpClient;

    private readonly Uri _analyzeFilesEndpoint;

    public LogAnalyzerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _analyzeFilesEndpoint = new Uri("analyze");
    }

    public async Task<ParseAndAnalyzeResult> AnalyzeFilesAsync(IEnumerable<string> fullFileNames)
    {
        using var formData = new MultipartFormDataContent();

        foreach (var fullFileName in fullFileNames)
        {
            using var fileStream = File.OpenRead(fullFileName);
            
            using var streamContent = new StreamContent(fileStream);
            
            formData.Add(streamContent, "logFiles", Path.GetFileName(fullFileName));
        }
        
        var httpResponse = await _httpClient.PostAsync(
            _analyzeFilesEndpoint,
            formData
        );

        var deserializedResponse = await httpResponse.Content.ReadFromJsonAsync
            <ParseAndAnalyzeResult>();

        return deserializedResponse;
    }
}