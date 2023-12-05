namespace FSharpNalejvarna.Core.Books

open System
open System.Data.SqlClient
open System.Threading.Tasks
open Dapper.FSharp.MSSQL

open FSharpNalejvarna.Core.Books.BooksList

type private BookRow = {
    Id: Guid
    Title: string
    Author: string
}

type BooksStorage(connection: SqlConnection) =
    let booksTable = table'<BookRow> "Books"

    member _.GetList () : Task<BookListItem list> =
        task {
            let! rows =
                select {
                    for book in booksTable do
                        selectAll
                }
                |> connection.SelectAsync<BookRow>


            let items =
                rows
                |> List.ofSeq
                |> List.map (fun row -> {
                    BookListItem.Id = row.Id
                    Title = row.Title
                })

            return items
        }

    member _.Create (bookId: Guid) (book: AddBook.ValidatedBook) : Task<unit> =
        task {
            let row = {
                Id = bookId
                Title = book.Title
                Author = book.Author
            }

            let! _ =
                insert {
                    into booksTable
                    value row
                }
                |> connection.InsertAsync

            return ()
        }

    member _.Get (bookId: Guid) =
        task {
            let! rows =
                select {
                    for book in booksTable do
                    where (book.Id = bookId)
                }
                |> connection.SelectAsync<BookRow>

            return
                rows
                |> List.ofSeq
                |> List.tryHead
                |> Option.map (fun row -> {
                    BookDetail.Id = row.Id
                    BookDetail.Title = row.Title
                    BookDetail.Author = row.Author
                })
        }