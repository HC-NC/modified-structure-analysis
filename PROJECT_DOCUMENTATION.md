# Modified Structure Analysis — Project Documentation

## 1. Overview

**Modified Structure Analysis** is a Windows desktop application (.NET 10.0 / WinForms) for remote sensing raster data analysis, kernel density estimation, and rule-based classification. It supports multi-spectral imagery from satellite and airborne sensors (Landsat, Sentinel, etc.) as well as tabular XYZ data from microprobe or laboratory instruments.

Key capabilities:
- Multi-format raster import via GDAL and CSV/TXT parsing
- Interactive false-color composite visualization
- Statistical analysis: histograms, KDE, scatter plots, correlation, profiles
- Two-stage rule-based classification with KDE density operands
- 10 kernel functions and 4 bandwidth optimization methods
- Geospatial and plot export (GeoTIFF, PNG+world file, SVG, PDF, CSV, JSON)

---

## 2. Architecture

```
┌──────────────────────────────────────────────────────┐
│                    Forms (UI)                        │
│  MainForm, SettingsForm, ClassAnalysisForm,          │
│  Viewport, RuleEditorForm, ExportDialogs, ...        │
├──────────────────────────────────────────────────────┤
│                   Engine                             │
│  ClassificationEngine — core classification logic    │
├──────────────────────────────────────────────────────┤
│                  Services                            │
│  KernelFunctions  BandwidthOptimizer                 │
│  BandStatisticsComputer  BandKdeEstimator            │
│  ClassificationExporter  PlotExportService           │
│  ResultRenderer  PaletteGenerator  DensitySource     │
├──────────────────────────────────────────────────────┤
│                   Models                             │
│  Band  ClassificationResult  ClassificationRule      │
│  ClassStatistics  Condition  GeoTransform  ...       │
├──────────────────────────────────────────────────────┤
│                   Config                             │
│  AppSettings  PlotSettings                           │
└──────────────────────────────────────────────────────┘
```

**Layers (top to bottom):**

| Layer | Responsibility |
|-------|---------------|
| **Forms** | WinForms UI: file loading, settings, plot display, classification management, export dialogs, viewport rendering |
| **Engine** | Pure classification logic, independent of UI |
| **Services** | Stateless computation: kernel evaluation, bandwidth optimization, statistics, export, rendering |
| **Models** | Data containers with minimal logic: bands, rules, results, geotransform |
| **Config** | Global settings (singleton, JSON-persisted) |

---

## 3. Project Structure

