module Day3

    open System
    open System.IO

    let private inputList = 
        let path = __SOURCE_DIRECTORY__ + "\Day3Input.txt"
        let line = File.ReadAllText(path)
        line.ToCharArray() |> Array.toList

    let private newLocation curr dir =
        let (x, y) = curr
        match dir with
        | '^' -> (x, y+1)
        | 'v' -> (x, y-1)
        | '<' -> (x-1, y)
        | '>' -> (x+1, y)


    let rec private allLocations acc curr input = 
        match input with
        | [] -> acc
        | head::tail ->
            let newloc = newLocation curr head
            allLocations (newloc::acc) newloc tail

    let private numDistinct coords = 
        let groups = coords |> Seq.groupBy (fun x -> x) |> Seq.toList
        groups.Length

    let private everyOdd elements =
        elements
        |> Seq.mapi (fun i e -> if i % 2 = 1 then Some(e) else None)
        |> Seq.choose id
        |> Seq.toList

    let private everyEven elements =
        elements
        |> Seq.mapi (fun i e -> if i % 2 = 0 then Some(e) else None)
        |> Seq.choose id
        |> Seq.toList

    let answer = 
        let coords = allLocations [(0,0)] (0,0) inputList
        numDistinct coords

    let answer2 = 
        let santasList = everyEven inputList
        let roboSantasList = everyOdd inputList
        let santasCoords = allLocations [(0,0)] (0,0) santasList
        let roboSantasCoords = allLocations [(0,0)] (0,0) roboSantasList
        let finalCoords = List.append santasCoords roboSantasCoords
        numDistinct finalCoords
        
