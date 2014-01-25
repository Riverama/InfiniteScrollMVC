var galleryvm;

function galleryViewModel(url) {
    var self = this;
    var galleryUrl = url;
    self.Page = ko.observable(0); //staring page
    self.Rows = ko.observable(40); //starting number of images
    self.Images = ko.observableArray([]);

    // refresh gallery
    self.Refresh = function () {
        $.getJSON(url, { 'page': self.Page(), 'rows': self.Rows() }, function (data) {
            $.each(data, function (x, i) {
                self.Images.push(i);
            });
        });
    };
    // advance to next page
    self.GetNextPage = function() {
        var nextPage = self.Page() + 1;
        self.Page(nextPage);
        self.Refresh();
    };

    //initial image load
    self.Refresh();
}

// hooks windows on scroll event
// detects when reaching bottom of page
$(window).scroll(function () {
    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        if (galleryvm) {
            // get next page set
            galleryvm.GetNextPage();
        };
    }
});