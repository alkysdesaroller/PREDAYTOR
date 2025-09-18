using PredictorActivos.Models.DTO;

namespace PredictorActivos.Models.Strategy
{
    /// <summary>
    /// Estrategia de predicción basada en regresión lineal.
    /// Calcula una recta de tendencia usando los 20 precios más recientes
    /// y predice el valor para el siguiente período.
    /// </summary>
    public class LinealRegressionStrategy : IPredicStrategy
    {
        /// <summary>
        /// Nombre descriptivo del modo de predicción.
        /// </summary>
        public string NombreModo => "Regresion Lineal";

        /// <summary>
        /// Ejecuta la predicción usando regresión lineal sobre los precios proporcionados.
        /// </summary>
        /// <param name="precios">Lista de 20 precios del activo con sus fechas.</param>
        /// <returns>Objeto <see cref="PredictionResultDto"/> con la tendencia y el valor futuro.</returns>
        /// <exception cref="ArgumentException">
        /// Se lanza si la lista no contiene exactamente 20 elementos.
        /// </exception>
        public PredictionResultDto CalculoPrediction(List<ActivosPrecio> precios)
        {
            if (precios.Count != 20)
                throw new ArgumentException("La lista de precios debe tener exactamente 20 elementos.");

            var precioOrden = precios.OrderByDescending(p => p.Fecha).ToList();

            for (int i = 0; i < precioOrden.Count; i++)
            {
                precioOrden[i].Index = i + 1;
            }

            var n = precioOrden.Count;
            var sumaX = precioOrden.Sum(p => p.Index);
            var sumaY = precioOrden.Sum(p => (double)p.Valor);
            var sumaXy = precioOrden.Sum(p => p.Index * (double)p.Valor);
            var sumaX2 = precioOrden.Sum(p => p.Index * p.Index);

            var m = (n * sumaXy - sumaX * sumaY) / (n * sumaX2 - sumaX * sumaX);
            var b = (sumaY - m * sumaX) / n;

            var valorFuturo = (decimal)(m * 21 + b);
            var valorActual = precioOrden.Last().Valor;
            var tendencia = valorFuturo > valorActual ? "Alcista" : "Bajista";

            return new PredictionResultDto
            {
                Modo = NombreModo,
                Tendencia = tendencia,
                ValorFuturo = valorFuturo,
                Detalles = $"Valor Actual: {valorActual:F2}, Valor Predicho: {valorFuturo:F2}",
                Calculos = new List<string>
                {
                    $"Pendiente (m): {m:F4}",
                    $"Intersección (b): {b:F2}",
                    $"Valor Actual: {valorActual:F2}",
                    $"Valor para el siguiente período (índice 21): {valorFuturo:F2}",
                    $"Tendencia: {tendencia}"
                }
            };
        }
    }
}
