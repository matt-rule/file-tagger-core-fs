namespace FileTaggerCoreFs.BusinessLogic

open System
open System.Drawing
open System.Threading.Tasks

module public ImageProcessing =

    let AveragePixelColour (image : Bitmap) (region : RectangularRegion) =
        let mapRegions f =
            [region.X1..region.X2 - 1]
            |> List.collect (fun x ->
                [region.Y1..region.Y2 - 1]
                |> List.map (fun y -> double (f x y))
            )
            |> List.average
        
        [|
            mapRegions (fun x y -> image.GetPixel(x,y).R);
            mapRegions (fun x y -> image.GetPixel(x,y).G);
            mapRegions (fun x y -> image.GetPixel(x,y).B)
        |]

    let PixelColourAverages (bmp : Bitmap) (divisionsPerAxis : int) : double array seq =
        RectangularRegion.Divide (divisionsPerAxis, bmp.Width, bmp.Height)
        |> Seq.map (AveragePixelColour bmp)

    let PixelColourAveragesGivenFilename (imageFilename : string) (divisionsPerAxis : int) : double array seq =
        let bmp = new Bitmap(imageFilename)
        RectangularRegion.Divide (divisionsPerAxis, bmp.Width, bmp.Height)
        |> Seq.map(fun x -> AveragePixelColour bmp x)

    let FromBitmap(image : Bitmap, divisionsPerAxis : int) : string =
        let a = 
            PixelColourAverages image divisionsPerAxis
            |> Seq.map (fun x -> ShortImageString.GetStringFromAverage(x))
        a
        |> String.Concat

    let FromBitmapFile (filename : string) (divisionsPerAxis : int) : string =
        FromBitmap (new Bitmap(filename), divisionsPerAxis)

    // let FilenamesMappedToImageStringsJson (divisionsPerAxis : int) : string =
    //     use context = new TaggerDBContext(new DbContextOptions<TaggerDBContext>())
