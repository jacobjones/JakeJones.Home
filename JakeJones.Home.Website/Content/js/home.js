﻿ready(function () {
	if (pageIs("home")) {
		var musicConnector = simpleConnect.connect("music-text", "current-album-wrapper", {
			lineWidth: 1,
			color: "#fff",
			type: "horizontal",
			offset: 20
		});

		var bookConnector = simpleConnect.connect("book-text", "current-book-wrapper", {
			lineWidth: 1,
			color: "#fff",
			type: "horizontal",
			offset: 20
		});

		document.getElementById('current-album-wrapper').sdrag(function () {
			simpleConnect.repaintConnection(musicConnector);
		});

		ajax.get("/api/music/current", function (album) {
			var e = document.getElementById("current-album");

			loaded(e);

			if (e) {
				e.src = album.imageUrl;
				e.alt = album.title;
				e.title = album.title;
				simpleConnect.repaintConnection(musicConnector);
				setTimeout(function () {
					simpleConnect.repaintConnection(bookConnector);
					simpleConnect.repaintConnection(musicConnector);
				}, 500);
			}
		});

		document.getElementById('current-book-wrapper').sdrag(function () {
			simpleConnect.repaintConnection(bookConnector);
		});

		window.onresize = function () {
			simpleConnect.repaintConnection(musicConnector);
			simpleConnect.repaintConnection(bookConnector);
		};

		ajax.get("/api/books/current", function (book) {
			var e = document.getElementById("current-book");

			loaded(e);

			if (book) {
				e.src = book.imageUrl;
				e.alt = book.title;
				e.title = book.title;
				simpleConnect.repaintConnection(bookConnector);
				setTimeout(function () {
					simpleConnect.repaintConnection(bookConnector);
					simpleConnect.repaintConnection(musicConnector);
				}, 500);
			}
		});
	};
});