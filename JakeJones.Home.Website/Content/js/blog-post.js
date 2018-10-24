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