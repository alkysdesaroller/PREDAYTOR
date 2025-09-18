using PredictorActivos.Models.Enums;

namespace PredictorActivos.Models.Services;

/// <summary>
/// Implementación de <see cref="IPredicModoService"/>.
/// Administra de manera segura el modo de predicción actual.
/// </summary>
public class PredicModoService : IPredicModoService
{
    private PredictionModo _modoActual = PredictionModo.Sma;
    private static readonly object _lock = new object();
    
    public PredictionModo ModoActual()
    {
        lock (_lock)
        {
            return _modoActual;
        }
    }

    
    public void SetModo(PredictionModo modo)
    {
        lock (_lock)
        {
            _modoActual = modo;
        }
    }

    
    public List<(PredictionModo modo, string? Nombre)> ModosDisponibles()
    {
        return Enum.GetValues<PredictionModo>()
            .Select(m => (modo: m, Nombre: m.ToString()))
            .ToList();
    }
}
