// This script randomly selects a specified number of rows from a table and clicks the 'star' icon in each.
// It aims to simulate user interaction for adding cryptocurrencies to a watchlist or favorites.

return new Promise((resolve) => {
    function pickRandomRowsAndClickStars() {
        const rows = Array.from(document.querySelectorAll('tbody > tr'));
        let selectedRowsData = [];
        let randomIndices = new Set();

        const numberOfRowsToSelect = Math.floor(Math.random() * 6) + 5; // This will give you 5, 6, 7, 8, 9, or 10    

        while (randomIndices.size < numberOfRowsToSelect) {
            let randomIndex = Math.floor(Math.random() * rows.length);
            randomIndices.add(randomIndex);
        }

        randomIndices.forEach(index => {
            const row = rows[index];
            clickStarButton(row);

            let rowData = Array.from(row.querySelectorAll('td:not(:empty)')).map(td => td.textContent.trim());
            // Further filter out any remaining empty strings in case :empty didn't catch them
            rowData = rowData.filter(text => text.length > 0);

            selectedRowsData.push(rowData);
        });

        console.log('resolving with rows');
        resolve(selectedRowsData);
    }

    function clickStarButton(row) {
        const starButton = row.querySelector('td:first-child .icon-Star');
        if (starButton) starButton.click();
    }

    pickRandomRowsAndClickStars();
});