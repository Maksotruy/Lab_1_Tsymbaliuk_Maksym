
(function () {
    if (window.footballMatchesScrollInitialized) {
        return;
    }

    window.footballMatchesScrollInitialized = true;

    function scrollToTop() {
        window.scrollTo(0, 0);
        document.documentElement.scrollTop = 0;
        document.body.scrollTop = 0;

        const main = document.getElementById("main-content");
        const content = document.getElementById("page-content");

        if (main) main.scrollTop = 0;
        if (content) content.scrollTop = 0;
    }

    function resetScroll() {
        scrollToTop();

        requestAnimationFrame(() => {
            scrollToTop();
            requestAnimationFrame(scrollToTop);
        });
    }

    window.footballMatchesScrollToTop = resetScroll;

    // Після кожного завершеного переходу Blazor.
    if (typeof Blazor !== "undefined") {
        Blazor.addEventListener("enhancedload", resetScroll);
    }

    // Відстеження натискань на всі пункти меню.
    document.addEventListener("click", function (event) {
        const link = event.target.closest(".sidebar a[href]");

        if (!link) return;

        resetScroll();
    });

    // Назад / Вперед у браузері.
    window.addEventListener("popstate", resetScroll);
})();
