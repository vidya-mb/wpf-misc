$nestingLevelsList = @("10", "20", "40", "80", "160")
# $nestingLevelsList = @("10")
# $recordsList = @("10", "100", "1000", "10000")
$recordsList = @("10000")
$iterations = 3
$testCaseList = @(
    "AddTestCase0", "AddTestCase1", "AddTestCase2", 
    "AddTestCase3", "AddTestCase0r", "AddTestCase3r",
    "RemoveTestCase0", "RemoveTestCase1", "RemoveTestCase2",
    "RemoveTestCase0r", "RemoveTestCase1r", "ReplaceTestCase0",
    "ReplaceTestCase0r", "MoveTestCase0", "MoveTestCase1"
)

foreach ( $testCase in $testCaseList)
{
    foreach ( $nestingLevel in $nestingLevelsList )
    {
        foreach ( $records in $recordsList )
        {
            Write-Output "Running Test : $testCase, $records, $nesting, $iterations"
            .\ICGPerfAutomated.exe -type $testCase -records $records -iterations $iterations -nesting $nestingLevel
        }
    }
}