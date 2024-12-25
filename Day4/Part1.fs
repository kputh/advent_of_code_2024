module AdventOfCode2024.Day4.Part1

let pattern = [| 'X'; 'M'; 'A'; 'S' |]

let countMatchesInArray (pattern: char array) arr =
    arr
    |> Array.windowed (Array.length pattern)
    |> Array.filter (fun window -> pattern = window)
    |> Array.length

let countMatchesInReverseArray pattern arr =
    arr |> Array.rev |> (countMatchesInArray pattern)


let countPatternHorizontally pattern grid =
    let countPatternInRow pattern row =
        countMatchesInArray pattern row + countMatchesInReverseArray pattern row

    Array.sumBy (countPatternInRow pattern) grid

let countPatternVertically pattern grid =
    grid |> Array.transpose |> countPatternHorizontally pattern

let countPatternDiagonally pattern grid =
    let countPatternAlongMainDiagonal pattern grid =
        let splitAndFill value index arr =
            let front, back = Array.splitAt index arr

            let frontFill = Array.create index value
            let backFill = Array.create (Array.length arr - index) value

            Array.concat [ frontFill; front; back; backFill ]

        grid |> Array.mapi (splitAndFill '.') |> countPatternVertically pattern

    let mainDiagonalCount = grid |> countPatternAlongMainDiagonal pattern

    let counterdiagonalCount = Array.rev grid |> countPatternAlongMainDiagonal pattern

    mainDiagonalCount + counterdiagonalCount


let countPatternInAllDirections pattern grid =
    countPatternHorizontally pattern grid
    + countPatternVertically pattern grid
    + countPatternDiagonally pattern grid

