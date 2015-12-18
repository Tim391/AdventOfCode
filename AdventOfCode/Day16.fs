module Day16
    
    open System.IO
    open System.Text.RegularExpressions

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day16Input.txt"
        File.ReadLines(path) |> Seq.toList

    let private clues = [("children", 3); ("cats", 7); ("samoyeds", 2); ("pomeranians", 3); ("akitas", 0); ("vizslas", 0); ("goldfish", 5); ("trees", 3); ("cars", 2); ("perfumes", 1)]

    let private getTrait (m: Match) = 
        let vals = m.Value.Split ':'
        (vals.[0], int vals.[1])

    let private answer1Match c t =
        c = t 

    let private answer2Match c t = 
        let c1, c2 = c
        let t1, t2 = t
        if c1 <> t1 then false
        else
        match c1 with
        | "cats" | "trees" -> t2 > c2
        | "pomeranians" | "goldfish" -> t2 < c2
        | _ -> t2 = c2

    let private matchClues clues sue f = 
        let sueNum = Regex.Match(sue, "^Sue (\d+)").Groups.[1].Value |> int
        let isMatch = 
            Regex.Matches(sue, "(\w+)\: (\d+)") 
            |> Seq.cast<Match> 
            |> Seq.map (fun m -> getTrait m)
            |> Seq.forall (fun t -> List.exists (fun c -> f c t) clues)

        if isMatch then sueNum
        else 0

    let rec private findSue clues input f = 
        match input with
        | [] -> 0
        | head::tail -> 
            let sue = matchClues clues head f
            if sue > 0 then sue
            else findSue clues tail f

    let answer = 
        findSue clues input answer1Match

    let answer2 = 
        findSue clues input answer2Match