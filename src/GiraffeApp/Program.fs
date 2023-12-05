module FSharpNalejvarna.GiraffeApp.Program

open Microsoft.AspNetCore.Builder
open Giraffe

open FSharpNalejvarna.Core.Books
open FSharpNalejvarna.GiraffeApp

let builder = WebApplication.CreateBuilder()

builder.Services.AddGiraffe() |> ignore
builder |> Books.register

let app = builder.Build()

let api =
    choose [
        route "/" >=> text "Hello World!"
        subRoute "/api" Books.BooksHandlers.booksHandler
    ]

app.UseGiraffe api

app.Run()
