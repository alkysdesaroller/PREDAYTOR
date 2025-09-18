using Microsoft.AspNetCore.Mvc;
using PredictorActivos.Models;
using PredictorActivos.Models.DTO;
using PredictorActivos.Models.Services;
using PredictorActivos.ViewModel;

namespace PredictorActivos.Controllers
{
    /// <summary>
    /// Controlador encargado de recibir y procesar los datos del activo
    /// y ejecutar la predicción utilizando los servicios configurados.
    /// </summary>
    public class PredictorController : Controller
    {
        private readonly IPredicService _predictorService;
        private readonly IPredicModoService _predictorModoService;

        /// <summary>
        /// Constructor del controlador Predictor.
        /// Inyecta los servicios para el manejo de predicciones y modos de predicción.
        /// </summary>
        /// <param name="predictorModoService">Servicio para gestionar los modos de predicción.</param>
        /// <param name="predictorService">Servicio para procesar y calcular predicciones.</param>
        /// <exception cref="ArgumentNullException">
        /// Lanza si alguno de los servicios proporcionados es nulo.
        /// </exception>
        public PredictorController(IPredicModoService predictorModoService, IPredicService predictorService)
        {
            _predictorService = predictorService ?? throw new ArgumentNullException(nameof(predictorService));
            _predictorModoService = predictorModoService ?? throw new ArgumentNullException(nameof(predictorModoService));
        }

        /// <summary>
        /// Acción GET que muestra la vista inicial para cargar los datos del activo.
        /// </summary>
        /// <returns>Vista Index con el modelo inicializado.</returns>
        public IActionResult Index()
        {
            var viewModel = new ActivoDataViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Acción POST que procesa los datos recibidos del formulario,
        /// valida la entrada y ejecuta la predicción.
        /// </summary>
        /// <param name="model">Modelo con los datos del activo.</param>
        /// <returns>
        /// Vista de resultado con la predicción realizada o
        /// la misma vista de entrada con errores de validación.
        /// </returns>
        [HttpPost]
        public IActionResult Index(ActivoDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                List<ActivosPrecio> precios;

                // Si el usuario seleccionó entrada individual, se validan las 20 entradas
                if (model.UsoIndividualInput)
                {
                    var validarInput = model.IndividualInput
                        .Where(x => x.Fecha.HasValue && x.Valor.HasValue)
                        .OrderBy(x => x.Fecha)
                        .ToList();

                    if (validarInput.Count != 20)
                    {
                        ModelState.AddModelError(string.Empty, "Se requieren 20 entradas válidas con fecha y valor.");
                        return View(model);
                    }

                    precios = validarInput.Select(x => new ActivosPrecio
                    {
                        Fecha = x.Fecha!.Value,
                        Valor = x.Valor!.Value
                    }).ToList();
                }
                else
                {
                    precios = _predictorService.FiltrarDatos(model.Data);
                }

                if (!_predictorService.ValidarDatos(precios))
                {
                    ModelState.AddModelError(string.Empty,
                        "Los datos proporcionados no son válidos. Asegúrese de que haya 20 entradas con fechas y valores correctos.");
                    return View(model);
                }

                var actualModo = _predictorModoService.ModoActual();
                var activoData = new ActivoDataDto
                {
                    Precios = precios,
                    PredictionMode = actualModo
                };

                var resultado = _predictorService.CalcularPrediction(activoData);

                if (resultado == null)
                {
                    throw new InvalidOperationException("El servicio de predicción devolvió un resultado nulo.");
                }

                var resultViewModel = new PredictionResultViewModel
                {
                    Modo = resultado.Modo ?? "Sin especificar",
                    Tendencia = resultado.Tendencia ?? "Sin determinar",
                    Detalles = resultado.Detalles ?? "Sin detalles disponibles",
                    ValorFuturo = resultado.ValorFuturo,
                    Calculos = resultado.Calculos ?? new List<string>(),
                    IsSuccess = true
                };

                return View("Resultado", resultViewModel);
            }
            catch (Exception e)
            {
                var errorViewModel = new PredictionResultViewModel
                {
                    IsSuccess = false,
                    ErrorMessage = $"Ocurrió un error al procesar los datos: {e.Message}",
                    Modo = string.Empty,
                    Tendencia = string.Empty,
                    Detalles = string.Empty,
                    Calculos = new List<string>()
                };

                return View("Resultado", errorViewModel);
            }
        }
    }
}
