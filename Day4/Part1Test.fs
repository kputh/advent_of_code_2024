module AdventOfCode2024.Day4.Part1Test

open AdventOfCode2024.Day4.Parser
open AdventOfCode2024.Day4.Part1
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Can count pattern in example line #1 - one occurence`` () =
    let result = countMatchesInArray pattern (Seq.toArray "MMMSXXMASM")

    result |> should equal 1

[<Fact>]
let ``Can count pattern in reverse example line #2 - one occurrence`` () =
    let result = countMatchesInReverseArray pattern (Seq.toArray "MSAMXMSMSA")

    result |> should equal 1


[<Fact>]
let ``Can count pattern horizontally`` () =
    let grid =
        """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
"""

    let result = countPatternHorizontally pattern (parseCharGrid grid)

    result |> should equal 5

[<Fact>]
let ``Can count pattern vertically`` () =
    let grid =
        """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
"""

    let result = countPatternVertically pattern (parseCharGrid grid)

    result |> should equal 3

[<Fact>]
let ``Can count pattern diagonally`` () =
    let grid =
        """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
"""

    let result = countPatternDiagonally pattern (parseCharGrid grid)

    result |> should equal 10


[<Fact>]
let ``Can count pattern in example grid`` () =
    let exampleGrid =
        """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
"""

    let result = countPatternInAllDirections pattern (parseCharGrid exampleGrid)

    result |> should equal 18

[<Fact>]
let ``Exercise result`` () =
    let result = rawInput |> parseCharGrid |> countPatternInAllDirections pattern

    result |> should equal 2685
