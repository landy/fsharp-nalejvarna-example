module FSharpNalejvarna.GiraffeApp.Books.BooksHandlers

open Giraffe
open Giraffe.GoodRead
open Microsoft.AspNetCore.Http

open FSharpNalejvarna.Core.Books

let booksListHandler (facade: BooksFacade) : HttpHandler =
    fun  (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! list = facade.GetBooks()
            return! json list next ctx
        }

let booksHandler: HttpHandler =
    choose [ GET >=> subRoute "/books" ( Require.services<BooksFacade> booksListHandler )  ]