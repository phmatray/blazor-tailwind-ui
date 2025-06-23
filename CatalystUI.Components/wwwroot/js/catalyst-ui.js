// CatalystUI Blazor Components - JavaScript Interop

window.CatalystUI = {
    // Focus management
    focus: function (element) {
        if (element) {
            element.focus();
        }
    },

    // Trap focus within an element (useful for modals/dialogs)
    trapFocus: function (element) {
        if (!element) return;

        const focusableElements = element.querySelectorAll(
            'a[href], button, textarea, input[type="text"], input[type="radio"], input[type="checkbox"], select, [tabindex]:not([tabindex="-1"])'
        );
        const firstFocusableElement = focusableElements[0];
        const lastFocusableElement = focusableElements[focusableElements.length - 1];

        element.addEventListener('keydown', function (e) {
            if (e.key === 'Tab') {
                if (e.shiftKey) { // Shift + Tab
                    if (document.activeElement === firstFocusableElement) {
                        lastFocusableElement.focus();
                        e.preventDefault();
                    }
                } else { // Tab
                    if (document.activeElement === lastFocusableElement) {
                        firstFocusableElement.focus();
                        e.preventDefault();
                    }
                }
            }
        });

        firstFocusableElement?.focus();
    },

    // Click outside handler
    addClickOutsideHandler: function (element, dotNetReference, methodName) {
        if (!element) return;

        const handler = function (event) {
            if (!element.contains(event.target)) {
                dotNetReference.invokeMethodAsync(methodName);
            }
        };

        document.addEventListener('click', handler);

        // Return a dispose function
        return {
            dispose: function () {
                document.removeEventListener('click', handler);
            }
        };
    },

    // Scroll lock (for modals)
    lockScroll: function () {
        document.body.style.overflow = 'hidden';
    },

    unlockScroll: function () {
        document.body.style.overflow = '';
    },

    // Get element dimensions
    getDimensions: function (element) {
        if (!element) return null;
        
        const rect = element.getBoundingClientRect();
        return {
            width: rect.width,
            height: rect.height,
            top: rect.top,
            left: rect.left,
            bottom: rect.bottom,
            right: rect.right
        };
    },

    // Set CSS variable
    setCssVariable: function (element, name, value) {
        if (element) {
            element.style.setProperty(name, value);
        }
    },

    // Smooth scroll to element
    scrollToElement: function (element, options = {}) {
        if (!element) return;
        
        element.scrollIntoView({
            behavior: options.behavior || 'smooth',
            block: options.block || 'start',
            inline: options.inline || 'nearest'
        });
    },

    // Copy to clipboard
    copyToClipboard: async function (text) {
        try {
            await navigator.clipboard.writeText(text);
            return true;
        } catch (err) {
            console.error('Failed to copy text: ', err);
            return false;
        }
    },

    // Check if element is in viewport
    isInViewport: function (element) {
        if (!element) return false;
        
        const rect = element.getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );
    },

    // Animation helpers
    animate: function (element, keyframes, options) {
        if (!element || !element.animate) return null;
        
        return element.animate(keyframes, options);
    },

    // Table helpers
    checkIsFirstTableCell: function (cellElement) {
        if (cellElement && cellElement.parentElement) {
            // Check if this is the first TD in the row
            const previousSibling = cellElement.previousElementSibling;
            return !previousSibling || previousSibling.tagName !== 'TD';
        }
        return true;
    }
};