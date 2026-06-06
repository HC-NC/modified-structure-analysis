# Roadmap

## v1.0.0 — Released

### Core Application
- Windows Forms desktop application with OxyPlot charting
- Multi-format raster import (GeoTIFF, ERDAS IMAGINE, CSV/TXT with column mapping)
- Multi-channel image support with auto-detection by filename mask
- RGB false-color composite with configurable band mapping
- Interactive viewport with pan, zoom, scroll wheel, arrow keys, interpolation modes
- Linked viewport synchronization for side-by-side comparison

### Statistics & Visualization
- Histogram with configurable bin rules: Sturges, Brooks-Carruther, Heinhold-Heide, Custom
- Cumulative frequency overlay, configurable bar/line colors
- Kernel Density Estimation (KDE) with 10 kernel functions
  - Single-band, Product, Multivariate modes
  - Z-score variants for each mode
- Scatter plots with band-to-band comparison
- Correlation matrix across all bands
- Profile tool (horizontal/vertical cross-sections)

### Classification Engine
- Two-stage classification (primary → secondary)
- Two modes: **RulePerClass** (first matching rule wins) and **DirectCheck** (bitmask)
- Density operand types: raw channel value, z-score, single/product/multivariate KDE
- Z-score variants for second-stage per-class analysis
- NaN/NoData pixel detection and skip
- Random subsampling for global multivariate KDE
- Density caching via `ConcurrentDictionary`
- HSV palette generation for arbitrary class counts
- Second-stage palette derived from first-stage colors

### Bandwidth Optimization
- **Silverman's Rule of Thumb** — `0.951889 · σ · n^{-1/5}`
- **Least-Squares Cross-Validation (LSCV)** — grid search minimizing MISE
- **Direct Plug-In (Sheather–Jones)** — two-stage estimation via 4th derivative
- **Leave-One-Out Likelihood** — maximizes cross-validated log-likelihood, respects per-band kernel type
- Automatic of subsampling to 2000 samples for all iterative methods

### Geospatial & Export
- Classification export: GeoTIFF (with RAT, LZW compression, CRS), PNG, PNG + world file (.pgw)
- Graph export: PNG, JPEG (configurable quality), SVG, PDF
- Statistics export: CSV, TXT, JSON
- Customizable export dimensions, DPI, and title

### Settings & Localization
- JSON-persisted settings in `%LOCALAPPDATA%/ModifiedStructureAnalysis/settings.json`
- Settings dialog with tabs: General, Histogram, Classification
- Default kernel type, bandwidth method, histogram bin rule, colors
- Viewport interpolation mode (NearestNeighbor, Bilinear, Bicubic, HighQualityBilinear, HighQualityBicubic)
- Default RGB band indices with `Math.Clamp`
- Language selection from installed satellite assemblies
- ~65 localized resource strings for English and Russian
- Language applied at startup before any form is created

### User Interface
- Application favicon on all forms (loaded from executable)
- Second-stage class filter with select-all, color preview, and compact class indexing
- Filtered classes physically excluded from result (no gaps in palette)

### Data Robustness
- Dual-culture `TryParse` for CSV/TXT import (supports both dot and comma decimal separators via `InvariantCulture` + `ru-RU` fallback)

### Code Quality
- Namespace organization: `Models`, `Services`, `Engine`, `Config`, `Forms`
- Interface-based density abstraction (`IDensitySource`) with 3 implementations
- Background worker for statistics and bandwidth computation
- Bandwidth automatically recalculated when settings or kernel type changes
- Status bar messages for long operations
- LockBits-based bitmap rendering (no SetPixel)
- GdalConfigured geotransform and projection support
- MinVer-based automatic versioning from git tags

---

## Future Plans

### v1.1
- [ ] Project file (.msaproj) — save/load full workspace state (bands, rules, settings)
- [ ] MRU (Most Recently Used) file list
- [ ] Toolbar with icons
- [ ] File drag & drop support
- [ ] Context menu on band ListBox
- [ ] Info panel with data summary
- [ ] Hotkeys and keyboard shortcuts
- [ ] About dialog with version, authors, license info
- [ ] Help / User manual

### v1.2
- [ ] Save/load classification configuration
- [ ] UI reorganization: fix control layout, sizing, tab organization
- [ ] Excel (.xlsx) import
- [ ] DBF import for legacy GIS formats
- [ ] Improved visual style and color scheme

### v2.0
- [ ] Memory optimization: lazy pixel data unloading
- [ ] Tiled image processing (1024×1024 tiles) for large scenes
- [ ] KD-tree acceleration for multivariate KDE
- [ ] short[] class indices for reduced memory footprint
- [ ] Tiled classification result rendering
- [ ] Random sampling optimization for large KDE
- [ ] Dependency Injection (Microsoft.Extensions.DependencyInjection)
- [ ] Logging via ILogger
- [ ] MVVM pattern for complex forms
