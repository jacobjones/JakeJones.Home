//ready(function () {
//	if (pageIs("blog--post")) {
//		var postId = document.getElementById('comments').dataset.postId;
//		var existingCommentsEl = document.getElementById('existingComments');

//		var commentTemplate = '<p id="<%this.id%>"><%this.author%></p><p><%this.content%></p>';

//		ajax.get("/blog/comment/" + postId, function (comments) {
//			var commentsHtml = "";

//			for (var i = 0; i < comments.length; i++) {
//				commentsHtml += TemplateEngine(commentTemplate, comments[i]);
//			}

//			existingCommentsEl.innerHTML = commentsHtml;

//			if (window.location.hash) {
//				var hash = window.location.hash.substring(1);
//				var offset = document.getElementById(hash).offsetTop;
//				document.documentElement.scrollTop = offset;
//			}
//		});
//	};
//});
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

//// http://krasimirtsonev.com/blog/article/Javascript-template-engine-in-just-20-line
//var TemplateEngine = function (html, options) {
//	var re = /<%([^%>]+)?%>/g, reExp = /(^( )?(if|for|else|switch|case|break|{|}))(.*)?/g, code = 'var r=[];\n', cursor = 0, match;
//	var add = function (line, js) {
//		js ? (code += line.match(reExp) ? line + '\n' : 'r.push(' + line + ');\n') :
//			(code += line != '' ? 'r.push("' + line.replace(/"/g, '\\"') + '");\n' : '');
//		return add;
//	};
//	while (match = re.exec(html)) {
//		add(html.slice(cursor, match.index))(match[1], true);
//		cursor = match.index + match[0].length;
//	}
//	add(html.substr(cursor, html.length - cursor));
//	code += 'return r.join("");';
//	return new Function(code.replace(/[\r\t\n]/g, '')).apply(options);
//};
ready(function () {
	if (pageIs("home")) {
		var musicConnector = simpleConnect.connect("music-text", "current-album-wrapper", {
			lineWidth: 2,
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
			}
		});

		var bookConnector = simpleConnect.connect("book-text", "current-book-wrapper", {
			lineWidth: 2,
			color: "#fff",
			type: "horizontal",
			offset: 20
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
			}
		});
	};
});
(function () {

    /**
     * THIS OBJECT WILL ONLY WORK IF your target is positioned relative or absolute,
     * or anything that works with the top and left css properties (not static).
     *
     * Howto
     * ============
     *
     * document.getElementById('my_target').sdrag();
     *
     * onDrag, onStop
     * -------------------
     * document.getElementById('my_target').sdrag(onDrag, null);
     * document.getElementById('my_target').sdrag(null, onStop);
     * document.getElementById('my_target').sdrag(onDrag, onStop);
     *
     * Both onDrag and onStop callback take the following arguments:
     *
     * - el, the currentTarget element (#my_target in the above examples)
     * - pageX: the mouse event's pageX property (horizontal position of the mouse compared to the viewport)
     * - startX: the distance from the element's left property to the horizontal mouse position in the viewport.
     *                  Usually, you don't need to use that property; it is internally used to fix the undesirable
     *                  offset that naturally occurs when you don't drag the element by its top left corner
     *                  (for instance if you drag the element from its center).
     * - pageY: the mouse event's pageX property (horizontal position of the mouse compared to the viewport)
     * - startY: same as startX, but for the vertical axis (and element's top property)
     *
     *
     *
     * The onDrag callback accepts an extra argument: fix.
     *
     * fix is an array used to fix the coordinates applied to the target.
     *
     * It can be used to constrain the movement of the target inside of a virtual rectangle area for instance.
     * Put a variable in the fix array to override it.
     * The possible keys are:
     *
     * - pageX
     * - startX
     * - pageY
     * - startY
     * - skipX
     * - skipY
     *
     * skipX and skipY let you skip the updating of the target's left property.
     * This might be required in some cases where the positioning of the target
     * is automatically done by the means of other css properties.
     *
     * 
     *
     *
     *
     *
     * Direction
     * -------------
     * With direction, you can constrain the drag to one direction only: horizontal or vertical.
     * Accepted values are:
     *
     * - <undefined> (the default)
     * - vertical
     * - horizontal
     *
     *
     *
     *
     */

	// simple drag
	function sdrag(onDrag, onStop, direction) {

		var startX = 0;
		var startY = 0;
		var el = this;
		var dragging = false;

		function move(e) {

			var fix = {};
			onDrag && onDrag(el, e.pageX, startX, e.pageY, startY, fix);
			if ("vertical" !== direction) {
				var pageX = ("pageX" in fix) ? fix.pageX : e.pageX;
				if ("startX" in fix) {
					startX = fix.startX;
				}
				if (false === ("skipX" in fix)) {
					el.style.left = (pageX - startX) + "px";
				}
			}
			if ("horizontal" !== direction) {
				var pageY = ("pageY" in fix) ? fix.pageY : e.pageY;
				if ("startY" in fix) {
					startY = fix.startY;
				}
				if (false === ("skipY" in fix)) {
					el.style.top = (pageY - startY) + "px";
				}
			}
		}

		function startDragging(e) {
			e.preventDefault();
			if (e.currentTarget instanceof HTMLElement || e.currentTarget instanceof SVGElement) {
				dragging = true;

				var rect = el.getBoundingClientRect();

				startX = e.pageX - rect.left + window.pageXOffset;
				startY = e.pageY - rect.top + window.pageYOffset;

				window.addEventListener("mousemove", move);
			}
			else {
				throw new Error("Your target must be an html element");
			}
		}

		this.addEventListener("mousedown", startDragging);
		window.addEventListener("mouseup", function (e) {
			if (true === dragging) {
				dragging = false;
				window.removeEventListener("mousemove", move);
				onStop && onStop(el, e.pageX, startX, e.pageY, startY);
			}
		});
	}

	Element.prototype.sdrag = sdrag;
})();
/**
 * Declare namespace
 */
