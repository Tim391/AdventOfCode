module Day6

    open System.Text.RegularExpressions
    open System
    open System.IO
  
    let private toggle start finish (grid: int [,]) = 
        let x1, y1 = start
        let x2, y2 = finish
        for i in x1..x2 do
            for j in y1..y2 do
                grid.[i, j] <- grid.[i, j]+2
//                if grid.[i, j] = 0 then grid.[i, j] <- 1
//                else grid.[i, j] <- 0

    let private turnOn start finish (grid: int [,]) = 
        let x1, y1 = start
        let x2, y2 = finish
        for i in x1..x2 do
            for j in y1..y2 do
                grid.[i, j] <- grid.[i, j]+1

    let private turnOff start finish (grid: int [,]) =
        let x1, y1 = start
        let x2, y2 = finish
        for i in x1..x2 do
            for j in y1..y2 do
                if grid.[i, j] > 0 then grid.[i, j] <- grid.[i, j]-1
               
    
    let private totalOn grid = 
        grid |> Seq.cast<int> |> Seq.sum

    let private runCommand str grid = 
        let coords = Regex.Split(str, @"\D+")
        let start = (Int32.Parse(coords.[1]), Int32.Parse(coords.[2])) //seem to have empty element at index 0
        let finish = (Int32.Parse(coords.[3]),Int32.Parse(coords.[4]))

        match str with
        | s when s.Contains("toggle") -> toggle start finish grid
        | s when s.Contains("turn off") -> turnOff start finish grid
        | s when s.Contains("turn on") -> turnOn start finish grid


    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day6Input.txt"
        File.ReadLines(path) |> Seq.toList

    let answer =
        let mutable grid = Array2D.init 1000 1000 (fun x y -> 0)
//        turnOn (0, 0) (999, 999) grid |> ignore
//        toggle (0,0) (999,999) grid |> ignore
        input |> List.iter (fun x -> runCommand x grid) 
        totalOn grid

//        let i = "turn off 370,39 through 425,839"
//        Regex.Split(i, @"\D+")