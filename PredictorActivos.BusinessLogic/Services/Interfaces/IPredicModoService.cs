using PredictorActivos.Models.Enums;

namespace PredictorActivos.Models.Services;

/// <summary>
/// Define las operaciones para manejar el modo de predicción seleccionado en el sistema.
/// Permite consultar el modo actual, cambiarlo y listar los modos disponibles.
/// </summary>
public interface IPredicModoService
{
    /// <summary>
    /// Obtiene el modo de predicción actualmente configurado.
    /// </summary>
    PredictionModo ModoActual();

    /// <summary>
    /// Establece el modo de predicción actual.
    /// </summary>
    /// <param name="modo">Enumeración del modo de predicción a utilizar.</param>
    void SetModo(PredictionModo modo);

    /// <summary>
    /// Obtiene la lista de modos disponibles junto con su nombre en formato legible.
    /// </summary>
    List<(PredictionModo modo, string? Nombre)> ModosDisponibles();
}