simpleConnect = {};

/**
 * This member is an auxiliary counter used for generate unique identifiers.
 */
simpleConnect._idGenerator = 0;

/**
 * This member is an associative array which contains all the document's connections.
 */
simpleConnect._connections = {};

/**
 * Gets the position of an element, optionally takes a second element to use
 * in calculating a relative position.
 * 
 * @param {object} elem The element.
 * @param {object} relativeElem The element to use for relative positioning.
 * @returns {object} An associative array containg the properties: top, vcenter (vertical
 * center), bottom, left, hcenter (horizontal center) and right.
 */
simpleConnect._getPosition = function (elem, relativeElem) {
	var rect = elem.getBoundingClientRect();

	var top = rect.top + window.pageYOffset;
	var left = rect.left + window.pageXOffset;

	if (relativeElem) {
		var relativeRect = relativeElem.getBoundingClientRect();
		top = rect.top - relativeRect.top;
		left = rect.left - relativeRect.left;
	}

	return {
		top: top,
		vcenter: top + (elem.offsetHeight / 2 | 0),
		bottom: top + elem.offsetHeight,
		left: left,
		hcenter: left + (elem.offsetWidth / 2 | 0),
		right: left + elem.offsetWidth
	};
}

/**
 * Positions a canvas object based on the position of the two supplied
 * elements.
 * 
 * @param {object} canvas The canvas object.
 * @param {object} elemA The first element.
 * @param {object} elemB The second element.
 */
simpleConnect._positionCanvas = function (canvas, elemA, elemB) {
	var elemAPos = simpleConnect._getPosition(elemA);
	var elemBPos = simpleConnect._getPosition(elemB);

	canvas.style.left = Math.min(elemAPos.left, elemBPos.left) + "px";
	canvas.style.top = Math.min(elemAPos.top, elemBPos.top) + "px";

	canvas.width = Math.max(elemAPos.right, elemBPos.right) - Math.min(elemAPos.left, elemBPos.left);
	canvas.height = Math.max(elemAPos.bottom, elemBPos.bottom) - Math.min(elemAPos.top, elemBPos.top);
}

/**
 * Creates a canvas and positions it relative to the two supplied elements.
 * 
 * @param {object} elemA The first element.
 * @param {object} elemB The second element.
 * @param {string} id The ID for the canvas object.
 * @returns {object} The canvas object.
 */
simpleConnect._createCanvas = function (elemA, elemB, id) {
	var canvas = document.createElement("canvas");

	canvas.id = "canvas_" + id;

	canvas.style.zIndex = -1;
	canvas.style.position = "absolute";
	//canvas.style.border = "1px dashed black";

	document.body.appendChild(canvas);
	simpleConnect._positionCanvas(canvas, elemA, elemB);

	return canvas;
}

/**
 * Draws a connection between two elements.
 *
 * @param {string} elemAId The ID of the first element.
 * @param {string} elemBId The ID of the second element.
 * @param {object} options An associative array with the properties 'color' (which defines the color of the connection),
 * 'lineWidth' (the width of the connection), 'type' (the preferred type of the connection: 'horizontal' or 'vertical'),
 * 'offset' (distance between connector and elements), 'arrowWidth' (width of the arrowhead, 0 for none) and
 * 'arrowHeight' (height of the arrowhead, 0 for none)
 * @returns {string} The connection identifier or 'null' if the connection could not be draw.
 */
