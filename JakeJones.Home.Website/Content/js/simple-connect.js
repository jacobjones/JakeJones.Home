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
	var width = Math.max(elemAPos.right, elemBPos.right) - Math.min(elemAPos.left, elemBPos.left);
	var height = Math.max(elemAPos.bottom, elemBPos.bottom) - Math.min(elemAPos.top, elemBPos.top);
	var dpr = window.devicePixelRatio;

	canvas.style.left = Math.min(elemAPos.left, elemBPos.left) + "px";
	canvas.style.top = Math.min(elemAPos.top, elemBPos.top) + "px";

	canvas.width = width * dpr;
	canvas.height = height * dpr;

	canvas.style.width = width + "px";
	canvas.style.height = height + "px";

	canvas.getContext('2d').scale(dpr, dpr);
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