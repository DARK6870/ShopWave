function setBannerImage() {
    var bannerImage = document.getElementById("bannerImage");
    var windowWidth = window.innerWidth;

    if (windowWidth < 500) {
        bannerImage.src = "/img/main_mobile_ru.webp";
    } else {
        bannerImage.src = "/img/main_ru.webp";
    }
}

window.addEventListener("load", setBannerImage);
window.addEventListener("resize", setBannerImage);