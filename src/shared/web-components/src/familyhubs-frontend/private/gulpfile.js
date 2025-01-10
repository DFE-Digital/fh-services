/// <binding ProjectOpened='js:watch, sass-to-min-css:watch' />
"use strict";

/*todo: new location*/

var tsScriptsSrc = '../scripts/**';

const gulp = require("gulp");
const sass = require('gulp-sass')(require('sass'));
const sourcemaps = require('gulp-sourcemaps');
const csso = require('gulp-csso');
const terser = require('gulp-terser');
const ts = require("gulp-typescript");
    //typescript = require('typescript'),
const rollup = require('gulp-better-rollup');
    //concat = require('gulp-concat'),
const del = require('del');
const rename = require('gulp-rename');
const fs = require('fs');

// Hacky but it works
let sassPaths = [
    '../',
    '../node_modules/govuk-frontend/dist'
];

gulp.task('sass-to-min-css', async function () {
    return gulp.src('../styles/all.scss')
        .pipe(sourcemaps.init())
          .pipe(sass({
              includePaths: sassPaths
          }).on('error', sass.logError))
        .pipe(csso())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('sass-to-min-css:watch', function () {
    gulp.watch('../styles/**', gulp.series('sass-to-min-css'));
});

// https://www.meziantou.net/compiling-typescript-using-gulp-in-visual-studio.htm

//todo: clean to delete files in dest? & tmp folder

var tsProject;

gulp.task('transpile-ts', function () {

    if (!tsProject) {
        tsProject = ts.createProject({
            noImplicitAny: false,
            noEmitOnError: true,
            removeComments: false,
            target: "es6",
            allowJs: true,
            checkJs: true,
            moduleResolution: "node"
        });
    }

    //console.log(`TypeScript version: ${typescript.version}`);

    var reporter = ts.reporter.fullReporter();

    var tsResult = gulp.src(tsScriptsSrc)
        .pipe(sourcemaps.init())
        .pipe(tsProject(reporter));

    return tsResult.js
        .pipe(sourcemaps.write())
        .pipe(gulp.dest("./tmp/js"));
});

//todo: try rollup-plugin-preserve-modules to preserve the order of js files (if including govuk)
//todo: any benefit of using rollup-plugin-terser?

//gulp.task('naive-bundle-js', () => {
//    return gulp.src(['./wwwroot/lib/govuk/assets/js/govuk-4.4.1.js', './tmp/js/app.js'])
//        .pipe(sourcemaps.init())
//        .pipe(concat('bundle.js'))
//        // inlining the sourcemap into the exported .js file
//        .pipe(sourcemaps.write())
//        .pipe(gulp.dest('./tmp/js'));
//});

gulp.task('bundle-and-minify-js', () => {

    const packageJson = JSON.parse(fs.readFileSync(`../package.json`));

    const baseFileName = `${packageJson.name}-${packageJson.version}`;

    console.log(`Creating js: ${baseFileName}.min.js`);

//    return gulp.src('./tmp/js/bundle.js')
    return gulp.src('./tmp/js/familyhubs.js')
        .pipe(sourcemaps.init())
        .pipe(rollup({}, 'es'))
        .pipe(terser())
        .pipe(rename({ basename: baseFileName, suffix: '.min' }))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('../wwwroot/js'));
});

gulp.task('bundle-debug-js', () => {

    const packageJson = JSON.parse(fs.readFileSync(`../package.json`));

    const baseFileName = `${packageJson.name}-${packageJson.version}`;

    console.log(`Creating js: ${baseFileName}.min.js`);

//    return gulp.src('./tmp/js/bundle.js')
    return gulp.src('./tmp/js/familyhubs.js')
        .pipe(rollup({}, 'es'))
        .pipe(rename({ basename: baseFileName, suffix: '.min' }))
        .pipe(gulp.dest('../wwwroot/js'));
});

gulp.task('clean', () => {
    return del('./tmp/**');
});

gulp.task('copy-js-files-to-example-site', function () {
    return gulp.src('../wwwroot/js/*')
        .pipe(gulp.dest('../../../example/FamilyHubs.Example/wwwroot/js'));
});

//gulp.task('js', gulp.series('clean', 'transpile-ts', 'naive-bundle-js', 'bundle-and-minify-js'));
gulp.task('js', gulp.series('clean', 'transpile-ts', 'bundle-and-minify-js', 'copy-js-files-to-example-site'));

gulp.task('js-debug', gulp.series('clean', 'transpile-ts', 'bundle-debug-js', 'copy-js-files-to-example-site'));

gulp.task('js:watch', function () {
    gulp.watch(tsScriptsSrc, gulp.series('js'));
});
