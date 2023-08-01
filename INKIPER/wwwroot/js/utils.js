

function initInactivityTimer(dotnetHelper, millseconds) {
    let timer;
    document.onmousemove = resetTimer;
    document.onkeydown = resetTimer;
    
    function resetTimer() {
        clearTimeout(timer)
        timer = setTimeout(logout, millseconds)
    }
    
    function logout() {
        dotnetHelper.invokeMethodAsync("Logout")
    }
}