// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System 

[<EntryPoint>]
let main argv = 

    let answer = Day12.answer2()

    printfn "%A" answer
    Console.ReadLine() |> ignore
    0 // return an integer exit code
