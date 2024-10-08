using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using N5.User.Domain.DTO;
using N5.Telemetry.Observability;  // Referencia a TelemetryTracker
using System.Collections.Generic;

namespace N5.User.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreatePermissionTelemetryController : Controller
    {
        private readonly TelemetryTracker _telemetryTracker;

        public CreatePermissionTelemetryController(TelemetryTracker telemetryTracker)
        {
            _telemetryTracker = telemetryTracker;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePermissionCompleteDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDto request)
        {
            // Inicia una actividad de telemetría para el proceso de creación de permisos
            using (var activity = _telemetryTracker.TrackActivity("CreatePermission",new Dictionary<string, object>
                   {
                       { "Activity", request.IdSession },
                       { "PermissionType", request.PermissionTypeId }
                   }))
            {
                // Simulación de lógica de negocio
                SimulateBusinessLogic();

                // Registro de un evento dentro de la actividad
                _telemetryTracker.TrackEvent("PermissionCreationEvent", activity, new Dictionary<string, object>
                {
                    { "UserId", request.IdSession },
                    { "PermissionType", request.PermissionTypeId }
                });

                // Registrar una métrica personalizada
                _telemetryTracker.TrackMetric("PermissionCreated", 1);
            }

            // Simular retorno de un objeto exitoso
            var response = new CreatePermissionCompleteDTO
            {
                IdSession = request.IdSession,  // Valor simulado
                CreatedDate = new DateTime()
            };

            return Ok(response);
        }

        private void SimulateBusinessLogic()
        {
            // Simula un retraso en la lógica de negocio
            System.Threading.Thread.Sleep(100);
        }
    }
}
