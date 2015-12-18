module Day4

    open System

    let private md5 = System.Security.Cryptography.MD5.Create()

    let private md5Hash (input : string) =
        input
        |> System.Text.Encoding.ASCII.GetBytes
        |> md5.ComputeHash
        |> Seq.map (fun c -> c.ToString("X2"))
        |> Seq.reduce (+)


    let rec private findKey (n: int) =
        if n % 100000 = 0 then Console.WriteLine(n)
        let hash = md5Hash("yzbqklnj"+ n.ToString())
        let valid = hash.Substring(0, 6)
        match valid with
        | "000000" -> n
        | _ -> findKey (n+1)

    let answer = 
        findKey 0


