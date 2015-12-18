module Day17
    
    open System.IO

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day17Input.txt"
        File.ReadLines(path) |> Seq.toList |> List.map (fun n -> n |> int)

    let private total = 150

    let rec private powerset s = 
      seq {
        match s with
        | [] -> yield []
        | h::t -> for x in powerset t do yield! [x; h::x]
      }

    let private combinations = powerset input

    let private sumIsTotal total com =
        com |> Seq.sum = total

    let rec private collectFull matches coms total =
        match coms with 
        | [] -> matches
        | head::tail -> 
            if sumIsTotal total head then collectFull (head::matches) tail total
            else collectFull matches tail total 

    let answer = 
        Seq.fold (fun acc com -> if sumIsTotal total com then acc+1 else acc) 0 combinations

    let answer2 = 
        let matchingCombos = collectFull [] (combinations |> Seq.toList) total
        matchingCombos |> Seq.groupBy (fun l -> l.Length) |> Seq.minBy fst |> snd |> Seq.length