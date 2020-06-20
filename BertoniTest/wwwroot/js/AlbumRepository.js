(function (window) {
    function AlbumRepository() {

    }

    AlbumRepository.prototype.GetAlbums = function (data, callback) {
        $.ajax({
            url: '/Home/GetAlbum/'+data,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json',
            success: function (response) {
                return callback(response);
            },
            error: function (xhr,status,error) {
                console.log(xhr);
            }
        })
    }
    window.app = window.app || {};
    window.app.AlbumRepository = AlbumRepository;
})(window);