```
Config/
├── AppSettings.cs          — Global user settings singleton (JSON persistence)
└── PlotSettings.cs         — Per-plot axis/legend/grid settings

Engine/
└── ClassificationEngine.cs — Core classification: rule evaluation, density caching, two-stage pipeline

Forms/
├── MainForm.cs             — Main window: file loading, all plots, classification, export (2176 lines)
├── Viewport.cs             — Interactive image display: pan, zoom, interpolation, linked views
├── SettingsForm.cs         — Settings dialog (kernel, bandwidth, histogram, language, etc.)
├── ClassAnalysisForm.cs    — Per-class statistics, pixel table, KDE/scatter plots
├── RuleEditorForm.cs       — Rule editor (list of conditions)
├── ConditionEditorForm.cs  — Single condition editor (left operand, operator, right operand)
├── BandSelectionForm.cs    — Multi-select band listbox
├── TextTableColumnSelector.cs — CSV column → X/Y/Band mapping dialog
├── DelimiterSelector.cs    — Tab/comma/semicolon choice dialog
├── GraphExportDialog.cs    — Plot export options (format, size, DPI, quality)
├── ExportClassificationDialog.cs — Classification export options (GeoTIFF/PNG, stats)
├── TwoImageViewForm.cs     — Side-by-side viewport comparison
└── ConditionDisplayItem.cs — Formats a Condition as a display string

Models/
├── Band.cs                 — Raster band: pixel values, statistics, spatial lookup, GDAL source
├── BandStatisticsComputer.cs → (moved to Services/)
├── ClassificationResult.cs — Classification output: class indices, width/height, palette
├── ClassificationRule.cs   — One classification rule: conditions, color, name, enums
├── ClassStatistics.cs      — Per-class statistics (raw + z-score) from classification result
├── CompareTarget.cs        — Right-hand side of a condition (constant or density operand)
├── Condition.cs            — One condition: left operand, operator, right operand
├── ExportOptions.cs        — Export enums and options classes
└── GeoTransform.cs         — Georeferencing: origin, pixel size, rotation, CRS

Services/
├── KernelFunctions.cs      — 10 kernel function implementations + default bandwidth formula
├── BandwidthOptimizer.cs   — 4 bandwidth selection methods (subsampled to 2000)
├── BandStatisticsComputer.cs — Computes per-band statistics (sum, min/max, variance, skewness, kurtosis, bandwidth)
├── BandKdeEstimator.cs     — KDE estimate at a normalized value for a band (used for plots)
├── DensitySource.cs        — IDensitySource interface + 3 implementations (Global, PerClass, ZScore)
├── ClassificationExporter.cs — Export classification to GeoTIFF, PNG, world file, stats
├── PlotExportService.cs    — Export OxyPlot models to PNG/JPEG/SVG/PDF
├── ResultRenderer.cs       — Render ClassificationResult to Bitmap
└── PaletteGenerator.cs     — HSV palette generation for first and second stages

Properties/
├── Resources.resx          — Default (English) localized strings
├── Resources.en.resx       — English satellite resource strings
├── Resources.ru.resx       — Russian satellite resource strings
└── Resources.Designer.cs   — Auto-generated strongly-typed resource accessors

Resources/
├── favicon.ico             — Application icon
├── red_sqaure.png          — Color swatch (used in UI)
├── green_sqaure.png        — Color swatch (used in UI)
└── blue_sqaure.png         — Color swatch (used in UI)

Program.cs                  — Entry point: GDAL init, culture setup, main form launch
```

---

## 4. Mathematical Foundations

### 4.1 Kernel Density Estimation (KDE)

KDE estimates the probability density function of a random variable. Given observations $(x_1, x_2, ..., x_n)$ and a kernel function $\phi$, the density at point $x$ is:

$$
\hat{f}_c(x) = \frac{1}{n \cdot c} \sum_{i=1}^{n} \phi\left(\frac{x - x_i}{c}\right)
$$

where $c$ is the bandwidth (smoothing parameter) and $\phi$ is a symmetric kernel function satisfying:

$$
\int_{-\infty}^{\infty} \phi(u) \, du = 1, \quad \int_{-\infty}^{\infty} u \, \phi(u) \, du = 0, \quad \int_{-\infty}^{\infty} u^2 \, \phi(u) \, du < \infty
$$

**Multivariate KDE** (product kernel, d dimensions):

$$
\hat{f}_\mathbf{c}(\mathbf{x}) = \frac{1}{n \prod_{j=1}^{d} c_j} \sum_{i=1}^{n} \prod_{j=1}^{d} \phi_j\left(\frac{x_j - x_{ij}}{c_j}\right)
$$

**Product density** for disjoint bands (assuming independence):

$$
\hat{f}_p(x_1, x_2) = \hat{f}_{c_1}(x_1) \cdot \hat{f}_{c_2}(x_2)
$$

#### Z-Score Density

For second-stage classification, per-class z-scores are computed:

$$
z_i = \frac{x_i - \mu_c}{\sigma_c}
$$

where $\mu_c$ and $\sigma_c$ are the mean and standard deviation of class $c$. KDE is then evaluated on the z-score domain using per-class bandwidth.

### 4.2 Kernel Functions (all 10)

