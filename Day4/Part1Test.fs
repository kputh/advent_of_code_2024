module AdventOfCode2024.Day4.Part1Test

open AdventOfCode2024.Day4.Input
open AdventOfCode2024.Day4.Parser
open AdventOfCode2024.Day4.Part1
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Can count pattern in example line #1 - one occurence`` () =
    let result =
        "MMMSXXMASM" |> Seq.toArray |> countMatchesInArray [| 'X'; 'M'; 'A'; 'S' |]

    result |> should equal 1

[<Fact>]
let ``Can count pattern in reverse example line #2 - one occurrence`` () =
    let result =
        "MSAMXMSMSA"
        |> Seq.toArray
        |> countMatchesInReverseArray [| 'X'; 'M'; 'A'; 'S' |]

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

    let result =
        grid |> parseCharGrid |> countPatternHorizontally [| 'X'; 'M'; 'A'; 'S' |]

    result |> should equal 5

[<Fact>]
let ``Can rotate grid (by one character)`` () =
    let result =
        """
            abcd
            efgh
            ijkl
            mnop
            """
        |> parseCharGrid
        |> rotateGrid

    result
    |> should
        equal
        ("""
            abcd....
            .efgh...
            ..ijkl..
            ...mnop.
            """
         |> parseCharGrid)

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

    let result =
        grid |> parseCharGrid |> countPatternVertically [| 'X'; 'M'; 'A'; 'S' |]

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

    let result =
        grid |> parseCharGrid |> countPatternDiagonally [| 'X'; 'M'; 'A'; 'S' |]

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

    let result =
        exampleGrid
        |> parseCharGrid
        |> countPatternInAllDirections [| 'X'; 'M'; 'A'; 'S' |]

    result |> should equal 18

[<Fact>]
let ``Exercise result`` () =
    let result =
        rawInput
        |> parseCharGrid
        |> countPatternInAllDirections [| 'X'; 'M'; 'A'; 'S' |]

    result |> should equal 2685
