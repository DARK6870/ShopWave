window.addEventListener("load", () => {
    const loader = document.querySelector(".loader");

    setTimeout(() => {
        loader.classList.add("loader--hidden");

        loader.addEventListener("transitionend", () => {
            loader.remove();
        });
    }, 300);
});
