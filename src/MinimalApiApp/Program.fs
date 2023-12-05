module FSharpNalejvarna.MinimalApiApp.Program

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection

open FSharpNalejvarna.Core.Books
open FSharpNalejvarna.MinimalApiApp.Books


let builder = WebApplication.CreateBuilder()

builder.Services.AddEndpointsApiExplorer() |> ignore
builder.Services.AddSwaggerGen() |> ignore

builder |> Books.register

let app = builder.Build()

app.UseSwagger() |> ignore
app.UseSwaggerUI() |> ignore

app |> BookEndpoints.RegisterBooksEndpoints |> ignore

app.Run()
