/// <reference path="../jquery-1.7.1.js" />
/// <reference path="../knockout-2.0.0.debug.js" />
/// <reference path="../twitter-bootstrap/bootstrap-modal.js" />

window.urlShrinker = {};

(function (urlS) {
    "use strict";

    var vm = {};

    vm.urlCreate = ko.observable(new Url());
    vm.urls = ko.observableArray([]);

    //ViewModel operations
    vm.addNew = function() {

        $("#addNewModal").modal("show");
    };

    vm.addNewRecord = function() { 

        //Work with vm.urlCreate() here
    };

    //addNewModal stuff
    $("#addNewModal").modal();
    $("#addNewModal").modal("hide");
    $("#addNewModal").modal("refresh");
    $('#addNewModal').on('hidden', function () {
        
        //clear the vm.urlCreate instance
        vm.urlCreate(new Url());
    });

    //helper functions
    function bindViewModel() {

        ko.applyBindings(vm);
    }

    function buildUrlsObservableArray(json) {

        //var arrayOfUrl = ko.observableArray();
        var arrayOfUrl = [];

        $.each(json, function (i, d) {

            var _tempUrl = new Url()
                    .Id(d.Id)
                    .Alias(d.Alias)
                    .Url(d.Url)
                    .CreatedOn(d.CreatedOn)
                    .UpdatedOn(d.UpdatedOn);
                    
            arrayOfUrl.push(_tempUrl);
        });

        return arrayOfUrl;
    }

    //domain models
    function Url() {

        var self = this;

        self.Id = ko.observable();
        self.Alias = ko.observable();
        self.Url = ko.observable();
        self.CreatedOn = ko.observable();
        self.UpdatedOn = ko.observable();
    }

    $.getJSON("/api/urls", function (result) {

        vm.urls(
            buildUrlsObservableArray(result)
        );

        bindViewModel();
    });

} (urlShrinker));