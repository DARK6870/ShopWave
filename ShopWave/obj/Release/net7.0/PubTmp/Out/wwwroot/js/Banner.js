function setBannerImage() {
    var bannerImage = document.getElementById("bannerImage");
    var windowWidth = window.innerWidth;

    if (windowWidth < 500) {
        bannerImage.src = "/img/main_mobile_ru.jpg";
    } else {
        bannerImage.src = "/img/main_ru.png";
    }
}

// Вызывайте функцию setBannerImage() при загрузке страницы и изменении размера окна
window.addEventListener("load", setBannerImage);
window.addEventListener("resize", setBannerImage);