using LLMStatsBlazor.Models;

namespace LLMStatsBlazor.Services;

public class MockDataService
{
    private List<LLMModel> _overallModels = new()
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
        new LLMModel { Name = "MiMo-V2-Flash", Company = "MiMo", Type = "proprietary", Rank = 13, Description = "Flash variant optimized for speed" },
        new LLMModel { Name = "Grok 4.1 Fast", Company = "xAI", Type = "proprietary", Rank = 14, Description = "Low-latency version for real-time applications" },
        new LLMModel { Name = "KAT-Code", Company = "KAT AI", Type = "proprietary", Rank = 15, Description = "Specialized coding assistant" },
        new LLMModel { Name = "Llama 4 Behemoth", Company = "Meta", Type = "opensource", Rank = 3, Description = "Large parameter open-weights model; powerful but resource-intensive" },
        new LLMModel { Name = "GPT-OSS-120B", Company = "OpenAI", Type = "opensource", Rank = 5, Description = "OpenAI's open-weight release with strong performance and stability" },
        new LLMModel { Name = "Qwen 3 Max", Company = "Alibaba", Type = "opensource", Rank = 6, Description = "Best Multilingual open model" },
        new LLMModel { Name = "Mixtral 8x7B", Company = "Mistral", Type = "opensource", Rank = 7, Description = "Top European MoE model; efficient and powerful" },
        new LLMModel { Name = "Nemotron-CC 70B", Company = "NVIDIA", Type = "opensource", Rank = 8, Description = "Open-source model optimized for NVIDIA hardware; RAG-ready" },
        new LLMModel { Name = "Llama 4 Scout", Company = "Meta", Type = "opensource", Rank = 9, Description = "The Goldilocks model; fits on dual 4090s/A6000s" },
        new LLMModel { Name = "Gemma 3 27B", Company = "Google", Type = "opensource", Rank = 10, Description = "Best single-GPU model (fits in 24GB VRAM with quantization)" },
        new LLMModel { Name = "Phi-4 Mini", Company = "Microsoft", Type = "opensource", Rank = 11, Description = "Microsoft's small reasoning monster for edge/mobile" },
        new LLMModel { Name = "Command R+ v2", Company = "Cohere", Type = "opensource", Rank = 16, Description = "Best open model for Tool Use and RAG" },
        new LLMModel { Name = "Falcon 3", Company = "TII", Type = "opensource", Rank = 13, Description = "Royalty-free commercial use focus" }
    };

    private List<LLMModel> _codingModels = new()
    {
        new LLMModel { Name = "Claude Opus 4.5 Thinking", Company = "Anthropic", Type = "proprietary", Rank = 1, Description = "Best \"Agentic\" coder; highest score on SWE-Bench Verified (80.9%)." },
        new LLMModel { Name = "Claude Opus 4.5", Company = "Anthropic", Type = "proprietary", Rank = 2, Description = "The best creative writer and nuanced reasoner; highest \"human-feel\" score." },
        new LLMModel { Name = "GPT-5.2 High", Company = "OpenAI", Type = "proprietary", Rank = 3, Description = "Enterprise-focused model with strong instruction following and reliability." },
        new LLMModel { Name = "Gemini 3 Pro", Company = "Google", Type = "proprietary", Rank = 4, Description = "Best for \"Whole Repo\" understanding due to 1M context window." },
        new LLMModel { Name = "Gemini 3 Flash", Company = "Google", Type = "proprietary", Rank = 5, Description = "The efficiency champion; beats GPT-4o while being 10x faster/cheaper." },
        new LLMModel { Name = "GLM 4.7", Company = "Zhipu AI", Type = "opensource", Rank = 6, Description = "Elite coding performance; best open-source for software engineering." },
        new LLMModel { Name = "MiniMax M2.1 Preview", Company = "MiniMax", Type = "proprietary", Rank = 7, Description = "Strong performance on coding benchmarks with efficient inference." },
        new LLMModel { Name = "Gemini 3 Flash (Thinking-Minimal)", Company = "Google", Type = "proprietary", Rank = 8, Description = "Lightweight reasoning model optimized for fast coding tasks." },
        new LLMModel { Name = "GPT-5.2", Company = "OpenAI", Type = "proprietary", Rank = 9, Description = "Balanced performance model for general coding tasks." },
        new LLMModel { Name = "GPT-5-Medium", Company = "OpenAI", Type = "proprietary", Rank = 10, Description = "Mid-tier model optimized for cost-effective coding workflows." },
        new LLMModel { Name = "Kimi K2 Thinking", Company = "Moonshot", Type = "proprietary", Rank = 11, Description = "Currently #1 on LiveCodeBench (83.1%); elite at debugging complex repos." },
        new LLMModel { Name = "GPT-5.2 Codex-Max", Company = "OpenAI", Type = "proprietary", Rank = 12, Description = "Specialized fine-tuned model optimized for system architecture and coding." },
        new LLMModel { Name = "Grok 3 Beta", Company = "xAI", Type = "proprietary", Rank = 13, Description = "Surprisingly strong at Python/Rust; massive pre-training on X/Twitter code data." },
        new LLMModel { Name = "DeepSeek V3.2 Coder", Company = "DeepSeek", Type = "opensource", Rank = 14, Description = "The choice for local VS Code users; extremely fast and accurate." },
        new LLMModel { Name = "Claude Sonnet 4.5", Company = "Anthropic", Type = "proprietary", Rank = 15, Description = "The daily driver for 60% of professional software engineers (Cursor/Windsurf default)." },
        new LLMModel { Name = "Qwen 3 Coder (480B)", Company = "Alibaba", Type = "opensource", Rank = 16, Description = "Largest open-weight coding specialist." },
        new LLMModel { Name = "OpenAI o3-mini", Company = "OpenAI", Type = "proprietary", Rank = 17, Description = "Reasoning model specialized for competitive programming and algorithmic challenges." },
        new LLMModel { Name = "Llama 4 Code-Maven", Company = "Meta", Type = "opensource", Rank = 18, Description = "Meta's fine-tuned variant of Llama 4 for software engineering." },
        new LLMModel { Name = "StarCoder 3 (15B)", Company = "Hugging Face", Type = "opensource", Rank = 19, Description = "Best low-latency autocomplete model for local deployment." },
        new LLMModel { Name = "Codestral 2.5", Company = "Mistral", Type = "proprietary", Rank = 20, Description = "Optimized for intermediate tasks (FIM - Fill In Middle)." },
        new LLMModel { Name = "GPT-OSS-20B", Company = "OpenAI", Type = "opensource", Rank = 21, Description = "Efficient open model that beats GPT-4 on Python tasks." },
        new LLMModel { Name = "Mistral Large 3", Company = "Mistral", Type = "proprietary", Rank = 22, Description = "Strong at C++/Java enterprise codebases." },
        new LLMModel { Name = "Granite Code 3.5", Company = "IBM", Type = "proprietary", Rank = 23, Description = "The standard for COBOL/Legacy mainframe modernization." },
        new LLMModel { Name = "Refact-1.6B v2", Company = "Refact", Type = "opensource", Rank = 24, Description = "Instant latency; runs on CPU." },
        new LLMModel { Name = "Phind-70B v3", Company = "Phind", Type = "opensource", Rank = 25, Description = "Tuned specifically for documentation-to-code generation." },
        new LLMModel { Name = "Magic LMM-2", Company = "Magic.dev", Type = "proprietary", Rank = 26, Description = "Startup model with 5M context for massive migrations." },
        new LLMModel { Name = "Stable Code 4", Company = "Stability AI", Type = "opensource", Rank = 27, Description = "Best for game dev (C#/Unity/Unreal) scripts." },
        new LLMModel { Name = "CodeGemma 3", Company = "Google", Type = "opensource", Rank = 28, Description = "Google's open contribution; excellent integration with JAX/TensorFlow." },
        new LLMModel { Name = "WizardCoder 2", Company = "Microsoft", Type = "opensource", Rank = 29, Description = "Community favorite for instruction following." },
        new LLMModel { Name = "DeepSeek R1-Coder", Company = "DeepSeek", Type = "opensource", Rank = 30, Description = "Distilled reasoning model for difficult algorithmic logic." },
        new LLMModel { Name = "Arctic 2", Company = "Snowflake", Type = "opensource", Rank = 31, Description = "Best for SQL generation and Data Engineering pipelines." },
        new LLMModel { Name = "SQLCoder-X", Company = "Defog", Type = "opensource", Rank = 32, Description = "Specialized text-to-SQL model." },
        new LLMModel { Name = "Dolma-Coder", Company = "AllenAI", Type = "opensource", Rank = 33, Description = "Fully transparent dataset model." },
        new LLMModel { Name = "Yi-Coder 34B", Company = "01.AI", Type = "opensource", Rank = 34, Description = "High performance/size ratio." },
        new LLMModel { Name = "OpenCodeInterpreter 2", Company = "OpenInterpreter", Type = "opensource", Rank = 35, Description = "Self-correcting model that runs code to test its own output." },
        new LLMModel { Name = "CodeLlama 3 (70B)", Company = "Meta", Type = "opensource", Rank = 36, Description = "Older but reliable baseline." },
        new LLMModel { Name = "AlphaCode 2", Company = "DeepMind", Type = "proprietary", Rank = 37, Description = "Research system for competitive programming." }
    };

    private List<LLMModel> _openSourceModels = new()
    {
        new LLMModel { Name = "GLM 4.7", Company = "Zhipu AI", Type = "opensource", Rank = 1, Description = "Top-tier open model with excellent bilingual and coding capabilities." },
        new LLMModel { Name = "DeepSeek V3.2 (MIT)", Company = "DeepSeek", Type = "opensource", Rank = 2, Description = "The current global standard for open AI; matches GPT-5 class performance." },
        new LLMModel { Name = "Llama 4 Behemoth", Company = "Meta", Type = "opensource", Rank = 3, Description = "Large parameter open-weights model; powerful but resource-intensive." },
        new LLMModel { Name = "Kimi K2 Thinking (Modified MIT)", Company = "Moonshot", Type = "opensource", Rank = 4, Description = "Best open reasoning model (Chain-of-Thought)." },
        new LLMModel { Name = "GPT-OSS-120B", Company = "OpenAI", Type = "opensource", Rank = 5, Description = "OpenAI's open-weight release with strong performance and stability." },
        new LLMModel { Name = "Qwen 3 Max (Apache 2.0)", Company = "Alibaba", Type = "opensource", Rank = 6, Description = "Best Multilingual open model." },
        new LLMModel { Name = "Mixtral 8x7B", Company = "Mistral", Type = "opensource", Rank = 7, Description = "Top European MoE model; efficient and powerful." },
        new LLMModel { Name = "Nemotron-CC 70B", Company = "NVIDIA", Type = "opensource", Rank = 8, Description = "Open-source model optimized for NVIDIA hardware; RAG-ready." },
        new LLMModel { Name = "Llama 4 Scout (109B)", Company = "Meta", Type = "opensource", Rank = 9, Description = "The \"Goldilocks\" model; fits on dual 4090s/A6000s." },
        new LLMModel { Name = "Gemma 3 27B", Company = "Google", Type = "opensource", Rank = 10, Description = "Best single-GPU model (fits in 24GB VRAM with quantization)." },
        new LLMModel { Name = "Phi-4 Mini", Company = "Microsoft", Type = "opensource", Rank = 11, Description = "Microsoft's small reasoning monster for edge/mobile." },
        new LLMModel { Name = "Mixtral 12x24B", Company = "Mistral", Type = "opensource", Rank = 12, Description = "High throughput MoE (Mixture of Experts)." },
        new LLMModel { Name = "Falcon 3", Company = "TII", Type = "opensource", Rank = 13, Description = "Royalty-free commercial use focus." },
        new LLMModel { Name = "GLM-4.7 Open", Company = "Z.ai", Type = "opensource", Rank = 14, Description = "Strongest Chinese/English bilingual open model." },
        new LLMModel { Name = "DeepSeek R1-Distill", Company = "DeepSeek", Type = "opensource", Rank = 15, Description = "Distilled reasoning capabilities for smaller hardware." },
        new LLMModel { Name = "Command R+ v2", Company = "Cohere", Type = "opensource", Rank = 16, Description = "Best open model for Tool Use and RAG." },
        new LLMModel { Name = "OpenChat 4.5", Company = "LMSYS", Type = "opensource", Rank = 17, Description = "Community fine-tune excellence." },
        new LLMModel { Name = "Hermes 4 (Nous Research)", Company = "Nous Research", Type = "opensource", Rank = 18, Description = "Uncensored/Steerable fine-tune of Llama 4." },
        new LLMModel { Name = "Yi-Lightning Open", Company = "01.AI", Type = "opensource", Rank = 19, Description = "100K context open model." },
        new LLMModel { Name = "Solar Pro 10.7B", Company = "Upstage", Type = "opensource", Rank = 20, Description = "Best small model for fine-tuning on specific domains." },
        new LLMModel { Name = "StarCoder 3", Company = "Hugging Face", Type = "opensource", Rank = 21, Description = "Best open coding assistant." },
        new LLMModel { Name = "OLMo 2.0 (AI2)", Company = "AI2", Type = "opensource", Rank = 22, Description = "Fully open source (weights, data, and training logs)." },
        new LLMModel { Name = "StableLM 3 (12B)", Company = "Stability AI", Type = "opensource", Rank = 23, Description = "Great for creative writing/roleplay." },
        new LLMModel { Name = "Qwen 2.5-VL", Company = "Alibaba", Type = "opensource", Rank = 24, Description = "Best open Vision-Language model." },
        new LLMModel { Name = "InternLM-X", Company = "Shanghai AI Lab", Type = "opensource", Rank = 25, Description = "Academic research standard." },
        new LLMModel { Name = "TinyLlama 2 (1.1B)", Company = "Meta", Type = "opensource", Rank = 26, Description = "Embedded systems standard." },
        new LLMModel { Name = "BitNet 2.0", Company = "Microsoft", Type = "opensource", Rank = 27, Description = "1.58-bit architecture; runs on incredibly low power." },
        new LLMModel { Name = "H2O-Danube 3", Company = "H2O.ai", Type = "opensource", Rank = 28, Description = "Optimized for mobile integration." },
        new LLMModel { Name = "Grok-1 (Open Release)", Company = "xAI", Type = "opensource", Rank = 29, Description = "The older base model, still useful for massive scale experiments." },
        new LLMModel { Name = "Pythia-Pro", Company = "EleutherAI", Type = "opensource", Rank = 30, Description = "Interpretability research focus." },
        new LLMModel { Name = "Bloom 2", Company = "BigScience", Type = "opensource", Rank = 31, Description = "BigScience collaboration; heavily multilingual." }
    };

    public async Task<List<LLMModel>> GetOverallModelsAsync()
    {
        // PLACEHOLDER: This is where you will call Firebase later
        // e.g., return await _firebaseClient.GetCollection<LLMModel>("overall").ToListAsync();
        await Task.Delay(100); // Simulate network latency
        return _overallModels;
    }

    public async Task<List<LLMModel>> GetCodingModelsAsync()
    {
        await Task.Delay(100);
        return _codingModels;
    }

    public async Task<List<LLMModel>> GetOpenSourceModelsAsync()
    {
        await Task.Delay(100);
        return _openSourceModels;
    }

    public async Task<List<LLMModel>> GetAllUniqueModelsAsync()
    {
        await Task.Delay(100);
        var all = new List<LLMModel>();
        all.AddRange(_overallModels);
        all.AddRange(_codingModels);
        all.AddRange(_openSourceModels);
        return all.GroupBy(m => m.Name).Select(g => g.First()).ToList();
    }
}