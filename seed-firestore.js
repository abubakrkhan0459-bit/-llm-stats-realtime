const { initializeApp } = require('firebase-admin/app');
const { getFirestore } = require('firebase-admin/firestore');
const { execSync } = require('child_process');
const path = require('path');
const os = require('os');

// Get the Firebase config from the local Firebase CLI
const firebaseConfig = {
  projectId: 'llm-stats-realtime',
  // For development/testing, we'll use a workaround
  // In production, the Blazor app uses the REST API directly
};

// Initialize Firebase Admin without credentials (for emulator or use firebase CLI)
initializeApp(firebaseConfig);

const db = getFirestore();

// Overall Models Data
const overallModels = [
  { name: "GPT-5.2 (xhigh)", company: "OpenAI", type: "proprietary", rank: 1, description: "High-performance enterprise model with advanced reasoning", category: "overall" },
  { name: "Claude Opus 4.5", company: "Anthropic", type: "proprietary", rank: 2, description: "Best creative writer and nuanced reasoner", category: "overall" },
  { name: "GPT-5.2 Codex (xhigh)", company: "OpenAI", type: "proprietary", rank: 3, description: "Specialized coding variant with elite programming capabilities", category: "overall" },
  { name: "Gemini 3 Pro Preview (high)", company: "Google", type: "proprietary", rank: 4, description: "Preview release with enhanced multimodal reasoning", category: "overall" },
  { name: "Kimi K2.5", company: "Moonshot", type: "proprietary", rank: 5, description: "Next-generation Chinese model with improved long-context", category: "overall" },
  { name: "Gemini 3 Flash", company: "Google", type: "proprietary", rank: 6, description: "Efficiency champion; fast and cost-effective", category: "overall" },
  { name: "Claude 4.5 Sonnet", company: "Anthropic", type: "proprietary", rank: 7, description: "Balanced model for daily developer tasks", category: "overall" },
  { name: "GLM-4.7", company: "Zhipu AI", type: "opensource", rank: 1, description: "Top-tier bilingual reasoning with strong coding", category: "overall" },
  { name: "DeepSeek V3.2", company: "DeepSeek", type: "opensource", rank: 2, description: "Strong open-weights model rivaling proprietary benchmarks", category: "overall" },
  { name: "Grok 4", company: "xAI", type: "proprietary", rank: 10, description: "Real-time knowledge integration with social data", category: "overall" }
];

// Coding Models Data
const codingModels = [
  { name: "Claude Opus 4.5 Thinking", company: "Anthropic", type: "proprietary", rank: 1, description: "Best 'Agentic' coder; highest score on SWE-Bench Verified (80.9%)", category: "coding" },
  { name: "Claude Opus 4.5", company: "Anthropic", type: "proprietary", rank: 2, description: "The best creative writer and nuanced reasoner; highest 'human-feel' score", category: "coding" },
  { name: "GPT-5.2 High", company: "OpenAI", type: "proprietary", rank: 3, description: "Enterprise-focused model with strong instruction following and reliability", category: "coding" },
  { name: "Gemini 3 Pro", company: "Google", type: "proprietary", rank: 4, description: "Best for 'Whole Repo' understanding due to 1M context window", category: "coding" },
  { name: "Gemini 3 Flash", company: "Google", type: "proprietary", rank: 5, description: "The efficiency champion; beats GPT-4o while being 10x faster/cheaper", category: "coding" },
  { name: "GLM 4.7", company: "Zhipu AI", type: "opensource", rank: 6, description: "Elite coding performance; best open-source for software engineering", category: "coding" }
];

// Open Source Models Data
const openSourceModels = [
  { name: "GLM 4.7", company: "Zhipu AI", type: "opensource", rank: 1, description: "Top-tier open model with excellent bilingual and coding capabilities", category: "opensource" },
  { name: "DeepSeek V3.2 (MIT)", company: "DeepSeek", type: "opensource", rank: 2, description: "The current global standard for open AI; matches GPT-5 class performance", category: "opensource" },
  { name: "Llama 4 Behemoth", company: "Meta", type: "opensource", rank: 3, description: "Large parameter open-weights model; powerful but resource-intensive", category: "opensource" },
  { name: "Kimi K2 Thinking (Modified MIT)", company: "Moonshot", type: "opensource", rank: 4, description: "Best open reasoning model (Chain-of-Thought)", category: "opensource" },
  { name: "GPT-OSS-120B", company: "OpenAI", type: "opensource", rank: 5, description: "OpenAI's open-weight release with strong performance and stability", category: "opensource" }
];

async function seedDatabase() {
  console.log('Starting to seed Firestore database...\n');
  
  const batch = db.batch();
  
  // Add overall models
  console.log('Adding overall models...');
  for (const model of overallModels) {
    const docRef = db.collection('models').doc(`overall_${model.rank}`);
    batch.set(docRef, model);
  }
  
  // Add coding models
  console.log('Adding coding models...');
  for (const model of codingModels) {
    const docRef = db.collection('models').doc(`coding_${model.rank}`);
    batch.set(docRef, model);
  }
  
  // Add open source models
  console.log('Adding open source models...');
  for (const model of openSourceModels) {
    const docRef = db.collection('models').doc(`opensource_${model.rank}`);
    batch.set(docRef, model);
  }
  
  // Commit the batch
  await batch.commit();
  
  console.log('\n✅ Database seeded successfully!');
  console.log(`   Total models added: ${overallModels.length + codingModels.length + openSourceModels.length}`);
  console.log('\nYou can view your data at:');
  console.log('https://console.firebase.google.com/project/llm-stats-realtime/firestore/data');
}

seedDatabase().catch(console.error);
