
(function () {
    "use strict";

    if ("scrollRestoration" in history) {
        history.scrollRestoration = "manual";
    }

    function scrollToTop() {
        window.scrollTo(0, 0);

        document.documentElement.scrollTop = 0;
        document.body.scrollTop = 0;

        document.querySelectorAll(
            "main, article, .content, .page, .overflow-auto"
        ).forEach(element => {
            element.scrollTop = 0;
        });
    }

    function resetScroll() {
        scrollToTop();

        requestAnimationFrame(() => {
            scrollToTop();

            requestAnimationFrame(scrollToTop);
        });
    }

    window.footballMatchesScrollToTop = resetScroll;

    document.addEventListener("enhancedload", resetScroll);

    window.addEventListener("popstate", resetScroll);

    document.addEventListener("click", function (event) {
        const link = event.target.closest("a[href]");

        if (!link) {
            return;
        }

        const url = new URL(link.href, window.location.href);

        if (
            url.origin === window.location.origin &&
            url.pathname === "/footballmatches" &&
            url.searchParams.has("page")
        ) {
            resetScroll();
        }
    });

    window.addEventListener("load", resetScroll);
})();
