var item = [];
$('.hidden-value').each(function () {
    var productId = $(this).data('product-id');
    var shopId = $(this).data('shop-id');
    var shopName = $(this).data('shop-name');
    var productName = $(this).data('product-name');
    var totalQuantity = $(this).data('total-quantity');
    var productPrice = $(this).data('product-price');

    item.push({
        productId: productId,
        shopId: shopId,
        shopName: shopName,
        productName: productName,
        totalQuantity: totalQuantity,
        productPrice: productPrice
    });
});

document.querySelectorAll('.plus').forEach((button, index) => {
    button.addEventListener('click', () => {
        var num = button.parentElement.querySelector('.num');
        var currentQuantity = parseInt(num.textContent);

        var origPrice = item[index].productPrice; // Use index directly from the forEach loop
        console.log("Original: ", origPrice);

        currentQuantity++;
        num.textContent = currentQuantity;
        console.log("Plus button - Index: " + index + ", New quantity: " + currentQuantity);

        var newPrice = updatePriceOfProduct(currentQuantity, origPrice);
        console.log("The new price is: " + newPrice);

        var displayTotal = button.parentElement.nextElementSibling.querySelector('.displayTotal');
        displayTotal.textContent = "₱ " + newPrice.toFixed(2);
    });
});


document.querySelectorAll('.minus').forEach((button, index) => {
    button.addEventListener('click', () => {
        var num = button.parentElement.querySelector('.num');
        var currentQuantity = parseInt(num.textContent);

        var origPrice = item[index].productPrice; // Use index directly from the forEach loop
        console.log("Original: ", origPrice);

        if (currentQuantity > 1) {
            currentQuantity--;
            num.textContent = currentQuantity;
            console.log("Minus button - Index: " + index + ", New quantity: " + currentQuantity);

            var newPrice = updatePriceOfProduct(currentQuantity, origPrice);
            console.log("The new price is: " + newPrice);

            var displayTotal = button.parentElement.nextElementSibling.querySelector('.displayTotal');
            displayTotal.textContent = "₱ " + newPrice.toFixed(2);
        }
    });
});


function updatePriceOfProduct(newQuantity, originalPrice) {
    console.log("Your in update Price");

    var newPrice = parseFloat(originalPrice) * parseInt(newQuantity);
    //console.log("The new price is: " + newPrice);

    return newPrice;
}