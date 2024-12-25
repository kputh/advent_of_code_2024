module AdventOfCode2024.Day2.Part2Test

open AdventOfCode2024.Day2.Parser
open AdventOfCode2024.Day2.Part1
open AdventOfCode2024.Day2.Part2
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Example report #1 is safe enough`` () =
    let result = isSafeEnoughReport [ 7; 6; 4; 2; 1 ]
    result |> should equal true

[<Fact>]
let ``Example report #2 is unsafe`` () =
    let result = isSafeEnoughReport [ 1; 2; 7; 8; 9 ]
    result |> should equal false

[<Fact>]
let ``Example report #3 is unsafe`` () =
    let result = isSafeEnoughReport [ 9; 7; 6; 2; 1 ]
    result |> should equal false

[<Fact>]
let ``Example report #4 is safe enough`` () =
    let result = isSafeEnoughReport [ 1; 3; 2; 4; 5 ]
    result |> should equal true

[<Fact>]
let ``Example report #5 is safe enough`` () =
    let result = isSafeEnoughReport [ 8; 6; 4; 4; 1 ]
    result |> should equal true

[<Fact>]
let ``Example report #6 is safe enough`` () =
    let result = isSafeEnoughReport [ 1; 3; 6; 7; 9 ]
    result |> should equal true

[<Fact>]
let ``Example report set has 4 safe enough reports`` () =
    let result =
        countReportsWhere isSafeEnoughReport
            [ [ 1; 3; 6; 7; 9 ]
              [ 1; 2; 7; 8; 9 ]
              [ 9; 7; 6; 2; 1 ]
              [ 1; 3; 2; 4; 5 ]
              [ 8; 6; 4; 4; 1 ]
              [ 1; 3; 6; 7; 9 ] ]

    result |> should equal 4

[<Fact>]
let ``Exercise result`` () =
    let result = rawInput |> parseListOfListsOfIntegers |> countReportsWhere isSafeEnoughReport

    result |> should equal 436
