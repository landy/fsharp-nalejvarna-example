namespace FSharpNalejvarna.Core.Books

open System
open FSharpNalejvarna.Core.Books


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

    member this.GetBook (bookId: Guid) =
        task {
            let! book = booksStorage.Get(bookId)
            return book
        }