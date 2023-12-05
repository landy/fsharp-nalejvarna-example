module FSharpNalejvarna.GiraffeApp.Books.BooksHandlers

open Giraffe
open Giraffe.GoodRead
open Microsoft.AspNetCore.Http

open FSharpNalejvarna.Core.Books

let booksListHandler (facade: BooksFacade) : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! list = facade.GetBooks()
            return! json list next ctx
        }

let createBookHandler (facade: BooksFacade) : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! book = ctx.BindJsonAsync<AddBook.UnvalidatedBook>()
            let! created = facade.CreateBook(book)

            match created with
            | Ok(AddBook.BookCreated bookId) ->
                ctx.SetHttpHeader("Location", $"/books/{bookId}")
                return! Successful.CREATED bookId next ctx
            | Error err ->
                ctx.SetStatusCode 400
                return! text err next ctx
        }

let booksHandler: HttpHandler =
    choose [
        subRoute
            "/books"
            (choose [
                GET >=> (Require.services<BooksFacade> booksListHandler)
                POST >=> (Require.services<BooksFacade> createBookHandler)
            ])
    ]