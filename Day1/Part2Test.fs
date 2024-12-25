module AdventOfCode2024.Day1.Part2Test

open FsUnit.Xunit
open Xunit

open AdventOfCode2024.Day1.Part2
open AdventOfCode2024.Day1.Parser

[<Fact>]
let ``Sum of products of empty lists is 0`` () =
    let sum = sumOfProducts ([], [])
    sum |> should equal 0

[<Fact>]
let ``Case #2`` () =
    let sum = sumOfProducts ([ 1; 2; 3 ], [ 1; 2; 3; 2; 3; 3 ])
    sum |> should equal 14

[<Fact>]
let ``Case #3`` () =
    let sum = sumOfProducts ([ 1; 2; 3 ], [])
    sum |> should equal 0

[<Fact>]
let ``Exercise result`` () =
    let sum = rawInput |> parsePairsOfIntegers |> sumOfProducts

    sum |> should equal 22962826
