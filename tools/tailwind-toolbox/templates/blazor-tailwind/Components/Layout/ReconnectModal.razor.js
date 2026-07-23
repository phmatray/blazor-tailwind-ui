(() => {
    const reconnectModal = document.getElementById('components-reconnect-modal');
    const reconnectShow = 'components-reconnect-show';
    const reconnectFailed = 'components-reconnect-failed';
    const reconnectRejected = 'components-reconnect-rejected';

    const maxRetries = 8;
    const retryIntervalMilliseconds = 2000;
    let currentAttempt = 0;

    Blazor.start({
        reconnectionHandler: {
            onConnectionDown: () => showReconnectionUI(),
            onConnectionUp: () => hideReconnectionUI()
        }
    });

    function showReconnectionUI() {
        if (reconnectModal) {
            reconnectModal.classList.add(reconnectShow);
        }
        currentAttempt = 0;
        attemptReconnect();
    }

    function hideReconnectionUI() {
        if (reconnectModal) {
            reconnectModal.classList.remove(reconnectShow, reconnectFailed, reconnectRejected);
        }
    }

    async function attemptReconnect() {
        currentAttempt++;

        if (currentAttempt > maxRetries) {
            if (reconnectModal) {
                reconnectModal.classList.remove(reconnectShow);
                reconnectModal.classList.add(reconnectFailed);
            }
            return;
        }

        await new Promise(resolve => setTimeout(resolve, retryIntervalMilliseconds));

        try {
            await Blazor.reconnect();
            hideReconnectionUI();
        } catch {
            attemptReconnect();
        }
    }

    window.Blazor = window.Blazor || {};
    window.Blazor.reconnect = async () => {
        hideReconnectionUI();
        showReconnectionUI();
    };
})();
