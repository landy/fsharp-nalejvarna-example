[<RequireQualifiedAccess>]
module FSharpNalejvarna.Core.Books.BookDetail

open System

type BookDetail = {
    Id: Guid
    Title: string
    Author: string
}