module AdventOfCode2024.Day5.Part1Test

open AdventOfCode2024.Day5.Parser
open AdventOfCode2024.Day5.Part1
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
let ``Should tell example update #1 is correctly ordered`` () =
    let result = isCorrectlyOrdered exampleRules [| 75; 47; 61; 53; 29 |]

    result |> should equal true

[<Fact>]
let ``Should tell example update #2 is correctly ordered`` () =
    let result = isCorrectlyOrdered exampleRules [| 97; 61; 53; 29; 13 |]

    result |> should equal true

[<Fact>]
let ``Should tell example update #3 is correctly ordered`` () =
    let result = isCorrectlyOrdered exampleRules [| 75; 29; 13 |]

    result |> should equal true

[<Fact>]
let ``Should tell example update #4 is incorrectly ordered`` () =
    let result = isCorrectlyOrdered exampleRules [| 75; 97; 47; 61; 53 |]

    result |> should equal false

[<Fact>]
let ``Should tell example update #5 is incorrectly ordered`` () =
    let result = isCorrectlyOrdered exampleRules [| 61; 13; 29 |]

    result |> should equal false

[<Fact>]
let ``Should tell example update #6 is incorrectly ordered`` () =
    let result = isCorrectlyOrdered exampleRules [| 97; 13; 75; 29; 47 |]

    result |> should equal false

[<Fact>]
let ``Can find middle page number`` () =
    let result = getMedian [| 75; 47; 61; 53; 29 |]

    result |> should equal 61

[<Fact>]
let ``Exercise result`` () =
    let result =
        rawInput
        |> parseOrderingRulesAndUpdates
        |> filterByRules
        |> sumOfMedianPageNumbers

    result |> should equal 4281
