// This script fetches rows of cryptocurrency data from a table, attempting up to 3 times if necessary.
// It selects only the non-empty cells within each row, ensuring clean and relevant data is collected.

return new Promise((resolve, reject) => {
    function getCryptoRowsFromTable(attempt = 1) {
        const rows = Array.from(document.querySelectorAll('tbody > tr'));
        let rowsData = [];

        rows.forEach(row => {
            // Here we push only non-empty cells' text content
            let rowData = Array.from(row.querySelectorAll('td:not(:empty)')).map(td => td.textContent.trim());
            // Further filter out any remaining empty strings in case :empty didn't catch them
            rowData = rowData.filter(text => text.length > 0);
            rowsData.push(rowData);
        });

        console.log('Attempt:', attempt, 'Data:', rowsData);

        if (rowsData.length > 0 || attempt >= 3) { // If data is found or attempts are 3 or more, resolve or reject
            if (rowsData.length > 0) {
                resolve(rowsData);
            } else {
                reject('Row data could not be loaded after 3 attempts.');
            }
        } else {
            setTimeout(() => getCryptoRowsFromTable(attempt + 1), 2000); // Retry after 2 seconds
        }
    }

    getCryptoRowsFromTable(); // Initial call
});
