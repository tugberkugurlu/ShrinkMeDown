/// <reference path="../jquery-1.7.1.js" />
/// <reference path="../knockout-2.0.0.debug.js" />

window.urlShrinker = {};

(function (urlS) {
    "use strict";

    var vm = {};

    $.getJSON("/api/urls", function (result) {

        vm.urls = buildUrlsObservableArray(result);
        ko.applyBindings(vm);
    });

    function buildUrlsObservableArray(json) {

        var arrayOfUrl = ko.observableArray();

        $.each(json, function (i, d) {

            var _tempUrl = new Url()
                    .Id(d.Id)
                    .Alias(d.Alias)
                    .Url(d.Url)
                    .CreatedOn(d.CreatedOn)
                    .UpdatedOn(d.UpdatedOn);

            arrayOfUrl.push(_tempUrl);
        });

        return arrayOfUrl();
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

}(urlShrinker));