using LLMStatsBlazor.Models;
using System.Net.Http.Json;

namespace LLMStatsBlazor.Services;

public class FirestoreDataService
{
    private readonly HttpClient _httpClient;
    private const string ProjectId = "llm-stats-realtime";
    private const string BaseUrl = "https://firestore.googleapis.com/v1";

    public FirestoreDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri($"{BaseUrl}/projects/{ProjectId}/databases/(default)/documents/");
    }

    public async Task<List<LLMModel>> GetOverallModelsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FirestoreResponse>("models?pageSize=100");
            if (response?.documents == null) return new List<LLMModel>();

            var models = response.documents
                .Where(d => d.fields?["category"]?.stringValue == "overall")
                .Select(d => ConvertToModel(d))
                .OrderBy(m => m.Rank)
                .ToList();

            // Fallback to mock data if Firestore is empty
            if (models.Count == 0)
            {
                return GetMockOverallModels();
            }

            return models;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching overall models: {ex.Message}");
            return GetMockOverallModels();
        }
    }

    public async Task<List<LLMModel>> GetCodingModelsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FirestoreResponse>("models?pageSize=100");
            if (response?.documents == null) return new List<LLMModel>();

            var models = response.documents
                .Where(d => d.fields?["category"]?.stringValue == "coding")
                .Select(d => ConvertToModel(d))
                .OrderBy(m => m.Rank)
                .ToList();

            // Fallback to mock data if Firestore is empty
            if (models.Count == 0)
            {
                return GetMockCodingModels();
            }

            return models;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching coding models: {ex.Message}");
            return GetMockCodingModels();
        }
    }

    public async Task<List<LLMModel>> GetOpenSourceModelsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FirestoreResponse>("models?pageSize=100");
            if (response?.documents == null) return new List<LLMModel>();

            var models = response.documents
                .Where(d => d.fields?["type"]?.stringValue == "opensource")
                .Select(d => ConvertToModel(d))
                .OrderBy(m => m.Rank)
                .ToList();

            // Fallback to mock data if Firestore is empty
            if (models.Count == 0)
            {
                return GetMockOpenSourceModels();
            }

            return models;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching open source models: {ex.Message}");
            return GetMockOpenSourceModels();
        }
    }

    public async Task<List<LLMModel>> GetAllUniqueModelsAsync()
    {
        var all = new List<LLMModel>();
        all.AddRange(await GetOverallModelsAsync());
        all.AddRange(await GetCodingModelsAsync());
        all.AddRange(await GetOpenSourceModelsAsync());
        return all.GroupBy(m => m.Name).Select(g => g.First()).ToList();
    }

    private LLMModel ConvertToModel(FirestoreDocument doc)
    {
        var fields = doc.fields ?? new Dictionary<string, FirestoreValue>();
        
        return new LLMModel
        {
            Name = GetStringField(fields, "name"),
            Company = GetStringField(fields, "company"),
            Type = GetStringField(fields, "type"),
            Rank = GetIntField(fields, "rank"),
            Description = GetStringField(fields, "description")
        };
    }

    private string GetStringField(Dictionary<string, FirestoreValue> fields, string key)
    {
        return fields.TryGetValue(key, out var value) ? value.stringValue ?? "" : "";
    }

    private int GetIntField(Dictionary<string, FirestoreValue> fields, string key)
    {
        if (fields.TryGetValue(key, out var value))
        {
            if (value.integerValue != null && int.TryParse(value.integerValue, out int result))
                return result;
        }
        return 0;
    }

    // Mock data fallback methods
    private List<LLMModel> GetMockOverallModels() => new()
    {
        new LLMModel { Name = "GPT-5.2 (xhigh)", Company = "OpenAI", Type = "proprietary", Rank = 1, Description = "High-performance enterprise model with advanced reasoning" },
        new LLMModel { Name = "Claude Opus 4.5", Company = "Anthropic", Type = "proprietary", Rank = 2, Description = "Best creative writer and nuanced reasoner" },
        new LLMModel { Name = "GPT-5.2 Codex (xhigh)", Company = "OpenAI", Type = "proprietary", Rank = 3, Description = "Specialized coding variant with elite programming capabilities" },
        new LLMModel { Name = "Gemini 3 Pro Preview (high)", Company = "Google", Type = "proprietary", Rank = 4, Description = "Preview release with enhanced multimodal reasoning" },
        new LLMModel { Name = "Kimi K2.5", Company = "Moonshot", Type = "proprietary", Rank = 5, Description = "Next-generation Chinese model with improved long-context" },
        new LLMModel { Name = "Gemini 3 Flash", Company = "Google", Type = "proprietary", Rank = 6, Description = "Efficiency champion; fast and cost-effective" },
        new LLMModel { Name = "Claude 4.5 Sonnet", Company = "Anthropic", Type = "proprietary", Rank = 7, Description = "Balanced model for daily developer tasks" },
        new LLMModel { Name = "GLM-4.7", Company = "Zhipu AI", Type = "opensource", Rank = 1, Description = "Top-tier bilingual reasoning with strong coding" },
        new LLMModel { Name = "DeepSeek V3.2", Company = "DeepSeek", Type = "opensource", Rank = 2, Description = "Strong open-weights model rivaling proprietary benchmarks" },
        new LLMModel { Name = "Grok 4", Company = "xAI", Type = "proprietary", Rank = 10, Description = "Real-time knowledge integration with social data" },
        new LLMModel { Name = "Kimi K2 Thinking", Company = "Moonshot", Type = "proprietary", Rank = 11, Description = "Reasoning specialist with Chain-of-Thought capabilities" },
        new LLMModel { Name = "MiniMax-M2.1", Company = "MiniMax", Type = "proprietary", Rank = 12, Description = "Efficient inference model for rapid deployment" },
    };

    private List<LLMModel> GetMockCodingModels() => new()
    {
        new LLMModel { Name = "Claude Opus 4.5 Thinking", Company = "Anthropic", Type = "proprietary", Rank = 1, Description = "Best \"Agentic\" coder; highest score on SWE-Bench Verified (80.9%)." },
        new LLMModel { Name = "Claude Opus 4.5", Company = "Anthropic", Type = "proprietary", Rank = 2, Description = "The best creative writer and nuanced reasoner; highest \"human-feel\" score." },
        new LLMModel { Name = "GPT-5.2 High", Company = "OpenAI", Type = "proprietary", Rank = 3, Description = "Enterprise-focused model with strong instruction following and reliability." },
        new LLMModel { Name = "Gemini 3 Pro", Company = "Google", Type = "proprietary", Rank = 4, Description = "Best for \"Whole Repo\" understanding due to 1M context window." },
        new LLMModel { Name = "Gemini 3 Flash", Company = "Google", Type = "proprietary", Rank = 5, Description = "The efficiency champion; beats GPT-4o while being 10x faster/cheaper." },
        new LLMModel { Name = "GLM 4.7", Company = "Zhipu AI", Type = "opensource", Rank = 6, Description = "Elite coding performance; best open-source for software engineering." },
    };

    private List<LLMModel> GetMockOpenSourceModels() => new()
    {
        new LLMModel { Name = "GLM 4.7", Company = "Zhipu AI", Type = "opensource", Rank = 1, Description = "Top-tier open model with excellent bilingual and coding capabilities." },
        new LLMModel { Name = "DeepSeek V3.2 (MIT)", Company = "DeepSeek", Type = "opensource", Rank = 2, Description = "The current global standard for open AI; matches GPT-5 class performance." },
        new LLMModel { Name = "Llama 4 Behemoth", Company = "Meta", Type = "opensource", Rank = 3, Description = "Large parameter open-weights model; powerful but resource-intensive." },
        new LLMModel { Name = "Kimi K2 Thinking (Modified MIT)", Company = "Moonshot", Type = "opensource", Rank = 4, Description = "Best open reasoning model (Chain-of-Thought)." },
        new LLMModel { Name = "GPT-OSS-120B", Company = "OpenAI", Type = "opensource", Rank = 5, Description = "OpenAI's open-weight release with strong performance and stability." },
    };

    // Firestore REST API response classes
    private class FirestoreResponse
    {
        public List<FirestoreDocument>? documents { get; set; }
    }

    private class FirestoreDocument
    {
        public string? name { get; set; }
        public Dictionary<string, FirestoreValue>? fields { get; set; }
        public string? createTime { get; set; }
        public string? updateTime { get; set; }
    }

    private class FirestoreValue
    {
        public string? stringValue { get; set; }
        public string? integerValue { get; set; }
        public bool? booleanValue { get; set; }
    }
}
