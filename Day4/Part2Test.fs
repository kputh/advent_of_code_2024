module AdventOfCode2024.Day4.Part2Test

open AdventOfCode2024.Day4.Input
open AdventOfCode2024.Day4.Parser
open AdventOfCode2024.Day4.Part2
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Can find pattern in array`` () =
    let result =
        [| 'M'; 'A'; 'S'; 'A'; 'M' |]
        |> findMatchesInArray [| 'M'; 'A'; 'S' |]
        |> Array.sort

    result |> should equal [| 1; 3 |]

[<Fact>]
let ``Can find pattern in columns`` () =
    let result =
        [| [| '.'; '.'; 'S'; '.' |]
           [| '.'; '.'; 'A'; '.' |]
           [| '.'; '.'; 'M'; '.' |]
           [| '.'; '.'; '.'; '.' |] |]
        |> findMatchesInColumns [| 'M'; 'A'; 'S' |]

    result |> should equal [| (1, 2) |]

[<Fact>]
let ``Can find pattern in main diagonal (4x4)`` () =
    let result =
        [| [| '.'; '.'; '.'; '.' |]
           [| '.'; '.'; 'M'; '.' |]
           [| '.'; 'A'; '.'; '.' |]
           [| 'S'; '.'; '.'; '.' |] |]
        |> findMatchesInMainDiagonal [| 'M'; 'A'; 'S' |]

    result |> should equal [| (2, 1) |]

[<Fact>]
let ``Can find pattern in main diagonal (10x10)`` () =
    let result =
        [| [| '.'; 'M'; '.'; 'S'; '.'; '.'; '.'; '.'; '.'; '.' |] // #0
           [| '.'; '.'; 'A'; '.'; '.'; 'M'; 'S'; 'M'; 'S'; '.' |] // #1
           [| '.'; 'M'; '.'; 'S'; '.'; 'M'; 'A'; 'A'; '.'; '.' |] // #2
           [| '.'; '.'; 'A'; '.'; 'A'; 'S'; 'M'; 'S'; 'M'; '.' |] // #3
           [| '.'; 'M'; '.'; 'S'; '.'; 'M'; '.'; '.'; '.'; '.' |] // #4
           [| '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.' |] // #5
           [| 'S'; '.'; 'S'; '.'; 'S'; '.'; 'S'; '.'; 'S'; '.' |] // #6
           [| '.'; 'A'; '.'; 'A'; '.'; 'A'; '.'; 'A'; '.'; '.' |] // #7
           [| 'M'; '.'; 'M'; '.'; 'M'; '.'; 'M'; '.'; 'M'; '.' |] // #8
           [| '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.' |] |]
        |> findMatchesInMainDiagonal [| 'M'; 'A'; 'S' |]
        |> Array.sortBy fst

    result
    |> should equal [| (1, 2); (2, 6); (2, 7); (3, 2); (3, 4); (7, 1); (7, 3); (7, 5); (7, 7) |]

[<Fact>]
let ``Can count pattern in example`` () =
    let result =
        [| [| '.'; 'M'; '.'; 'S'; '.'; '.'; '.'; '.'; '.'; '.' |]
           [| '.'; '.'; 'A'; '.'; '.'; 'M'; 'S'; 'M'; 'S'; '.' |]
           [| '.'; 'M'; '.'; 'S'; '.'; 'M'; 'A'; 'A'; '.'; '.' |]
           [| '.'; '.'; 'A'; '.'; 'A'; 'S'; 'M'; 'S'; 'M'; '.' |]
           [| '.'; 'M'; '.'; 'S'; '.'; 'M'; '.'; '.'; '.'; '.' |]
           [| '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.' |]
           [| 'S'; '.'; 'S'; '.'; 'S'; '.'; 'S'; '.'; 'S'; '.' |]
           [| '.'; 'A'; '.'; 'A'; '.'; 'A'; '.'; 'A'; '.'; '.' |]
           [| 'M'; '.'; 'M'; '.'; 'M'; '.'; 'M'; '.'; 'M'; '.' |]
           [| '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.' |] |]
        |> findXMatches [| 'M'; 'A'; 'S' |]
        |> Set.count

    result |> should equal 9

[<Fact>]
let ``Exercise result`` () =
    let result =
        rawInput |> parseCharGrid |> findXMatches [| 'M'; 'A'; 'S' |] |> Set.count

    result |> should equal 2048
