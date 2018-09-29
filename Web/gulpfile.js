﻿/// <binding Clean='clean:lib' AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');
var rimraf = require("rimraf");
var merge = require('merge-stream');
var fs = require("fs");
var less = require("gulp-less");

// Dependency Dirs
var deps = {
    "jquery": {
        "dist/*": ""
    },
    "jquery-validation": {
        "dist/**/*": ""
    },
    "jquery-validation-unobtrusive": {
        "dist/*": ""
    },
    "bootstrap": {
        "dist/**/*": ""
    },
    "requirejs": {
        "*.js": ""
    },
    "@fortawesome/fontawesome-free-webfonts": {
        "**/*": ""
    }
};

gulp.task("clean:lib", function (cb) {
    return rimraf("wwwroot/lib/", cb);
});

gulp.task("scripts", function () {

    var streams = [];

    for (var prop in deps) {
        console.log("Prepping Scripts for: " + prop);
        for (var itemProp in deps[prop]) {
            streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
                .pipe(gulp.dest("wwwroot/lib/" + prop + "/" + deps[prop][itemProp])));
        }
    }

    return merge(streams);

});

gulp.task("less", function () {
    return gulp.src('Styles/*.less')
        .pipe(less())
        .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("default", [ 'scripts','less']);