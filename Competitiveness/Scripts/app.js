angular.module("startApp", [])
    .controller("MAVTController", function ($scope) {
        $scope.number = [];
        $scope.totalnum = 0;
        $scope.weight = [];

        $scope.total = function () {
            var total = 0;
            for (var i = 0 ; i < $scope.number.length; i++) {
                total += $scope.number[i];
            }
            $scope.totalnum = total.toFixed(2);

        };
        $scope.functionCaculatorWeight = function () {
            for (var i = 0 ; i < $scope.number.length; i++) {
                if ($scope.totalnum > 0) {
                    $scope.weight[i] = ($scope.number[i] / $scope.totalnum).toPrecision(4);
                }
            }
        }


    })



;