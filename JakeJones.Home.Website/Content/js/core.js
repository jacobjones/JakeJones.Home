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

// http://krasimirtsonev.com/blog/article/Javascript-template-engine-in-just-20-line
var TemplateEngine = function (html, options) {
	var re = /<%([^%>]+)?%>/g, reExp = /(^( )?(if|for|else|switch|case|break|{|}))(.*)?/g, code = 'var r=[];\n', cursor = 0, match;
	var add = function (line, js) {
		js ? (code += line.match(reExp) ? line + '\n' : 'r.push(' + line + ');\n') :
			(code += line != '' ? 'r.push("' + line.replace(/"/g, '\\"') + '");\n' : '');
		return add;
	};
	while (match = re.exec(html)) {
		add(html.slice(cursor, match.index))(match[1], true);
		cursor = match.index + match[0].length;
	}
	add(html.substr(cursor, html.length - cursor));
	code += 'return r.join("");';
	return new Function(code.replace(/[\r\t\n]/g, '')).apply(options);
};