ready(function () {
	if (pageIs("home")) {

		ajax.get("/api/music/current", function (album) {
			var e = document.getElementById("current-album");

			loaded(e);

			if (e) {
				e.src = album.imageUrl;
				e.alt = album.title;
				e.title = album.title;
			}
		});

		ajax.get("/api/books/current", function (book) {
			var e = document.getElementById("current-book");

			loaded(e);

			if (book) {
				e.src = book.imageUrl;
				e.alt = book.title;
				e.title = book.title;
			}
		});

	};
});