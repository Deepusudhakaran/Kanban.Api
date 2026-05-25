using Kanban.API.Dto;
using Kanban.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.API.Controllers
{
    /// <summary>
    /// API controller for managing Kanban tasks.
    /// Exposes endpoints to list, retrieve, create, update, change status and delete kanban items.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanController(IKanbanService kanbanService) : ControllerBase
    {
        /// <summary>
        /// Returns all kanban tasks.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token supplied by the framework.</param>
        /// <returns>List of KanbanResponse representing tasks.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<KanbanResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<KanbanResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var tasks = await kanbanService.GetAllAsync(cancellationToken);
            return Ok(tasks);
        }

        /// <summary>
        /// Returns a single kanban task by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the kanban task.</param>
        /// <param name="cancellationToken">Cancellation token supplied by the framework.</param>
        /// <returns>The matching KanbanResponse or 404 if not found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<KanbanResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var task = await kanbanService.GetByIdAsync(id, cancellationToken);
            return task is null ? NotFound() : Ok(task);
        }

        /// <summary>
        /// Creates a new kanban task.
        /// </summary>
        /// <param name="request">Create request payload containing task details.</param>
        /// <param name="cancellationToken">Cancellation token supplied by the framework.</param>
        /// <returns>The created KanbanResponse with 201 Created.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KanbanResponse>> Create(CreateKanbanRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        /// <summary>
        /// Updates an existing kanban task.
        /// </summary>
        /// <param name="id">Identifier of the task to update.</param>
        /// <param name="request">Update payload containing new values.</param>
        /// <param name="cancellationToken">Cancellation token supplied by the framework.</param>
        /// <returns>The updated KanbanResponse or 404 if the task was not found.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KanbanResponse>> Update(Guid id, UpdateKanbanRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanService.UpdateAsync(id, request, cancellationToken);
            return task is null ? NotFound() : Ok(task);
        }

        /// <summary>
        /// Updates only the status of an existing kanban task.
        /// </summary>
        /// <param name="id">Identifier of the task whose status will be changed.</param>
        /// <param name="request">Payload containing the new status value.</param>
        /// <param name="cancellationToken">Cancellation token supplied by the framework.</param>
        /// <returns>The updated KanbanResponse or 404 if the task was not found.</returns>
        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(typeof(KanbanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<KanbanResponse>> UpdateStatus(Guid id, UpdateKanbanStatusRequest request, CancellationToken cancellationToken)
        {
            var task = await kanbanService.UpdateStatusAsync(id, request, cancellationToken);
            return task is null ? NotFound() : Ok(task);
        }

        /// <summary>
        /// Deletes an existing kanban task.
        /// </summary>
        /// <param name="id">Identifier of the task to delete.</param>
        /// <param name="cancellationToken">Cancellation token supplied by the framework.</param>
        /// <returns>NoContent on success or 404 if the task was not found.</returns>
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
