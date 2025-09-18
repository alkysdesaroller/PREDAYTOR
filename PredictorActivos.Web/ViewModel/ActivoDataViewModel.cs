using System.ComponentModel.DataAnnotations;
using PredictorActivos.Models;

namespace PredictorActivos.ViewModel
{
    /// <summary>
    /// ViewModel que representa los datos de entrada para un activo.
    /// Permite cargar un archivo de datos completo o introducir los valores individualmente.
    /// </summary>
    public class ActivoDataViewModel
    {
        /// <summary>
        /// Contenido del archivo con datos de precios.
        /// Obligatorio y limitado a 2000 caracteres.
        /// </summary>
        [Required(ErrorMessage = "Los datos del archivo son requeridos")]
        [StringLength(2000, ErrorMessage = "El archivo no puede exceder los 2000 caracteres")]
        public string Data { get; set; } = string.Empty;

        /// <summary>
        /// Lista de entradas individuales de precios para 20 registros (fecha y valor).
        /// Se inicializa en el constructor con índices 1 a 20.
        /// </summary>
        public List<ActivosPrecioInputModel> IndividualInput { get; set; } = new();

        /// <summary>
        /// Indica si se usará la entrada individual en lugar del archivo de datos.
        /// </summary>
        public bool UsoIndividualInput { get; set; } = false;

        /// <summary>
        /// Constructor que inicializa la lista de entradas individuales con 20 posiciones.
        /// </summary>
        public ActivoDataViewModel()
        {
            for (int i = 0; i < 20; i++)
            {
                IndividualInput.Add(new ActivosPrecioInputModel { Index = i + 1 });
            }
        }
    }
}