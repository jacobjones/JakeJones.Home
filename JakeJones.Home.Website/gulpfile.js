/// <binding AfterBuild='sass, js' />
var fs = require("fs"),
	path = require("path"),
	gulp = require("gulp"),
	concat	= require("gulp-concat"),
	cssmin	= require("gulp-cssmin"),
	htmlmin	= require("gulp-htmlmin"),
	uglify	= require("gulp-uglify"),
	merge	= require("merge-stream"),
	del		= require("del"),
	sass	= require("gulp-sass"),
	neat	= require("bourbon-neat").includePaths,
	config	= require("./gulp.config.json");

function getFolders(dir) {
	return fs.readdirSync(dir)
		.filter(function (file) {
			return fs.statSync(path.join(dir, file)).isDirectory();
		});
}


gulp.task("sass:compile", function () {
	return gulp.src(config.sass.src)
		.pipe(sass({
			includePaths: [neat]
		}))
		.pipe(concat(config.css.concat))
		.pipe(gulp.dest(config.css.path));
});

gulp.task("sass:minify", function () {
	return gulp.src(config.css.all)
		.pipe(cssmin())
		.pipe(concat(config.css.min))
		.pipe(gulp.dest(config.css.path));
});

gulp.task("sass", gulp.series("sass:compile", "sass:minify"));

gulp.task("js:concat", function () {
	var folders = getFolders(config.js.src);

	var tasks = folders.map(function (folder) {
		return gulp.src(path.join(config.js.src, folder, "/**/*.js"))
			.pipe(concat(config.js.concat))
			.pipe(gulp.dest(path.join(config.js.path, folder)));
	});

	var root = gulp.src(path.join(config.js.src, "/*.js"))
		.pipe(concat(config.js.concat))
		.pipe(gulp.dest(config.js.path));

	return merge(tasks, root);
});

gulp.task("js:minify", function () {
	var folders = getFolders(config.js.path);

	var tasks = folders.map(function (folder) {
		return gulp.src(path.join(config.js.path, folder, "/**/", config.js.concat))
			.pipe(uglify())
			.pipe(concat(config.js.min))
			.pipe(gulp.dest(path.join(config.js.path, folder)));
	});

	var root = gulp.src(path.join(config.js.path, config.js.concat))
		.pipe(uglify())
		.pipe(concat(config.js.min))
		.pipe(gulp.dest(config.js.path));

	return merge(tasks, root);
});

gulp.task("js", gulp.series("js:concat", "js:minify"));