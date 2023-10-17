window.addEventListener("load", () => {
    const loader = document.querySelector(".loader");

    document.body.appendChild(loader);

    setTimeout(() => {
        loader.classList.add("loader--hidden");
    }, 300);
});
