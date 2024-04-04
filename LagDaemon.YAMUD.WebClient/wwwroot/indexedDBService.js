window.indexedDBService = {
    saveData: async function (key, value) {
        return new Promise((resolve, reject) => {
            const request = window.indexedDB.open('MyDatabase', 1);

            request.onerror = function (event) {
                reject('Error opening database');
            };

            request.onsuccess = function (event) {
                const db = event.target.result;
                const transaction = db.transaction('MyStore', 'readwrite');
                const store = transaction.objectStore('MyStore');

                const putRequest = store.put(value, key);
                putRequest.onsuccess = function (event) {
                    resolve(true);
                };

                putRequest.onerror = function (event) {
                    reject('Error saving data');
                };
            };

            request.onupgradeneeded = function (event) {
                const db = event.target.result;
                const objectStore = db.createObjectStore('MyStore', { keyPath: 'id', autoIncrement: true });
            };
        });
    },

    getData: async function (key) {
        return new Promise((resolve, reject) => {
            const request = window.indexedDB.open('MyDatabase', 1);

            request.onerror = function (event) {
                reject('Error opening database');
            };

            request.onsuccess = function (event) {
                const db = event.target.result;
                const transaction = db.transaction('MyStore', 'readonly');
                const store = transaction.objectStore('MyStore');
                const getRequest = store.get(key);

                getRequest.onsuccess = function (event) {
                    resolve(event.target.result);
                };

                getRequest.onerror = function (event) {
                    reject('Error retrieving data');
                };
            };
        });
    }
};
