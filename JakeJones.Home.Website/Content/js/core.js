var ajax = {};

ajax.get = function (url, callback) {
	var request = new XMLHttpRequest();
	request.open("GET", url, true);

	request.onload = function () {
		if (request.status >= 200 && request.status < 400) {
			// Success!
			callback(JSON.parse(request.responseText));
		} else {
			// We reached our target server, but it returned an error
		}
	};

	request.onerror = function () {
		// There was a connection error of some sort
	};

	request.send();
};

function ready(callback) {
	if (document.attachEvent ? document.readyState === "complete" : document.readyState !== "loading") {
		callback();
	} else {
		document.addEventListener('DOMContentLoaded', callback);
	}
}

function pageIs(className) {
	if (document.body.classList)
		return document.body.classList.contains(className);
	else
		return new RegExp('(^| )' + className + '( |$)', 'gi').test(document.body.className);
}


/*TODO: This needs tidying up*/
function loaded(el) {
	if (el.parentNode.classList)
		el.parentNode.classList.remove("loading");
}