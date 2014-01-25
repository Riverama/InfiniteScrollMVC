InfiniteScrollMVC
=================

###Infinite Image Scroll MVC with knockout.js

Updated: Jan 25th 2014 
By: Cristian F Rivera @ Sifabs Systems<br>
[MSDN Project Page](http://code.msdn.microsoft.com/Infinite-Image-Scroll-66590b24)

###Project Description
Simple ASP.NET MVC based template showcasing an infinite image scrolling app which utilizes [twitter bootstrap](http://getbootstrap.com/) for layout and gallery composition and a [knockou.js](http://knockoutjs.com/index.html) model with the required functionality to refresh the gallery as you move down the page (à la google images or bing).

The structure is fairly simple, in the server side we provide a 'service method' with a 'JsonResult' action in the home controller that we use to retrieve a paged collection of image files from the source, which is encoded in json format so we can use it with jquery ajax calls. This may also be replaced with a Web API controller if you wish:
<pre>
  public class HomeController : Controller
  {
      public JsonResult GetGallery(int page = 0, int rows = 16)
      {
        // return paged images collection
      }
  }
</pre>        

In the client side, the knockout model calls the service method and stores the resulting image details in an observableArray property:
<pre>
function galleryViewModel(url) {
    var self = this;
    var galleryUrl = url;
    self.Page = ko.observable(0); //starting page
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
</pre>

Finally we subscribe to the scroll event of the window object to detect when moving to the bottom of the page:
<pre>
$(window).scroll(function () {
  //calls the model's GetNextPage() when reaching the bottom of the page
});
</pre>

###To Dos

* Show count of all images in gallery.
* Add a gif or animation when loading a new page of images.
* Provide a 'fade in' effect for displaying new images.

###Technologies
* ASP.NET MVC 4 C#
* jQuery AJAX, Json
* Bootstrap
* Knockout.js
* Javascript

###Requirements
Visual Studio 2012 or 2013

 
###License
Licensed under Apache, Version 2.0 license [Apache License](http://www.apache.org/licenses/LICENSE-2.0.html)

