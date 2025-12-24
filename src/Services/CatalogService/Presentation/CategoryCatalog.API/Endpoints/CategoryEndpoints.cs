using CatalogService.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.Commands.DeleteCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using CatalogService.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;
using CatalogService.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;
using MediatR;

namespace CategoryCatalog.API.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder RegisterCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        // Category Endpoints
        var group = app.MapGroup("/categories")
            .WithTags("Categories");


        // Get All Categories Endpoint
        group.MapGet("/getall", async (IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var queryResponse = await mediator.Send(new GetAllCategoryQueryRequest(),
              cancellationToken
            );

            return queryResponse.Result.IsSuccess
                ? Results.Ok(queryResponse.Result.Data)
                : Results.BadRequest(queryResponse.Result);
        })
        .WithName("GetAllCategories")
        .WithDescription("Retrieves all categories.");


        // Get Category By Id Endpoint
        group.MapGet("/getbyid/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var queryResponse = await mediator.Send(new GetByIdCategoryQueryRequest
            {
                Id = id
            },
                 cancellationToken
             );

            return queryResponse.Result.IsSuccess
                ? Results.Ok(queryResponse.Result.Data)
                : Results.NotFound(queryResponse.Result);
        })
            .WithName("GetByIdCategory")
            .WithDescription("Get category by ID")
            .Produces<GetByIdCategoryQueryResponse>(200)
            .Produces(404);


        group.MapPost("/create", async (
            CreateCategoryCommandDto createCategoryCommandDto,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new CreateCategoryCommandRequest
            {
                CreateCategoryCommandRequestDto = createCategoryCommandDto
            },
                cancellationToken
            );
            return commandResponse.Result.IsSuccess
                ? Results.Created($"/api/categories/create", commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("CreateCategory")
            .WithDescription("Create a new category")
            .Produces<CreateCategoryCommandResponse>(201)
            .Produces(400);


        group.MapPut("/update", async (
            UpdateCategoryCommandDto updateCategoryCommandDto,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new UpdateCategoryCommandRequest
            {
                UpdateCategoryCommandRequestDto = updateCategoryCommandDto
            },
            cancellationToken
            );

            return commandResponse.Result.IsSuccess
                ? Results.Ok(commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("UpdateCategory")
            .WithDescription("Update an existing category")
            .Produces(200)
            .Produces(400)
            .Produces(404);


        group.MapDelete("/delete/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new DeleteCategoryCommandRequest
            {
                Id = id
            },
                cancellationToken
            );
            return commandResponse.Result.IsSuccess
                ? Results.Ok(commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("DeleteCategory")
            .WithDescription("Delete category by ID")
            .Produces(200)
            .Produces(400)
            .Produces(404);

        return app;
    }
}

