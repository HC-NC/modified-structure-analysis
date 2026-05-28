# Modified Structure Analysis

A Windows desktop application for remote sensing raster data analysis, classification, and visualization.

Built with .NET 10.0, Windows Forms, OxyPlot, and GDAL.

## Features

### Raster Data Import
- GeoTIFF (.tif/.tiff) via GDAL
- ERDAS IMAGINE (.img)
- CSV/TXT with column mapping (X, Y, band columns) and configurable delimiter
- Multi-band auto-detection by filename mask (e.g., `Landsat_B*.tif`)
- Irregular grid interpolation and averaging for coincident positions

### Visualization
- False-color composite with any 3 bands mapped to RGB
- Interactive viewport with pan, zoom (mouse wheel + arrow keys), and interpolation modes (NearestNeighbor, Bilinear, Bicubic, HighQualityBilinear, HighQualityBicubic)
- Linked viewport synchronization for side-by-side comparison
- Histogram with configurable bin rules (Sturges, Brooks-Carruther, Heinhold-Heide, Custom)
- Kernel Density Estimation (KDE) plots: single-band, product, multivariate
- Scatter plots (band vs band)
- Correlation matrix
- Profile tool (horizontal/vertical cross-section)

### Classification
- Two-stage classification pipeline
- **RulePerClass** mode — first matching rule assigns the class
- **DirectCheck** mode — all rules evaluated; class = bitmask of matches (2^N classes)
- Operand types: raw channel value, z-score, single/product/multivariate KDE, z-score KDE variants
- 10 kernel functions for KDE: Uniform, Triangular, Epanechnikov, Quartic, Triweight, Tricube, Gaussian, Cosine, Logistic, Sigmoid
- 4 bandwidth selection methods: Rule of Thumb, Least-Squares Cross-Validation, Direct Plug-In (Sheather–Jones), Leave-One-Out Likelihood
- NaN/NoData pixel handling
- HSV auto-palette for arbitrary class counts

### Geospatial Export
- Classification as GeoTIFF with RAT (Raster Attribute Table), LZW compression, and embedded CRS
- PNG + world file (.pgw) for GIS overlay
- Statistics export to CSV, TXT, JSON

### Plot Export
- PNG, JPEG (configurable quality), SVG, PDF
- Adjustable dimensions and DPI

### Localization
- English (default) and Russian interface
- Language switchable in Settings

### Settings
- JSON-persisted settings at `%LOCALAPPDATA%/ModifiedStructureAnalysis/settings.json`
- Default kernel type, bandwidth method, histogram style, viewport interpolation, RGB band mapping

## Quick Start

```
dotnet publish -c Release -r win-x64 --self-contained true
```

Or download a pre-built release from the [Releases](https://github.com/HC-NC/modified-structure-analysis/releases) page.

1. **File → Open** → select a GeoTIFF, IMG, CSV, or TXT file
2. Explore bands via histogram, KDE, scatter plot tabs
3. Define classification rules in the **First** tab
4. Click **Classify** to run (optional: configure **Second** stage)
5. **Export** results as GeoTIFF, PNG, or statistics

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| .NET | 10.0-windows | Runtime + Windows Forms |
| OxyPlot.WindowsForms | 2.2.0 | Plotting and charting |
| MaxRev.Gdal.Core | 3.13.0.527 | GDAL raster I/O |
| MaxRev.Gdal.WindowsRuntime.Minimal | 3.13.0.527 | GDAL Windows runtime |
| MinVer | 7.0.0 | Automatic versioning from git tags |

## Project Structure

```
Config/       — AppSettings, PlotSettings, enums
Engine/       — ClassificationEngine (core classification logic)
Forms/        — WinForms UI (MainForm, Viewport, dialogs)
Models/       — Data models (Band, ClassificationRule, GeoTransform, etc.)
Services/     — Computation services (KernelFunctions, BandwidthOptimizer, exporters)
Properties/   — Resources (localized strings, embedded images)
```

## License

MIT — see [LICENSE](LICENSE).