| Kernel | Formula $\phi(u)$ | Support |
|--------|-------------------|---------|
| **Uniform** | $\frac{1}{2}$ | $\|u\| \leq 1$ |
| **Triangular** | $1 - \|u\|$ | $\|u\| \leq 1$ |
| **Epanechnikov** | $\frac{3}{4}(1 - u^2)$ | $\|u\| \leq 1$ |
| **Quartic (Biweight)** | $\frac{15}{16}(1 - u^2)^2$ | $\|u\| \leq 1$ |
| **Triweight** | $\frac{35}{32}(1 - u^2)^3$ | $\|u\| \leq 1$ |
| **Tricube** | $\frac{70}{81}(1 - \|u\|^3)^3$ | $\|u\| \leq 1$ |
| **Gaussian** | $\frac{1}{\sqrt{2\pi}} e^{-u^2/2}$ | $\mathbb{R}$ |
| **Cosine** | $\frac{\pi}{4} \cos\left(\frac{\pi}{2} u\right)$ | $\|u\| \leq 1$ |
| **Logistic** | $\frac{1}{e^u + 2 + e^{-u}}$ | $\mathbb{R}$ |
| **Sigmoid** | $\frac{2}{\pi} \cdot \frac{1}{e^u + e^{-u}}$ | $\mathbb{R}$ |

The Epanechnikov kernel is the default and is optimal in the AMISE sense (minimizing asymptotic mean integrated squared error). Gaussian is used as the reference kernel for LSCV and Direct Plug-In bandwidth methods (analytic convolution properties).

### 4.3 Bandwidth Selection

Four methods are implemented in `BandwidthOptimizer.cs`. For the three iterative methods, data is subsampled to a maximum of 2000 points for performance.

#### Silverman's Rule of Thumb (Default)

$$
c_{\text{RoT}} = 0.951889 \cdot \sigma \cdot n^{-1/5}
$$

where $\sigma$ is the sample standard deviation and $n$ is the number of data points. This follows the optimal bandwidth for a Gaussian reference distribution scaled to the Epanechnikov kernel.

#### Least-Squares Cross-Validation (LSCV)

Minimizes the Mean Integrated Squared Error via cross-validation:

$$
\text{CV}(c) = \frac{R(\phi)}{n \cdot c} + \frac{1}{n^2 c} \sum_{i=1}^{n} \sum_{j=1}^{n} \left[ \phi^{*}\phi\left(\frac{x_i - x_j}{c}\right) - 2\phi\left(\frac{x_i - x_j}{c}\right) \right]
$$

where $R(\phi) = \int \phi(u)^2 du = 1/(2\sqrt{\pi})$ for the Gaussian kernel, and $\phi^{*}\phi$ denotes the convolution:

$$
\phi^{*}\phi(v) = \frac{e^{-v^2/4}}{\sqrt{4\pi}}
$$

The search grid spans $[0.1 c_0, 10 c_0]$ with 50 equally-spaced steps.

#### Direct Plug-In (Sheather–Jones)

A two-stage estimator that directly approximates the optimal bandwidth:

$$
c_{\text{SJ}} = \left[ \frac{R(\phi)}{\sigma_K^4 \, n \, \hat{\psi}_4} \right]^{1/5}
$$

where $\sigma_\phi^2 = \int u^2 \phi(u) du = 1/5$ and $R(\phi) = 3/5$ for Epanechnikov. The pilot estimate $\hat{\psi}_4$ is the 4th derivative of the density:

$$
\hat{\psi}_4 = \frac{2}{n(n-1) c_0^5} \sum_{i=1}^{n} \sum_{j>i} G^{(4)}\left(\frac{x_i - x_j}{c_0}\right)
$$

where $G^{(4)}$ is the 4th derivative of the standard Gaussian kernel:

$$
G^{(4)}(x) = (x^4 - 6x^2 + 3) \cdot \frac{e^{-x^2/2}}{\sqrt{2\pi}}
$$

The result is clamped to a reasonable range, falling back to the rule-of-thumb if degenerate.

#### Leave-One-Out Likelihood

Maximizes the leave-one-out cross-validated log-likelihood:

$$
\text{NLL}(c) = -\sum_{t=1}^{n} \log\left[ \frac{1}{(n-1)c} \sum_{s \neq t} \phi\left(\frac{x_t - x_s}{c}\right) \right]
$$

Unline LSCV and Direct Plug-In, this method uses the **per-band kernel type** (not a fixed Gaussian reference), making it sensitive to the chosen kernel's shape. The search grid spans $[0.1 c_0, 10 c_0]$ with 50 steps.

### 4.4 Classification

