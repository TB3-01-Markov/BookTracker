using BookTracker.Api.Application;
using BookTracker.Api.Application.Books.CreateBook;
using BookTracker.Api.Application.Books.DeleteBook;
using BookTracker.Api.Application.Books.GetBookDetails;
using BookTracker.Api.Application.Books.GetBookSummaries;
using BookTracker.Api.Application.Books.UpdateBook;
using BookTracker.Api.Application.Members;
using BookTracker.Api.Application.Members.CreateMember;
using BookTracker.Api.Application.Members.DeleteMember;
using BookTracker.Api.Application.Members.GetMemberDetails;
using BookTracker.Api.Application.Members.GetMemberSummaries;
using BookTracker.Api.Application.Members.UpdateMember;
using BookTracker.Api.Domain;
using BookTracker.Api.Domain.Members;

namespace BookTracker.Api.Endpoints.Members
{
    public static class MemberEndpoints
    {
        public static IEndpointRouteBuilder MapMemberEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/members", GetMemberSummaries);
            app.MapGet("/members/{id:int}", GetMemberDetails);
            app.MapPost("/members", CreateMember);
            app.MapPut("/members/{id:int}", UpdateMember);
            app.MapDelete("/members/{id:int}", DeleteMember);

            return app;
        }

        public static async Task<IResult> GetMemberSummaries([AsParameters] GetMemberSummariesRequest request, GetMemberSummariesQueryHandler query)
        {
            var members = await query.Execute(request);
            return Results.Ok(members);
        }
        public static async Task<IResult> GetMemberDetails(int id, GetMemberDetailsQueryHandler query)
        {
            var member = await query.Execute(id);
            if (member is null) return Results.NotFound();
            return Results.Ok(member);
        }
        public static async Task<IResult> CreateMember(CreateMemberRequest request, CreateMemberCommandHandler handler)
        {
            try
            {
                var response = await handler.Execute(request);
                return Results.Created($"/members/{response.Id}", response);
            }
            catch (DomainException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
        }
        public static async Task<IResult> UpdateMember(int id, UpdateMemberRequest request, UpdateMemberCommandHandler handler)
        {
            try
            {
                var updated = await handler.Execute(id, request);
                if (!updated) return Results.NotFound();
                return Results.NoContent();
            }
            catch (DomainException exception)
            {
                return Results.BadRequest(new { error = exception.Message });
            }
        }
        public static async Task<IResult> DeleteMember(int id, DeleteMemberCommandHandler handler)
        {
            var deleted = await handler.Execute(id);
            if (!deleted) return Results.NotFound();
            return Results.NoContent();
        }

    }
}
