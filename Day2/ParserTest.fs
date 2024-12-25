module AdventOfCode2024.Day2.ParserTest

open FsUnit.Xunit
open Xunit

open AdventOfCode2024.Day2.Parser

[<Fact>]
let ``Can parse input`` () =
    let result =
        parseListOfListsOfIntegers
            "
62 65 67 70 73 76 75
68 71 73 76 78 78
77 80 81 82 86
"

    result
    |> should
        equal
        [ [ 62; 65; 67; 70; 73; 76; 75 ]
          [ 68; 71; 73; 76; 78; 78 ]
          [ 77; 80; 81; 82; 86 ] ]
