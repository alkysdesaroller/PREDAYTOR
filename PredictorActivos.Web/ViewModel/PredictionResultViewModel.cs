namespace PredictorActivos.ViewModel
{
    /// <summary>
    /// ViewModel que contiene los resultados de la predicción realizada.
    /// </summary>
    public class PredictionResultViewModel
    {
        /// <summary>
        /// Nombre del modo de predicción usado, por ejemplo: Sma, Regresión Lineal.
        /// </summary>
        public string Modo { get; set; } = string.Empty;

        /// <summary>
        /// Tendencia determinada por la predicción, por ejemplo: alcista, bajista.
        /// </summary>
        public string Tendencia { get; set; } = string.Empty;

        /// <summary>
        /// Texto con detalles adicionales de la predicción.
        /// </summary>
        public string Detalles { get; set; } = string.Empty;

        /// <summary>
        /// Valor futuro estimado del activo según la predicción.
        /// </summary>
        public decimal? ValorFuturo { get; set; }

        /// <summary>
        /// Lista de pasos o cálculos realizados para llegar al resultado.
        /// </summary>
        public List<string> Calculos { get; set; } = new();

        /// <summary>
        /// Indica si la predicción se realizó con éxito.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Mensaje de error en caso de que la predicción falle.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}