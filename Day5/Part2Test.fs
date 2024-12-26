module AdventOfCode2024.Day5.Part2Test

open AdventOfCode2024.Day5.Parser
open AdventOfCode2024.Day5.Part1
open AdventOfCode2024.Day5.Part2
open FsUnit.Xunit
open Xunit

let exampleRules =
    [| (47, 53)
       (97, 13)
       (97, 61)
       (97, 47)
       (75, 29)
       (61, 13)
       (75, 53)
       (29, 13)
       (97, 29)
       (53, 29)
       (61, 53)
       (97, 53)
       (61, 29)
       (47, 13)
       (75, 47)
       (97, 75)
       (47, 61)
       (75, 61)
       (47, 29)
       (75, 13)
       (53, 13) |]

[<Fact>]
let ``Can correct example update #1`` () =
    let result = correctPageOrder exampleRules [| 75; 97; 47; 61; 53 |]

    result |> should equal [| 97; 75; 47; 61; 53 |]

[<Fact>]
let ``Can correct example update #2`` () =
    let result = correctPageOrder exampleRules [| 61; 13; 29 |]

    result |> should equal [| 61; 29; 13 |]

[<Fact>]
let ``Can correct example update #3`` () =
    let result = correctPageOrder exampleRules [| 97; 13; 75; 29; 47 |]

    result |> should equal [| 97; 75; 47; 29; 13 |]

[<Fact>]
let ``Exercise result`` () =
    let rules, updates = parseOrderingRulesAndUpdates rawInput

    let result =
        updates
        |> filterExcludingByRules rules
        |> Array.map (correctPageOrder rules)
        |> sumOfMedianPageNumbers

    result |> should equal 5466
