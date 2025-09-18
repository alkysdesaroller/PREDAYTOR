using Microsoft.AspNetCore.Mvc.Rendering;
using PredictorActivos.Models.Enums;

namespace PredictorActivos.ViewModel
{
    /// <summary>
    /// ViewModel que gestiona el modo de predicción seleccionado y
    /// la lista de modos disponibles para mostrar en un memu de tipo dropdown.
    /// </summary>
    public class PredictionModoViewModel
    {
        /// <summary>
        /// Modo de predicción seleccionado por el usuario.
        /// Por defecto es Sma.
        /// </summary>
        public PredictionModo SelectedModo { get; set; } = PredictionModo.Sma;

        /// <summary>
        /// Lista de modos disponibles para ser mostrados en un combo o select.
        /// </summary>
        public List<SelectListItem> ModosAvailable { get; set; } = new();

        /// <summary>
        /// Mensaje de éxito que puede mostrarse tras realizar la predicción.
        /// </summary>
        public string SuccessMessage { get; set; } = string.Empty;
    }
}