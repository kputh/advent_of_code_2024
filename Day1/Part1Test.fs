module AdventOfCode2024.Day1.Part1Test

open FsUnit.Xunit
open Xunit

open AdventOfCode2024.Day1.Part1
open AdventOfCode2024.Day1.Parser

[<Fact>]
let ``Sum of distances of empty lists is 0`` () =
    let sum = calculateSumOfDistance ([], [])
    sum |> should equal 0

[<Fact>]
let ``Sum of distances of identical lists is 0`` () =
    let sum = calculateSumOfDistance ([ 1; 2; 3 ], [ 1; 2; 3 ])
    sum |> should equal 0

[<Fact>]
let ``Sum of distances of lists with same elements is 0`` () =
    let sum = calculateSumOfDistance ([ 1; 2; 3 ], [ 3; 2; 1 ])
    sum |> should equal 0

[<Fact>]
let ``Mixed test case`` () =
    let sum = calculateSumOfDistance ([ -2; 12 ], [ 13; -1 ])
    sum |> should equal 2

[<Fact>]
let ``Exercise result`` () =
    let result = rawInput |> parsePairsOfIntegers |> calculateSumOfDistance

    result |> should equal 0
