[<RequireQualifiedAccess>]
module FSharpNalejvarna.Core.Books.Books


open System.Data.SqlClient
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Configuration

open FSharpNalejvarna.Core.Books


let register (builder: WebApplicationBuilder) =
    let connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")

    builder.Services.AddScoped<SqlConnection>(fun _ ->
        new SqlConnection(connectionString))
    |> ignore

    builder.Services.AddTransient<BooksStorage>() |> ignore
    builder.Services.AddTransient<BooksFacade>() |> ignore