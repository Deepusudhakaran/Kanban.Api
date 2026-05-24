using Kanban.API.Dto;
using Kanban.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanController(IKanbanService kanbanService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<KanbanResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<KanbanResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var tasks = await kanbanService.GetAllAsync(cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<KanbanResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var task = await kanbanService.GetByIdAsync(id, cancellationToken);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KanbanResponse>> Create(CreateKanbanRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KanbanResponse>> Update(Guid id, UpdateKanbanRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanService.UpdateAsync(id, request, cancellationToken);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KanbanResponse>> UpdateStatus(Guid id, UpdateKanbanStatusRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanService.UpdateStatusAsync(id, request, cancellationToken);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await kanbanService.DeleteAsync(id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }
    }
}
