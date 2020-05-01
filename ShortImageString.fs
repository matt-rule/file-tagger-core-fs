namespace FileTaggerCoreFs.BusinessLogic

open System.Drawing

module public ShortImageString =
    [<Literal>] 
    let SisChars = "0123456789ABCDEF"

    let ValueToSisChar (value : int) char =
        match value with
        | x when x < 0 -> SisChars |> Seq.head
        | x when x > (SisChars |> Seq.length) -> SisChars |> Seq.last
        | x -> SisChars |> (Seq.item x)

    let CharToValue (c : char) : int =
        let index = SisChars.IndexOf(c)
        if index = -1 then 0 else index

    let Difference (s1 : string, s2 : string) : int =
        Seq.zip s1 s2
        |> Seq.sumBy (fun (x, y) ->
            abs((int x) - (int y))
        )

    let GetStringFromAverage(avg : double array) : string =
        avg
        |> Seq.map (
            fun channel -> channel / 255.0 * (double)((SisChars |> Seq.length) - 1)
            >> int
            >> ValueToSisChar
            >> (fun x -> x.ToString())
        )
        |> Seq.reduce (+)

    // let DifferenceBetweenImageFiles(filename1 : string, filename2 : string, divisionsPerAxis : int) : int =
    //     Difference(
    //         FromBitmapFile(filename1, divisionsPerAxis),
    //         FromBitmapFile(filename2, divisionsPerAxis)
    //     );
    