window.isDarkMode = function () {
    return localStorage.getItem('darkMode') === 'true' ||
        (!localStorage.getItem('darkMode') && window.matchMedia('(prefers-color-scheme: dark)').matches);
}

window.setDarkMode = function (isDark) {
    localStorage.setItem('darkMode', isDark);
}

// Explanation:
// 1. isDarkMode function:
//    - First checks if 'darkMode' is explicitly set to 'true' in localStorage
//    - If not set, it then checks the system preference using matchMedia
//    - This provides a fallback to system preference if the user hasn't made a choice
//
// 2. setDarkMode function:
//    - Simply sets the 'darkMode' item in localStorage
//    - This allows the user's choice to persist across page reloads