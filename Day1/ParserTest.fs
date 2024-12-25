module AdventOfCode2024.Day1.ParserTest

open FsUnit.Xunit
open Xunit

open AdventOfCode2024.Day1.Parser

[<Fact>]
let ``Can parse input`` () =
    let result =
        parsePairsOfIntegers
            "
98415   86712
21839   96206
"

    result |> should equal ([ 98415; 21839 ], [ 86712; 96206 ])