#### RulePerClass Mode

Rules are evaluated in order. The first rule whose all conditions are satisfied assigns the class index:

$$
\text{Class}(p) = \min\{ i : \text{Rule}_i \text{ is enabled} \land \bigwedge_{c \in \text{Rule}_i.\text{Conditions}} \text{Evaluate}(c, p) = \text{true} \}
$$

If no rule matches, the pixel is left as "undefined" (index -1).

#### DirectCheck Mode

All rules are evaluated independently. Each matching rule sets one bit in the class index:

$$
\text{Class}(p) = \sum_{i=0}^{N-1} b_i \cdot 2^i, \quad b_i = \begin{cases} 1 & \text{if Rule}_i \text{ matches} \\ 0 & \text{otherwise} \end{cases}
$$

This produces up to $2^N$ classes, assigned an HSV-generated palette.

#### Two-Stage Classification

1. **First stage**: run RulePerClass or DirectCheck on all pixels
2. **Compute class statistics**: per-band statistics (mean, sigma, skewness, kurtosis, bandwidth) for each first-stage class
3. **Second stage**: for each pixel, evaluate second-stage rules using per-class statistics (z-score or regular KDE within the class)

The final class index is:

$$
\text{FinalClass}(p) = \text{FirstClass}(p) \times N_{\text{second}} + \text{SecondClass}(p)
$$

#### Condition Evaluation

Each condition compares a left operand against a right operand using a comparison operator (>, <, >=, <=, =):

$$
\text{Left}(p) \bowtie \text{Right}(p)
$$

Operand types:
- **ChannelValue**: raw pixel value from a band
- **ChannelZScore**: $z = (x - \mu_b) / \sigma_b$ for band $b$
- **Single**: KDE at the pixel's value for one band, $\hat{f}_c(x_b)$
- **Product**: product of single KDEs across multiple bands, $\prod \hat{f}_{c_j}(x_j)$
- **Multivariate**: product-kernel KDE across multiple bands evaluated jointly, $\frac{1}{n \prod c_j} \sum \phi_j(\frac{x_j - x_{ij}}{c_j})$
- **ZScoreSingle / ZScoreProduct / ZScoreMultivariate**: same as above but on per-class z-score values (second stage only)

---

## 5. Data Flow

### 5.1 File Loading

```
User selects file (GeoTIFF/IMG/CSV/TXT)
        │
        ▼
GDAL: Gdal.Open()                        CSV/TXT: StreamReader + column mapping
        │                                         │
        ▼                                         ▼
Read raster bands → Band objects          Parse rows → sort into grid → Band objects
        │                                         │
        └──────────────┬──────────────────────────┘
                       ▼
         BackgroundWorker (statsWorker)
         - BandStatisticsComputer.Compute() for each band
         - BandwidthOptimizer.Compute() for each band
         - Correlation matrix computation
                       │
                       ▼
         UI update: histograms, KDE, scatter, viewport
```

### 5.2 Classification Pipeline

```
User configures rules (RuleEditorForm)
        │
        ▼
ClassificationEngine._bands = loaded bands
ClassificationEngine._rules = configured rules
        │
        ▼
   ┌────┴────┐
   │ Stage 1 │
   └────┬────┘
        │
        ▼
ClassificationResult (first stage)
        │
        ▼
   ┌────────────┐
   │ Compute    │
   │ ClassStats │ ← ClassStatistics.ComputeFromResult()
   └────────────┘
        │
        ▼
   ┌────┴────┐
   │ Stage 2 │ (if second rules configured)
   └────┬────┘
        │
        ▼
ClassificationResult (final: firstClass × ruleCount + secondClass)
        │
        ▼
Export: GeoTIFF / PNG / PNG+world / stats (CSV/TXT/JSON)
```

### 5.3 KDE Plotting

```
MainForm requests KDE plot for band
        │
        ▼
BandKdeEstimator.Estimate(band, normalizedValue)
        │
        ▼
For each pixel:
  value = band.GetNormalizedValue(pixelIndex)
  if not NaN:
    u = (queryValue - value) / band.NormalizeKernelC
    density += KernelFunctions.GetKernel(band.KernelType, u)
        │
        ▼
density /= (band.Count * band.NormalizeKernelC)
```

