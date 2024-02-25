// This script continuously scrolls to the bottom of a webpage until all dynamic content has loaded.
// We're using it to load additional content that is loaded on scrolling (lazy loading?), ensuring all data is visible for processing.

function checkRowLoaded(row) {
    // Check if a specific cell in the row that should have content is empty
    // Replace 'td:nth-child(n)' with the appropriate child index that should have content
    return row.querySelector('td:nth-child(3) div') !== null;
}

function areAllRowsLoaded() {
    const rows = Array.from(document.querySelectorAll('tbody > tr'));
    // Check if all rows are loaded based on the third cell having content
    return rows.every(checkRowLoaded);
}

function scrollToLoadAllItems() {
    let lastScrollTop = document.documentElement.scrollTop;
    const scrollInterval = setInterval(() => {
        window.scrollBy(0, 1000); // Scroll by 100 pixels
        const currentScrollTop = document.documentElement.scrollTop;

        if (lastScrollTop === currentScrollTop) {
            if (areAllRowsLoaded()) {
                console.log('All rows loaded.');
                clearInterval(scrollInterval);
                // Once scrolling is complete, set a flag
                window.scrollComplete = true;
            } else {
                console.log('Not all rows loaded yet. Scrolling more...');
                window.scrollBy(0, 1000);
            }
        } else {
            lastScrollTop = currentScrollTop;
            console.log(`Scrolled to position: ${lastScrollTop}`);
        }
    }, 500);
}
scrollToLoadAllItems();