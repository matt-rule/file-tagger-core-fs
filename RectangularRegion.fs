namespace FileTaggerCoreFs.BusinessLogic

// TODO write tests; The whole file has been ported from C# and is so far untested

type public RectangularRegion(x1 : int, x2 : int, y1 : int, y2 : int) =

    static let getRegionsFromXYRanges (xRanges : (int * int) array) (yRanges : (int * int) array) : RectangularRegion seq =
        Seq.collect (fun yRange ->
            Seq.map (fun xRange -> RectangularRegion (fst xRange, snd xRange, fst yRange, snd yRange)) xRanges
        ) yRanges

    static let rangesForAxis (divisions : int) (axisLength : int) : (int * int) array =
        [| 0..divisions |]
        |> Array.map (fun x ->
            (
                int ((double axisLength) / (double divisions) * (double x)),
                int ((double axisLength) / (double divisions) * (double (x + 1))) - 1
            )
        )

    static member Divide (divisionsPerAxis : int, width : int, height : int) : RectangularRegion seq =
        getRegionsFromXYRanges
            (rangesForAxis divisionsPerAxis width)
            (rangesForAxis divisionsPerAxis height)
    
    member public this.X1 : int = x1
    member public this.X2 : int = x2
    member public this.Y1 : int = y1
    member public this.Y2 : int = y2