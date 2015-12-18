module Day10
    
    open System

    let private insert e state = 
        match state with
        | cur::rest ->
            match cur with
            | h::_ when e = h ->  (e::cur)::rest // add e to current list
            | _ -> [e]::state   // start a new list 
        | _ -> [[e]]
        
    let private nextSeq input = 
        let numbers = 
            input 
            |> Seq.map (fun n -> Int32.Parse(n.ToString())) 
            |> Seq.toList

        let runs = List.foldBack insert numbers []

        let nextBlock currentBlock (run: list<int>) =
            let runLength = run.Length
            let num = List.head run
            currentBlock + runLength.ToString() + num.ToString()

        List.fold (fun acc n -> nextBlock acc n) "" runs
        
        
    let private initialInput = "3113322113"

    let rec private finalSeq current count = 
        //Console.WriteLine(current.ToString() + " current loop:" + count.ToString()) |> ignore   
        Console.WriteLine("current loop:" + count.ToString()) |> ignore 
        if count = 50 then current
        else 
            let next = nextSeq current
            let newCount = count+1
            finalSeq next newCount

    let private final = finalSeq initialInput 0

    let answer = final.Length          