simpleConnect.connect = function (elemAId, elemBId, options) {
	// Get the elements and verify they exist.
	var elemA = document.getElementById(elemAId);
	if (!elemA) return null;
	var elemB = document.getElementById(elemBId);
	if (!elemB) return null;

	// Create connection object.
	var connection = {};
	connection.id = "simpleConnect_" + simpleConnect._idGenerator++;
	connection.elemA = elemA;
	connection.elemB = elemB;
	connection.lineWidth = (options && options.lineWidth !== null && !isNaN(options.lineWidth)) ? (options.lineWidth | 0) : 4;
	connection.color = (options && options.color !== null) ? options.color + "" : "#000";
	connection.type = (options && options.type !== null && (options.type === "vertical" || options.type === "horizontal")) ? options.type : "horizontal";
	connection.offset = (options && options.offset !== null && !isNaN(options.offset)) ? (options.offset | 0) : 10;
	connection.canvas = simpleConnect._createCanvas(elemA, elemB, connection.id);
	connection.arrowWidth = (options && options.arrowWidth !== null && !isNaN(options.arrowWidth)) ? (options.arrowWidth | 0) : 10;
	connection.arrowHeight = (options && options.arrowHeight !== null && !isNaN(options.arrowHeight)) ? (options.arrowHeight | 0) : 10;

	// Add connection to the connection's list.
	simpleConnect._connections[connection.id] = connection;

	// Populate the connection with the actual lines.
	simpleConnect._populateConnection(connection);

	// Return result.
	return connection.id;
}

/**
 * Repaints a connection.
 *
 * @param {string} connectionId The connection identifier.
 * @returns {boolean} 'true' if the operation was done, 'false' if the connection no exists.
 */
simpleConnect.repaintConnection = function (connectionId) {
	var connection = simpleConnect._connections[connectionId];
	if (connection) {
		simpleConnect._positionCanvas(connection.canvas, connection.elemA, connection.elemB);
		simpleConnect._populateConnection(connection);
		return true;
	}
	return false;
}

/**
 * Calculates the middle of two points. 
 *
 * @param {object} elemAPos The position of the first element.
 * @param {object} elemBPos The position of the second element.
 * @param {string} pointA The property name of the first position.
 * @param {string} pointB The property name of the second position.
 * @returns {int} The middle of the given points.
 */
simpleConnect._calculateMiddle = function (elemAPos, elemBPos, pointA, pointB) {
	var p1 = pointA;
	var p2 = pointB;

	// The point with the smallest value needs to come first
	if (Math.min(elemAPos[pointB], elemBPos[pointB]) > Math.min(elemAPos[pointA], elemBPos[pointA])) {
		p1 = pointB;
		p2 = pointA;
	}

	return (Math.min(elemAPos[p1], elemBPos[p1]) + (Math.abs(Math.max(elemAPos[p2], elemBPos[p2]) - Math.min(elemAPos[p1], elemBPos[p1])) / 2) | 0);
}

/**
 * Draws a connection.
 *
 * @param {object} c The connection properties.
 * @param {object} ctx The canvas context.
 * @param {object} elemAPos The position of the first element.
 * @param {object} elemBPos The position of the second element.
 * @param {string} type The type of the connection, either 'vertical' or 'horizontal'.
 * @returns {boolean} 'true' if the operation was successful, 'false' if it wasn't.
 */
