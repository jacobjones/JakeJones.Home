document.getElementById("search-input").addEventListener("keyup", function (event) {
	if (event.keyCode !== 13) {
		return;
	}

	// Don"t follow the link
	event.preventDefault();

	var query = document.getElementById("search-input").value;

	ajax.get("/api/search/?q=" + query, function (results) {
		console.log(results);
		//results.forEach(function (result) {
		//	resultsElem.innerHTML = resultsElem.innerHTML + "<li class=\"search-results__item\"><a href=\"" + result.url + "\">" + result.name + "</a></li>";
		//});
	});

}, false);