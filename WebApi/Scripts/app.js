var ViewModel = function () {
    var self = this;
    self.students = ko.observableArray();
    self.error = ko.observable();

    var studentsUri = '/api/Students/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllStudents() {
        ajaxHelper(studentsUri, 'GET').done(function (data) {
            self.students(data);
        });
    }

    // Fetch the initial data.
    getAllStudents();
};

ko.applyBindings(new ViewModel());