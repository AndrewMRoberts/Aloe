app.controller('TransactionsController', ['$scope', function($scope, TransactionService) {
    var controller = this;
    controller.Title = 'Transactions';

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
        $scope.reverse = $scope.sortParameter === sortParameter ? !$scope.reverse : false;
        $scope.sortParameter = sortParameter;
    };
}]);