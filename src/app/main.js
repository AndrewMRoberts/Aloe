const electron = require('electron');
const contextMenu = require('electron-context-menu')
const path = require('path');
const url = require('url');
const os = require('os');
const proc = require('child_process');

const app = electron.app;
const BrowserWindow = electron.BrowserWindow;
contextMenu({
    prepend: (params, browserWindow) => [{
        label: 'Rainbow',
        isible: params.mediaType === 'image'
    }]
});

let mainWindow;

/* Constants */
let width = 1280;
let height = 720;

var apiProcess = null;

app.on('ready', startApi);

app.on('window-all-closed', function() {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', function() {
    if (mainWindow == null) {
        createWindow();
    }
});

process.on('exit', function() {
    writeLog('exit');
    if (apiProcess) {
        apiProcess.kill();
    }
});

function createWindow() {
    mainWindow = new BrowserWindow({
        width: width, 
        height: height,
        webPreferences: {
            webSecurity: false
        }
    });

    mainWindow.loadURL(url.format({
        pathname: path.join(__dirname, 'Views/Overview/overview.html'),
        protocol: 'file:',
        slashes: true
    }));

    mainWindow.webContents.openDevTools();

    mainWindow.on('closed', function() {
        mainWindow = null;
    });
}

function startApi() {
    var process = proc.spawn;
    // var apiPath = path.join(__dirname, '../api/bin/Debug/netcoreapp1.1/api.dll');

    // apiProcess = process(apiPath);

    // apiProcess.stdout.on('data', (data) => {
    //     writeLog('stdout: $(data)');
    //     if (mainWindow == null) {
    //         createWindow();
    //     }
    // });

    if (mainWindow == null) {
        createWindow();
    }
}

function writeLog(msg) {
    console.log(msg);
}