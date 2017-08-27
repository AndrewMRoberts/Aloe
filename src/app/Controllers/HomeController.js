app.controller('HomeController', function(ContactsService) {
    var controller = this;
    controller.Title = 'Overview';

    LoadContacts();

    function LoadContacts() {
        ContactsService.Get()
            .then(function(contacts) {
                controller.Contacts = contacts;
            }, function(error) {
                controller.ErrorMessage = error;
            });
    }
});