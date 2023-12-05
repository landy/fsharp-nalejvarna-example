namespace FSharpNalejvarna.Core.Books

open System

type BookListItem = { Id: Guid; Title: string }

type BooksFacade(booksStorage: BooksStorage) =
    member this.GetBooks () =
        task {
            let! rows = booksStorage.GetList()
            return rows
        }