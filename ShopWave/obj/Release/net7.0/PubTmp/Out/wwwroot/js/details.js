function change_image(image) {
    var container = document.getElementById("main-image");
    container.src = image.src;
}

document.addEventListener("DOMContentLoaded", function (event) {
    var priceElement = document.getElementById("product-price");
    var quantityElement = document.getElementById("product-quantity");
    var radioButtons = document.querySelectorAll('input[type="radio"][name="VariationId"]');

    function updatePriceAndQuantity() {
        var selectedVariation = null;
        radioButtons.forEach(function (radio) {
            if (radio.checked) {
                selectedVariation = radio;
            }
        });

        if (selectedVariation) {
            var newPrice = selectedVariation.getAttribute("data-price") + " $";
            var newQuantity = selectedVariation.getAttribute("data-quantity");
            priceElement.textContent = newPrice;
            quantityElement.textContent = newQuantity;
        }
    }

    radioButtons.forEach(function (radio) {
        radio.addEventListener("change", updatePriceAndQuantity);
    });

    updatePriceAndQuantity();
});