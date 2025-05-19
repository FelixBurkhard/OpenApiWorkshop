using Microsoft.AspNetCore.Mvc;
using ProjectManagerSimulatorApi.DataObjects;
using ProjectManagerSimulatorApi.Domain;
using ProjectManagerSimulatorApi.Repositories;

namespace ProjectManagerSimulatorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeadlinesController(DeadlineRepository deadlineRepository, EstimateRepository estimateRepository) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Deadline>> GetAll()
    {
        var deadlines = deadlineRepository.GetAll();
        return Ok(deadlines);
    }

    [HttpGet("{projectId:guid}")]
    public ActionResult<Deadline> GetByProjectId(Guid projectId)
    {
        var deadline = deadlineRepository.GetByProjectId(projectId);
        if (deadline is null)
            return NotFound();
        return Ok(deadline);
    }

    [HttpPost("calculate")]
    public ActionResult<Deadline> Calculate([FromBody] CalculateDeadlineRequest request)
    {
        if (request is null)
            return BadRequest("Request is required.");

        var estimates = estimateRepository.GetByProjectId(request.ProjectId).ToList();
        if (estimates.Count == 0)
            return BadRequest("No estimates found for the specified project.");

        var deadline = DeadlineCalculator.CalculateDeadline(estimates, request.Name, request.ProjectId);
        deadlineRepository.Save(deadline);
        return CreatedAtAction(nameof(GetByProjectId), new { projectId = deadline.ProjectId }, deadline);
    }

    [HttpPut("{projectId:guid}")]
    public IActionResult Update(Guid projectId, [FromBody] UpdateDeadlineRequest request)
    {
        var existing = deadlineRepository.GetByProjectId(projectId);
        if (existing is null)
            return NotFound();

        if (request is null || request.DeadlineDate >= existing.DeadlineDate)
            return BadRequest("Deadline can only be shortened.");

        var updated = existing with { DeadlineDate = request.DeadlineDate, Name = request.Name ?? existing.Name };
        var updatedResult = deadlineRepository.Update(projectId, updated);
        if (!updatedResult)
            return NotFound();

        return NoContent();
    }
}

public record CalculateDeadlineRequest
{
    public string Name { get; init; } = string.Empty;
    public Guid ProjectId { get; init; }
}

public record UpdateDeadlineRequest
{
    public string? Name { get; init; }
    public DateTimeOffset DeadlineDate { get; init; }
}
