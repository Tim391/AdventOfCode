module Day14

    open System

     type private Reindeer (name: string, speed: int, burst: int, rest: int) =
        let mutable distance = 0
        let mutable burstCount = 0
        let mutable restCount = 0
        let mutable points = 0
        let mutable resting = false

        member this.Name = name

        member this.Speed = speed
        member this.Burst = burst
        member this.Rest = rest

        member this.Tick = 
            if resting then
                restCount <- restCount+1
                if restCount = rest then
                    resting <- false
                    restCount <- 0
            else
                distance <- distance+speed
                burstCount <- burstCount+1
                if burstCount = burst then
                    resting <- true
                    burstCount <- 0

        member this.Distance = distance            
        member this.AddPoint = points <- points+1
        member this.Points = points  
                    

    let private Dancer = new Reindeer("Dancer", 27, 5, 132)
    let private Cupid = new Reindeer("Cupid", 22, 2, 41)
    let private Rudolph = new Reindeer("Rudolph", 11, 5, 48)
    let private Donner = new Reindeer("Donner", 28, 5, 134)
    let private Dasher = new Reindeer("Dasher", 4, 16, 55)
    let private Blitzen = new Reindeer("Blitzen", 14, 3, 38)
    let private Prancer = new Reindeer("Prancer", 3, 21, 40)
    let private Comet = new Reindeer("Comet", 18, 6, 103)
    let private Vixen = new Reindeer("Vixen", 18, 5, 84)

    let private allReindeer = [Dancer; Cupid; Rudolph; Donner; Dasher; Blitzen; Prancer; Comet; Vixen]

    let private time = 2503

    let private race time = 
        for i in 0..time do
            allReindeer |> List.iter (fun r -> r.Tick)
            allReindeer |> List.groupBy (fun r -> r.Distance) |> List.maxBy fst |> snd |> List.iter (fun r -> r.AddPoint)
        allReindeer

    let answer() =
        race time |> ignore
        allReindeer |> List.map (fun r -> r.Distance) |> List.max

    let answer2() = 
        race time |> ignore
        let totals = allReindeer |> List.map (fun r -> r.Points) |> List.sum
        allReindeer |> List.map (fun r -> r.Points) |> List.max

