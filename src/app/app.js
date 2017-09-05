'use strict';

var path = require('path');
var requireDir = require('require-dir');

var app = angular.module('Aloe', []);

requireDir(path.join(__dirname, '../Controllers'));
requireDir(path.join(__dirname, '../Services'));

document.addEventListener('DOMContentLoaded', function() {
    angular.bootstrap(document, ['Aloe']);
});