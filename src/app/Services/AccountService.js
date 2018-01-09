app.service('AccountService', function($http) {
    var service = this;
    var apiUrl = 'http://localhost:5000/api/account';

    service.Get = function() {
        
        return $http.get(apiUrl)
            .then(function success(response) {
                return response.data;
            });
    }

    service.Create = function(account) {

        return $http.post(apiUrl + '/Create', account)
            .then(function success(response) {
                return response.data;
            });
    }
});