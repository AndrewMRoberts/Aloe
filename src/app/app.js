'use strict';

var path = require('path');

var app = angular.module('Aloe', []);

require(path.join(__dirname, 'controllers.js'));
require(path.join(__dirname, 'services.js'));

document.addEventListener('DOMContentLoaded', function() {
    angular.bootstrap(document, ['Aloe']);
});