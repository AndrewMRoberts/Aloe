app.controller('TransactionsController', function(TransactionService) {
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
});