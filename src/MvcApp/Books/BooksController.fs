namespace FSharpNalejvarna.MvcApp.Books

open Microsoft.AspNetCore.Mvc

open FSharpNalejvarna.Core.Books


[<Route("api/books")>]
type BooksController(facade: BooksFacade) =
    inherit ControllerBase()

    [<HttpGet>]
    [<Route("")>]
    member _.GetList () =
        task {
            let! books = facade.GetBooks()

            return books
        }
