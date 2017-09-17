app.service('TransactionService', function($http) {
    var service = this;
    var apiUrl = 'http://localhost:5000/api';

    service.Get = function() {
        return $http.get(apiUrl + '/transaction')
            .then(function success(response) {
                return response.data;
            });
    }
});