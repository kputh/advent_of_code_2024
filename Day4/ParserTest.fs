module AdventOfCode2024.Day4.ParserTest

open FsUnit.Xunit
open Xunit

open AdventOfCode2024.Day4.Parser

[<Fact>]
let ``Can parse example #1`` () =
    let result =
        parseCharGrid
            "
..X...
.SAMX.
.A..A.
XMAS.S
.X....
"

    result
    |> should
        equal
        [| [| '.'; '.'; 'X'; '.'; '.'; '.' |]
           [| '.'; 'S'; 'A'; 'M'; 'X'; '.' |]
           [| '.'; 'A'; '.'; '.'; 'A'; '.' |]
           [| 'X'; 'M'; 'A'; 'S'; '.'; 'S' |]
           [| '.'; 'X'; '.'; '.'; '.'; '.' |] |]
