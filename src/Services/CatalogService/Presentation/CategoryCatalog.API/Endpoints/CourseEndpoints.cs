using CatalogService.Application.Features.CourseFeature.Commands.CreateCourseCommand;
using CatalogService.Application.Features.CourseFeature.Commands.DeleteCourseCommand;
using CatalogService.Application.Features.CourseFeature.Commands.UpdateCourseCommand;
using CatalogService.Application.Features.CourseFeature.DTOs;
using CatalogService.Application.Features.CourseFeature.Queries.GetAllCourseQuery;
using CatalogService.Application.Features.CourseFeature.Queries.GetByIdCourseQuery;
using MediatR;

namespace CategoryCatalog.API.Endpoints;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder RegisterCourseEndpoints(this IEndpointRouteBuilder app)
    {
        // Course Endpoints
        var group = app.MapGroup("/api/v{version:apiVersion}/courses")
            .WithTags("Courses");


        // Get All Courses Endpoint
        group.MapGet("/getall", async (IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var queryResponse = await mediator.Send(new GetAllCourseQueryRequest(),
              cancellationToken
            );

            return queryResponse.Result.IsSuccess
                ? Results.Ok(queryResponse.Result.Data)
                : Results.BadRequest(queryResponse.Result);
        })
        .WithName("GetAllCourses")
        .WithDescription("Retrieves all courses.");


        // Get Course By Id Endpoint
        group.MapGet("/getbyid/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var queryResponse = await mediator.Send(new GetByIdCourseQueryRequest
            {
                Id = id
            },
                 cancellationToken
             );

            return queryResponse.Result.IsSuccess
                ? Results.Ok(queryResponse.Result.Data)
                : Results.NotFound(queryResponse.Result);
        })
            .WithName("GetByIdCourse")
            .WithDescription("Get course by ID")
            .Produces<GetByIdCourseQueryResponse>(200)
            .Produces(404);


        group.MapPost("/create", async (
            CreateCourseCommandDto createCourseCommandDto,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new CreateCourseCommandRequest
            {
                CreateCourseCommandRequestDto = createCourseCommandDto
            },
                cancellationToken
            );
            return commandResponse.Result.IsSuccess
                ? Results.Created($"/api/courses/create", commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("CreateCourse")
            .WithDescription("Create a new course")
            .Produces<CreateCourseCommandResponse>(201)
            .Produces(400);


        group.MapPut("/update/{id}", async (
            string id,
            UpdateCourseCommandDto updateCourseCommandDto,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new UpdateCourseCommandRequest
            {
                Id = id,
                UpdateCourseCommandRequestDto = updateCourseCommandDto
            },
            cancellationToken
            );

            return commandResponse.Result.IsSuccess
                ? Results.Ok(commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("UpdateCourse")
            .WithDescription("Update an existing course")
            .Produces(200)
            .Produces(400)
            .Produces(404);


        group.MapDelete("/delete/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new DeleteCourseCommandRequest
            {
                Id = id
            },
                cancellationToken
            );
            return commandResponse.Result.IsSuccess
                ? Results.Ok(commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("DeleteCourse")
            .WithDescription("Delete course by ID")
            .Produces(200)
            .Produces(400)
            .Produces(404);

        return app;
    }
}