let rawInput =
    """
ASAMXSMXMAXSAMMAMXAXXAXXMSAMXASAMXSASMSMXAXMAMMSSSMAMSMMSASMMSSMSMSMSXMMSXMXMAXXMAASMMMXMXMMXMAMXSSSMSMSMSMMMMAMMSMSXXAXMMXSAXMMXXAMASMXMASM
MMXXAMXAXSMXMMSSMMSSMSMMXSSMXMAASASAAAAMSMSMSSXAMXXAMXAAAAMXASXAAAAAMASASASMSSMMMSXSAMXAMAMSSMASAMAAAAAAXXAAXMAMXSAMAMSSMAMXMMSMMXSXAMAAASAS
SAMMAXMSMXMAXMAMAAMXAAASAMXSXASAMAMMMXMMAXMAAXMXMMXSSSMMSXXMXMASMSMSMXMASAMAAXAAAMMSMMXMXAXAASAMAMMMSMSMSSSMSSXSAMSMMMAAMXXXAAAAAMAMSSSXMXAX
AAMSAMMAMASXSMSMMMSMSMSMASAMMMMSMXMASXSSMSMMMSMSXAMXAASAMMMXSAMAAXAXMXMAMXMXXSXMMMAXASAAMSSSMMXSAMXAAXMAMXMAXAXMXAAXMMSSMMSMMSSSMAMMAAAMMMSM
MMMSAXSMXMXAMAXXAXXAAXXMXMASMAAXXAXMSAAXSAXAAAASMSMMAMMXSAXAXAXMMMMMMXMASXMSMMXSAMMSAMMXMXAMXMAAXXMSMSMSMMMSMMXSMAMMSAAAXAASXXAMASXMMSMAAAMA
XAAMAMXXSMMMMAMSSMMSMSXMXSXMMMXXMMSXMMMMSXSMSMXMAMMXAMMMMMMXMMMMMAXMAAMXSXAAAXAMAXMAMMMSSMSMAMASAMXAAAAAMXAAAXAXAASAXMSXMSSSMMMMAMAMXXXSMSSM
SMSSXSXAAMMAMXXAMAMXXXAMMSAMXMXSMMAXMASXMAMAAAAAXXXSMSXASXMSXSAMSMSMMMSAXMASMMXSXMASXAXAXAMSXSAXMSSMSMSMSMSSSMMMSXMASMAXAXMXMAAMSSSSMSAMAAMX
XAMXAMMSMMSASMMASMMXMSSMAXAMASAAAXMMSASAMAMSMSMSAMXSAAMSMAAAAXXXAXAAMXMASXXMMAMXMSXMSXSMMMMXXMMSAAXXAXXASAAAXMSMMASXMXAAMMXXMMXMAAMAAMAMMMSS
XSSMAMAXAASXXXSAMXMAXAAMSSSMAMXSMMSAMXSXSXXAAXAXMXAMMMSMSMMMSSSSMSSSMMMAMMAMSAMAMXAXSXSXMSAXXMAMMMMMMXMASXSMMXSAMAMSAMXSSSXMSSSMMSMMSMXMAAXA
AXAMSMSMMMSAXMMMMAMMMMSAMXMMXSAXXAMXSAXMXXSMMMMMXMSSMXAAAXXXMAAAAAMAMAMAMMAMMAMXSMMMMAXMASASXXAXXMXSSXMAMMMAMASMMSSMXSXMAMMXAAXMAXMXMXSXMMMS
SSSMXAXXXASMSMAXXAAAAAXAXAMXAMASMSMMXMSMXAMXMAMAAAMAMSMSMSSMMSSMMMMAMSSMSSSSSSMSMXSAMSMXMSAMXSMSSXAAAXMASXSAMASXMMAXASAMMMAMMSMMSMSXMAMXMAAA
MAAXMAMSMMSAAMAMXMMSMXSSMASAMMMMMMASMXAAMSMMMASMSMSAMXXAAMMXAXAAXXMAXXAAMAAXMASXAMSAMAAXXMMMXAAAXMSSSMXSSMSAMMSAMXAMXMAMXXMAAAAMAASXMASASMSM
MSMMMSMSAAMMMSMSASMMXXAXSASAMXMXMSAMAMMSMMASXXSXAAMAMXMMSMAMXSSMMSSXSSMMMMSMSXMMXXSAMXSMMSASXSMMSMXAAAXAXASMMASMMMSMXSSMSSSSSSMMMMMASMSASAXX
XMAMSAAXMMMAMAMXMXAAMMMMMMSAMXXAXMAXMSAAASAMXMMMMMMSXSAXAMASXAASAAXMAXXXXAMXSMXMMMMAMSXMASMSAAAXAMMMMSMMMXMAMAXAXXXAAAXAAAAMAMAXSSSMMXMAMXMA
XSAMXMSMXMSMSASXSMMMMAAAXMSAMXSSSMSMXMASXMAXSAAAMAAXAMXSXSAMMMMMMXSASMSMMMSASMAXAAMAMAXMXSAMXMMSMSMSAXAXSXMXMSSSMMAMMXMMMSMMAMXMAMXAXXMSMSMS
MMMMXSAXAAAASAMXSAMMXSMSMMSAMMXAMAAXXXXMASXSXMASMSSMXMAMXMAMAAAAXAXXAAAXAXMXMSAMXSSSSXMXMMXMASXAXMXMMSMMMXMAMXAAAAXXMXSXMXXMASXMXMSMMXSAAAAA
XAAXXMAMMSMXMSXASAMMXMAAAXSAMXMMMSAMXXXMXXXSXSAMXMAMSMSSMSAXSMSSMMSSMSMSSXMAXMMSXAAAAMAMSAMXAMXMAXAMXAMAXSSMSMSMMMSMMMAXMAMSASXSAMAXAXSMXMSM
SSSSSMSMXAXSAMMMSAMXAMMXSMSXMASXMXMSSMMSASMSXMASMSXMXAMAMMMMMAMAXMAXXAASMAMMSAASMMMMMAXAMASMSSSXSXMAMXSMSAAXSAXXXXXMASMSAAXMASASASMSSMMSAAAX
XAMAAAXASAXMAXXXSXMMMSAAXXMASASMMMMAAMXAASASAMXMASMMMMMXMASAMMSAMMAMSMSMMAMMMMMXAXXAXMSXSAMXAAAAAMXSXMMXAMXXSAMSMMXSXSXMAMSMAMXMASAAMAASXSSM
MAMSMMMAASXMMMMXMMXSSMMXSASXMAMMXXMSSMXMSMMMMXSAMXAMAMSMSASXSXMAMMMMAMAMSSSMMSASXMXMSXAAMMSMMMMMMSAXSXSXXXMMMMMXAAXMMMMMAXMMMSXXXMMMMMMSXMAM
SXMXSMMSMAXMAXSAMXAXAASMSAMXMMMSMSMMXMMMAMXMAMMAXMMMASAAMMSXMASXMXMXASMXAAAAAXMMSAASMXMXMASXSAASXMASMMSAMXXAAXXMXMMAAAASXMXAXMASMXAXXSAMASAM
AASAXMAMXAASMASASMMSSMMXMMMXXMAXAMMAXMAXAMAXMASAMXMSXSMSMAXASAMXMASXMMMMMSMMXMXAMSMXAAXAMXMAXMMMAMMMXASAMSSSSSMSMMASMMMSMMSMXMAAMMMAAMASAMAS
SSMMSMASMMXAXXSAMAAAMASAMASAMXMMMMMSSSSSSSSMSXSASAMSAMXAMXSXMASAMAXMAASAXXAASXMMMMXMSMSSSSMSMSMXAMAAMMSAMXAXAMAAAMMMXSMXAXXXXMSMMSSSMSAMXSXM
XMAMXMASXMAMMXMASMMMSASASAMAAAMMSAMAAXAXMAMASMMASMMMAMMAMAMMSXSAMMSMXMSSXSMMMAAXAXAAAXMMAMAAAAAXSSMSSXSMMMSMMMSSSMAXAAASMMSMSMAMXAAAXMASXMAS
MMAMAMAXAMSXSXXAXAMAMMSMMASMSMAAMAMMMMSMMAMMMAMMMMASAMSSMMSAMAMAMSAXXAXMMSAMSSMMMSMSMSMMAMSMSSMXMAXAMXMASAXAAXMAMMXMXMAMXAAAXSMMMMSMMSAXAXAS
SSMSXSXSSMAAAXMMSAMXSASASXMAAMXMSSMSAAAAMXSXSSMAASMXAMXMASMXSASAMMASMAMXASAMAAMXXXMAMAMMXMXXAMXASMMMSAMAMMSSMSMSMMMAASMSMSMSMSXSAXAXXMASMMAS
XAAAXXAAMAMMMMXXAXXMMASMMAXSXSXAAAASMMMSMSMXMXSSMMMSSMASXMAXMAMASMAMXMMMMMAMSSMMXSSMSMXSXSAMXXSASMAASMSMSAXAAMMMAAASMSAAAAMAAXAAMSASXSAAXMAS
SMMSSMSMMXSSXMMSMMXAMXMXSSMMAMMXMMMMSXAXMAXAXXMMMSMAAXASAMMMMSMMSMSMAMMXSXMXXXXMAMAMXXAAMXMAXAXXSXMXSAAAMMSMXMAMSMMMAMMMSMSMSMSMMMAMAMSSXMAS
XMAMAXMASAAAAMXAAAXSSSMXSXXMAMMMXMSAXMMMSASMSMXAAAMXSMXSAMXAAXAXXAXSASMASAMXMAMMASMMAMMSMAMSMSMXMASAMMMSMXXAXMMXAMAMXMXMAXAMXAMXMXAMXMMMMMAS
MMSSMMXMMMSSMMSMMMSMAMMMMAMSMMAXSAMXSASAXAXXAXMMMMMAMXAXAMSMMSMMMMMSXSMASAMAMAMMAMAMXMMAXAXAAAAXMAMASXAXMXMMMSXAXSMSASASXSMSMXMASXXSXXXAXMXS
MMMAXMAXMAMXMXAXAMMMMAAAMAMAMXMSMXXXMXASMMMXAMXSASAMXMMSMMXXMAXAASMMMSMMXAXSSMSMMSMMSXSASXMMMMMAAMSMMMMSMXAAAXMMXAASXSASAXXSAMXAMXSMMMSMXAMX
SAXXMSSSMMSASXXMMXXXXXXXXASASAMMMSXMXXMMSMAMMAXMASAXXSMSAAMXAMMSMSAAAXMASMMAAAXXAAMMSAMXXMASXMSSSMAXAXMAMSSMMSAMMMXSAMMMMSMXAMSAMSAXAAAMMSMM
SAMSAMAAAASXMAMMSMMSSSXSMMMAXXSAAXAMMMXMAMSASMMMMMXAAMAMMASXAXAMMXMMMXXMASMSMMMSSMSAMSMXMMAMAAAAAAXMMMSAXAMXAXAXMXAMAMXAAAAMAMSAMAMMSMMMAAAX
MAMXAMSMMMSASXMAAAXAAXMSASMSMAMMSSSMAAAMXXXMSAMXSAMXXMAMSAMXMMMSASXSSMSMMXMMAMAMMXMASAMAXMASMMMSMMSMSAXMSMMMSMXMMMXMMMSMSSSMXXMAMMXMXASMSSSM
MAMXAMXAXXSAMAMXSSMMSMASAMAAAXMXMAMXSSMSSSMXSXMSMAMMMSXXMMSAMXMMASAAMMSAXMSSSMAXXAMXSASAXXAMAMXMAASXMASXAAXSXAMMMMSMSASAMXXXXXMAMXAXMAMAXAAA
SASXAXSXMXMASXMAMMAMAAMMXMXMSAMXMAMAMAMAAAMASASXSAAAAAXSAAMMSAAMAMMMMAMAMXAAASXSSXSMSMMMSMAMXMAXMMSAMMAMSSMSMSMAAMAAMASXSXMAMSMMXSMSMSMXMSMM
SAMMSMMAAXSAMMSXXMAMMSMMAXASXXSASAMMSAMMSMMASMMAMSMMXSXMMMAAXMMMSSSMMSSMSMMMMMMAAMAAXXAASXSMSSXSAMSXMXMAXMMMAAMXMSMSMXMAMMMAAAASMSXSAAAAXXXS
SASAMASXSXMAMAAASMMXAAASMXXMAASXSXSASAMXXXMASAMAMMXSMMMXSMMMXSXXMAXMAXAAXMSMSAAMMAMSMSMMSAXAMAXAAMMSMXXXMSAMSMSMXXMAMMMAMXMMSSSMAMAMSMSMMSAX
SAMASAMAMMSSMMMSMAAMSSMAMSMMMMXMSAMXSASMSAMAXAXSMSASAAXAXAXMAXMMMAMMSMMMMXMASXXXXXXAAAMMMXMSMMMSSMAAXSASASMMAMAXMAMXMSSSMXMAAXAMAMMMAMAAXMAM
MAMMMMMSMAAMAASAMMMMAAXMAAAXAMXSMXMASXMAXXMAXSMXAMAXSSMSSMMMMXAAMMSAMAMMMMMAMMSMMMSMSMSXAMXXAMAAAMSSXSAMASXSMSAXMAMSMAAAASMMSSSSSMXMASXMAMMA
SMMAAXXAAMSXSMSASMSSSXMXSXSMSSXMASMASAMSMSMSAAMMSMSMMMAXMASAMSSMSMMASMMAMAMASXAAAAXAMAMXMSASMMMSAMXMAMXMXMAMAMAMMAMAMMSMMMAAAAXAAXSSMSAMASAM
XAXMMMMXXMXAXMMMMMAAMAMXMAAXXMASXMMSSXMAAAAAMMXAAAMAAMMMSAMAXAMMMMMXMXSAXAXMMXXSMXSAMXSAAMAAXXXMASMMXMMMMMAMAMMASMSMSAXAMSMMMSMMMMMAMSAMASAM
SSMSSSSSSXXMXAAXAMMSMMXAMXMXXMAMAXMASAXMSMSMXXMSMSSSMSAAMMSXMASAXAMMXASMSMSAAMXMXMMAXXMMXMXMMSSMASAMMAMXASMSXSMMMXAXMASXMASMXXAAMAMAMXMMASAM
AAAXAXAAMAMXMMMSSMMAMMMXMAXAMMMMMMMAXXMXAMXXXMAXAMAAASXSXMAXSXMMSMSAMXSXAASMMMASASMXMXMASAMXAAAMXSAMSSMMXSAMASXXASAMSXSAMXSMMMSASXMAXAXMAXXM
MMMMMMMMSASAXAMXXAXXSAMMSSSMAAAASMSMSSXMASMMMAAMAXXMXMAXAXAMXMMMSXAMAMXMMMXMAMXSAMXMMAMASXSMMXSMAMAMAMAMASAMXMAMXAMXMASMMASASAMXAAASMSSSSMSS
MSMSAXXXSASASXSMSSMAXASAMMAMSMSMSAAAAAASASMMXMASMMMXMXAMXSXMASAAXXMXSAAAXXASXXAMAMAAXAMAMAMXXAXMMSSMXSAMXSAMXXSMAAXAMAMXMASAXXXMMMMXAAMAAAAA
XAAXASXMMAMAMAAMAMASXMMXXXAMXAXAMMMMMSXMAXASAMAAAXSMMMSSXMASXSMXSAMAMSSSSSMMSMXSXSXMSXSXMSXMMMSAXXAMASASAMAXAAMSXMXMMSAMXAMMMSXXAAAMMMMSMMMS
MMMMAMAMMSMSMSSMXSAMXSMSMSMSMMMMMSMMXXAXAMSMSMSSSMSASAMAMSAMXXAAMAMMXAXXAAMAMMMMAAMASAMXXMASAXXXMSMMASMMASMMMXAAXAMSAMASMSSXAXMAXMSSMXMXXAMX
XAASASXMAXMXXXMAMMMSXMAAAAMAXAAAAAAMSSMMMMXAMXAMXASXMASAXMXSSMMSSMMSMMXXSMMAMAAMSMMASAMSASAMMMXAXXXMASASAMXXXMMXSMMAASAMAAXMSXMSAAMMMAMMMSSM
MSXSASAMXSMSSMMAMMASMMMMAMMAMXSMSSSMAXMAXAMAMMMSMXMMSAMXSSMMAXAMAMAAASMAMXSXSSSXMAMXXAMSAMASASXXMAXSAMXMAMXMXMMAMAASMMMMMMXMAMAMMMXAMXSAAXAX
XMMMAMXMAAXMASXSSMAMAAAXSMMSSMMMMMAMAXMXMMSAMMMXXAAXMXSMXMASAMXMAMXSAAXAMMSXAAXAXXMAXSMMAMAMASAAMSMMMSASMMSSSMMASXMMXXXXAMXMSAMXXSSSMMMMMXAM
MSAMXMAMSMMXAMXMAMXSSMMMXAMAAAMXXSAMSMSAAXSASXMAXMSMMMMXAMXMMSSMASAMMMMASASMMMMSMAMMMXASAMXSXMMMMAAAASXSAAAAAXSASMSMMMSSMSXAXSXMXAAAXMMAXMXM
ASAMSMXXAASMSSMSAMXXMASMXAMSSMMAMAMXXASMSMSAMXMSXMAAXMAXMXMAXAXXMMXSAXSAMXSAMAAAXAMXASAMXMAXAXXSXSMMXSXSMMMSMMMXSAAAAAXAAMMMMMMMMMSMMSMASMSM
AMAMMAMSSSMAMAASMMMMXMASMSMXMAMSSMMAMXMXMAXMXMAXAMSXMMSSMASXMXSAXAXXXXMXXASAMMSSSMXXAMAXAMXMMMMSAXXSAMMXXAMMMMMMMXSSMSSMMMAAAAAAXXAXAAMMSAAA
MMMMSAMXMXMAMXXXMASXASAMXXMMXSMMAXMXSASAMMMMXMAMAMMXMAXAMASAMASMMSSMSSSSMASXMAXMAMSMSSMXMSMMSAMMAMAMAMAMSAMAAAAAMXMAXMAMXSSSMSSMXSASMMSAMXMM
SASMSASXSAXXSXMASXSXMMXXXAXSAMASMMXAXAMMXXAXXMSSSMXAXASMMMMAMASXAMAASAASMXMAMXSMAMXAAXMASAMAXASMXMMSMMSXMASMSSSSXSXSMMMMMMAXAXAMAMXAAMMMSMMM
SMSAMASASMSMMMSAMXMASMSMMMAMASXMMAMSMSMSMXSSXSAAMASXMAAXAMSSMASMMSMMMMMMMMMXMASMSSMSMMSXXAMASXMXAXXXASMAMAMMAMXMASAMXXAAMAAAMSAMXSMXSMAXAAAX
XXMAMXMAMSAAAAMASASMMAAAASXSXMMAMAXXAAAAAAXAMAMXMXXMASMSSMAMMMXAMMXXAMXAAASAMXSAAAAMAAAMSXMASAASMXXSMMMAMASMMMXMAMAMMXSSMMXSXAXSAXXAMMSSMSMS
MSSMMSMMMXXXMSSMSASAMSMXMXAMXXAXSMSMSMSMSMXASXXAXMAXMAXXAMXSXASMMMMMMXSSSSSSSXMMMXXSMMSAAAMXMMMMXSMXMASASXSASAXMMSSMAMMMMSAMXSSXXXMASAXMAMAS
AAAMAMASMSSMMAMAMAMXMXMXSMSMSMMMXMAAXXXAAXSXMMSXSXXMSMMSXMMXMXXMASAASMAAMAMAMMMXSXMAXMAMSSMSXAXSAAAXMASASASAMMXAAAMMXSXAAMASAMAMMXMSMXMMSMAM
SMMMXSMMMAAAXAMMMMMMMAMAXAXAXAMSASMSMSSSMMXAAXSMMMSMAAXMASASMSMSAMSSMAMAMAMAMSMAMXAXXXMAXAAMXAMMXMMXMMMXMXMXMASMMMSAASXMMSSMAMMAXAXXXAMXXXXA
AMAMXAMAMSSMMXMAAAAAXAMMMSMMMSMSAMAXAAXAXXSSMMMAAAAXMSASAMMAAAMMSMXXMSXASASAMAMASASMMMXSSMMASXXMMSMXMXMXAMXXXXAXSAMMXXAXAXXSXMASXMSMSMMMMMMA
MSASXMSAMMAXAMSSSSSSSXSXAAAMXXXMAMAMMSMXXXAAMSSSMSSSSXMMMSSMMMSAMXAXMASXSAMMSXSAXXXAMXAAXAMAMAAXAAAAMASMASXSMMSXMASMSSSMMSXMXSMAAAAAAMAAAAAS
XSASXXSXMSAMXXAMAAAAMMSMMSSMMMMSAMXXAAAASMSAMXAXMXMXMAXMMAAXXXMMMMMMSAMXMMMAAAMMSMSSMMSMSXMASXMMSSSXSAXSAMXMAMMMSAMXAAMAXSAMASXAMSMSMSSXSSSM
XMASMXMAMMASXMASMMMXMAXAXAAAAXAXXXSXSMSMXAAMXMMMSAMASXMSMSSSSSMXAMXAXAXXAAMSMXMAXMAMAXAASMSXSAMAXMAMMXXMXSXXAMAAMASMMMMSMXAMASAMXAAMMAMAAMAM
XMAXMAMSMSXMASXMXXAAMMSMMMSSMASMAMSMXXMAMMMSSXSASASAMAAAAAAAAAAASMMMSMSMSMXMXXAMXMSSSMMSMAMMMAMSMMSMSMMMMMMASMMXMMMMXSXXMSXMAXXSSMXMMASMMMAM
AMASXMXAAAAXMAMSAMXMXXAXAMAAMAXXMMSAMSMMSAAXMAMASMMASMSMMMMMMMMMMAAAXXAAAXXMASMXMMMAMXXAMAMMSMMAMSAAAAAAAAAMXASMSMAAAMASAAAMMXAMXMASMMSAXMAS
XMASASXMSMSSSMXSXMASMSMSSMXXMXSAMAXMAMXAMMSSMXMAMASXMXXXXXXMAASXSSMMSSMSMSSMAXMAMAMAMMSMSXMXASXXSXMSMSMSSSSXSAMAASMMXSAMXMMXXAMXXMASAMMMXSAM
XMMSMMAAAXXAAMXMASMSAAXAXXAMSMSXMAXSSMMMSAXAMMMSMMMMSMMMSMMSSSSMAAAAMAAMAMMMSSXSSXSXSAAMAMXMASXXMAMMAXAAAMXMAMMXMXXAXMMMMXMAMSXMSMAXASAMXMSS
SMXSXSMMMMMSMMASXMAMMMSSMMXSAAXMASMMAAAAMASAMMAMAXXMAAAAXAAAMXMAXSMSSMMMAMAMXAAXAMXMMMMMSASMAMASXSMMAMMMXMMMMMSMXMMMSMAAXAMAMAAXXMAMXXAXXAAS
SXAMMXAASXAXXSAXAMXMAMAXAMXXMXMMSMASMMMSSXSAXMASMMSXMMMXSMMSSSSSMMMAXMAMASASMMMMAMMXMASXMAMXAMXMXAMMSSXMXMASAAAAAAAMXXSMSMSXSSSMMMMMSSSMMXMS
MMMMAMSMMMXSAMXSMMMXXMASXMMMSMAXMXAMXXMAMAMXMMAXAAMMSSSXXASMXAAMAXSMXXMXMSAXAXXXMSMXSASAMAMSXSXAXASAAMAAXXAMMSMSMSSMAMMASMSXAAAMSAAAXAMXAAAX
SXMASMXASAAMAMMMAAXMSXMAXAAAAXMMSMSXSXMASMSXSMSSMMXAAAMAMSMMMMMMMMMXSASAMMMMSSMAMAAXMXXASXMMAXMAMXMMSXSMMMMSXMAXXMAMXSMAMASMMMMMSMSSMAMXSMMM
AXSAXASAXMXMSMASMXMAXAXAXXMSSXSAXAAASMMMSXMMMAXMASMMMMMSMXAMXMXSAMSASAXMMAMXMAXSMMSMMAXMMMAMAMMSSXSSXXMAXAAMASAMASAMXMMXMAMAXXXMXAAXXXSAXAAA
MMMAMXMXMMXXAMASXAASXMMMSMMXMASXMMMXMAAXMAXAMAMMAMXXAMXAASAMAMMSASMAMMMSSMSSSMMXASMMXMASAMAMMSAAAAXASXMSMSMSAMXSAMAAMSSMMXSMMMMMMMMSMXMASMMS
XXMAMASXSAASXMASMXXMAMAAAMMSMMMXSXMXSSMSASXSSSMSASMMSXSMAMMSAXXMXMMXMAAAAAAAAXASMMAXMXAXASXXAMMSMSMMMXAAAAAMXMAMAMAMXAAXSAMXXAAXXSMXMAMXMAMM
SMSMSMSAMXMSAMXSAMMSMXXMXSAAAAXMASXAXAXMXXXXAAXAASAAMAMXMAMSASAMSXXSSMSSMMMSMMMSXMAMMMMMXAXMASAXXMAAAMSMMMSMSMXSSMMXMSSMMASMMSXSAMXMSXMASAMS
AXAMXAMAMXAMAMXMMSAAMAMSMMMSSMMAMXMXSSMSAMMMSMSMMMMAMAMSMMXSAAMAAASAMXAMAMXAMXASAMAAMAMXMAAMXMASMSSMMXMAXAXAXXMAMAMXMAXASAMXAAXMAMAAAXMXXXSA
MXMSMMMAMXMXSMSAAMXMMASAAMAXAMXMXAAXMAAMXXAAAXAXXAXAMAMAAMASAMMXXXAXXMXMMMMXAMXMAMASXXSXMXMASMMSAAXXMASMMSMSMSAXMSMMMASMMASMSSMXAMXMASMAXXXM
XAXAMXXAXAXXXAMMMSSMXXSXSSXSAMSSSMSXSMMMXXXSAAMMSASMSSSMSMAXAXMSSSMXMSSMAASMSMSSMMMMAXSXSAXXAAAMAMSAMXMXAMXXASMSMXAAXXSAMXMXAAXSSSMXAAMMSMAA
SMSASMSMSASMMXMAMSXSAAMAMAAMAMAMAAXMASMSSXMAMAXAXAAAAMMMXMMXSMSAAAXAMAMSMMXAAAAASMMMMMXASMSMSMMMXAMXMAMXSXMMXMASAMSMSMXMMSSMMMMXMAMMSXXAAMMM
AASAMAAXXXMMAXSSXMASMSMSMSMSAMMSMMMAMAMAMMMSSSSMSAMMMSMMMASAAXMMXMXXMAXSMSMSMMMSMSAAMMMMMAXXAMASXXXAMASXMAXSXMSMMMXAMXXAAAAMASXMSAMXMMMSSMSX
MAMAMMMSMAMXMMXMAMAMXAAXMAXMAMXXXAAAMAMXMAAMAAAAXAXMAMAASAMSSMSXSMSMSMSMAXAXSSXMASXMAAMAMXMMASASAMSMSASASMMMAMXAXAMAMASMMSAMXSAAMMMAXMAMAASX
XASAMXAXXXMAMXASXMSSXMSMMMMXSMMSSMSXSMSMXMXSMSMMSMMMASMMMAMXXAXAXAAAAMAMXMXMASAMAMMMSASASAMMAMXSAMXMAXSMMMAMMMSMMMSAMXSXAXAMXSMMMMSASMSXMXMA
SASXSMXXMXSMSMMMMMAAXSAMXXAAMAMXAAMAXMAXAAMSAMXXAXAMAMAASXMMMMMMMSMSMSSSMAAMXMXMXMAAAXSASASAMXASAMASXMMXMXAMAAXXAXXAMMXMMSMMMMMMAMAAMAMASMMM
XAMXMXSAMAMAMAAAAMMSMSASMMMMSAMSMMMAMSASMSMSASMSMMMSSSSMMXMAMAAXMMMMAAAMAXXXSMMMMMMSXMMAMAMXXMASAMXMASAAMMSMMMSMSSSXMMXSAAAASAAXAMMSMXMASAAX
MSMSAASAMMSASXSSXSXMAXAMXAXASAMMASXXAMASXMAMAMAXSAMXAAAMXXSASMMSASXMMMSMSMSXAAMXAMXMAAMXMAMXMMAMMMASXMSXSAMAMMAMMMMAAXAMSSSMSSMSASXAMAMASMMS
XAAMMXMXMXMAXAAMMSMMMMSMMSMMSAMSAMMMAMXMASXMMMSMSSSMMMMSSXMASAASAMASXAXAAAASMMMSMSAXMMMSSXSAMMSSMSXSAMAXMAMAMSMSAAMXMMXXAMAAXAXMXMXSMMMAMAXM
SMXMXMSMMXMSMMMSAXXAAMAAAXAXSXMMXSASXMMSAMASXAAAMXMAMAXAXAMSMMMMAMXMMMSSMSMXMAMAMMMSMAAXMASASAMAAMMSXMASMXXAMAMSMXSASAXXMSXMMSMSXMAXAMMMSMMM
AMASAMAMXMAMXAAMASMSSSSSMMAMMMMMMMXSAAAMASAMMXMXMAXAMXMMSXMASXXXAMXSXXAMXXAASMMASAMSMMSSMAMMMMSSMSXSMSAMAASXMMMXAASAMSMMXMXMAAAAAMASMMAMAAAM
MMSSXSASXMSMSXSMAMMMAAAAXMMAMXAAAMASMMMSAMASAXSASXSMSMAXMASASMSSSMXSMMSSSMSXMXXXMAXXXXMMMSSXAXMASAMMASMMSMSAAXMMSMMXMAMMAMMMSSSSSMXMMSXSXSSS
XSAMXMXSXAAASXMMMSMSSMSMMXSAMSSXSMASASAMXSAMXAXMMXSAAMAMSAMXXAAMAMAXXAAMAAMXMSMSMSSMMMSAAAXMASMXMMAMAMAXMAXMMMMMMAXMSSMSAXSAAAXAXXAAMMMMMMMM
MAAMSMMMMSMXMAXXAAAXAAAXAXXAMAMAXMMSAMXSAMXSSMSXSAMSMMSAXXMMXSXSAMSSXSXSMMMAXAAAAAMAAASMSXXXMXXSMXSMMSMMMSMMXAASMXMASAASASMMSSMMMSSSMAAAAAAM
SSXMAAXMAXXASMMMSMSSMMMXSMSSMAXMMSXMASAMXSMMAXAXMAMMXMXMSMMSAAMSASAMXMMXXMXXXMSMSMSMMMSXMMSSMSAMAAMAXAAMAAAXMSXMASAMXMMMXMAXAMMXAMMAXSSSSSSS
AASXSSMMMSSMMAAAXXMAMXXXMMAMSMAAMXAMAMASMXASMMXMSSMMXMAXXAAXMMMMMMXMASASMMMSAAXXMASMMXMAMAAASMAMXSSMSSSMSSSMXMMSSMSAMXMXMMXMSSMSMMSAMMAMAAXX
MMMXAMASMMAMSSMSMXSAMXXXAMAXAXMMSXSMASAXMAXAMSSMMMASXSMSSMMSMSMSXSASASMMAAAXXMAXSASAXAMAMMMSAMAMXMAXAAMMXAMMMAAMAAXXAASAXXXAXAXXAAMXSMMMXMAM
MSMMXXAMMXAMMMAAAASAMMXSMSXSAXSASAXSMMMSXXSXMASAASMMASAAXMAAXAAAASAMXSMSMMXSAMXMMAMXXMMSXXXMMMMXXMAMMXMXMXMAXMMSMMMSSXSASMMMSMMMMMSASAMMAMXM
MAAMSMSSXSASAMXMMMSAMSMMAAXMASMAMMMXSAXMAMMXMASMMSAMXMMMMMSSSMSMMMXXMMXAAAASMMMMMMMSXMAXMAXXXASXSAXSMXAXSXSASXMSAMAMAAMMMAAAAAAAAMMMSAMSMMAM
SMSMAAAAASXMASAXXAMAXMAMMMMXXAMXMSMAMMXAMXAMMXMAXSMMSMAMSMAMAXXXXAXSXMSSSMXMMAXMAMAXAMASAMMXSXSASAMAXAMXMXMAMAAXSMASMAMASMMMSMMXMXAAXMMAMSAS
MAAXMMMSMMXSMSASMSMMMSSMSSMSXSAMXAMASMSMSXSAAMMSMMMAXSASAMASAMAMMSMMAMAAXMMMSMXSASMSSMASMMMXXXMMMMSXSXMAMMXMSMMMMMMXXMAMMAMAMAAASMXMXAXAXSAM
MSMSXAXXAMXSAMXXXXASXXXAMAASXMAXSASASAAAXAAXAAAAAXMSMMXSASXMMMSXAAASAMMSMAAXAMMSAMMAAMASAASXXMAXAXSXMASMSMAMAXSXMASXMASMXSMAMMMMSAAMSSMSMMAM
XAXMXMSSMMMMMMMMMSXMAMMXMMMMASAMMASMMXMMMMMMSMSSSMMXAMASMMMXSAXMXMMAMMAMXSMSASAMSSMMMMXSXMXXMASXMSMMSXAAAMSSXSAXSAMAMSMSAMMMSMMXXMMMAAXXASAM
XMSMAMAMXAAAAAAAMMMSAMAXSAMXMMMMSAMXAAMSSXSXXAAMAAASAMXSAXXAMASMMSXMSSSMXMASAMXSASASMMXMMSSMSAMXMAMAXMMSMMMAMMMMMASXMAAMXXAAAASMMMSMSXMMAMXM
MSAMAXASXSMSSSSXXAASXXXSAMXSAMXAMASMSMSAMXXMSMMXSMMMAMASXMMXMAMAXMAXAAAXMMMMAMXMASXMAMXMAMAXMXMAXAMMMAXAMAMAMMXMSAMASMSMSSMMSXMAMAAMXASMSSMM
XMAXMSMSAMXXAMXMMSXMXAMXAMAMAMMMXAMXMMMMSMSMXAAXXAMSSMMMAXXAMXSMMMMMMSMMXAAXAMXAMXMSAMAMSSMMMMSXSASXSSSMXAXASXAAAXSXMAAAAXMXXXSXMSSSXMMAAAAM
MXSXAAAXMXMSAMAMMXAMSSSMMMXXAMXSMSSMXAAXAAAMMMMXSAMAMASXMMSMSAMXSAAAXAXMSSSSXSXXMAXMAXAXMAXMAAAMXAAMXAAASXSMSMSSXXMAMXMMMSMXMASXAMAMAXMMMSSM
SAMMSMSMMAMXAMXMAMAMAMAAMSXSXSAAAAAASXSSSSXMAASXSMMXSAMASXMAMXSAMMXMMMXMAAAAASMASXSSMXSMSAMXMSSMMMAAMSMMMAAAXAAMMMSSMSXSMAMXMAMMAMASMMSAAMAA
MASAAAAMMASXSMSMXSAMASXMMSAAASAMXMMMAAMAAAASXMMMSAMAMMSMMAMSMAMMSSMMMXSAMMMMXMAXAAAMAAXXMXSXAAAXMAAMMAMAXSMMMSMMAAAXAAAMMAMXMASAXMASXAMMSSXM
MAMXMSMXXASAXAAMMMASASAXAMAMMMXXMMXMMMMSMMMMSAAASAMXMAAAMAMXMAXMAAAAAAAMAXXXAMSMMSMMMMSAMASMMSXMMSMXMASMMMXXAAMXSSMMSMAMSMSMSASMXMAMMMSMAMMM
SSSSXMXMASMXMSMSASAMASMMAMXMASMAMXAMXXAAXXXAXSMMSXMASXSSSSXXMAXMXMMMMMSXAXXSXSAAXAAAXMMAMAXAXXMMMAMASAMMAXSMSXSAAAXAMMXMAMAAMAXMXMASXMAMAMAX
SAAAAMSAMXAXMXXSXMMSMMMSMSMMASASMSSSMMMSSXMAMMSMXMXMAXAXAMXSMMSMSASXAXAMXMXAXSXXMSSMSSSMMSSSMMAXSASAMASXSMMXMAMMSMMXMSASASMSMASXMSASMSSSSSSS
MMMMMMXMAXMXAAMMMMAAXAAAASASAMXAAXAAXASAMXMMMMASXASMSMSMSMAMAMAASAMXXAMXAAXMMMXXMAMXAAXSAMXMASMMMXMAMAMXAAXAXMMAAXXAXXASXSAXMASMAMXSAMAAAAAA
MMXMASXSSMSMMMSAAMSMSMMSASAMASMMXMSMMSMASMMSASAMMMXAMAMAMMXSAMMXMAMASXXSXSMXAASMMAXMXMSMMMAXXMXAXMSSMAXASMMMSXXSASMMSMMMXMXMMAMMAMMSMMMMMMMM
MASAMSAAASMAAAMMSXXASXAXMMXMMAMXSAAMMMXAMAAMMMAXAXMXMAMAMAMSASXXSAMAMSAMXMASMMMASAMSAMAAMSSSMMXSAAAMSMMMXSASXMAXMMMAAAASXSXAAAXSASAMXXSASAXX
SASMXMMMMASXMSSMMAMAMXXXAMXSMAMAMMXSASMMMMSXMXSMSSSMSXMAMAMSAMXAXXMASXMASMXMSASMMMXSASMMMXMAMAAAMMMMAMXMASXMXMAMAAMMSSMSAMMAMXMXAMXSAASASMSA
MASXXMXAXMMXAAAMXAMMMMSSXMASMXMASMMMMSAXSAMXSAAMAMAAXMSMSMXMMMMMAMMMSMMASXAXSMMAAXAXAMXSAXSAMMMSXMMSMSXMAMASXXXXMMSAAAAMMMMASMSMSMSMXXMAMAXA
MMMMMMMMMXSMMSSMXXXAXAAAAMAXMAMXMAAAASXMMASAMSMMASMMMMAMAMAAAAAAAAAAMAMASMMMMXSSMMMSMSMAAMMMSMAMASXAXAAMXSAMMSMSMAMMSMMMAASXMASAMXXMMMMMMMMM
SMAXAAAXXAXAMAXASXSMSSMMXMASXXSAMSXMMMXXMAMAMAMSMMXAAMASXSSXSMSXMMMMSXMAMXMAXAAMAXXAAAMMSMXAAMMSAMXMMSXMXMMAMXAAMXMAMXXMSASAMSMMMSSMAMASAMAS
MXMXSXSXMXSXMASAMAAAAXXXAXAXAAXMMAXSAMMMMASMMMMAAMSXXXASAXMAMAMXSASXMAMMMSXMSSSSSMSMSMSXAXMXSSXMAMASAMMSMXSMSASMSXMXSMSXXAMMMAAMAAAXMSASASAS
SASAMMXASASAAXMAMSMMMSSSXSMMMMMSMSMXAAAXMASAAMSMSAMASMMMMMMXSAMASASMSAMXASAMXAAAMAXAXXAXXMMSMXXSXMMMASAAAAMASAMASMSMAAMAMXMASXSMMXSMAMASAMAS
SXMASASAMASAMXSXMMAXSAAXMXMASAXAAXASXSSXMMMXMMXMMAMAMAAAAAMAMAMXMAMAXSXMASAMMMMMMSMMMSMSSMSAAXMASXXMAMXMSXMXSAMXMAAXMMMAMASXSXMASXMAMMMMAMSM
XMASMAMAMXMAXASAMSSMMMSMSASXSMSSSMAMAAAMSSSSMXXXSXMAMMSMSXSMSMMMMMMSMMXSASAMAAXAAXAMASXAAXAMSMXXAMXMASXMAXMASMMXMSMSMMSASXXSXASMXASASXSMMMAM
SAMXMMMMMMSSMASAMAAXSXXXSXXAXMAMAMMMMMMMXAAAXXSAMXSSSMMXMXSXMASXMAAAAXMAASAMSXSMMXSMASMSSMXSAAMXMXMMAMXMAAMAMAAXXAXAAASAXXMAXAXMSMSAMXSAASXS
AXSXMAAAAXAXMASMMSAMXAXAMMMSMMSMSMSASMMSMMMMAMMSMXXMAXXASAMAXAMASMMSSMXMASXXMAMASAXMXSAAMAXMMSMSMASMMXXMSXMSSSMXSSSMSMMMAMXMMMAAXXMAMASXMMAM
MMMASMSSSMSSMASXXXXMAXMAMAAAAAAAMASASXASAMAMASAMXXASAMSMMASAMXSMMSXAXAMXMSMSXMSAMXSSSMXMXSAMXMAMMAMAAASXXXXMXAXMAAAAMXXAAMAXAXMSMMSMMASASMAM
XASXMXAMXAMXMSMMMMAMAMSXMMSSMMMSMAMMMMMSASXSMXAMMMMMAMAMMMMMSXAXAMMMMSXXXMAMAMMMMAXMAMAXMMMASMMMSMSXMMSAMXSMSMMSMSMMSSSSXSXSSXAAMXASMXSAMSSS
SASXXMSMMSMXMAAAXMAMAMMASMXMMSAAMAMXXAXXASAXXSMMASXSXSMSXMAAXMAMXMAAAXMMXMXMAMAAMMMXAMSMAAXMMAMAAXMASXMAMAMMMXAMXMAMXAAAAXMAXMSMMSMSXAMXMAMM
SAMXAAAAAXMASXSMSSXSXMSAMXSAASMMMAMMMSMMMMMMXAXSAMAMXSXMASXSMSMSASMSMSAMAMASMSXMAASMMMAASMSXSAMMMXXASASXMASASMMSAMSSMMMMXMASXMAMMMMXMASAMMSM
XAMMMSSMMMMAMMAAXAASAMMSSMMMMXAXSXSAAAXAAAAXXMXMASMMAMAMMMXAAAAMAAAMXSXSASXSXMAXMMMASXMMAMXMSMSMXAMXSAMXSASASAAMAMAAASXXAASMMMAXAAMAMXSXSAMX
SAMXAMAMSSMMSXMAMMMMAMAMMXAXMSSMMSSMSSSSSSSSMXMXAAAMASAMXXSMMMSMSMAMXMASASMXAXAASXSMMAMMAXMMSXAAMSMMMXMAMMSMMMMSMMSSMMSSXMXAMSXSSSMAXASAMXSX
MMXMASXMAAAAAAMAMXASMMXSASMSMAXAXMXXXMAMAXAXMASMMSXXAXAXAAAAAXXAXAMXAMAMMMMSMMSXMASASAMSMMMASMMXMAAXMAMXSASAXXAAAMAMXMXMSXMAXAXMAMXXMASXSAAM
XXXSXMXMSSMMSMSASXXXXXSMMMMMMASXMMMMXMAMXMMMSXSXXMMMSSSMAXXSMSMMMASMMSXSXAAAAAMAMAMMMAMASMMASXSASMSMSAMAMAMMMMSSSMMXAAAXAXSXMSMSAMXSSMMMMAXA
MSMMAAAXMXAAAASMMMSMXMASAAASAASAASAAMSASMASAMAMXAAMMMAMMMSAAXSAAMXMAMMAMMMXXMMSAMASXSXMMMMMMXASXSAXXSASXMSMSAXAXAAMSSSSSMXSXXXAMXMXXMASASMSS
AXASMMMMMSMMMXMMSAXMASMSSSSSMXSMSSMXXAAMXAMASAMSSXMAMSMAAMMMMSMMSSSMMSAMASAXSXMXXAMXSAMXASXAMMMMMAMAMMXAAAASMSMSMMMAAMAAXASMSMAMMMSASAMXAAAX
MSXMXMXAXAAAXMAMASXMAMXMAXAXAXMMAXXSAMMMMMSASMXAMASXXAMMMSASMXASAAAAASAXAXMXMAXASXXMXMXMMMXAXMAMMAMAMASMMMXMAAXAXXMMMMSMMMSAAMAMXXAAMMSAMMMM
MXMAASASMSSMXXMMAMMSSMAMXMMSXMAMXSMMAXAMAAMMXXMAMMMSSMSXASXSAMXMASMMMSAMSSSMSMMMXAASXMSSSMSSSSMMSXSXSMSSMSSMSMSSSMASXAXSMASXMSSSSMMSMXXMXMMM
AXASXMAXXXXMASMMMSAAASMMMAMAXMXMAXXSAMMSMSSMXSSSMSAAMXMMMMXXMSMSXMASAMAXAAAAAXAXMSMMAMAXAAMAAMMAMASAMXSAMAAMAMXAAXAMMSMAXAXSXXAAXXXAASMMASAM
MAXXSMMMMSMMASXAAMMSXMSAAXMAXMAMSSMMASXAXXAMAAAAMMMMSAMASXMSAXAAAAXMXSMMMSMMMSASMMMSAMASMMMMMMMAMXMSAMXMMSMMASMSMMMSAAMMMMMMXXMASMAASMASASAS
XMSAMXAAMAXMXSXMMSAMXASXSXMAXSAMXAASAMXAMSAMXMSMMAAMSASASAAXMMSMMAMSAAAXMAMSAMXAAAASASASAAXXXXSASAAXMSAXMAASASAMAXMMXSSXXMASAXXAMXAMXXXMXSMM
AXMXMSSSSMSMXMASAMXSMMMXAXMAMSASXSMMMSAXMSXXAXMASXSXSMMMSMSMXAMXSXAMSSXMSXSSXXSSMMMSAMXSXMSAAXSASMSMXSASMSXMASMSSMXMAMMAMXASASXMASAXSAMXXMAS
"""
