namespace FSharpNalejvarna.MvcApp.Books

open Microsoft.AspNetCore.Mvc

open FSharpNalejvarna.MvcApp
open FSharpNalejvarna.Core.Books


type BooksController () =
    inherit ApiController()
    
    [<HttpGet>]
    member _.GetList() = task {
        return List.empty<BookListItem>
    }