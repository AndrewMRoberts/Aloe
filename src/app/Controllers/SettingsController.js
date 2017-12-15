app.controller('SettingsController', ['$scope', 'AccountService', function($scope, AccountService) {
    var controller = this;
    controller.Title = 'Settings';

    LoadAccounts();

    function LoadAccounts() {
        AccountService.Get()
            .then(function(accounts) {
                controller.Accounts = accounts;
            }, function(error) {
                controller.ErrorMessage = error;
            });
    }
}]);