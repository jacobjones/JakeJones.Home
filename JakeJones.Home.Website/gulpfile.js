/// <binding AfterBuild='sass, js' />
var gulp = require("gulp"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	htmlmin = require("gulp-htmlmin"),
	uglify = require("gulp-uglify"),
	merge = require("merge-stream"),
	del = require("del"),
	fs = require("fs"),
	sass = require("gulp-sass"),
	config = require("./gulp.config.json");
	

gulp.task("sass:compile", function () {
	return gulp.src(config.sass.src)
		.pipe(sass())
		.pipe(concat(config.css.concat))
		.pipe(gulp.dest(config.css.path));
});

gulp.task("sass:minify", function () {
	return gulp.src(config.css.all)
		.pipe(cssmin())
		.pipe(concat(config.css.min))
		.pipe(gulp.dest(config.css.path));
});

gulp.task("sass", ["sass:compile", "sass:minify"]);

gulp.task("js:concat", function () {
	return gulp.src(config.js.src)
		.pipe(concat(config.js.concat))
		.pipe(gulp.dest(config.js.path));
});

gulp.task("js:minify", function () {
	return gulp.src(config.js.all)
		.pipe(uglify())
		.pipe(concat(config.js.min))
		.pipe(gulp.dest(config.js.path));
});

gulp.task("js", ["js:concat", "js:minify"]);