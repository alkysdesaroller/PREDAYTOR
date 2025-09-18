using System.Globalization;
using PredictorActivos.Models.DTO;
using PredictorActivos.Models.Enums;
using PredictorActivos.Models.Strategy;

namespace PredictorActivos.Models.Services;

/// <summary>
/// Implementación de <see cref="IPredicService"/>.
/// Orquesta las estrategias de predicción disponibles (SMA, regresión lineal, momentum).
/// </summary>
public class PredicService : IPredicService
{
    private readonly Dictionary<PredictionModo, IPredicStrategy> _strategies;

    /// <summary>
    /// Inicializa el servicio y registra las estrategias disponibles.
    /// </summary>
    public PredicService()
    {
        _strategies = new Dictionary<PredictionModo, IPredicStrategy>
        {
            { PredictionModo.Sma, new SmaStrategy() },
            { PredictionModo.LinealRegrasion, new LinealRegressionStrategy() },
            { PredictionModo.Momentum, new MomentumStrategy() }
        };
    }
    
    public PredictionResultDto CalcularPrediction(ActivoDataDto data)
    {
        if (!ValidarDatos(data.Precios))
            throw new ArgumentException("Datos no válidos");

        var strategy = _strategies[data.PredictionMode];
        return strategy.CalculoPrediction(data.Precios);
    }
    
    public List<ActivosPrecio> FiltrarDatos(string cvsData)
    {
        var precios = new List<ActivosPrecio>();
        if (string.IsNullOrEmpty(cvsData)) return precios;

        var lines = cvsData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length == 2)
            {
                if (DateTime.TryParse(parts[0], out DateTime fecha) &&
                    decimal.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture,
                        out decimal precio))
                {
                    precios.Add(new ActivosPrecio
                    {
                        Fecha = fecha,
                        Valor = precio
                    });
                }
            }
        }
        return precios;
    }
    
    public bool ValidarDatos(List<ActivosPrecio> precios)
    {
        return precios != null && precios.Count == 20 && precios.All(p => p.Valor > 0);
    }
}
