namespace FSharpNalejvarna.Core.Books

open System
open FSharpNalejvarna.Core.Books

type BookListItem = { Id: Guid; Title: string }

type BooksFacade(booksStorage: BooksStorage) =
    member this.GetBooks () =
        task {
            let! rows = booksStorage.GetList()
            return rows
        }

    member this.CreateBook (book: AddBook.UnvalidatedBook) =
        task {
            let storeBook = booksStorage.Create

            return! AddBook.execute storeBook book
        }