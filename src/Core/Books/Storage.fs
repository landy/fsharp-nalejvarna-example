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