simpleConnect._drawConnection = function (c, ctx, elemAPos, elemBPos, type) {
	// Default is horizontal, e.g. a line connection left and right
	var pointA = "left";
	var pointB = "right";
	var pointC = "vcenter";

	if (type === "vertical") {
		pointA = "top";
		pointB = "bottom";
		pointC = "hcenter";
	}

	// An array to contain our calculated co-ordinates
	var coords = [];

	// Are we drawing from element A to B, or is the reverse true?
	var reverse = elemAPos[pointA] > elemBPos[pointA];

	// The available space to draw the line in
	var availableSpace = Math.max(elemAPos[pointA], elemBPos[pointA]) - Math.min(elemAPos[pointB], elemBPos[pointB]);

	// We're going to have problems fitting this in (buffer of 5).
	// Don't draw it in this scenario.
	if ((availableSpace / 2) <= (c.offset + c.arrowHeight + (c.lineWidth / 2) + 5)) {
		return false;
	}

	// Calculate the midpoint between our two points to be connected
	var midpoint = simpleConnect._calculateMiddle(elemAPos, elemBPos, pointA, pointB);

	// Generate our co-ordinates, incorporating offsets and arrows if specified
	coords.push({
		c1: reverse ? Math.max(elemAPos[pointA] - c.offset, midpoint) : Math.min(elemAPos[pointB] + c.offset, midpoint),
		c2: elemAPos[pointC]
	});
	coords.push({
		c1: midpoint,
		c2: elemAPos[pointC]
	});
	coords.push({
		c1: midpoint,
		c2: elemBPos[pointC]
	});
	coords.push({
		c1: reverse ? Math.min(elemBPos[pointB] + c.offset + c.arrowHeight, midpoint) : Math.max(elemBPos[pointA] - c.offset - c.arrowHeight, midpoint),
		c2: elemBPos[pointC]
	});

	simpleConnect._drawPath(ctx, coords, type);

	// We should draw an arrow if dimensions are specified
	if (c.arrowHeight > 0 && c.arrowWidth > 0) {
		simpleConnect._drawArrow(c, ctx, coords[coords.length - 1], type, reverse);
	}

	return true;
}


/**
 * Draws a path.
 *
 * @param {object} ctx The canvas context.
 * @param {Array.<Object>} coords The co-ordinates of the path to draw. Objects contain two properties 'c1' and 'c2', reflecting the co-ordinate values.
 * @param {string} type The type of the connection, either 'vertical' or 'horizontal'.
 */
simpleConnect._drawPath = function (ctx, coords, type) {
	ctx.beginPath();

	// Draw our coords, ensuring we switch them around if they are
	// for a vertical line.
	coords.forEach(function (el, index) {
		if (index === 0) {
			if (type === "vertical") {
				ctx.moveTo(el.c2, el.c1);
			} else {
				ctx.moveTo(el.c1, el.c2);
			}
		} else {
			if (type === "vertical") {
				ctx.lineTo(el.c2, el.c1);
			} else {
				ctx.lineTo(el.c1, el.c2);
			}
		}
	});

	ctx.stroke();
}

/**
 * Draws an arrow.
 *
 * @param {object} c The connection properties.
 * @param {object} ctx The canvas context.
 * @param {object} startCoords The co-ordinates to start the arrow from.
 * @param {string} type The type of the connection, either 'vertical' or 'horizontal'.
 * @param {bool} reverse Indicates whether the arrow direction should be reversed.
 */
simpleConnect._drawArrow = function (c, ctx, startCoords, type, reverse) {
	var coords = [];

	coords.push({
		c1: startCoords.c1,
		c2: startCoords.c2 - (c.arrowWidth / 2)
	});

	coords.push({
		c1: startCoords.c1 + (reverse ? -c.arrowHeight : c.arrowHeight),
		c2: startCoords.c2
	});

	coords.push({
		c1: startCoords.c1,
		c2: startCoords.c2 + (c.arrowWidth / 2)
	});

	simpleConnect._drawPath(ctx, coords, type);

	ctx.fill();
}

/**
 * Populates a connection, acording to the position of the elements to be connected.
 * 
 * @param {object} connection The connection properties.
 */
simpleConnect._populateConnection = function (connection) {
	var ctx = connection.canvas.getContext("2d");

	// Get the positions of our elementts
	var elemAPos = simpleConnect._getPosition(connection.elemA, connection.canvas);
	var elemBPos = simpleConnect._getPosition(connection.elemB, connection.canvas);

	// Set the color for our canvas context
	ctx.strokeStyle = connection.color;
	ctx.fillStyle = connection.color;
	ctx.lineWidth = connection.lineWidth;

	var success = simpleConnect._drawConnection(connection, ctx, elemAPos, elemBPos, connection.type);

	if (!success) {
		simpleConnect._drawConnection(connection, ctx, elemAPos, elemBPos, (connection.type === "vertical" ? "horizontal" : "vertical"));
	}
}

/**
 * Removes a connection.
 *
 * @param {string} connectionId The connection identifier.
 * @returns {boolean} 'true' if the operation was done, 'false' if the connection no exists.
 */
simpleConnect.removeConnection = function (connectionId) {
	if (simpleConnect._connections[connectionId] !== null) {
		// Remove the canvas
		var canvas = simpleConnect._connections[connectionId].canvas;
		canvas.parentNode.removeChild(canvas);

		// Remove connection data.
		simpleConnect._connections[connectionId] = null;
		delete simpleConnect._connections[connectionId];

		// Return result.
		return true;
	}
	return false;
}