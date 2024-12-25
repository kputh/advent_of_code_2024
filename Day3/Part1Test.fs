module AdventOfCode2024.Day3.Part1Test

open AdventOfCode2024.Day3.Part1
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Can parse example`` () =
    let result =
        extractPairs "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"

    result |> should equal [ (2, 4); (5, 5); (11, 8); (8, 5) ]

[<Fact>]
let ``Exercise result`` () =
    let result = processInput rawInput

    result |> should equal 179834255
