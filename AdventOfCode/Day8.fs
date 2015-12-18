module Day8
    
    open System.IO
    open System.Text.RegularExpressions

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day8Input.txt"
        File.ReadLines(path)

    let private codeLength = input |> Seq.sumBy (fun s -> s.Length)

    let answer =        
        let stringLength = input |> Seq.sumBy (fun s -> (Regex.Unescape (s.Substring (1, s.Length - 2))).Length)
        codeLength - stringLength

    let answer2 = 
         let stringLength = input |> Seq.sumBy (fun s -> (Regex.Escape s).Length + (s |> Seq.filter (fun c -> c = '"') |> Seq.length) + 2)
         stringLength - codeLength