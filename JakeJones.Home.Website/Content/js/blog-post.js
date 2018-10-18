ready(function () {
	if (pageIs("blog--post")) {
		var postId = document.getElementById('comments').dataset.postId;
		var existingCommentsEl = document.getElementById('existingComments');

		var commentTemplate = '<p><%this.author%></p><p><%this.content%></p>';

		ajax.get("/blog/comment/" + postId, function (comments) {
			var commentsHtml = "";

			for (var i = 0; i < comments.length; i++) {
				commentsHtml += TemplateEngine(commentTemplate, comments[i]);
			}

			existingCommentsEl.innerHTML = commentsHtml;
		});
	};
});