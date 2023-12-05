[<RequireQualifiedAccess>]
module FSharpNalejvarna.Core.Books.AddBook

open System
open System.Threading.Tasks

type UnvalidatedBook = { Title: string; Author: string }

type ValidatedBook = { Title: string; Author: string }

type BookCreated = BookCreated of Guid

type StoreBook = Guid -> ValidatedBook -> Task<unit>

let private validateBook (book: UnvalidatedBook) =
    if String.IsNullOrWhiteSpace(book.Title) then
        Error "Title is required"
    elif String.IsNullOrWhiteSpace(book.Author) then
        Error "Author is required"
    else
        Ok {
            ValidatedBook.Title = book.Title
            Author = book.Author
        }

let private createBook (saveBook: StoreBook) (book: ValidatedBook) =
    task {
        let newBookId = Guid.NewGuid()
        let! _ = saveBook newBookId book
        return BookCreated newBookId
    }

let execute
    (storeBook: StoreBook)
    unvalidatedBook
    : Task<Result<BookCreated, string>> =
    task {
        let validated = validateBook unvalidatedBook

        let! res =
            match validated with
            | Ok book ->
                task {
                    let! res = createBook storeBook book
                    return Ok res
                }
            | Error err -> Error err |> Task.FromResult

        return res
    }