module Day5

    open System
    open System.IO

    let rec private containsMatch (str: string) list =     
        match list with
        | [] -> false
        | head::tail -> 
            if str.Contains(head) then true
            else containsMatch str tail

    let private isVowel char = 
        match char with
        | 'a' | 'e' | 'i' | 'o' | 'u' -> true
        | _ -> false

    let rec private contains3Vowels acc chars =
        if acc > 2 then true
        else 
        match chars with
        | [] -> 
            if acc > 2 then true 
            else false
        | head::tail -> 
            if isVowel head then contains3Vowels (acc+1) tail
            else contains3Vowels acc tail

    let private containsVowels (str:string) = 
        let chars = str.ToCharArray() |> Array.toList
        contains3Vowels 0 chars

    let private containsDisallowed str = 
        let disallowed = ["ab"; "cd"; "pq"; "xy"]
        containsMatch str disallowed

    let rec private isSeq str = 
        match str with
        | [] -> false
        | [x] -> false
        | x::xn::xt -> 
            if x = xn then true
            else isSeq(xn::xt)

    let private containsSeq (str: string) = 
        let l = str.ToCharArray() |> Array.toList
        isSeq l

    let private niceString str =
        str |> containsVowels &&
        str |> containsSeq &&
        not (str |> containsDisallowed)

    let rec private numOfNice func acc strs =
        match strs with  
        | [] -> acc
        | head::tail -> 
            if func head then numOfNice func (acc+1) tail
            else numOfNice func acc tail

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day5Input.txt"
        File.ReadLines(path) |> Seq.toList

    let private testInput = ["ugknbfddgicrmopn"; "aaa"; "jchzalrnumimnmhp"; "haegwjzuvuyypxyu"; "dvszwmarrgswjxmb"] //correct answer is 2

    let answer = 
        numOfNice niceString 0 input

    //part 2

    let rec pair chars = 
         match chars with
         | [] -> false
         | [x] -> false
         | [x; x1] -> false
         | x::x1::x2::tail ->
            if x <> x1 || (x = x1 && x1 <> x2) then 
                let p = x::x1::[] |> List.toArray |> String
                let remainingString = x2::tail |> List.toArray |> String
                if remainingString.Contains(p) then true
                else pair (x1::x2::tail)
            else pair (x1::x2::tail)

    let containsPair (str:string) = 
        let chars = str.ToCharArray() |> Array.toList
        pair chars

    let rec repeats chars = 
        match chars with 
        | [] -> false
        | [x] -> false
        | [x; x1] -> false
        | x::x1::x2::tail ->
            if x = x2 then true
            else repeats (x1::x2::tail)

    let containsRepeat (str:string) = 
        let chars = str.ToCharArray() |> Array.toList
        repeats chars

    let isAlsoNiceString str =
        str |> containsPair &&
        str |> containsRepeat

    let testInput2 = ["aaa"; "qjhvhtzxzqqjkmpb"; "xxyxx"; "uurcxstgmygtbstg"; "ieodomkazucvgmuy"; "aabaaa"]
    let testInput3 = ["aabaaa"]

    let answer2 = 
        numOfNice containsPair 0 testInput2
        //numOfNice isAlsoNiceString 0 input
        //containsRepeat 