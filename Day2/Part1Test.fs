module AdventOfCode2024.Day2.Part1Test


open AdventOfCode2024.Day2.Parser
open FsUnit.Xunit
open Xunit

open AdventOfCode2024.Day2.Part1

[<Fact>]
let ``Example report #1 is safe`` () =
    let result = isSafeReport [ 7; 6; 4; 2; 1 ]
    result |> should equal true

[<Fact>]
let ``Example report #2 is unsafe`` () =
    let result = isSafeReport [ 1; 2; 7; 8; 9 ]
    result |> should equal false

[<Fact>]
let ``Example report #3 is unsafe`` () =
    let result = isSafeReport [ 9; 7; 6; 2; 1 ]
    result |> should equal false

[<Fact>]
let ``Example report #4 is unsafe`` () =
    let result = isSafeReport [ 1; 3; 2; 4; 5 ]
    result |> should equal false

[<Fact>]
let ``Example report #5 is unsafe`` () =
    let result = isSafeReport [ 8; 6; 4; 4; 1 ]
    result |> should equal false

[<Fact>]
let ``Example report #6 is safe`` () =
    let result = isSafeReport [ 1; 3; 6; 7; 9 ]
    result |> should equal true

[<Fact>]
let ``Example report set has 2 safe reports`` () =
    let result =
        countReportsWhere
            isSafeReport
            [ [ 1; 3; 6; 7; 9 ]
              [ 1; 2; 7; 8; 9 ]
              [ 9; 7; 6; 2; 1 ]
              [ 1; 3; 2; 4; 5 ]
              [ 8; 6; 4; 4; 1 ]
              [ 1; 3; 6; 7; 9 ] ]

    result |> should equal 2

[<Fact>]
let ``Exercise result`` () =
    let result = rawInput |> parseListOfListsOfIntegers |> countReportsWhere isSafeReport

    result |> should equal 383
