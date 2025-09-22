using PredictorActivos.BusinessLogic.Models;
using PredictorActivos.Models.DTO;

namespace PredictorActivos.Models.Strategy
{
    /// <summary>
    /// Estrategia de predicción basada en Momentum.
    /// Calcula la variación (ROC) entre el valor actual y el de varios períodos atrás.
    /// </summary>
    public class MomentumStrategy : IPredicStrategy
    {
        /// <summary>
        /// Nombre descriptivo del modo de predicción.
        /// </summary>
        public string NombreModo => "Momentum Strategy";

        private const int Periodo = 5;

        /// <summary>
        /// Ejecuta la predicción de Momentum calculando los cambios en precios.
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
            var calculos = new List<string>();

            for (int i = 0; i < precioOrden.Count; i++)
            {
                if (i < Periodo)
                {
                    calculos.Add($"t={i}, Precio={precioOrden[i].Valor:F2}, Momentum=N/A");
                }
                else
                {
                    var valorActual = precioOrden[i].Valor;
                    var valorAnterior = precioOrden[i - Periodo].Valor;
                    var roc = valorActual - valorAnterior;

                    calculos.Add(
                        $"t={i}, Precio={valorActual:F2}, Precio t-{Periodo}={valorAnterior:F2}, Momentum={roc:F2}%");
                }
            }

            var ultimoIndex = precioOrden.Count - 1;
            var ultimoValorActual = precioOrden[ultimoIndex].Valor;
            var ultimoValorAnterior = precioOrden[ultimoIndex - Periodo].Valor;
            var ultimoRoc = (ultimoValorActual / ultimoValorAnterior - 1) * 100;
            var tendencia = ultimoRoc > 0 ? "Alcista" : "Bajista";

            return new PredictionResultDto
            {
                Modo = NombreModo,
                Tendencia = tendencia,
                Detalles = $"Último ROC: {ultimoRoc:F2}%",
                Calculos = calculos
            };
        }
    }
}
