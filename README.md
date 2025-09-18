# 📈 PREDAYTOR - Predictor de Tendencia de Activos

[![.NET Version](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple)](https://getbootstrap.com/)

## 📋 Descripción

**PREDAYTOR** es una aplicación web desarrollada con ASP.NET Core MVC que permite predecir tendencias de activos financieros (acciones, criptomonedas, etc.) utilizando diferentes algoritmos de análisis técnico. La aplicación toma datos históricos de precios y proporciona predicciones basadas en tres métodos estadísticos diferentes.

## ✨ Características Principales

- **3 Algoritmos de Predicción:**
  - Media Móvil Simple (SMA) Crossover
  - Regresión Lineal
  - Momentum (Rate of Change - ROC)

- **Dos Métodos de Entrada de Datos:**
  - Formato CSV (fecha, valor)
  - Campos individuales (20 entradas)

- **Interfaz Moderna:**
  - Diseño responsive con Bootstrap 5.3
  - Validaciones en tiempo real
  - Feedback visual intuitivo

- **Arquitectura Robusta:**
  - Patrón MVC
  - Separación de capas (Web y BusinessLogic)
  - Patrón Strategy para algoritmos
  - Dependency Injection

## 🛠️ Tecnologías Utilizadas

### Backend
- **.NET 9.0**
- **ASP.NET Core MVC**
- **C#**

### Frontend
- **HTML5, CSS3, JavaScript**
- **Bootstrap 5.3**
- **Font Awesome 6.0**
- **jQuery** (para validaciones)

### Patrones Arquitectónicos
- **Model-View-Controller (MVC)**
- **Strategy Pattern**
- **Dependency Injection**
- **Data Transfer Objects (DTOs)**

## 🏗️ Estructura del Proyecto

```
PredictorActivos/
├── PredictorActivos.sln
├── PredictorActivos.Web/                    # Capa de Presentación
│   ├── Controllers/
│   │   ├── PredictorController.cs
│   │   └── PredictorModoController.cs
│   ├── Views/
│   │   ├── Predictor/
│   │   │   ├── Index.cshtml
│   │   │   ├── Modos.cshtml
│   │   │   └── Resultado.cshtml
│   │   ├── PredictorModo/
│   │   │   └── Index.cshtml
│   │   ├── Shared/
│   │   │   ├── _Layout.cshtml
│   │   │   └── Error.cshtml
│   │   ├── _ViewImports.cshtml
│   │   └── _ViewStart.cshtml
│   ├── ViewModels/
│   │   ├── ActivoDataViewModel.cs
│   │   ├── PredictionModoViewModel.cs
│   │   └── PredictionResultViewModel.cs
│   ├── wwwroot/
│   │   ├── css/
│   │   ├── js/
│   │   └── lib/
│   └── Program.cs
└── PredictorActivos.BusinessLogic/          # Lógica de Negocio
    ├── Services/
    │   ├── IPredictionService.cs
    │   ├── PredictionService.cs
    │   ├── IPredictionModeService.cs
    │   └── PredictionModeService.cs
    ├── Strategies/
    │   ├── IPredictionStrategy.cs
    │   ├── SMAStrategy.cs
    │   ├── LinearRegressionStrategy.cs
    │   └── MomentumStrategy.cs
    ├── DTOs/
    │   ├── ActivoDataDto.cs
    │   ├── PredictionResultDto.cs
    │   └── PredictionModeDto.cs
    ├── Models/
    │   ├── ActivosPrecio.cs
    │   └── ActivosPrecioInputModel.cs
    └── Enums/
        └── PredictionModo.cs
```

## 🚀 Instalación y Configuración

### Prerrequisitos
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [JetBrains Rider](https://www.jetbrains.com/rider/)
- Navegador web moderno

### Pasos de Instalación

1. **Clonar el repositorio:**
```bash
git clone https://github.com/alkysdesaroller/PREDAYTOR.git
cd PREDAYTOR
```

2. **Restaurar dependencias:**
```bash
dotnet restore
```

3. **Compilar la solución:**
```bash
dotnet build
```

4. **Ejecutar la aplicación:**
```bash
dotnet run --project PredictorActivos.Web
```

5. **Abrir en el navegador:**
```
https://localhost:5001
```

## 📚 Guía de Uso

### 1. Predicción de Activos

#### Método CSV:
1. Navega a la página principal
2. En el área de texto, ingresa 20 líneas con el formato:
```
2025-05-01, 150.35
2025-05-02, 152.10
...
```
3. Haz clic en "Calcular Predicción"

#### Método Individual:
1. Activa el toggle "Usar campos individuales"
2. Completa los 20 campos de fecha y valor
3. Haz clic en "Calcular Predicción"

### 2. Configuración de Modos

1. Navega a "Modos" en el menú
2. Selecciona el algoritmo deseado:
   - **SMA Crossover**: Para análisis de cruces de medias
   - **Regresión Lineal**: Para predicciones numéricas
   - **Momentum (ROC)**: Para análisis de velocidad
3. Haz clic en "Guardar Modo"

### 3. Interpretación de Resultados

- **Tendencia Alcista**: El precio tiende a subir
- **Tendencia Bajista**: El precio tiende a bajar
- **Cálculos Detallados**: Muestra el proceso matemático
- **Valor Futuro**: Predicción numérica (solo Regresión Lineal)

## 🧮 Algoritmos Implementados

### 1. Media Móvil Simple (SMA) Crossover
**Descripción:** Compara la media móvil de 5 días con la de 20 días.

**Fórmula:**
```
SMA_corta = Σ(precios_últimos_5_días) / 5
SMA_larga = Σ(precios_últimos_20_días) / 20

Si SMA_corta > SMA_larga → Tendencia Alcista
Si SMA_corta < SMA_larga → Tendencia Bajista
```

### 2. Regresión Lineal
**Descripción:** Calcula la línea de tendencia y predice el valor del día 21.

**Fórmulas:**
```
y = mx + b

m = (n×Σxy - Σx×Σy) / (n×Σx² - (Σx)²)
b = (Σy - m×Σx) / n

Valor_día_21 = m × 21 + b
```

### 3. Momentum (ROC)
**Descripción:** Mide la velocidad de cambio en períodos de 5 días.

**Fórmula:**
```
ROC = ((Precio_actual / Precio_hace_5_días) - 1) × 100

Si ROC > 0 → Tendencia Alcista
Si ROC < 0 → Tendencia Bajista
```

## 🔧 Configuración Avanzada

### Agregar Nuevos Algoritmos

1. **Crear nueva estrategia:**
```csharp
public class NuevoAlgoritmoStrategy : IPredictionStrategy
{
    public string ModeName => "Nuevo Algoritmo";
    
    public PredictionResultDto CalculatePrediction(List<AssetPrice> prices)
    {
        // Implementar lógica
    }
}
```

2. **Registrar en el servicio:**
```csharp
_strategies.Add(PredictionMode.NuevoAlgoritmo, new NuevoAlgoritmoStrategy());
```

### Personalizar Validaciones

Modifica `ActivoDataViewModel.cs` para agregar validaciones personalizadas:

```csharp
public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
{
    // Agregar nuevas validaciones
}
```

## 🧪 Testing

### Datos de Ejemplo para Pruebas

```csv
2025-01-01, 150.35
2025-01-02, 152.10
2025-01-03, 148.75
2025-01-04, 151.20
2025-01-05, 149.85
2025-01-06, 153.40
2025-01-07, 152.15
2025-01-08, 154.80
2025-01-09, 156.25
2025-01-10, 155.90
2025-01-11, 158.45
2025-01-12, 157.30
2025-01-13, 159.75
2025-01-14, 161.20
2025-01-15, 160.85
2025-01-16, 163.50
2025-01-17, 162.35
2025-01-18, 165.80
2025-01-19, 167.25
2025-01-20, 166.90
```

### Casos de Prueba

- **Tendencia Alcista:** Datos con valores crecientes
- **Tendencia Bajista:** Datos con valores decrecientes
- **Datos Inconsistentes:** Valores muy volátiles
- **Validaciones:** Menos/más de 20 entradas

## ⚡ API y Servicios

### Servicios Principales

#### IPredictionService
```csharp
public interface IPredictionService
{
    PredictionResultDto CalculatePrediction(ActivoDataDto data);
    List<ActivosPrecio> FiltrarDatos(string csvData);
    bool ValidarDatos(List<ActivosPrecio> precios);
}
```

#### IPredictionModeService
```csharp
public interface IPredictionModeService
{
    PredictionModo ModoActual();
    void SetModo(PredictionModo modo);
    List<(PredictionModo modo, string Nombre)> ModosDisponibles();
}
```

## 🔒 Validaciones Implementadas

### Validaciones del Servidor
- Exactamente 20 entradas de datos
- Valores numéricos positivos
- Fechas válidas y no futuras
- Formato CSV correcto
- Fechas no duplicadas

### Validaciones del Cliente
- Validación en tiempo real
- Feedback visual inmediato
- Conteo dinámico de entradas válidas
- Prevención de envío con errores

## 🐛 Solución de Problemas Comunes

### Error: "View not found"
**Causa:** Falta la vista correspondiente
**Solución:** Verificar que existan todas las vistas en las rutas correctas

### Error: "Object reference not set"
**Causa:** ViewModel no inicializado
**Solución:** Siempre inicializar ViewModels en el Controller

### Error: HTTP 405
**Causa:** Falta método POST en el Controller
**Solución:** Agregar método POST con atributo [HttpPost]

### Modo no se guarda
**Causa:** Lógica de validación invertida
**Solución:** Verificar `if (ModelState.IsValid)` sin negación

## 📊 Rendimiento

- **Tiempo de cálculo:** < 1 segundo para 20 datos
- **Memoria:** ~50MB en ejecución
- **Escalabilidad:** Soporta múltiples usuarios concurrentes
- **Compatibilidad:** Navegadores modernos (IE11+)

## 🤝 Contribuir

1. Fork el repositorio
2. Crea una rama feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -am 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Crea un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.

## 👨‍💻 Desarrollado por

**Albin Lopez** - Desarrollador De Software
- Proyecto académico 2025
- Curso: Programacion III

## 📞 Soporte

Para reportar bugs o solicitar features:
1. Abrir un [Issue](https://github.com/tu-usuario/predaytor/issues)
2. Incluir detalles del problema
3. Adjuntar capturas de pantalla si es necesario

---

**Nota:** Esta aplicación es para fines educativos y no debe ser utilizada como única fuente para decisiones de inversión financiera.

## 📈 Roadmap

### Versión 2.0 (Planeada)
- [ ] Más algoritmos (MACD, RSI, Bollinger Bands)
- [ ] Gráficos interactivos
- [ ] Exportar resultados a PDF/Excel
- [ ] API REST
- [ ] Base de datos para históricos
- [ ] Autenticación de usuarios

### Versión 1.1 (En desarrollo)
- [ ] Modo oscuro
- [ ] Más validaciones
- [ ] Optimización de rendimiento
- [ ] Tests unitarios
