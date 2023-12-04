namespace FSharpNalejvarna.MvcApp

open Microsoft.AspNetCore.Mvc

[<Route("api/[controller]")>]
type ApiController () =
    inherit ControllerBase()