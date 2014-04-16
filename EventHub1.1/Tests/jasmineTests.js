describe("Location Tests", function () {
    var listOfLocaiton;

    // Setup
    beforeEach(function () {
        var listOfActivities = [
            {
                Name: "Test Activity"
            }
        ];

        listOfLocaiton = [
            {
                LocationId: 1,
                Name: "Test Location",
                Address: "Test Address",
                Active: true,
                listOfAct: listOfActivities
            }
        ];
    });

    // TearDown
    afterEach(function () {
        presentValue = 0;
        previousValue = 0;
        aPercentChanges = [];
    });

    it("calcWeeklyPercentChange() should return the change between two numbers as a percentage.", function () {
        var calcWeeklyPercentChange = function (presentValue, previousValue, aPercentChanges) {
            var percentChange = presentValue / previousValue - 1;
            aPercentChanges.push(percentChange);
            return percentChange;
        };

        //actually returns 0.10000000000000009!
        var result = calcWeeklyPercentChange(presentValue, previousValue, aPercentChanges);
        expect(result).toBeCloseTo(0.1);
        expect(aPercentChanges.length).toEqual(1);
    });

    it("The aPercentChanges array should now be empty.", function () {
        expect(aPercentChanges.length).toEqual(0);
    });
});