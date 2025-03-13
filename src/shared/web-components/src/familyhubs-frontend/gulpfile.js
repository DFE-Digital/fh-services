"use strict";

const gulp = require("gulp"),
    rename = require('gulp-rename'),
    path = require('path'),
    fs = require('fs');

function getWwwRootDir(remotelyInstalled) {
    let baseDir = remotelyInstalled ? process.env.npm_config_local_prefix : '.';

    // Check if this is a NPM link situation
    //if (process.env.npm_lifecycle_event === 'link') {}

    return baseDir + '/wwwroot';
}

gulp.task('assets', function () {
    return gulp.src(['node_modules/govuk-frontend/dist/govuk/assets/**/*'])
        .pipe(gulp.dest('wwwroot/lib/govuk/assets'));
});

gulp.task('copy-wwwroot', function () {
    let baseDir = getWwwRootDir(false);
    return gulp.src('wwwroot/**/*')
        .pipe(gulp.dest(baseDir));
});

gulp.task('copy-wwwroot-remote', function () {
    let baseDir = getWwwRootDir(true);
    return gulp.src('wwwroot/**/*')
      .pipe(gulp.dest(baseDir));
});

//todo: map files also need to be copied
function copyPackageJsToWwwroot(packageName, srcFilename) {
    let nodeModules = 'node_modules';

    // Read the package.json file to get the package version
    const packageJson = JSON.parse(fs.readFileSync(`${nodeModules}/${packageName}/package.json`));
    const packageVersion = packageJson.version;

    // Set the destination file name
    let destPackageName = packageName.replaceAll(/[\\\/]/g, '-').replaceAll(/@/g, '');
    const destFileName = `${destPackageName}-${packageVersion}.min.js`;

    let baseDir = getWwwRootDir();

    //console.log(process.cwd());
    //console.log(baseDir);
    // Copy and rename the file
    return gulp.src(`${nodeModules}/${packageName}/${srcFilename}`)
        .pipe(rename(destFileName))
        .pipe(gulp.dest(baseDir + '/js'));
}

gulp.task('copy-accessible-autocomplete-js', function () {
    return copyPackageJsToWwwroot('accessible-autocomplete', 'dist/accessible-autocomplete.min.js');
});

gulp.task('copy-govuk-frontend-js', function () {
    return copyPackageJsToWwwroot('govuk-frontend', 'dist/govuk/all.bundle.js');
});

gulp.task('copy-dfe-frontend-js', function () {
    return copyPackageJsToWwwroot('dfe-frontend', 'dist/dfefrontend.min.js');
});

gulp.task('copy-moj-frontend-js', function () {
    return copyPackageJsToWwwroot('@ministryofjustice/frontend', 'moj/all.js');
});

gulp.task('copy-js', gulp.series('copy-accessible-autocomplete-js', 'copy-govuk-frontend-js', 'copy-dfe-frontend-js', 'copy-moj-frontend-js'));

gulp.task('populate-wwwroot', gulp.series('assets', 'copy-wwwroot-remote'));

//todo: delegate from consumer gulp to this gulp?