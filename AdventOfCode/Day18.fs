module Day18

    open System.IO
    open System

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day18Input.txt"
        File.ReadLines(path) |> Seq.map (fun s -> 
        s.Replace('#', '1').Replace('.', '0')) 
        |> array2D
        |> Array2D.map (fun e -> Int32.Parse(e.ToString()))


    let private nextValue i j e (currArray: int[,]) = 
        let inGrid n = n >= 0 && n < 100
        let mutable sum = 0
        for x in [i-1..i+1] do
            for y in [j-1..j+1] do
                if inGrid x && inGrid y && (x <> i || y <> j) then sum <- sum + currArray.[x,y]

        if e = 1 then 
            if sum = 2 || sum = 3 then 1 else 0
        else 
            if sum = 3 then 1 else 0

    let private nextCornervalue i j e (currArray: int[,]) = 
        match (i,j) with
        | (0,0) | (0,99) | (99,0) | (99,99) -> 1
        | _ -> nextValue i j e currArray

    let private nextArray currArray = 
        currArray |> Array2D.mapi (fun x y e -> nextValue x y e currArray)

    let private nextCornerArray currArray =
        currArray |> Array2D.mapi (fun x y e -> nextCornervalue x y e currArray)

    let rec private finalArray acc currArray f = 
        match acc with
        | 100 -> currArray
        | _ -> finalArray (acc+1) (f currArray) f

    let answer = 
        finalArray 0 input nextArray |> Seq.cast<int> |> Seq.sum

    let answer2 =       
        input.[0,0] <- 1
        input.[0,99] <- 1
        input.[99,0] <- 1
        input.[99,99] <- 1

        finalArray 0 input nextCornerArray |> Seq.cast<int> |> Seq.sum