**Note**: KDE plots use normalized [0,1] values and `NormalizeKernelC` for visual consistency. The classification engine uses raw values and `KernelC` for accuracy.

---

## 6. Configuration Reference

### 6.1 AppSettings (JSON persisted)

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `DefaultKernelType` | `KernelType` | `Epanechnikov` | Kernel used for new bands and fallback |
| `BandwidthMethod` | `BandwidthMethod` | `RuleOfThumb` | Bandwidth optimization method |
| `Language` | `string` | Current culture | UI language code (e.g. `"ru-RU"`, `"en"`) |
| `HistogramBinsRule` | `HistogramBinsRule` | `BrooksCarruther` | Bin count selection rule |
| `HistogramCustomBins` | `int` | 50 | Custom bin count |
| `HistogramShowCumulative` | `bool` | `true` | Show cumulative frequency overlay |
| `HistogramBarColor` | `string` | `"#4CAF50"` | Bar fill color (hex) |
| `HistogramLineColor` | `string` | `"#F44336"` | Cumulative line color (hex) |
| `DefaultRedBand` | `int` | 0 | Default red channel index |
| `DefaultGreenBand` | `int` | 1 | Default green channel index |
| `DefaultBlueBand` | `int` | 2 | Default blue channel index |
| `Interpolation` | `ViewportInterpolation` | `NearestNeighbor` | Viewport image interpolation |
| `GraphShowAxisLabels` | `bool` | `true` | Show axis titles on plots |
| `GraphShowLegend` | `bool` | `true` | Show legend on plots |
| `UndefinedThreshold` | `float` | 0 | Threshold for undefined class |

**Storage location**: `%LOCALAPPDATA%/ModifiedStructureAnalysis/settings.json`

### 6.2 Histogram Bin Rules

| Rule | Formula |
|------|---------|
| **Sturges** | $k = \lceil \log_2(n) + 1 \rceil$ |
| **Brooks-Carruther** | $k = \lceil 2 \sqrt[3]{n} \rceil$ (default) |
| **Heinhold-Heide** | $k = \lceil \sqrt{2n} \rceil$ |
| **Custom** | User-specified integer |

### 6.3 Enums

**`KernelType`**: Uniform, Triangular, Epanechnikov, Quartic, Triweight, Tricube, Gaussian, Cosine, Logistic, Sigmoid

**`BandwidthMethod`**: RuleOfThumb, LeastSquaresCrossValidation, DirectPlugIn, LeaveOneOutLikelihood

**`ViewportInterpolation`**: NearestNeighbor, Bilinear, Bicubic, HighQualityBilinear, HighQualityBicubic

**`HistogramBinsRule`**: Sturges, BrooksCarruther, HeinholdHeide, Custom

**`ClassificationMode`**: RulePerClass, DirectCheck

**`DensityType`**: ChannelValue, ChannelZScore, Single, Product, Multivariate, ZScoreSingle, ZScoreProduct, ZScoreMultivariate

**`ComparisonOperator`**: GreaterThan, LessThan, GreaterOrEqual, LessOrEqual, Equal

**`GraphExportFormat`**: Png, Jpeg, Svg, Pdf

**`ClassificationExportFormat`**: GeoTiff, Png, PngWithWorldFile

**`StatsExportFormat`**: Csv, Txt, Json

---

## 7. Key Components

### 7.1 Band (Models/Band.cs)

Core data model representing one spectral band:
- **Pixel storage**: flat `float[]` array of size `width × height`, NaN for NoData
- **Statistics**: min, max, mean, variance, skewness, kurtosis, kernel bandwidth (`KernelC` for raw values, `NormalizeKernelC` for [0,1] normalized)
- **Spatial**: optional `PointF[]` for irregular grids (CSV import), `GeoTransform` reference
- **GDAL**: lazy pixel loading via `EnsurePixelValuesLoaded()` (reads only when classification needs it)
- **Kernel type**: per-band `KernelType` (default: `AppSettings.Instance.DefaultKernelType`)

