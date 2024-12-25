module AdventOfCode2024.Day3.Part2Test

open AdventOfCode2024.Day3.Part1
open AdventOfCode2024.Day3.Part2
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Can parse example`` () =
    let result =
        extractPairs2 "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"

    result |> should equal [ (2, 4); (8, 5) ]

[<Fact>]
let ``Can process example`` () =
    let result =
        processInput2 "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"

    result |> should equal 48
    
[<Fact>]
let ``Exercise result`` () =
    let result = processInput2 rawInput

    result |> should equal 170819695