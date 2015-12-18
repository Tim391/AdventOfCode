module Day12

    open System.IO
    open Newtonsoft.Json
    open Newtonsoft.Json.Linq

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day12Input.txt"
        File.ReadAllText(path)
        |> JsonConvert.DeserializeObject
        |> JArray

    let rec private sum (j: obj) = 
        match j with
        | :? JObject as jo -> jo |> Seq.sumBy (fun j -> sum j)
        | :? JArray as ja-> ja |> Seq.sumBy (fun j -> sum j)
        | :? JProperty as jp ->  jp |> Seq.sumBy (fun j -> sum j)
        | :? JValue as jv -> 
            if jv.Type = JTokenType.Integer then int jv else 0

    let answer() = 
        Seq.fold (fun acc jt -> acc+sum jt) 0 input

    //Part 2

    let private isRed (jo:JObject) =
        let values = 
            jo.Properties()
            |> Seq.filter (fun jp -> jp.Value :? JValue) 
            |> Seq.filter (fun jp -> jp.Value.ToString().Contains("red"))
            |> Seq.length

        if values > 0 then true else false

    let rec private sumNoRed (j: obj) = 
        match j with
        | :? JObject as jo -> jo |> (fun jo -> if isRed jo then 0 else jo |> Seq.sumBy (fun j -> sumNoRed j))
        | :? JArray as ja-> ja |> Seq.sumBy (fun j -> sumNoRed j)
        | :? JProperty as jp ->  jp |> Seq.sumBy (fun j -> sumNoRed j)
        | :? JValue as jv -> 
            if jv.Type = JTokenType.Integer then int jv else 0

    let answer2() = 
        Seq.fold (fun acc jt -> acc+sumNoRed jt) 0 input