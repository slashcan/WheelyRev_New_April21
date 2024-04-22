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

        console.log("---- ", item[index].totalQuantity);

        currentQuantity++;
        num.textContent = currentQuantity; //Display in the page ang quantity
        console.log("Plus button - Index: " + index + ", New quantity: " + currentQuantity);

        var newPrice = updatePriceOfProduct(currentQuantity, origPrice);
        console.log("The new price is: " + newPrice);

        var displayTotal = button.parentElement.nextElementSibling.querySelector('.displayTotal');
        displayTotal.textContent = "₱ " + newPrice.toFixed(2);

        

        item[index].totalQuantity = currentQuantity;
        updateTotalPrice();

        var nQ = item[index].totalQuantity = currentQuantity;
        console.log("+++++++++ ", nQ);

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
            num.textContent = currentQuantity; //Display sa page ang quantity
            console.log("Minus button - Index: " + index + ", New quantity: " + currentQuantity);

            var newPrice = updatePriceOfProduct(currentQuantity, origPrice);
            console.log("The new price is: " + newPrice);

            var displayTotal = button.parentElement.nextElementSibling.querySelector('.displayTotal');
            displayTotal.textContent = "₱ " + newPrice.toFixed(2);


            // Update the totalSum element
            //var totalSumElement = document.getElementById('totalSum');
            //totalSumElement.textContent = "₱ " + calculateTotalPrice();

            item[index].totalQuantity = currentQuantity;
            updateTotalPrice();

            var nQ = item[index].totalQuantity = currentQuantity;
            console.log("+-+-+-+-+", nQ);
        }
    });
});


function updatePriceOfProduct(newQuantity, originalPrice) {
    console.log("Your in update Price");

    var newPrice = parseFloat(originalPrice) * parseInt(newQuantity);
    //console.log("The new price is: " + newPrice);

    return newPrice;
}

function updateTotalPrice() {
    var totalPrice = 0;
    item.forEach(item => {
        totalPrice += parseFloat(item.productPrice) * parseInt(item.totalQuantity);
    });

    document.getElementById('totalSum').textContent = "₱ " + totalPrice.toFixed(2);
    document.getElementById('CheckOut').textContent = "₱ " + totalPrice.toFixed(2);;
}