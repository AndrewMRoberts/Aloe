app.controller('TransactionsController', ['$scope', 'TransactionService', function($scope, TransactionService) {
    var controller = this;
    controller.Title = 'Transactions';
    $scope.sortParameter = 'EffectiveDate';
    $scope.columnSearchText = 'Search Column';

    LoadTransactions();

    function LoadTransactions() {
        TransactionService.Get()
            .then(function(transactions) {
                controller.Transactions = transactions;
            }, function(error) {
                controller.ErrorMessage = error;
            });
    }

    $scope.sortBy = function(sortParameter) {
        $scope.reverseSort = $scope.sortParameter === sortParameter ? !$scope.reverseSort : false;
        $scope.sortParameter = sortParameter;
    };
}]);