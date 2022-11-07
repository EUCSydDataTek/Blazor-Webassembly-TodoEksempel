function Vibrate(TimeMs) {
    console.log("vibrating for " + TimeMs + "ms.");
    window.navigator.vibrate(TimeMs)
}

function GetBrowserLanguage() {
    console.log("Getting language from browser")
    return window.navigator.language
}