using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PredictorActivos.Models.Services;
using PredictorActivos.ViewModel;

namespace PredictorActivos.Controllers
{
    /// <summary>
    /// Controlador que gestiona la selección y el cambio del modo de predicción.
    /// Permite al usuario ver los modos disponibles y actualizar el modo actual.
    /// </summary>
    public class PredictorModoController : Controller
    {
        private readonly IPredicModoService _predicModoService;

        /// <summary>
        /// Constructor del controlador PredictorModo.
        /// Inyecta el servicio para manejar los modos de predicción.
        /// </summary>
        /// <param name="predicModoService">Servicio para consultar y actualizar el modo de predicción.</param>
        /// <exception cref="ArgumentNullException">
        /// Lanza si el servicio es nulo.
        /// </exception>
        public PredictorModoController(IPredicModoService predicModoService)
        {
            _predicModoService = predicModoService ?? throw new ArgumentNullException(nameof(predicModoService));
        }

        /// <summary>
        /// Acción GET que muestra la vista con el modo actual y la lista de modos disponibles.
        /// </summary>
        /// <returns>Vista con el modelo de modos.</returns>
        public IActionResult Index()
        {
            var modoActual = _predicModoService.ModoActual();
            var modosDisponibles = _predicModoService.ModosDisponibles();

            var viewModel = new PredictionModoViewModel
            {
                SelectedModo = modoActual,
                ModosAvailable = modosDisponibles.Select(m => new SelectListItem
                {
                    Value = ((int)m.modo).ToString(),
                    Text = m.Nombre,
                    Selected = m.modo == modoActual
                }).ToList()
            };
            return View(viewModel);
        }

        /// <summary>
        /// Acción POST que recibe el modo seleccionado y lo actualiza en el sistema.
        /// </summary>
        /// <param name="model">Modelo que contiene el modo seleccionado.</param>
        /// <returns>Vista actualizada con mensaje de éxito y lista de modos.</returns>
        [HttpPost]
        public IActionResult Index(PredictionModoViewModel model)
        {
            if (ModelState.IsValid)
            {
                _predicModoService.SetModo(model.SelectedModo);
                model.SuccessMessage = "Modo de predicción actualizado con éxito.";
            }

            var modosDisponibles = _predicModoService.ModosDisponibles();
            model.ModosAvailable = modosDisponibles.Select(m => new SelectListItem
            {
                Value = ((int)m.modo).ToString(),
                Text = m.Nombre,
                Selected = m.modo == model.SelectedModo
            }).ToList();

            return View(model);
        }
    }
}
