using PredictorActivos.BusinessLogic.Models;
using PredictorActivos.Models.DTO;

namespace PredictorActivos.Models.Strategy
{
    /// <summary>
    /// Estrategia de predicción basada en medias móviles simples (SMA Crossover).
    /// Compara la media móvil corta (5 períodos) con la larga (20 períodos) para determinar tendencia.
    /// </summary>
    public class SmaStrategy : IPredicStrategy
    {
        /// <summary>
        /// Nombre descriptivo del modo de predicción.
        /// </summary>
        public string NombreModo => "Media Movil Simple SMA Crossover";

        /// <summary>
        /// Ejecuta la predicción usando medias móviles simples sobre los precios proporcionados.
        /// </summary>
        /// <param name="precios">Lista de 20 precios del activo con sus fechas.</param>
        /// <returns>Objeto <see cref="PredictionResultDto"/> con la tendencia y cálculos detallados.</returns>
        /// <exception cref="ArgumentException">
        /// Se lanza si la lista no contiene exactamente 20 elementos.
        /// </exception>
        public PredictionResultDto CalculoPrediction(List<ActivosPrecio> precios)
        {
            if (precios.Count != 20)
                throw new ArgumentException("La lista de precios debe tener exactamente 20 elementos.");

            var precioOrden = precios.OrderByDescending(p => p.Fecha).ToList();

            var smaCorto = precioOrden.Take(5).Average(p => p.Valor);
            var smaLargo = precioOrden.Average(p => p.Valor);

            var tendencia = smaCorto > smaLargo ? "Alcista" : "Bajista";

            return new PredictionResultDto
            {
                Modo = NombreModo,
                Tendencia = tendencia,
                Detalles = $"SMA Corto (5): {smaCorto:F2}, SMA Largo (20): {smaLargo:F2}",
                Calculos = new List<string>
                {
                    $"SMA Corto (5): {smaCorto:F2}",
                    $"SMA Largo (20): {smaLargo:F2}",
                    $"Tendencia: {tendencia} (SMA Corto {(smaCorto > smaLargo ? ">" : "<")} SMA Largo)"
                }
            };
        }
    }
}
