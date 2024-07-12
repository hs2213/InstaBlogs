export function GetSelectedText() {
    const textarea = document.getElementById('typed-content');
    
    return textarea.value.substring(textarea.selectionStart, textarea.selectionEnd);
}