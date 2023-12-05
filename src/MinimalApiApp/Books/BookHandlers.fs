namespace FSharpNalejvarna.MinimalApiApp.Books

open System
open FSharpNalejvarna.Core.Books
open Microsoft.AspNetCore.Builder

type BookEndpoints() =
    static member GetList (facade: BooksFacade) =
        task { return! facade.GetBooks() }

    static member RegisterBooksEndpoints (app: WebApplication) =
        app.MapGet("/api/books", Func<_, _>(BookEndpoints.GetList))
