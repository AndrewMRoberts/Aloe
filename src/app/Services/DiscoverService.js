app.service('DiscoverService', function($http) {
    var service = this;
    var apiUrl = 'https://portal.discover.com/customersvcs/universalLogin/signin';

    service.Get = function() {
        return $http.get(apiUrl)
            .then(function success(response) {
                return response.data;
            });
    }
});