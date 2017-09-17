app.controller('OverviewController', function(OverviewService) {
    var controller = this;
    controller.Title = 'Overview';

    LoadContacts();

    function LoadContacts() {
        OverviewService.Get()
            .then(function(contacts) {
                controller.Contacts = contacts;
            }, function(error) {
                controller.ErrorMessage = error;
            });
    }
});