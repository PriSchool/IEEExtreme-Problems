def parser():
    dataInput = list(input().split(' '))

    integer_map = map(int, dataInput)

    dataInputInts = list(integer_map)
    return dataInputInts


def switchBrushPaints(i_Scenario_Sequence, i_Current_Index):
    global minimal_Amount
    global first_Brush
    global second_Brush

    movingIndex = i_Current_Index
    foundAnyBrush = False

    for movingIndex in range(movingIndex, len(i_Scenario_Sequence)):
        if i_Scenario_Sequence[movingIndex] == first_Brush:
            second_Brush = i_Scenario_Sequence[i_Current_Index]
            minimal_Amount += 1
            foundAnyBrush = True
            break
        if i_Scenario_Sequence[movingIndex] == second_Brush:
            first_Brush = i_Scenario_Sequence[i_Current_Index]
            minimal_Amount += 1
            foundAnyBrush = True
            break
    if not foundAnyBrush:
        first_Brush = i_Scenario_Sequence[i_Current_Index]
        minimal_Amount += 1


def computeMinimalAmount(i_Scenario):
    global minimal_Amount
    global first_Brush
    global second_Brush

    minimal_Amount = 1
    first_Brush = i_Scenario[0]
    second_Brush = 0
    i = 0
    scenario_Size = len(i_Scenario)

    for i in range(1, scenario_Size):
        if(i_Scenario[i] != first_Brush):
            second_Brush = i_Scenario[i]
            minimal_Amount += 1
            break
        i += 1

    i += 1
    for i in range(i, scenario_Size):
        if i_Scenario[i] == first_Brush or i_Scenario[i] == second_Brush:
            continue
        else:
            switchBrushPaints(i_Scenario, i)

    return minimal_Amount


amount_Of_Scenarios = int(input())

listOfColorSequences = []
for x in range(0, amount_Of_Scenarios):
    input()
    colorSequence = []
    colorSequence = parser()
    listOfColorSequences.append(colorSequence)

for scenario in listOfColorSequences:
    answer = computeMinimalAmount(scenario)
    print(answer)