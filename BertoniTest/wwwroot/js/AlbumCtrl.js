(function (window) {
    var $btnShowAlbum,$dropdownSelected,$tbodyalbumsphoto;
    var _repository;
    function AlbumCtrl(repository) {
        _repository = repository;
    }
    var initHtmlElements = function () {
        $btnShowAlbum = $("#btn-show-album");
        $dropdownSelected = $("#Album");
        $tbodyalbumsphoto = $("#tbody-albums-photos");
    }

    var bindEvents = function () {
        $btnShowAlbum.unbind("click").bind("click", function () {
            _repository.GetAlbums($dropdownSelected.val(), function (response) {
                $tbodyalbumsphoto.empty();
                $tbodyalbumsphoto.html(response.vista);
                console.log(response);
            });
        });
    }

    AlbumCtrl.prototype.Init = function () {
        initHtmlElements();
        bindEvents();
    }
    window.app = window.app || {};
    window.app.AlbumCtrl = AlbumCtrl;
})(window);