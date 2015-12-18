module Day12

    open System.IO
    open Newtonsoft.Json
    open Newtonsoft.Json.Linq

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day12Input.txt"
        File.ReadAllText(path)
        |> JsonConvert.DeserializeObject
        |> JArray

    let rec sum (j: obj) = 
        match j with
        | :? JObject as jo -> jo |> Seq.sumBy (fun j -> sum j)
        | :? JArray as ja-> ja |> Seq.sumBy (fun j -> sum j)
        | :? JProperty as jp ->  jp |> Seq.sumBy (fun j -> sum j)
        | :? JValue as jv -> 
            if jv.Type = JTokenType.Integer then int jv else 0

    let totalSum = 
        Seq.fold (fun acc jt -> acc+sum jt) 0 input

    let answer() = 
        totalSum