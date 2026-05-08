using DoctorLicenseManagement.API.Models;
using DoctorLicenseManagement.Application.Doctors.Commands;
using DoctorLicenseManagement.Application.Doctors.Dtos;
using DoctorLicenseManagement.Application.Doctors.Queries;
using DoctorLicenseManagement.Application.Doctors.Queries.GetDoctors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DoctorLicenseManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] string? search,
    [FromQuery] int? status,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var doctors = await _mediator.Send(
            new GetDoctorsQuery(search, status, pageNumber, pageSize));

        return Ok(
            ApiResponse<object>.SuccessResponse(
                doctors,
                "Doctors retrieved successfully"));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var doctor = await _mediator.Send(new GetDoctorByIdQuery(id));

        if (doctor == null)
        {
            return NotFound(
                ApiResponse<object>.FailureResponse("Doctor not found"));
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                doctor,
                "Doctor retrieved successfully"));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDoctorRequest request)
    {
        var doctor = await _mediator.Send(new CreateDoctorCommand(request));

        return Ok(
            ApiResponse<object>.SuccessResponse(
                doctor,
                "Doctor created successfully"));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateDoctorRequest request)
    {
        var updated = await _mediator.Send(new UpdateDoctorCommand(id, request));

        if (!updated)
        {
            return NotFound(
                ApiResponse<object>.FailureResponse("Doctor not found"));
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                true,
                "Doctor updated successfully"));
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateDoctorStatusRequest request)
    {
        var updated = await _mediator.Send(
            new UpdateDoctorStatusCommand(id, request));

        if (!updated)
        {
            return NotFound(
                ApiResponse<object>.FailureResponse("Doctor not found"));
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                true,
                "Doctor status updated successfully"));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteDoctorCommand(id));

        if (!deleted)
        {
            return NotFound(
                ApiResponse<object>.FailureResponse("Doctor not found"));
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                true,
                "Doctor deleted successfully"));
    }
}