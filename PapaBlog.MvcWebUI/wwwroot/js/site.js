function convertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
}

function convertToShortDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('tr-TR', { year: 'numeric', month: '2-digit', day: '2-digit' });
    return shortDate;
}