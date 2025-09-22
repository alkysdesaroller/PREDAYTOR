using PredictorActivos.BusinessLogic.Models;
using PredictorActivos.Models.DTO;

namespace PredictorActivos.Models.Services;

/// <summary>
/// Define las operaciones del servicio que procesa los datos y calcula las predicciones.
/// </summary>
public interface IPredicService
{
    /// <summary>
    /// Calcula la predicción en base a los datos de entrada y al modo configurado.
    /// </summary>
    /// <param name="data">Datos de activos para la predicción.</param>
    /// <returns>Objeto con resultados de la predicción.</returns>
    PredictionResultDto CalcularPrediction(ActivoDataDto data);

    /// <summary>
    /// Convierte un texto CSV a una lista de precios (fecha y valor).
    /// </summary>
    /// <param name="cvsData">Cadena con datos CSV (fecha,valor).</param>
    /// <returns>Lista de <see cref="ActivosPrecio"/>.</returns>
    List<ActivosPrecio> FiltrarDatos(string cvsData);

    /// <summary>
    /// Valida que los datos tengan el formato y cantidad esperada (20 precios > 0).
    /// </summary>
    /// <param name="precios">Lista de precios a validar.</param>
    /// <returns>True si los datos son válidos; false de lo contrario.</returns>
    bool ValidarDatos(List<ActivosPrecio> precios);
}
