using System.ComponentModel.DataAnnotations;

namespace PredictorActivos.ViewModel
{
    /// <summary>
    /// Representa un dato individual de precio de un activo,
    /// compuesto por índice, fecha y valor.
    /// </summary>
    public class ActivosPrecioInputModel
    {
        /// <summary>
        /// Índice del registro (1, 2, 3, ...). 
        /// Se asigna desde el ViewModel padre.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Fecha del dato del activo.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        /// <summary>
        /// Valor del activo para la fecha especificada.
        /// Debe ser mayor que 0.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal? Valor { get; set; }
    }
}