### 7.2 ClassificationEngine (Engine/ClassificationEngine.cs)

Core logic (~578 lines):
- **Density caching**: `ConcurrentDictionary` for single-band and multivariate densities (avoids redundant KDE computation)
- **Z-score caching**: flat `float[]` indexed by `bandIndex × pixelCount` with lazy evaluation
- **Multivariate KDE subsampling**: random sample of ~`sqrt(n) × 4` pixels for global density (avoids O(n²) per pixel)
- **Progress reporting**: via `IProgress<int>` delegates passed through the call chain

### 7.3 DensitySource (Services/DensitySource.cs)

Interface `IDensitySource` abstracts where density values come from:

- **`GlobalDensitySource`**: uses band-level statistics (min/max/mean/KernelC) — first stage
- **`PerClassRegularDensitySource`**: uses per-class statistics and pixel indices — second stage, regular mode
- **`ZScoreDensitySource`**: uses per-class z-score statistics — second stage, z-score mode

The engine creates the appropriate source based on `DensityType` flags and stage context.

### 7.4 Viewport (Forms/Viewport.cs)

Interactive image display control:
- **Pan**: mouse drag updates image offset
- **Zoom**: mouse scroll wheel zooms about screen center using formula:  
  $x_{\text{new}} = x_{\text{old}} + \text{screenCenter} \cdot (1/z_{\text{new}} - 1/z_{\text{old}})$
- **Interpolation**: configurable via `ViewportInterpolation` enum mapped to `System.Drawing.Drawing2D.InterpolationMode`
- **Linking**: `OnLinkZoom`/`OnLinkMove` events synchronize two viewports in `TwoImageViewForm`
- **Cursor crosshair**: external cursor position display

### 7.5 GeoTransform (Models/GeoTransform.cs)

Georeferencing model for raster-to-world coordinate mapping:

$$
\begin{bmatrix} X \\ Y \end{bmatrix} = \begin{bmatrix} \text{OriginX} + \text{PixelSizeX} \cdot \text{col} + \text{Rotation1} \cdot \text{row} \\ \text{OriginY} + \text{Rotation2} \cdot \text{col} + \text{PixelSizeY} \cdot \text{row} \end{bmatrix}
$$

Reverse mapping (world to pixel) solves the 2×2 linear system. Supports:
- GDAL 6-element GeoTransform array (compatible with `GDALDataset.GetGeoTransform()`)
- OGC WKT projection string
- World file format (.pgw) for PNG export

---

## 8. Localization

Localization uses .NET resource files (`Properties/Resources.resx`) with satellite assemblies.

**Mechanism**:
1. `Program.Main()` reads `AppSettings.Instance.Language` on startup
2. Sets `Thread.CurrentCulture`, `CurrentUICulture`, `DefaultThreadCurrentCulture`, `DefaultThreadCurrentUICulture`
3. All UI strings accessed via `Properties.Resources.ResourceManager` pick up the correct satellite assembly
4. Language selection in SettingsForm scans subdirectories of the application directory for `*.resources.dll`

**Resource files**:
- `Resources.resx` — default (English, compile-time fallback)
- `Resources.en.resx` — English satellite (code: `en`)
- `Resources.ru.resx` — Russian satellite (code: `ru-RU`)

**Scope**: ~65 resource keys covering status messages, error messages, tooltips, chart labels, export format names, and UI element text. File-dialog filters and PropertyGrid attribute strings are intentionally excluded (compile-time constraints / data-format interchange reasons).

---

## 9. Dependencies

| NuGet Package | Version | Purpose |
|---------------|---------|---------|
| `MaxRev.Gdal.Core` | 3.13.0.527 | GDAL core bindings |
| `MaxRev.Gdal.WindowsRuntime.Minimal` | 3.13.0.527 | GDAL Windows runtime |
| `OxyPlot.WindowsForms` | 2.2.0 | Scientific plotting |
| `MinVer` | 7.0.0 | Auto-versioning from git tags |

Target framework: `net10.0-windows` with `UseWindowsForms=true` and `AllowUnsafeBlocks=true` (for `LockBits` bitmap rendering).
