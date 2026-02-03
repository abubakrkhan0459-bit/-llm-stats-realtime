# AGENTS.md

Guidance for agentic coding assistants working on this LLM Stats Realtime project.

## Project Structure

**Blazor WebAssembly** project with legacy HTML/CSS/JS reference code.

- **Primary**: `LLMStatsBlazor/` - Blazor WebAssembly app
  - Entry: `wwwroot/index.html`
  - Components: `Pages/*.razor`, `Components/*.razor`
- **Legacy** (reference only): Root `index.html`, `styles.css`, `script.js`

## Build, Lint, Test Commands

```bash
# Development server (hot reload enabled)
cd LLMStatsBlazor && dotnet run
# Opens at http://localhost:5268

# Build for production
cd LLMStatsBlazor && dotnet build -c Release

# No automated tests - test manually in browser
# No linting configured - follow style guidelines below
```

## Code Style Guidelines

### C# / Blazor

- Use file-scoped namespaces
- Prefer `var` when type is obvious
- Use PascalCase for classes, methods, properties; camelCase for variables
- Async methods suffixed with `Async`
- Component parameters use `[Parameter]` attribute
- Use nullable reference types (`?` suffix)
- Error handling: check for null before DOM operations, use try-catch for external calls

### HTML

- Semantic HTML5 elements (`nav`, `main`, `section`)
- BEM-like naming: `block block--modifier` or `dash-card`, `dash-ranking-item`
- Use `data-*` attributes for JS hooks (`data-tab`, `data-filter`)
- Include ARIA attributes for accessibility
- Inline SVGs with `viewBox="0 0 24 24"`

### CSS

- CSS custom properties in `:root`: `--primary`, `--accent`, `--surface`, `--border`, `--text-primary`
- Kebab-case class names
- 4px grid system (multiples of 4 for spacing)
- Fonts: `Inter` (body), `Space Grotesk` (headings)
- Breakpoints: 1400px, 1024px, 768px, 480px
- Transitions: `all 0.2s/0.3s ease` or `cubic-bezier(0.4, 0, 0.2, 1)`
- Hover: `transform: translateY(-2px)`, `border-color: var(--primary)`
- Flexbox for components, Grid for page layouts
- `-webkit-` prefixes for gradients/backdrop-filter

### JavaScript

- `const` for immutables, `let` for mutables (never `var`)
- camelCase variables/functions, PascalCase classes
- ES6+: arrow functions, template literals, destructuring
- Use `document.querySelector(All)()` and `element.dataset.*`
- Avoid inline `onclick`; use `addEventListener` with arrow functions
- Render functions: early return if element not found
- Array methods: `map()`, `filter()`, `forEach()`
- Initialize in `DOMContentLoaded`

### Naming Conventions

| Type | Pattern | Example |
|------|---------|---------|
| Classes | PascalCase | `Particle` |
| Functions | camelCase | `initParticles()` |
| Variables | camelCase | `filteredData` |
| DOM elements | descriptive | `heroRankings` |
| Data arrays | descriptive | `openSourceModels` |
| Constants | UPPER_SNAKE | `PARTICLE_COUNT` |

### State Management

- Centralized state object: `state = { category, type, displayCount }`
- Update state through functions that trigger re-renders
- Reset state on context switches

### Utilities

- `debounce(func, wait=300)` for performance events
- `sanitizeString(str)` using textContent to prevent XSS
- `escapeRegExp(string)` for safe regex
- `highlightText(text, term)` with `<mark>` elements

### Error Handling

- Always check `if (!element) return;` before DOM ops
- Graceful degradation for missing browser features
- Validate data before rendering
- Try-catch for external API calls

### Performance

- Use `requestAnimationFrame()` for animations
- Debounce scroll/resize handlers
- Cache DOM queries in variables
- Minimize DOM manipulation in loops
- Lazy load non-critical components

### Accessibility

- ARIA labels on interactive elements
- Keyboard navigation support
- Focus management for modals/dropdowns
- WCAG AA color contrast
- Screen reader-friendly text

## File Organization

- `index.html`: Page structure
- `styles.css`: All styles with variables and responsive breakpoints
- `script.js`: Data, logic, event listeners (data arrays at top)

## Adding New Features

1. Add data array in `script.js`
2. Create render function with null checks
3. Add init call in `DOMContentLoaded`
4. Add HTML with BEM classes
5. Add CSS using existing variables
6. Update state object if needed
7. Add event listeners

## Development Tools

### GitHub Pull Request Extension (VS Code)

The user has installed the GitHub Pull Request extension for faster productivity. **Agents should leverage this when working with git:**

**Capabilities:**
- Create PRs directly from VS Code without opening browser
- Review PR diffs inline with code comments
- Check CI/build status within the editor
- Checkout and test PR branches locally
- Merge and approve PRs from VS Code

**When to use:**
- After committing changes that need review
- Before pushing large feature branches
- When user asks to "create a PR" or "review changes"
- To quickly check if CI passes on recent commits

**Commands available:**
- `gh pr create` - Create PR from current branch
- `gh pr checkout <number>` - Checkout a PR locally
- `gh pr view` - View PR details in terminal
- Use VS Code Command Palette: `GitHub Pull Requests: Create Pull Request`
