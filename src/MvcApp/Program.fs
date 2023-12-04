module FSharpNalejvarna.MvcApp.Program

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

let builder = WebApplication.CreateBuilder()

builder.Services.AddControllers() |> ignore
builder.Services.AddSwaggerGen() |> ignore

let app = builder.Build()

if app.Environment.IsDevelopment() then
    app.UseDeveloperExceptionPage() |> ignore
    app.UseSwagger() |> ignore
    app.UseSwaggerUI() |> ignore
else
    app.UseExceptionHandler("/Error") |> ignore

app.MapControllers() |> ignore

app.Run()